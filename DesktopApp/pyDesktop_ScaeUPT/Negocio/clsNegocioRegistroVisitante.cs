using MySql.Data.MySqlClient;
using SCAE_UPT.Config;
using SCAE_UPT.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCAE_UPT.Negocio
{
    public class clsNegocioRegistroVisitante
    {
        clsConfigConexion Conexion = new clsConfigConexion();
        MySqlConnection con = null;

        public DataTable ListaRegistroVisitante()
        {
            String sqlQuery = "Select r.ID_registro,r.DNI,p.Nombre,p.Apellido,r.FechaHora_Ingreso,r.FechaHora_Salida from tbregistro_visitante as r "
                + "JOIN tbvisitante as p on p.DNI = r.DNI";
            DataTable dataTable = new DataTable();
            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);

                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommandExecuter);
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
            }
            return dataTable;
        }


        public DataTable MtdBuscarRegistroVisitante(String DNI)
        {
            String sqlQuery = "SELECT FechaHora_Ingreso, FechaHora_Salida "
                + "FROM tbregistro_visitante "
                + "WHERE DNI = @DNI "
                + "ORDER BY FechaHora_Ingreso DESC "
                + "LIMIT 1";
            DataTable dataTable = new DataTable();
            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@DNI", DNI);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommandExecuter);
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable;
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
            }
            return dataTable;
        }


        public bool MtdGuardarIngresoVisitante(clsEntidadRegistroVisitante objEntRegistroVisitante)
        {
            String sqlQuery = "Insert into tbregistro_visitante (DNI,FechaHora_Ingreso, IDUsuario_Ingreso) VALUES(@DNI,@FechaHora_Ingreso,@IDUsuario_Ingreso)";

            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@DNI", objEntRegistroVisitante.DNI);
                sqlCommandExecuter.Parameters.AddWithValue("@FechaHora_Ingreso", objEntRegistroVisitante.FechaHora_Ingreso);
                sqlCommandExecuter.Parameters.AddWithValue("@IDUsuario_Ingreso", objEntRegistroVisitante.IDUsuario_Ingreso);

                sqlCommandExecuter.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
                return false;
            }
        }

        public bool MtdGuardarSalidaVisitante(clsEntidadRegistroVisitante objEntRegistroVisitante)
        {
            String sqlQuery = "UPDATE tbregistro_visitante set FechaHora_Salida=@FechaHora_Salida, IDUsuario_Salida=@IDUsuario_Salida where DNI=@DNI and FechaHora_Salida IS NULL";


            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@DNI", objEntRegistroVisitante.DNI);
                sqlCommandExecuter.Parameters.AddWithValue("@FechaHora_Salida", objEntRegistroVisitante.FechaHora_Salida);
                sqlCommandExecuter.Parameters.AddWithValue("@IDUsuario_Salida", objEntRegistroVisitante.IDUsuario_Salida);

                sqlCommandExecuter.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
                return false;
            }
        }
    }
}
