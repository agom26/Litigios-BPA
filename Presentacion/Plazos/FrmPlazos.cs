
using ClosedXML.Excel;
using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Dominio.Entidades;
using Dominio.Entidades.Plazos;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace Presentacion.Plazos
{
    public partial class FrmPlazos : Form
    {

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsPlazos = new BindingSource();
        private int _idPlazo;
        PlazosModel plazosModel = new PlazosModel();
        private string origenActual = "";
        private bool _cargando = false;

        private bool isAdminPlazos = false;
        private bool isLectorPlazos= false;
        UserModel userModel = new UserModel();

        private async Task VerificarPermisoPorRamaDelPlazo(int moduloId)
        {
            var resp = await userModel.ObtenerPermisoPorModulo(UserSession.Id, moduloId);

            isAdminPlazos = false;
            isLectorPlazos = true;

            if (resp.success && resp.data != null)
            {
                string rol = resp.data.nombre_rol;

                if (rol == "Administrador")
                {
                    isAdminPlazos = true;
                    isLectorPlazos = false;
                }
                else if (rol == "Usuario Normal")
                {
                    isAdminPlazos = false;
                    isLectorPlazos = false;
                }
                else if (rol == "Usuario Lector")
                {
                    isAdminPlazos = false;
                    isLectorPlazos = true;
                }
            }
        }

        public FrmPlazos()
        {
            InitializeComponent();
            EliminarTabPage(tabPageReportes);
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
                var centro = this.PointToScreen(System.Drawing.Point.Empty);

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

        private void LimpiarFormulario()
        {
            txtObservaciones.Text = "";
            comboboxEstado.SelectedIndex = -1;
            dateTimePickerFechaVencimiento.Value = DateTime.Now;
            dateTimePickerFechaEstado.Value = DateTime.Now;
        }

        public void CargarEstadosComboBoxSegunOrigen(string origen)
        {

            DeshabilitarFechas();

            switch (origen)
            {
                case "LABORAL PRIMER INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoLaboralHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "LABORAL SEGUNDA INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoLaboralHelper.ObtenerEstadosSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO ORAL PRIMER INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosOralPrimerInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO ORAL SEGUNDA INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosOralSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO SUMARIO PRIMER INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosJSPrimeraInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO SUMARIO SEGUNDA INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosJSSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPEViaApremio().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN COMÚN":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPEComun().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN SEGUNDA INSTANCIA":
                case "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO SEGUNDA INSTANCIA":
                case "CIVIL PROCESO DE EJECUCIÓN COMÚN SEGUNDA INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPESegundaInstancia().ToArray());
                        break;
                    }
                case "ADMINISTRATIVO GENERAL PRIMER INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "ADMINISTRATIVO TRIBUTARIO PRIMER INSTANCIA":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "RECURSO DE CASACIÓN":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosSegundaInstancia().ToArray());
                        break;
                    }
                case "CONSTITUCIONAL AMPARO":
                    {
                        comboboxEstado.Items.Clear();
                        comboboxEstado.Items.AddRange(EstadoConstitucionalHelper.ObtenerEstadosAmparo().ToArray());
                        break;
                    }
            }
        }
        private int ObtenerModuloIdPorOrigen(string origen)
        {
            if (origen.StartsWith("LABORAL"))
                return 1;

            if (origen.StartsWith("CIVIL"))
                return 2;

            if (origen.StartsWith("CONSTITUCIONAL"))
                return 3;

            if (origen.StartsWith("ADMINISTRATIVO") ||
                origen.StartsWith("RECURSO DE CASACIÓN"))
                return 4;

            return 0;
        }
        private async Task CargarDatosPlazo(int historialId)
        {

            int usuarioId = UserSession.Id;
            var plazo = await plazosModel.ObtenerPlazoPorId(usuarioId, historialId);

            if (plazo.success && plazo.data != null)
            {
                //fecha del estado
                if (plazo.data.fecha_inicio.HasValue)
                    dateTimePickerFechaEstado.Value = plazo.data.fecha_inicio.Value;
                origenActual = plazo.data.origen ?? "";
                int moduloId = ObtenerModuloIdPorOrigen(origenActual);

                if (moduloId == 0)
                {
                    MessageBox.Show(
                        "No se pudo determinar la rama del plazo.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                await VerificarPermisoPorRamaDelPlazo(moduloId);
                CargarEstadosComboBoxSegunOrigen(origenActual);
                
                txtObservaciones.Text = plazo.data.anotaciones;

                if (plazo.data.fecha_vencimiento.HasValue)
                {
                    var fechaVenc = plazo.data.fecha_vencimiento.Value;
                    // Fecha
                    dateTimePickerFechaVencimiento.Value = fechaVenc.Date;
                    // Hora (con minutos incluidos)
                    dateTimePickerHoraVencimiento.Value = fechaVenc;
                }
                else
                {
                    dateTimePickerFechaVencimiento.Value = DateTime.Now;
                    dateTimePickerHoraVencimiento.Value = DateTime.Now;
                }
                comboboxEstado.SelectedItem = plazo.data.estado;
                AnadirTabPage(Detalles);
                EliminarTabPage(Listar);
            }
            else
            {
                MessageBox.Show(plazo.message);
            }

            if (isLectorPlazos)
            {
                DeshabilitarControles();
            }
            else
            {
                HabilitarControles();
            }
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
                dtgPlazos.ClearSelection();
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
                    Text = isLectorPlazos
                    ? "👁️"
                    : "✏️"
                    ,
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Standard, // estilo estándar, sin colores
                    Width = 40,
                    MinimumWidth = 40,   // Evita que se haga más pequeño al redimensionar
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None // Mantiene el tamaño fijo
                };
                dtg.Columns.Add(btnEditar);
            }

            /*
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
            }*/

            // Mover los botones al final
            dtg.Columns["Editar"].DisplayIndex = dtg.ColumnCount - 1;
            //dtg.Columns["Eliminar"].DisplayIndex = dtg.ColumnCount - 1;
        }

        private void CentrarPanel()
        {
            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;
                panelBusqueda.Dock = DockStyle.Top;
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new System.Drawing.Point(0, 0); // o donde quieras
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Nuevo Tercero Interesado";
            btnActualizar.Text = "Guardar";
            LimpiarFormulario();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }

        private async Task CargarPlazos()
        {
            int usuarioId = UserSession.Id;
            string busqueda = txtBuscar.Text;
            string modulo = comboBoxRama.SelectedItem?.ToString() ?? "";
            int? moduloId = null;

            switch (modulo)
            {
                case "Laboral":
                    {
                        moduloId = 1;
                        break;
                    }
                case "Civil":
                    {
                        moduloId = 2;
                        break;
                    }
                case "Contencioso Administrativo":
                    {
                        moduloId = 4;
                        break;
                    }
                case "Constitucional":
                    {
                        moduloId = 3;
                        break;
                    }
                case "Todas las ramas":
                    {
                        moduloId = null;
                        break;
                    }

            }

            var response = await plazosModel.ObtenerPlazos(usuarioId, paginaActual, registrosPorPagina, moduloId, busqueda);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsPlazos.DataSource = response.data;
                dtgPlazos.Refresh();
                // Actualizar paginación
                totalRegistros = response.total;
                labelTotal.Text = $"Total de plazos: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmPlazos_Load(object sender, EventArgs e)
        {
            // Asignar BindingSource al DataGridView
            dtgPlazos.DataSource = bsPlazos;
            // Cargar Demandados
            comboBoxRama.SelectedIndex = 4;
            await CargarPlazos();

            if (dtgPlazos.Columns.Contains("Editar"))
            {
                dtgPlazos.Columns["Editar"].Width = 40;
            }
            
            EliminarTabPage(Detalles);

        }

        private void dtgTercerosInteresados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgPlazos.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgPlazos.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await CargarPlazos();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarPlazos();
            }
        }

        private void dtgTercerosInteresados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Ocultar columnas
            if (dtgPlazos.Columns["caso_id"] != null)
                dtgPlazos.Columns["caso_id"].Visible = false;

            if (dtgPlazos.Columns["historial_id"] != null)
                dtgPlazos.Columns["historial_id"].Visible = false;

            // 🔹 Hacer pequeñas las columnas
            if (dtgPlazos.Columns["oficial"] != null)
            {
                dtgPlazos.Columns["oficial"].Width = 60;
            }

            if (dtgPlazos.Columns["notificador"] != null)
            {
                dtgPlazos.Columns["notificador"].Width = 66;
            }

            if (dtgPlazos.Columns["nombre"] != null)
            {
                dtgPlazos.Columns["nombre"].Width = 150;
            }

            if (dtgPlazos.Columns["expediente"] != null)
            {
                dtgPlazos.Columns["expediente"].Width = 150;
            }

            if (dtgPlazos.Columns["tipo_instancia"] != null)
            {
                dtgPlazos.Columns["tipo_instancia"].Width = 90;
            }

            if (dtgPlazos.Columns["organo_judicial"] != null)
            {
                dtgPlazos.Columns["organo_judicial"].Width = 100;
            }

            if (dtgPlazos.Columns["estado"] != null)
            {
                dtgPlazos.Columns["estado"].Width = 250;
            }

            if (dtgPlazos.Columns["fecha_inicio"] != null)
            {
                dtgPlazos.Columns["fecha_inicio"].Width = 100;
            }

            if (dtgPlazos.Columns["fecha_vencimiento"] != null)
            {
                dtgPlazos.Columns["fecha_vencimiento"].Width = 180;
            }

            if (dtgPlazos.Columns["rama"] != null)
            {
                dtgPlazos.Columns["rama"].Width = 100;
            }

            if (dtgPlazos.Columns["expediente"] != null)
            {
                dtgPlazos.Columns["expediente"].HeaderText = "Expediente";
            }

            if (dtgPlazos.Columns["nombre"] != null)
            {
                dtgPlazos.Columns["nombre"].HeaderText = "Nombre";
            }

            if (dtgPlazos.Columns["tipo_instancia"] != null)
            {
                dtgPlazos.Columns["tipo_instancia"].HeaderText = " Tipo Instancia";
            }

            if (dtgPlazos.Columns["organo_judicial"] != null)
            {
                dtgPlazos.Columns["organo_judicial"].HeaderText = "Órgano Judicial";
            }

            if (dtgPlazos.Columns["oficial"] != null)
            {
                dtgPlazos.Columns["oficial"].HeaderText = "Oficial";
            }

            if (dtgPlazos.Columns["notificador"] != null)
            {
                dtgPlazos.Columns["notificador"].HeaderText = "Notificador";
            }

            if (dtgPlazos.Columns["estado"] != null)
            {
                dtgPlazos.Columns["estado"].HeaderText = "Estado";
            }

            if (dtgPlazos.Columns["fecha_inicio"] != null)
            {
                dtgPlazos.Columns["fecha_inicio"].HeaderText = "Fecha Inicio";
                dtgPlazos.Columns["fecha_inicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dtgPlazos.Columns["fecha_vencimiento"] != null)
            {
                dtgPlazos.Columns["fecha_vencimiento"].HeaderText = "Fecha Vencimiento";
                dtgPlazos.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dtgPlazos.Columns["rama"] != null)
            {
                dtgPlazos.Columns["rama"].HeaderText = "Rama";
            }

            CrearBotonesAccion(dtgPlazos);
            dtgPlazos.ClearSelection();
        }

        private void FrmPlazos_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // Detecta la tecla Enter
            {
                e.SuppressKeyPress = true; // Evita el sonido de beep
                await CargarPlazos();
            }
        }

        private string GenerarFormatoBase()
        {
            string fechaEstado = dateTimePickerFechaEstado.Value
                .ToString("dd/MM/yyyy");

            string estado = comboboxEstado.Text;

            string observacion = $"{fechaEstado} {estado}";

            if (checkBoxTieneVencimiento.Checked)
            {
                DateTime fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;

                string fechaVencimientoTexto =
                    fechaVencimiento.ToString("dd/MM/yyyy HH:mm");

                observacion += $" | Fecha de vencimiento: {fechaVencimientoTexto}";
            }

            return observacion;
        }

        private void DeshabilitarControles()
        {
            dateTimePickerFechaVencimiento.Enabled = false;
            dateTimePickerFechaEstado.Enabled = false;
            dateTimePickerHoraVencimiento.Enabled = false;

            dateTimePickerFecha1.Enabled = false;
            dateTimePickerFecha2.Enabled = false;
            dateTimePickerFecha3.Enabled = false;

            comboboxEstado.Enabled = false;
            txtObservaciones.Enabled = false;

            btnActualizar.Enabled = false;
            btnActualizar.Visible = false;
        }

        private void HabilitarControles()
        {
            dateTimePickerFechaVencimiento.Enabled = true;
            dateTimePickerFechaEstado.Enabled = true;
            dateTimePickerHoraVencimiento.Enabled = true;
            dateTimePickerFecha1.Enabled = true;
            dateTimePickerFecha2.Enabled = true;
            dateTimePickerFecha3.Enabled = true;

            comboboxEstado.Enabled = true;
            txtObservaciones.Enabled = true;
            btnActualizar.Enabled = true;
            btnActualizar.Visible = true;

        }

        private async Task ActualizarPlazo()
        {
           

            string formatoCorrecto = GenerarFormatoBase();
            string textoUsuario = txtObservaciones.Text;
            int usuarioId = UserSession.Id;
            DateTime fechaEstado = dateTimePickerFechaEstado.Value;
            DateTime? fechaVencimiento = dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
            string estado = comboboxEstado.Text;
            string anotaciones = txtObservaciones.Text;

            // Si el usuario borró todo o dañó el formato
            if (!textoUsuario.StartsWith(dateTimePickerFechaEstado.Value.ToString("dd/MM/yyyy")))
            {
                // Reconstruimos el formato y agregamos lo que el usuario escribió
                if (!string.IsNullOrWhiteSpace(textoUsuario))
                    formatoCorrecto += " " + textoUsuario;

                VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(estado);
            }
            else
            {
                formatoCorrecto = textoUsuario;
            }

            if (checkBoxTieneVencimiento.Checked)
            {
                fechaVencimiento = dateTimePickerFechaVencimiento.Value+
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
            }
            else
            {
                fechaVencimiento = null;
            }

            var resultado = await plazosModel.EditarPlazo(usuarioId, _idPlazo, fechaEstado, estado, anotaciones, fechaVencimiento);



            if (resultado.success)
            {
                MessageBox.Show("Datos del plazo actualizados correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarPlazos();
                LimpiarFormulario();
                AnadirTabPage(Listar);
                EliminarTabPage(Detalles);
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void roundedButton18_Click(object sender, EventArgs e)
        {
            await ActualizarPlazo();

        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(Listar);
            EliminarTabPage(Detalles);
        }

        
        private async void dtgTercerosInteresados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgPlazos.Columns[e.ColumnIndex].Name == "Editar")
            {
                btnActualizar.Text = "Actualizar";
                lblTitulo.Text = "Editar Plazo";
                int historialId = Convert.ToInt32(dtgPlazos.Rows[e.RowIndex].Cells["historial_id"].Value);
               
               
                _idPlazo = historialId;
                await CargarDatosPlazo(historialId);
            }
        }

        private async void comboBoxRama_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarPlazos();
        }

        public void DeshabilitarFechas()
        {
            dateTimePickerFecha1.Visible = false;
            dateTimePickerFecha2.Visible = false;
            dateTimePickerFecha3.Visible = false;
            labelFecha1.Visible = false;
            labelFecha2.Visible = false;
            labelFecha3.Visible = false;
        }

        private void VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(string origen)
        {
            if (comboboxEstado.SelectedItem == null)
                return;
            DateTime fecha = dateTimePickerFechaEstado.Value;
            DateTime fechaVencimiento = dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
            string estado = comboboxEstado.Text;
            bool requiereVencimiento = false;


            switch (origen)
            {
                case "LABORAL PRIMER INSTANCIA":
                    {
                        requiereVencimiento = EstadoLaboralHelper.RequiereVencimientoPrimeraInstancia(estado);
                        txtObservaciones.Text =
                            EstadoLaboralHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
                case "LABORAL SEGUNDA INSTANCIA":
                    {
                        requiereVencimiento = EstadoLaboralHelper.RequiereVencimientoSegundaInstancia(estado);
                        txtObservaciones.Text =
                        EstadoLaboralHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
                case "CIVIL JUICIO ORAL PRIMER INSTANCIA":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoJOPI(estado);
                        txtObservaciones.Text =
                            EstadoCivilHelper.GenerarObservacionConFechas(fecha, estado, requiereVencimiento, fechaVencimiento, dateTimePickerFecha1, dateTimePickerFecha2, dateTimePickerFecha3, labelFecha1, labelFecha2, labelFecha3);
                        break;
                    }
                case "CIVIL JUICIO ORAL SEGUNDA INSTANCIA":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoJOSI(estado);
                        txtObservaciones.Text =
                            EstadoCivilHelper.GenerarObservacionConFechas(fecha, estado, requiereVencimiento, fechaVencimiento, dateTimePickerFecha1, dateTimePickerFecha2, dateTimePickerFecha3, labelFecha1, labelFecha2, labelFecha3);
                        break;
                    }
                case "CIVIL JUICIO SUMARIO PRIMER INSTANCIA":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoJSPrimeraInstancia(estado);
                        txtObservaciones.Text =
                            EstadoCivilHelper.GenerarObservacionConFechas(fecha, estado, requiereVencimiento, fechaVencimiento, dateTimePickerFecha1, dateTimePickerFecha2, dateTimePickerFecha3, labelFecha1, labelFecha2, labelFecha3);
                        break;
                    }
                case "CIVIL JUICIO SUMARIO SEGUNDA INSTANCIA":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoJSSegundaInstancia(estado);
                        txtObservaciones.Text =
                            EstadoCivilHelper.GenerarObservacionConFechas(fecha, estado, requiereVencimiento, fechaVencimiento, dateTimePickerFecha1, dateTimePickerFecha2, dateTimePickerFecha3, labelFecha1, labelFecha2, labelFecha3);
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoPEViaApremio(estado);
                        txtObservaciones.Text =
                            EstadoCivilHelper.GenerarObservacionConFechas(fecha, estado, requiereVencimiento, fechaVencimiento, dateTimePickerFecha1, dateTimePickerFecha2, dateTimePickerFecha3, labelFecha1, labelFecha2, labelFecha3);
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN COMÚN":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoPEComun(estado);
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN SEGUNDA INSTANCIA":
                    {
                        requiereVencimiento = EstadoCivilHelper.RequiereVencimientoPESegundaInstancia(estado);
                        break;
                    }
                case "ADMINISTRATIVO GENERAL PRIMER INSTANCIA":
                    {
                        requiereVencimiento = EstadoContenciosoHelper.RequiereVencimientoPrimeraInstancia(estado);
                        txtObservaciones.Text =
                            EstadoContenciosoHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
                case "ADMINISTRATIVO TRIBUTARIO PRIMER INSTANCIA":
                    {
                        requiereVencimiento = EstadoContenciosoHelper.RequiereVencimientoPrimeraInstancia(estado);
                        txtObservaciones.Text =
                            EstadoContenciosoHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
                case "RECURSO DE CASACIÓN":
                    {
                        requiereVencimiento = EstadoContenciosoHelper.RequiereVencimientoSegundaInstancia(estado);
                        txtObservaciones.Text =
                            EstadoContenciosoHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
                case "CONSTITUCIONAL AMPARO":
                    {
                        requiereVencimiento = EstadoConstitucionalHelper.RequiereVencimiento(estado);
                        txtObservaciones.Text =
                            EstadoConstitucionalHelper.GenerarObservacion(fecha, estado, requiereVencimiento, fechaVencimiento);
                        break;
                    }
            }

            checkBoxTieneVencimiento.Checked = requiereVencimiento;
            dateTimePickerFechaVencimiento.Enabled = requiereVencimiento;
            dateTimePickerHoraVencimiento.Enabled = requiereVencimiento;
        }

        private void comboboxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerFechaEstado_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void comboboxEstado_SelectedValueChanged_1(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerFecha1_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerFecha2_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerFecha3_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private void dateTimePickerHoraVencimiento_ValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomaticoYActualizarObservaciones(origenActual);
        }

        private async void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            int? moduloId = null;
            string? rama = comboBoxRamaReporte.Text;
            string? origen = comboBoxOrigen.Text;
            string? expediente = txtExpediente.Text;
            string? nombre = txtNombre.Text;
            string? tipoInstancia = comboBoxTipoInstancia.Text;
            string? organoJudicial = comboBoxOrganoJudicial.Text;
            string? oficial = txtOficial.Text;
            string? notificador = txtNotificador.Text;
            string? estado = comboBoxEstadoReporte.Text;
            DateTime? fechaIngresoInicio = dateTimePickerFechaIngresoInicio.Value;
            DateTime? fechaIngresoFin = dateTimePickerFechaIngresoFin.Value;
            DateTime? fechaVencimientoInicio = dateTimePickerFechaVencimientoInicio.Value;
            DateTime? fechaVencimientoFin = dateTimePickerFechaVencimientoFin.Value;


            if (checkBoxRama.Checked)
            {
                switch (rama)
                {
                    case "Laboral":
                        {
                            moduloId = 1;
                            break;
                        }
                    case "Civil":
                        {
                            moduloId = 2;
                            break;
                        }
                    case "Contencioso":
                        {
                            moduloId = 4;
                            break;
                        }
                    case "Constitucional":
                        {
                            moduloId = 3;
                            break;
                        }

                    default:
                        {
                            moduloId = null;
                            break;
                        }
                }
            }
            else
            {
                moduloId = null;
            }


            //asignar valores


            //expediente
            if (checkBoxExpediente.Checked)
            {
                expediente = txtExpediente.Text;
            }
            else
            {
                expediente = null;
            }

            //nombre 
            if (checkBoxNombre.Checked)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                nombre = null;
            }

            //tipo de instancia
            if (checkBoxTipoInstancia.Checked)
            {
                tipoInstancia = comboBoxTipoInstancia.SelectedItem?.ToString().ToLower();
            }
            else
            {
                tipoInstancia = null;
            }

            //organo judicial 
            if (checkBoxOrganoJudicial.Checked)
            {
                organoJudicial = comboBoxOrganoJudicial.Text;
            }
            else
            {
                organoJudicial = null;
            }

            //oficial
            if (checkBoxOficial.Checked)
            {
                oficial = txtOficial.Text;
            }
            else
            {
                oficial = null;
            }

            //notificador
            if (checkBoxNotificador.Checked)
            {
                notificador = txtNotificador.Text;
            }
            else
            {
                notificador = null;
            }

            //origen 
            if (checkBoxOrigen.Checked)
            {
                origen = comboBoxOrigen.Text;
            }
            else
            {
                origen = null;
            }

            //estado
            if (checkBoxEstado.Checked)
            {
                estado = comboBoxEstadoReporte.Text;
            }
            else
            {
                estado = null;
            }

            //fechas de ingreso
            if (checkBoxFechaIngreso.Checked)
            {
                fechaIngresoInicio = dateTimePickerFechaIngresoInicio.Value;
                fechaIngresoFin = dateTimePickerFechaIngresoFin.Value;
            }
            else
            {
                fechaIngresoInicio = null;
                fechaIngresoFin = null;
            }

            //fechas de vencimiento
            if (checkBoxFechaVencimiento.Checked)
            {
                fechaVencimientoInicio = dateTimePickerFechaVencimientoInicio.Value;
                fechaVencimientoFin = dateTimePickerFechaVencimientoFin.Value;
            }
            else
            {
                fechaVencimientoInicio = null;
                fechaVencimientoFin = null;
            }
            int usuarioId = UserSession.Id;
            var response = await plazosModel.GenerarReportePlazos(usuarioId, moduloId, expediente, nombre, oficial, notificador, tipoInstancia, organoJudicial, estado, origen, fechaIngresoInicio, fechaIngresoFin, fechaVencimientoInicio, fechaVencimientoFin);

            if (response.success)
            {
                labelTotalReporte.Text = $"Total de resultados: {response.total}";
                dtgReportesResultado.DataSource = response.data;
                dtgReportesResultado.Refresh();
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void ExportarReportePlazosAExcel(DataTable dataTable, string titulo)
        {
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

                    // =========================
                    // 🔹 TÍTULO Y FECHA
                    // =========================
                    ws.Cell("E2").Value = titulo;
                    ws.Cell("E2").Style.Font.Bold = true;
                    ws.Cell("E2").Style.Font.FontSize = 16;
                    ws.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    ws.Cell("E3").Value = $"Generado el {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:HH:mm}";
                    ws.Cell("E3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // =========================
                    // 🔹 LOGO
                    // =========================
                    if (File.Exists(tempLogoPath))
                    {
                        ws.AddPicture(tempLogoPath)
                          .MoveTo(ws.Cell("A2"))
                          .Scale(0.15);
                    }

                    // =========================
                    // 🔹 ORDEN DE COLUMNAS
                    // =========================
                    string[] columnasOrdenadas =
                    {
                "expediente",
                "nombre",
                "rama",
                "subrama",
                "organo_judicial",
                "oficial",
                "notificador",
                "estado",
                "fecha_inicio",
                "fecha_vencimiento",
                "anotaciones"
            };

                    int startRow = 6;
                    int colIndex = 1;

                    // =========================
                    // 🔹 HEADERS
                    // =========================
                    foreach (var col in columnasOrdenadas)
                    {
                        ws.Cell(startRow, colIndex).Value = col.Replace("_", " ").ToUpper();

                        var cell = ws.Cell(startRow, colIndex);
                        cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#274e77");
                        cell.Style.Font.FontColor = XLColor.White;
                        cell.Style.Font.Bold = true;
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        colIndex++;
                    }

                    // =========================
                    // 🔹 FILAS
                    // =========================
                    int rowIndex = startRow + 1;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        colIndex = 1;

                        foreach (var col in columnasOrdenadas)
                        {
                            object value = dataTable.Columns.Contains(col) ? row[col] : "";

                            // 🔥 FORMATO FECHAS
                            if (col == "fecha_inicio" || col == "fecha_vencimiento")
                            {
                                if (value != null && DateTime.TryParse(value.ToString(), out DateTime fecha))
                                {
                                    value = fecha.ToString("dd/MM/yyyy");
                                }
                            }

                            ws.Cell(rowIndex, colIndex).Value = value?.ToString();

                            colIndex++;
                        }

                        rowIndex++;
                    }

                    // =========================
                    // 🔹 CONVERTIR A TABLA EXCEL
                    // =========================
                    var rangoTabla = ws.Range(startRow, 1, rowIndex - 1, columnasOrdenadas.Length);
                    var tabla = rangoTabla.CreateTable();

                    tabla.Theme = XLTableTheme.TableStyleMedium2; // estilo bonito
                    tabla.ShowAutoFilter = true;

                    // =========================
                    // 🔹 AJUSTES VISUALES
                    // =========================
                    ws.Columns().AdjustToContents();

                    // Bordes
                    var rango = ws.Range(startRow, 1, rowIndex - 1, columnasOrdenadas.Length);
                    rango.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rango.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // =========================
                    // 🔹 GUARDAR
                    // =========================
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
        private async Task CrearPdfReportePlazos(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el PDF");
                return;
            }

            // =========================
            // 🔹 CONFIGURAR CHROME
            // =========================
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

            // =========================
            // 🔹 LOGO BASE64
            // =========================
            string base64Logo;
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.logoBPA2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                base64Logo = Convert.ToBase64String(ms.ToArray());
            }

            string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' style='width:150px;' />";

            // =========================
            // 🔹 ORDEN DE COLUMNAS
            // =========================
            string[] columnasOrdenadas = {
                "expediente",
                "nombre",
                "rama",
                "subrama",
                "organo_judicial",
                "oficial",
                "notificador",
                "estado",
                "fecha_inicio",
                "fecha_vencimiento",
                "anotaciones"
            };

            // =========================
            // 🔹 HEADERS
            // =========================
            string headers = "";
            foreach (var col in columnasOrdenadas)
            {
                headers += $"<th>{col.Replace("_", " ").ToUpper()}</th>";
            }

            // =========================
            // 🔹 FILAS
            // =========================
            string rows = "";

            foreach (DataRow row in dt.Rows)
            {
                rows += "<tr>";

                foreach (var col in columnasOrdenadas)
                {
                    object value = dt.Columns.Contains(col) ? row[col] : "";


                    if (col == "fecha_inicio")
                    {
                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime fecha))
                        {
                            value = fecha.ToString("dd/MM/yyyy");
                        }
                    }

                    if (col == "fecha_vencimiento")
                    {
                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime fecha))
                        {
                            value = fecha.ToString("dd/MM/yyyy HH:mm");
                        }
                    }

                    rows += $"<td>{value}</td>";
                }

                rows += "</tr>";
            }

            // =========================
            // 🔹 HTML COMPLETO
            // =========================
            string html = $@"
                <html>
                <head>
                <style>
                    body {{
                        font-family: Arial;
                        font-size: 11px;
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
                    }}

                    tr:nth-child(even) {{
                        background-color: #f9f9f9
                    }}

                    tr:hover {{
                        background-color: #ddd;
                    }}

                    @page {{
                        size: legal landscape;
                        margin: 15mm;
                    }}
                </style>
                </head>

                <body>

                <div class='header'>REPORTE DE PLAZOS</div>
                <div class='subheader'>Fecha: &nbsp;&nbsp; Hora: {DateTime.Now:HH:mm} </div>

                <div class='logo'>
                    {imageHtml}
                </div>

                <table>
                    <thead>
                        <tr>
                            {headers}
                        </tr>
                    </thead>
                    <tbody>
                        {rows}
                    </tbody>
                </table>

                </body>
                </html>";

            // =========================
            // 🔹 GUARDAR PDF
            // =========================
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = "Reporte_Plagos_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".pdf"
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
                Scale = 0.7m
            });

            await browser.CloseAsync();

            MessageBox.Show("PDF generado correctamente");
            Process.Start("explorer.exe", Path.GetDirectoryName(saveFileDialog.FileName));
        }

        private void comboBoxRamaReporte_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxRamaReporte.SelectedItem)
            {
                case "Laboral":
                    {
                        comboBoxOrigen.Items.Clear();
                        comboBoxOrigen.Items.AddRange(new string[] { "LABORAL PRIMERA INSTANCIA", "LABORAL SEGUNDA INSTANCIA" });
                        break;
                    }
                case "Civil":
                    {
                        comboBoxOrigen.Items.Clear();
                        comboBoxOrigen.Items.AddRange(new string[] { "CIVIL JUICIO ORAL PRIMER INSTANCIA", "CIVIL JUICIO ORAL SEGUNDA INSTANCIA", "CIVIL JUICIO SUMARIO PRIMER INSTANCIA", "CIVIL JUICIO SUMARIO SEGUNDA INSTANCIA", "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO", "CIVIL PROCESO DE EJECUCIÓN COMÚN", "CIVIL PROCESO DE EJECUCIÓN SEGUNDA INSTANCIA" });
                        break;
                    }
                case "Contencioso":
                    {
                        comboBoxOrigen.Items.Clear();
                        comboBoxOrigen.Items.AddRange(new string[] { "ADMINISTRATIVO GENERAL PRIMER INSTANCIA", "ADMINISTRATIVO TRIBUTARIO PRIMER INSTANCIA", "RECURSO DE CASACIÓN" });
                        break;
                    }
                case "Constitucional":
                    {
                        comboBoxOrigen.Items.Clear();
                        comboBoxOrigen.Items.AddRange(new string[] { "CONSTITUCIONAL AMPARO" });
                        break;
                    }
                case "Todas":
                    {
                        comboBoxOrigen.Items.Clear();
                        comboBoxOrigen.Items.Add("Todos");
                        comboBoxOrigen.SelectedIndex = 0; // 🔥 dispara el evento

                        break;
                    }
                default:
                    {
                        comboBoxOrigen.Items.Clear();
                        break;
                    }
            }
        }

        private void comboBoxOrigen_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxOrigen.SelectedItem)
            {
                case "LABORAL PRIMERA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoLaboralHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "LABORAL SEGUNDA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoLaboralHelper.ObtenerEstadosSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO ORAL PRIMER INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosOralPrimerInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO ORAL SEGUNDA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosOralSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO SUMARIO PRIMER INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosJSPrimeraInstancia().ToArray());
                        break;
                    }
                case "CIVIL JUICIO SUMARIO SEGUNDA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosJSSegundaInstancia().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN VÍA APREMIO":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPEViaApremio().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN COMÚN":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPEComun().ToArray());
                        break;
                    }
                case "CIVIL PROCESO DE EJECUCIÓN SEGUNDA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoCivilHelper.ObtenerEstadosPESegundaInstancia().ToArray());
                        break;
                    }
                case "ADMINISTRATIVO GENERAL PRIMERA INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "ADMINISTRATIVO TRIBUTARIO PRIMER INSTANCIA":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia().ToArray());
                        break;
                    }
                case "RECURSO DE CASACIÓN":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoContenciosoHelper.ObtenerEstadosSegundaInstancia().ToArray());
                        break;
                    }
                case "CONSTITUCIONAL AMPARO":
                    {
                        comboBoxEstadoReporte.Items.Clear();
                        comboBoxEstadoReporte.Items.AddRange(EstadoConstitucionalHelper.ObtenerEstadosAmparo().ToArray());
                        break;
                    }
                case "Todos":
                    {
                        comboBoxEstadoReporte.Items.Clear();

                        var todosEstados = new HashSet<string>();

                        // LABORAL
                        todosEstados.UnionWith(EstadoLaboralHelper.ObtenerEstadosPrimeraInstancia());
                        todosEstados.UnionWith(EstadoLaboralHelper.ObtenerEstadosSegundaInstancia());

                        // CIVIL
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosOralPrimerInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosOralSegundaInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosJSPrimeraInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosJSSegundaInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPEViaApremio());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPEComun());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPESegundaInstancia());

                        // CONTENCIOSO
                        todosEstados.UnionWith(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia());
                        todosEstados.UnionWith(EstadoContenciosoHelper.ObtenerEstadosSegundaInstancia());

                        // CONSTITUCIONAL
                        todosEstados.UnionWith(EstadoConstitucionalHelper.ObtenerEstadosAmparo());

                        comboBoxEstadoReporte.Items.AddRange(todosEstados.ToArray());

                        break;
                    }
                default:
                    {
                        comboBoxEstadoReporte.Items.Clear();

                        var todosEstados = new HashSet<string>();

                        // LABORAL
                        todosEstados.UnionWith(EstadoLaboralHelper.ObtenerEstadosPrimeraInstancia());
                        todosEstados.UnionWith(EstadoLaboralHelper.ObtenerEstadosSegundaInstancia());

                        // CIVIL
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosOralPrimerInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosOralSegundaInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosJSPrimeraInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosJSSegundaInstancia());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPEViaApremio());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPEComun());
                        todosEstados.UnionWith(EstadoCivilHelper.ObtenerEstadosPESegundaInstancia());

                        // CONTENCIOSO
                        todosEstados.UnionWith(EstadoContenciosoHelper.ObtenerEstadosPrimeraInstancia());
                        todosEstados.UnionWith(EstadoContenciosoHelper.ObtenerEstadosSegundaInstancia());

                        // CONSTITUCIONAL
                        todosEstados.UnionWith(EstadoConstitucionalHelper.ObtenerEstadosAmparo());

                        comboBoxEstadoReporte.Items.AddRange(todosEstados.ToArray());

                        break;
                    }
            }
        }

        private async void btnGenerarPDF_Click(object sender, EventArgs e)
        {

            if (dtgReportesResultado.DataSource == null)
            {
                MessageBox.Show("No hay datos", "Advertencia");
                return;
            }

            DataTable dt;

            if (dtgReportesResultado.DataSource is DataTable tabla)
            {
                dt = tabla;
            }
            else
            {
                var lista = dtgReportesResultado.DataSource as IEnumerable<object>;

                if (lista == null)
                {
                    MessageBox.Show("Formato de datos no válido", "Error");
                    return;
                }

                dt = ConvertirListaADataTable(lista);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros para generar el PDF", "Advertencia");
                return;
            }

            await EjecutarConLoaderAsync(async () =>
            {
                await CrearPdfReportePlazos(dt);
            });

        }

        public DataTable ConvertirListaADataTable(IEnumerable<object> lista)
        {
            DataTable dt = new DataTable();

            var propiedades = lista.First().GetType().GetProperties();

            foreach (var prop in propiedades)
            {
                dt.Columns.Add(prop.Name);
            }

            foreach (var item in lista)
            {
                var valores = propiedades.Select(p => p.GetValue(item, null)).ToArray();
                dt.Rows.Add(valores);
            }

            return dt;
        }

        private async void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            if (dtgReportesResultado.DataSource == null)
            {
                MessageBox.Show("No hay datos", "Advertencia");
                return;
            }

            DataTable dt;

            if (dtgReportesResultado.DataSource is DataTable tabla)
            {
                dt = tabla;
            }
            else
            {
                var lista = dtgReportesResultado.DataSource as IEnumerable<object>;

                if (lista == null)
                {
                    MessageBox.Show("Formato de datos no válido", "Error");
                    return;
                }

                dt = ConvertirListaADataTable(lista);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros para generar el PDF", "Advertencia");
                return;
            }

            await EjecutarConLoaderAsync(async () =>
            {
                ExportarReportePlazosAExcel(dt, "REPORTE DE PLAZOS");
            });

        }

        private void roundedButton7_Click(object sender, EventArgs e)
        {
            AnadirTabPage(Listar);
            EliminarTabPage(tabPageReportes);
            dtgReportesResultado.DataSource = null;
            dtgReportesResultado.Refresh();
            labelTotalReporte.Text = "Total de resultados: 0";
        }

        private void roundedButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageReportes);
            labelTotalReporte.Text = "Total de resultados: 0";  
            EliminarTabPage(Listar);
        }
    }
}
