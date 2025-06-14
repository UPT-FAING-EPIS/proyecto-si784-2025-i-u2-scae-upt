using MySql.Data.MySqlClient;
using SCAE_UPT.Config;
using SCAE_UPT.Entidad;
using System;
using System.Data;
using System.Windows.Forms;

namespace SCAE_UPT.Negocio
{
    public class clsNegocioRegistro
    {
        clsConfigConexion Conexion = new clsConfigConexion();

        public bool MtdGuardarIngresoUpetino(clsEntidadRegistro objEntRegistro)
        {
            string sql = "INSERT INTO tbregistro (DNI, FechaHora_Ingreso, IDUsuario_Ingreso) VALUES (@DNI, @FechaHora_Ingreso, @IDUsuario_Ingreso)";
            MySqlConnection con = null;
            MySqlCommand cmd = null;

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", objEntRegistro.DNI);
                cmd.Parameters.AddWithValue("@FechaHora_Ingreso", objEntRegistro.FechaHora_Ingreso);
                cmd.Parameters.AddWithValue("@IDUsuario_Ingreso", objEntRegistro.IDUsuario_Ingreso);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
                return false;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }

        public bool MtdGuardarSalidaUpetino(clsEntidadRegistro objEntRegistroVisitante)
        {
            string sql = "UPDATE tbregistro SET FechaHora_Salida = @FechaHora_Salida, IDUsuario_Salida = @IDUsuario_Salida WHERE DNI = @DNI AND FechaHora_Salida IS NULL";
            MySqlConnection con = null;
            MySqlCommand cmd = null;

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", objEntRegistroVisitante.DNI);
                cmd.Parameters.AddWithValue("@FechaHora_Salida", objEntRegistroVisitante.FechaHora_Salida);
                cmd.Parameters.AddWithValue("@IDUsuario_Salida", objEntRegistroVisitante.IDUsuario_Salida);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
                return false;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }

        public DataTable ListaRegistro()
        {
            string sql = "SELECT r.ID_registro, r.DNI, p.Nombre, p.Apellido, p.ID_escuela, r.FechaHora_Ingreso, r.FechaHora_Salida " +
                         "FROM tbregistro AS r JOIN tbpersona AS p ON p.DNI = r.DNI";
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return dt;
            }
            finally
            {
                if (adapter != null) adapter.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }

        public DataTable MtdBuscarRegistro(string DNI)
        {
            string sql = "SELECT FechaHora_Ingreso, FechaHora_Salida FROM tbregistro WHERE DNI = @DNI ORDER BY FechaHora_Ingreso DESC LIMIT 1";
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", DNI);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return dt;
            }
            finally
            {
                if (adapter != null) adapter.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }

        public DataTable MtdBuscarPersonaPorDNI(string DNI)
        {
            string sql = "SELECT p.DNI, p.Nombre, p.Apellido, p.Foto, p.Telefono FROM tbpersona p WHERE p.DNI = @DNI";
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", DNI);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (adapter != null) adapter.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }

        public bool MtdCompararToken(string Token, string DNI)
        {
            string sql = "SELECT TOKEN FROM tbtoken WHERE DNI_TOKEN = @DNI";
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", DNI);
                reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
            }
        }

        public void MtdEliminarToken(string DNI)
        {
            string sql = "UPDATE tbtoken SET TOKEN = 'a'  WHERE DNI_TOKEN = @DNI";
            MySqlConnection con = null;
            MySqlCommand cmd = null;

            try
            {
                con = Conexion.GetConnection();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DNI", DNI);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.ToString());
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }
    }
}
