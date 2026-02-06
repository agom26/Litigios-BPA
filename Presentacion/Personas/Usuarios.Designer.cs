namespace Presentacion.Personas
{
    partial class Usuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuarios));
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            panelBusqueda = new Panel();
            btnAdd = new Presentacion.Clases.RoundedButton();
            txtBuscar = new TextBox();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            label1 = new Label();
            dtgUsuarios = new DataGridView();
            lblPagina = new Label();
            btnAnterior = new Presentacion.Clases.RoundedButton();
            btnSiguiente = new Presentacion.Clases.RoundedButton();
            tabControl1 = new TabControl();
            Listar = new TabPage();
            labelTotal = new Label();
            Detalles = new TabPage();
            roundedButton19 = new Presentacion.Clases.RoundedButton();
            btnGuardarUsuario = new Presentacion.Clases.RoundedButton();
            panel1 = new Panel();
            dtgPermisos = new DataGridView();
            roundedButton10 = new Presentacion.Clases.RoundedButton();
            roundedButton11 = new Presentacion.Clases.RoundedButton();
            panelInformacionPersonal = new Panel();
            txtPass = new TextBox();
            lblNombre = new Label();
            lblApellido = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            txtNombre = new TextBox();
            checkBoxCambiarPass = new CheckBox();
            txtConfirmarPass = new TextBox();
            lblConfirmarPassword = new Label();
            label2 = new Label();
            lblPassword = new Label();
            txtCorreo = new TextBox();
            roundedButton9 = new Presentacion.Clases.RoundedButton();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            txtApellido = new TextBox();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton6 = new Presentacion.Clases.RoundedButton();
            roundedButton8 = new Presentacion.Clases.RoundedButton();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            lblTitulo = new Label();
            label5 = new Label();
            txtTelefono = new TextBox();
            roundedButton12 = new Presentacion.Clases.RoundedButton();
            panelBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).BeginInit();
            tabControl1.SuspendLayout();
            Listar.SuspendLayout();
            Detalles.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPermisos).BeginInit();
            panelInformacionPersonal.SuspendLayout();
            SuspendLayout();
            // 
            // panelBusqueda
            // 
            panelBusqueda.BackColor = Color.White;
            panelBusqueda.Controls.Add(btnAdd);
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(roundedButton1);
            panelBusqueda.Controls.Add(label1);
            panelBusqueda.Location = new Point(3, 3);
            panelBusqueda.MinimumSize = new Size(800, 100);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(800, 100);
            panelBusqueda.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.FromArgb(194, 160, 91);
            btnAdd.BackgroundColor = Color.FromArgb(194, 160, 91);
            btnAdd.BorderColor = Color.Empty;
            btnAdd.BorderRadius = 10;
            btnAdd.BorderSize = 1;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.Location = new Point(602, 24);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(3, 0, 0, 0);
            btnAdd.Size = new Size(150, 40);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Agregar Usuario";
            btnAdd.TextColor = Color.White;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.None;
            txtBuscar.Location = new Point(414, 38);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar usuarios ...";
            txtBuscar.Size = new Size(136, 16);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedButton1.BackColor = Color.White;
            roundedButton1.BackgroundColor = Color.White;
            roundedButton1.BorderColor = Color.LightGray;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 1;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Image = (Image)resources.GetObject("roundedButton1.Image");
            roundedButton1.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton1.Location = new Point(382, 25);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(182, 40);
            roundedButton1.TabIndex = 1;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(47, 25);
            label1.Name = "label1";
            label1.Size = new Size(97, 30);
            label1.TabIndex = 0;
            label1.Text = "Usuarios";
            // 
            // dtgUsuarios
            // 
            dtgUsuarios.AllowUserToAddRows = false;
            dtgUsuarios.AllowUserToDeleteRows = false;
            dtgUsuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(249, 247, 242);
            dtgUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            dtgUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgUsuarios.BackgroundColor = Color.White;
            dtgUsuarios.BorderStyle = BorderStyle.None;
            dtgUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dtgUsuarios.ColumnHeadersHeight = 40;
            dtgUsuarios.EnableHeadersVisualStyles = false;
            dtgUsuarios.Location = new Point(36, 161);
            dtgUsuarios.MinimumSize = new Size(719, 226);
            dtgUsuarios.MultiSelect = false;
            dtgUsuarios.Name = "dtgUsuarios";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = Color.White;
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle11.SelectionForeColor = Color.Black;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dtgUsuarios.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = Color.White;
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle12.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            dtgUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle12;
            dtgUsuarios.RowTemplate.Height = 45;
            dtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgUsuarios.Size = new Size(744, 229);
            dtgUsuarios.TabIndex = 1;
            dtgUsuarios.CellClick += dtgUsuarios_CellClick;
            dtgUsuarios.CellFormatting += dtgUsuarios_CellFormatting;
            dtgUsuarios.DataBindingComplete += dtgUsuarios_DataBindingComplete;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(693, 132);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(38, 15);
            lblPagina.TabIndex = 2;
            lblPagina.Text = "label2";
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAnterior.BackColor = Color.FromArgb(194, 160, 91);
            btnAnterior.BackgroundColor = Color.FromArgb(194, 160, 91);
            btnAnterior.BorderColor = Color.Empty;
            btnAnterior.BorderRadius = 10;
            btnAnterior.BorderSize = 1;
            btnAnterior.FlatAppearance.BorderSize = 0;
            btnAnterior.FlatStyle = FlatStyle.Flat;
            btnAnterior.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.ForeColor = Color.White;
            btnAnterior.Image = (Image)resources.GetObject("btnAnterior.Image");
            btnAnterior.ImageAlign = ContentAlignment.MiddleLeft;
            btnAnterior.Location = new Point(587, 407);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Padding = new Padding(3, 0, 0, 0);
            btnAnterior.Size = new Size(87, 40);
            btnAnterior.TabIndex = 3;
            btnAnterior.Text = "Anterior";
            btnAnterior.TextAlign = ContentAlignment.MiddleRight;
            btnAnterior.TextColor = Color.White;
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSiguiente.BackColor = Color.FromArgb(194, 160, 91);
            btnSiguiente.BackgroundColor = Color.FromArgb(194, 160, 91);
            btnSiguiente.BorderColor = Color.Empty;
            btnSiguiente.BorderRadius = 10;
            btnSiguiente.BorderSize = 1;
            btnSiguiente.FlatAppearance.BorderSize = 0;
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.Image = (Image)resources.GetObject("btnSiguiente.Image");
            btnSiguiente.ImageAlign = ContentAlignment.MiddleRight;
            btnSiguiente.Location = new Point(693, 407);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Padding = new Padding(3, 0, 0, 0);
            btnSiguiente.Size = new Size(87, 40);
            btnSiguiente.TabIndex = 4;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.TextAlign = ContentAlignment.MiddleLeft;
            btnSiguiente.TextColor = Color.White;
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Listar);
            tabControl1.Controls.Add(Detalles);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(807, 560);
            tabControl1.TabIndex = 5;
            // 
            // Listar
            // 
            Listar.AutoScroll = true;
            Listar.AutoScrollMinSize = new Size(800, 506);
            Listar.Controls.Add(labelTotal);
            Listar.Controls.Add(panelBusqueda);
            Listar.Controls.Add(btnSiguiente);
            Listar.Controls.Add(lblPagina);
            Listar.Controls.Add(btnAnterior);
            Listar.Controls.Add(dtgUsuarios);
            Listar.Location = new Point(4, 24);
            Listar.Name = "Listar";
            Listar.Padding = new Padding(3);
            Listar.Size = new Size(799, 532);
            Listar.TabIndex = 0;
            Listar.UseVisualStyleBackColor = true;
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Location = new Point(36, 132);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(38, 15);
            labelTotal.TabIndex = 5;
            labelTotal.Text = "label2";
            // 
            // Detalles
            // 
            Detalles.AutoScroll = true;
            Detalles.AutoScrollMinSize = new Size(799, 532);
            Detalles.BackColor = Color.FromArgb(250, 249, 246);
            Detalles.Controls.Add(roundedButton19);
            Detalles.Controls.Add(btnGuardarUsuario);
            Detalles.Controls.Add(panel1);
            Detalles.Controls.Add(panelInformacionPersonal);
            Detalles.Controls.Add(lblTitulo);
            Detalles.Font = new Font("Microsoft Sans Serif", 8.25F);
            Detalles.Location = new Point(4, 24);
            Detalles.Name = "Detalles";
            Detalles.Padding = new Padding(3);
            Detalles.Size = new Size(799, 532);
            Detalles.TabIndex = 1;
            Detalles.Click += Detalles_Click;
            // 
            // roundedButton19
            // 
            roundedButton19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedButton19.BackColor = Color.FromArgb(250, 249, 246);
            roundedButton19.BackgroundColor = Color.FromArgb(250, 249, 246);
            roundedButton19.BorderColor = Color.Silver;
            roundedButton19.BorderRadius = 10;
            roundedButton19.BorderSize = 1;
            roundedButton19.FlatAppearance.BorderSize = 0;
            roundedButton19.FlatStyle = FlatStyle.Flat;
            roundedButton19.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundedButton19.ForeColor = Color.Black;
            roundedButton19.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton19.Location = new Point(433, 800);
            roundedButton19.Name = "roundedButton19";
            roundedButton19.Padding = new Padding(3, 0, 0, 0);
            roundedButton19.Size = new Size(150, 40);
            roundedButton19.TabIndex = 6;
            roundedButton19.Text = "Cancelar";
            roundedButton19.TextColor = Color.Black;
            roundedButton19.UseVisualStyleBackColor = false;
            roundedButton19.Click += roundedButton19_Click;
            // 
            // btnGuardarUsuario
            // 
            btnGuardarUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardarUsuario.BackColor = Color.FromArgb(194, 160, 91);
            btnGuardarUsuario.BackgroundColor = Color.FromArgb(194, 160, 91);
            btnGuardarUsuario.BorderColor = Color.Empty;
            btnGuardarUsuario.BorderRadius = 10;
            btnGuardarUsuario.BorderSize = 1;
            btnGuardarUsuario.FlatAppearance.BorderSize = 0;
            btnGuardarUsuario.FlatStyle = FlatStyle.Flat;
            btnGuardarUsuario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarUsuario.ForeColor = Color.White;
            btnGuardarUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarUsuario.Location = new Point(616, 800);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Padding = new Padding(3, 0, 0, 0);
            btnGuardarUsuario.Size = new Size(150, 40);
            btnGuardarUsuario.TabIndex = 5;
            btnGuardarUsuario.Text = "Guardar Usuario";
            btnGuardarUsuario.TextColor = Color.White;
            btnGuardarUsuario.UseVisualStyleBackColor = false;
            btnGuardarUsuario.Click += roundedButton18_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 249, 246);
            panel1.Controls.Add(dtgPermisos);
            panel1.Controls.Add(roundedButton10);
            panel1.Controls.Add(roundedButton11);
            panel1.Location = new Point(52, 489);
            panel1.Name = "panel1";
            panel1.Size = new Size(714, 288);
            panel1.TabIndex = 4;
            // 
            // dtgPermisos
            // 
            dtgPermisos.AllowUserToAddRows = false;
            dtgPermisos.AllowUserToDeleteRows = false;
            dtgPermisos.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = Color.FromArgb(249, 247, 242);
            dtgPermisos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            dtgPermisos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgPermisos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPermisos.BackgroundColor = Color.White;
            dtgPermisos.BorderStyle = BorderStyle.None;
            dtgPermisos.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgPermisos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle14.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle14.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle14.SelectionForeColor = Color.Black;
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.True;
            dtgPermisos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dtgPermisos.ColumnHeadersHeight = 40;
            dtgPermisos.EnableHeadersVisualStyles = false;
            dtgPermisos.Location = new Point(3, 62);
            dtgPermisos.MinimumSize = new Size(619, 226);
            dtgPermisos.MultiSelect = false;
            dtgPermisos.Name = "dtgPermisos";
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = Color.White;
            dataGridViewCellStyle15.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle15.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle15.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle15.SelectionForeColor = Color.Black;
            dataGridViewCellStyle15.WrapMode = DataGridViewTriState.True;
            dtgPermisos.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dtgPermisos.RowHeadersVisible = false;
            dataGridViewCellStyle16.BackColor = Color.White;
            dataGridViewCellStyle16.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle16.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle16.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle16.SelectionForeColor = Color.Black;
            dtgPermisos.RowsDefaultCellStyle = dataGridViewCellStyle16;
            dtgPermisos.RowTemplate.Height = 45;
            dtgPermisos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgPermisos.Size = new Size(708, 226);
            dtgPermisos.TabIndex = 5;
            dtgPermisos.CellContentClick += dtgPermisos_CellContentClick;
            // 
            // roundedButton10
            // 
            roundedButton10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton10.BackColor = Color.FromArgb(249, 247, 242);
            roundedButton10.BackgroundColor = Color.FromArgb(249, 247, 242);
            roundedButton10.BorderColor = Color.Empty;
            roundedButton10.BorderRadius = 40;
            roundedButton10.BorderSize = 0;
            roundedButton10.Enabled = false;
            roundedButton10.FlatAppearance.BorderSize = 0;
            roundedButton10.FlatStyle = FlatStyle.Flat;
            roundedButton10.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton10.ForeColor = Color.Black;
            roundedButton10.Location = new Point(0, 0);
            roundedButton10.Name = "roundedButton10";
            roundedButton10.Size = new Size(731, 40);
            roundedButton10.TabIndex = 4;
            roundedButton10.Text = "Permisos de Módulos";
            roundedButton10.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton10.TextColor = Color.Black;
            roundedButton10.UseVisualStyleBackColor = false;
            // 
            // roundedButton11
            // 
            roundedButton11.BackColor = Color.White;
            roundedButton11.BackgroundColor = Color.White;
            roundedButton11.BorderColor = Color.Empty;
            roundedButton11.BorderRadius = 40;
            roundedButton11.BorderSize = 1;
            roundedButton11.Dock = DockStyle.Fill;
            roundedButton11.FlatAppearance.BorderSize = 0;
            roundedButton11.FlatStyle = FlatStyle.Flat;
            roundedButton11.ForeColor = Color.White;
            roundedButton11.Location = new Point(0, 0);
            roundedButton11.Name = "roundedButton11";
            roundedButton11.Size = new Size(714, 288);
            roundedButton11.TabIndex = 6;
            roundedButton11.TextColor = Color.White;
            roundedButton11.UseVisualStyleBackColor = false;
            // 
            // panelInformacionPersonal
            // 
            panelInformacionPersonal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelInformacionPersonal.BackColor = Color.FromArgb(250, 249, 246);
            panelInformacionPersonal.Controls.Add(label5);
            panelInformacionPersonal.Controls.Add(txtTelefono);
            panelInformacionPersonal.Controls.Add(roundedButton12);
            panelInformacionPersonal.Controls.Add(txtPass);
            panelInformacionPersonal.Controls.Add(lblNombre);
            panelInformacionPersonal.Controls.Add(lblApellido);
            panelInformacionPersonal.Controls.Add(lblUsuario);
            panelInformacionPersonal.Controls.Add(txtUsuario);
            panelInformacionPersonal.Controls.Add(txtNombre);
            panelInformacionPersonal.Controls.Add(checkBoxCambiarPass);
            panelInformacionPersonal.Controls.Add(txtConfirmarPass);
            panelInformacionPersonal.Controls.Add(lblConfirmarPassword);
            panelInformacionPersonal.Controls.Add(label2);
            panelInformacionPersonal.Controls.Add(lblPassword);
            panelInformacionPersonal.Controls.Add(txtCorreo);
            panelInformacionPersonal.Controls.Add(roundedButton9);
            panelInformacionPersonal.Controls.Add(roundedButton7);
            panelInformacionPersonal.Controls.Add(roundedButton5);
            panelInformacionPersonal.Controls.Add(txtApellido);
            panelInformacionPersonal.Controls.Add(roundedButton4);
            panelInformacionPersonal.Controls.Add(roundedButton3);
            panelInformacionPersonal.Controls.Add(roundedButton6);
            panelInformacionPersonal.Controls.Add(roundedButton8);
            panelInformacionPersonal.Controls.Add(roundedButton2);
            panelInformacionPersonal.Location = new Point(55, 71);
            panelInformacionPersonal.Name = "panelInformacionPersonal";
            panelInformacionPersonal.Size = new Size(711, 363);
            panelInformacionPersonal.TabIndex = 3;
            // 
            // txtPass
            // 
            txtPass.Anchor = AnchorStyles.Top;
            txtPass.BorderStyle = BorderStyle.None;
            txtPass.Location = new Point(101, 264);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "...";
            txtPass.Size = new Size(156, 13);
            txtPass.TabIndex = 16;
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.White;
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(101, 64);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(75, 19);
            lblNombre.TabIndex = 4;
            lblNombre.Text = "Nombre *";
            // 
            // lblApellido
            // 
            lblApellido.Anchor = AnchorStyles.Top;
            lblApellido.AutoSize = true;
            lblApellido.BackColor = Color.White;
            lblApellido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblApellido.Location = new Point(294, 64);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(76, 19);
            lblApellido.TabIndex = 5;
            lblApellido.Text = "Apellido *";
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top;
            lblUsuario.AutoSize = true;
            lblUsuario.BackColor = Color.White;
            lblUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuario.Location = new Point(501, 64);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(70, 19);
            lblUsuario.TabIndex = 6;
            lblUsuario.Text = "Usuario *";
            // 
            // txtUsuario
            // 
            txtUsuario.Anchor = AnchorStyles.Top;
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(501, 99);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "...";
            txtUsuario.Size = new Size(156, 13);
            txtUsuario.TabIndex = 12;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Location = new Point(101, 99);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "...";
            txtNombre.Size = new Size(156, 13);
            txtNombre.TabIndex = 4;
            // 
            // checkBoxCambiarPass
            // 
            checkBoxCambiarPass.Anchor = AnchorStyles.Top;
            checkBoxCambiarPass.AutoSize = true;
            checkBoxCambiarPass.BackColor = Color.White;
            checkBoxCambiarPass.Font = new Font("Microsoft Sans Serif", 10F);
            checkBoxCambiarPass.Location = new Point(101, 308);
            checkBoxCambiarPass.Name = "checkBoxCambiarPass";
            checkBoxCambiarPass.Size = new Size(156, 21);
            checkBoxCambiarPass.TabIndex = 7;
            checkBoxCambiarPass.Text = "Cambiar Contraseña";
            checkBoxCambiarPass.UseVisualStyleBackColor = false;
            // 
            // txtConfirmarPass
            // 
            txtConfirmarPass.Anchor = AnchorStyles.Top;
            txtConfirmarPass.BorderStyle = BorderStyle.None;
            txtConfirmarPass.Location = new Point(299, 264);
            txtConfirmarPass.Name = "txtConfirmarPass";
            txtConfirmarPass.PlaceholderText = "...";
            txtConfirmarPass.Size = new Size(156, 13);
            txtConfirmarPass.TabIndex = 18;
            // 
            // lblConfirmarPassword
            // 
            lblConfirmarPassword.Anchor = AnchorStyles.Top;
            lblConfirmarPassword.AutoSize = true;
            lblConfirmarPassword.BackColor = Color.White;
            lblConfirmarPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmarPassword.Location = new Point(294, 229);
            lblConfirmarPassword.Name = "lblConfirmarPassword";
            lblConfirmarPassword.Size = new Size(154, 19);
            lblConfirmarPassword.TabIndex = 9;
            lblConfirmarPassword.Text = "Confirmar contraseña\r\n";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(101, 145);
            label2.Name = "label2";
            label2.Size = new Size(56, 19);
            label2.TabIndex = 7;
            label2.Text = "Correo";
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Top;
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.White;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.Location = new Point(101, 229);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(94, 19);
            lblPassword.TabIndex = 8;
            lblPassword.Text = "Contraseña *";
            // 
            // txtCorreo
            // 
            txtCorreo.Anchor = AnchorStyles.Top;
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Location = new Point(101, 180);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "...";
            txtCorreo.Size = new Size(273, 13);
            txtCorreo.TabIndex = 14;
            // 
            // roundedButton9
            // 
            roundedButton9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton9.BackColor = Color.FromArgb(249, 247, 242);
            roundedButton9.BackgroundColor = Color.FromArgb(249, 247, 242);
            roundedButton9.BorderColor = Color.Empty;
            roundedButton9.BorderRadius = 22;
            roundedButton9.BorderSize = 0;
            roundedButton9.Enabled = false;
            roundedButton9.FlatAppearance.BorderSize = 0;
            roundedButton9.FlatStyle = FlatStyle.Flat;
            roundedButton9.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton9.ForeColor = Color.Black;
            roundedButton9.Location = new Point(3, 3);
            roundedButton9.Name = "roundedButton9";
            roundedButton9.Size = new Size(705, 40);
            roundedButton9.TabIndex = 4;
            roundedButton9.Text = "Información Personal";
            roundedButton9.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton9.TextColor = Color.Black;
            roundedButton9.UseVisualStyleBackColor = false;
            // 
            // roundedButton7
            // 
            roundedButton7.Anchor = AnchorStyles.Top;
            roundedButton7.BackColor = Color.White;
            roundedButton7.BackgroundColor = Color.White;
            roundedButton7.BorderColor = Color.LightGray;
            roundedButton7.BorderRadius = 10;
            roundedButton7.BorderSize = 1;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.ForeColor = Color.White;
            roundedButton7.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton7.Location = new Point(96, 251);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(170, 40);
            roundedButton7.TabIndex = 17;
            roundedButton7.TextColor = Color.White;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.White;
            roundedButton5.BackgroundColor = Color.White;
            roundedButton5.BorderColor = Color.LightGray;
            roundedButton5.BorderRadius = 10;
            roundedButton5.BorderSize = 1;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton5.Location = new Point(496, 86);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(170, 40);
            roundedButton5.TabIndex = 13;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // txtApellido
            // 
            txtApellido.Anchor = AnchorStyles.Top;
            txtApellido.BorderStyle = BorderStyle.None;
            txtApellido.Location = new Point(299, 99);
            txtApellido.Name = "txtApellido";
            txtApellido.PlaceholderText = "...";
            txtApellido.Size = new Size(156, 13);
            txtApellido.TabIndex = 10;
            // 
            // roundedButton4
            // 
            roundedButton4.Anchor = AnchorStyles.Top;
            roundedButton4.BackColor = Color.White;
            roundedButton4.BackgroundColor = Color.White;
            roundedButton4.BorderColor = Color.LightGray;
            roundedButton4.BorderRadius = 10;
            roundedButton4.BorderSize = 1;
            roundedButton4.Enabled = false;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton4.Location = new Point(294, 86);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(170, 40);
            roundedButton4.TabIndex = 11;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.White;
            roundedButton3.BackgroundColor = Color.White;
            roundedButton3.BorderColor = Color.LightGray;
            roundedButton3.BorderRadius = 10;
            roundedButton3.BorderSize = 1;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton3.Location = new Point(96, 86);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(170, 40);
            roundedButton3.TabIndex = 5;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton6
            // 
            roundedButton6.Anchor = AnchorStyles.Top;
            roundedButton6.BackColor = Color.White;
            roundedButton6.BackgroundColor = Color.White;
            roundedButton6.BorderColor = Color.LightGray;
            roundedButton6.BorderRadius = 10;
            roundedButton6.BorderSize = 1;
            roundedButton6.Enabled = false;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.ForeColor = Color.White;
            roundedButton6.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton6.Location = new Point(96, 167);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(289, 40);
            roundedButton6.TabIndex = 15;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // roundedButton8
            // 
            roundedButton8.Anchor = AnchorStyles.Top;
            roundedButton8.BackColor = Color.White;
            roundedButton8.BackgroundColor = Color.White;
            roundedButton8.BorderColor = Color.LightGray;
            roundedButton8.BorderRadius = 10;
            roundedButton8.BorderSize = 1;
            roundedButton8.Enabled = false;
            roundedButton8.FlatAppearance.BorderSize = 0;
            roundedButton8.FlatStyle = FlatStyle.Flat;
            roundedButton8.ForeColor = Color.White;
            roundedButton8.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton8.Location = new Point(294, 251);
            roundedButton8.Name = "roundedButton8";
            roundedButton8.Size = new Size(170, 40);
            roundedButton8.TabIndex = 19;
            roundedButton8.TextColor = Color.White;
            roundedButton8.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.White;
            roundedButton2.BackgroundColor = Color.White;
            roundedButton2.BorderColor = Color.Empty;
            roundedButton2.BorderRadius = 30;
            roundedButton2.BorderSize = 0;
            roundedButton2.Dock = DockStyle.Fill;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(711, 363);
            roundedButton2.TabIndex = 2;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(52, 38);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(97, 30);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Usuarios";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(496, 145);
            label5.Name = "label5";
            label5.Size = new Size(77, 19);
            label5.TabIndex = 20;
            label5.Text = "Teléfono *";
            // 
            // txtTelefono
            // 
            txtTelefono.Anchor = AnchorStyles.Top;
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Location = new Point(496, 180);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "...";
            txtTelefono.Size = new Size(156, 13);
            txtTelefono.TabIndex = 21;
            // 
            // roundedButton12
            // 
            roundedButton12.Anchor = AnchorStyles.Top;
            roundedButton12.BackColor = Color.White;
            roundedButton12.BackgroundColor = Color.White;
            roundedButton12.BorderColor = Color.LightGray;
            roundedButton12.BorderRadius = 10;
            roundedButton12.BorderSize = 1;
            roundedButton12.Enabled = false;
            roundedButton12.FlatAppearance.BorderSize = 0;
            roundedButton12.FlatStyle = FlatStyle.Flat;
            roundedButton12.ForeColor = Color.White;
            roundedButton12.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton12.Location = new Point(491, 167);
            roundedButton12.Name = "roundedButton12";
            roundedButton12.Size = new Size(170, 40);
            roundedButton12.TabIndex = 22;
            roundedButton12.TextColor = Color.White;
            roundedButton12.UseVisualStyleBackColor = false;
            // 
            // Usuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(807, 560);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Usuarios";
            Text = "Usuarios";
            Load += Usuarios_Load;
            Resize += Usuarios_Resize;
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).EndInit();
            tabControl1.ResumeLayout(false);
            Listar.ResumeLayout(false);
            Listar.PerformLayout();
            Detalles.ResumeLayout(false);
            Detalles.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgPermisos).EndInit();
            panelInformacionPersonal.ResumeLayout(false);
            panelInformacionPersonal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBusqueda;
        private Clases.RoundedButton roundedButton1;
        private Label label1;
        private Clases.RoundedButton btnAdd;
        private TextBox txtBuscar;
        private DataGridView dtgUsuarios;
        private Label lblPagina;
        private Clases.RoundedButton btnAnterior;
        private Clases.RoundedButton btnSiguiente;
        private TabControl tabControl1;
        private TabPage Listar;
        private TabPage Detalles;
        private Label labelTotal;
        private Label lblTitulo;
        private Panel panelInformacionPersonal;
        private Clases.RoundedButton roundedButton2;
        private Label lblNombre;
        private Label lblApellido;
        private Label label2;
        private Label lblUsuario;
        private Label lblConfirmarPassword;
        private Label lblPassword;
        private TextBox txtNombre;
        private Clases.RoundedButton roundedButton3;
        private TextBox txtUsuario;
        private Clases.RoundedButton roundedButton5;
        private TextBox txtApellido;
        private Clases.RoundedButton roundedButton4;
        private TextBox txtCorreo;
        private Clases.RoundedButton roundedButton6;
        private TextBox txtConfirmarPass;
        private Clases.RoundedButton roundedButton8;
        private TextBox txtPass;
        private Clases.RoundedButton roundedButton7;
        private Clases.RoundedButton roundedButton9;
        private Panel panel1;
        private Clases.RoundedButton roundedButton10;
        private Clases.RoundedButton btnGuardarUsuario;
        private Clases.RoundedButton roundedButton19;
        private DataGridView dtgPermisos;
        private CheckBox checkBoxCambiarPass;
        private Clases.RoundedButton roundedButton11;
        private Label label5;
        private TextBox txtTelefono;
        private Clases.RoundedButton roundedButton12;
    }
}