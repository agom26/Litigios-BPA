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

namespace Presentacion.Casos.Abogados_asignados
{
    public partial class FrmAgregarAbogadoAsistente : Form
    {
        private readonly BindingList<UserListDataResponse> _listaDestino;
        public List<UserListDataResponse> AbogadosDirectoresSeleccionados { get; private set; }
            = new List<UserListDataResponse>();
        

        UserModel usuarioModel = new UserModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsSociosResponsables = new BindingSource();
        public FrmAgregarAbogadoAsistente(BindingList<UserListDataResponse> listaDestino)
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

                btnAgregarDemandado.Visible = true;
                btnCancelar.Visible = true;
            }
        }

        private async void txtBuscarDemandado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Filtrar();
            }
        }

        private void dtgSociosResponsables_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgSociosResponsables.Columns["id"] != null)
            {
                dtgSociosResponsables.Columns["id"].Visible = false;
            }

            dtgSociosResponsables.ClearSelection();
        }

        private void radioButtonAgregar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAgregar.Checked)
            {
                AnadirTabPage(tabPageAgregar);
                EliminarTabPage(tabPageBuscar);

                btnAgregarDemandado.Visible = false;
                btnCancelar.Visible = false;
            }

        }

        private void btnAgregarDemandado_Click(object sender, EventArgs e)
        {
            if (dtgSociosResponsables.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgSociosResponsables.SelectedRows)
                {
                    var abogado = (UserListDataResponse)row.DataBoundItem;

                    if (!_listaDestino.Any(x => x.id == abogado.id))
                        _listaDestino.Add(abogado);
                }

                dtgSociosResponsables.ClearSelection();
                MessageBox.Show("Abogado agregado a la lista", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un abogado para poder agregarlo",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgSociosResponsables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int idPersona = Convert.ToInt32(dtgSociosResponsables.Rows[e.RowIndex].Cells["id"].Value);
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

        /*
        private async Task GuardarDemandado()
        {
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            string nombreA = txtNombreA.Text;
            string telefonoA = txtTelefonoA.Text;
            string correoA = txtCorreoA.Text;


            var resultado = await demandadoModel.CrearDemandado(nombre, direccion, correo, telefono, nombreA, telefonoA, correoA);

            if (resultado.success)
            {
                MessageBox.Show("Demandado creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }*/

        private void btnGuardarDemandado_Click(object sender, EventArgs e)
        {
            //await GuardarDemandado();
        }

        private async Task Filtrar()
        {

            string filtro = txtBuscarDemandado.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await usuarioModel.ObtenerUsuariosFiltrados(pagina, registrosPorPagina, filtro);

            if (resultado.success)
            {
                bsSociosResponsables.DataSource = resultado.data;
                dtgSociosResponsables.Refresh();
                labelTotal.Text = $"Total de Abogados: {resultado.totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)resultado.totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmAgregarAbogadoAsistente_Load(object sender, EventArgs e)
        {
            dtgSociosResponsables.DataSource = bsSociosResponsables;
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
