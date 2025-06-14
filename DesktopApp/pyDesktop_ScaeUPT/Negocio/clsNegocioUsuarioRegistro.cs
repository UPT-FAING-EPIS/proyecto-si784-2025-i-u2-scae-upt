using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCAE_UPT.Config;
using MySql.Data;
using MySql.Data.MySqlClient;
using SCAE_UPT.Entidad;
using System.CodeDom;
using System.Security.Cryptography;
using System.Data.Common;
using System.Windows.Forms;


namespace SCAE_UPT.Negocio
{
    public class clsNegocioUsuarioRegistro
    {
        clsConfigConexion Conexion = new clsConfigConexion();
        MySqlConnection con = null;
        
        public bool MtdGuardarIngreso(clsEntidadUsuarioRegistro objEntidadUsuarioRegistro)
        {
            string sqlQuery = "INSERT INTO tbusuario_registro (ID_empleado, FechaHoraIngreso, TipoEntrada, TiempoTardanza) VALUES (@ID_empleado, @FechaHoraIngreso, @TipoEntrada, @TiempoTardanza)";
            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery,con);
                sqlCommandExecuter.Parameters.AddWithValue("@ID_empleado",objEntidadUsuarioRegistro.ID_Empleado);
                sqlCommandExecuter.Parameters.AddWithValue("@FechaHoraIngreso",objEntidadUsuarioRegistro.FechaHora_Ingreso);

                TimeSpan horaEsperada = new TimeSpan(8,0,0);
                TimeSpan horaIngreso = objEntidadUsuarioRegistro.FechaHora_Ingreso.Value.TimeOfDay;

                string tipoEntrada;
                TimeSpan? tiempoTardanza = null;

                if (horaIngreso <= horaEsperada)
                {
                    tipoEntrada = "Puntual";
                }
                else
                {
                    TimeSpan duracionTardanza = horaIngreso - horaEsperada;
                    long minutosTardanza = (long)duracionTardanza.TotalMinutes;

                    if (minutosTardanza <= 30)
                    {
                        tipoEntrada = "Tardanza";
                        tiempoTardanza = new TimeSpan(0, (int)minutosTardanza, 0);
                    }
                    else
                    {
                        tipoEntrada = "Falta";
                    }
                }

                sqlCommandExecuter.Parameters.AddWithValue("@TipoEntrada", tipoEntrada);
                if (tiempoTardanza.HasValue)
                {
                    sqlCommandExecuter.Parameters.AddWithValue("@TiempoTardanza", tiempoTardanza);
                }else
                {
                    sqlCommandExecuter.Parameters.AddWithValue("@TiempoTardanza", DBNull.Value);
                }
                
                sqlCommandExecuter.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return false;
            }
        }


        public bool MtdGuardarSalida(clsEntidadUsuarioRegistro objEntidadUsuarioRegistro)
        {
            String sqlQuery = "UPDATE tbusuario_registro SET FechaHoraSalida = @FechaHoraSalida WHERE ID_empleado = @ID_empleado AND FechaHoraSalida IS NULL";
            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@FechaHoraSalida", objEntidadUsuarioRegistro.ID_Empleado);
                sqlCommandExecuter.Parameters.AddWithValue("@ID_empleado", objEntidadUsuarioRegistro.FechaHora_Ingreso);

                sqlCommandExecuter.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return false;
            }
        }
    }
}
