using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using SCAE_UPT.Config;
using SCAE_UPT.Entidad;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCAE_UPT.Negocio
{
    public class clsNegocioVisitante
    {
        private static readonly HttpClient client = new HttpClient();
        clsConfigConexion Conexion = new clsConfigConexion();
        MySqlConnection con = null;

        public bool MtdGuardarVisitante(clsEntidadVisitante objEntVisitante)
        {
            String sqlQuery = "Insert into tbvisitante (DNI,Nombre,Apellido,Telefono) VALUES(@DNI,@Nombre,@Apellido,@Telefono)";

            try
            {
                con = Conexion.GetConnection();
                MySqlCommand sqlCommandExecuter = new MySqlCommand(sqlQuery, con);
                sqlCommandExecuter.Parameters.AddWithValue("@DNI", objEntVisitante.DNI);
                sqlCommandExecuter.Parameters.AddWithValue("@Nombre", objEntVisitante.Nombre);
                sqlCommandExecuter.Parameters.AddWithValue("@Apellido", objEntVisitante.Apellido);
                sqlCommandExecuter.Parameters.AddWithValue("@Telefono", objEntVisitante.Telefono);

                sqlCommandExecuter.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return false;
            }
        }

        public DataTable MtdBuscarVisitante(String DNI)
        {
            String sqlQuery = "Select DNI,Nombre,Apellido,Telefono from tbvisitante where DNI=@DNI";
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



        public async Task<string[]> ConsultarDNI(string dni)
        {
            string[] datosPersona = new string[3]; // [nombres, apellidoPaterno, apellidoMaterno]
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InJlbnpvYW50YXlodWE3QGdtYWlsLmNvbSJ9.p9lyrwOzyi8AiF23Z-j-DOhA7xheGVbUpiMg5dNhaQk"; // El token proporcionado

            try
            {
                string url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token={token}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta del servidor ({dni}): {responseBody}");

                    JObject jsonResponse = JObject.Parse(responseBody);

                    if (jsonResponse.ContainsKey("nombres") &&
                        jsonResponse.ContainsKey("apellidoPaterno") &&
                        jsonResponse.ContainsKey("apellidoMaterno"))
                    {
                        datosPersona[0] = jsonResponse["nombres"]?.ToString();
                        datosPersona[1] = jsonResponse["apellidoPaterno"]?.ToString();
                        datosPersona[2] = jsonResponse["apellidoMaterno"]?.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Los datos esperados no se encontraron en la respuesta JSON.");
                    }
                }
                else
                {
                    Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción: " + e.Message);
            }

            return datosPersona;
        }

        public async Task<string[]> ConsultarCarnetExtranjeria(string carnetExtranjeria)
        {
            string url = $"https://api.factiliza.com/pe/v1/cee/info/{carnetExtranjeria}";
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzODEwNCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImNvbnN1bHRvciJ9.n6qzs6YekPVa80d8k2BamUBXZ8tAwerf53Cw7VKtKkM";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    JObject json = JObject.Parse(jsonResponse);

                    string nombres = json["nombres"]?.ToString() ?? "";
                    string apellidoPaterno = json["apellido_paterno"]?.ToString() ?? "";
                    string apellidoMaterno = json["apellido_materno"]?.ToString() ?? "";

                    return new string[] { nombres, apellidoPaterno, apellidoMaterno };
                }
                else
                {
                    Console.WriteLine("Error: " + jsonResponse);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                return null;
            }
        }
    }
}
