namespace Presentacion.Casos.Participantes
{
    partial class FrmAgregarContactoEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarContactoEmpresa));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            radioButtonAgregar = new RadioButton();
            radioButtonBuscar = new RadioButton();
            panel2 = new Panel();
            tablessControl1 = new Presentacion.Clases.TablessControl();
            tabPageBuscar = new TabPage();
            btnSiguiente = new Presentacion.Clases.RoundedButton();
            btnAnterior = new Presentacion.Clases.RoundedButton();
            lblPagina = new Label();
            labelTotal = new Label();
            dtgContactoEmpresas = new DataGridView();
            lblNombre = new Label();
            txtBuscarContactoEmpresa = new TextBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            tabPageAgregar = new TabPage();
            panel4 = new Panel();
            label3 = new Label();
            txtTelefonoA = new TextBox();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            label4 = new Label();
            txtNombreA = new TextBox();
            label7 = new Label();
            txtCorreoA = new TextBox();
            roundedButton8 = new Presentacion.Clases.RoundedButton();
            roundedButton10 = new Presentacion.Clases.RoundedButton();
            roundedButton11 = new Presentacion.Clases.RoundedButton();
            roundedButton13 = new Presentacion.Clases.RoundedButton();
            btnCancelarAgregarContactoEmpresa = new Presentacion.Clases.RoundedButton();
            btnGuardarContactoEmpresa = new Presentacion.Clases.RoundedButton();
            panelInformacionPersonal = new Panel();
            label5 = new Label();
            roundedButton9 = new Presentacion.Clases.RoundedButton();
            txtTelefono = new TextBox();
            roundedButton12 = new Presentacion.Clases.RoundedButton();
            label1 = new Label();
            lblApellido = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtCorreo = new TextBox();
            txtDireccion = new TextBox();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            roundedButton6 = new Presentacion.Clases.RoundedButton();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            panel3 = new Panel();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            btnAgregarContactoEmpresa = new Presentacion.Clases.RoundedButton();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tablessControl1.SuspendLayout();
            tabPageBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgContactoEmpresas).BeginInit();
            tabPageAgregar.SuspendLayout();
            panel4.SuspendLayout();
            panelInformacionPersonal.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 64);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonAgregar);
            groupBox1.Controls.Add(radioButtonBuscar);
            groupBox1.Location = new Point(98, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 52);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // radioButtonAgregar
            // 
            radioButtonAgregar.AutoSize = true;
            radioButtonAgregar.Location = new Point(226, 20);
            radioButtonAgregar.Name = "radioButtonAgregar";
            radioButtonAgregar.Size = new Size(67, 19);
            radioButtonAgregar.TabIndex = 2;
            radioButtonAgregar.Text = "Agregar";
            radioButtonAgregar.UseVisualStyleBackColor = true;
            radioButtonAgregar.CheckedChanged += radioButtonAgregar_CheckedChanged;
            // 
            // radioButtonBuscar
            // 
            radioButtonBuscar.AutoSize = true;
            radioButtonBuscar.Checked = true;
            radioButtonBuscar.Location = new Point(139, 20);
            radioButtonBuscar.Name = "radioButtonBuscar";
            radioButtonBuscar.Size = new Size(60, 19);
            radioButtonBuscar.TabIndex = 1;
            radioButtonBuscar.TabStop = true;
            radioButtonBuscar.Text = "Buscar";
            radioButtonBuscar.UseVisualStyleBackColor = true;
            radioButtonBuscar.CheckedChanged += radioButtonBuscar_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(tablessControl1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 64);
            panel2.Name = "panel2";
            panel2.Size = new Size(684, 414);
            panel2.TabIndex = 1;
            // 
            // tablessControl1
            // 
            tablessControl1.Controls.Add(tabPageBuscar);
            tablessControl1.Controls.Add(tabPageAgregar);
            tablessControl1.Dock = DockStyle.Fill;
            tablessControl1.Location = new Point(0, 0);
            tablessControl1.Name = "tablessControl1";
            tablessControl1.SelectedIndex = 0;
            tablessControl1.Size = new Size(684, 414);
            tablessControl1.TabIndex = 0;
            tablessControl1.SelectedIndexChanged += tablessControl1_SelectedIndexChanged;
            // 
            // tabPageBuscar
            // 
            tabPageBuscar.BackColor = Color.FromArgb(250, 249, 246);
            tabPageBuscar.Controls.Add(btnSiguiente);
            tabPageBuscar.Controls.Add(btnAnterior);
            tabPageBuscar.Controls.Add(lblPagina);
            tabPageBuscar.Controls.Add(labelTotal);
            tabPageBuscar.Controls.Add(dtgContactoEmpresas);
            tabPageBuscar.Controls.Add(lblNombre);
            tabPageBuscar.Controls.Add(txtBuscarContactoEmpresa);
            tabPageBuscar.Controls.Add(roundedButton3);
            tabPageBuscar.Controls.Add(roundedButton1);
            tabPageBuscar.Location = new Point(4, 24);
            tabPageBuscar.Name = "tabPageBuscar";
            tabPageBuscar.Padding = new Padding(3);
            tabPageBuscar.Size = new Size(676, 386);
            tabPageBuscar.TabIndex = 0;
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
            btnSiguiente.Location = new Point(570, 333);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Padding = new Padding(3, 0, 0, 0);
            btnSiguiente.Size = new Size(87, 40);
            btnSiguiente.TabIndex = 15;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.TextAlign = ContentAlignment.MiddleLeft;
            btnSiguiente.TextColor = Color.White;
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.Click += btnSiguiente_Click;
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
            btnAnterior.Location = new Point(464, 333);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Padding = new Padding(3, 0, 0, 0);
            btnAnterior.Size = new Size(87, 40);
            btnAnterior.TabIndex = 14;
            btnAnterior.Text = "Anterior";
            btnAnterior.TextAlign = ContentAlignment.MiddleRight;
            btnAnterior.TextColor = Color.White;
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(586, 70);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(38, 15);
            lblPagina.TabIndex = 13;
            lblPagina.Text = "label2";
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Location = new Point(26, 70);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(38, 15);
            labelTotal.TabIndex = 12;
            labelTotal.Text = "label2";
            // 
            // dtgContactoEmpresas
            // 
            dtgContactoEmpresas.AllowUserToAddRows = false;
            dtgContactoEmpresas.AllowUserToDeleteRows = false;
            dtgContactoEmpresas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(249, 247, 242);
            dtgContactoEmpresas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgContactoEmpresas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgContactoEmpresas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgContactoEmpresas.BackgroundColor = Color.White;
            dtgContactoEmpresas.BorderStyle = BorderStyle.None;
            dtgContactoEmpresas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgContactoEmpresas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgContactoEmpresas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgContactoEmpresas.ColumnHeadersHeight = 40;
            dtgContactoEmpresas.EnableHeadersVisualStyles = false;
            dtgContactoEmpresas.Location = new Point(37, 100);
            dtgContactoEmpresas.MinimumSize = new Size(400, 100);
            dtgContactoEmpresas.MultiSelect = false;
            dtgContactoEmpresas.Name = "dtgContactoEmpresas";
            dtgContactoEmpresas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgContactoEmpresas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgContactoEmpresas.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dtgContactoEmpresas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgContactoEmpresas.RowTemplate.Height = 45;
            dtgContactoEmpresas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgContactoEmpresas.Size = new Size(611, 215);
            dtgContactoEmpresas.TabIndex = 11;
            dtgContactoEmpresas.CellClick += dtgContactoEmpresas_CellClick;
            dtgContactoEmpresas.DataBindingComplete += dtgContactoEmpresas_DataBindingComplete;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.FromArgb(250, 249, 246);
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(26, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(217, 19);
            lblNombre.TabIndex = 7;
            lblNombre.Text = "Buscar por nombre o dirección";
            // 
            // txtBuscarContactoEmpresa
            // 
            txtBuscarContactoEmpresa.BorderStyle = BorderStyle.None;
            txtBuscarContactoEmpresa.Font = new Font("Segoe UI", 9F);
            txtBuscarContactoEmpresa.Location = new Point(61, 38);
            txtBuscarContactoEmpresa.Name = "txtBuscarContactoEmpresa";
            txtBuscarContactoEmpresa.PlaceholderText = "...";
            txtBuscarContactoEmpresa.Size = new Size(346, 16);
            txtBuscarContactoEmpresa.TabIndex = 3;
            txtBuscarContactoEmpresa.KeyDown += txtBuscarContactoEmpresa_KeyDown;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.White;
            roundedButton3.BackgroundColor = Color.White;
            roundedButton3.BorderColor = Color.LightGray;
            roundedButton3.BorderRadius = 10;
            roundedButton3.BorderSize = 1;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Image = Properties.Resources.buscar;
            roundedButton3.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton3.Location = new Point(26, 25);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(390, 40);
            roundedButton3.TabIndex = 8;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton1.BackColor = Color.White;
            roundedButton1.BackgroundColor = Color.White;
            roundedButton1.BorderColor = Color.LightGray;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 1;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton1.Location = new Point(26, 88);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(631, 239);
            roundedButton1.TabIndex = 10;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // tabPageAgregar
            // 
            tabPageAgregar.AutoScroll = true;
            tabPageAgregar.BackColor = Color.FromArgb(250, 249, 246);
            tabPageAgregar.Controls.Add(panel4);
            tabPageAgregar.Controls.Add(btnCancelarAgregarContactoEmpresa);
            tabPageAgregar.Controls.Add(btnGuardarContactoEmpresa);
            tabPageAgregar.Controls.Add(panelInformacionPersonal);
            tabPageAgregar.Location = new Point(4, 24);
            tabPageAgregar.Name = "tabPageAgregar";
            tabPageAgregar.Padding = new Padding(3);
            tabPageAgregar.Size = new Size(676, 386);
            tabPageAgregar.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(250, 249, 246);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(txtTelefonoA);
            panel4.Controls.Add(roundedButton7);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(txtNombreA);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txtCorreoA);
            panel4.Controls.Add(roundedButton8);
            panel4.Controls.Add(roundedButton10);
            panel4.Controls.Add(roundedButton11);
            panel4.Controls.Add(roundedButton13);
            panel4.Location = new Point(35, 297);
            panel4.Name = "panel4";
            panel4.Size = new Size(606, 261);
            panel4.TabIndex = 9;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(396, 149);
            label3.Name = "label3";
            label3.Size = new Size(67, 19);
            label3.TabIndex = 20;
            label3.Text = "Teléfono";
            // 
            // txtTelefonoA
            // 
            txtTelefonoA.Anchor = AnchorStyles.Top;
            txtTelefonoA.BorderStyle = BorderStyle.None;
            txtTelefonoA.Location = new Point(396, 184);
            txtTelefonoA.Name = "txtTelefonoA";
            txtTelefonoA.PlaceholderText = "...";
            txtTelefonoA.Size = new Size(156, 16);
            txtTelefonoA.TabIndex = 21;
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
            roundedButton7.Location = new Point(391, 171);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(170, 40);
            roundedButton7.TabIndex = 22;
            roundedButton7.TextColor = Color.White;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(34, 68);
            label4.Name = "label4";
            label4.Size = new Size(65, 19);
            label4.TabIndex = 4;
            label4.Text = "Nombre";
            // 
            // txtNombreA
            // 
            txtNombreA.Anchor = AnchorStyles.Top;
            txtNombreA.BorderStyle = BorderStyle.None;
            txtNombreA.Location = new Point(34, 103);
            txtNombreA.Name = "txtNombreA";
            txtNombreA.PlaceholderText = "...";
            txtNombreA.Size = new Size(156, 16);
            txtNombreA.TabIndex = 4;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label7.Location = new Point(34, 149);
            label7.Name = "label7";
            label7.Size = new Size(56, 19);
            label7.TabIndex = 7;
            label7.Text = "Correo";
            // 
            // txtCorreoA
            // 
            txtCorreoA.Anchor = AnchorStyles.Top;
            txtCorreoA.BorderStyle = BorderStyle.None;
            txtCorreoA.Location = new Point(34, 184);
            txtCorreoA.Name = "txtCorreoA";
            txtCorreoA.PlaceholderText = "...";
            txtCorreoA.Size = new Size(307, 16);
            txtCorreoA.TabIndex = 14;
            // 
            // roundedButton8
            // 
            roundedButton8.BackColor = Color.FromArgb(249, 247, 242);
            roundedButton8.BackgroundColor = Color.FromArgb(249, 247, 242);
            roundedButton8.BorderColor = Color.Empty;
            roundedButton8.BorderRadius = 22;
            roundedButton8.BorderSize = 0;
            roundedButton8.Dock = DockStyle.Top;
            roundedButton8.Enabled = false;
            roundedButton8.FlatAppearance.BorderSize = 0;
            roundedButton8.FlatStyle = FlatStyle.Flat;
            roundedButton8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton8.ForeColor = Color.Black;
            roundedButton8.Location = new Point(0, 0);
            roundedButton8.Name = "roundedButton8";
            roundedButton8.Size = new Size(606, 40);
            roundedButton8.TabIndex = 4;
            roundedButton8.Text = "Información Abogado Representante";
            roundedButton8.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton8.TextColor = Color.Black;
            roundedButton8.UseVisualStyleBackColor = false;
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
            roundedButton10.Location = new Point(29, 90);
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
            roundedButton11.Location = new Point(29, 171);
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
            roundedButton13.Size = new Size(606, 261);
            roundedButton13.TabIndex = 2;
            roundedButton13.TextColor = Color.White;
            roundedButton13.UseVisualStyleBackColor = false;
            // 
            // btnCancelarAgregarContactoEmpresa
            // 
            btnCancelarAgregarContactoEmpresa.BackColor = Color.FromArgb(250, 249, 246);
            btnCancelarAgregarContactoEmpresa.BackgroundColor = Color.FromArgb(250, 249, 246);
            btnCancelarAgregarContactoEmpresa.BorderColor = Color.Silver;
            btnCancelarAgregarContactoEmpresa.BorderRadius = 10;
            btnCancelarAgregarContactoEmpresa.BorderSize = 1;
            btnCancelarAgregarContactoEmpresa.FlatAppearance.BorderSize = 0;
            btnCancelarAgregarContactoEmpresa.FlatStyle = FlatStyle.Flat;
            btnCancelarAgregarContactoEmpresa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancelarAgregarContactoEmpresa.ForeColor = Color.Black;
            btnCancelarAgregarContactoEmpresa.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarAgregarContactoEmpresa.Location = new Point(308, 567);
            btnCancelarAgregarContactoEmpresa.Name = "btnCancelarAgregarContactoEmpresa";
            btnCancelarAgregarContactoEmpresa.Padding = new Padding(3, 0, 0, 0);
            btnCancelarAgregarContactoEmpresa.Size = new Size(150, 40);
            btnCancelarAgregarContactoEmpresa.TabIndex = 8;
            btnCancelarAgregarContactoEmpresa.Text = "Cancelar";
            btnCancelarAgregarContactoEmpresa.TextColor = Color.Black;
            btnCancelarAgregarContactoEmpresa.UseVisualStyleBackColor = false;
            btnCancelarAgregarContactoEmpresa.Click += roundedButton19_Click;
            // 
            // btnGuardarContactoEmpresa
            // 
            btnGuardarContactoEmpresa.BackColor = Color.FromArgb(194, 160, 91);
            btnGuardarContactoEmpresa.BackgroundColor = Color.FromArgb(194, 160, 91);
            btnGuardarContactoEmpresa.BorderColor = Color.Empty;
            btnGuardarContactoEmpresa.BorderRadius = 10;
            btnGuardarContactoEmpresa.BorderSize = 1;
            btnGuardarContactoEmpresa.FlatAppearance.BorderSize = 0;
            btnGuardarContactoEmpresa.FlatStyle = FlatStyle.Flat;
            btnGuardarContactoEmpresa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnGuardarContactoEmpresa.ForeColor = Color.White;
            btnGuardarContactoEmpresa.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarContactoEmpresa.Location = new Point(491, 567);
            btnGuardarContactoEmpresa.Name = "btnGuardarContactoEmpresa";
            btnGuardarContactoEmpresa.Padding = new Padding(3, 0, 0, 0);
            btnGuardarContactoEmpresa.Size = new Size(150, 40);
            btnGuardarContactoEmpresa.TabIndex = 7;
            btnGuardarContactoEmpresa.Text = "Guardar";
            btnGuardarContactoEmpresa.TextColor = Color.White;
            btnGuardarContactoEmpresa.UseVisualStyleBackColor = false;
            btnGuardarContactoEmpresa.Click += btnGuardarContactoEmpresa_Click;
            // 
            // panelInformacionPersonal
            // 
            panelInformacionPersonal.BackColor = Color.FromArgb(250, 249, 246);
            panelInformacionPersonal.Controls.Add(label5);
            panelInformacionPersonal.Controls.Add(roundedButton9);
            panelInformacionPersonal.Controls.Add(txtTelefono);
            panelInformacionPersonal.Controls.Add(roundedButton12);
            panelInformacionPersonal.Controls.Add(label1);
            panelInformacionPersonal.Controls.Add(lblApellido);
            panelInformacionPersonal.Controls.Add(txtNombre);
            panelInformacionPersonal.Controls.Add(label2);
            panelInformacionPersonal.Controls.Add(txtCorreo);
            panelInformacionPersonal.Controls.Add(txtDireccion);
            panelInformacionPersonal.Controls.Add(roundedButton4);
            panelInformacionPersonal.Controls.Add(roundedButton2);
            panelInformacionPersonal.Controls.Add(roundedButton6);
            panelInformacionPersonal.Controls.Add(roundedButton5);
            panelInformacionPersonal.Location = new Point(34, 21);
            panelInformacionPersonal.Name = "panelInformacionPersonal";
            panelInformacionPersonal.Size = new Size(607, 261);
            panelInformacionPersonal.TabIndex = 4;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(396, 145);
            label5.Name = "label5";
            label5.Size = new Size(67, 19);
            label5.TabIndex = 20;
            label5.Text = "Teléfono";
            // 
            // roundedButton9
            // 
            roundedButton9.BackColor = Color.FromArgb(249, 247, 242);
            roundedButton9.BackgroundColor = Color.FromArgb(249, 247, 242);
            roundedButton9.BorderColor = Color.Empty;
            roundedButton9.BorderRadius = 22;
            roundedButton9.BorderSize = 0;
            roundedButton9.Dock = DockStyle.Top;
            roundedButton9.Enabled = false;
            roundedButton9.FlatAppearance.BorderSize = 0;
            roundedButton9.FlatStyle = FlatStyle.Flat;
            roundedButton9.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            roundedButton9.ForeColor = Color.Black;
            roundedButton9.Location = new Point(0, 0);
            roundedButton9.Name = "roundedButton9";
            roundedButton9.Size = new Size(607, 40);
            roundedButton9.TabIndex = 4;
            roundedButton9.Text = "Información Personal";
            roundedButton9.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton9.TextColor = Color.Black;
            roundedButton9.UseVisualStyleBackColor = false;
            // 
            // txtTelefono
            // 
            txtTelefono.Anchor = AnchorStyles.Top;
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Location = new Point(396, 180);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "...";
            txtTelefono.Size = new Size(156, 16);
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
            roundedButton12.Location = new Point(391, 167);
            roundedButton12.Name = "roundedButton12";
            roundedButton12.Size = new Size(170, 40);
            roundedButton12.TabIndex = 22;
            roundedButton12.TextColor = Color.White;
            roundedButton12.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(34, 64);
            label1.Name = "label1";
            label1.Size = new Size(75, 19);
            label1.TabIndex = 4;
            label1.Text = "Nombre *";
            // 
            // lblApellido
            // 
            lblApellido.Anchor = AnchorStyles.Top;
            lblApellido.AutoSize = true;
            lblApellido.BackColor = Color.White;
            lblApellido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblApellido.Location = new Point(227, 64);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(82, 19);
            lblApellido.TabIndex = 5;
            lblApellido.Text = "Dirección *";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Location = new Point(34, 99);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "...";
            txtNombre.Size = new Size(156, 16);
            txtNombre.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(34, 145);
            label2.Name = "label2";
            label2.Size = new Size(56, 19);
            label2.TabIndex = 7;
            label2.Text = "Correo";
            // 
            // txtCorreo
            // 
            txtCorreo.Anchor = AnchorStyles.Top;
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Location = new Point(34, 180);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "...";
            txtCorreo.Size = new Size(307, 16);
            txtCorreo.TabIndex = 14;
            // 
            // txtDireccion
            // 
            txtDireccion.Anchor = AnchorStyles.Top;
            txtDireccion.BorderStyle = BorderStyle.None;
            txtDireccion.Location = new Point(232, 99);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "...";
            txtDireccion.Size = new Size(353, 16);
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
            roundedButton4.Location = new Point(227, 86);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(367, 40);
            roundedButton4.TabIndex = 11;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.Top;
            roundedButton2.BackColor = Color.White;
            roundedButton2.BackgroundColor = Color.White;
            roundedButton2.BorderColor = Color.LightGray;
            roundedButton2.BorderRadius = 10;
            roundedButton2.BorderSize = 1;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton2.Location = new Point(29, 86);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(170, 40);
            roundedButton2.TabIndex = 5;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
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
            roundedButton6.Location = new Point(29, 167);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(322, 40);
            roundedButton6.TabIndex = 15;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.White;
            roundedButton5.BackgroundColor = Color.White;
            roundedButton5.BorderColor = Color.Empty;
            roundedButton5.BorderRadius = 30;
            roundedButton5.BorderSize = 0;
            roundedButton5.Dock = DockStyle.Fill;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(0, 0);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(607, 261);
            roundedButton5.TabIndex = 2;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnAgregarContactoEmpresa);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 478);
            panel3.Name = "panel3";
            panel3.Size = new Size(684, 63);
            panel3.TabIndex = 2;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.FromArgb(250, 249, 246);
            btnCancelar.BackgroundColor = Color.FromArgb(250, 249, 246);
            btnCancelar.BorderColor = Color.Silver;
            btnCancelar.BorderRadius = 10;
            btnCancelar.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(328, 11);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(3, 0, 0, 0);
            btnCancelar.Size = new Size(150, 40);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAgregarContactoEmpresa
            // 
            btnAgregarContactoEmpresa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarContactoEmpresa.BackColor = Color.FromArgb(52, 109, 235);
            btnAgregarContactoEmpresa.BackgroundColor = Color.FromArgb(52, 109, 235);
            btnAgregarContactoEmpresa.BorderColor = Color.Empty;
            btnAgregarContactoEmpresa.BorderRadius = 10;
            btnAgregarContactoEmpresa.BorderSize = 1;
            btnAgregarContactoEmpresa.FlatAppearance.BorderSize = 0;
            btnAgregarContactoEmpresa.FlatStyle = FlatStyle.Flat;
            btnAgregarContactoEmpresa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAgregarContactoEmpresa.ForeColor = Color.White;
            btnAgregarContactoEmpresa.Image = Properties.Resources.boton_agregar;
            btnAgregarContactoEmpresa.ImageAlign = ContentAlignment.MiddleRight;
            btnAgregarContactoEmpresa.Location = new Point(511, 11);
            btnAgregarContactoEmpresa.Name = "btnAgregarContactoEmpresa";
            btnAgregarContactoEmpresa.Padding = new Padding(3, 0, 0, 0);
            btnAgregarContactoEmpresa.Size = new Size(150, 40);
            btnAgregarContactoEmpresa.TabIndex = 5;
            btnAgregarContactoEmpresa.Text = "Agregar";
            btnAgregarContactoEmpresa.TextAlign = ContentAlignment.MiddleLeft;
            btnAgregarContactoEmpresa.TextColor = Color.White;
            btnAgregarContactoEmpresa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarContactoEmpresa.UseVisualStyleBackColor = false;
            btnAgregarContactoEmpresa.Click += btnAgregarDemante_Click;
            // 
            // FrmAgregarContactoEmpresa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(684, 541);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmAgregarContactoEmpresa";
            Text = "Agregar Contacto de Empresa";
            Load += FrmAgregarContactoEmpresa_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            tablessControl1.ResumeLayout(false);
            tabPageBuscar.ResumeLayout(false);
            tabPageBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgContactoEmpresas).EndInit();
            tabPageAgregar.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelInformacionPersonal.ResumeLayout(false);
            panelInformacionPersonal.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Clases.TablessControl tablessControl1;
        private TabPage tabPageBuscar;
        private TabPage tabPageAgregar;
        private GroupBox groupBox1;
        private RadioButton radioButtonAgregar;
        private RadioButton radioButtonBuscar;
        private Label lblNombre;
        private TextBox txtBuscarContactoEmpresa;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton1;
        private DataGridView dtgContactoEmpresas;
        private Panel panel3;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnAgregarContactoEmpresa;
        private Panel panelInformacionPersonal;
        private Label label5;
        private Clases.RoundedButton roundedButton9;
        private TextBox txtTelefono;
        private Clases.RoundedButton roundedButton12;
        private Label label1;
        private Label lblApellido;
        private TextBox txtNombre;
        private Label label2;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private Clases.RoundedButton roundedButton4;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton6;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton btnCancelarAgregarContactoEmpresa;
        private Clases.RoundedButton btnGuardarContactoEmpresa;
        private Panel panel4;
        private Label label3;
        private TextBox txtTelefonoA;
        private Clases.RoundedButton roundedButton7;
        private Label label4;
        private TextBox txtNombreA;
        private Label label7;
        private TextBox txtCorreoA;
        private Clases.RoundedButton roundedButton8;
        private Clases.RoundedButton roundedButton10;
        private Clases.RoundedButton roundedButton11;
        private Clases.RoundedButton roundedButton13;
        private Label labelTotal;
        private Label lblPagina;
        private Clases.RoundedButton btnSiguiente;
        private Clases.RoundedButton btnAnterior;
    }
}