using ClosedXML.Excel;
using Comun;
using Comun.Models;
using Comun.Models.Reportes;
using DocumentFormat.OpenXml.EMMA;
using Dominio.Entidades.Reportes;
using Presentacion.Casos.Civiles.Proceso_ejecucion;
using Presentacion.Casos.Participantes;
using Presentacion.Reportes.BuscarPersonaForm;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void alistarListaContactosEmpresa()
        {
            dtgContactosEmpresa.DataSource = listaContactosEmpresa;

            dtgContactosEmpresa.AllowUserToAddRows = false;
            dtgContactosEmpresa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgContactosEmpresa.DataSource = listaContactosEmpresa;

            listaContactosEmpresa.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewContactosEmpresa();
            };

            CrearBotonQuitarContactosEmpresa();
            dtgContactosEmpresa.CellClick -= dtgContactosEmpresa_CellClick;
            dtgContactosEmpresa.CellClick += dtgContactosEmpresa_CellClick;
        }

        private void alistarListaAbogadosDirectores()
        {
            dtgAbogadosDirectores.DataSource = listaAbogadosDirectores;

            dtgAbogadosDirectores.AllowUserToAddRows = false;
            dtgAbogadosDirectores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgAbogadosDirectores.DataSource = listaAbogadosDirectores;

            listaAbogadosDirectores.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewAbogadosDirectores();
            };

            CrearBotonQuitarAbogadosDirectores();
            dtgAbogadosDirectores.CellClick -= dtgAbogadosDirectores_CellClick;
            dtgAbogadosDirectores.CellClick += dtgAbogadosDirectores_CellClick;
        }

        private void alistarListaAbogadosAsistentes()
        {
            dtgAbogadosAsistentes.DataSource = listaAbogadosAsistentes;

            dtgAbogadosAsistentes.AllowUserToAddRows = false;
            dtgAbogadosAsistentes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgAbogadosAsistentes.DataSource = listaAbogadosAsistentes;

            listaAbogadosAsistentes.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewAbogadosAsistentes();
            };

            CrearBotonQuitarAbogadosAsistentes();
            dtgAbogadosAsistentes.CellClick -= dtgAbogadosAsistentes_CellClick;
            dtgAbogadosAsistentes.CellClick += dtgAbogadosAsistentes_CellClick;
        }

        private void alistarListaSocioResponsable()
        {
            dtgSociosResponsables.DataSource = listaSociosResponsables;

            dtgSociosResponsables.AllowUserToAddRows = false;
            dtgSociosResponsables.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgSociosResponsables.DataSource = listaSociosResponsables;

            listaSociosResponsables.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewSociosResponsables();
            };

            CrearBotonQuitarSociosResponsables();
            dtgSociosResponsables.CellClick -= dtgSociosResponsables_CellClick;
            dtgSociosResponsables.CellClick += dtgSociosResponsables_CellClick;
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

        private void AjustarAlturaDataGridViewContactosEmpresa()
        {
            dtgContactosEmpresa.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgContactosEmpresa.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgContactosEmpresa.ColumnHeadersHeight;

            dtgContactosEmpresa.Height = alturaFilas + alturaHeaders + 22;

            dtgContactosEmpresa.ScrollBars = ScrollBars.None;
            dtgContactosEmpresa.PerformLayout();
        }

        private void AjustarAlturaDataGridViewAbogadosDirectores()
        {
            dtgAbogadosDirectores.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosDirectores.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosDirectores.ColumnHeadersHeight;

            dtgAbogadosDirectores.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosDirectores.ScrollBars = ScrollBars.None;
            dtgAbogadosDirectores.PerformLayout();
        }

        private void AjustarAlturaDataGridViewAbogadosAsistentes()
        {
            dtgAbogadosAsistentes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosAsistentes.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosAsistentes.ColumnHeadersHeight;

            dtgAbogadosAsistentes.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosAsistentes.ScrollBars = ScrollBars.None;
            dtgAbogadosAsistentes.PerformLayout();
        }

        private void AjustarAlturaDataGridViewSociosResponsables()
        {
            dtgSociosResponsables.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgSociosResponsables.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgSociosResponsables.ColumnHeadersHeight;

            dtgSociosResponsables.Height = alturaFilas + alturaHeaders + 22;

            dtgSociosResponsables.ScrollBars = ScrollBars.None;
            dtgSociosResponsables.PerformLayout();
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

        private void CrearBotonQuitarContactosEmpresa()
        {
            if (!dtgContactosEmpresa.Columns.Contains("Quitar"))
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

                dtgContactosEmpresa.Columns.Add(btnQuitar);
                dtgContactosEmpresa.Columns["Quitar"].DisplayIndex = dtgContactosEmpresa.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarAbogadosDirectores()
        {
            if (!dtgAbogadosDirectores.Columns.Contains("Quitar"))
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

                dtgAbogadosDirectores.Columns.Add(btnQuitar);
                dtgAbogadosDirectores.Columns["Quitar"].DisplayIndex = dtgAbogadosDirectores.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarAbogadosAsistentes()
        {
            if (!dtgAbogadosAsistentes.Columns.Contains("Quitar"))
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

                dtgAbogadosAsistentes.Columns.Add(btnQuitar);
                dtgAbogadosAsistentes.Columns["Quitar"].DisplayIndex = dtgAbogadosAsistentes.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarSociosResponsables()
        {
            if (!dtgSociosResponsables.Columns.Contains("Quitar"))
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

                dtgSociosResponsables.Columns.Add(btnQuitar);
                dtgSociosResponsables.Columns["Quitar"].DisplayIndex = dtgSociosResponsables.ColumnCount - 1;
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
            alistarListaContactosEmpresa();
            alistarListaAbogadosDirectores();
            alistarListaAbogadosAsistentes();
            alistarListaSocioResponsable();
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
            string? notificador = null;
            string? oficial = null;
            string? estado = null;
            string? tipoReferencia = null;
            string? motivoCasacion = null;
            string? titulo = null;
            var organos = new List<string>();
            int? tieneReferencia = null;
            int? incluirRelacionados = null;
            int soloTerminados = 0;
            List<int>? demandadosIds = null;
            List<int>? demandantesIds = null;
            List<int>? autoridadesIds = null;
            List<int>? solicitantesIds = null;
            List<int>? tercerosInteresadosIds = null;
            List<int>? contactosEmpresaIds = null;
            List<int>? abogadosDirectoresIds = null;
            List<int>? abogadosAsistentesIds = null;
            List<int>? sociosResponsablesIds = null;

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


            //organos judiciales
            if (checkBoxJuzgado.Checked && comboBoxJuzgado.SelectedItem != null)
            {
                organos.Add(comboBoxJuzgado.SelectedItem.ToString());
            }

            if (checkBoxSala.Checked && comboBoxSala.SelectedItem != null)
            {
                organos.Add(comboBoxSala.SelectedItem.ToString());
            }

            if (checkBoxCorte.Checked && comboBoxCorte.SelectedItem != null)
            {
                organos.Add(comboBoxCorte.SelectedItem.ToString());
            }

            if (checkBoxCamara.Checked && comboBoxCamara.SelectedItem != null)
            {
                organos.Add(comboBoxCamara.SelectedItem.ToString());
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

            if (checkBoxContactosEmpresa.Checked)
            {
                contactosEmpresaIds = listaContactosEmpresa
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                contactosEmpresaIds = null;
            }

            if (checkBoxAbogadosDirectores.Checked)
            {
                abogadosDirectoresIds = listaAbogadosDirectores
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                abogadosDirectoresIds = null;
            }

            if (checkBoxAbogadoAsistente.Checked)
            {
                abogadosAsistentesIds = listaAbogadosAsistentes
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                abogadosAsistentesIds = null;
            }

            if (checkBoxSociosResponsables.Checked)
            {
                sociosResponsablesIds = listaSociosResponsables
                    .Where(x => x.id != null)
                    .Select(x => x.id)
                    .ToList();
            }
            else
            {
                sociosResponsablesIds = null;
            }

            if (checkBoxMotivoCasacion.Checked)
            {
                switch (comboBoxMotivoCasacion.SelectedIndex)
                {
                    case 0: // Todos
                        motivoCasacion = "FORMA";
                        break;
                    case 1: // Solo vinculados
                        motivoCasacion = "FONDO";
                        break;
                    case 2: // Sin vinculación
                        motivoCasacion = "FORMA Y FONDO";
                        break;
                }
            }
            else
            {
                motivoCasacion = null;
            }

            if (checkBoxTitulo.Checked)
            {
                titulo = comboBoxTitulo.SelectedItem?.ToString();
            }
            else
            {
                titulo = null;
            }


            // Aquí decides el separador
            string? organosJudiciales = organos.Count > 0
                ? string.Join(", ", organos)
                : null;

            var req = new ReporteMaestroCasosRequest
            {
                UsuarioId = usuarioId,
                Pagina = 1,
                RegistrosPorPagina = 20,
                ModuloId = rama,
                Origen = subrama,
                EstadoActual = estado,
                Expediente = expediente,
                //partes interesadas
                DemandanteIds = demandantesIds,
                DemandadoIds = demandadosIds,
                SolicitanteIds = solicitantesIds,
                AutoridadIds = autoridadesIds,
                TerceroIds = tercerosInteresadosIds,
                ContactoIds = contactosEmpresaIds,
                // equipo legal
                AbogadoDirectorIds = abogadosDirectoresIds,
                AsistenteIds = abogadosAsistentesIds,
                SocioIds = sociosResponsablesIds,
                TieneReferencia = tieneReferencia,
                TipoReferencia = tipoReferencia,
                SoloTerminados = soloTerminados,
                IncluirRelacionados = incluirRelacionados,
                MotivoCasacion = motivoCasacion,
                Titulo = titulo,
                OrganoJudicial = organosJudiciales,
                FechaDesde = null,
                FechaHasta = null
            };


            var resp = await reporteModel.ObtenerReporteMaestroCasosExportacionRelacionados(req);

            if (resp.success)
            {
                var total = resp.total;
                var lista = resp.data;

                dtgResultadosReporte.DataSource = null;
                dtgResultadosReporte.DataSource = lista;

                ConfigurarColumnasReporte(rama, titulo, motivoCasacion);

                dtgResultadosReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dtgResultadosReporte.Refresh();
            }
            else
            {
                var mensaje = resp.message;
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasReporte(int? rama, string? titulo, string? motivoCasacion)
        {
            if (dtgResultadosReporte.Columns.Count == 0)
                return;

            // Ocultar primero las columnas opcionales
            OcultarSiExiste("causa");
            OcultarSiExiste("titulo");
            OcultarSiExiste("motivo_casacion");
            OcultarSiExiste("demandantes");
            OcultarSiExiste("demandados");
            OcultarSiExiste("solicitantes");
            OcultarSiExiste("autoridades_impugnadas");

            // Reglas:
            // rama = 1 Laboral
            // rama = 2 Civil
            // rama = 3 Constitucional
            // rama = 4 Contencioso Administrativo
            // rama = null => Todas

            if (rama == 3) // Constitucional
            {
                MostrarSiExiste("causa");
                MostrarSiExiste("solicitantes");
                MostrarSiExiste("autoridades_impugnadas");

                OcultarSiExiste("demandantes");
                OcultarSiExiste("demandados");
                OcultarSiExiste("titulo");
                OcultarSiExiste("motivo_casacion");
            }
            else if (rama.HasValue && rama != null) // Cualquier rama específica distinta de "Todas"
            {
                MostrarSiExiste("demandantes");
                MostrarSiExiste("demandados");

                OcultarSiExiste("solicitantes");
                OcultarSiExiste("autoridades_impugnadas");
                OcultarSiExiste("causa");

                if (rama == 2) // Civil
                {
                    MostrarSiExiste("titulo");
                }

                if (rama == 4 //&& !string.IsNullOrWhiteSpace(motivoCasacion)
                    ) // Contencioso + filtro motivo
                {
                    MostrarSiExiste("motivo_casacion");
                }
            }
            else // Todas
            {
                // En "Todas", para evitar mezclar tipos, deja ocultas las columnas especiales
                MostrarSiExiste("demandantes");
                MostrarSiExiste("demandados");
                MostrarSiExiste("solicitantes");
                MostrarSiExiste("autoridades_impugnadas");
                MostrarSiExiste("titulo");
                MostrarSiExiste("motivo_casacion");
                MostrarSiExiste("causa");
                OcultarSiExiste("abogados_asistentes");
            }

            // Encabezados bonitos
            RenombrarColumna("expediente", "Expediente");
            RenombrarColumna("nombre_particular", "Nombre particular");
            RenombrarColumna("tipo_instancia", "Tipo instancia");
            RenombrarColumna("organo_judicial", "Organo judicial");
            RenombrarColumna("oficial", "Oficial");
            RenombrarColumna("notificador", "Notificador");
            RenombrarColumna("causa", "Causa");
            RenombrarColumna("titulo", "Titulo");
            RenombrarColumna("motivo_casacion", "Motivo de casacion");
            RenombrarColumna("rama", "Rama");
            RenombrarColumna("estado_actual", "Estado actual");
            RenombrarColumna("origen_actual", "Origen actual");
            RenombrarColumna("abogados_directores", "Abogados directores");
            RenombrarColumna("socios_responsables", "Socios responsables");
            RenombrarColumna("abogados_asistentes", "Abogados asistentes");
            RenombrarColumna("demandantes", "Demandantes");
            RenombrarColumna("demandados", "Demandados");
            RenombrarColumna("solicitantes", "Solicitantes");
            RenombrarColumna("autoridades_impugnadas", "Autoridades impugnadas");
            RenombrarColumna("terceros_interesados", "Terceros interesados");
            RenombrarColumna("contactos_empresa", "Contactos empresa");
            RenombrarColumna("referencias", "Referencias");
            RenombrarColumna("ultima_anotacion", "Ultima anotacion");
        }

        private void OcultarSiExiste(string nombreColumna)
        {
            if (dtgResultadosReporte.Columns.Contains(nombreColumna))
                dtgResultadosReporte.Columns[nombreColumna].Visible = false;
        }

        private void MostrarSiExiste(string nombreColumna)
        {
            if (dtgResultadosReporte.Columns.Contains(nombreColumna))
                dtgResultadosReporte.Columns[nombreColumna].Visible = true;
        }

        private void RenombrarColumna(string nombreColumna, string encabezado)
        {
            if (dtgResultadosReporte.Columns.Contains(nombreColumna))
                dtgResultadosReporte.Columns[nombreColumna].HeaderText = encabezado;
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

        private void btnAgregarContactosEmpresa_Click(object sender, EventArgs e)
        {
            var frm = new Presentacion.Reportes.BuscarPersonaForm.BuscarPersonaForm(listaContactosEmpresa, "Contacto Empresa");

            frm.ShowDialog();
        }

        private void dtgContactosEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgContactosEmpresa.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgContactosEmpresa.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este contacto de empresa";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de contactos de empresa?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaContactosEmpresa.Remove(item);
                }
            }
        }

        private void dtgContactosEmpresa_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgContactosEmpresa.Columns["id"] != null)
            {
                dtgContactosEmpresa.Columns["id"].Visible = false;
            }
            dtgContactosEmpresa.ClearSelection();
        }

        private void btnAgregarAbogadosDirectores_Click(object sender, EventArgs e)
        {
            var frm = new BuscarAbogadoForm(listaAbogadosDirectores, "Abogado Director");

            frm.ShowDialog();
        }

        private void dtgAbogadosDirectores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgAbogadosDirectores.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgAbogadosDirectores.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Usuario ?? "este abogado director";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de abogados directores?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaAbogadosDirectores.Remove(item);
                }
            }
        }

        private void dtgAbogadosDirectores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAbogadosDirectores.Columns["id"] != null)
            {
                dtgAbogadosDirectores.Columns["id"].Visible = false;
            }
            dtgAbogadosDirectores.ClearSelection();
        }

        private void btnAgregarAbogadoAsistente_Click(object sender, EventArgs e)
        {
            var frm = new BuscarAbogadoForm(listaAbogadosAsistentes, "Abogado Asistente");

            frm.ShowDialog();
        }

        private void dtgAbogadosAsistentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgAbogadosAsistentes.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgAbogadosAsistentes.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Usuario ?? "este abogado asistente";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de abogados asistentes?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaAbogadosAsistentes.Remove(item);
                }
            }
        }

        private void dtgAbogadosAsistentes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAbogadosAsistentes.Columns["id"] != null)
            {
                dtgAbogadosAsistentes.Columns["id"].Visible = false;
            }
            dtgAbogadosAsistentes.ClearSelection();
        }

        private void btnAgregarSocioResponsable_Click(object sender, EventArgs e)
        {
            var frm = new BuscarAbogadoForm(listaSociosResponsables, "Socio Responsable");

            frm.ShowDialog();
        }

        private void dtgSociosResponsables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgSociosResponsables.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgSociosResponsables.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Usuario ?? "este socio responsable";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de socios responsables?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaSociosResponsables.Remove(item);
                }
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
        private DataTable ObtenerDataTableDesdeGrid(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // Solo columnas visibles
            var columnasVisibles = dgv.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            foreach (var col in columnasVisibles)
            {
                string nombreColumna = string.IsNullOrWhiteSpace(col.DataPropertyName)
                    ? col.Name
                    : col.DataPropertyName;

                if (!dt.Columns.Contains(nombreColumna))
                    dt.Columns.Add(nombreColumna);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                DataRow dr = dt.NewRow();

                foreach (var col in columnasVisibles)
                {
                    string nombreColumna = string.IsNullOrWhiteSpace(col.DataPropertyName)
                        ? col.Name
                        : col.DataPropertyName;

                    var valor = row.Cells[col.Name].Value;
                    dr[nombreColumna] = valor?.ToString() ?? "";
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private async Task CrearPdfReporteMaestroCasos(DataGridView dgv, string titulo)
        {
            DataTable dt = ObtenerDataTableDesdeGrid(dgv);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el PDF");
                return;
            }

            string chromePath = "chrome";

            string[] possiblePaths =
            {
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\Application\\chrome.exe")
    };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    chromePath = path;
                    break;
                }
            }

            string base64Logo;
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.logoBPA2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                base64Logo = Convert.ToBase64String(ms.ToArray());
            }

            string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' style='width:150px;' />";

            int cantidadColumnas = dt.Columns.Count;

            int bodyFontSize = cantidadColumnas >= 16 ? 8 :
                   cantidadColumnas >= 13 ? 9 :
                   cantidadColumnas >= 10 ? 10 : 11;

            int headerFontSize = cantidadColumnas >= 16 ? 8 :
                                 cantidadColumnas >= 13 ? 9 :
                                 cantidadColumnas >= 10 ? 10 : 11;

            int bodyPadding = cantidadColumnas >= 16 ? 4 :
                              cantidadColumnas >= 13 ? 5 : 6;

            int headerPadding = cantidadColumnas >= 16 ? 4 :
                                cantidadColumnas >= 13 ? 5 : 6;

            string headers = "";
            foreach (DataColumn col in dt.Columns)
            {
                headers += $"<th>{System.Net.WebUtility.HtmlEncode(col.ColumnName.Replace("_", " ").ToUpper())}</th>";
            }

            string rows = "";
            foreach (DataRow row in dt.Rows)
            {
                rows += "<tr>";

                foreach (DataColumn col in dt.Columns)
                {
                    object value = row[col.ColumnName] ?? "";

                    if (DateTime.TryParse(value.ToString(), out DateTime fecha))
                    {
                        value = fecha.ToString("dd/MM/yyyy HH:mm");
                    }

                    rows += $"<td>{System.Net.WebUtility.HtmlEncode(value.ToString())}</td>";
                }

                rows += "</tr>";
            }

            string html = $@"
    <html>
    <head>
        <meta charset='UTF-8'>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                color: #222;
            }}

            .header {{
                text-align: center;
                font-size: 20px;
                font-weight: bold;
                margin-bottom: 4px;
            }}

            .subheader {{
                text-align: center;
                margin-bottom: 10px;
                font-size: 11px;
            }}

            .logo {{
                text-align: center;
                margin-bottom: 12px;
            }}

            table {{
                border-collapse: collapse;
                width: 100%;
                table-layout: auto;
            }}

            thead {{
                display: table-header-group;
            }}

            tbody {{
                display: table-row-group;
            }}

            tr {{
                page-break-inside: avoid !important;
                break-inside: avoid-page !important;
            }}

            th {{
                background-color: #274e77 !important;
                color: white;
                padding: {headerPadding}px;
                border: 1px solid #d6d6d6;
                text-align: center;
                vertical-align: middle;
                font-weight: 700;
                font-size: {headerFontSize}px;
                line-height: 1.3;
                white-space: normal;
                word-break: break-word;
            }}

            td {{
                padding: {bodyPadding}px;
                border: 1px solid #ddd;
                vertical-align: top;
                font-size: {bodyFontSize}px;
                line-height: 1.2;
                white-space: normal;
                word-break: break-word;
            }}

            tr:nth-child(even) {{
                background-color: #f9f9f9;
            }}

            @page {{
                size: legal landscape;
                margin: 12mm;
            }}
        </style>
    </head>
    <body>
        <div class='header'>{System.Net.WebUtility.HtmlEncode(titulo)}</div>
        <div class='subheader'>Generado el {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:HH:mm}</div>

        <div class='logo'>
            {imageHtml}
        </div>

        <table>
            <thead>
                <tr>{headers}</tr>
            </thead>
            <tbody>
                {rows}
            </tbody>
        </table>
    </body>
    </html>";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = titulo.Replace(" ", "_") + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".pdf"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = chromePath
            });

            try
            {
                var page = await browser.NewPageAsync();

                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1600,
                    Height = 1200,
                    DeviceScaleFactor = 1
                });

                await page.SetContentAsync(html);

                decimal scale = await CalcularEscalaPdfAsync(page);

                await page.PdfAsync(saveFileDialog.FileName, new PdfOptions
                {
                    Format = PaperFormat.Legal,
                    Landscape = true,
                    PrintBackground = true,
                    Scale = scale
                });

                MessageBox.Show("PDF generado correctamente");
                Process.Start("explorer.exe", Path.GetDirectoryName(saveFileDialog.FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
            finally
            {
                await browser.CloseAsync();
            }
        }

        private async Task<decimal> CalcularEscalaPdfAsync(PuppeteerSharp.IPage page)
        {
            int anchoContenido = await page.EvaluateFunctionAsync<int>(@"() => {
        const table = document.querySelector('table');
        const body = document.body;
        const html = document.documentElement;

        const tableWidth = table ? table.scrollWidth : 0;
        const bodyWidth = body ? body.scrollWidth : 0;
        const htmlWidth = html ? html.scrollWidth : 0;

        return Math.max(tableWidth, bodyWidth, htmlWidth);
    }");

            decimal anchoDisponiblePx = 1250m;

            if (anchoContenido <= 0)
                return 1.0m;

            decimal scale = anchoDisponiblePx / anchoContenido;

            if (scale > 1.0m)
                scale = 1.0m;

            if (scale < 0.55m)
                scale = 0.55m;

            return Math.Round(scale, 2);
        }

        /*
        private async Task CrearPdfReporteMaestroCasos(DataGridView dgv, string titulo)
        {
            DataTable dt = ObtenerDataTableDesdeGrid(dgv);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el PDF");
                return;
            }

            string chromePath = "chrome";

            string[] possiblePaths = {
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\Application\\chrome.exe")
    };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    chromePath = path;
                    break;
                }
            }

            string base64Logo;
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.logoBPA2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                base64Logo = Convert.ToBase64String(ms.ToArray());
            }

            string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' style='width:150px;' />";

            string headers = "";
            foreach (DataColumn col in dt.Columns)
            {
                headers += $"<th>{col.ColumnName.Replace("_", " ").ToUpper()}</th>";
            }

            string rows = "";
            foreach (DataRow row in dt.Rows)
            {
                rows += "<tr>";

                foreach (DataColumn col in dt.Columns)
                {
                    object value = row[col.ColumnName] ?? "";

                    if (DateTime.TryParse(value.ToString(), out DateTime fecha))
                    {
                        value = fecha.ToString("dd/MM/yyyy HH:mm");
                    }

                    rows += $"<td>{System.Net.WebUtility.HtmlEncode(value.ToString())}</td>";
                }

                rows += "</tr>";
            }

            string html = $@"
                <html>
                <head>
                <meta charset='UTF-8'>
                <style>
                    body {{
                        font-family: Arial;
                        font-size: 10px;
                    }}

                    .header {{
                        text-align: center;
                        font-size: 20px;
                        font-weight: bold;
                    }}

                    .subheader {{
                        text-align: center;
                        margin-bottom: 10px;
                    }}

                    .logo {{
                        text-align: center;
                        margin-bottom: 10px;
                    }}

                    table {{
                        border-collapse: collapse;
                        width: 100%;
                    }}

                    th {{
                        background-color: #274e77 !important;
                        color: white;
                        padding: 6px;
                        border: 1px solid #ddd;
                        text-align: center;
                    }}

                    td {{
                        padding: 6px;
                        border: 1px solid #ddd;
                        vertical-align: top;
                        word-break: break-word;
                    }}

                    tr:nth-child(even) {{
                        background-color: #f9f9f9;
                    }}

                    @page {{
                        size: legal landscape;
                        margin: 12mm;
                    }}
                </style>
                </head>

                <body>
                    <div class='header'>{titulo}</div>
                    <div class='subheader'>Generado el {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:HH:mm}</div>

                    <div class='logo'>
                        {imageHtml}
                    </div>

                    <table>
                        <thead>
                            <tr>{headers}</tr>
                        </thead>
                        <tbody>
                            {rows}
                        </tbody>
                    </table>
                </body>
                </html>";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = titulo.Replace(" ", "_") + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".pdf"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = chromePath
            });

            var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);

            await page.PdfAsync(saveFileDialog.FileName, new PdfOptions
            {
                Format = PaperFormat.Legal,
                Landscape = true,
                PrintBackground = true,
                Scale = 0.6m
            });

            await browser.CloseAsync();

            MessageBox.Show("PDF generado correctamente");
            Process.Start("explorer.exe", Path.GetDirectoryName(saveFileDialog.FileName));
        }*/


        public void ExportarReporteMaestroCasosAExcel(DataGridView dgv, string titulo)
        {
            DataTable dataTable = ObtenerDataTableDesdeGrid(dgv);

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = nombre + ".xlsx"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string tempLogoPath = Path.Combine(Path.GetTempPath(), "logo_temp.png");
                Properties.Resources.logoBPA2.Save(tempLogoPath);

                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("REPORTE");

                    ws.Cell("E2").Value = titulo;
                    ws.Cell("E2").Style.Font.Bold = true;
                    ws.Cell("E2").Style.Font.FontSize = 16;
                    ws.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    ws.Cell("E3").Value = $"Generado el {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:HH:mm}";
                    ws.Cell("E3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    if (File.Exists(tempLogoPath))
                    {
                        ws.AddPicture(tempLogoPath)
                          .MoveTo(ws.Cell("A2"))
                          .Scale(0.15);
                    }

                    int startRow = 6;
                    int colIndex = 1;

                    foreach (DataColumn col in dataTable.Columns)
                    {
                        ws.Cell(startRow, colIndex).Value = col.ColumnName.Replace("_", " ").ToUpper();

                        var cell = ws.Cell(startRow, colIndex);
                        cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#274e77");
                        cell.Style.Font.FontColor = XLColor.White;
                        cell.Style.Font.Bold = true;
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        colIndex++;
                    }

                    int rowIndex = startRow + 1;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        colIndex = 1;

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            object value = row[col.ColumnName] ?? "";

                            if (DateTime.TryParse(value.ToString(), out DateTime fecha))
                            {
                                value = fecha.ToString("dd/MM/yyyy HH:mm");
                            }

                            ws.Cell(rowIndex, colIndex).Value = value.ToString();
                            colIndex++;
                        }

                        rowIndex++;
                    }

                    var rangoTabla = ws.Range(startRow, 1, rowIndex - 1, dataTable.Columns.Count);
                    var tabla = rangoTabla.CreateTable();

                    tabla.Theme = XLTableTheme.TableStyleMedium2;
                    tabla.ShowAutoFilter = true;

                    ws.Columns().AdjustToContents();

                    var rango = ws.Range(startRow, 1, rowIndex - 1, dataTable.Columns.Count);
                    rango.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rango.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    workbook.SaveAs(saveFileDialog.FileName);
                }

                if (File.Exists(tempLogoPath))
                    File.Delete(tempLogoPath);

                MessageBox.Show("Excel generado correctamente");
                Process.Start("explorer.exe", Path.GetDirectoryName(saveFileDialog.FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar Excel: " + ex.Message);
            }
        }
        private async void btnExportarPdf_Click(object sender, EventArgs e)
        {
            await CrearPdfReporteMaestroCasos(dtgResultadosReporte, "REPORTE DE CASOS");
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarReporteMaestroCasosAExcel(dtgResultadosReporte, "REPORTE DE CASOS");
        }
    }
}
