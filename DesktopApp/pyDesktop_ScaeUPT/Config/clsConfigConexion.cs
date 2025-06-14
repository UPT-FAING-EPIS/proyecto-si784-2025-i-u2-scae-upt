using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCAE_UPT.Config
{
    public class clsConfigConexion
    {
        private MySqlConnection con;
        public clsConfigConexion()
        {
            try
            {
                string connectionString = "server=161.132.68.132;port=3306;database=dbscae;user=root;password=Upt2025"; //deploy
                //string connectionString = "server=localhost;port=3306;database=dbscae;user=root;password="; //localtesting
                con = new MySqlConnection(connectionString);
                con.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
            }
        }
        public MySqlConnection GetConnection()
        {
            return con;
        }
    }
}
