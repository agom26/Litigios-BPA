namespace Presentacion.Personas
{
    partial class Demandantes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demandantes));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            panelBusqueda = new Panel();
            btnAdd = new Presentacion.Clases.RoundedButton();
            txtBuscar = new TextBox();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            label1 = new Label();
            dtgDemandantes = new DataGridView();
            lblPagina = new Label();
            btnAnterior = new Presentacion.Clases.RoundedButton();
            btnSiguiente = new Presentacion.Clases.RoundedButton();
            tabControl1 = new TabControl();
            Listar = new TabPage();
            labelTotal = new Label();
            Detalles = new TabPage();
            panel1 = new Panel();
            label3 = new Label();
            txtTelefonoA = new TextBox();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            label4 = new Label();
            txtNombreA = new TextBox();
            label7 = new Label();
            txtCorreoA = new TextBox();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            roundedButton10 = new Presentacion.Clases.RoundedButton();
            roundedButton11 = new Presentacion.Clases.RoundedButton();
            roundedButton13 = new Presentacion.Clases.RoundedButton();
            roundedButton19 = new Presentacion.Clases.RoundedButton();
            btnGuardarUsuario = new Presentacion.Clases.RoundedButton();
            panelInformacionPersonal = new Panel();
            label5 = new Label();
            txtTelefono = new TextBox();
            roundedButton12 = new Presentacion.Clases.RoundedButton();
            lblNombre = new Label();
            lblApellido = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtCorreo = new TextBox();
            roundedButton9 = new Presentacion.Clases.RoundedButton();
            txtDireccion = new TextBox();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton6 = new Presentacion.Clases.RoundedButton();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            lblTitulo = new Label();
            panelBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgDemandantes).BeginInit();
            tabControl1.SuspendLayout();
            Listar.SuspendLayout();
            Detalles.SuspendLayout();
            panel1.SuspendLayout();
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
            btnAdd.ImageAlign = ContentAlignment.MiddleRight;
            btnAdd.Location = new Point(602, 24);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(3, 0, 0, 0);
            btnAdd.Size = new Size(150, 40);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "  Agregar ";
            btnAdd.TextAlign = ContentAlignment.MiddleLeft;
            btnAdd.TextColor = Color.White;
            btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.None;
            txtBuscar.Location = new Point(414, 38);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar  ...";
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
            label1.Size = new Size(162, 60);
            label1.TabIndex = 0;
            label1.Text = "Demandantes /\r\nActores";
            // 
            // dtgDemandantes
            // 
            dtgDemandantes.AllowUserToAddRows = false;
            dtgDemandantes.AllowUserToDeleteRows = false;
            dtgDemandantes.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(249, 247, 242);
            dtgDemandantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dtgDemandantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgDemandantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgDemandantes.BackgroundColor = Color.White;
            dtgDemandantes.BorderStyle = BorderStyle.None;
            dtgDemandantes.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgDemandantes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtgDemandantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtgDemandantes.ColumnHeadersHeight = 40;
            dtgDemandantes.EnableHeadersVisualStyles = false;
            dtgDemandantes.Location = new Point(36, 161);
            dtgDemandantes.MinimumSize = new Size(719, 226);
            dtgDemandantes.MultiSelect = false;
            dtgDemandantes.Name = "dtgDemandantes";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dtgDemandantes.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dtgDemandantes.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dtgDemandantes.RowsDefaultCellStyle = dataGridViewCellStyle8;
            dtgDemandantes.RowTemplate.Height = 45;
            dtgDemandantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgDemandantes.Size = new Size(744, 226);
            dtgDemandantes.TabIndex = 1;
            dtgDemandantes.CellClick += dtgDemandantes_CellClick;
            dtgDemandantes.CellFormatting += dtgDemandantes_CellFormatting;
            dtgDemandantes.DataBindingComplete += dtgDemandantes_DataBindingComplete;
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
            btnAnterior.Location = new Point(587, 322);
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
            btnSiguiente.Location = new Point(693, 322);
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
            Listar.Controls.Add(dtgDemandantes);
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
            Detalles.Controls.Add(panel1);
            Detalles.Controls.Add(roundedButton19);
            Detalles.Controls.Add(btnGuardarUsuario);
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
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(250, 249, 246);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtTelefonoA);
            panel1.Controls.Add(roundedButton5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtNombreA);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtCorreoA);
            panel1.Controls.Add(roundedButton7);
            panel1.Controls.Add(roundedButton10);
            panel1.Controls.Add(roundedButton11);
            panel1.Controls.Add(roundedButton13);
            panel1.Location = new Point(58, 389);
            panel1.Name = "panel1";
            panel1.Size = new Size(670, 261);
            panel1.TabIndex = 7;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(409, 152);
            label3.Name = "label3";
            label3.Size = new Size(67, 19);
            label3.TabIndex = 20;
            label3.Text = "Teléfono";
            // 
            // txtTelefonoA
            // 
            txtTelefonoA.Anchor = AnchorStyles.Top;
            txtTelefonoA.BorderStyle = BorderStyle.None;
            txtTelefonoA.Location = new Point(409, 187);
            txtTelefonoA.Name = "txtTelefonoA";
            txtTelefonoA.PlaceholderText = "...";
            txtTelefonoA.Size = new Size(156, 13);
            txtTelefonoA.TabIndex = 21;
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
            roundedButton5.Location = new Point(404, 174);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(170, 40);
            roundedButton5.TabIndex = 22;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(47, 71);
            label4.Name = "label4";
            label4.Size = new Size(65, 19);
            label4.TabIndex = 4;
            label4.Text = "Nombre";
            // 
            // txtNombreA
            // 
            txtNombreA.Anchor = AnchorStyles.Top;
            txtNombreA.BorderStyle = BorderStyle.None;
            txtNombreA.Location = new Point(47, 106);
            txtNombreA.Name = "txtNombreA";
            txtNombreA.PlaceholderText = "...";
            txtNombreA.Size = new Size(156, 13);
            txtNombreA.TabIndex = 4;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label7.Location = new Point(47, 152);
            label7.Name = "label7";
            label7.Size = new Size(56, 19);
            label7.TabIndex = 7;
            label7.Text = "Correo";
            // 
            // txtCorreoA
            // 
            txtCorreoA.Anchor = AnchorStyles.Top;
            txtCorreoA.BorderStyle = BorderStyle.None;
            txtCorreoA.Location = new Point(47, 187);
            txtCorreoA.Name = "txtCorreoA";
            txtCorreoA.PlaceholderText = "...";
            txtCorreoA.Size = new Size(307, 13);
            txtCorreoA.TabIndex = 14;
            // 
            // roundedButton7
            // 
            roundedButton7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton7.BackColor = Color.FromArgb(249, 247, 242);
            roundedButton7.BackgroundColor = Color.FromArgb(249, 247, 242);
            roundedButton7.BorderColor = Color.Empty;
            roundedButton7.BorderRadius = 22;
            roundedButton7.BorderSize = 0;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(3, 3);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(667, 40);
            roundedButton7.TabIndex = 4;
            roundedButton7.Text = "Información Abogado Representante";
            roundedButton7.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // roundedButton10
            // 
            roundedButton10.Anchor = AnchorStyles.Top;
            roundedButton10.BackColor = Color.White;
            roundedButton10.BackgroundColor = Color.White;
            roundedButton10.BorderColor = Color.LightGray;
            roundedButton10.BorderRadius = 10;
            roundedButton10.BorderSize = 1;
            roundedButton10.Enabled = false;
            roundedButton10.FlatAppearance.BorderSize = 0;
            roundedButton10.FlatStyle = FlatStyle.Flat;
            roundedButton10.ForeColor = Color.White;
            roundedButton10.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton10.Location = new Point(42, 93);
            roundedButton10.Name = "roundedButton10";
            roundedButton10.Size = new Size(170, 40);
            roundedButton10.TabIndex = 5;
            roundedButton10.TextColor = Color.White;
            roundedButton10.UseVisualStyleBackColor = false;
            // 
            // roundedButton11
            // 
            roundedButton11.Anchor = AnchorStyles.Top;
            roundedButton11.BackColor = Color.White;
            roundedButton11.BackgroundColor = Color.White;
            roundedButton11.BorderColor = Color.LightGray;
            roundedButton11.BorderRadius = 10;
            roundedButton11.BorderSize = 1;
            roundedButton11.Enabled = false;
            roundedButton11.FlatAppearance.BorderSize = 0;
            roundedButton11.FlatStyle = FlatStyle.Flat;
            roundedButton11.ForeColor = Color.White;
            roundedButton11.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton11.Location = new Point(42, 174);
            roundedButton11.Name = "roundedButton11";
            roundedButton11.Size = new Size(322, 40);
            roundedButton11.TabIndex = 15;
            roundedButton11.TextColor = Color.White;
            roundedButton11.UseVisualStyleBackColor = false;
            // 
            // roundedButton13
            // 
            roundedButton13.BackColor = Color.White;
            roundedButton13.BackgroundColor = Color.White;
            roundedButton13.BorderColor = Color.Empty;
            roundedButton13.BorderRadius = 30;
            roundedButton13.BorderSize = 0;
            roundedButton13.Dock = DockStyle.Fill;
            roundedButton13.Enabled = false;
            roundedButton13.FlatAppearance.BorderSize = 0;
            roundedButton13.FlatStyle = FlatStyle.Flat;
            roundedButton13.ForeColor = Color.White;
            roundedButton13.Location = new Point(0, 0);
            roundedButton13.Name = "roundedButton13";
            roundedButton13.Size = new Size(670, 261);
            roundedButton13.TabIndex = 2;
            roundedButton13.TextColor = Color.White;
            roundedButton13.UseVisualStyleBackColor = false;
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
            roundedButton19.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            roundedButton19.ForeColor = Color.Black;
            roundedButton19.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton19.Location = new Point(395, 690);
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
            btnGuardarUsuario.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnGuardarUsuario.ForeColor = Color.White;
            btnGuardarUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarUsuario.Location = new Point(578, 690);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Padding = new Padding(3, 0, 0, 0);
            btnGuardarUsuario.Size = new Size(150, 40);
            btnGuardarUsuario.TabIndex = 5;
            btnGuardarUsuario.Text = "Guardar";
            btnGuardarUsuario.TextColor = Color.White;
            btnGuardarUsuario.UseVisualStyleBackColor = false;
            btnGuardarUsuario.Click += roundedButton18_Click;
            // 
            // panelInformacionPersonal
            // 
            panelInformacionPersonal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelInformacionPersonal.BackColor = Color.FromArgb(250, 249, 246);
            panelInformacionPersonal.Controls.Add(label5);
            panelInformacionPersonal.Controls.Add(txtTelefono);
            panelInformacionPersonal.Controls.Add(roundedButton12);
            panelInformacionPersonal.Controls.Add(lblNombre);
            panelInformacionPersonal.Controls.Add(lblApellido);
            panelInformacionPersonal.Controls.Add(txtNombre);
            panelInformacionPersonal.Controls.Add(label2);
            panelInformacionPersonal.Controls.Add(txtCorreo);
            panelInformacionPersonal.Controls.Add(roundedButton9);
            panelInformacionPersonal.Controls.Add(txtDireccion);
            panelInformacionPersonal.Controls.Add(roundedButton4);
            panelInformacionPersonal.Controls.Add(roundedButton3);
            panelInformacionPersonal.Controls.Add(roundedButton6);
            panelInformacionPersonal.Controls.Add(roundedButton2);
            panelInformacionPersonal.Location = new Point(55, 71);
            panelInformacionPersonal.Name = "panelInformacionPersonal";
            panelInformacionPersonal.Size = new Size(673, 261);
            panelInformacionPersonal.TabIndex = 3;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(418, 148);
            label5.Name = "label5";
            label5.Size = new Size(67, 19);
            label5.TabIndex = 20;
            label5.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Anchor = AnchorStyles.Top;
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Location = new Point(418, 183);
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
            roundedButton12.Location = new Point(413, 170);
            roundedButton12.Name = "roundedButton12";
            roundedButton12.Size = new Size(170, 40);
            roundedButton12.TabIndex = 22;
            roundedButton12.TextColor = Color.White;
            roundedButton12.UseVisualStyleBackColor = false;
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.White;
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(56, 67);
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
            lblApellido.Location = new Point(249, 67);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(82, 19);
            lblApellido.TabIndex = 5;
            lblApellido.Text = "Dirección *";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Location = new Point(56, 102);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "...";
            txtNombre.Size = new Size(156, 13);
            txtNombre.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(56, 148);
            label2.Name = "label2";
            label2.Size = new Size(56, 19);
            label2.TabIndex = 7;
            label2.Text = "Correo";
            // 
            // txtCorreo
            // 
            txtCorreo.Anchor = AnchorStyles.Top;
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Location = new Point(56, 183);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "...";
            txtCorreo.Size = new Size(307, 13);
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
            roundedButton9.Size = new Size(670, 40);
            roundedButton9.TabIndex = 4;
            roundedButton9.Text = "Información Personal";
            roundedButton9.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton9.TextColor = Color.Black;
            roundedButton9.UseVisualStyleBackColor = false;
            // 
            // txtDireccion
            // 
            txtDireccion.Anchor = AnchorStyles.Top;
            txtDireccion.BorderStyle = BorderStyle.None;
            txtDireccion.Location = new Point(254, 102);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "...";
            txtDireccion.Size = new Size(353, 13);
            txtDireccion.TabIndex = 10;
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
            roundedButton4.Location = new Point(249, 89);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(367, 40);
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
            roundedButton3.Location = new Point(51, 89);
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
            roundedButton6.Location = new Point(51, 170);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(322, 40);
            roundedButton6.TabIndex = 15;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
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
            roundedButton2.Size = new Size(673, 261);
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
            lblTitulo.Size = new Size(147, 30);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Demandantes";
            // 
            // Demandantes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(807, 560);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Demandantes";
            Text = "Demandantes";
            Load += Demandantes_Load;
            Resize += Demandantes_Resize;
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgDemandantes).EndInit();
            tabControl1.ResumeLayout(false);
            Listar.ResumeLayout(false);
            Listar.PerformLayout();
            Detalles.ResumeLayout(false);
            Detalles.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private DataGridView dtgDemandantes;
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
        private TextBox txtNombre;
        private Clases.RoundedButton roundedButton3;
        private TextBox txtDireccion;
        private Clases.RoundedButton roundedButton4;
        private TextBox txtCorreo;
        private Clases.RoundedButton roundedButton6;
        private Clases.RoundedButton roundedButton9;
        private Clases.RoundedButton btnGuardarUsuario;
        private Clases.RoundedButton roundedButton19;
        private Label label5;
        private TextBox txtTelefono;
        private Clases.RoundedButton roundedButton12;
        private Panel panel1;
        private Label label3;
        private TextBox txtTelefonoA;
        private Clases.RoundedButton roundedButton5;
        private Label label4;
        private TextBox txtNombreA;
        private Label label7;
        private TextBox txtCorreoA;
        private Clases.RoundedButton roundedButton7;
        private Clases.RoundedButton roundedButton10;
        private Clases.RoundedButton roundedButton11;
        private Clases.RoundedButton roundedButton13;
    }
}