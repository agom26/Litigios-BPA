using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Contenciosos;
using Comun.Models.Casos.Laborales;
using Dominio.Entidades;
using Dominio.Entidades.Civiles;
using Dominio.Entidades.Contenciosos;
using Presentacion.Casos.Abogados_asignados;
using Presentacion.Casos.Civiles.Estados_civil;
using Presentacion.Casos.Civiles.Mostrar_Casos;
using Presentacion.Casos.Contenciosos.Estados_contenciosos;
using Presentacion.Casos.Contenciosos.Mostrar_marcas;
using Presentacion.Casos.Estados;
using Presentacion.Casos.Participantes;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Presentacion.Casos.Contenciosos
{
    public partial class Contencioso_General_PI : Form, IAsyncLoadable
    {
        private bool _yaCargo = false;
        private bool _huboCambioEstado = false;

        private bool _actualizandoCaso = false;
        private bool _cargandoCaso = false;
        private bool _cargando = false;
        private bool _procesandoArchivo = false;

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;

        private int _idHistorialEditar = 0;
        private int _casoIdHistorialEditar = 0;

        private BindingSource bsTercerosInteresados = new BindingSource();
        private int _idCasoEditar;

        HistorialCasoContenciosoModel historialModel = new HistorialCasoContenciosoModel();
        ArchivosContenciososModel archivoModel = new ArchivosContenciososModel();
        CAGeneralModel casoContenciosoModel = new CAGeneralModel();
        CARecursoCasacionModel recursoCasacionModel = new CARecursoCasacionModel();
        //caso 
        private BindingList<PersonaListDataResponse> listaDemandados
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaDemandantes
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
        private bool isAdminContencioso = false;
        private int? idMarcaReferencia = null;
        private string? expedienteCasacion = null;
        private string? motivoCasacion = null;
        private void VerificarTipoUsuario()

        {
            isAdminContencioso = UserSession.Modulos.Any(m =>
                (m.clave_slug ?? "").Trim().Equals("contencioso administrativo", StringComparison.OrdinalIgnoreCase) &&
                (m.nombre_rol ?? "").Trim().Equals("Administrador", StringComparison.OrdinalIgnoreCase));
        }

        public async Task LoadAsync()
        {
            if (_yaCargo) return;
            _yaCargo = true;

            panelBotonesCaso.Visible = false;
            dtgCasosContenciosos.DataSource = bsTercerosInteresados;

            await CargarCasos();

            dtgCasosContenciosos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            if (dtgCasosContenciosos.Columns.Contains("Editar"))
                dtgCasosContenciosos.Columns["Editar"].Width = 40;

            if (dtgCasosContenciosos.Columns.Contains("Eliminar"))
                dtgCasosContenciosos.Columns["Eliminar"].Width = 40;

            if (dtgCasosContenciosos.Columns.Contains("Terminar"))
                dtgCasosContenciosos.Columns["Terminar"].Width = 40;

            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(tabPageHistorial);
            EliminarTabPage(tabPageEditarHistorial);
            EliminarTabPage(tabPageMarcasReferencia);

            //caso
            alistarListaDemandantes();
            alistarListaDemandados();
            alistarListaTercerosInteresados();
            alistarListaContactosEmpresa();
            alistarListaAbogadosDirectores();
            alistarListaSociosResponsables();
            alistarListaAbogadosAsistentes();

            

            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;

            panelDemandados.AutoSize = true;
            panelDemandados.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelDemandantes.AutoSize = true;
            panelDemandantes.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelTercerosInteresados.AutoSize = true;
            panelTercerosInteresados.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelContactosEmpresas.AutoSize = true;
            panelContactosEmpresas.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelAbogadosDirectores.AutoSize = true;
            panelAbogadosDirectores.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelSociosResponsables.AutoSize = true;
            panelSociosResponsables.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelAbogadosAsistentes.AutoSize = true;
            panelAbogadosAsistentes.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }

        private async Task EjecutarConLoaderAsync(Func<Task> accion)
        {
            using (var loading = new Presentacion.Alertas.FrmLoading(accion))
            {
                if (_cargando) return;

                _cargando = true;

                this.Enabled = false;
                loading.Show(this);

                // Centrar respecto al formulario actual
                var centro = this.PointToScreen(Point.Empty);

                loading.Left = centro.X + (this.ClientSize.Width - loading.Width) / 2;
                loading.Top = centro.Y + (this.ClientSize.Height - loading.Height) / 2;

                try
                {
                    while (loading.Visible)
                    {
                        await Task.Delay(100);
                    }
                }
                finally
                {
                    this.Enabled = true;
                    _cargando = false;
                }
            }
        }

        public Contencioso_General_PI()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            comboBoxJuzgado.SelectedIndex = -1;
            comboboxOficial.SelectedIndex= -1;
            txtNombreParticular.Text = "";
            comboboxNotificador.SelectedIndex = -1;
            txtNombreParticular.Text = "";
            txtEstado.Text = "";
            txtObservaciones.Text = "";
            LimpiarListas();
        }

        private void LimpiarFormularioMR()
        {
            txtExpedienteMarcaReferencia.Text = "";
            txtSignoMarcaReferencia.Text = "";
            txtSignoDistintivoMarcaReferencia.Text = "";
            txtTipoSignoDistintivoMarcaReferencia.Text = "";
            txtClaseMarcaReferencia.Text = "";
            txtTitularMarcaReferencia.Text = "";

            txtEstadoCasoReferencia.Text = "";
            textBoxObervacionesCasoReferencia.Text = "";
            

            txtSignoMarcaReferencia.Text = "";
            txtSignoMarcaReferencia.Text = "";
        }

        private async Task CargarDatosCaso(int idCaso)
        {
            int idUsuario = UserSession.Id;
            var resp = await casoContenciosoModel.ObtenerCasoContenciosoPorId(idUsuario, idCaso);

            if (!resp.success || resp.data == null)
            {
                MessageBox.Show(resp.message ?? "No se pudo cargar el caso");
                return;
            }

            var data = resp.data;

            if (data != null)
            {
                txtExpediente.Text = data.caso.expediente ?? "";
                comboBoxJuzgado.Text = data.caso.sala ?? "";
                comboboxOficial.Text = data.caso.oficial ?? "";
                comboboxNotificador.Text = data.caso.notificador ?? "";
                txtNombreParticular.Text = data.caso.nombre_particular ?? "";
                // si tienes estado/observaciones en textbox:
                txtEstado.Text = data.caso.estado ?? "";
                
                txtObservaciones.Text = (data.caso.observaciones ?? "")
                    .Replace("\n", Environment.NewLine); ;
            }

            var motivoCasacion = resp.data?.recurso_casacion?.motivo;

            if (motivoCasacion == "FORMA")
            {
                comboBoxMotivoCasacion.SelectedItem = "De forma";
            }
            else if (motivoCasacion == "FONDO")
            {
                comboBoxMotivoCasacion.SelectedItem = "De fondo";
            }
            else if (motivoCasacion == "FORMA Y FONDO")
            {
                comboBoxMotivoCasacion.SelectedItem = "De forma y fondo";
            }
            else
            {
                comboBoxMotivoCasacion.SelectedIndex = -1; // 🔥 limpia selección
            }

            txtExpedienteRecursoCasacion.Text = resp.data?.recurso_casacion?.expediente ?? "";
            comboBoxJuzgado.SelectedIndex = 0;
            LimpiarListas();

            // 3) Personas por rol -> tus BindingList<PersonaListDataResponse>
            var p = data.personas_por_rol ?? new Dictionary<string, List<PersonaMiniDto>>();

            MapPersonas(p, "Demandante", listaDemandantes);
            MapPersonas(p, "Demandado", listaDemandados);
            MapPersonas(p, "Tercero Interesado", listaTercerosInteresados);
            MapPersonas(p, "Contacto de Empresa", listaContactosEmpresa);

            // 4) Usuarios por rol -> tus BindingList<UserListDataResponse>
            var u = data.usuarios_por_rol ?? new Dictionary<string, List<UsuarioMiniDto>>();

            MapUsuarios(u, "Abogado Director", listaAbogadosDirectores);
            MapUsuarios(u, "Socio Responsable", listaSociosResponsables);
            MapUsuarios(u, "Abogado Asistente", listaAbogadosAsistentes);

            // 5) refrescar grids
            dtgDemandantes.Refresh();
            dtgDemandados.Refresh();
            dtgTercerosInteresados.Refresh();
            dtgContactoEmpresa.Refresh();

            dtgAbogadosDirectores.Refresh();
            dtgSociosResponsables.Refresh();
            dtgAbogadosAsistentes.Refresh();

            var casoReferencia = data.referencia_recurso ?? null;
            if(casoReferencia != null)
            {
                idMarcaReferencia = casoReferencia.recurso_revocatoria_id;
            }
            else
            {
                idMarcaReferencia = 0;
            }

            AjustarFilasSegunEstado(txtEstado.Text);
            // 6) Ir al tab Detalles
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
            btnGuardarCaso.Visible = false;
            btnEditarCaso.Visible = true;
        }

        private async Task CargarDatosMarcaReferencia(int idMarca)
        {
            CAObtenerMarcasContenciosasModel marcasModel = new CAObtenerMarcasContenciosasModel();

            
            int idUsuario = UserSession.Id;
            var resp = await marcasModel.ObtenerMarcaReferenciaPorId(idMarca);
            
            if (!resp.success || resp.data == null)
            {
                MessageBox.Show(resp.message ?? "No se pudo cargar la marca de referencia");
                return;
            }

            var data = resp.data;

            if (data != null)
            {
                txtExpedienteMarcaReferencia.Text = data.expediente ?? "";
                txtSignoMarcaReferencia.Text = data.signo ?? "";
                txtClaseMarcaReferencia.Text = data.clase ?? "";
                txtSignoDistintivoMarcaReferencia.Text = data.signo_distintivo ?? "";
                txtTipoSignoDistintivoMarcaReferencia.Text = data.tipoSigno ?? "";
                txtTitularMarcaReferencia.Text = data.titular ?? "";
                
            }
        }

        // Helpers de mapeo
        private void MapPersonas(
            Dictionary<string, List<PersonaMiniDto>> dict,
            string rol,
            BindingList<PersonaListDataResponse> target)
        {
            if (!dict.TryGetValue(rol, out var items) || items == null) return;

            foreach (var it in items)
            {
                target.Add(new PersonaListDataResponse
                {
                    id = it.id,
                    Nombre = it.nombre,
                    Direccion = it.direccion
                });
            }
        }

        private void MapUsuarios(
            Dictionary<string, List<UsuarioMiniDto>> dict,
            string rol,
            BindingList<UserListDataResponse> target)
        {
            if (!dict.TryGetValue(rol, out var items) || items == null) return;

            foreach (var it in items)
            {
                target.Add(new UserListDataResponse
                {
                    id = it.id,
                    Nombres = it.nombres,
                    Apellidos = it.apellidos,
                    Usuario = it.usuario,
                    Correo = it.correo
                });
            }
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
                dtgCasosContenciosos.ClearSelection();
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = nombre;
        }



        private void CrearBotonesAccion(DataGridView dtg)
        {
            if (!dtg.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = "✏️",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEditar);
            }
            VerificarTipoUsuario();

            if (isAdminContencioso == true)
            {
                if (!dtg.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                    {
                        Name = "Eliminar",
                        HeaderText = "",
                        Text = "🗑️",
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Standard,
                        Width = 40,
                        MinimumWidth = 40,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };
                    dtg.Columns.Add(btnEliminar);
                }

                if (!dtg.Columns.Contains("Terminar"))
                {
                    DataGridViewButtonColumn btnTerminar = new DataGridViewButtonColumn
                    {
                        Name = "Terminar",
                        HeaderText = "",
                        Text = "🔒",
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Standard,
                        Width = 40,
                        MinimumWidth = 40,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };
                    dtg.Columns.Add(btnTerminar);
                }
            }

            if (dtg.Columns.Contains("Editar"))
                dtg.Columns["Editar"].DisplayIndex = dtg.ColumnCount - 1;

            if (isAdminContencioso == true && dtg.Columns.Contains("Eliminar"))
                dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 2;

            if (isAdminContencioso == true && dtg.Columns.Contains("Terminar"))
                dtg.Columns["Terminar"].DisplayIndex = dtg.ColumnCount - 3;
        }

        private void CrearBotonesAccionHistorial(DataGridView dtg)
        {
            if (!dtg.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = "✏️",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEditar);
            }
            VerificarTipoUsuario();

            if (isAdminContencioso == true)
            {
                if (!dtg.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                    {
                        Name = "Eliminar",
                        HeaderText = "",
                        Text = "🗑️",
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Standard,
                        Width = 40,
                        MinimumWidth = 40,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };
                    dtg.Columns.Add(btnEliminar);
                }
            }

            if (dtg.Columns.Contains("Editar"))
                dtg.Columns["Editar"].DisplayIndex = dtg.ColumnCount - 1;

            if (isAdminContencioso == true && dtg.Columns.Contains("Eliminar"))
                dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 2;

        }

        private void CentrarPanel()
        {
            int anchoMinimo = panelBusquedaCaso.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusquedaCaso.Anchor = AnchorStyles.None;
                panelBusquedaCaso.Dock = DockStyle.Top;
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusquedaCaso.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusquedaCaso.Location = new Point(0, 0); // o donde quieras
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Nuevo Caso Contencioso Administrativo General";
            btnGuardarCaso.Text = "Guardar";
            AjustarFilasSegunEstado("");
            LimpiarFormulario();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
            btnGuardarCaso.Visible = true;
            btnEditarCaso.Visible = false;
        }

        private async Task CargarCasos()
        {

            int idUsuario = UserSession.Id;
            string filtro = txtBuscar.Text;
            var response = await casoContenciosoModel.ObtenerCasosContenciosos(idUsuario, paginaActual, registrosPorPagina, filtro);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsTercerosInteresados.DataSource = response.data;
                dtgCasosContenciosos.Refresh();
                // Actualizar paginación
                totalRegistros = response.total;
                labelTotal.Text = $"Total de casos contenciosos: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        //caso
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
        private void alistarListaDemandantes()
        {
            dtgDemandantes.DataSource = listaDemandantes;

            dtgDemandantes.AllowUserToAddRows = false;
            dtgDemandantes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgDemandantes.DataSource = listaDemandantes;

            listaDemandantes.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewDemandantes();
            };

            CrearBotonQuitarDemandante();
            dtgDemandantes.CellClick -= dtgDemandantes_CellClick;
            dtgDemandantes.CellClick += dtgDemandantes_CellClick;
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

            CrearBotonQuitarTerceroInteresado();
            dtgTercerosInteresados.CellClick -= dtgPartesInteresadas_CellClick;
            dtgTercerosInteresados.CellClick += dtgPartesInteresadas_CellClick;
        }
        private void alistarListaContactosEmpresa()
        {
            dtgContactoEmpresa.DataSource = listaContactosEmpresa;

            dtgContactoEmpresa.AllowUserToAddRows = false;
            dtgContactoEmpresa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgContactoEmpresa.DataSource = listaContactosEmpresa;

            listaContactosEmpresa.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewContactosEmpresa();
            };

            CrearBotonQuitarContactoEmpresa();
            dtgContactoEmpresa.CellClick -= dtgContactoEmpresa_CellClick;
            dtgContactoEmpresa.CellClick += dtgContactoEmpresa_CellClick;
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

            CrearBotonQuitarAbogadoDirector();
            dtgAbogadosDirectores.CellClick -= dtgAbogadosDirectores_CellClick;
            dtgAbogadosDirectores.CellClick += dtgAbogadosDirectores_CellClick;
        }

        private void alistarListaSociosResponsables()
        {
            dtgSociosResponsables.DataSource = listaSociosResponsables;

            dtgSociosResponsables.AllowUserToAddRows = false;
            dtgSociosResponsables.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgSociosResponsables.DataSource = listaSociosResponsables;

            listaSociosResponsables.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewSociosResponsables();
            };

            CrearBotonQuitarSocioResponsable();
            dtgSociosResponsables.CellClick -= dtgSociosResponsables_CellClick;
            dtgSociosResponsables.CellClick += dtgSociosResponsables_CellClick;
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

            CrearBotonQuitarAbogadoAsistente();
            dtgAbogadosAsistentes.CellClick -= dtgAbogadosAsistentes_CellClick;
            dtgAbogadosAsistentes.CellClick += dtgAbogadosAsistentes_CellClick;
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

        private void CrearBotonQuitarDemandante()
        {
            if (!dtgDemandantes.Columns.Contains("Quitar"))
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

                dtgDemandantes.Columns.Add(btnQuitar);
                dtgDemandantes.Columns["Quitar"].DisplayIndex = dtgDemandantes.ColumnCount - 1;
            }
        }
        private void CrearBotonQuitarTerceroInteresado()
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

        private void CrearBotonQuitarAbogadoDirector()
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

        private void CrearBotonQuitarContactoEmpresa()
        {
            if (!dtgContactoEmpresa.Columns.Contains("Quitar"))
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

                dtgContactoEmpresa.Columns.Add(btnQuitar);
                dtgContactoEmpresa.Columns["Quitar"].DisplayIndex = dtgContactoEmpresa.ColumnCount - 1;
            }
        }

        private void CrearBotonQuitarSocioResponsable()
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

        private void CrearBotonQuitarAbogadoAsistente()
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

        private async void Contencioso_General_PI_Load(object sender, EventArgs e)
        {
            VerificarTipoUsuario();
            if (!_yaCargo)
                await LoadAsync();
        }


        private void dtgCasosCiviles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgCasosContenciosos.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgCasosContenciosos.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await EjecutarConLoaderAsync(async () =>
                {
                    await CargarCasos();
                });
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await EjecutarConLoaderAsync(async () =>
                {
                    await CargarCasos();
                });
            }
        }

        private void dtgCasosCiviles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgCasosContenciosos.Columns["id"] != null)
            {
                dtgCasosContenciosos.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgCasosContenciosos.Columns["id_rol"] != null)
            {
                dtgCasosContenciosos.Columns["id_rol"].Visible = false;
            }

            CrearBotonesAccion(dtgCasosContenciosos);
            dtgCasosContenciosos.ClearSelection();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await EjecutarConLoaderAsync(async () =>
                {
                    await CargarCasos();
                });

            }
        }

        private async Task GuardarCaso()
        {
            if (string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                MessageBox.Show("Expediente es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtExpediente.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(comboBoxJuzgado.Text))
            {
                MessageBox.Show("Juzgado es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxJuzgado.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(comboboxOficial.Text))
            {
                MessageBox.Show("Oficial es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboboxOficial.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(comboboxNotificador.Text))
            {
                MessageBox.Show("Notificador es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboboxNotificador.Focus();
                return;
            }

            // Estado/Fecha: tu app depende de EstadoContencioso
            if (string.IsNullOrWhiteSpace(EstadoContencioso.estado) && string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                MessageBox.Show("Debe agregar un estado antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!EstadoContencioso.fechaEstado.HasValue)
            {
                MessageBox.Show("Debe agregar la fecha del estado antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            var req = new CrearCasoContenciosoRequest
            {
                Expediente = txtExpediente.Text,
                Juzgado = comboBoxJuzgado.Text,
                Oficial = comboboxOficial.Text,
                Notificador = comboboxNotificador.Text,
                NombreParticular = txtNombreParticular.Text,
                Estado = EstadoContencioso.estado ?? txtEstado.Text,
                Observaciones = EstadoContencioso.observaciones ?? txtObservaciones.Text,
                UsuarioCreador = UserSession.Id,
                Fecha = EstadoContencioso.fechaEstado.HasValue
                ? EstadoContencioso.fechaEstado.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : null,
                FechaVencimiento = EstadoContencioso.fechaVencimiento.HasValue
                ? EstadoContencioso.fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "",

                Demandantes = listaDemandantes.Select(x => x.id).ToList(),
                Demandados = listaDemandados.Select(x => x.id).ToList(),
                TercerosInteresados = listaTercerosInteresados.Select(x => x.id).ToList(),
                ContactosEmpresa = listaContactosEmpresa.Select(x => x.id).ToList(),

                AbogadosDirectores = listaAbogadosDirectores.Select(x => x.id).ToList(),
                SociosResponsables = listaSociosResponsables.Select(x => x.id).ToList(),
                AbogadosAsistentes = listaAbogadosAsistentes.Select(x => x.id).ToList(),
                MarcaReferenciaId = idMarcaReferencia,

                

            };

            var resultado = await casoContenciosoModel.CrearCasoContencioso(req);

            if (resultado.success)
            {
                MessageBox.Show("Caso contencioso administrativo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await EjecutarConLoaderAsync(async () =>
                {
                    await CargarCasos();
                });
                LimpiarFormulario();
                AnadirTabPage(Listar);
                EliminarTabPage(Detalles);
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void roundedButton18_Click(object sender, EventArgs e)
        {
            await GuardarCaso();
        }

        private async void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(Listar);
            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageMarcasReferencia);
            await EjecutarConLoaderAsync(async () =>
            {
                await CargarCasos();
            });
        }


        private async void dtgCasosCiviles_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            if (_cargandoCaso) return;

            if (dtgCasosContenciosos.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idCaso = Convert.ToInt32(dtgCasosContenciosos.Rows[e.RowIndex].Cells["id"].Value);
                string? expediente = Convert.ToString(dtgCasosContenciosos.Rows[e.RowIndex].Cells["expediente"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que desea eliminar el caso " + expediente + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    ApiResponse<object> resultado = null;

                    await EjecutarConLoaderAsync(async () =>
                    {
                        resultado = await casoContenciosoModel.EliminarCasoContencioso(idCaso, UserSession.Id);
                    });

                    if (resultado == null)
                    {
                        MessageBox.Show("No se obtuvo respuesta del servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (resultado.success)
                    {
                        MessageBox.Show("Caso contencioso eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await EjecutarConLoaderAsync(async () =>
                        {
                            await CargarCasos();
                        });
                    }
                    else
                    {
                        MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

            if (dtgCasosContenciosos.Columns[e.ColumnIndex].Name == "Terminar")
            {
                int idCaso = Convert.ToInt32(dtgCasosContenciosos.Rows[e.RowIndex].Cells["id"].Value);
                string? expediente = Convert.ToString(dtgCasosContenciosos.Rows[e.RowIndex].Cells["expediente"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que desea terminar el caso " + expediente + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    FrmAgregarEstadoCivilTerminado frmAgregarEstado = new FrmAgregarEstadoCivilTerminado();
                    frmAgregarEstado.ShowDialog();

                    if (EstadoContencioso.estado != null && EstadoContencioso.fechaEstado != null)
                    {
                        var response = await historialModel.TerminarCasoContencioso(
                            casoId: idCaso,
                            usuarioId: UserSession.Id,
                            fecha: EstadoContencioso.fechaEstado.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                            anotaciones: EstadoContencioso.observaciones,
                            origen: "ADMINISTRATIVO GENERAL PRIMER INSTANCIA"
                        );

                        if (response.success)
                        {
                            MessageBox.Show("Caso terminado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await EjecutarConLoaderAsync(async () =>
                            {
                                await CargarCasos();
                            });
                        }
                        else
                        {
                            MessageBox.Show(response.message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El caso no se mandó a terminado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }


                }
            }

            if (dtgCasosContenciosos.Columns[e.ColumnIndex].Name == "Editar")
            {
                try
                {
                    _cargandoCaso = true;
                    dtgCasosContenciosos.Enabled = false;

                    btnGuardarCaso.Text = "Actualizar";
                    lblTitulo.Text = "Editar Caso Contencioso";

                    int idCaso = Convert.ToInt32(dtgCasosContenciosos.Rows[e.RowIndex].Cells["id"].Value);
                    _idCasoEditar = idCaso;
                    _actualizandoCaso = true;
                    _huboCambioEstado = false;

                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosCaso(idCaso);
                    });
                }
                finally
                {
                    dtgCasosContenciosos.Enabled = true;
                    _cargandoCaso = false;
                }
            }
        }

        private void Detalles_Click(object sender, EventArgs e)
        {

        }

        private void dtgPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Contencioso_General_PI_Resize_1(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void AjustarFilasSegunEstado(string estado)
        {
            bool esSentencia = estado?.Trim()
                .Equals("Sentencia/Recurso de Casación", StringComparison.OrdinalIgnoreCase) == true;

            // 🔹 Fila 0 (la que se oculta/muestra)
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanel1.RowStyles[0].Height = esSentencia ? 160F : 0F;

            // 🔹 Filas fijas (NO se tocan)
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPanel1.RowStyles[1].Height = 310F;

            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute;
            tableLayoutPanel1.RowStyles[2].Height = 2000F;

            // 🔥 Ocultar controles de la fila 0
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (tableLayoutPanel1.GetRow(ctrl) == 0)
                {
                    ctrl.Visible = esSentencia;
                }
            }

            // 🔹 Tu lógica visual
            if (esSentencia)
            {
                if (_huboCambioEstado || !_actualizandoCaso)
                {
                    roundedButtonExpedienteCasacion.BackColor = Color.White;
                    txtExpedienteRecursoCasacion.Enabled = true;
                    comboBoxMotivoCasacion.Enabled=true;
                    txtExpedienteRecursoCasacion.BackColor = Color.White;
                }
                else
                {
                    var color = ColorTranslator.FromHtml("#f0f0f0");

                    roundedButtonExpedienteCasacion.BackColor = color;
                    txtExpedienteRecursoCasacion.Enabled = false;
                    comboBoxMotivoCasacion.Enabled = false;
                    txtExpedienteRecursoCasacion.BackColor = color;
                }
            }
        }
        private void btnAgregarEstado_Click(object sender, EventArgs e)
        {
            FrmAgregarEstadoContenciosoGPI frmAgregarEstado = new FrmAgregarEstadoContenciosoGPI();
            frmAgregarEstado.ShowDialog();

            if (EstadoContencioso.estado != null)
            {
                _huboCambioEstado = true;

                if(EstadoContencioso.estado =="Sentencia/Recurso de Casación")
                {
                    expedienteCasacion = frmAgregarEstado.expedienteC;
                    motivoCasacion = frmAgregarEstado.motivoC;
                    txtExpedienteRecursoCasacion.Text = expedienteCasacion;
                    switch (motivoCasacion)
                    {
                        case "FONDO": comboBoxMotivoCasacion.SelectedItem = "De fondo";break;
                        case "FORMA": comboBoxMotivoCasacion.SelectedItem = "De forma"; break;
                        case "FORMA Y FONDO": comboBoxMotivoCasacion.SelectedItem = "De forma y fondo"; break;
                    }
                    
                }
                else
                {
                    expedienteCasacion = null;
                    motivoCasacion = null;
                    txtExpedienteRecursoCasacion.Text = "";
                    comboBoxMotivoCasacion.SelectedIndex = -1;
                }

                AjustarFilasSegunEstado(EstadoContencioso.estado);

                txtEstado.Text = EstadoContencioso.estado.ToString();
                txtObservaciones.AppendText(Environment.NewLine + EstadoContencioso.observaciones);

                MessageBox.Show("Estado agregado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string texto = txtEstado.Text.Trim();
                if (!String.IsNullOrWhiteSpace(texto))
                {
                    txtEstado.Text = texto;
                }
                else
                {
                    txtEstado.Text = "";
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == Detalles)
            {
                Detalles.AutoScrollPosition = new Point(0, 0);

                if (_actualizandoCaso)
                {
                    btnVerArchivos.Visible = true;
                    btnVerHistorial.Visible = true;
                }
                else
                {
                    btnVerArchivos.Visible = false;
                    btnVerHistorial.Visible = false;
                }

                panelBotonesCaso.Visible = true;

            }
            else if (tabControl1.SelectedTab == Listar)
            {
                panelBotonesCaso.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPageHistorial)
            {
                panelBotonesCaso.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPageArchivos)
            {
                panelBotonesCaso.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPageMarcasReferencia)
            {
                tabPageMarcasReferencia.AutoScrollPosition = new Point(0, 0);
            }
        }

        private void btnAgregarDemandantes_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarDemandante(listaDemandantes);

            frm.Show();
        }

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

        //caso
        private void AjustarAlturaDataGridViewDemandados()
        {
            dtgDemandados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandados.ColumnHeadersHeight;

            dtgDemandados.Height = alturaFilas + alturaHeaders + 22;

            dtgDemandados.ScrollBars = ScrollBars.None;

            panelDemandados.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }
        private void AjustarAlturaDataGridViewDemandantes()
        {
            dtgDemandantes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandantes.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandantes.ColumnHeadersHeight;

            dtgDemandantes.Height = alturaFilas + alturaHeaders + 22;

            dtgDemandantes.ScrollBars = ScrollBars.None;

            dtgDemandantes.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }

        private void AjustarAlturaDataGridViewTercerosInteresados()
        {
            dtgTercerosInteresados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgTercerosInteresados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgTercerosInteresados.ColumnHeadersHeight;

            dtgTercerosInteresados.Height = alturaFilas + alturaHeaders + 22;

            dtgTercerosInteresados.ScrollBars = ScrollBars.None;

            dtgTercerosInteresados.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }

        private void AjustarAlturaDataGridViewContactosEmpresa()
        {
            dtgContactoEmpresa.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgContactoEmpresa.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgContactoEmpresa.ColumnHeadersHeight;

            dtgContactoEmpresa.Height = alturaFilas + alturaHeaders + 22;

            dtgContactoEmpresa.ScrollBars = ScrollBars.None;

            dtgContactoEmpresa.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }
        private void AjustarAlturaDataGridViewAbogadosDirectores()
        {
            dtgAbogadosDirectores.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosDirectores.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosDirectores.ColumnHeadersHeight;

            dtgAbogadosDirectores.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosDirectores.ScrollBars = ScrollBars.None;

            dtgAbogadosDirectores.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }

        private void AjustarAlturaDataGridViewSociosResponsables()
        {
            dtgSociosResponsables.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgSociosResponsables.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgSociosResponsables.ColumnHeadersHeight;

            dtgSociosResponsables.Height = alturaFilas + alturaHeaders + 22;

            dtgSociosResponsables.ScrollBars = ScrollBars.None;

            dtgSociosResponsables.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }

        private void AjustarAlturaDataGridViewAbogadosAsistentes()
        {
            dtgAbogadosAsistentes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosAsistentes.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosAsistentes.ColumnHeadersHeight;

            dtgAbogadosAsistentes.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosAsistentes.ScrollBars = ScrollBars.None;

            dtgAbogadosAsistentes.PerformLayout();
            flowLayoutPanel1.PerformLayout();

        }

        private void btnAgregarDemandados_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarDemandado(listaDemandados);

            frm.Show();

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
                    listaDemandados.Remove(item); // ✅ aquí se quita

                }
            }
        }
        private void LimpiarListas()
        {
            listaDemandados.Clear();
            listaDemandantes.Clear();
            listaTercerosInteresados.Clear();
            listaContactosEmpresa.Clear();

            listaAbogadosDirectores.Clear();
            listaSociosResponsables.Clear();
            listaAbogadosAsistentes.Clear();
        }

        

        private void dtgDemandados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgDemandados.Columns["id"] != null)
            {
                dtgDemandados.Columns["id"].Visible = false;
            }

            if (dtgDemandados.Columns["id_rol"] != null)
            {
                dtgDemandados.Columns["id_rol"].Visible = false;
            }

            dtgDemandados.ClearSelection();
        }

        private void dtgDemandantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgDemandantes.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgDemandantes.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
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

        private void dtgDemandantes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgDemandantes.Columns["id"] != null)
            {
                dtgDemandantes.Columns["id"].Visible = false;
            }

            if (dtgDemandantes.Columns["id_rol"] != null)
            {
                dtgDemandantes.Columns["id_rol"].Visible = false;
            }

            dtgDemandantes.ClearSelection();
        }

        private void btnAgregarPartesInteresadas_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarTerceroInteresado(listaTercerosInteresados);

            frm.Show();
        }

        private void dtgPartesInteresadas_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    listaTercerosInteresados.Remove(item); // ✅ aquí se quita

                }
            }
        }

        private void dtgPartesInteresadas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgTercerosInteresados.Columns["id"] != null)
            {
                dtgTercerosInteresados.Columns["id"].Visible = false;
            }

            if (dtgTercerosInteresados.Columns["id_rol"] != null)
            {
                dtgTercerosInteresados.Columns["id_rol"].Visible = false;
            }

            dtgTercerosInteresados.ClearSelection();
        }

        private void btnAgregarContactoEmpresa_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarContactoEmpresa(listaContactosEmpresa);

            frm.Show();
        }

        private void dtgContactoEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgContactoEmpresa.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgContactoEmpresa.Rows[e.RowIndex].DataBoundItem as PersonaListDataResponse;
                if (item == null) return;

                string nombre = item.Nombre ?? "este contacto de empresa";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de contactos de empresa?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaContactosEmpresa.Remove(item); // ✅ aquí se quita

                }
            }
        }

        private void dtgContactoEmpresa_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgContactoEmpresa.Columns["id"] != null)
            {
                dtgContactoEmpresa.Columns["id"].Visible = false;
            }

            if (dtgContactoEmpresa.Columns["id_rol"] != null)
            {
                dtgContactoEmpresa.Columns["id_rol"].Visible = false;
            }

            dtgContactoEmpresa.ClearSelection();
        }

        private void btnAgregarAbogadosDirectores_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarAbogadoDirector(listaAbogadosDirectores);
            frm.Show();
        }

        private void dtgAbogadosDirectores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgAbogadosDirectores.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgAbogadosDirectores.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Nombres + " " + item.Apellidos ?? "este abogado ";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de abogados directores?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaAbogadosDirectores.Remove(item); // ✅ aquí se quita

                }
            }
        }

        private void dtgAbogadosDirectores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgAbogadosDirectores.Columns["id"] != null)
            {
                dtgAbogadosDirectores.Columns["id"].Visible = false;
            }

            if (dtgAbogadosDirectores.Columns["id_rol"] != null)
            {
                dtgAbogadosDirectores.Columns["id_rol"].Visible = false;
            }

            dtgAbogadosDirectores.ClearSelection();
        }

        private void btnAgregarSociosResponsables_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarSocioResponsable(listaSociosResponsables);
            frm.Show();
        }

        private void dtgSociosResponsables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgSociosResponsables.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgSociosResponsables.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Nombres + " " + item.Apellidos ?? "este abogado ";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de abogados directores?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaSociosResponsables.Remove(item); // ✅ aquí se quita
                }
            }
        }

        private void dtgSociosResponsables_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgSociosResponsables.Columns["id"] != null)
            {
                dtgSociosResponsables.Columns["id"].Visible = false;
            }

            if (dtgSociosResponsables.Columns["id_rol"] != null)
            {
                dtgSociosResponsables.Columns["id_rol"].Visible = false;
            }

            dtgSociosResponsables.ClearSelection();
        }

        private void btnAgregarAbogadosAsistentes_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarAbogadoAsistente(listaAbogadosAsistentes);
            frm.Show();
        }

        private void dtgAbogadosAsistentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgAbogadosAsistentes.Columns[e.ColumnIndex].Name == "Quitar")
            {
                var item = dtgAbogadosAsistentes.Rows[e.RowIndex].DataBoundItem as UserListDataResponse;
                if (item == null) return;

                string nombre = item.Nombres + " " + item.Apellidos ?? "este abogado ";

                var confirm = MessageBox.Show(
                    $"¿Desea quitar a {nombre} de la lista de abogados asistentes?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    listaAbogadosAsistentes.Remove(item); // ✅ aquí se quita
                }
            }
        }

        private void dtgAbogadosAsistentes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAbogadosAsistentes.Columns["id"] != null)
            {
                dtgAbogadosAsistentes.Columns["id"].Visible = false;
            }

            if (dtgAbogadosAsistentes.Columns["id_rol"] != null)
            {
                dtgAbogadosAsistentes.Columns["id_rol"].Visible = false;
            }

            dtgAbogadosAsistentes.ClearSelection();
        }

        private void Contencioso_General_PI_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void Detalles_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));

        }


        private async void btnEditarCaso_Click(object sender, EventArgs e)
        {
            //aqui actualizo los datos del caso 
            bool cambioEstado = false;

            if (_idCasoEditar <= 0)
            {
                MessageBox.Show("No hay caso seleccionado para editar.");
                return;
            }

            var confirm = MessageBox.Show(
                "¿Desea guardar los cambios del caso?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            if (_huboCambioEstado && _actualizandoCaso)
            {
                cambioEstado = true;
            }
            else
            {
                cambioEstado = false;
            }

            var req = new EditarCasoContenciosoRequest
            {
                UsuarioId = UserSession.Id,
                CasoId = _idCasoEditar,

                Expediente = txtExpediente.Text,
                Juzgado = comboBoxJuzgado.Text,
                Oficial = comboboxOficial.Text,
                Notificador = comboboxNotificador.Text,
                NombreParticular = txtNombreParticular.Text,


                // historial (tomas lo último elegido en tu modal de estado)
                HuboCambioEstado = cambioEstado,
                Estado = EstadoContencioso.estado ?? txtEstado.Text,
                Observaciones = EstadoContencioso.observaciones ?? txtObservaciones.Text,

                Fecha = (EstadoContencioso.fechaEstado ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                FechaVencimiento = EstadoContencioso.fechaVencimiento.HasValue
                        ? EstadoContencioso.fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : "",

                Demandantes = listaDemandantes.Select(x => x.id).ToList(),
                Demandados = listaDemandados.Select(x => x.id).ToList(),
                TercerosInteresados = listaTercerosInteresados.Select(x => x.id).ToList(),
                ContactosEmpresa = listaContactosEmpresa.Select(x => x.id).ToList(),

                AbogadosDirectores = listaAbogadosDirectores.Select(x => x.id).ToList(),
                SociosResponsables = listaSociosResponsables.Select(x => x.id).ToList(),
                AbogadosAsistentes = listaAbogadosAsistentes.Select(x => x.id).ToList(),

                MarcaReferenciaId = idMarcaReferencia
            };

            var req2= new CrearCasoContenciosoRequest();
            switch (comboBoxMotivoCasacion.SelectedItem)
            {
                case "De fondo": motivoCasacion = "FONDO"; break;
                case "De forma": motivoCasacion = "FORMA"; break;
                case "De forma y fondo": motivoCasacion = "FORMA Y FONDO";break;
            }

            if(EstadoContencioso.estado =="Sentencia/Recurso de Casación")
            {
                req2 = new CrearCasoContenciosoRequest
                {
                    UsuarioCreador = UserSession.Id,
                    CasoOrigenId = _idCasoEditar,
                    MotivoCasacion = motivoCasacion,

                    Expediente = txtExpedienteRecursoCasacion.Text.Trim(),
                    Juzgado = "Cámara Civil de la Corte Suprema de Justicia",
                    Oficial = comboboxOficial.Text,
                    Notificador = comboboxNotificador.Text,
                    NombreParticular = txtNombreParticular.Text,

                    Estado = "Recurso de Casación",
                    Observaciones = EstadoContencioso.observaciones ?? txtObservaciones.Text,
                    Fecha = (EstadoContencioso.fechaEstado ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                    FechaVencimiento = EstadoContencioso.fechaVencimiento.HasValue
                        ? EstadoContencioso.fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : "",
                };
            }
            

            ApiResponseEditarCasoContencioso resultado = null;
            ApiResponseCrearCasoContencioso resultado2 = null;

            await EjecutarConLoaderAsync(async () =>
            {
                resultado = await casoContenciosoModel.EditarCasoContencioso(req);
                if (EstadoContencioso.estado == "Sentencia/Recurso de Casación")
                {
                    resultado2 = await recursoCasacionModel.CrearRecursoCasacion(req2);
                }
                
            });


            if (EstadoContencioso.estado == "Sentencia/Recurso de Casación")
            {
                if (resultado == null || resultado2 == null)
                {
                    MessageBox.Show("No se obtuvo respuesta del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (resultado.success && resultado2.success)
                {
                    MessageBox.Show("Caso contencioso actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarCasos();
                    });
                    LimpiarFormulario();
                    _idCasoEditar = 0;

                    AnadirTabPage(Listar);
                    EliminarTabPage(Detalles);
                    EliminarTabPage(tabPageMarcasReferencia);
                    _actualizandoCaso = false;
                }
                else
                {
                    MessageBox.Show("Error: " + (resultado2?.message ?? resultado.message, MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
            }
            else
            {
                if (resultado == null)
                {
                    MessageBox.Show("No se obtuvo respuesta del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (resultado.success)
                {
                    MessageBox.Show("Caso contencioso actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarCasos();
                    });
                    LimpiarFormulario();
                    _idCasoEditar = 0;

                    AnadirTabPage(Listar);
                    EliminarTabPage(Detalles);
                    EliminarTabPage(tabPageMarcasReferencia);
                    _actualizandoCaso = false;
                }
                else
                {
                    MessageBox.Show("Error: " + resultado.message);
                }
            }
            
        }

        private void roundedButton24_Click(object sender, EventArgs e)
        {

        }
        private async Task ListarHistorial()
        {

            var datosHistorial = await historialModel.ObtenerHistorialCasoContencioso(_idCasoEditar);

            if (!datosHistorial.success || datosHistorial.data == null)
                return;


            dtgHistorial.DataSource = datosHistorial.data;

            // Cambiar encabezados
            dtgHistorial.Columns["fecha"].HeaderText = "Fecha";
            dtgHistorial.Columns["fecha_vencimiento"].HeaderText = "Fecha Vencimiento";
            dtgHistorial.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dtgHistorial.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dtgHistorial.Columns["estado"].HeaderText = "Estado";
            dtgHistorial.Columns["origen"].HeaderText = "Origen";
            dtgHistorial.Columns["anotaciones"].HeaderText = "Anotaciones";
            dtgHistorial.Columns["usuario_creador"].HeaderText = "Usuario Creador";
            dtgHistorial.Columns["usuario_editor"].HeaderText = "Usuario Editor";
        }

        private async void btnVerHistorial_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorial);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageMarcasReferencia);
            await EjecutarConLoaderAsync(async () =>
            {
                await ListarHistorial();
            });
        }

        private async Task ListarArchivosCaso()
        {
            var res = await archivoModel.ListarArchivosCasoContencioso(_idCasoEditar);

            if (!res.success)
            {
                MessageBox.Show(res.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtgArchivos.DataSource = null;
                return;
            }
            else
            {
                dtgArchivos.DataSource = res.data;

                dtgArchivos.Columns["nombre"].HeaderText = "Nombre";
                dtgArchivos.Columns["tamano_bytes"].HeaderText = "Tamaño";
                dtgArchivos.Columns["fecha"].HeaderText = "Fecha";
                dtgArchivos.Columns["archivo_id"].Visible = false;

                CrearBotonesAccionArchivos(dtgArchivos);
            }
        }

        private void CrearBotonesAccionArchivos(DataGridView dtg)
        {
            dtg.AutoGenerateColumns = true;

            // Abrir
            if (!dtg.Columns.Contains("Abrir"))
            {
                var btnAbrir = new DataGridViewButtonColumn
                {
                    Name = "Abrir",
                    HeaderText = "",
                    Text = "👁️", // ver/abrir
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 45,
                    MinimumWidth = 45,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnAbrir);
            }

            // Descargar
            if (!dtg.Columns.Contains("Descargar"))
            {
                var btnDescargar = new DataGridViewButtonColumn
                {
                    Name = "Descargar",
                    HeaderText = "",
                    Text = "⬇️",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 45,
                    MinimumWidth = 45,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnDescargar);
            }

            // Eliminar
            if (!dtg.Columns.Contains("Eliminar"))
            {
                var btnEliminar = new DataGridViewButtonColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Text = "🗑️",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 45,
                    MinimumWidth = 45,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEliminar);
            }

            // mover al final (en orden)
            dtg.Columns["Abrir"].DisplayIndex = dtg.ColumnCount - 3;
            dtg.Columns["Descargar"].DisplayIndex = dtg.ColumnCount - 2;
            dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 1;
        }

        private async void btnVerArchivos_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageArchivos);
            EliminarTabPage(tabPageHistorial);
            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageMarcasReferencia);
            await EjecutarConLoaderAsync(async () =>
            {
                await ListarArchivosCaso();
            });
        }

        private void roundedButton19_Click_1(object sender, EventArgs e)
        {
            AnadirTabPage(Detalles);
            EliminarTabPage(tabPageHistorial);
        }

        private void btnRegresarDetalleDeArchivos_Click(object sender, EventArgs e)
        {
            AnadirTabPage(Detalles);
            EliminarTabPage(tabPageArchivos);
        }

        private void dtgHistorial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dtgHistorial.Columns["id"] != null)
            {
                dtgHistorial.Columns["id"].Visible = false;
            }

            if (dtgHistorial.Columns["usuario_creador_id"] != null)
            {
                dtgHistorial.Columns["usuario_creador_id"].Visible = false;
            }

            if (dtgHistorial.Columns["usuario_editor_id"] != null)
            {
                dtgHistorial.Columns["usuario_editor_id"].Visible = false;
            }

            if (dtgHistorial.Columns["caso_id"] != null)
            {
                dtgHistorial.Columns["caso_id"].Visible = false;
            }

            CrearBotonesAccionHistorial(dtgHistorial);
            dtgHistorial.ClearSelection();
        }
        private async Task RefrescarListaArchivos()
        {
            var resp = await archivoModel.ListarArchivosCasoContencioso(_idCasoEditar);

            if (!resp.success)
            {
                MessageBox.Show(resp.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtgArchivos.DataSource = null;
                return;
            }

            dtgArchivos.DataSource = resp.data;
            dtgArchivos.Columns["nombre"].HeaderText = "Nombre";
            dtgArchivos.Columns["tamano_bytes"].HeaderText = "Tamaño";
            dtgArchivos.Columns["fecha"].HeaderText = "Fecha";
            dtgArchivos.Columns["archivo_id"].Visible = false;
            CrearBotonesAccionArchivos(dtgArchivos);
        }

        private async void dtgArchivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_procesandoArchivo) return;

            var grid = (DataGridView)sender;
            var colName = grid.Columns[e.ColumnIndex].Name;

            var archivoId = Convert.ToString(grid.Rows[e.RowIndex].Cells["archivo_id"].Value);
            var nombre = Convert.ToString(grid.Rows[e.RowIndex].Cells["nombre"].Value);

            if (string.IsNullOrWhiteSpace(archivoId)) return;

            try
            {
                _procesandoArchivo = true;
                dtgArchivos.Enabled = false;
                btnSubirArchivo.Enabled = false;

                // ABRIR (descarga a TEMP y abre)
                if (colName == "Abrir")
                {
                    var tempFile = Path.Combine(Path.GetTempPath(), nombre ?? "archivo");

                    ApiResponse<string> resp = null;

                    await EjecutarConLoaderAsync(async () =>
                    {
                        resp = await archivoModel.DescargarArchivoCasoContencioso(_idCasoEditar, archivoId, tempFile);
                    });

                    if (resp == null)
                    {
                        MessageBox.Show("No se obtuvo respuesta del servidor.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!resp.success)
                    {
                        MessageBox.Show(resp.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Process.Start(new ProcessStartInfo(resp.data) { UseShellExecute = true });
                }

                // DESCARGAR (elige destino y guarda)
                else if (colName == "Descargar")
                {
                    using var sfd = new SaveFileDialog
                    {
                        FileName = nombre ?? "archivo",
                        Filter = "Todos los archivos (*.*)|*.*"
                    };

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ApiResponse<string> resp = null;

                        await EjecutarConLoaderAsync(async () =>
                        {
                            resp = await archivoModel.DescargarArchivoCasoContencioso(_idCasoEditar, archivoId, sfd.FileName);
                        });

                        if (resp == null)
                        {
                            MessageBox.Show("No se obtuvo respuesta del servidor.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!resp.success)
                        {
                            MessageBox.Show(resp.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        MessageBox.Show("Archivo descargado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // ELIMINAR (confirmación)
                else if (colName == "Eliminar")
                {
                    var r = MessageBox.Show($"¿Eliminar el archivo?\n\n{nombre}", "Confirmar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (r != DialogResult.Yes)
                        return;

                    if (r == DialogResult.Yes)
                    {
                        ApiResponse<object> resp = null;

                        await EjecutarConLoaderAsync(async () =>
                        {
                            resp = await archivoModel.EliminarArchivoCasoContencioso(_idCasoEditar, archivoId);
                        });

                        if (resp == null)
                        {
                            MessageBox.Show("No se obtuvo respuesta del servidor.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!resp.success)
                        {
                            MessageBox.Show(resp.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        MessageBox.Show("Archivo eliminado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefrescarListaArchivos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dtgArchivos.Enabled = true;
                btnSubirArchivo.Enabled = true;
                _procesandoArchivo = false;
            }
        }

        private async void btnSubirArchivo_Click(object sender, EventArgs e)
        {
            if (_idCasoEditar <= 0)
            {
                MessageBox.Show("Debe seleccionar un caso válido.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var ofd = new OpenFileDialog
            {
                Title = "Seleccionar archivos",
                Filter = "Archivos permitidos|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.png;*.jpg;*.jpeg;*.zip;*.rar;*.txt",
                Multiselect = true
            };

            if (ofd.ShowDialog() != DialogResult.OK || ofd.FileNames.Length == 0)
                return;

            ApiResponse<List<SubirArchivoCasoContenciosoData>> response = null;

            try
            {
                btnSubirArchivo.Enabled = false;
                btnSubirArchivo.Text = "Subiendo...";

                await EjecutarConLoaderAsync(async () =>
                {
                    response = await archivoModel.SubirArchivosCasoContencioso(_idCasoEditar, ofd.FileNames.ToList());
                });

                if (response == null)
                {
                    MessageBox.Show("No se obtuvo respuesta del servidor.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!response.success)
                {
                    MessageBox.Show(response.message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Se subieron {response.data?.Count ?? 0} archivo(s) correctamente.",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await RefrescarListaArchivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubirArchivo.Enabled = true;
                btnSubirArchivo.Text = "Subir archivo";
            }
        }

        private void btnCancelarEdicionHistorial_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorial);
            EliminarTabPage(tabPageEditarHistorial);
        }

        private async void dtgHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender;
            var colName = grid.Columns[e.ColumnIndex].Name;
            var item = grid.Rows[e.RowIndex].DataBoundItem as HistorialCasoContenciosoDetalle;
            if (item == null) return;

            // EDITAR
            if (colName == "Editar")
            {

                CargarDatosHistorialEnTab(item);

                AnadirTabPage(tabPageEditarHistorial);
                EliminarTabPage(tabPageHistorial);

                return;
            }

            // ELIMINAR
            if (colName == "Eliminar")
            {
                var confirm = MessageBox.Show(
                    $"¿Desea eliminar este registro de historial?\n\nEstado: {item.estado}",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm != DialogResult.Yes)
                    return;

                ApiResponse<object> resp = null;

                await EjecutarConLoaderAsync(async () =>
                {
                    resp = await historialModel.EliminarHistorialCasoContencioso(
                        item.id,
                        item.caso_id,
                        UserSession.Id
                    );
                });

                if (resp == null)
                {
                    MessageBox.Show("No se obtuvo respuesta del servidor.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!resp.success)
                {
                    MessageBox.Show(resp.message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Historial eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await EjecutarConLoaderAsync(async () =>
                {
                    await ListarHistorial();
                    await CargarDatosCaso(_idCasoEditar);
                });
            }
        }

        private void CargarDatosHistorialEnTab(HistorialCasoContenciosoDetalle item)
        {
            _idHistorialEditar = item.id;
            _casoIdHistorialEditar = item.caso_id;

            dateTimePickerFechaEstado.Value = item.fecha;

            comboboxEstado.Text = item.estado ?? "";
            string origen = item.origen;
            bool requiereVencimiento = EstadoContenciosoHelper.RequiereVencimiento(item.estado ?? "", origen);
            bool tieneVencimiento = item.fecha_vencimiento.HasValue || requiereVencimiento;

            checkBoxTieneVencimiento.Checked = tieneVencimiento;

            dateTimePickerFechaVencimiento.Enabled = tieneVencimiento;
            dateTimePickerHoraVencimiento.Enabled = tieneVencimiento;

            if (item.fecha_vencimiento.HasValue)
            {
                dateTimePickerFechaVencimiento.Value = item.fecha_vencimiento.Value.Date;
                dateTimePickerHoraVencimiento.Value = DateTime.Today.Date + item.fecha_vencimiento.Value.TimeOfDay;
            }
            else
            {
                dateTimePickerFechaVencimiento.Value = DateTime.Today;
                dateTimePickerHoraVencimiento.Value = DateTime.Today.Date.AddHours(8);
            }

            txtObservacionesHistorial.Text = item.anotaciones ?? "";
            txtOrigenHistorial.Text = item.origen ?? "";
            txtUsuarioCreadorHistorial.Text = item.usuario_creador ?? "";
            txtUsuarioEditorHistorial.Text = item.usuario_editor ?? "";
        }

        private async void btnGuardarEdicionHistorial_Click(object sender, EventArgs e)
        {
            if (_idHistorialEditar <= 0)
            {
                MessageBox.Show("No hay historial seleccionado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(comboboxEstado.Text))
            {
                MessageBox.Show("Estado es requerido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboboxEstado.Focus();
                return;
            }

            DateTime fechaEstado = dateTimePickerFechaEstado.Value.Date;

            DateTime? fechaVencimiento = null;
            if (checkBoxTieneVencimiento.Checked)
            {
                fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
            }

            var req = new EditarHistorialCasoContenciosoRequest
            {
                HistorialId = _idHistorialEditar,
                CasoId = _casoIdHistorialEditar,
                UsuarioId = UserSession.Id,
                Fecha = fechaEstado.ToString("yyyy-MM-dd HH:mm:ss"),
                FechaVencimiento = fechaVencimiento.HasValue
                    ? fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                    : "",
                Estado = comboboxEstado.Text.Trim(),
                Anotaciones = txtObservacionesHistorial.Text.Trim()
            };

            ApiResponse<object> resp = null;

            await EjecutarConLoaderAsync(async () =>
            {
                resp = await historialModel.EditarHistorialCaso(req);
            });

            if (resp == null)
            {
                MessageBox.Show("No se obtuvo respuesta del servidor.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!resp.success)
            {
                MessageBox.Show(resp.message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Historial actualizado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            await EjecutarConLoaderAsync(async () =>
            {
                await ListarHistorial();
                await CargarDatosCaso(_idCasoEditar);
            });

            AnadirTabPage(tabPageHistorial);
            EliminarTabPage(tabPageEditarHistorial);
        }

        private void VerificarEstadoEditarHistorial()
        {

            string origenActual = txtOrigenHistorial.Text?.Trim() ?? "";

            bool requiereVencimiento = EstadoContenciosoHelper.RequiereVencimiento(
                comboboxEstado.Text,
                origenActual
            );

            if (requiereVencimiento)
            {
                checkBoxTieneVencimiento.Checked = true;
            }

            dateTimePickerFechaVencimiento.Enabled = checkBoxTieneVencimiento.Checked;
            dateTimePickerHoraVencimiento.Enabled = checkBoxTieneVencimiento.Checked;
        }

        private void ActualizarObservacionEditarHistorial()
        {
            if (string.IsNullOrWhiteSpace(comboboxEstado.Text))
                return;

            DateTime fechaEstado = dateTimePickerFechaEstado.Value.Date;

            DateTime? fechaVencimiento = null;
            if (checkBoxTieneVencimiento.Checked)
            {
                fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
            }

            txtObservacionesHistorial.Text = EstadoContenciosoHelper.GenerarObservacion(
                fechaEstado,
                comboboxEstado.Text,
                checkBoxTieneVencimiento.Checked,
                fechaVencimiento
            );
        }

        private void comboboxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            VerificarEstadoEditarHistorial();
            ActualizarObservacionEditarHistorial();
        }

        private void checkBoxTieneVencimiento_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerFechaVencimiento.Enabled = checkBoxTieneVencimiento.Checked;
            dateTimePickerHoraVencimiento.Enabled = checkBoxTieneVencimiento.Checked;
            ActualizarObservacionEditarHistorial();
        }

        private void dateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservacionEditarHistorial();
        }

        private void dateTimePickerFechaEstado_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservacionEditarHistorial();
        }

        private void dateTimePickerHoraVencimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservacionEditarHistorial();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnCasoReferencia_Click(object sender, EventArgs e)
        {
            LimpiarFormularioMR();
            if (idMarcaReferencia != null && idMarcaReferencia != 0)
            {
                int idMarcaR = idMarcaReferencia ?? 0;
                if(idMarcaR!= 0)
                {
                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosMarcaReferencia(idMarcaR);

                    });
                }
            }
           
            AnadirTabPage(tabPageMarcasReferencia);
            EliminarTabPage(Detalles);
            EliminarTabPage(Listar);
            EliminarTabPage(tabPageEditarHistorial);
            EliminarTabPage(tabPageEditarHistorial);

        }

        private async void btnAgregarCasoReferencia_Click(object sender, EventArgs e)
        {
            FrmAgregarMarcaReferencia frmAgregarMarcaReferencia = new FrmAgregarMarcaReferencia();

            if (frmAgregarMarcaReferencia.ShowDialog() == DialogResult.OK)
            {
                int? idMarca = frmAgregarMarcaReferencia.IdMarcaSeleccionada;
                int idMarcaR = idMarca ?? 0;
                if (idMarca != null)
                {
                    
                    txtCasoReferenciaId.Text = idMarca.ToString();
                    
                    idMarcaReferencia = idMarca;
                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosMarcaReferencia(idMarcaR);
                    });
                }
                else
                {
                    MessageBox.Show("No fue posible cargar los datos de la marca de referencia ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void roundedButton19_Click_2(object sender, EventArgs e)
        {
            AnadirTabPage(Detalles);
            EliminarTabPage(tabPageMarcasReferencia);
        }

        private void tabPageCasoReferencia_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
        }

        private void roundedButton57_Click(object sender, EventArgs e)
        {

        }
        private void btnEliminarCasoReferencia_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar la marca de referencia?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                idMarcaReferencia = null;
                LimpiarFormularioMR();
            }
        }

    }
}
