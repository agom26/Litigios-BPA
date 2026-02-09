
using Comun.Models;
using Dominio.Entidades;
using System.Data;

namespace Presentacion.Personas
{
    public partial class Demandantes : Form
    {

        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsDemandantes = new BindingSource();
        private int _idDemandanteEditar;
        DemandanteModel demandanteModel= new DemandanteModel();

        public Demandantes()
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
            var persona=await demandanteModel.ObtenerDetallesDemandantePorId(idPersona);

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
                dtgDemandantes.ClearSelection();
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
            lblTitulo.Text = "Nuevo Demandante / Actor";
            btnGuardarUsuario.Text = "Guardar";
            LimpiarFormulario();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }


        
        private async Task CargarDemandantes()
        {
            
            var response = await demandanteModel.ObtenerDemanante(paginaActual, registrosPorPagina);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsDemandantes.DataSource = response.data;
                dtgDemandantes.Refresh();
                // Actualizar paginación
                totalRegistros = response.totalRegistros;
                labelTotal.Text = $"Total de Demandantes: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void Demandantes_Load(object sender, EventArgs e)
        {

            // Asignar BindingSource al DataGridView
            dtgDemandantes.DataSource = bsDemandantes;

            // Cargar Demandados
            await CargarDemandantes();

            dtgDemandantes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dtgDemandantes.Columns["Editar"].Width = 40;
            dtgDemandantes.Columns["Eliminar"].Width = 40;

            EliminarTabPage(Detalles);

        }

        private void dtgDemandantes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgDemandantes.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgDemandantes.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
            }
        }



        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await CargarDemandantes();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarDemandantes();
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

            CrearBotonesAccion(dtgDemandantes);
            dtgDemandantes.ClearSelection();
        }

        private void Demandantes_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async Task Filtrar()
        {
            
            string filtro = txtBuscar.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await demandanteModel.ObtenerDemandantesFiltrados( pagina, registrosPorPagina, filtro);

            if (resultado.success)
            {
                bsDemandantes.DataSource = resultado.data;  
                dtgDemandantes.Refresh();
                labelTotal.Text = $"Total de Demandantes: {resultado.totalRegistros}";
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

        
        private async Task ActualizarDemandante()
        {
            var resultado = await demandanteModel.EditarDemandante(
                _idDemandanteEditar,
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
                MessageBox.Show("Datos del demandante actualizados correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDemandantes();
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

           
            var resultado = await demandanteModel.CrearDemandante(nombre, direccion, correo,telefono, nombreA, telefonoA, correoA);

            if (resultado.success)
            {
                MessageBox.Show("Demandante creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDemandantes();
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
            if (lblTitulo.Text == "Nuevo Demandante / Actor")
            {
                await GuardarDemandante();
            }
            else
            {
                await ActualizarDemandante();
            }
        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(Listar);
            EliminarTabPage(Detalles);
        }

        
        private async void dtgDemandantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0) return;

            if (dtgDemandantes.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idDemandante= Convert.ToInt32(dtgDemandantes.Rows[e.RowIndex].Cells["id"].Value);
                string? demandante = Convert.ToString(dtgDemandantes.Rows[e.RowIndex].Cells["nombre"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que deseas eliminar a la persona "+demandante+"?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var resultado = await demandanteModel.EliminarDemandante(idDemandante);

                    if (resultado.success)
                    {
                        MessageBox.Show("Demandante eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarDemandantes();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + resultado.message 
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (dtgDemandantes.Columns[e.ColumnIndex].Name == "Editar")
            {
                btnGuardarUsuario.Text = "Actualizar";
                lblTitulo.Text = "Editar Demandante / Actor";
                int idPersona = Convert.ToInt32(dtgDemandantes.Rows[e.RowIndex].Cells["id"].Value);
                _idDemandanteEditar= idPersona;
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
