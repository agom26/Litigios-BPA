
using Comun.Models;
using Dominio.Entidades;
using System.Data;

namespace Presentacion.Personas
{
    public partial class Usuarios : Form
    {

        private UserModel userModel = new UserModel();
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalRegistros = 0;
        private BindingSource bsUsuarios = new BindingSource();
        private int _idUsuarioEditar;


        public Usuarios()
        {
            InitializeComponent();
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtUsuario.Text = "";
            txtPass.Text = "";
            txtConfirmarPass.Text = "";
            _idUsuarioEditar = 0;
        }

        private string ObtenerPermisosSeleccionados()
        {
            var lista = new List<object>();

            foreach (DataGridViewRow row in dtgPermisos.Rows)
            {
                bool seleccionado = Convert.ToBoolean(row.Cells["Seleccionar"].Value);
                if (!seleccionado) continue;

                int idModulo = Convert.ToInt32(row.Cells["id_modulo"].Value); // columna oculta con id_modulo
                int idRol = Convert.ToInt32(row.Cells["Rol"].Value);

                lista.Add(new { id_modulo = idModulo, id_rol = idRol });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(lista);
        }

        private async Task CargarModulosRoles()
        {
            
            var resultado = await userModel.ObtenerModulosRoles();

            if (!resultado.success)
            {
                MessageBox.Show("Error al cargar módulos y roles: " + resultado.message);
                return;
            }

            dtgPermisos.Columns.Clear();
            dtgPermisos.AutoGenerateColumns = false;

            // ✅ Checkbox
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "Seleccionar",
                HeaderText = "Seleccionar"
            };
            dtgPermisos.Columns.Add(chkCol);

            // ✅ Módulo (nombre visible)
            var moduloCol = new DataGridViewTextBoxColumn
            {
                Name = "Modulo",
                HeaderText = "Módulo",
                ReadOnly = true
            };
            dtgPermisos.Columns.Add(moduloCol);

            // ✅ Id del módulo (oculto, para enviar al backend)
            var idModuloCol = new DataGridViewTextBoxColumn
            {
                Name = "id_modulo",
                Visible = false
            };
            dtgPermisos.Columns.Add(idModuloCol);

            // ✅ ComboBox Rol
            var rolCol = new DataGridViewComboBoxColumn
            {
                Name = "Rol",
                HeaderText = "Rol",
                DisplayMember = "nombre_rol",
                ValueMember = "id",
                DataSource = resultado.roles, // Todos los roles
                FlatStyle = FlatStyle.Flat,   // Estilo moderno
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };
            dtgPermisos.Columns.Add(rolCol);

            // Ajustamos altura de filas
            dtgPermisos.RowTemplate.Height = 30;
            
            // 🔹 Llenamos filas con los módulos
            foreach (var modulo in resultado.modulos)
            {
                // por defecto checkbox desmarcado y rol null
                dtgPermisos.Rows.Add(false, modulo.nombre_modulo, modulo.id, null);
            }
        }



        private async Task CargarPermisosUsuario(int idUsuario)
        {
            var usuario=await userModel.ObtenerUsuarioPorId(idUsuario);

            txtNombre.Text = usuario.data.nombres.ToString();
            txtApellido.Text=usuario.data.apellidos.ToString();
            txtCorreo.Text= usuario.data.correo.ToString();
            txtUsuario.Text=usuario.data.usuario.ToString();
            if(usuario.data.telefono != "0")
            {

                txtTelefono.Text = usuario.data.telefono.ToString();
            }
            else
            {

                txtTelefono.Text = "";
            }

            await CargarModulosRoles();
           
            var permisosResponse = usuario.data.permisos;

            foreach (DataGridViewRow row in dtgPermisos.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["id_modulo"].Value);

                var permiso = permisosResponse.FirstOrDefault(p => p.id_modulo == idModulo);

                var comboCell = row.Cells["Rol"] as DataGridViewComboBoxCell;

                if (permiso != null)
                {
                    row.Cells["Seleccionar"].Value = true;

                    // Solo asignar si existe el rol en el ComboBox
                    if (comboCell != null)
                    {
                        var roles = comboCell.DataSource as List<RolResponse>;
                        if (roles != null && roles.Any(r => r.id == permiso.id_rol))
                            comboCell.Value = permiso.id_rol;
                        else
                            comboCell.Value = null;
                    }
                }
                else
                {
                    row.Cells["Seleccionar"].Value = false;
                    if (comboCell != null)
                        comboCell.Value = null;
                }
            }

            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }





        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
                dtgUsuarios.ClearSelection();
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

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Nuevo Usuario";
            lblPassword.Text = "Contraseña *";
            lblConfirmarPassword.Text = "Confirmar Contraseña *";
            LimpiarFormulario();
            await CargarModulosRoles();
            AnadirTabPage(Detalles);
            EliminarTabPage(Listar);
        }


        private async Task CargarUsuarios()
        {
            
            var response = await userModel.ObtenerUsuarios(paginaActual, registrosPorPagina);

            if (response.success)
            {
                // Asignar los datos al BindingSource
                bsUsuarios.DataSource = response.data;
                dtgUsuarios.Refresh();
                // Actualizar paginación
                totalRegistros = response.totalRegistros;
                labelTotal.Text = $"Total de usuarios: {totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void Usuarios_Load(object sender, EventArgs e)
        {

            // Asignar BindingSource al DataGridView
            dtgUsuarios.DataSource = bsUsuarios;

            // Cargar usuarios
            await CargarUsuarios();
            // Crear botones de acción solo una vez
            CrearBotonesAccion(dtgUsuarios);
            // Ajustar columnas automáticamente
            dtgUsuarios.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dtgUsuarios.Columns["Editar"].Width = 40;
            dtgUsuarios.Columns["Eliminar"].Width = 40;

            EliminarTabPage(Detalles);

        }

        private void dtgUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgUsuarios.Columns[e.ColumnIndex].Name == "Nombre" && e.Value != null)
            {
                string nombres = e.Value.ToString();
                string[] partes = nombres.Split(' ');
                string iniciales = string.Join("", partes.Select(p => p[0])).ToUpper();
                // Puedes agregarlo como tooltip o columna extra
                dtgUsuarios.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = iniciales;
            }
        }



        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual * registrosPorPagina < totalRegistros)
            {
                paginaActual++;
                await CargarUsuarios();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarUsuarios();
            }
        }

        private void dtgUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Oculta la columna 'id'
            if (dtgUsuarios.Columns["id"] != null)
            {
                dtgUsuarios.Columns["id"].Visible = false;
            }

            // Oculta la columna 'id'
            if (dtgUsuarios.Columns["id_rol"] != null)
            {
                dtgUsuarios.Columns["id_rol"].Visible = false;
            }

            dtgUsuarios.ClearSelection();
        }

        private void Usuarios_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async Task Filtrar()
        {
            /*
            string filtro = txtBuscar.Text;
            int pagina = 1;
            int registrosPorPagina = 10;

            var resultado = await _userModel.ObtenerUsuariosFiltrados(filtro, pagina, registrosPorPagina);

            if (resultado.success)
            {
                dtgUsuarios.DataSource = resultado.data;
                labelTotal.Text = $"Total de usuarios: {resultado.totalRegistros}";
                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalRegistros / registrosPorPagina)}";
            }
            else
            {
                MessageBox.Show(resultado.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // Detecta la tecla Enter
            {
                e.SuppressKeyPress = true; // Evita el sonido de beep
                await Filtrar();   // Simula click en el botón login
                CrearBotonesAccion(dtgUsuarios);
            }
        }
        private async Task ActualizarUsuario()
        {
            string permisosJson = ObtenerPermisosSeleccionados();
            string passwordFinal = null;

            // 🔐 Solo si está marcado
            if (checkBoxCambiarPass.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPass.Text) && string.IsNullOrWhiteSpace(txtConfirmarPass.Text))
                {
                    MessageBox.Show("Debe escribir la nueva contraseña");
                    return;
                }

                if (txtPass.Text != txtConfirmarPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                    return;
                }

                passwordFinal = txtPass.Text; // Dominio la va a hashear
            }

            
            var resultado = await userModel.EditarUsuario(
                _idUsuarioEditar,
                txtUsuario.Text,
                passwordFinal, // vacío = no cambia
                txtNombre.Text,
                txtApellido.Text,
                txtCorreo.Text,
                txtTelefono.Text,
                permisosJson
            );

            if (resultado.success)
            {
                MessageBox.Show("Usuario actualizado correctamente");
                await CargarUsuarios();
                LimpiarFormulario();
                AnadirTabPage(Listar);
                EliminarTabPage(Detalles);
            }
            else
            {
                MessageBox.Show("Error: " + resultado.message);
            }
        }

        

        private async Task GuardarUsuario()
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtPass.Text;
            string nombres = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string correo = txtCorreo.Text;
            string telefono= txtTelefono.Text;

            string permisosJson = ObtenerPermisosSeleccionados();

           
            UserModel userModel = new UserModel();
            var resultado = await userModel.CrearUsuario(usuario, contrasena, nombres, apellidos, correo, telefono, permisosJson);

            if (resultado.success)
            {
                MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarUsuarios();
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
            if (lblTitulo.Text == "Nuevo Usuario")
            {
                await GuardarUsuario();
            }
            else
            {
                await ActualizarUsuario();
            }

        }

        private void roundedButton19_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            AnadirTabPage(Listar);
            EliminarTabPage(Detalles);
        }

        private async void dtgUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dtgUsuarios.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idUsuario = Convert.ToInt32(dtgUsuarios.Rows[e.RowIndex].Cells["id"].Value);
                string? usuario = Convert.ToString(dtgUsuarios.Rows[e.RowIndex].Cells["usuario"].Value);
                var confirm = MessageBox.Show(
                    "¿Seguro que deseas eliminar al usuario "+usuario+"?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var resultado = await userModel.EliminarUsuario(idUsuario);

                    if (resultado.success)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");
                        await CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + resultado.message);
                    }
                }
            }

            if (dtgUsuarios.Columns[e.ColumnIndex].Name == "Editar")
            {
                lblTitulo.Text = "Editar Usuario";
                lblPassword.Text = "Contraseña";
                lblConfirmarPassword.Text = "Confirmar Contraseña";
                int idUsuario = Convert.ToInt32(dtgUsuarios.Rows[e.RowIndex].Cells["id"].Value);
                _idUsuarioEditar = idUsuario;
                await CargarPermisosUsuario(idUsuario);
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
