using Comun;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Laborales;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace Presentacion.Reportes.BuscarPersonaForm
{
    public partial class BuscarPersonaForm: Form
    {
        public PersonaListDataResponse? PersonaSeleccionada { get; private set; }
        private readonly BindingList<PersonaListDataResponse> _listaDestino;
        
        DemandadoModel demandadoModel = new DemandadoModel();
        DemandanteModel demandanteModel = new DemandanteModel();
        ContactoEmpresaModel ContactoEmpresaModel = new ContactoEmpresaModel();
        TerceroInteresadoModel TerceroInteresadoModel = new TerceroInteresadoModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsPersonas = new BindingSource();
        public int? IdPersonaSeleccionada { get; set; }
        public BuscarPersonaForm(BindingList<PersonaListDataResponse> listaDestino, string tipoPersona)
        {
            InitializeComponent();

            switch (tipoPersona)
            {
                case "Demandado":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.AddRange(new string[] { "Demandado" });
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                case "Autoridad Impugnada":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.AddRange(new string[] { "Autoridad Impugnada" });
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                case "Solicitante":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.AddRange(new string[] { "Solicitante"});
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                case "Demandante":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.AddRange(new string[] { "Demandante"});
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                case "Contacto Empresa":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.Add("Contacto Empresa");
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                case "Tercero Interesado":
                    {
                        comboBoxRama.Items.Clear();
                        comboBoxRama.Items.Add("Tercero Interesado");
                        comboBoxRama.SelectedIndex = 0;
                        break;
                    }
                default:
                    {
                        comboBoxRama.Items.Clear();
                        MessageBox.Show("Tipo de persona no reconocido. Se mostrarán todos los tipos.");
                        comboBoxRama.Items.AddRange(new string[] { "Demandado", "Autoridad Impugnada", "Solicitante", "Demandante", "Contacto Empresa", "Tercero Interesado" });
                        comboBoxRama.SelectedIndex = 0;
                        comboBoxRama.Enabled = true;
                        break;
                    }


            }

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

            if (dtgPersonas.Columns["id"] != null)
            {
                dtgPersonas.Columns["id"].Visible = false;
            }

            dtgPersonas.ClearSelection();
        }



        private void btnAgregarDemante_Click(object sender, EventArgs e)
        {
            if (dtgPersonas.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgPersonas.SelectedRows)
                {
                    var personaSeleccionada = (PersonaListDataResponse)row.DataBoundItem;

                    if (!_listaDestino.Any(x => x.id == personaSeleccionada.id))
                        _listaDestino.Add(personaSeleccionada);
                    
                }

                MessageBox.Show("Persona agregada", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("Debe seleccionar una persona para poder agregarla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dtgPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgPersonas.SelectedRows.Count > 0)
            {
                var row = dtgPersonas.SelectedRows[0];
                var persona = (PersonaListDataResponse)row.DataBoundItem;

                IdPersonaSeleccionada = persona.id;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una persona para poder agregarla",
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
            string tipoPersona = comboBoxRama.SelectedItem?.ToString() ?? string.Empty;

            ApiGetUserListResponse<List<PersonaListDataResponse>> resultado = null;
            switch (tipoPersona)
            {
                case "Demandado":
                case "Autoridad Impugnada":
                    {
                        resultado = await demandadoModel.ObtenerDemandadosFiltrados( pagina, registrosPorPagina, filtro);
                        break;
                    }
                case "Solicitante":
                case "Demandante":
                    {
                        resultado = await demandanteModel.ObtenerDemandantesFiltrados(pagina, registrosPorPagina, filtro);
                        break;
                    }
                case "Contacto Empresa":
                    {
                        resultado = await ContactoEmpresaModel.ObtenerContactosDeEmpresaFiltrados(pagina, registrosPorPagina, filtro);
                        break;
                    }
                case "Tercero Interesado":
                    {
                        resultado = await TerceroInteresadoModel.ObtenerTercerosInteresadosFiltrados(pagina, registrosPorPagina, filtro);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Seleccione un tipo válido");
                        return;
                    }
            }


            if (resultado.success)
            {
                bsPersonas.DataSource = resultado.data;
                dtgPersonas.Refresh();
                labelTotal.Text = $"Total de personas: {resultado.totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)resultado.totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarPersonaForm_Load(object sender, EventArgs e)
        {
            dtgPersonas.DataSource = bsPersonas;
            //await Filtrar();
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
            IdPersonaSeleccionada = null;
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
