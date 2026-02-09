
using Comun.Models;
using Dominio.Entidades;
using System.Data;

namespace Presentacion.Personas
{
    public partial class TercerosInteresados : Form
    {

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsTercerosInteresados = new BindingSource();
        private int _idTerceroInteresadoEditar;
        TerceroInteresadoModel terceroInteresadoModel= new TerceroInteresadoModel();

        public TercerosInteresados()
        {
            InitializeComponent();
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

        
        private async Task CargarDatosPersona(int idPersona)
        {
            var persona=await terceroInteresadoModel.ObtenerDetallesTerceroInteresadoPorId(idPersona);

            if (persona.success && persona.data != null)
            {
                txtNombre.Text = persona.data.nombre.ToString();
                txtDireccion.Text = persona.data.direccion.ToString();
                txtTelefono.Text = persona.data.telefono ?? "";
                txtCorreo.Text = persona.data.correo ?? "";


                var datosAbogado = persona.data.abogado;

                if (datosAbogado != null)
                {
                    txtNombreA.Text = datosAbogado.nombre ?? "";
                    txtCorreoA.Text = datosAbogado.correo ?? "";
                    txtTelefonoA.Text = datosAbogado.telefono ?? "";
                }
                else
                {
                    txtNombreA.Text = "";
                    txtCorreoA.Text = "";
                    txtTelefonoA.Text = "";
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
                panelBusqueda.Location = new Point(0, 0); // o donde quieras
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


        
        private async Task CargarTercerosInteresados()
        {
            
            var response = await terceroInteresadoModel.ObtenerTerceroInteresado(paginaActual, registrosPorPagina);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsTercerosInteresados.DataSource = response.data;
                dtgTercerosInteresados.Refresh();
                // Actualizar paginación
                totalRegistros = response.totalRegistros;
                labelTotal.Text = $"Total de Terceros Interesados: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void TercerosInteresados_Load(object sender, EventArgs e)
        {

            // Asignar BindingSource al DataGridView
            dtgTercerosInteresados.DataSource = bsTercerosInteresados;

            // Cargar Demandados
            await CargarTercerosInteresados();

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
                await CargarTercerosInteresados();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarTercerosInteresados();
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

        private void TercerosInteresados_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async Task Filtrar()
        {
            
            string filtro = txtBuscar.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await terceroInteresadoModel.ObtenerTercerosInteresadosFiltrados( pagina, registrosPorPagina, filtro);

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
                txtNombre.Text,
                txtDireccion.Text, 
                txtCorreo.Text,
                txtTelefono.Text,
                txtNombreA.Text,
                txtCorreoA.Text,
                txtTelefonoA.Text
            );

            if (resultado.success)
            {
                MessageBox.Show("Datos del tercero interesado actualizados correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarTercerosInteresados();
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
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;
            string telefono = txtTelefono.Text;
            string correo= txtCorreo.Text;
            string nombreA= txtNombreA.Text;
            string telefonoA= txtTelefonoA.Text;
            string correoA=txtCorreoA.Text;

           
            var resultado = await terceroInteresadoModel.CrearTerceroInteresado(nombre, direccion, correo,telefono, nombreA, telefonoA, correoA);

            if (resultado.success)
            {
                MessageBox.Show("Tercero interesado creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarTercerosInteresados();
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
                int idTerceroInteresado= Convert.ToInt32(dtgTercerosInteresados.Rows[e.RowIndex].Cells["id"].Value);
                string? terceroInteresado = Convert.ToString(dtgTercerosInteresados.Rows[e.RowIndex].Cells["nombre"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que deseas eliminar a la persona "+terceroInteresado+"?",
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
                        await CargarTercerosInteresados();
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
                _idTerceroInteresadoEditar= idPersona;
                await CargarDatosPersona(idPersona);
            }

        }

        private void Detalles_Click(object sender, EventArgs e)
        {

        }

        private void dtgPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
