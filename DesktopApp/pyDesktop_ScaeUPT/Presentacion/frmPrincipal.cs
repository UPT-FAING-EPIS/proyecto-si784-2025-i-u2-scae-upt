using Org.BouncyCastle.Pqc.Crypto.Frodo;
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

namespace SCAE_UPT.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();

            switch (IdentificarUsuario())
            {
                case 1:
                    btnRegistroUpetinos.Enabled = false;
                    btnRegistroVisitantes.Enabled = false;
                    this.FormClosing += new FormClosingEventHandler(OnFormClosing);
                    break;
                case 0:
                    btnAdministrarUpetinos.Enabled=false;
                    btnAdministrarUsuarios.Enabled = false;
                    break;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Estás seguro de que deseas salir?",
            "Confirmar salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if(resultado == DialogResult.Yes)
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

        private void btnRegistroVisitantes_Click(object sender, EventArgs e)
        {
            frmRegistroVisitante frm  = new frmRegistroVisitante();
            frm.Show();
            this.Hide();
        }

        private void btnHistorialRegistroVisitantes_Click(object sender, EventArgs e)
        {
            frmHistorialVisitante frm = new frmHistorialVisitante();
            frm.Show();
            this.Hide();
        }

        private void btnHistorialRegistroUpetinos_Click(object sender, EventArgs e)
        {
            frmHistorialUpetino frm = new frmHistorialUpetino();
            frm.Show();
            this.Hide();
        }

        private void btnRegistroUpetinos_Click(object sender, EventArgs e)
        {
            frmRegistrarUpetino frm = new frmRegistrarUpetino();
            frm.Show();
            this.Hide();
        }

        private void btnAdministrarUsuarios_Click(object sender, EventArgs e)
        {
            frmAdministrarUsuario frm = new frmAdministrarUsuario();
            frm.Show();
            this.Hide();
        }

        private void btnAdministrarUpetinos_Click(object sender, EventArgs e)
        {
            frmAdministrarUpetino frm = new frmAdministrarUpetino();
            frm.Show();
            this.Hide();
        }

        private int IdentificarUsuario()
        {
            if ("Administrador".Equals(frmLoginGuardiania.usuarioLogueado[4]))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
