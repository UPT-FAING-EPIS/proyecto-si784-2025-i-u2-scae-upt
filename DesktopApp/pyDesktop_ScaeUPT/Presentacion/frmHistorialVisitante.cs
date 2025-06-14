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
    public partial class frmHistorialVisitante : Form
    {
        private static Timer timer;
        public frmHistorialVisitante()
        {
            InitializeComponent();
            ListarRegistroVisitante();
            IniciarAutoActualizacion();
        }

        private void ListarRegistroVisitante()
        {
            clsNegocioRegistroVisitante objNegRegistroVisitante = new clsNegocioRegistroVisitante();
            DataTable dt = objNegRegistroVisitante.ListaRegistroVisitante();

            dgvHistorialRegistroUpetino.DataSource = dt;
        }

        private void IniciarAutoActualizacion()
        {
            timer = new Timer();
            timer.Tick += OnTimerTick;
            timer.Interval = 5000;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            ListarRegistroVisitante();
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            timer.Stop();
            frmPrincipal frm = new frmPrincipal();
            frm.Show();
            this.Hide();
        }
    }
}
