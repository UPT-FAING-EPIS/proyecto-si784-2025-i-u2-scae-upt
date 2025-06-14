using MySql.Data.MySqlClient;
using SCAE_UPT.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCAE_UPT.Negocio
{
    public class clsNegocioUsuario
    {
        clsConfigConexion Conexion = new clsConfigConexion();
        MySqlConnection con = null;

        public DataTable MtdIniciarSesionToken(String Token)
        {
            String sqlQuery = "SELECT p.DNI, p.Nombre, p.Apellido,u.Tipo ,u.Estado, u.Usuario "
                + "FROM tbpersona p "
                + "INNER JOIN tbempleado e ON p.DNI = e.ID_empleado "
                + "INNER JOIN tbusuario u ON e.ID_empleado = u.ID_empleado "
                + "INNER JOIN tbtoken t ON p.DNI = t.DNI_TOKEN "
                + "WHERE t.TOKEN = @Token AND u.Estado = 1";
            DataTable dataTable = new DataTable();

            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@Token", Token);

                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommandExecuter);
                adapter.Fill(dataTable);

                if(dataTable.Rows.Count > 0)
                {
                    return dataTable;
                }
            }
            catch (MySqlException ex) 
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            return dataTable;
        }

        public DataTable MtdIniciarSesionOficina(String usuario, String clave)
        {
            String sqlQuery = "SELECT u.ID_Usuario, p.DNI, p.Nombre, p.Apellido,u.Tipo ,u.Estado, u.Usuario "
                + "FROM tbpersona p "
                + "INNER JOIN tbempleado e ON p.DNI = e.ID_empleado "
                + "INNER JOIN tbusuario u ON e.ID_empleado = u.ID_empleado "
                + "WHERE u.Usuario = @Usuario AND u.Clave = @Clave AND u.Estado = 1";
            DataTable dataTable = new DataTable();

            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@Usuario",usuario);
                sqlCommandExecuter.Parameters.AddWithValue("@Clave",clave);

                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommandExecuter);
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            return dataTable;
        }


    }
}
