
using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Dominio.Entidades;
using Presentacion.Casos.Estados;
using Presentacion.Casos.Participantes;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Presentacion.Casos.Laborales
{
    public partial class Laboral_primer_instancia : Form
    {

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsTercerosInteresados = new BindingSource();
        private int _idTerceroInteresadoEditar;
        CasosLaboralesModel casoLaboralMode = new CasosLaboralesModel();
        TerceroInteresadoModel terceroInteresadoModel = new TerceroInteresadoModel();
        private BindingList<PersonaListDataResponse> listaDemandados
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaDemandantes
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaTercerosInteresados
        = new BindingList<PersonaListDataResponse>();
        private BindingList<PersonaListDataResponse> listaContactosEmpresa
        = new BindingList<PersonaListDataResponse>();
        public Laboral_primer_instancia()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtJuzgado.Text = "";
            comboboxOficial.Text = "";
            txtNombreParticular.Text = "";
            comboboxNotificador.Text = "";
            txtJuzgado.Text = "";
            txtNombreParticular.Text = "";
            LimpiarDemandados();
        }


        private async Task CargarDatosPersona(int idPersona)
        {
            var persona = await terceroInteresadoModel.ObtenerDetallesTerceroInteresadoPorId(idPersona);

            if (persona.success && persona.data != null)
            {
                txtExpediente.Text = persona.data.nombre.ToString();
                txtJuzgado.Text = persona.data.direccion.ToString();
                txtNombreParticular.Text = persona.data.telefono ?? "";
                comboboxOficial.Text = persona.data.correo ?? "";


                var datosAbogado = persona.data.abogado;

                if (datosAbogado != null)
                {
                    comboboxNotificador.Text = datosAbogado.nombre ?? "";
                    txtNombreParticular.Text = datosAbogado.correo ?? "";
                    txtJuzgado.Text = datosAbogado.telefono ?? "";
                }
                else
                {
                    comboboxNotificador.Text = "";
                    txtNombreParticular.Text = "";
                    txtJuzgado.Text = "";
                }

                AnadirTabPage(Detalles);
                EliminarTabPage(Listar);
            }
            else
            {
                MessageBox.Show(persona.message);
            }

        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
                dtgCasosLaborales.ClearSelection();
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
            lblTitulo.Text = "Nuevo Tercero Interesado";
            btnGuardarCaso.Text = "Guardar";
            LimpiarFormulario();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }



        private async Task CargarCasos()
        {

            int idUsuario = UserSession.Id;
            string filtro = txtBuscar.Text;
            var response = await casoLaboralMode.ObtenerCasosLaborales(idUsuario, paginaActual, registrosPorPagina, filtro);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsTercerosInteresados.DataSource = response.data;
                dtgCasosLaborales.Refresh();
                // Actualizar paginación
                totalRegistros = response.total;
                labelTotal.Text = $"Total de casos laborales: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private async void Laboral_primer_instancia_Load(object sender, EventArgs e)
        {

            // Asignar BindingSource al DataGridView
            dtgCasosLaborales.DataSource = bsTercerosInteresados;

            // Cargar Demandados
            await CargarCasos();

            dtgCasosLaborales.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dtgCasosLaborales.Columns["Editar"].Width = 40;
            dtgCasosLaborales.Columns["Eliminar"].Width = 40;

            EliminarTabPage(Detalles);

            alistarListaDemandantes();
            alistarListaDemandados();
            alistarListaTercerosInteresados();
            alistarListaContactosEmpresa();

            //prueba
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true; // para que aparezca scroll si se pasa del alto visible

            panelDemandados.AutoSize = true;
            panelDemandados.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelDemandantes.AutoSize = true;
            panelDemandantes.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelTercerosInteresados.AutoSize = true;
            panelTercerosInteresados.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            panelContactosEmpresas.AutoSize = true;
            panelContactosEmpresas.AutoSizeMode= AutoSizeMode.GrowAndShrink;

            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
        }


        private void dtgCasosLaborales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgCasosLaborales.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgCasosLaborales.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
            }
        }



        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await CargarCasos();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarCasos();
            }
        }

        private void dtgCasosLaborales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgCasosLaborales.Columns["id"] != null)
            {
                dtgCasosLaborales.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgCasosLaborales.Columns["id_rol"] != null)
            {
                dtgCasosLaborales.Columns["id_rol"].Visible = false;
            }

            CrearBotonesAccion(dtgCasosLaborales);
            dtgCasosLaborales.ClearSelection();
        }



        private async Task Filtrar()
        {

            string filtro = txtBuscar.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await terceroInteresadoModel.ObtenerTercerosInteresadosFiltrados(pagina, registrosPorPagina, filtro);

            if (resultado.success)
            {
                bsTercerosInteresados.DataSource = resultado.data;
                dtgCasosLaborales.Refresh();
                labelTotal.Text = $"Total de Terceros Interesados: {resultado.totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // Detecta la tecla Enter
            {
                e.SuppressKeyPress = true; // Evita el sonido de beep
                await Filtrar();   // Simula click en el botón login


            }
        }


        private async Task ActualizarTerceroInteresado()
        {
            var resultado = await terceroInteresadoModel.EditarTerceroInteresado(
                _idTerceroInteresadoEditar,
                txtExpediente.Text,
                txtJuzgado.Text,
                comboboxOficial.Text,
                txtNombreParticular.Text,
                txtJuzgado.Text,
                txtNombreParticular.Text,
                txtEstado.Text
            );

            if (resultado.success)
            {
                MessageBox.Show("Datos del tercero interesado actualizados correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarCasos();
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



        private async Task GuardarDemandante()
        {

            //campos 
            string expediente = txtExpediente.Text;
            string juzgado = txtJuzgado.Text;
            string oficial = comboboxOficial.Text;
            string notificador = comboboxNotificador.Text;
            string nombreParticular = txtNombreParticular.Text;

            //historial
            string estado = txtEstado.Text;
            DateTime fecha = DateTime.Now;
            DateTime fechaVencimiento = DateTime.Now;
            string observaciones = txtObservaciones.Text;


            //var resultado = await terceroInteresadoModel.CrearTerceroInteresado(nombre, direccion, correo, telefono, nombreA, telefonoA, correoA);
            /*
            if (resultado.success)
            {
                MessageBox.Show("Tercero interesado creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarCasos();
                LimpiarFormulario();
                AnadirTabPage(Listar);
                EliminarTabPage(Detalles);
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }



        private async void roundedButton18_Click(object sender, EventArgs e)
        {
            if (lblTitulo.Text == "Nuevo Tercero Interesado")
            {
                await GuardarDemandante();
            }
            else
            {
                await ActualizarTerceroInteresado();
            }
        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(Listar);
            EliminarTabPage(Detalles);
        }


        private async void dtgCasosLaborales_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            if (dtgCasosLaborales.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idTerceroInteresado = Convert.ToInt32(dtgCasosLaborales.Rows[e.RowIndex].Cells["id"].Value);
                string? terceroInteresado = Convert.ToString(dtgCasosLaborales.Rows[e.RowIndex].Cells["nombre"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que deseas eliminar a la persona " + terceroInteresado + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var resultado = await terceroInteresadoModel.EliminarTerceroInteresado(idTerceroInteresado);

                    if (resultado.success)
                    {
                        MessageBox.Show("Tercero interesado eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarCasos();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + resultado.message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (dtgCasosLaborales.Columns[e.ColumnIndex].Name == "Editar")
            {
                btnGuardarCaso.Text = "Actualizar";
                lblTitulo.Text = "Editar Tercero Interesado";
                int idPersona = Convert.ToInt32(dtgCasosLaborales.Rows[e.RowIndex].Cells["id"].Value);
                _idTerceroInteresadoEditar = idPersona;
                await CargarDatosPersona(idPersona);
            }

        }

        private void Detalles_Click(object sender, EventArgs e)
        {

        }

        private void dtgPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Laboral_primer_instancia_Resize_1(object sender, EventArgs e)
        {
            CentrarPanel();
            this.BeginInvoke(new Action(AjustarLayoutPorResolucion));
        }

        private void btnAgregarEstado_Click(object sender, EventArgs e)
        {
            FrmAgregarEstadoLaboralPI frmAgregarEstado = new FrmAgregarEstadoLaboralPI();
            frmAgregarEstado.ShowDialog();


            if (EstadoLaboral.estado != null)
            {
                txtEstado.Text = EstadoLaboral.estado.ToString();
                txtObservaciones.Text = EstadoLaboral.observaciones + "\n";
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
            }
        }

        private void btnAgregarDemandantes_Click(object sender, EventArgs e)
        {
            var frm = new FrmAgregarDemandante(listaDemandantes);

            frm.Show(); // ← NO modal
        }

        private void AjustarLayoutPorResolucion()
        {
            int w = flowLayoutPanel1.ClientSize.Width;
            if (w <= 50) return; // evita cálculos raros cuando aún no está dibujado

            int padding = flowLayoutPanel1.Padding.Left + flowLayoutPanel1.Padding.Right;

            bool pantallaGrande = w >= 1200;

            if (pantallaGrande)
            {
                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel1.WrapContents = true;

                int cols = 2;
                int anchoPanel = ((w - padding) / cols) - 12;

                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c is Panel p)
                    {
                        // NO tocamos AutoSize aquí
                        p.MinimumSize = new Size(anchoPanel, p.MinimumSize.Height);
                        p.MaximumSize = new Size(anchoPanel, 0); // 0 = sin límite de alto
                        p.Width = anchoPanel;
                    }
                }
            }
            else
            {
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel1.WrapContents = false;

                int anchoPanel = (w - padding) - 12;

                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c is Panel p)
                    {
                        // NO tocamos AutoSize aquí
                        p.MinimumSize = new Size(anchoPanel, p.MinimumSize.Height);
                        p.MaximumSize = new Size(anchoPanel, 0);
                        p.Width = anchoPanel;
                    }
                }
            }

            flowLayoutPanel1.PerformLayout();
        }
        private void AjustarAlturaDataGridViewDemandados()
        {
            dtgDemandados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandados.ColumnHeadersHeight;

            dtgDemandados.Height = alturaFilas + alturaHeaders + 22;

            // Opcional: evitar scroll interno del grid si quieres que siempre se vea todo
            dtgDemandados.ScrollBars = ScrollBars.None;

            // Fuerza a recalcular layouts del panel y del flow
            panelDemandados.PerformLayout();
            flowLayoutPanel1.PerformLayout();
        }
        private void AjustarAlturaDataGridViewDemandantes()
        {
            dtgDemandantes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgDemandantes.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgDemandantes.ColumnHeadersHeight;

            dtgDemandantes.Height = alturaFilas + alturaHeaders + 22;

            // Opcional: evitar scroll interno del grid si quieres que siempre se vea todo
            dtgDemandantes.ScrollBars = ScrollBars.None;

            // Fuerza a recalcular layouts del panel y del flow
            dtgDemandantes.PerformLayout();
            flowLayoutPanel1.PerformLayout();
        }

        private void AjustarAlturaDataGridViewTercerosInteresados()
        {
            dtgTercerosInteresados.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgTercerosInteresados.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgTercerosInteresados.ColumnHeadersHeight;

            dtgTercerosInteresados.Height = alturaFilas + alturaHeaders + 22;

            // Opcional: evitar scroll interno del grid si quieres que siempre se vea todo
            dtgTercerosInteresados.ScrollBars = ScrollBars.None;

            // Fuerza a recalcular layouts del panel y del flow
            dtgTercerosInteresados.PerformLayout();
            flowLayoutPanel1.PerformLayout();
        }

        private void AjustarAlturaDataGridViewContactosEmpresa()
        {
            dtgContactoEmpresa.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            int alturaFilas = dtgContactoEmpresa.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            int alturaHeaders = dtgContactoEmpresa.ColumnHeadersHeight;

            dtgContactoEmpresa.Height = alturaFilas + alturaHeaders + 22;

            // Opcional: evitar scroll interno del grid si quieres que siempre se vea todo
            dtgContactoEmpresa.ScrollBars = ScrollBars.None;

            // Fuerza a recalcular layouts del panel y del flow
            dtgContactoEmpresa.PerformLayout();
            flowLayoutPanel1.PerformLayout();
        }
        private void btnAgregarDemandados_Click(object sender, EventArgs e)
        {
            // Agregar demandado
            var frm = new FrmAgregarDemandado(listaDemandados);

            frm.Show(); // ← NO modal

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
                                                  // tu ListChanged ya llama AjustarAlturaDataGridViewDemandados()
                }
            }
        }
        private void LimpiarDemandados()
        {
            if (listaDemandados.Count == 0) return;

            listaDemandados.Clear(); // 🔥 limpia todo
        }
        private void dtgDemandados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgDemandados.Columns["id"] != null)
            {
                dtgDemandados.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
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
                                                   // tu ListChanged ya llama AjustarAlturaDataGridViewDemandados()
                }
            }
        }

        private void dtgDemandantes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgDemandantes.Columns["id"] != null)
            {
                dtgDemandantes.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgDemandantes.Columns["id_rol"] != null)
            {
                dtgDemandantes.Columns["id_rol"].Visible = false;
            }

            dtgDemandantes.ClearSelection();
        }

        private void btnAgregarPartesInteresadas_Click(object sender, EventArgs e)
        {
            // Agregar demandado
            var frm = new FrmAgregarTerceroInteresado(listaTercerosInteresados);

            frm.Show(); // ← NO modal
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
                                                           // tu ListChanged ya llama AjustarAlturaDataGridViewDemandados()
                }
            }
        }

        private void dtgPartesInteresadas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgTercerosInteresados.Columns["id"] != null)
            {
                dtgTercerosInteresados.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgTercerosInteresados.Columns["id_rol"] != null)
            {
                dtgTercerosInteresados.Columns["id_rol"].Visible = false;
            }

            dtgTercerosInteresados.ClearSelection();
        }

        private void btnAgregarContactoEmpresa_Click(object sender, EventArgs e)
        {
            // Agregar demandado
            var frm = new FrmAgregarContactoEmpresa(listaContactosEmpresa);

            frm.Show(); // ← NO modal
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
                                                        // tu ListChanged ya llama AjustarAlturaDataGridViewDemandados()
                }
            }
        }

        private void dtgContactoEmpresa_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgContactoEmpresa.Columns["id"] != null)
            {
                dtgContactoEmpresa.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgContactoEmpresa.Columns["id_rol"] != null)
            {
                dtgContactoEmpresa.Columns["id_rol"].Visible = false;
            }

            dtgContactoEmpresa.ClearSelection();
        }
    }
}
