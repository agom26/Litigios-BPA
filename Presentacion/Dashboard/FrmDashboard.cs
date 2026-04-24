
using AccesoDatos.Entidades;
using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Comun.Models.Casos.Laborales;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Dominio.Entidades;
using Dominio.Entidades.Alertas;
using Dominio.Entidades.Plazos;
using Newtonsoft.Json;
using Presentacion.Casos.Abogados_asignados;
using Presentacion.Casos.Estados;
using Presentacion.Casos.Participantes;
using Presentacion.Personas;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Presentacion.Dashboard
{
    public partial class FrmDashboard : Form, IAsyncLoadable
    {
        private bool _yaCargo = false;
        private bool _cargandoCaso = false;
        private bool _cargando = false;

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private string ultimoOrigenHistorial;
        private HistorialCasoLaboralDetalle _historialSeleccionado;
        private int _idHistorialEditar = 0;
        private int _casoIdHistorialEditar = 0;

        private BindingSource bsPlazos = new BindingSource();
        private BindingSource bsAlertas = new BindingSource();
        private int _idCasoEditar;
        PlazosModel plazosModel = new PlazosModel();
        AlertasModel alertasModel = new AlertasModel();
        UserModel userModel = new UserModel();

        private bool isAdminLaboral = false;
        Color normal = ColorTranslator.FromHtml("#ffffff"); // tu color base
        Color hover = ColorTranslator.FromHtml("#e5e5e5");
        private void VerificarTipoUsuario()

        {
            isAdminLaboral = UserSession.Modulos.Any(m =>
                (m.clave_slug ?? "").Trim().Equals("laboral", StringComparison.OrdinalIgnoreCase) &&
                (m.nombre_rol ?? "").Trim().Equals("Administrador", StringComparison.OrdinalIgnoreCase));
        }
        public async Task LoadAsync()
        {
            if (_yaCargo) return;
            _yaCargo = true;
            dtgAlertas.DataSource = bsAlertas;
            dtgPlazos.DataSource = bsPlazos;

            await CargarAlertas();
            await CargarPlazos();
            await CargarCasosPorUsuario();


            EliminarTabPage(Detalles);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(tabPageHistorial);
            EliminarTabPage(tabPageEditarHistorial);


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

        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            comboBoxJuzgado.Text = "";
            comboboxOficial.Text = "";
            txtNombreParticular.Text = "";
            comboboxNotificador.Text = "";
            txtNombreParticular.Text = "";
        }
        private void BotonesAdmin()
        {
            VerificarTipoUsuario();
            if (isAdminLaboral)
            {

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
            }
            else
            {


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
            }
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
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
            // Editar
            if (!dtg.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = isAdminLaboral
                    ? "✏️"
                    : "👁️"
                    ,
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard, // estilo estándar, sin colores
                    Width = 40,
                    MinimumWidth = 40,   // Evita que se haga más pequeño al redimensionar
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None // Mantiene el tamaño fijo
                };
                dtg.Columns.Add(btnEditar);
            }

            // Eliminar
            if (!dtg.Columns.Contains("Eliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Text = "🗑️", // Icono de basura
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard,
                    Width = 40,
                    MinimumWidth = 40,   // Evita que se haga más pequeño al redimensionar
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dtg.Columns.Add(btnEliminar);
            }

            // Mover los botones al final
            dtg.Columns["Editar"].DisplayIndex = dtg.ColumnCount - 2;
            dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 1;
        }

        private void CrearBotonesAccionHistorial(DataGridView dtg)
        {
            VerificarTipoUsuario();
            if (isAdminLaboral)
            {
                // Editar
                if (!dtg.Columns.Contains("Editar"))
                {
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                    {
                        Name = "Editar",
                        HeaderText = "",
                        Text = "✏️", // Icono de lápiz
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Standard, // estilo estándar, sin colores
                        Width = 40,
                        MinimumWidth = 40,   // Evita que se haga más pequeño al redimensionar
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None // Mantiene el tamaño fijo
                    };
                    dtg.Columns.Add(btnEditar);
                }

                // Eliminar
                if (!dtg.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                    {
                        Name = "Eliminar",
                        HeaderText = "",
                        Text = "🗑️", // Icono de basura
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Standard,
                        Width = 40,
                        MinimumWidth = 40,   // Evita que se haga más pequeño al redimensionar
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };
                    dtg.Columns.Add(btnEliminar);
                }

                // Mover los botones al final
                dtg.Columns["Editar"].DisplayIndex = dtg.ColumnCount - 2;
                dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 1;
            }

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
            lblTitulo.Text = "Nuevo Caso Laboral";
            LimpiarFormulario();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }

        private async Task CargarAlertas()
        {

            int idUsuario = UserSession.Id;
            var response = await alertasModel.ObtenerAlertasUsuarioPaginadas(idUsuario, 0, paginaActual, registrosPorPagina, false, "");

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsAlertas.DataSource = response.Tabla;
                dtgAlertas.Refresh();
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarCasosPorUsuario()
        {
            var resp = await userModel.ObtenerTotalesDashboardUsuario(UserSession.Id);

            if (resp.success && resp.data != null)
            {
                lblLaborales.Text = resp.data.laborales.ToString();
                lblCiviles.Text = resp.data.civiles.ToString();
                lblContenciosos.Text = resp.data.contenciosos_administrativos.ToString();
                lblConstitucionales.Text = resp.data.constitucionales.ToString();
                //lblTotalCasos.Text = resp.data.total_casos.ToString();
            }
            else
            {
                MessageBox.Show(resp.message ?? "No se pudieron obtener los totales");
            }
        }

        private async Task CargarPlazos()
        {

            int idUsuario = UserSession.Id;
            var response = await plazosModel.ObtenerPlazos(idUsuario, paginaActual, registrosPorPagina, null, "");

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsPlazos.DataSource = response.data;
                dtgPlazos.Refresh();
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void FrmDashboard_Load(object sender, EventArgs e)
        {
            if (!_yaCargo)
                await LoadAsync();

            if (!this.IsHandleCreated || this.IsDisposed) return;


            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
        }


        private void FrmDashboard_Resize_1(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void AjustarLayoutPorResolucion()
        {
            if (flowLayoutPanel2.Controls.Count == 0) return;

            int w = flowLayoutPanel2.ClientSize.Width;
            if (w <= 50) return;

            int padding = flowLayoutPanel2.Padding.Left + flowLayoutPanel1.Padding.Right;

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

        private async void btnIrAAlertas_Click(object sender, EventArgs e)
        {
            var menuPrincipal = Application.OpenForms.OfType<MenuPrincipal>().FirstOrDefault();
            if (menuPrincipal != null)
            {
                await menuPrincipal.AbrirAlertas();
            }
        }

        private async void btnIrAPlazos_Click(object sender, EventArgs e)
        {
            var menuPrincipal = Application.OpenForms.OfType<MenuPrincipal>().FirstOrDefault();
            if (menuPrincipal != null)
            {
                await menuPrincipal.AbrirPlazos();
            }
        }

        private void dtgAlertas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgAlertas.Columns["id"] != null)
            {
                dtgAlertas.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgAlertas.Columns["caso_id"] != null)
            {
                dtgAlertas.Columns["caso_id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgAlertas.Columns["historial_id"] != null)
            {
                dtgAlertas.Columns["historial_id"].Visible = false;
            }

            if (dtgAlertas.Columns["modulo_id"] != null)
            {
                dtgAlertas.Columns["modulo_id"].Visible = false;
            }


            if (dtgAlertas.Columns["mensaje"] != null)
            {
                dtgAlertas.Columns["mensaje"].Visible = false;
            }

            //CrearBotonesAccion(dtgAlertas);
            dtgAlertas.ClearSelection();
        }

        private void dtgPlazos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgPlazos.Columns["id"] != null)
            {
                dtgPlazos.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgPlazos.Columns["caso_id"] != null)
            {
                dtgPlazos.Columns["caso_id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgPlazos.Columns["historial_id"] != null)
            {
                dtgPlazos.Columns["historial_id"].Visible = false;
            }

            if (dtgPlazos.Columns["modulo_id"] != null)
            {
                dtgPlazos.Columns["modulo_id"].Visible = false;
            }


            if (dtgPlazos.Columns["mensaje"] != null)
            {
                dtgPlazos.Columns["mensaje"].Visible = false;
            }

            dtgPlazos.ClearSelection();
        }

        private void dtgAlertas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgAlertas.Columns[e.ColumnIndex].Name == "leida" && e.Value != null)
            {
                string valor = e.Value.ToString();

                if (valor == "1")
                    e.Value = "Sí";
                else if (valor == "0")
                    e.Value = "No";

                e.FormattingApplied = true;
            }
        }

        private void Listar_Click(object sender, EventArgs e)
        {

        }

        private void lblLaborales_MouseEnter(object sender, EventArgs e)
        {
            lblLaborales.BackColor = hover;
            btnLaborales.BackColor = hover;
        }

        private void lblLaborales_MouseLeave(object sender, EventArgs e)
        {
            lblLaborales.BackColor = normal;
            btnLaborales.BackColor = normal;
        }

        private void roundedButton37_MouseEnter(object sender, EventArgs e)
        {
            lblLaborales.BackColor = hover;
        }

        private void roundedButton37_MouseLeave(object sender, EventArgs e)
        {
            lblLaborales.BackColor = normal;
        }

        private void btnCiviles_MouseEnter(object sender, EventArgs e)
        {
            lblCiviles.BackColor = hover;
        }

        private void btnCiviles_MouseLeave(object sender, EventArgs e)
        {
            lblCiviles.BackColor = normal;
        }

        private void btnContenciosos_MouseEnter(object sender, EventArgs e)
        {
            lblContenciosos.BackColor = hover;
        }

        private void btnContenciosos_MouseLeave(object sender, EventArgs e)
        {
            lblContenciosos.BackColor = normal;
        }

        private void btnConstitucionales_MouseEnter(object sender, EventArgs e)
        {
            lblConstitucionales.BackColor = hover;
        }

        private void btnConstitucionales_MouseLeave(object sender, EventArgs e)
        {
            lblConstitucionales.BackColor = normal;
        }

        private void lblCiviles_MouseEnter(object sender, EventArgs e)
        {
            btnCiviles.BackColor = hover;
            lblCiviles.BackColor = hover;
        }

        private void lblCiviles_MouseLeave(object sender, EventArgs e)
        {
            btnCiviles.BackColor = normal;
            lblCiviles.BackColor = normal;
        }

        private void lblContenciosos_MouseEnter(object sender, EventArgs e)
        {
            lblContenciosos.BackColor = hover;
            btnContenciosos.BackColor = hover;
        }

        private void lblContenciosos_MouseLeave(object sender, EventArgs e)
        {
            lblContenciosos.BackColor = normal;
            btnContenciosos.BackColor = normal;
        }

        private void lblConstitucionales_MouseEnter(object sender, EventArgs e)
        {
            lblConstitucionales.BackColor = hover;
            btnConstitucionales.BackColor = hover;
        }

        private void lblConstitucionales_MouseLeave(object sender, EventArgs e)
        {
            lblConstitucionales.BackColor = normal;
            btnConstitucionales.BackColor = normal;
        }
    }
}
