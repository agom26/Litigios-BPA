namespace Presentacion.Casos.Participantes
{
    partial class FrmAgregarDemandado
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarDemandado));
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            radioButtonAgregar = new RadioButton();
            radioButtonBuscar = new RadioButton();
            panel2 = new Panel();
            tablessControl1 = new Presentacion.Clases.TablessControl();
            tabPageBuscar = new TabPage();
            dtgDemandados = new DataGridView();
            lblNombre = new Label();
            txtBuscarDemandado = new TextBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            tabPageAgregar = new TabPage();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tablessControl1.SuspendLayout();
            tabPageBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgDemandados).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 88);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonAgregar);
            groupBox1.Controls.Add(radioButtonBuscar);
            groupBox1.Location = new Point(52, 22);
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
            radioButtonAgregar.TabIndex = 1;
            radioButtonAgregar.TabStop = true;
            radioButtonAgregar.Text = "Agregar";
            radioButtonAgregar.UseVisualStyleBackColor = true;
            radioButtonAgregar.CheckedChanged += radioButtonAgregar_CheckedChanged;
            // 
            // radioButtonBuscar
            // 
            radioButtonBuscar.AutoSize = true;
            radioButtonBuscar.Location = new Point(139, 20);
            radioButtonBuscar.Name = "radioButtonBuscar";
            radioButtonBuscar.Size = new Size(60, 19);
            radioButtonBuscar.TabIndex = 0;
            radioButtonBuscar.TabStop = true;
            radioButtonBuscar.Text = "Buscar";
            radioButtonBuscar.UseVisualStyleBackColor = true;
            radioButtonBuscar.CheckedChanged += radioButtonBuscar_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(tablessControl1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 88);
            panel2.Name = "panel2";
            panel2.Size = new Size(600, 373);
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
            tablessControl1.Size = new Size(600, 373);
            tablessControl1.TabIndex = 0;
            // 
            // tabPageBuscar
            // 
            tabPageBuscar.BackColor = Color.FromArgb(250, 249, 246);
            tabPageBuscar.Controls.Add(dtgDemandados);
            tabPageBuscar.Controls.Add(lblNombre);
            tabPageBuscar.Controls.Add(txtBuscarDemandado);
            tabPageBuscar.Controls.Add(roundedButton3);
            tabPageBuscar.Controls.Add(roundedButton1);
            tabPageBuscar.Location = new Point(4, 24);
            tabPageBuscar.Name = "tabPageBuscar";
            tabPageBuscar.Padding = new Padding(3);
            tabPageBuscar.Size = new Size(592, 345);
            tabPageBuscar.TabIndex = 0;
            // 
            // dtgDemandados
            // 
            dtgDemandados.AllowUserToAddRows = false;
            dtgDemandados.AllowUserToDeleteRows = false;
            dtgDemandados.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(249, 247, 242);
            dtgDemandados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgDemandados.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgDemandados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgDemandados.BackgroundColor = Color.White;
            dtgDemandados.BorderStyle = BorderStyle.None;
            dtgDemandados.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dtgDemandados.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(250, 248, 245);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgDemandados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgDemandados.ColumnHeadersHeight = 40;
            dtgDemandados.EnableHeadersVisualStyles = false;
            dtgDemandados.Location = new Point(28, 83);
            dtgDemandados.MinimumSize = new Size(400, 100);
            dtgDemandados.MultiSelect = false;
            dtgDemandados.Name = "dtgDemandados";
            dtgDemandados.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgDemandados.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgDemandados.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dtgDemandados.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgDemandados.RowTemplate.Height = 45;
            dtgDemandados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgDemandados.Size = new Size(536, 232);
            dtgDemandados.TabIndex = 11;
            dtgDemandados.DataBindingComplete += dtgDemandados_DataBindingComplete;
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top;
            lblNombre.AutoSize = true;
            lblNombre.BackColor = Color.FromArgb(250, 249, 246);
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(28, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(217, 19);
            lblNombre.TabIndex = 7;
            lblNombre.Text = "Buscar por nombre o dirección";
            // 
            // txtBuscarDemandado
            // 
            txtBuscarDemandado.Anchor = AnchorStyles.Top;
            txtBuscarDemandado.BorderStyle = BorderStyle.None;
            txtBuscarDemandado.Font = new Font("Segoe UI", 9F);
            txtBuscarDemandado.Location = new Point(61, 38);
            txtBuscarDemandado.Name = "txtBuscarDemandado";
            txtBuscarDemandado.PlaceholderText = "...";
            txtBuscarDemandado.Size = new Size(346, 16);
            txtBuscarDemandado.TabIndex = 6;
            txtBuscarDemandado.KeyDown += txtBuscarDemandado_KeyDown;
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
            roundedButton1.Anchor = AnchorStyles.Top;
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
            roundedButton1.Location = new Point(17, 71);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(556, 256);
            roundedButton1.TabIndex = 10;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // tabPageAgregar
            // 
            tabPageAgregar.BackColor = Color.FromArgb(250, 249, 246);
            tabPageAgregar.Location = new Point(4, 24);
            tabPageAgregar.Name = "tabPageAgregar";
            tabPageAgregar.Padding = new Padding(3);
            tabPageAgregar.Size = new Size(592, 345);
            tabPageAgregar.TabIndex = 1;
            // 
            // FrmAgregarDemandado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(600, 461);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmAgregarDemandado";
            Text = "Agregar Demandante";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            tablessControl1.ResumeLayout(false);
            tabPageBuscar.ResumeLayout(false);
            tabPageBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgDemandados).EndInit();
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
        private TextBox txtBuscarDemandado;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton1;
        private DataGridView dtgDemandados;
    }
}