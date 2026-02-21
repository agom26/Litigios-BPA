
using Comun;
using Comun.DatosParaInterfaz;
using Comun.Models;
using Dominio.Entidades;
using Presentacion.Casos.Estados;
using Presentacion.Casos.Participantes;
using System.Data;

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
                dtgTercerosInteresados.ClearSelection();
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
            btnGuardarUsuario.Text = "Guardar";
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
                dtgTercerosInteresados.Refresh();
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


        private async void Laboral_primer_instancia_Load(object sender, EventArgs e)
        {

            // Asignar BindingSource al DataGridView
            dtgTercerosInteresados.DataSource = bsTercerosInteresados;

            // Cargar Demandados
            await CargarCasos();

            dtgTercerosInteresados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dtgTercerosInteresados.Columns["Editar"].Width = 40;
            dtgTercerosInteresados.Columns["Eliminar"].Width = 40;

            EliminarTabPage(Detalles);

        }

        private void dtgTercerosInteresados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgTercerosInteresados.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgTercerosInteresados.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
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

        private void dtgTercerosInteresados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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

            CrearBotonesAccion(dtgTercerosInteresados);
            dtgTercerosInteresados.ClearSelection();
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
                dtgTercerosInteresados.Refresh();
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


        private async void dtgTercerosInteresados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            if (dtgTercerosInteresados.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idTerceroInteresado = Convert.ToInt32(dtgTercerosInteresados.Rows[e.RowIndex].Cells["id"].Value);
                string? terceroInteresado = Convert.ToString(dtgTercerosInteresados.Rows[e.RowIndex].Cells["nombre"].Value);
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

            if (dtgTercerosInteresados.Columns[e.ColumnIndex].Name == "Editar")
            {
                btnGuardarUsuario.Text = "Actualizar";
                lblTitulo.Text = "Editar Tercero Interesado";
                int idPersona = Convert.ToInt32(dtgTercerosInteresados.Rows[e.RowIndex].Cells["id"].Value);
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
            
        }

        private void btnAgregarDemandados_Click(object sender, EventArgs e)
        {
            FrmAgregarDemandado frmDemandado = new FrmAgregarDemandado();
            frmDemandado.ShowDialog();
        }
    }
}
