using Comun.Models.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Reportes
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            AjustarFilas();
        }

        private void AjustarFilas()
        {
            // 🔹 Fila 0 
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 65F;

            // 🔹 Filas fijas 
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 35F;

        }

        private void btnEditarCaso_Click(object sender, EventArgs e)
        {
            string? rama = null;
            string? expediente = null;
            string? juzgado = null;
            string? notificador = null;
            string? oficial = null;

            if (checkBoxRama.Checked)
            {
                if (comboBoxRama.SelectedIndex == 0)
                {
                    rama = "1";
                }
                else if (comboBoxRama.SelectedIndex == 1)
                {
                    rama = "2";
                }
                else if (comboBoxRama.SelectedIndex == 3)
                {
                    rama = "4";
                }
                else if (comboBoxRama.SelectedIndex == 4)
                {
                    rama = "3";
                }
            }

            if (checkBoxExpediente.Checked)
            {
                expediente = txtExpediente.Text;
            }
            else
            {
                expediente = null;
            }

            if (checkBoxJuzgado.Checked)
            {
                juzgado = txtJuzgado.Text;
            }
            else
            {
                juzgado = null;
            }

            if (checkBoxNotificador.Checked)
            {

            }

            if (checkBoxOficial.Checked)
            {

            }

            /*
            var req = new ReporteCasosRequest
            {
                Rama = "CONSTITUCIONAL",
                Expediente = "2026-005",
                Demandantes = new List<int> { 1, 2 },
                AbogadosDirectores = new List<int> { 5 }
            };*/


            //var resp = await model.ObtenerReporteCasos(req);
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            

        }
    }
}
