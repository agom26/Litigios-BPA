namespace Presentacion.Reportes.BuscarPersonaForm
{
    partial class BuscarPersonaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuscarPersonaForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            radioButtonBuscar = new RadioButton();
            panel2 = new Panel();
            tablessControl1 = new Presentacion.Clases.TablessControl();
            tabPageBuscar = new TabPage();
            comboBoxRama = new ComboBox();
            btnSiguiente = new Presentacion.Clases.RoundedButton();
            btnAnterior = new Presentacion.Clases.RoundedButton();
            lblPagina = new Label();
            labelTotal = new Label();
            dtgPersonas = new DataGridView();
            lblNombre = new Label();
            txtBuscarContactoEmpresa = new TextBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel3 = new Panel();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            btnAgregarContactoEmpresa = new Presentacion.Clases.RoundedButton();
            label1 = new Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tablessControl1.SuspendLayout();
            tabPageBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPersonas).BeginInit();
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
            groupBox1.Controls.Add(radioButtonBuscar);
            groupBox1.Location = new Point(98, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 52);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // radioButtonBuscar
            // 
            radioButtonBuscar.AutoSize = true;
            radioButtonBuscar.Checked = true;
            radioButtonBuscar.Location = new Point(215, 20);
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
            tablessControl1.Dock = DockStyle.Fill;
            tablessControl1.Location = new Point(0, 0);
            tablessControl1.Name = "tablessControl1";
            tablessControl1.SelectedIndex = 0;
            tablessControl1.Size = new Size(684, 414);
            tablessControl1.TabIndex = 0;
            // 
            // tabPageBuscar
            // 
            tabPageBuscar.BackColor = Color.FromArgb(250, 249, 246);
            tabPageBuscar.Controls.Add(label1);
            tabPageBuscar.Controls.Add(comboBoxRama);
            tabPageBuscar.Controls.Add(btnSiguiente);
            tabPageBuscar.Controls.Add(btnAnterior);
            tabPageBuscar.Controls.Add(lblPagina);
            tabPageBuscar.Controls.Add(labelTotal);
            tabPageBuscar.Controls.Add(dtgPersonas);
            tabPageBuscar.Controls.Add(lblNombre);
            tabPageBuscar.Controls.Add(txtBuscarContactoEmpresa);
            tabPageBuscar.Controls.Add(roundedButton3);
            tabPageBuscar.Controls.Add(roundedButton1);
            tabPageBuscar.Location = new Point(4, 24);
            tabPageBuscar.Name = "tabPageBuscar";
            tabPageBuscar.Padding = new Padding(3);
            tabPageBuscar.Size = new Size(676, 386);
            tabPageBuscar.TabIndex = 0;
            tabPageBuscar.Click += tabPageBuscar_Click;
            // 
            // comboBoxRama
            // 
            comboBoxRama.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRama.Enabled = false;
            comboBoxRama.FormattingEnabled = true;
            comboBoxRama.Items.AddRange(new object[] { "Demandado", "Demandante", "Tercero Interesado", "Contacto de Empresa", "Solicitante", "Autoridad Impugnada" });
            comboBoxRama.Location = new Point(440, 35);
            comboBoxRama.Name = "comboBoxRama";
            comboBoxRama.Size = new Size(184, 23);
            comboBoxRama.TabIndex = 16;
            comboBoxRama.SelectedIndexChanged += comboBoxRama_SelectedIndexChanged;
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
            // dtgPersonas
            // 
            dtgPersonas.AllowUserToAddRows = false;
            dtgPersonas.AllowUserToDeleteRows = false;
            dtgPersonas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(249, 247, 242);
            dtgPersonas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgPersonas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPersonas.BackgroundColor = Color.White;
            dtgPersonas.BorderStyle = BorderStyle.None;
            dtgPersonas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgPersonas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgPersonas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgPersonas.ColumnHeadersHeight = 40;
            dtgPersonas.EnableHeadersVisualStyles = false;
            dtgPersonas.Location = new Point(37, 100);
            dtgPersonas.MinimumSize = new Size(400, 100);
            dtgPersonas.MultiSelect = false;
            dtgPersonas.Name = "dtgPersonas";
            dtgPersonas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgPersonas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgPersonas.RowHeadersVisible = false;
            dtgPersonas.RowHeadersWidth = 62;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dtgPersonas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgPersonas.RowTemplate.Height = 45;
            dtgPersonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgPersonas.Size = new Size(611, 215);
            dtgPersonas.TabIndex = 11;
            dtgPersonas.CellClick += dtgPersonas_CellClick;
            dtgPersonas.DataBindingComplete += dtgContactoEmpresas_DataBindingComplete;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.FromArgb(250, 249, 246);
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(26, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(204, 19);
            lblNombre.TabIndex = 7;
            lblNombre.Text = "Buscar por nombre,dirección";
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
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(250, 249, 246);
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(440, 3);
            label1.Name = "label1";
            label1.Size = new Size(118, 19);
            label1.TabIndex = 17;
            label1.Text = "Tipo de Persona";
            // 
            // BuscarPersonaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(684, 541);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BuscarPersonaForm";
            Text = "Buscar Persona";
            Load += BuscarPersonaForm_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            tablessControl1.ResumeLayout(false);
            tabPageBuscar.ResumeLayout(false);
            tabPageBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPersonas).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Clases.TablessControl tablessControl1;
        private TabPage tabPageBuscar;
        private GroupBox groupBox1;
        private RadioButton radioButtonBuscar;
        private Label lblNombre;
        private TextBox txtBuscarContactoEmpresa;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton1;
        private DataGridView dtgPersonas;
        private Panel panel3;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnAgregarContactoEmpresa;
        private Label labelTotal;
        private Label lblPagina;
        private Clases.RoundedButton btnSiguiente;
        private Clases.RoundedButton btnAnterior;
        private ComboBox comboBoxRama;
        private Label label1;
    }
}