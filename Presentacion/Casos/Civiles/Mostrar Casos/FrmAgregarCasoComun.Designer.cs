namespace Presentacion.Casos.Civiles.Mostrar_Casos
{
    partial class FrmAgregarCasoComun
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarCasoComun));
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
            btnSiguiente = new Presentacion.Clases.RoundedButton();
            btnAnterior = new Presentacion.Clases.RoundedButton();
            lblPagina = new Label();
            labelTotal = new Label();
            dtgContactoEmpresas = new DataGridView();
            lblNombre = new Label();
            txtBuscarContactoEmpresa = new TextBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel3 = new Panel();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            btnAgregarContactoEmpresa = new Presentacion.Clases.RoundedButton();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tablessControl1.SuspendLayout();
            tabPageBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgContactoEmpresas).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(977, 107);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonBuscar);
            groupBox1.Location = new Point(140, 13);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(700, 87);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // radioButtonBuscar
            // 
            radioButtonBuscar.AutoSize = true;
            radioButtonBuscar.Checked = true;
            radioButtonBuscar.Location = new Point(307, 33);
            radioButtonBuscar.Margin = new Padding(4, 5, 4, 5);
            radioButtonBuscar.Name = "radioButtonBuscar";
            radioButtonBuscar.Size = new Size(88, 29);
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
            panel2.Location = new Point(0, 107);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 690);
            panel2.TabIndex = 1;
            // 
            // tablessControl1
            // 
            tablessControl1.Controls.Add(tabPageBuscar);
            tablessControl1.Dock = DockStyle.Fill;
            tablessControl1.Location = new Point(0, 0);
            tablessControl1.Margin = new Padding(4, 5, 4, 5);
            tablessControl1.Name = "tablessControl1";
            tablessControl1.SelectedIndex = 0;
            tablessControl1.Size = new Size(977, 690);
            tablessControl1.TabIndex = 0;
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
            tabPageBuscar.Location = new Point(4, 34);
            tabPageBuscar.Margin = new Padding(4, 5, 4, 5);
            tabPageBuscar.Name = "tabPageBuscar";
            tabPageBuscar.Padding = new Padding(4, 5, 4, 5);
            tabPageBuscar.Size = new Size(969, 652);
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
            btnSiguiente.Location = new Point(814, 555);
            btnSiguiente.Margin = new Padding(4, 5, 4, 5);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Padding = new Padding(4, 0, 0, 0);
            btnSiguiente.Size = new Size(124, 67);
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
            btnAnterior.Location = new Point(663, 555);
            btnAnterior.Margin = new Padding(4, 5, 4, 5);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Padding = new Padding(4, 0, 0, 0);
            btnAnterior.Size = new Size(124, 67);
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
            lblPagina.Location = new Point(837, 117);
            lblPagina.Margin = new Padding(4, 0, 4, 0);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(59, 25);
            lblPagina.TabIndex = 13;
            lblPagina.Text = "label2";
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Location = new Point(37, 117);
            labelTotal.Margin = new Padding(4, 0, 4, 0);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(59, 25);
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
            dtgContactoEmpresas.Location = new Point(53, 167);
            dtgContactoEmpresas.Margin = new Padding(4, 5, 4, 5);
            dtgContactoEmpresas.MinimumSize = new Size(571, 167);
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
            dtgContactoEmpresas.RowHeadersWidth = 62;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dtgContactoEmpresas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgContactoEmpresas.RowTemplate.Height = 45;
            dtgContactoEmpresas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgContactoEmpresas.Size = new Size(873, 358);
            dtgContactoEmpresas.TabIndex = 11;
            dtgContactoEmpresas.CellClick += dtgContactoEmpresas_CellClick;
            dtgContactoEmpresas.DataBindingComplete += dtgContactoEmpresas_DataBindingComplete;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.FromArgb(250, 249, 246);
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(37, 5);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(500, 28);
            lblNombre.TabIndex = 7;
            lblNombre.Text = "Buscar por expediente,  oficial, notificador, juzgado";
            // 
            // txtBuscarContactoEmpresa
            // 
            txtBuscarContactoEmpresa.BorderStyle = BorderStyle.None;
            txtBuscarContactoEmpresa.Font = new Font("Segoe UI", 9F);
            txtBuscarContactoEmpresa.Location = new Point(87, 63);
            txtBuscarContactoEmpresa.Margin = new Padding(4, 5, 4, 5);
            txtBuscarContactoEmpresa.Name = "txtBuscarContactoEmpresa";
            txtBuscarContactoEmpresa.PlaceholderText = "...";
            txtBuscarContactoEmpresa.Size = new Size(494, 24);
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
            roundedButton3.Location = new Point(37, 42);
            roundedButton3.Margin = new Padding(4, 5, 4, 5);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(557, 67);
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
            roundedButton1.Location = new Point(37, 147);
            roundedButton1.Margin = new Padding(4, 5, 4, 5);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(901, 398);
            roundedButton1.TabIndex = 10;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnAgregarContactoEmpresa);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 797);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(977, 105);
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
            btnCancelar.Location = new Point(469, 18);
            btnCancelar.Margin = new Padding(4, 5, 4, 5);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(4, 0, 0, 0);
            btnCancelar.Size = new Size(214, 67);
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
            btnAgregarContactoEmpresa.Location = new Point(730, 18);
            btnAgregarContactoEmpresa.Margin = new Padding(4, 5, 4, 5);
            btnAgregarContactoEmpresa.Name = "btnAgregarContactoEmpresa";
            btnAgregarContactoEmpresa.Padding = new Padding(4, 0, 0, 0);
            btnAgregarContactoEmpresa.Size = new Size(214, 67);
            btnAgregarContactoEmpresa.TabIndex = 5;
            btnAgregarContactoEmpresa.Text = "Agregar";
            btnAgregarContactoEmpresa.TextAlign = ContentAlignment.MiddleLeft;
            btnAgregarContactoEmpresa.TextColor = Color.White;
            btnAgregarContactoEmpresa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarContactoEmpresa.UseVisualStyleBackColor = false;
            btnAgregarContactoEmpresa.Click += btnAgregarDemante_Click;
            // 
            // FrmAgregarCasoComun
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(977, 902);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmAgregarCasoComun";
            Text = "Agregar Caso Referencia";
            Load += FrmAgregarCasoComun_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            tablessControl1.ResumeLayout(false);
            tabPageBuscar.ResumeLayout(false);
            tabPageBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgContactoEmpresas).EndInit();
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
        private DataGridView dtgContactoEmpresas;
        private Panel panel3;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnAgregarContactoEmpresa;
        private Label labelTotal;
        private Label lblPagina;
        private Clases.RoundedButton btnSiguiente;
        private Clases.RoundedButton btnAnterior;
    }
}