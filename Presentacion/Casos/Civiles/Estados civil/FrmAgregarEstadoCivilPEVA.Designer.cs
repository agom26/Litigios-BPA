namespace Presentacion.Casos.Civiles.Estados_civil
{
    partial class FrmAgregarEstadoCivilPEVA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarEstadoCivilPEVA));
            panelDetalles = new Panel();
            dateTimePickerFecha3 = new DateTimePicker();
            labelFecha3 = new Label();
            dateTimePickerFecha2 = new DateTimePicker();
            labelFecha2 = new Label();
            dateTimePickerFecha1 = new DateTimePicker();
            labelFecha1 = new Label();
            dateTimePickerFechaEstado = new DateTimePicker();
            label1 = new Label();
            label16 = new Label();
            txtObservaciones = new TextBox();
            roundedButton23 = new Presentacion.Clases.RoundedButton();
            comboboxEstado = new ComboBox();
            label2 = new Label();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            label3 = new Label();
            label4 = new Label();
            panelVencimiento = new Panel();
            dateTimePickerFechaVencimiento = new DateTimePicker();
            dateTimePickerHoraVencimiento = new DateTimePicker();
            label6 = new Label();
            label5 = new Label();
            checkBoxTieneVencimiento = new CheckBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            btnGuardarUsuario = new Presentacion.Clases.RoundedButton();
            panelDetalles.SuspendLayout();
            panelVencimiento.SuspendLayout();
            SuspendLayout();
            // 
            // panelDetalles
            // 
            panelDetalles.Controls.Add(dateTimePickerFecha3);
            panelDetalles.Controls.Add(labelFecha3);
            panelDetalles.Controls.Add(dateTimePickerFecha2);
            panelDetalles.Controls.Add(labelFecha2);
            panelDetalles.Controls.Add(dateTimePickerFecha1);
            panelDetalles.Controls.Add(labelFecha1);
            panelDetalles.Controls.Add(dateTimePickerFechaEstado);
            panelDetalles.Controls.Add(label1);
            panelDetalles.Controls.Add(label16);
            panelDetalles.Controls.Add(txtObservaciones);
            panelDetalles.Controls.Add(roundedButton23);
            panelDetalles.Controls.Add(comboboxEstado);
            panelDetalles.Controls.Add(label2);
            panelDetalles.Controls.Add(roundedButton1);
            panelDetalles.Location = new Point(12, 51);
            panelDetalles.Name = "panelDetalles";
            panelDetalles.Size = new Size(448, 274);
            panelDetalles.TabIndex = 0;
            // 
            // dateTimePickerFecha3
            // 
            dateTimePickerFecha3.Format = DateTimePickerFormat.Short;
            dateTimePickerFecha3.Location = new Point(308, 108);
            dateTimePickerFecha3.Name = "dateTimePickerFecha3";
            dateTimePickerFecha3.Size = new Size(100, 23);
            dateTimePickerFecha3.TabIndex = 22;
            dateTimePickerFecha3.ValueChanged += dateTimePickerFecha3_ValueChanged;
            // 
            // labelFecha3
            // 
            labelFecha3.Anchor = AnchorStyles.Top;
            labelFecha3.AutoSize = true;
            labelFecha3.BackColor = Color.White;
            labelFecha3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFecha3.Location = new Point(308, 86);
            labelFecha3.Name = "labelFecha3";
            labelFecha3.Size = new Size(59, 19);
            labelFecha3.TabIndex = 23;
            labelFecha3.Text = "Fecha 3";
            // 
            // dateTimePickerFecha2
            // 
            dateTimePickerFecha2.Format = DateTimePickerFormat.Short;
            dateTimePickerFecha2.Location = new Point(175, 108);
            dateTimePickerFecha2.Name = "dateTimePickerFecha2";
            dateTimePickerFecha2.Size = new Size(100, 23);
            dateTimePickerFecha2.TabIndex = 20;
            dateTimePickerFecha2.ValueChanged += dateTimePickerFecha2_ValueChanged;
            // 
            // labelFecha2
            // 
            labelFecha2.Anchor = AnchorStyles.Top;
            labelFecha2.AutoSize = true;
            labelFecha2.BackColor = Color.White;
            labelFecha2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFecha2.Location = new Point(175, 86);
            labelFecha2.Name = "labelFecha2";
            labelFecha2.Size = new Size(59, 19);
            labelFecha2.TabIndex = 21;
            labelFecha2.Text = "Fecha 2";
            // 
            // dateTimePickerFecha1
            // 
            dateTimePickerFecha1.Format = DateTimePickerFormat.Short;
            dateTimePickerFecha1.Location = new Point(33, 108);
            dateTimePickerFecha1.Name = "dateTimePickerFecha1";
            dateTimePickerFecha1.Size = new Size(100, 23);
            dateTimePickerFecha1.TabIndex = 18;
            dateTimePickerFecha1.ValueChanged += dateTimePickerFecha1_ValueChanged;
            // 
            // labelFecha1
            // 
            labelFecha1.Anchor = AnchorStyles.Top;
            labelFecha1.AutoSize = true;
            labelFecha1.BackColor = Color.White;
            labelFecha1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFecha1.Location = new Point(33, 86);
            labelFecha1.Name = "labelFecha1";
            labelFecha1.Size = new Size(59, 19);
            labelFecha1.TabIndex = 19;
            labelFecha1.Text = "Fecha 1";
            // 
            // dateTimePickerFechaEstado
            // 
            dateTimePickerFechaEstado.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaEstado.Location = new Point(33, 51);
            dateTimePickerFechaEstado.Name = "dateTimePickerFechaEstado";
            dateTimePickerFechaEstado.Size = new Size(100, 23);
            dateTimePickerFechaEstado.TabIndex = 1;
            dateTimePickerFechaEstado.ValueChanged += dateTimePickerFechaEstado_ValueChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(33, 29);
            label1.Name = "label1";
            label1.Size = new Size(47, 19);
            label1.TabIndex = 15;
            label1.Text = "Fecha";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top;
            label16.AutoSize = true;
            label16.BackColor = Color.White;
            label16.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label16.Location = new Point(33, 146);
            label16.Name = "label16";
            label16.Size = new Size(108, 19);
            label16.TabIndex = 12;
            label16.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Anchor = AnchorStyles.Top;
            txtObservaciones.BorderStyle = BorderStyle.None;
            txtObservaciones.Location = new Point(43, 179);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.PlaceholderText = "...";
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(358, 62);
            txtObservaciones.TabIndex = 3;
            // 
            // roundedButton23
            // 
            roundedButton23.Anchor = AnchorStyles.Top;
            roundedButton23.BackColor = Color.White;
            roundedButton23.BackgroundColor = Color.White;
            roundedButton23.BorderColor = Color.LightGray;
            roundedButton23.BorderRadius = 10;
            roundedButton23.BorderSize = 1;
            roundedButton23.Enabled = false;
            roundedButton23.FlatAppearance.BorderSize = 0;
            roundedButton23.FlatStyle = FlatStyle.Flat;
            roundedButton23.ForeColor = Color.White;
            roundedButton23.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton23.Location = new Point(33, 168);
            roundedButton23.Name = "roundedButton23";
            roundedButton23.Size = new Size(375, 83);
            roundedButton23.TabIndex = 14;
            roundedButton23.TextColor = Color.White;
            roundedButton23.UseVisualStyleBackColor = false;
            // 
            // comboboxEstado
            // 
            comboboxEstado.Anchor = AnchorStyles.Top;
            comboboxEstado.DropDownHeight = 200;
            comboboxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboboxEstado.DropDownWidth = 500;
            comboboxEstado.Font = new Font("Segoe UI", 9F);
            comboboxEstado.FormattingEnabled = true;
            comboboxEstado.IntegralHeight = false;
            comboboxEstado.ItemHeight = 15;
            comboboxEstado.Items.AddRange(new object[] { "Demanda presentada", "Admisión de demanda y mandamiento de ejecución", "Excepciones", "Excepciones/Apertura a prueba de incidente", "Orden de remate", "Publicación de orden de remate", "Remate", "Incidente de liquidación", "Incidente de liquidación/Recurso de Apelación", "Incidente de liquidación/Recurso de Ampliación y Aclaración", "Incidente de liquidación/Recurso de Ampliación y Aclaración/Apertura a prueba", "Traslativa de dominio", "Entrega de bienes" });
            comboboxEstado.Location = new Point(158, 53);
            comboboxEstado.Name = "comboboxEstado";
            comboboxEstado.Size = new Size(250, 23);
            comboboxEstado.TabIndex = 2;
            comboboxEstado.SelectedValueChanged += comboboxEstado_SelectedValueChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(158, 29);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 9;
            label2.Text = "Estado";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.White;
            roundedButton1.BackgroundColor = Color.White;
            roundedButton1.BorderColor = Color.Black;
            roundedButton1.BorderRadius = 35;
            roundedButton1.BorderSize = 0;
            roundedButton1.Dock = DockStyle.Fill;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(0, 0);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(448, 274);
            roundedButton1.TabIndex = 17;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(250, 249, 246);
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(12, 19);
            label3.Name = "label3";
            label3.Size = new Size(135, 19);
            label3.TabIndex = 16;
            label3.Text = "Detalles del Estado";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(250, 249, 246);
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(12, 346);
            label4.Name = "label4";
            label4.Size = new Size(91, 19);
            label4.TabIndex = 17;
            label4.Text = "Vencimiento";
            // 
            // panelVencimiento
            // 
            panelVencimiento.Controls.Add(dateTimePickerFechaVencimiento);
            panelVencimiento.Controls.Add(dateTimePickerHoraVencimiento);
            panelVencimiento.Controls.Add(label6);
            panelVencimiento.Controls.Add(label5);
            panelVencimiento.Controls.Add(checkBoxTieneVencimiento);
            panelVencimiento.Controls.Add(roundedButton3);
            panelVencimiento.Location = new Point(12, 368);
            panelVencimiento.Name = "panelVencimiento";
            panelVencimiento.Size = new Size(448, 97);
            panelVencimiento.TabIndex = 18;
            // 
            // dateTimePickerFechaVencimiento
            // 
            dateTimePickerFechaVencimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaVencimiento.Location = new Point(165, 61);
            dateTimePickerFechaVencimiento.Name = "dateTimePickerFechaVencimiento";
            dateTimePickerFechaVencimiento.Size = new Size(100, 23);
            dateTimePickerFechaVencimiento.TabIndex = 5;
            dateTimePickerFechaVencimiento.ValueChanged += dateTimePickerFechaVencimiento_ValueChanged;
            // 
            // dateTimePickerHoraVencimiento
            // 
            dateTimePickerHoraVencimiento.CalendarForeColor = Color.Black;
            dateTimePickerHoraVencimiento.CalendarMonthBackground = Color.White;
            dateTimePickerHoraVencimiento.CustomFormat = "hh:mm tt";
            dateTimePickerHoraVencimiento.Format = DateTimePickerFormat.Custom;
            dateTimePickerHoraVencimiento.Location = new Point(292, 61);
            dateTimePickerHoraVencimiento.Name = "dateTimePickerHoraVencimiento";
            dateTimePickerHoraVencimiento.ShowUpDown = true;
            dateTimePickerHoraVencimiento.Size = new Size(109, 23);
            dateTimePickerHoraVencimiento.TabIndex = 6;
            dateTimePickerHoraVencimiento.Tag = "";
            dateTimePickerHoraVencimiento.ValueChanged += dateTimePickerHoraVencimiento_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.Location = new Point(292, 39);
            label6.Name = "label6";
            label6.Size = new Size(43, 19);
            label6.TabIndex = 19;
            label6.Text = "Hora";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(165, 39);
            label5.Name = "label5";
            label5.Size = new Size(47, 19);
            label5.TabIndex = 17;
            label5.Text = "Fecha";
            // 
            // checkBoxTieneVencimiento
            // 
            checkBoxTieneVencimiento.AutoSize = true;
            checkBoxTieneVencimiento.BackColor = Color.White;
            checkBoxTieneVencimiento.Enabled = false;
            checkBoxTieneVencimiento.Location = new Point(33, 12);
            checkBoxTieneVencimiento.Name = "checkBoxTieneVencimiento";
            checkBoxTieneVencimiento.Size = new Size(134, 19);
            checkBoxTieneVencimiento.TabIndex = 4;
            checkBoxTieneVencimiento.Text = "¿Tiene Vencimiento?";
            checkBoxTieneVencimiento.UseVisualStyleBackColor = false;
            checkBoxTieneVencimiento.CheckedChanged += checkBoxTieneVencimiento_CheckedChanged;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.White;
            roundedButton3.BackgroundColor = Color.White;
            roundedButton3.BorderColor = Color.Empty;
            roundedButton3.BorderRadius = 40;
            roundedButton3.BorderSize = 0;
            roundedButton3.Dock = DockStyle.Fill;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(0, 0);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(448, 97);
            roundedButton3.TabIndex = 21;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
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
            btnCancelar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(207, 482);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(3, 0, 0, 0);
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarUsuario
            // 
            btnGuardarUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardarUsuario.BackColor = Color.FromArgb(52, 109, 235);
            btnGuardarUsuario.BackgroundColor = Color.FromArgb(52, 109, 235);
            btnGuardarUsuario.BorderColor = Color.Empty;
            btnGuardarUsuario.BorderRadius = 10;
            btnGuardarUsuario.BorderSize = 1;
            btnGuardarUsuario.FlatAppearance.BorderSize = 0;
            btnGuardarUsuario.FlatStyle = FlatStyle.Flat;
            btnGuardarUsuario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnGuardarUsuario.ForeColor = Color.White;
            btnGuardarUsuario.Image = Properties.Resources.boton_guardar;
            btnGuardarUsuario.ImageAlign = ContentAlignment.MiddleRight;
            btnGuardarUsuario.Location = new Point(340, 482);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Padding = new Padding(3, 0, 0, 0);
            btnGuardarUsuario.Size = new Size(120, 40);
            btnGuardarUsuario.TabIndex = 8;
            btnGuardarUsuario.Text = " Agregar";
            btnGuardarUsuario.TextAlign = ContentAlignment.MiddleLeft;
            btnGuardarUsuario.TextColor = Color.White;
            btnGuardarUsuario.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarUsuario.UseVisualStyleBackColor = false;
            btnGuardarUsuario.Click += btnGuardarUsuario_Click;
            // 
            // FrmAgregarEstadoCivilPEVA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(484, 548);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardarUsuario);
            Controls.Add(panelVencimiento);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panelDetalles);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmAgregarEstadoCivilPEVA";
            Text = "AGREGAR ESTADO";
            panelDetalles.ResumeLayout(false);
            panelDetalles.PerformLayout();
            panelVencimiento.ResumeLayout(false);
            panelVencimiento.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDetalles;
        private ComboBox comboboxEstado;
        private Label label2;
        private Label label16;
        private TextBox txtObservaciones;
        private Clases.RoundedButton roundedButton23;
        private Label label1;
        private DateTimePicker dateTimePickerFechaEstado;
        private Label label3;
        private Clases.RoundedButton roundedButton1;
        private Clases.RoundedButton roundedButton2;
        private Label label4;
        private Panel panelVencimiento;
        private CheckBox checkBoxTieneVencimiento;
        private DateTimePicker dateTimePickerFecha2;
        private Label label5;
        private DateTimePicker dateTimePickerHoraVencimiento;
        private Label label6;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnGuardarUsuario;
        private DateTimePicker dateTimePickerFechaVencimiento;
        private DateTimePicker dateTimePickerFecha1;
        private Label labelFecha1;
        private DateTimePicker dateTimePickerFecha3;
        private Label labelFecha3;
        private Label labelFecha2;
    }
}