using MySql.Data.MySqlClient;
using SCAE_UPT.Entidad;
using SCAE_UPT.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace SCAE_UPT.Presentacion
{
    public partial class frmRegistrarUpetino : Form
    {
        
        private readonly string _encryptionKey = "ScaeUPT2024SecretKey123456789012345";
        public frmRegistrarUpetino()
        {
            InitializeComponent();
            ListarRegistroUpetino();
            LimpiarTexto();
            this.FormClosing += new FormClosingEventHandler(OnFormClosing);
        }



        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Estás seguro de que deseas salir?",
            "Confirmar salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                clsEntidadUsuarioRegistro objEntUsuarioRegistro = new clsEntidadUsuarioRegistro();
                clsNegocioUsuarioRegistro objNegUsuarioRegistro = new clsNegocioUsuarioRegistro();
                DateTime fechaHoraActual = DateTime.Now;

                objEntUsuarioRegistro.ID_Empleado = frmLoginGuardiania.usuarioLogueado[0];
                objEntUsuarioRegistro.FechaHora_Salida = fechaHoraActual;

                objNegUsuarioRegistro.MtdGuardarSalida(objEntUsuarioRegistro);

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ListarRegistroUpetino()
        {
            clsNegocioRegistro objNegRegistroVisitante = new clsNegocioRegistro();

            DataTable dataTable = objNegRegistroVisitante.ListaRegistro();

            dgvHistorialRegistroUpetino.DataSource = dataTable;
        }

        private void LimpiarTexto()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            pcbFoto.Image = null;
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            frm.Show();
            this.Hide();
        }

        private void RegistrarUpetino(string token)
        {
            clsNegocioRegistro objNegRegistro = new clsNegocioRegistro();

            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return;

                DataTable dtPersona = objNegRegistro.MtdBuscarPersonaPorDNI(token);

                if (dtPersona == null || dtPersona.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró ninguna persona con ese token.");
                    return;
                }

                // Solo debería haber una fila
                DataRow row = dtPersona.Rows[0];

                string dni = row["DNI"].ToString();
                string nombre = row["Nombre"].ToString();
                string apellido = row["Apellido"].ToString();
                string telefono = row["Telefono"].ToString();

                byte[] fotoBytes = row["Foto"] as byte[];

                txtNombre.Text = nombre;
                txtApellido.Text = apellido;
                MostrarFoto(fotoBytes);

                ProcesarRegistro(dni, telefono);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error MySQL: " + ex.Message);
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }


        private void ProcesarRegistro(string dni, string telefono)
        {
            clsNegocioRegistro objNegRegistro = new clsNegocioRegistro();

            DataTable dtRegistro = objNegRegistro.MtdBuscarRegistro(dni);

            if (dtRegistro.Rows.Count > 0)
            {
                DataRow registro = dtRegistro.Rows[0];
                var fechaSalida = registro["FechaHora_Salida"] as DateTime?;

                if (fechaSalida == null)
                    RegistrarSalida(dni);
                else
                    RegistrarIngreso(dni);
            }
            else
            {
                RegistrarIngreso(dni);
            }
        }

        private void RegistrarIngreso(string dni)
        {
            clsNegocioRegistro objNegRegistro = new clsNegocioRegistro();

            var entidad = new clsEntidadRegistro
            {
                DNI = dni,
                FechaHora_Ingreso = DateTime.Now,
                IDUsuario_Ingreso = frmLoginGuardiania.usuarioLogueado[0]
            };

            if (objNegRegistro.MtdGuardarIngresoUpetino(entidad))
            {
                objNegRegistro.MtdEliminarToken(dni);
                ListarRegistroUpetino();
                MessageBox.Show("Registro del usuario correcto!");
                LimpiarTexto();
            }
            else
            {
                MessageBox.Show("Error al registrar el ingreso.");
            }
        }

        private void RegistrarSalida(string dni)
        {
            clsNegocioRegistro objNegRegistro = new clsNegocioRegistro();

            var entidad = new clsEntidadRegistro
            {
                DNI = dni,
                FechaHora_Salida = DateTime.Now,
                IDUsuario_Salida = frmLoginGuardiania.usuarioLogueado[0]
            };

            if (objNegRegistro.MtdGuardarSalidaUpetino(entidad))
            {
                objNegRegistro.MtdEliminarToken(dni);
                ListarRegistroUpetino();
                MessageBox.Show("Salida registrada!");
                LimpiarTexto();
            }
            else
            {
                MessageBox.Show("Error al registrar la salida.");
            }
        }

        private void MostrarFoto(byte[] fotoBytes)
        {
            if (fotoBytes == null || fotoBytes.Length == 0)
                return;

            using (var ms = new MemoryStream(fotoBytes))
            {
                try
                {
                    var image = Image.FromStream(ms);
                    var scaledImage = new Bitmap(image, new Size(307, 249));
                    pcbFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    pcbFoto.Image = scaledImage; // Usa tu PictureBox aquí
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al mostrar la foto: " + ex.Message);
                }
            }
        }


        private string DecryptData(string encryptedData)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                string decryptedData;

                using (Aes aes = Aes.Create())
                {
                    // Derivar clave y vector de inicialización (IV) usando PBKDF2
                    using (var deriveBytes = new Rfc2898DeriveBytes(_encryptionKey, new byte[16], 1000))
                    {
                        aes.Key = deriveBytes.GetBytes(32); // 256 bits
                        aes.IV = deriveBytes.GetBytes(16);  // 128 bits
                    }

                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Crear desencriptador
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    // Desencriptar los datos
                    using (var ms = new MemoryStream(encryptedBytes))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                decryptedData = sr.ReadToEnd();
                            }
                        }
                    }
                }

                return decryptedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desencriptar datos: "+ ex);
                throw;
            }
        }

        private string EscanearQR()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg;*.bmp";
                openFileDialog.Title = "Seleccionar imagen con código QR";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                        BarcodeReader reader = new BarcodeReader();
                        var result = reader.Decode(bitmap);

                        return result.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al leer la imagen: " + ex.Message, "Error");
                    }
                }
                return null;
            }
        }

        private void btnEscanearQR_Click(object sender, EventArgs e)
        {
            clsNegocioRegistro objNegRegistro = new clsNegocioRegistro();
            string tokenEncriptado = EscanearQR();

            if(tokenEncriptado == null)
            {
                return;
            }

            string tokenDesencriptado = DecryptData(tokenEncriptado);

            string[] tokenSeparado = tokenDesencriptado.Split('|');

            if (ValidarHora(tokenSeparado[1])==false)
            {
                MessageBox.Show("QR Expirado");
                objNegRegistro.MtdEliminarToken(tokenSeparado[0]);
                return;
            }

            if (objNegRegistro.MtdCompararToken(tokenEncriptado, tokenSeparado[0]))
            {
                RegistrarUpetino(tokenSeparado[0]);

            }
        }

        public bool ValidarHora(string horaParametro)
        {
            try
            {
                DateTime horaRecibida = DateTime.ParseExact(horaParametro, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime horaActual = DateTime.Now;

                TimeSpan diferencia = horaActual - horaRecibida;
                double minutos = Math.Abs(diferencia.TotalMinutes);

                return minutos <= 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al comparar horas: " + ex.Message);
                return false;
            }
        }
    }
}
