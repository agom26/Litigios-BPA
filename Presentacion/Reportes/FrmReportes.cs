using Comun;
using Comun.Models;
using Comun.Models.Reportes;
using DocumentFormat.OpenXml.EMMA;
using Dominio.Entidades.Reportes;
using Presentacion.Casos.Civiles.Proceso_ejecucion;
using Presentacion.Casos.Participantes;
using Presentacion.Reportes.BuscarPersonaForm;
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
        ReportesModel reporteModel = new ReportesModel();
        private BindingList<PersonaListDataResponse> listaDemandados
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaDemandantes
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaAutoridadesImpugnadas
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaSolicitantes
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaTercerosInteresados
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaContactosEmpresa
        = new BindingList<PersonaListDataResponse>();
        //abogados en el caso
        private BindingList<UserListDataResponse> listaAbogadosDirectores
        = new BindingList<UserListDataResponse>();
        private BindingList<UserListDataResponse> listaSociosResponsables
        = new BindingList<UserListDataResponse>();
        private BindingList<UserListDataResponse> listaAbogadosAsistentes
        = new BindingList<UserListDataResponse>();

        private void AjustarLayoutPorResolucion()
        {
            if (flowLayoutPanel1.Controls.Count == 0) return;

            int w = flowLayoutPanel1.ClientSize.Width;
            if (w <= 50) return;

            int padding = flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right;

            int marginX = 10;
            int gap = 20;
            int ancho2Cols = (w - padding - gap) / 2;
            bool caben2 = (ancho2Cols >= 620);

            if (caben2)
            {
                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel1.WrapContents = true;

                foreach (Panel p in flowLayoutPanel1.Controls.OfType<Panel>())
                {
                    // Tus paneles son AutoSize, se mantienen así
                    p.AutoSize = true;
                    p.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                    // Le "encerramos" el ancho
                    p.MinimumSize = new Size(ancho2Cols, p.MinimumSize.Height);
                    p.MaximumSize = new Size(ancho2Cols, 0);

                    // Margen para que se vea bien y el wrap calcule
                    p.Margin = new Padding(5);
                }
            }
            else
            {
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel1.WrapContents = false;

                int ancho1Col = w - padding - 10;

                foreach (Panel p in flowLayoutPanel1.Controls.OfType<Panel>())
                {
                    p.AutoSize = true;
                    p.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                    p.MinimumSize = new Size(ancho1Col, p.MinimumSize.Height);
                    p.MaximumSize = new Size(ancho1Col, 0);

                    p.Margin = new Padding(5);
                }
            }

            flowLayoutPanel1.PerformLayout();

        }

        //alistar listas
        private void alistarListaDemandantes()
        {
            dtgDemandantes2.DataSource = listaDemandantes;

            dtgDemandantes2.AllowUserToAddRows = false;
            dtgDemandantes2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgDemandantes2.DataSource = listaDemandantes;

            listaDemandantes.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewDemandantes();
            };

            CrearBotonQuitarDemandante();
            dtgDemandantes2.CellClick -= dtgDemandantes2_CellClick;
            dtgDemandantes2.CellClick += dtgDemandantes2_CellClick;
        }

        private void alistarListaDemandados()
        {
            dtgDemandados.DataSource = listaDemandados;

            dtgDemandados.AllowUserToAddRows = false;
            dtgDemandados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgDemandados.DataSource = listaDemandados;

            listaDemandados.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewDemandados();
            };

            CrearBotonQuitarDemandado();
            dtgDemandados.CellClick -= dtgDemandados_CellClick;
            dtgDemandados.CellClick += dtgDemandados_CellClick;
        }

        private void alistarListaSolicitantes()
        {
            dtgSolicitantes.DataSource = listaSolicitantes;

            dtgSolicitantes.AllowUserToAddRows = false;
            dtgSolicitantes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgSolicitantes.DataSource = listaSolicitantes;

            listaSolicitantes.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewSolicitantes();
            };

            CrearBotonQuitarSolicitante();
            dtgSolicitantes.CellClick -= dtgSolicitantes_CellClick;
            dtgSolicitantes.CellClick += dtgSolicitantes_CellClick;
        }

        private void alistarListaAutoridadesImpugnadas()
        {
            dtgAutoridadesImpugnadas.DataSource = listaAutoridadesImpugnadas;

            dtgAutoridadesImpugnadas.AllowUserToAddRows = false;
            dtgAutoridadesImpugnadas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgAutoridadesImpugnadas.DataSource = listaAutoridadesImpugnadas;

            listaAutoridadesImpugnadas.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewAutoridadesImpugnadas();
            };

            CrearBotonQuitarAutoridadesImpugnadas();
            dtgAutoridadesImpugnadas.CellClick -= dtgAutoridadesImpugnadas_CellClick;
            dtgAutoridadesImpugnadas.CellClick += dtgAutoridadesImpugnadas_CellClick;
        }
        private void alistarListaTercerosInteresados()
        {
            dtgTercerosInteresados.DataSource = listaTercerosInteresados;

            dtgTercerosInteresados.AllowUserToAddRows = false;
            dtgTercerosInteresados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgTercerosInteresados.DataSource = listaTercerosInteresados;

            listaTercerosInteresados.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewTercerosInteresados();
            };

            CrearBotonQuitarTercerosInteresados();
            dtgTercerosInteresados.CellClick -= dtgTercerosInteresados_CellClick;
            dtgTercerosInteresados.CellClick += dtgTercerosInteresados_CellClick;
        }

        private void AjustarAlturaDataGridViewDemandantes()
        {
            dtgDemandantes2.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandantes2.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandantes2.ColumnHeadersHeight;

            dtgDemandantes2.Height = alturaFilas + alturaHeaders + 22;

            dtgDemandantes2.ScrollBars = ScrollBars.None;

            dtgDemandantes2.PerformLayout();

        }
        private void AjustarAlturaDataGridViewDemandados()
        {
            dtgDemandados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandados.ColumnHeadersHeight;

            dtgDemandados.Height = alturaFilas + alturaHeaders + 22;

            dtgDemandados.ScrollBars = ScrollBars.None;
            dtgDemandados.PerformLayout();

        }
        private void AjustarAlturaDataGridViewSolicitantes()
        {
            dtgSolicitantes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgSolicitantes.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgSolicitantes.ColumnHeadersHeight;

            dtgSolicitantes.Height = alturaFilas + alturaHeaders + 22;

            dtgSolicitantes.ScrollBars = ScrollBars.None;
            dtgSolicitantes.PerformLayout();
        }

        private void AjustarAlturaDataGridViewAutoridadesImpugnadas()
        {
            dtgAutoridadesImpugnadas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAutoridadesImpugnadas.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAutoridadesImpugnadas.ColumnHeadersHeight;

            dtgAutoridadesImpugnadas.Height = alturaFilas + alturaHeaders + 22;

            dtgAutoridadesImpugnadas.ScrollBars = ScrollBars.None;
            dtgAutoridadesImpugnadas.PerformLayout();
        }

        private void AjustarAlturaDataGridViewTercerosInteresados()
        {
            dtgTercerosInteresados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgTercerosInteresados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgTercerosInteresados.ColumnHeadersHeight;

            dtgTercerosInteresados.Height = alturaFilas + alturaHeaders + 22;

            dtgTercerosInteresados.ScrollBars = ScrollBars.None;
            dtgTercerosInteresados.PerformLayout();
        }

        private void CrearBotonQuitarDemandante()
        {
            if (!dtgDemandantes2.Columns.Contains("Quitar"))
            {
                var btnQuitar = new DataGridViewButtonColumn
                {
                    Name = "Quitar",
                    HeaderText = "",
                    Text = "➖",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                dtgDemandantes2.Columns.Add(btnQuitar);
                dtgDemandantes2.Columns["Quitar"].DisplayIndex = dtgDemandantes2.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarDemandado()
        {
            if (!dtgDemandados.Columns.Contains("Quitar"))
            {
                var btnQuitar = new DataGridViewButtonColumn
                {
                    Name = "Quitar",
                    HeaderText = "",
                    Text = "➖",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                dtgDemandados.Columns.Add(btnQuitar);
                dtgDemandados.Columns["Quitar"].DisplayIndex = dtgDemandados.ColumnCount - 1;
            }
        }
        private void CrearBotonQuitarSolicitante()
        {
            if (!dtgSolicitantes.Columns.Contains("Quitar"))
            {
                var btnQuitar = new DataGridViewButtonColumn
                {
                    Name = "Quitar",
                    HeaderText = "",
                    Text = "➖",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                dtgSolicitantes.Columns.Add(btnQuitar);
                dtgSolicitantes.Columns["Quitar"].DisplayIndex = dtgSolicitantes.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarAutoridadesImpugnadas()
        {
            if (!dtgAutoridadesImpugnadas.Columns.Contains("Quitar"))
            {
                var btnQuitar = new DataGridViewButtonColumn
                {
                    Name = "Quitar",
                    HeaderText = "",
                    Text = "➖",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                dtgAutoridadesImpugnadas.Columns.Add(btnQuitar);
                dtgAutoridadesImpugnadas.Columns["Quitar"].DisplayIndex = dtgAutoridadesImpugnadas.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarTercerosInteresados()
        {
            if (!dtgTercerosInteresados.Columns.Contains("Quitar"))
            {
                var btnQuitar = new DataGridViewButtonColumn
                {
                    Name = "Quitar",
                    HeaderText = "",
                    Text = "➖",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                dtgTercerosInteresados.Columns.Add(btnQuitar);
                dtgTercerosInteresados.Columns["Quitar"].DisplayIndex = dtgTercerosInteresados.ColumnCount - 1;
            }
        }

        public FrmReportes()
        {
            InitializeComponent();
            AjustarFilas();
            alistarListaDemandantes();
            alistarListaDemandados();
            alistarListaSolicitantes();
            alistarListaAutoridadesImpugnadas();
            alistarListaTercerosInteresados();
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

        private async void btnEditarCaso_Click(object sender, EventArgs e)
        {
            int usuarioId = UserSession.Id;
            int? rama = null;
            string? subrama = null;
            string? expediente = null;
            string? juzgado = null;
            string? notificador = null;
            string? oficial = null;
            string? estado = null;
            string? tipoReferencia = null;
            int? tieneReferencia = null;
            int? incluirRelacionados = null;
            int soloTerminados = 0;
            List<int>? demandadosIds = null;
            List<int>? demandantesIds = null;
            List<int>? autoridadesIds = null;
            List<int>? solicitantesIds = null;
            List<int>? tercerosInteresadosIds = null;
            List<int>? contactosEmpresaIds = null;

            if (checkBoxRama.Checked)
            {
                if (comboBoxRama.SelectedIndex == 0)
                {
                    rama = 1;
                }
                else if (comboBoxRama.SelectedIndex == 1)
                {
                    rama = 2;
                }
                else if (comboBoxRama.SelectedIndex == 2)
                {
                    rama = 4;
                }
                else if (comboBoxRama.SelectedIndex == 3)
                {
                    rama = 3;
                }
                else
                {
                    rama = null;
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
                juzgado = comboBoxJuzgado.SelectedItem?.ToString();
            }
            else
            {
                juzgado = null;
            }

            if (checkBoxNotificador.Checked)
            {
                notificador = txtNotificador.Text;
            }
            else
            {
                notificador = null;
            }

            if (checkBoxOficial.Checked)
            {
                oficial = txtOficial.Text;
            }
            else
            {
                oficial = null;
            }

            if (checkBoxSubrama.Checked)
            {
                subrama = comboBoxSubrama.SelectedItem?.ToString();
            }
            else
            {
                subrama = null;
            }

            if (checkBoxEstado.Checked)
            {
                estado = txtEstado.Text;
            }
            else
            {
                estado = null;
            }

            if (checkBoxTipoReferencia.Checked)
            {
                switch (comboBoxTipoReferencia.SelectedIndex)
                {
                    case 0:
                        tipoReferencia = "APREMIO_A_COMUN";
                        break;
                    case 1:
                        tipoReferencia = "RECURSO_CASACION";
                        break;
                    case 2:
                        tipoReferencia = "AMPARO";
                        break;
                    default:
                        tipoReferencia = null;
                        break;
                }
            }
            else
            {
                tipoReferencia = null;
            }

            if (checkBoxTieneReferencia.Checked)
            {
                switch (comboBoxTieneRelacion.SelectedIndex)
                {
                    case 0: // Todos
                        tieneReferencia = null;
                        break;
                    case 1: // Solo vinculados
                        tieneReferencia = 1;
                        break;
                    case 2: // Sin vinculación
                        tieneReferencia = 0;
                        break;
                }
            }
            else
            {
                tieneReferencia = null;
            }

            if (checkBoxIncluirRelacionados.Checked)
            {
                incluirRelacionados = 1;
            }
            else
            {
                incluirRelacionados = null;
            }


            if (checkBoxSoloTerminados.Checked)
            {
                soloTerminados = 1;
            }
            else
            {
                soloTerminados = 0;
            }

            if (checkBoxDemandantes2.Checked)
            {
                demandantesIds = listaDemandantes
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                demandantesIds = null;
            }

            if (checkBoxDemandados2.Checked)
            {
                demandadosIds = listaDemandados
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                demandadosIds = null;
            }


            if (checkBoxAutoridadesImpugnadas.Checked)
            {
                autoridadesIds = listaAutoridadesImpugnadas
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                autoridadesIds = null;
            }

            if (checkBoxSolicitantes.Checked)
            {
                solicitantesIds = listaSolicitantes
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                solicitantesIds = null;
            }

            if (checkBoxTercerosInteresados.Checked)
            {
                tercerosInteresadosIds = listaTercerosInteresados
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                tercerosInteresadosIds = null;
            }

            var req = new ReporteMaestroCasosRequest
            {
                UsuarioId = usuarioId,
                Pagina = 1,
                RegistrosPorPagina = 20,
                ModuloId = rama,
                Origen = subrama,
                EstadoActual = estado,
                Expediente = expediente,
                AbogadoDirectorIds = null,//new List<int> { 3, 4 },
                DemandanteIds = demandantesIds,
                DemandadoIds = demandadosIds,
                SolicitanteIds = solicitantesIds,
                AutoridadIds = autoridadesIds,
                TerceroIds = tercerosInteresadosIds,
                TieneReferencia = tieneReferencia,
                TipoReferencia = tipoReferencia,
                SoloTerminados = soloTerminados,
                IncluirRelacionados = incluirRelacionados,
                FechaDesde = null,
                FechaHasta = null
            };



            var resp = await reporteModel.ObtenerReporteMaestroCasosExportacionRelacionados(req);

            if (resp.success)
            {
                var total = resp.total;
                var lista = resp.data;

                dtgResultadosReporte.DataSource = lista;
                dtgResultadosReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dtgResultadosReporte.Refresh();
            }
            else
            {
                var mensaje = resp.message;
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {


        }

        private void comboBoxRama_SelectedValueChanged(object sender, EventArgs e)
        {
            string? rama = comboBoxRama.SelectedItem?.ToString();
            switch (rama)
            {
                case "Civil":
                    comboBoxSubrama.Items.Clear();
                    comboBoxSubrama.Items.AddRange(new string[] { "CIVIL JUICIO ORAL PRIMER INSTANCIA", "CIVIL JUICIO ORAL SEGUNDA INSTANCIA", "CIVIL JUICIO SUMARIO PRIMER INSTANCIA", "CIVIL JUICIO SUMARIO SEGUNDA INSTANCIA", "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO", "CIVIL PROCESO DE EJECUIÓN COMÚN", "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO SEGUNDA INSTANCIA", "CIVIL PROCESO DE EJECUCIÓN COMÚN SEGUNDA INSTANCIA" });
                    break;
                case "Contencioso Administrativo":
                    comboBoxSubrama.Items.Clear();
                    comboBoxSubrama.Items.AddRange(new string[] { "ADMINISTRATIVO GENERAL PRIMER INSTANCIA", "ADMINISTRATIVO TRIBUTARIO PRIMER INSTANCIA", "RECURSO DE CASACIÓN" });
                    break;
                case "Laboral":
                    comboBoxSubrama.Items.Clear();
                    comboBoxSubrama.Items.AddRange(new string[] { "LABORAL PRIMER INSTANCIA", "LABORAL SEGUNDA INSTANCIA" });
                    break;
                case "Constitucional":
                    comboBoxSubrama.Items.Clear();
                    comboBoxSubrama.Items.AddRange(new string[] { "CONSTITUCIONAL AMPARO" });
                    break;
                default:
                    comboBoxSubrama.Items.Clear();
                    break;
            }
        }

        private void btnAgregarDemandantes_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaDemandantes, "Demandante");

            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        private void FrmReportes_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
        }

        private void dtgDemandantes2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgDemandantes2.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgDemandantes2.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este demandante";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de demandantes?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaDemandantes.Remove(item); // ✅ aquí se quita

                }
            }
        }

        private void dtgDemandantes2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgDemandantes2.Columns["id"] != null)
            {
                dtgDemandantes2.Columns["id"].Visible = false;
            }
            dtgDemandantes2.ClearSelection();
        }

        private void btnAgregarDemandados_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaDemandados, "Demandado");

            frm.ShowDialog();
        }

        private void dtgDemandados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgDemandados.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgDemandados.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este demandado";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de demandados?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaDemandados.Remove(item);
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

        private void btnAgregarSolicitantes_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaSolicitantes, "Solicitante");

            frm.ShowDialog();
        }

        private void dtgSolicitantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgSolicitantes.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgSolicitantes.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este solicitante";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de solicitantes?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaSolicitantes.Remove(item);
                }
            }
        }

        private void dtgSolicitantes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgSolicitantes.Columns["id"] != null)
            {
                dtgSolicitantes.Columns["id"].Visible = false;
            }
            dtgSolicitantes.ClearSelection();
        }

        private void btnAgregarAutoridadesImpugnadas_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaAutoridadesImpugnadas, "Autoridad Impugnada");

            frm.ShowDialog();
        }

        private void dtgAutoridadesImpugnadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgAutoridadesImpugnadas.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgAutoridadesImpugnadas.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "esta autoridad impugnada";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de autoridades impugnadas?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaAutoridadesImpugnadas.Remove(item);
                }
            }
        }

        private void btnAgregarTercerosInteresados_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaTercerosInteresados, "Tercero Interesado");

            frm.ShowDialog();
        }

        private void dtgAutoridadesImpugnadas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAutoridadesImpugnadas.Columns["id"] != null)
            {
                dtgAutoridadesImpugnadas.Columns["id"].Visible = false;
            }
            dtgAutoridadesImpugnadas.ClearSelection();
        }

        private void dtgTercerosInteresados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgTercerosInteresados.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgTercerosInteresados.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este tercero interesado";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de terceros interesados?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaTercerosInteresados.Remove(item);
                }
            }
        }

        private void dtgTercerosInteresados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgTercerosInteresados.Columns["id"] != null)
            {
                dtgTercerosInteresados.Columns["id"].Visible = false;
            }
            dtgTercerosInteresados.ClearSelection();
        }

        
    }
}
