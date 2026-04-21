using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Contenciosos;
using Dominio.Entidades;
using Dominio.Entidades.Civiles;
using Dominio.Entidades.Contenciosos;
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

namespace Presentacion.Casos.Contenciosos.Mostrar_marcas
{
    public partial class FrmAgregarMarcaReferencia : Form
    {

        CAObtenerMarcasContenciosasModel marcasModel = new CAObtenerMarcasContenciosasModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsViaAmpremio = new BindingSource();
        public int? IdMarcaSeleccionada { get; set; }
        public FrmAgregarMarcaReferencia()
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

            if (dtgMarcas.Columns["id"] != null)
            {
                dtgMarcas.Columns["id"].Visible = false;
            }

            if (dtgMarcas.Columns["recurso_id"] != null)
            {
                dtgMarcas.Columns["recurso_id"].Visible = false;
            }

            dtgMarcas.ClearSelection();
        }

        

        private void btnAgregarDemante_Click(object sender, EventArgs e)
        {
            if (dtgMarcas.SelectedRows.Count > 0)
            {
                
                MessageBox.Show("Marca de referencia agregada", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; 
                this.Close();

            }
            else
            {
                MessageBox.Show("Debe seleccionar un Caso Común para poder agregarlo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            
        }

        private void dtgContactoEmpresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgMarcas.SelectedRows.Count > 0)
            {
                var row = dtgMarcas.SelectedRows[0];
                var caso = (MarcaContenciosaListItem)row.DataBoundItem;

                IdMarcaSeleccionada = caso.recurso_id; // 👈 guardas el ID

                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una marca para poder agregarla",
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

            var resultado = await marcasModel.ObtenerCasosContenciosos(pagina, registrosPorPagina, filtro);
            
            if (resultado.success)
            {
                bsViaAmpremio.DataSource = resultado.data;
                dtgMarcas.Refresh();
                labelTotal.Text = $"Total de marcas: {resultado.total}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)resultado.registros/ resultado.registros)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmAgregarMarcaReferencia_Load(object sender, EventArgs e)
        {
            dtgMarcas.DataSource = bsViaAmpremio;
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
            IdMarcaSeleccionada = null;
            this.Close();
        }

        
    }
}
