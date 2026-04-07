using Comun;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Laborales;
using Dominio.Entidades;
using Dominio.Entidades.Civiles;
using Dominio.Entidades.Constitucionales;
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

namespace Presentacion.Casos.Constitucionales.Agregar_caso
{
    public partial class FrmAgregarCaso : Form
    {
        CasoConstitucionalAmparoModel constitucionalModel = new CasoConstitucionalAmparoModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsCasos = new BindingSource();
        public int? IdCasoSeleccionado { get; set; }
        public FrmAgregarCaso()
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
                AnadirTabPage(tabPageBuscar);

                btnAgregarContactoEmpresa.Visible = true;
                btnCancelar.Visible = true;
            }
        }

        private async void txtBuscarContactoEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Filtrar();
            }
        }

        private void dtgContactoEmpresas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgContactoEmpresas.Columns["id"] != null)
            {
                dtgContactoEmpresas.Columns["id"].Visible = false;
            }

            dtgContactoEmpresas.ClearSelection();
        }



        private void btnAgregarDemante_Click(object sender, EventArgs e)
        {
            if (dtgContactoEmpresas.SelectedRows.Count > 0)
            {

                MessageBox.Show("Caso referencial agregado", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("Debe seleccionar un caso para poder agregarlo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void dtgContactoEmpresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgContactoEmpresas.SelectedRows.Count > 0)
            {
                var row = dtgContactoEmpresas.SelectedRows[0];
                var caso = (CasoLaboralListItem)row.DataBoundItem;

                IdCasoSeleccionado = caso.id;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un caso para poder agregarlo",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {

            AnadirTabPage(tabPageBuscar);
            radioButtonBuscar.Checked = true;
        }



        private async Task Filtrar()
        {

            string filtro = txtBuscarContactoEmpresa.Text;
            int pagina = 1;
            int registrosPorPagina = 10;
            string rama = comboBoxRama.SelectedItem?.ToString() ?? string.Empty;

            var resultado = await constitucionalModel.ObtenerCasosPorRama(UserSession.Id, pagina, registrosPorPagina, rama, filtro);

            if (resultado.success)
            {
                bsCasos.DataSource = resultado.data;
                dtgContactoEmpresas.Refresh();
                labelTotal.Text = $"Total de casos: {resultado.total}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)resultado.registros / resultado.registros)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmAgregarCaso_Load(object sender, EventArgs e)
        {
            dtgContactoEmpresas.DataSource = bsCasos;
            await Filtrar();
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await Filtrar();
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await Filtrar();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IdCasoSeleccionado = null;
            this.Close();
        }

        private void tabPageBuscar_Click(object sender, EventArgs e)
        {

        }

        private async void comboBoxRama_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Filtrar();
        }
    }
}
