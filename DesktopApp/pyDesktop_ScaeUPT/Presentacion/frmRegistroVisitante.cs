using SCAE_UPT.Entidad;
using SCAE_UPT.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net;
using System.Data.SqlClient;


namespace SCAE_UPT.Presentacion
{
    public partial class frmRegistroVisitante : Form
    {
        public frmRegistroVisitante()
        {
            InitializeComponent();
            ListarRegistroVisitante();
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

        private void ListarRegistroVisitante()
        {
            clsNegocioRegistroVisitante objNegRegistroVisitante = new clsNegocioRegistroVisitante();

            DataTable dataTable = objNegRegistroVisitante.ListaRegistroVisitante();

            dgvHistorialRegistroVisitante.DataSource = dataTable;
        }

        private void LimpiarTexto()
        {
            txtApellido.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtNombre.Text = string.Empty;
            btnRegistrarVisitante.Enabled = false;
            txtDocumento.Enabled = false;
        }

        private async void CargarAPIDNIAsync(String dni)
        {
            clsNegocioVisitante objNegVisitante = new clsNegocioVisitante();
            String[] datos = await objNegVisitante.ConsultarDNI(dni);

            if (datos != null ) {
                if(!datos[0].Equals("")){
                    txtNombre.Text=datos[0];
                    txtApellido.Text= datos[1] + " "+datos[2];
                    btnRegistrarVisitante.Enabled=true;
                    txtDocumento.Enabled=false;
                }
                else{
                    MessageBox.Show("No se logro encontrar este DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnRegistrarVisitante.Enabled=(false);
                }
            }
            else{
                MessageBox.Show("No se logro encontrar este DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Text="";      
            }
        }

        private async void CargarAPICEE(String CEE)
        {
            clsNegocioVisitante objNegVisitante = new clsNegocioVisitante();
            String[] datos = await objNegVisitante.ConsultarCarnetExtranjeria(CEE);

            if (datos != null)
            {
                if (!datos[0].Equals(""))
                {
                    txtNombre.Text = datos[0];
                    txtApellido.Text = datos[1] + " " + datos[2];
                    btnRegistrarVisitante.Enabled = true;
                    txtDocumento.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No se logro encontrar este Carnet de Extranjeria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnRegistrarVisitante.Enabled = (false);
                }
            }
            else
            {
                MessageBox.Show("No se logro encontrar este Carnet de Extranjeria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Text = "";
            }
        }

        private void RegistrarVisitante(string dni)
        {
            clsNegocioVisitante objNegVisitante = new clsNegocioVisitante();
            try
            {
                DataTable datatable= objNegVisitante.MtdBuscarVisitante(dni);

                if (datatable == null || datatable.Rows.Count==0)
                {
                    RegistrarNuevoVisitante(dni);
                }
                else
                {
                    RegistrarIngresoOSalida(dni);
                }

                ListarRegistroVisitante();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                // Puedes usar MessageBox.Show si es app de escritorio
            }
        }

        private void RegistrarNuevoVisitante(string dni)
        {
            clsNegocioVisitante objNegVisitante = new clsNegocioVisitante();

            clsEntidadVisitante objEntVisitante = new clsEntidadVisitante
            {
                DNI = dni,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = ObtenerTelefono()
            };

            if (string.IsNullOrWhiteSpace(objEntVisitante.Telefono))
            {
                MessageBox.Show("Operación cancelada, faltó ingresar el teléfono.");
                return;
            }

            if (objNegVisitante.MtdGuardarVisitante(objEntVisitante))
            {
                RegistrarIngreso(dni, "1era visita, registro del visitante correcto!.");
            }
            else
            {
                MessageBox.Show("Error al registrar el visitante.");
            }
        }

        private void RegistrarIngresoOSalida(string dni)
        {
            clsNegocioRegistroVisitante objNegRegistroVisitante = new clsNegocioRegistroVisitante();
            DataTable rsRegistro = objNegRegistroVisitante.MtdBuscarRegistroVisitante(dni);
            

            if (rsRegistro.Rows.Count > 0)
            {
                DataRow row = rsRegistro.Rows[0];
                var fechaSalida = row["FechaHora_Salida"] as DateTime?;

                if (fechaSalida == null)
                {
                    RegistrarSalida(dni);
                }
                else
                {
                    RegistrarIngreso(dni, "Registro del visitante correcto!.");
                }
            }
            else
            {
                RegistrarIngreso(dni, "Registro del visitante correcto!.");
            }
        }

        private void RegistrarIngreso(string dni, string mensajeExito)
        {
            clsNegocioRegistroVisitante objNegRegistroVisitante = new clsNegocioRegistroVisitante();

            clsEntidadRegistroVisitante objEntRegistroVisitante = new clsEntidadRegistroVisitante
            {
                DNI = dni,
                FechaHora_Ingreso = DateTime.Now,
                IDUsuario_Ingreso = frmLoginGuardiania.usuarioLogueado[0]
            };

            if (objNegRegistroVisitante.MtdGuardarIngresoVisitante(objEntRegistroVisitante))
            {
                MessageBox.Show(mensajeExito);
                LimpiarTexto();
            }
            else
            {
                MessageBox.Show("Error al registrar el ingreso.");
            }
        }

        private void RegistrarSalida(string dni)
        {
            clsNegocioRegistroVisitante objNegRegistroVisitante = new clsNegocioRegistroVisitante();

            clsEntidadRegistroVisitante objEntRegistroVisitante = new clsEntidadRegistroVisitante
            {
                DNI = dni,
                FechaHora_Salida = DateTime.Now,
                IDUsuario_Salida = frmLoginGuardiania.usuarioLogueado[0]
            };

            if (objNegRegistroVisitante.MtdGuardarSalidaVisitante(objEntRegistroVisitante))
            {
                MessageBox.Show("Salida registrada!.");
                LimpiarTexto();
            }
            else
            {
                MessageBox.Show("Error al registrar la salida.");
            }
        }


        private string ObtenerTelefono()
        {
            string telefono = "";

            bool telefonoValido = false;

            while (!telefonoValido)
            {
                telefono = Interaction.InputBox(
                    "Ingrese su número de teléfono en formato internacional (ej. +51987654321):",
                    "Validación de Teléfono",
                    ""
                );

                if( telefono == null)
                {
                    telefono = "";
                    telefonoValido = true;
                    break;
                }

                telefono = telefono.Trim();

                if (!telefono.Equals(""))
                {
                    if (Regex.IsMatch(telefono, @"^\+?[1-9]\d{6,14}$"))
                    {
                        telefonoValido = true;
                    }
                    else
                    {
                        MessageBox.Show("Número de teléfono inválido. Ingrese un número en formato válido (hasta 15 dígitos, opcionalmente con '+').","Error", MessageBoxButtons.OK , MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un némero de telefono.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return telefono;
        }

        private void btnRegistrarVisitante_Click(object sender, EventArgs e)
        {
            string Documento = txtDocumento.Text;
            if (rbtnDNI.Checked && !Documento.Equals("") && !txtNombre.Equals("")) 
            {
                RegistrarVisitante(Documento);
            }
            else if (rbtnCarnetExtranjeria.Checked && !Documento.Equals("") && !txtNombre.Equals(""))
            {
                RegistrarVisitante(Documento);
            }
            else
            {
                MessageBox.Show("Debe seleccionar el tipo de Documento");
            }
        }

        private void rbtnDNI_CheckedChanged(object sender, EventArgs e)
        {
            LimpiarTexto();
            txtDocumento.Enabled = true;
        }

        private void rbtnCarnetExtranjeria_CheckedChanged(object sender, EventArgs e)
        {
            LimpiarTexto();
            txtDocumento.Enabled = true;
        }

        private void txtDocumento_KeyUp(object sender, KeyEventArgs e)
        {
            if (rbtnDNI.Checked)
            {
                if (txtDocumento.Text.Length == 8)
                {
                    CargarAPIDNIAsync(txtDocumento.Text);
                }
            } else if (rbtnCarnetExtranjeria.Text.Length == 9)
            {
                CargarAPICEE(txtDocumento.Text);
            }
        }


        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rbtnDNI.Checked)
            {
                if (!char.IsDigit(e.KeyChar) || txtDocumento.Text.Length >= 8)
                {
                    e.Handled = true; // Bloquea la entrada del carácter
                }
            }
            else if (rbtnCarnetExtranjeria.Checked)
            {
                if (!char.IsDigit(e.KeyChar) || txtDocumento.Text.Length >= 9)
                {
                    e.Handled = true; // Bloquea la entrada del carácter
                }
            }
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            frm.Show();
            this.Hide();
        }
    }
}
