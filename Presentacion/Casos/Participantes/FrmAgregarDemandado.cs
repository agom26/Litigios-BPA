using Comun.Models;
using Dominio.Entidades;
using Presentacion.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Casos.Participantes
{
    public partial class FrmAgregarDemandado : Form
    {
        DemandadoModel demandadoModel = new DemandadoModel();
        public FrmAgregarDemandado()
        {
            InitializeComponent();
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tablessControl1.TabPages.Contains(nombre))
            {
                tablessControl1.TabPages.Remove(nombre);
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tablessControl1.TabPages.Contains(nombre))
            {
                tablessControl1.TabPages.Add(nombre);
            }

            tablessControl1.SelectedTab = nombre;
        }
        private void radioButtonBuscar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBuscar.Checked)
            {
                EliminarTabPage(tabPageAgregar);
                AnadirTabPage(tabPageBuscar);
            }
        }

        private async void txtBuscarDemandado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string filtro = txtBuscarDemandado.Text;

                if (!String.IsNullOrEmpty(filtro))

                {
                    var demandados = await demandadoModel.ObtenerDemandadosFiltrados(1, 10, filtro);

                    dtgDemandados.DataSource = demandados.data;

                }

            }
        }

        private void dtgDemandados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgDemandados.Columns["id"] != null)
            {
                dtgDemandados.Columns["id"].Visible = false;
            }

            dtgDemandados.ClearSelection();
        }

        private void radioButtonAgregar_CheckedChanged(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageAgregar);
            EliminarTabPage(tabPageBuscar);
        }
    }
}
