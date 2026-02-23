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
    public partial class FrmAgregarDemandante : Form
    {
        private readonly BindingList<PersonaListDataResponse> _listaDestino;

        DemandanteModel demandanteModel = new DemandanteModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsDemandantes = new BindingSource();

        public FrmAgregarDemandante(BindingList<PersonaListDataResponse> listaDestino)
        {
            InitializeComponent();
            _listaDestino = listaDestino;
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

                btnAgregarDemandante.Visible = true;
                btnCancelar.Visible = true;
            }
        }

        private async void txtBuscarDemandante_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Filtrar();
            }
        }

        private void dtgDemandantes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgDemandantes.Columns["id"] != null)
            {
                dtgDemandantes.Columns["id"].Visible = false;
            }

            dtgDemandantes.ClearSelection();
        }

        private void radioButtonAgregar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAgregar.Checked)
            {
                AnadirTabPage(tabPageAgregar);
                EliminarTabPage(tabPageBuscar);

                btnAgregarDemandante.Visible = false;
                btnCancelar.Visible = false;
            }

        }

        private void btnAgregarDemante_Click(object sender, EventArgs e)
        {
            if (dtgDemandantes.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgDemandantes.SelectedRows)
                {
                    var demandante = (PersonaListDataResponse)row.DataBoundItem;

                    if (!_listaDestino.Any(x => x.id == demandante.id))
                        _listaDestino.Add(demandante);
                }

                dtgDemandantes.ClearSelection();
                MessageBox.Show("Demandante agregado a la lista", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Debe seleccionar un demandante para poder agregarlo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgDemandantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int idPersona = Convert.ToInt32(dtgDemandantes.Rows[e.RowIndex].Cells["id"].Value);
        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(tabPageBuscar);
            EliminarTabPage(tabPageAgregar);
            radioButtonBuscar.Checked = true;
            radioButtonAgregar.Checked = false;
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtNombreA.Text = "";
            txtCorreoA.Text = "";
            txtTelefonoA.Text = "";
        }

        private async Task GuardarDemandante()
        {
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            string nombreA = txtNombreA.Text;
            string telefonoA = txtTelefonoA.Text;
            string correoA = txtCorreoA.Text;


            var resultado = await demandanteModel.CrearDemandante(nombre, direccion, correo, telefono, nombreA, telefonoA, correoA);

            if (resultado.success)
            {
                MessageBox.Show("Demandante creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await Filtrar();
                LimpiarFormulario();
                AnadirTabPage(tabPageBuscar);
                EliminarTabPage(tabPageAgregar);
                radioButtonAgregar.Checked = false;
                radioButtonBuscar.Checked = true;
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardarDemandante_Click(object sender, EventArgs e)
        {
            await GuardarDemandante();
        }

        private async Task Filtrar()
        {

            string filtro = txtBuscarDemandante.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await demandanteModel.ObtenerDemandantesFiltrados(pagina, registrosPorPagina, filtro);

            if (resultado.success)
            {
                bsDemandantes.DataSource = resultado.data;
                dtgDemandantes.Refresh();
                labelTotal.Text = $"Total de Demandantes: {resultado.totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)resultado.totalRegistros / resultado.registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmAgregarDemandante_Load(object sender, EventArgs e)
        {
            dtgDemandantes.DataSource = bsDemandantes;
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
            this.Close();
        }

        private void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tablessControl1.SelectedTab == tabPageAgregar)
            {
                tabPageAgregar.AutoScrollPosition = new Point(0, 0);
            }
        }
    }
}
