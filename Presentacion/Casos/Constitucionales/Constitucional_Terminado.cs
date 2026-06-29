using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Constitucionales;
using Comun.Models.Casos.Contenciosos;
using Comun.Models.Casos.Laborales;
using Dominio.Entidades;
using Dominio.Entidades.Constitucionales;
using Presentacion.Casos.Abogados_asignados;
using Presentacion.Casos.Constitucionales.Agregar_caso;
using Presentacion.Casos.Constitucionales.Estados_constitucionales;
using Presentacion.Casos.Contenciosos.Estados_contenciosos;
using Presentacion.Casos.Participantes;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Presentacion.Casos.Constitucionales.Constitucional_Terminado
{
    public partial class Constitucional_Terminado : Form, IAsyncLoadable
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

        CasoConstitucionalTerminadoModel casoConstitucionalModel = new CasoConstitucionalTerminadoModel();
        HistorialCasoConstitucionalModel historialModel = new HistorialCasoConstitucionalModel();
        ArchivosCasosConstitucionalesModel archivoModel = new ArchivosCasosConstitucionalesModel();
        
        //caso referencia
        private BindingList<PersonaListDataResponse> listaDemandadosCasoReferencia
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaDemandantesCasoReferencia
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaTercerosInteresadosCasoReferencia
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaContactosEmpresaCasoReferencia
        = new BindingList<PersonaListDataResponse>();
        //abogados en el caso
        private BindingList<UserListDataResponse> listaAbogadosDirectoresCasoReferencia
        = new BindingList<UserListDataResponse>();
        private BindingList<UserListDataResponse> listaSociosResponsablesCasoReferencia
        = new BindingList<UserListDataResponse>();
        private BindingList<UserListDataResponse> listaAbogadosAsistentesCasoReferencia
        = new BindingList<UserListDataResponse>();

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
        private int? idCasoReferencia = null;
        private bool isAdminConstitucional = false;
        private bool isLectorConstitucional = false;
        UserModel userModel = new UserModel();

        private async Task VerificarTipoUsuario()
        {
            var resp = await userModel.ObtenerPermisoPorModulo(UserSession.Id, 3);
            if (resp.success && resp.data != null)
            {
                string rol = resp.data.nombre_rol;

                if (rol == "Administrador")
                {
                    isAdminConstitucional = true;
                    isLectorConstitucional = false;
                }
                else if (rol == "Usuario Lector")
                {
                    isLectorConstitucional = true;
                    isAdminConstitucional = false;
                }
                else if (rol == "Usuario Normal")
                {
                    isLectorConstitucional = false;
                    isAdminConstitucional = false;
                }
            }
            else
            {
                MessageBox.Show(resp.message);
            }
        }

        public async Task LoadAsync()
        {
            if (_yaCargo) return;
            _yaCargo = true;

            panelBotonesCaso.Visible = false;
            dtgCasosCiviles.DataSource = bsTercerosInteresados;

            await CargarCasos();

            dtgCasosCiviles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            if (dtgCasosCiviles.Columns.Contains("Editar"))
                dtgCasosCiviles.Columns["Editar"].Width = 40;

            if (dtgCasosCiviles.Columns.Contains("Eliminar"))
                dtgCasosCiviles.Columns["Eliminar"].Width = 40;

            if (dtgCasosCiviles.Columns.Contains("Terminar"))
                dtgCasosCiviles.Columns["Terminar"].Width = 40;

            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(tabPageHistorial);
            EliminarTabPage(tabPageEditarHistorial);
            EliminarTabPage(tabPageCasoReferencia);

            //caso
            alistarListaDemandantes();
            alistarListaDemandados();
            alistarListaTercerosInteresados();
            alistarListaContactosEmpresa();
            alistarListaAbogadosDirectores();
            alistarListaSociosResponsables();
            alistarListaAbogadosAsistentes();

            //caso referencia
            alistarListaDemandantesCR();
            alistarListaDemandadosCR();
            alistarListaTercerosInteresadosCR();
            alistarListaContactosEmpresaCR();
            alistarListaAbogadosDirectoresCR();
            alistarListaSociosResponsablesCR();
            alistarListaAbogadosAsistentesCR();

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


            //caso referencia
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.WrapContents = false;
            flowLayoutPanel2.AutoScroll = true;

            panelDemandadosCR.AutoSize = true;
            panelDemandadosCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelDemandantesCR.AutoSize = true;
            panelDemandantesCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelTercerosInteresadosCR.AutoSize = true;
            panelTercerosInteresadosCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelContactosEmpresaCR.AutoSize = true;
            panelContactosEmpresaCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelAbogadosDirectoresCR.AutoSize = true;
            panelAbogadosDirectoresCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelSociosResponsablesCR.AutoSize = true;
            panelSociosResponsablesCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelAbogdadosAsistentesCR.AutoSize = true;
            panelAbogdadosAsistentesCR.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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

        public Constitucional_Terminado()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtCausaAgravioHechos.Text = "";
            txtExpediente.Text = "";
            comboBoxCorte.Text = "";
            comboboxOficial.Text = "";
            txtNombreParticular.Text = "";
            comboboxNotificador.Text = "";
            txtNombreParticular.Text = "";
            txtEstado.Text = "";
            txtObservaciones.Text = "";
            LimpiarListas();
        }

        private void LimpiarFormularioCR()
        {
            txtExpedienteCasoReferencia.Text = "";
            txtNombreParticularCasoReferencia.Text = "";

            txtEstadoCasoReferencia.Text = "";
            textBoxObervacionesCasoReferencia.Text = "";

            txtExpedienteRecursoCasacionCR.Text = "";
            txtboxNombreParticularCRRC.Text = "";
            txtExpedienteReferenciaCRRC.Text = "";

            comboBoxNotificadorCasoReferencia.SelectedIndex = -1;
            comboBoxOficialCasoReferencia.SelectedIndex = -1;
            comboBoxJuzgadoCasoReferencia.SelectedIndex = -1;
            comboBoxCamara.SelectedIndex = -1;
            comboBoxMotivoCasacion.SelectedIndex = -1;
            comboBoxMotivoCasacionCR.SelectedIndex = -1;


            LimpiarListasCasoReferencia();
        }

        private async Task BotonesAdmin()
        {
            await VerificarTipoUsuario();
            if (isAdminConstitucional)
            {
                //editar caso
                btnEditarCaso.Visible = true;
                btnEditarCaso.Enabled = true;

                //agregar estado
                btnAgregarEstado.Visible = true;
                btnAgregarEstado.Enabled = true;

                //agregar participantes
                btnAgregarDemandantes.Visible = true;
                btnAgregarDemandantes.Enabled = true;

                btnAgregarDemandados.Visible = true;
                btnAgregarDemandados.Enabled = true;

                btnAgregarPartesInteresadas.Visible = true;
                btnAgregarPartesInteresadas.Enabled = true;

                btnAgregarContactoEmpresa.Visible = true;
                btnAgregarContactoEmpresa.Enabled = true;

                //agregar equipo legal
                btnAgregarAbogadosAsistentes.Visible = true;
                btnAgregarAbogadosAsistentes.Enabled = true;

                btnAgregarAbogadosDirectores.Visible = true;
                btnAgregarAbogadosDirectores.Enabled = true;

                btnAgregarSociosResponsables.Visible = true;
                btnAgregarSociosResponsables.Enabled = true;

                btnAgregarCasoReferencia.Visible = true;
                btnAgregarCasoReferencia.Enabled = true;

                btnEliminarCasoReferencia.Visible = true;
                btnEliminarCasoReferencia.Enabled = true;
            }
            else
            {
                //editar caso
                btnEditarCaso.Visible = false;
                btnEditarCaso.Enabled = false;

                //agregar estado
                btnAgregarEstado.Visible = false;
                btnAgregarEstado.Enabled = false;

                //agregar participantes
                btnAgregarDemandantes.Visible = false;
                btnAgregarDemandantes.Enabled = false;

                btnAgregarDemandados.Visible = false;
                btnAgregarDemandados.Enabled = false;

                btnAgregarPartesInteresadas.Visible = false;
                btnAgregarPartesInteresadas.Enabled = false;

                btnAgregarContactoEmpresa.Visible = false;
                btnAgregarContactoEmpresa.Enabled = false;

                //agregar equipo legal
                btnAgregarAbogadosAsistentes.Visible = false;
                btnAgregarAbogadosAsistentes.Enabled = false;

                btnAgregarAbogadosDirectores.Visible = false;
                btnAgregarAbogadosDirectores.Enabled = false;

                btnAgregarSociosResponsables.Visible = false;
                btnAgregarSociosResponsables.Enabled = false;

                btnAgregarCasoReferencia.Visible = false;
                btnAgregarCasoReferencia.Enabled = false;

                btnEliminarCasoReferencia.Visible = false;
                btnEliminarCasoReferencia.Enabled = false;
            }
        }
        void HabilitarBotonesCaso()
        {
            txtExpediente.Enabled = isAdminConstitucional;
            txtNombreParticular.Enabled = isAdminConstitucional;
            comboboxNotificador.Enabled = isAdminConstitucional;
            comboBoxCamara.Enabled = isAdminConstitucional;
            comboBoxCorte.Enabled = isAdminConstitucional;
            comboboxOficial.Enabled = isAdminConstitucional;
            btnAgregarDemandados.Enabled = isAdminConstitucional;
            btnAgregarDemandantes.Enabled = isAdminConstitucional;
            btnAgregarPartesInteresadas.Enabled = isAdminConstitucional;
            btnAgregarContactoEmpresa.Enabled = isAdminConstitucional;
            btnAgregarAbogadosAsistentes.Enabled = isAdminConstitucional;
            btnAgregarAbogadosDirectores.Enabled = isAdminConstitucional;
            btnAgregarSociosResponsables.Enabled = isAdminConstitucional;
            btnAgregarEstado.Enabled = isAdminConstitucional;
            txtCausaAgravioHechos.Enabled= isAdminConstitucional;
            btnAgregarCasoReferencia.Enabled = isAdminConstitucional;
            btnEliminarCasoReferencia.Enabled = isAdminConstitucional;
        }

        private async Task CargarDatosCaso(int idCaso)
        {
            LimpiarListas();
            int idUsuario = UserSession.Id;
            var resp = await casoConstitucionalModel.ObtenerCasoConstitucionalPorId(idUsuario, idCaso);

            if (!resp.success || resp.data == null)
            {
                MessageBox.Show(resp.message ?? "No se pudo cargar el caso");
                return;
            }

            var data = resp.data;

            if (data != null)
            {
                txtExpediente.Text = data.caso?.expediente ?? "";
                comboboxOficial.Text = data.caso?.oficial ?? "";
                txtNombreParticular.Text = data.caso?.nombre_particular ?? "";
                txtCausaAgravioHechos.Text= data.caso?.causa ?? "";
                comboBoxCorte.SelectedItem = data.caso?.corte ?? "";
                txtEstado.Text = data.caso?.estado ?? "";
                txtObservaciones.Text = (data.caso?.observaciones ?? "")
                    .Replace("\n", Environment.NewLine); ;
            }


            // 3) Personas por rol -> tus BindingList<PersonaListDataResponse>
            var p = data?.personas_por_rol ?? new Dictionary<string, List<PersonaMiniDto>>();

            MapPersonas(p, "Solicitante", listaDemandantes);
            MapPersonas(p, "Autoridad Impugnada", listaDemandados);
            MapPersonas(p, "Tercero Interesado", listaTercerosInteresados);
            MapPersonas(p, "Contacto de Empresa", listaContactosEmpresa);

            // 4) Usuarios por rol -> tus BindingList<UserListDataResponse>
            var u = data?.usuarios_por_rol ?? new Dictionary<string, List<UsuarioMiniDto>>();

            MapUsuarios(u, "Abogado Director", listaAbogadosDirectores);
            MapUsuarios(u, "Socio Responsable", listaSociosResponsables);
            MapUsuarios(u, "Abogado Asistente", listaAbogadosAsistentes);

            var casoReferencia = data?.referencia_amparo ?? null;
            if (casoReferencia != null)
            {
                idCasoReferencia = casoReferencia.caso_referencia_id;
            }
            else
            {
                idCasoReferencia = 0;
            }

            // 5) refrescar grids
            dtgDemandantes.Refresh();
            dtgDemandados.Refresh();
            dtgTercerosInteresados.Refresh();
            dtgContactoEmpresa.Refresh();

            dtgAbogadosDirectores.Refresh();
            dtgSociosResponsables.Refresh();
            dtgAbogadosAsistentes.Refresh();

            this.BeginInvoke(new Action(() =>
            {
                AjustarAlturaDataGridViewDemandantes();
                AjustarAlturaDataGridViewDemandados();
                AjustarAlturaDataGridViewTercerosInteresados();
                AjustarAlturaDataGridViewContactosEmpresa();
                AjustarAlturaDataGridViewAbogadosDirectores();
                AjustarAlturaDataGridViewSociosResponsables();
                AjustarAlturaDataGridViewAbogadosAsistentes();
            }));

            // 6) Ir al tab Detalles
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
            btnGuardarCaso.Visible = false;
            btnEditarCaso.Visible = true;
            HabilitarBotonesCaso();
            await BotonesAdmin();
        }

        private void AjustarFilasSegunRama(string rama)
        {
            bool esContencioso = rama == "CONTENCIOSO";
            bool esCasacion = rama.Contains("CASACION");

            // 🔹 Reset (opcional pero recomendable)
            tableLayoutPanel1.RowStyles[0].Height = 0F;
            tableLayoutPanel1.RowStyles[1].Height = 0F;

            if (esContencioso)
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[0].Height = 150F;
            }
            else if (esCasacion)
            {
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[1].Height = 150F;
            }

            // 🔥 Mostrar/ocultar controles por fila
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                int row = tableLayoutPanel1.GetRow(ctrl);

                if (row == 0)
                    ctrl.Visible = esContencioso;

                if (row == 1)
                    ctrl.Visible = esCasacion;
            }
        }
        private void ControlesLaboralCasoReferencia()
        {
            labelExpedienteRecursoCasacionCR.Visible = false;
            txtExpedienteRecursoCasacionCR.Visible = false;
            labelExpedienteReferenciaCRRC.Visible = false;
            txtExpedienteReferenciaCRRC.Visible = false;
            labelNombreParticularCRRC.Visible = false;
            txtboxNombreParticularCRRC.Visible = false;

            labelJuzgadoCasoReferencia.Visible = true;
            comboBoxJuzgadoCasoReferencia.Visible = true;

            labelCamaraCasoReferencia.Visible = false;
            comboBoxCamara.Visible= false;
            labelTitulo.Visible = false;
            comboBoxTituloCasoReferencia.Visible= false;
            labelMotivoCasacion.Visible = false;
            comboBoxMotivoCasacion.Visible = false;
            labelMotivoCasacionCR.Visible = false;
            comboBoxMotivoCasacionCR.Visible = false;
            AjustarFilasSegunRama("LABORAL");

        }
        private void ControlesCivilCasoReferencia()
        {
            labelExpedienteRecursoCasacionCR.Visible = false;
            txtExpedienteRecursoCasacionCR.Visible = false;
            labelExpedienteReferenciaCRRC.Visible = false;
            txtExpedienteReferenciaCRRC.Visible = false;
            labelNombreParticularCRRC.Visible = false;
            txtboxNombreParticularCRRC.Visible = false;

            labelJuzgadoCasoReferencia.Visible = true;
            labelJuzgadoCasoReferencia.Text = "Juzgado *";
            comboBoxJuzgadoCasoReferencia.Visible = true;

            labelCamaraCasoReferencia.Visible = false;
            comboBoxCamara.Visible = false;
            labelTitulo.Visible = false;
            comboBoxTituloCasoReferencia.Visible = false;
            labelMotivoCasacion.Visible = false;
            comboBoxMotivoCasacion.Visible = false;
            labelMotivoCasacionCR.Visible = false;
            comboBoxMotivoCasacionCR.Visible = false;
            AjustarFilasSegunRama("CIVIL");
        }

        private void ControlesCivilViaApremioCasoReferencia()
        {
            labelExpedienteRecursoCasacionCR.Visible = false;
            txtExpedienteRecursoCasacionCR.Visible = false;
            labelExpedienteReferenciaCRRC.Visible = false;
            txtExpedienteReferenciaCRRC.Visible = false;
            labelNombreParticularCRRC.Visible = false;
            txtboxNombreParticularCRRC.Visible = false;

            labelJuzgadoCasoReferencia.Visible = true;
            labelJuzgadoCasoReferencia.Text = "Juzgado *";
            comboBoxJuzgadoCasoReferencia.Visible = true;


            labelCamaraCasoReferencia.Visible = false;
            comboBoxCamara.Visible = false;
            labelTitulo.Visible = true;
            comboBoxTituloCasoReferencia.Visible = true;
            labelMotivoCasacion.Visible = false;
            comboBoxMotivoCasacion.Visible = false;
            labelMotivoCasacionCR.Visible = false;
            comboBoxMotivoCasacionCR.Visible = false;
            AjustarFilasSegunRama("CIVIL VIA APREMIO");
        }

        private void ControlesContenciosoCasoReferencia()
        {
            labelExpedienteRecursoCasacionCR.Visible = true;
            txtExpedienteRecursoCasacionCR.Visible = true;
            labelExpedienteReferenciaCRRC.Visible = false;
            txtExpedienteReferenciaCRRC.Visible = false;
            labelNombreParticularCRRC.Visible = false;
            txtboxNombreParticularCRRC.Visible = false;

            labelJuzgadoCasoReferencia.Visible = true;
            labelJuzgadoCasoReferencia.Text = "Sala *";
            comboBoxJuzgadoCasoReferencia.Visible = true;
            labelCamaraCasoReferencia.Visible = false;
            comboBoxCamara.Visible = false;
            labelTitulo.Visible = false;
            comboBoxTituloCasoReferencia.Visible = false;
            labelMotivoCasacion.Visible = false;
            comboBoxMotivoCasacion.Visible = false;
            labelMotivoCasacionCR.Visible = true;
            comboBoxMotivoCasacionCR.Visible = true;
            AjustarFilasSegunRama("CONTENCIOSO");
        }

        private void ControlesContenciosoRecursoCasacionCasoReferencia()
        {
            labelExpedienteRecursoCasacionCR.Visible = false;
            txtExpedienteRecursoCasacionCR.Visible = false;
            labelExpedienteReferenciaCRRC.Visible = true;
            txtExpedienteReferenciaCRRC.Visible = true;
            labelNombreParticularCRRC.Visible = true;
            txtboxNombreParticularCRRC.Visible = true;

            labelJuzgadoCasoReferencia.Visible = false;
            comboBoxJuzgadoCasoReferencia.Visible = false;
            labelCamaraCasoReferencia.Visible = true;
            comboBoxCamara.Visible = true;
            labelTitulo.Visible = false;
            comboBoxTituloCasoReferencia.Visible = false;
            labelMotivoCasacion.Visible = true;
            comboBoxMotivoCasacion.Visible = true;
            labelMotivoCasacionCR.Visible = false;
            comboBoxMotivoCasacionCR.Visible = false;
            AjustarFilasSegunRama("CONTENCIOSO RECURSO DE CASACION");
        }


        private void CargarCivil(CasoCivilDetalleData data)
        {
            ControlesCivilCasoReferencia();
            txtExpedienteCasoReferencia.Text = data.caso.expediente ?? "";
            txtNombreParticularCasoReferencia.Text = data.caso.nombre_particular ?? "";
            comboBoxOficialCasoReferencia.Text = data.caso.oficial ?? "";
            comboBoxNotificadorCasoReferencia.Text = data.caso.notificador ?? "";
            comboBoxJuzgadoCasoReferencia.Text = data.caso.juzgado ?? "";
            txtEstadoCasoReferencia.Text = data.caso.estado ?? "";
            textBoxObervacionesCasoReferencia.Text =
                (data.caso.observaciones ?? "").Replace("\n", Environment.NewLine);

            CargarListas(data);
        }

        private void CargarCivilApremio(CasoCivilViaApremioDetalleData data)
        {
            ControlesCivilViaApremioCasoReferencia();
            txtExpedienteCasoReferencia.Text = data.caso.expediente ?? "";
            txtNombreParticularCasoReferencia.Text = data.caso.nombre_particular ?? "";
            comboBoxOficialCasoReferencia.Text = data.caso.oficial ?? "";
            comboBoxNotificadorCasoReferencia.Text = data.caso.notificador ?? "";
            comboBoxJuzgadoCasoReferencia.Text = data.caso.juzgado ?? "";
            txtEstadoCasoReferencia.Text = data.caso.estado ?? "";
            textBoxObervacionesCasoReferencia.Text =
                (data.caso.observaciones ?? "").Replace("\n", Environment.NewLine);
            comboBoxTituloCasoReferencia.SelectedIndex = comboBoxTituloCasoReferencia.Items.IndexOf(data.caso.titulo ?? "");

            CargarListas(data);

            // 🔥 extra si quieres
            // labelTipo.Text = "VÍA DE APREMIO";
        }

        private void CargarLaboral(CasoLaboralDetalleData data)
        {
            ControlesLaboralCasoReferencia();

            txtExpedienteCasoReferencia.Text = data.caso.expediente ?? "";
            txtNombreParticularCasoReferencia.Text = data.caso.nombre_particular ?? "";
            comboBoxOficialCasoReferencia.Text = data.caso.oficial ?? "";
            comboBoxNotificadorCasoReferencia.Text = data.caso.notificador ?? "";
            comboBoxJuzgadoCasoReferencia.Text = data.caso.juzgado ?? "";
            txtEstadoCasoReferencia.Text = data.caso.estado ?? "";
            textBoxObervacionesCasoReferencia.Text = data.caso.observaciones != null ? data.caso.observaciones.Replace("\n", Environment.NewLine) : "";

            CargarListas(data);
        }

        private void CargarContencioso(CasoContenciosoDetalleData data)
        {
            ControlesContenciosoCasoReferencia();
            txtExpedienteCasoReferencia.Text = data.caso?.expediente ?? "";
            txtNombreParticularCasoReferencia.Text = data.caso?.nombre_particular ?? "";
            comboBoxOficialCasoReferencia.Text = data.caso?.oficial ?? "";
            comboBoxNotificadorCasoReferencia.Text = data.caso?.notificador ?? "";
            comboBoxJuzgadoCasoReferencia.Text = data.caso?.sala ?? "";
            txtEstadoCasoReferencia.Text = data.caso?.estado ?? "";
            textBoxObervacionesCasoReferencia.Text = data.caso?.observaciones != null ? data.caso.observaciones.Replace("\n", Environment.NewLine) : "";

            txtExpedienteRecursoCasacionCR.Text = data.recurso_casacion?.expediente ?? "";

            if (data.recurso_casacion?.motivo == "FONDO")
            {
                comboBoxMotivoCasacionCR.SelectedIndex = 1;
            }
            else if (data.recurso_casacion?.motivo == "FORMA")
            {
                comboBoxMotivoCasacionCR.SelectedIndex = 0;
            }
            else if (data.recurso_casacion?.motivo == "FORMA Y FONDO")
            {
                comboBoxMotivoCasacionCR.SelectedIndex = 2;
            }

            CargarListas(data);
        }

        private void CargarCasacion(CasoRecursoCasacionDetalleData data)
        {
            ControlesContenciosoRecursoCasacionCasoReferencia();
            txtExpedienteCasoReferencia.Text = data.caso.expediente ?? "";
            txtNombreParticularCasoReferencia.Text = data.caso.nombre_particular ?? "";
            comboBoxOficialCasoReferencia.Text = data.caso.oficial ?? "";
            comboBoxNotificadorCasoReferencia.Text = data.caso.notificador ?? "";
            //comboBoxJuzgadoCasoReferencia.Text = data.caso.juzgado ?? "";
            txtEstadoCasoReferencia.Text = data.caso.estado ?? "";
            textBoxObervacionesCasoReferencia.Text = data.caso.observaciones != null ? data.caso.observaciones.Replace("\n", Environment.NewLine) : "";

            txtExpedienteReferenciaCRRC.Text = data.caso_origen?.expediente ?? "";
            txtboxNombreParticularCRRC.Text = data.caso_origen?.nombre_particular ?? "";
            comboBoxCamara.SelectedItem = data.caso.juzgado ?? "";
            if (data.motivo_casacion == "FONDO")
            {
                comboBoxMotivoCasacion.SelectedIndex = 1;
            }
            else if (data.motivo_casacion == "FORMA")
            {
                comboBoxMotivoCasacion.SelectedIndex = 0;
            }
            else if (data.motivo_casacion == "FORMA Y FONDO")
            {
                comboBoxMotivoCasacion.SelectedIndex = 2;
            }
            CargarListas(data);

        }

        private void CargarListas(dynamic data)
        {
            var p = data.personas_por_rol ?? new Dictionary<string, List<PersonaMiniDto>>();

            MapPersonas(p, "Demandante", listaDemandantesCasoReferencia);
            MapPersonas(p, "Demandado", listaDemandadosCasoReferencia);
            MapPersonas(p, "Tercero Interesado", listaTercerosInteresadosCasoReferencia);
            MapPersonas(p, "Contacto de Empresa", listaContactosEmpresaCasoReferencia);

            var u = data.usuarios_por_rol ?? new Dictionary<string, List<UsuarioMiniDto>>();

            MapUsuarios(u, "Abogado Director", listaAbogadosDirectoresCasoReferencia);
            MapUsuarios(u, "Socio Responsable", listaSociosResponsablesCasoReferencia);
            MapUsuarios(u, "Abogado Asistente", listaAbogadosAsistentesCasoReferencia);

            dtgDemandadosCasoReferencia.Refresh();
            dataGridViewDemandantesCasoReferencia.Refresh();
            dtgTercerosInteresadosCasoReferencia.Refresh();
            dtgContactosEmpresaCasoReferencia.Refresh();
            dtgAbogadosDirectoresCasoReferencia.Refresh();
            dtgSociosResponsablesCasoReferencia.Refresh();
            dtgAbogadosAsistentesCasoReferencia.Refresh();
        }

        private async Task CargarDatosCasoReferencia(int idCaso)
        {
            var model = new CasoConstitucionalAmparoModel();
            int idUsuario = UserSession.Id;

            var caso = await model.ObtenerCaso(idUsuario, idCaso);

            if (caso == null)
            {
                MessageBox.Show("No se pudo cargar el caso");
                return;
            }

            LimpiarFormularioCR();
            LimpiarListasCasoReferencia();

            // 🔥 SWITCH POR TIPO REAL
            switch (caso)
            {
                case CasoCivilDetalleData civil:
                    CargarCivil(civil);
                    break;

                case CasoCivilViaApremioDetalleData civilApremio:
                    CargarCivilApremio(civilApremio);
                    break;

                case CasoLaboralDetalleData laboral:
                    CargarLaboral(laboral);
                    break;

                case CasoContenciosoDetalleData contencioso:
                    CargarContencioso(contencioso);
                    break;

                case CasoRecursoCasacionDetalleData casacion:
                    CargarCasacion(casacion);
                    break;

                default:
                    MessageBox.Show("Tipo de caso no soportado");
                    break;
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
                dtgCasosCiviles.ClearSelection();
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



        private async void CrearBotonesAccion(DataGridView dtg)
        {
            await VerificarTipoUsuario();
            if (!dtg.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = isAdminConstitucional
                    ? "✏️"
                    : "👁️"
                    ,
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEditar);
            }
            

            if (isAdminConstitucional == true)
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

            if (isAdminConstitucional == true && dtg.Columns.Contains("Eliminar"))
                dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 2;

        }

        private async void CrearBotonesAccionHistorial(DataGridView dtg)
        {
            await VerificarTipoUsuario();
            if (!dtg.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = isAdminConstitucional
                    ? "✏️"
                    : "👁️"
                    ,
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEditar);
            }

            if (isAdminConstitucional == true)
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

            if (isAdminConstitucional == true && dtg.Columns.Contains("Eliminar"))
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
            lblTitulo.Text = "Nuevo Caso Constitucional";
            btnGuardarCaso.Text = "Guardar";
            idCasoReferencia = null;
            _actualizandoCaso= false;
            LimpiarFormulario();
            LimpiarFormularioCR();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
            btnGuardarCaso.Visible = true;
            btnEditarCaso.Visible = false;
        }

        private async Task CargarCasos()
        {

            int idUsuario = UserSession.Id;
            string filtro = txtBuscar.Text;
            var response = await casoConstitucionalModel.ObtenerCasosConstitucionales(idUsuario, paginaActual, registrosPorPagina, filtro);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsTercerosInteresados.DataSource = response.data;
                dtgCasosCiviles.Refresh();
                // Actualizar paginación
                totalRegistros = response.total;
                labelTotal.Text = $"Total de casos constitucionales: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //caso referencia
        private void alistarListaDemandadosCR()
        {
            dtgDemandadosCasoReferencia.DataSource = listaDemandadosCasoReferencia;

            dtgDemandadosCasoReferencia.AllowUserToAddRows = false;
            dtgDemandadosCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgDemandadosCasoReferencia.DataSource = listaDemandadosCasoReferencia;

            listaDemandadosCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewDemandadosCR();
            };

            //CrearBotonQuitarDemandado();
            //dtgDemandadosCasoReferencia.CellClick -= dtgDemandados_CellClick;
            //dtgDemandadosCasoReferencia.CellClick += dtgDemandados_CellClick;
        }
        private void alistarListaDemandantesCR()
        {
            dataGridViewDemandantesCasoReferencia.DataSource = listaDemandantesCasoReferencia;
            dataGridViewDemandantesCasoReferencia.AllowUserToAddRows = false;
            dataGridViewDemandantesCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridViewDemandantesCasoReferencia.DataSource = listaDemandantesCasoReferencia;

            listaDemandantesCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewDemandantesCR();
            };

        }
        private void alistarListaTercerosInteresadosCR()
        {
            dtgTercerosInteresadosCasoReferencia.DataSource = listaTercerosInteresadosCasoReferencia;

            dtgTercerosInteresadosCasoReferencia.AllowUserToAddRows = false;
            dtgTercerosInteresadosCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgTercerosInteresadosCasoReferencia.DataSource = listaTercerosInteresadosCasoReferencia;

            listaTercerosInteresadosCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewTercerosInteresadosCR();
            };

        }

        private void alistarListaContactosEmpresaCR()
        {
            dtgContactosEmpresaCasoReferencia.DataSource = listaContactosEmpresaCasoReferencia;

            dtgContactosEmpresaCasoReferencia.AllowUserToAddRows = false;
            dtgContactosEmpresaCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgContactosEmpresaCasoReferencia.DataSource = listaContactosEmpresaCasoReferencia;

            listaContactosEmpresaCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewContactosEmpresaCR();
            };

        }

        private void alistarListaAbogadosDirectoresCR()
        {
            dtgAbogadosDirectoresCasoReferencia.DataSource = listaAbogadosDirectoresCasoReferencia;

            dtgAbogadosDirectoresCasoReferencia.AllowUserToAddRows = false;
            dtgAbogadosDirectoresCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgAbogadosDirectoresCasoReferencia.DataSource = listaAbogadosDirectoresCasoReferencia;

            listaAbogadosDirectoresCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewAbogadosDirectoresCR();
            };

        }

        private void alistarListaSociosResponsablesCR()
        {
            dtgSociosResponsablesCasoReferencia.DataSource = listaSociosResponsablesCasoReferencia;

            dtgSociosResponsablesCasoReferencia.AllowUserToAddRows = false;
            dtgSociosResponsablesCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgSociosResponsablesCasoReferencia.DataSource = listaSociosResponsablesCasoReferencia;

            listaSociosResponsablesCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewSociosResponsablesCR();
            };
        }

        private void alistarListaAbogadosAsistentesCR()
        {
            dtgAbogadosAsistentesCasoReferencia.DataSource = listaAbogadosAsistentesCasoReferencia;

            dtgAbogadosAsistentesCasoReferencia.AllowUserToAddRows = false;
            dtgAbogadosAsistentesCasoReferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgAbogadosAsistentesCasoReferencia.DataSource = listaAbogadosAsistentesCasoReferencia;

            listaAbogadosAsistentesCasoReferencia.ListChanged += (s, e) =>
            {
                AjustarAlturaDataGridViewAbogadosAsistentesCR();
            };
        }


        //caso
        private void alistarListaDemandados()
        {
            dtgDemandados.DataSource = listaDemandados;

            dtgDemandados.AllowUserToAddRows = false;
            dtgDemandados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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

        private async void CrearBotonQuitarDemandado()
        {
            await VerificarTipoUsuario();
            if (!dtgDemandados.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private void CrearBotonQuitarDemandante()
        {
            if (!dtgDemandantes.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }
        private void CrearBotonQuitarTerceroInteresado()
        {
            if (!dtgTercerosInteresados.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private void CrearBotonQuitarAbogadoDirector()
        {
            if (!dtgAbogadosDirectores.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private void CrearBotonQuitarContactoEmpresa()
        {
            if (!dtgContactoEmpresa.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private void CrearBotonQuitarSocioResponsable()
        {
            if (!dtgSociosResponsables.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private void CrearBotonQuitarAbogadoAsistente()
        {
            if (!dtgAbogadosAsistentes.Columns.Contains("Quitar"))
            {
                if (isAdminConstitucional)
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
        }

        private async void Constitucional_Terminado_Load(object sender, EventArgs e)
        {
            await VerificarTipoUsuario();
            if (!_yaCargo)
                await LoadAsync();
        }


        private void dtgCasosCiviles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgCasosCiviles.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgCasosCiviles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
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
            if (dtgCasosCiviles.Columns["id"] != null)
            {
                dtgCasosCiviles.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgCasosCiviles.Columns["id_rol"] != null)
            {
                dtgCasosCiviles.Columns["id_rol"].Visible = false;
            }

            CrearBotonesAccion(dtgCasosCiviles);
            dtgCasosCiviles.ClearSelection();
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
            int idCasoReferenciaAmparo = idCasoReferencia ?? 0;

            var req = new CrearCasoAmparoRequest
            {
                Expediente = txtExpediente.Text,
                Oficial = comboboxOficial.Text,
                NombreParticular = txtNombreParticular.Text,
                Estado = EstadoConstitucional.estado ?? txtEstado.Text,
                Observaciones = EstadoConstitucional.observaciones ?? txtObservaciones.Text,
                UsuarioCreador = UserSession.Id,
                Fecha = EstadoConstitucional.fechaEstado.HasValue
                ? EstadoConstitucional.fechaEstado.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : null,
                FechaVencimiento = EstadoConstitucional.fechaVencimiento.HasValue
                ? EstadoConstitucional.fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "",
                Causa = txtCausaAgravioHechos.Text,

                Solicitantes = listaDemandantes.Select(x => x.id).ToList(),
                AutoridadesImpugnadas = listaDemandados.Select(x => x.id).ToList(),
                TercerosInteresados = listaTercerosInteresados.Select(x => x.id).ToList(),
                ContactosEmpresa = listaContactosEmpresa.Select(x => x.id).ToList(),

                AbogadosDirectores = listaAbogadosDirectores.Select(x => x.id).ToList(),
                SociosResponsables = listaSociosResponsables.Select(x => x.id).ToList(),
                AbogadosAsistentes = listaAbogadosAsistentes.Select(x => x.id).ToList(),
                CasoReferenciaId = idCasoReferenciaAmparo
            };

            var resultado = await casoConstitucionalModel.CrearCasoAmparo(req);

            if (resultado.success)
            {
                MessageBox.Show("Caso constitucional creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            EliminarTabPage(tabPageCasoReferencia);
            await EjecutarConLoaderAsync(async () =>
            {
                await CargarCasos();
            });
        }


        private async void dtgCasosCiviles_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            if (_cargandoCaso) return;

            if (dtgCasosCiviles.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idCaso = Convert.ToInt32(dtgCasosCiviles.Rows[e.RowIndex].Cells["id"].Value);
                string? expediente = Convert.ToString(dtgCasosCiviles.Rows[e.RowIndex].Cells["expediente"].Value);
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
                        resultado = await casoConstitucionalModel.EliminarCasoConstitucional(idCaso, UserSession.Id);
                    });

                    if (resultado == null)
                    {
                        MessageBox.Show("No se obtuvo respuesta del servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (resultado.success)
                    {
                        MessageBox.Show("Caso constitucional eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await EjecutarConLoaderAsync(async () =>
                        {
                            await CargarCasos();
                        });
                    }
                    else
                    {
                        MessageBox.Show(resultado?.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

            if (dtgCasosCiviles.Columns[e.ColumnIndex].Name == "Terminar")
            {
                int idCaso = Convert.ToInt32(dtgCasosCiviles.Rows[e.RowIndex].Cells["id"].Value);
                string? expediente = Convert.ToString(dtgCasosCiviles.Rows[e.RowIndex].Cells["expediente"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que desea terminar el caso " + expediente + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    FrmAgregarEstadoConstitucionalTerminado frmAgregarEstado = new FrmAgregarEstadoConstitucionalTerminado();
                    frmAgregarEstado.ShowDialog();

                    if (EstadoConstitucional.estado != null && EstadoConstitucional.fechaEstado != null)
                    {
                        var response = await historialModel.TerminarCasoConstitucional(
                            casoId: idCaso,
                            usuarioId: UserSession.Id,
                            fecha: EstadoConstitucional.fechaEstado.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                            anotaciones: EstadoConstitucional.observaciones,
                            origen: "CONSTITUCIONAL AMPARO"
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

            if (dtgCasosCiviles.Columns[e.ColumnIndex].Name == "Editar")
            {
                try
                {
                    _cargandoCaso = true;
                    dtgCasosCiviles.Enabled = false;

                    btnGuardarCaso.Text = "Actualizar";
                    lblTitulo.Text = "Editar Caso Constitucional";
                    
                    int idCaso = Convert.ToInt32(dtgCasosCiviles.Rows[e.RowIndex].Cells["id"].Value);
                    _idCasoEditar = idCaso;
                    _actualizandoCaso = true;
                    _huboCambioEstado = false;
                    idCasoReferencia = null;
                    LimpiarFormulario();
                    LimpiarFormularioCR();

                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosCaso(idCaso);
                    });
                }
                finally
                {
                    dtgCasosCiviles.Enabled = true;
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

        private void Constitucional_Terminado_Resize_1(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void btnAgregarEstado_Click(object sender, EventArgs e)
        {
            FrmAgregarEstadoConstitucional frmAgregarEstado = new FrmAgregarEstadoConstitucional();
            frmAgregarEstado.ShowDialog();

            if (EstadoConstitucional.estado != null)
            {
                _huboCambioEstado = true;
                txtEstado.Text = EstadoConstitucional.estado.ToString();
                txtObservaciones.AppendText(Environment.NewLine + EstadoConstitucional.observaciones);

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
            else if (tabControl1.SelectedTab == tabPageCasoReferencia)
            {
                tabPageCasoReferencia.AutoScrollPosition = new Point(0, 0);
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

        private void AjustarLayoutPorResolucionCR()
        {
            if (flowLayoutPanel2.Controls.Count == 0) return;

            int w = flowLayoutPanel2.ClientSize.Width;
            if (w <= 50) return;

            int padding = flowLayoutPanel2.Padding.Left + flowLayoutPanel2.Padding.Right;

            int marginX = 10;
            int gap = 20;
            int ancho2Cols = (w - padding - gap) / 2;
            bool caben2 = (ancho2Cols >= 620);

            if (caben2)
            {
                flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel2.WrapContents = true;

                foreach (Panel p in flowLayoutPanel2.Controls.OfType<Panel>())
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
                flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel2.WrapContents = false;

                int ancho1Col = w - padding - 10;

                foreach (Panel p in flowLayoutPanel2.Controls.OfType<Panel>())
                {
                    p.AutoSize = true;
                    p.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                    p.MinimumSize = new Size(ancho1Col, p.MinimumSize.Height);
                    p.MaximumSize = new Size(ancho1Col, 0);

                    p.Margin = new Padding(5);
                }
            }

            flowLayoutPanel2.PerformLayout();

        }
        //caso referencia
        private void AjustarAlturaDataGridViewDemandadosCR()
        {
            dtgDemandadosCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandadosCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandadosCasoReferencia.ColumnHeadersHeight;

            dtgDemandadosCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgDemandadosCasoReferencia.ScrollBars = ScrollBars.None;

            panelDemandadosCR.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }
        private void AjustarAlturaDataGridViewDemandantesCR()
        {
            dataGridViewDemandantesCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dataGridViewDemandantesCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dataGridViewDemandantesCasoReferencia.ColumnHeadersHeight;

            dataGridViewDemandantesCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dataGridViewDemandantesCasoReferencia.ScrollBars = ScrollBars.None;

            dataGridViewDemandantesCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }
        private void AjustarAlturaDataGridViewTercerosInteresadosCR()
        {
            dtgTercerosInteresadosCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgTercerosInteresadosCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgTercerosInteresadosCasoReferencia.ColumnHeadersHeight;

            dtgTercerosInteresadosCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgTercerosInteresadosCasoReferencia.ScrollBars = ScrollBars.None;

            dtgTercerosInteresadosCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }

        private void AjustarAlturaDataGridViewContactosEmpresaCR()
        {
            dtgContactosEmpresaCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgContactosEmpresaCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgContactosEmpresaCasoReferencia.ColumnHeadersHeight;

            dtgContactosEmpresaCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgContactosEmpresaCasoReferencia.ScrollBars = ScrollBars.None;

            dtgContactosEmpresaCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }
        private void AjustarAlturaDataGridViewAbogadosDirectoresCR()
        {
            dtgAbogadosDirectoresCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosDirectoresCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosDirectoresCasoReferencia.ColumnHeadersHeight;

            dtgAbogadosDirectoresCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosDirectoresCasoReferencia.ScrollBars = ScrollBars.None;

            dtgAbogadosDirectoresCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }

        private void AjustarAlturaDataGridViewSociosResponsablesCR()
        {
            dtgSociosResponsablesCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgSociosResponsablesCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgSociosResponsablesCasoReferencia.ColumnHeadersHeight;

            dtgSociosResponsablesCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgSociosResponsablesCasoReferencia.ScrollBars = ScrollBars.None;

            dtgSociosResponsablesCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

        }

        private void AjustarAlturaDataGridViewAbogadosAsistentesCR()
        {
            dtgAbogadosAsistentesCasoReferencia.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgAbogadosAsistentesCasoReferencia.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgAbogadosAsistentesCasoReferencia.ColumnHeadersHeight;

            dtgAbogadosAsistentesCasoReferencia.Height = alturaFilas + alturaHeaders + 22;

            dtgAbogadosAsistentesCasoReferencia.ScrollBars = ScrollBars.None;

            dtgAbogadosAsistentesCasoReferencia.PerformLayout();
            flowLayoutPanel2.PerformLayout();

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

        private void LimpiarListasCasoReferencia()
        {
            listaDemandadosCasoReferencia.Clear();
            listaDemandantesCasoReferencia.Clear();
            listaTercerosInteresadosCasoReferencia.Clear();
            listaContactosEmpresaCasoReferencia.Clear();

            listaAbogadosDirectoresCasoReferencia.Clear();
            listaSociosResponsablesCasoReferencia.Clear();
            listaAbogadosAsistentesCasoReferencia.Clear();

            dtgDemandadosCasoReferencia.ClearSelection();
            dataGridViewDemandantesCasoReferencia.ClearSelection();
            dtgTercerosInteresadosCasoReferencia.ClearSelection();
            dtgTercerosInteresadosCasoReferencia.ClearSelection();

            dtgAbogadosDirectoresCasoReferencia.ClearSelection();
            dtgAbogadosAsistentesCasoReferencia.ClearSelection();
            dtgAbogadosAsistentes.ClearSelection();

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

        private void Constitucional_Terminado_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void Detalles_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
            this.BeginInvoke(new Action(AjustarLayoutPorResolucionCR));

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

            int idCasoReferenciaConstitucional = idCasoReferencia ?? 0;
            var req = new EditarCasoAmparoRequest
            {
                UsuarioId = UserSession.Id,
                CasoId = _idCasoEditar,

                Expediente = txtExpediente.Text,
                Oficial = comboboxOficial.Text,
                NombreParticular = txtNombreParticular.Text,
                Causa = txtCausaAgravioHechos.Text,

                // historial (tomas lo último elegido en tu modal de estado)
                HuboCambioEstado = cambioEstado,
                Estado = EstadoConstitucional.estado ?? txtEstado.Text,
                Observaciones = EstadoConstitucional.observaciones ?? txtObservaciones.Text,

                Fecha = (EstadoConstitucional.fechaEstado ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
                FechaVencimiento = EstadoConstitucional.fechaVencimiento.HasValue
                        ? EstadoConstitucional.fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : null,

                Solicitantes = listaDemandantes.Select(x => x.id).ToList(),
                AutoridadesImpugnadas = listaDemandados.Select(x => x.id).ToList(),
                TercerosInteresados = listaTercerosInteresados.Select(x => x.id).ToList(),
                ContactosEmpresa = listaContactosEmpresa.Select(x => x.id).ToList(),

                AbogadosDirectores = listaAbogadosDirectores.Select(x => x.id).ToList(),
                SociosResponsables = listaSociosResponsables.Select(x => x.id).ToList(),
                AbogadosAsistentes = listaAbogadosAsistentes.Select(x => x.id).ToList(),

                CasoReferenciaId = idCasoReferenciaConstitucional
            };

            ApiResponseEditarCasoAmparo resultado = null;

            await EjecutarConLoaderAsync(async () =>
            {
                resultado = await casoConstitucionalModel.EditarCasoAmparo(req);
            });

            if (resultado == null)
            {
                MessageBox.Show("No se obtuvo respuesta del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.success)
            {
                MessageBox.Show("Caso constitucional actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await EjecutarConLoaderAsync(async () =>
                {
                    await CargarCasos();
                });
                LimpiarFormulario();
                _idCasoEditar = 0;

                AnadirTabPage(Listar);
                EliminarTabPage(Detalles);
                EliminarTabPage(tabPageCasoReferencia);
                _actualizandoCaso = false;
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void roundedButton24_Click(object sender, EventArgs e)
        {

        }
        private async Task ListarHistorial()
        {

            var datosHistorial = await historialModel.ObtenerHistorialCasoConstitucional(_idCasoEditar);

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
            EliminarTabPage(tabPageCasoReferencia);
            await EjecutarConLoaderAsync(async () =>
            {
                await ListarHistorial();
            });
        }

        private async Task ListarArchivosCaso()
        {
            var res = await archivoModel.ListarArchivosCasoConstitucional(_idCasoEditar);

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

        private async void CrearBotonesAccionArchivos(DataGridView dtg)
        {
            await VerificarTipoUsuario();
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

            if (isAdminConstitucional)
            {
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
            }

            // mover al final (en orden)
            dtg.Columns["Abrir"].DisplayIndex = dtg.ColumnCount - 1;
            dtg.Columns["Descargar"].DisplayIndex = dtg.ColumnCount - 2;
            if (isAdminConstitucional)
            {
                dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 3;
            }
        }

        private async void btnVerArchivos_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageArchivos);
            EliminarTabPage(tabPageHistorial);
            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageCasoReferencia);
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
            var resp = await archivoModel.ListarArchivosCasoConstitucional(_idCasoEditar);

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
                        resp = await archivoModel.DescargarArchivoCasoConstitucional(_idCasoEditar, archivoId, tempFile);
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
                            resp = await archivoModel.DescargarArchivoCasoConstitucional(_idCasoEditar, archivoId, sfd.FileName);
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
                            resp = await archivoModel.EliminarArchivoCasoConstitucional(_idCasoEditar, archivoId);
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

            ApiResponse<List<SubirArchivoCasoConstitucionalData>> response = null;

            try
            {
                btnSubirArchivo.Enabled = false;
                btnSubirArchivo.Text = "Subiendo...";

                await EjecutarConLoaderAsync(async () =>
                {
                    response = await archivoModel.SubirArchivosCasoConstitucional(_idCasoEditar, ofd.FileNames.ToList());
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
            var item = grid.Rows[e.RowIndex].DataBoundItem as HistorialCasoConstitucionalDetalle;
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
                    resp = await historialModel.EliminarHistorialCasoConstitucional(
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
        void HabilitarControlesEdicionHistorial()
        {
            comboboxEstado.Enabled = isAdminConstitucional;
            dateTimePickerFechaEstado.Enabled = isAdminConstitucional;
            txtObservacionesHistorial.Enabled = isAdminConstitucional;
            dateTimePickerFechaVencimiento.Enabled = isAdminConstitucional;
            dateTimePickerHoraVencimiento.Enabled = isAdminConstitucional;
        }

        private void CargarDatosHistorialEnTab(HistorialCasoConstitucionalDetalle item)
        {
            _idHistorialEditar = item.id;
            _casoIdHistorialEditar = item.caso_id;

            dateTimePickerFechaEstado.Value = item.fecha;

            comboboxEstado.Text = item.estado ?? "";
            string origen = item.origen;
            bool requiereVencimiento = EstadoConstitucionalHelper.RequiereVencimiento(item.estado ?? "");
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

            if (isAdminConstitucional)
            {
                btnGuardarEdicionHistorial.Visible = true;
            }
            else
            {
                btnGuardarEdicionHistorial.Visible = false;
            }

            HabilitarControlesEdicionHistorial();
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

            var req = new EditarHistorialCasoConstitucionalRequest
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

            bool requiereVencimiento = EstadoConstitucionalHelper.RequiereVencimiento(
                comboboxEstado.Text
            );

            if (requiereVencimiento)
            {
                checkBoxTieneVencimiento.Checked = true;
            }
            else
            {
                checkBoxTieneVencimiento.Checked = false;
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

            txtObservacionesHistorial.Text = EstadoConstitucionalHelper.GenerarObservacion(
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
            LimpiarFormularioCR();
            if (idCasoReferencia != null && idCasoReferencia != 0)
            {
                int idCasoR = idCasoReferencia ?? 0;
                if (idCasoR != 0)
                {
                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosCasoReferencia(idCasoR);

                    });
                }
            }
            AnadirTabPage(tabPageCasoReferencia);
            EliminarTabPage(Detalles);
            EliminarTabPage(Listar);
            EliminarTabPage(tabPageEditarHistorial);
            EliminarTabPage(tabPageEditarHistorial);
        }

        private async void btnAgregarCasoReferencia_Click(object sender, EventArgs e)
        {
            FrmAgregarCaso frmAgregarCasoReferencia = new FrmAgregarCaso();

            if (frmAgregarCasoReferencia.ShowDialog() == DialogResult.OK)
            {
                int? idCaso = frmAgregarCasoReferencia.IdCasoSeleccionado;
                int idCasoR = idCaso ?? 0;
                if (idCaso != null)
                {
                    txtCasoReferenciaId.Text = idCaso.ToString();
                    idCasoReferencia = idCasoR;
                    await EjecutarConLoaderAsync(async () =>
                    {
                        await CargarDatosCasoReferencia(idCasoR);
                    });
                }
                else
                {
                    MessageBox.Show("No fue posible cargar los datos del caso de referencia ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void roundedButton19_Click_2(object sender, EventArgs e)
        {
            AnadirTabPage(Detalles);
            EliminarTabPage(tabPageCasoReferencia);
        }

        private void tabPageCasoReferencia_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
            this.BeginInvoke(new Action(AjustarLayoutPorResolucionCR));
        }

        private void roundedButton57_Click(object sender, EventArgs e)
        {

        }
        private void btnEliminarCasoReferencia_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar el caso de referencia?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                idCasoReferencia = null;
                LimpiarFormularioCR();
            }
        }

        private void dataGridViewDemandantesCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridViewDemandantesCasoReferencia.Columns["id"] != null)
            {
                dataGridViewDemandantesCasoReferencia.Columns["id"].Visible = false;
            }

            if (dataGridViewDemandantesCasoReferencia.Columns["id_rol"] != null)
            {
                dataGridViewDemandantesCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dataGridViewDemandantesCasoReferencia.ClearSelection();
        }

        private void dtgDemandadosCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgDemandadosCasoReferencia.Columns["id"] != null)
            {
                dtgDemandadosCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgDemandadosCasoReferencia.Columns["id_rol"] != null)
            {
                dtgDemandadosCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgDemandadosCasoReferencia.ClearSelection();
        }

        private void dtgTercerosInteresadosCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgTercerosInteresadosCasoReferencia.Columns["id"] != null)
            {
                dtgTercerosInteresadosCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgTercerosInteresadosCasoReferencia.Columns["id_rol"] != null)
            {
                dtgTercerosInteresadosCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgTercerosInteresadosCasoReferencia.ClearSelection();
        }

        private void dtgContactosEmpresaCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgContactosEmpresaCasoReferencia.Columns["id"] != null)
            {
                dtgContactosEmpresaCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgContactosEmpresaCasoReferencia.Columns["id_rol"] != null)
            {
                dtgContactosEmpresaCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgContactosEmpresaCasoReferencia.ClearSelection();
        }

        private void dtgAbogadosDirectoresCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAbogadosDirectoresCasoReferencia.Columns["id"] != null)
            {
                dtgAbogadosDirectoresCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgAbogadosDirectoresCasoReferencia.Columns["id_rol"] != null)
            {
                dtgAbogadosDirectoresCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgAbogadosDirectoresCasoReferencia.ClearSelection();
        }

        private void dtgAbogadosAsistentesCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgAbogadosAsistentesCasoReferencia.Columns["id"] != null)
            {
                dtgAbogadosAsistentesCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgAbogadosAsistentesCasoReferencia.Columns["id_rol"] != null)
            {
                dtgAbogadosAsistentesCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgAbogadosAsistentesCasoReferencia.ClearSelection();
        }

        private void dtgSociosResponsablesCasoReferencia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgSociosResponsablesCasoReferencia.Columns["id"] != null)
            {
                dtgSociosResponsablesCasoReferencia.Columns["id"].Visible = false;
            }

            if (dtgSociosResponsablesCasoReferencia.Columns["id_rol"] != null)
            {
                dtgSociosResponsablesCasoReferencia.Columns["id_rol"].Visible = false;
            }

            dtgSociosResponsablesCasoReferencia.ClearSelection();
        }

        private void tabPageCasoReferencia_Click(object sender, EventArgs e)
        {

        }
    }
}
