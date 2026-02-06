namespace Presentacion
{
    partial class FormLogin2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin2));
            panel1 = new Panel();
            btnVerPass = new Button();
            pictureBox1 = new PictureBox();
            checkBoxRememberMe = new CheckBox();
            btnLogin = new Presentacion.Clases.RoundedButton();
            txtPassword = new TextBox();
            txtUsuario = new TextBox();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnVerPass);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(checkBoxRememberMe);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtUsuario);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(roundedButton3);
            panel1.Location = new Point(55, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(247, 326);
            panel1.TabIndex = 0;
            // 
            // btnVerPass
            // 
            btnVerPass.Image = Properties.Resources.ojo_abierto;
            btnVerPass.Location = new Point(167, 177);
            btnVerPass.Name = "btnVerPass";
            btnVerPass.Size = new Size(26, 35);
            btnVerPass.TabIndex = 7;
            btnVerPass.UseVisualStyleBackColor = true;
            btnVerPass.Click += btnVerPass_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = Properties.Resources.huella_dactilar;
            pictureBox1.Image = Properties.Resources.huella_dactilar;
            pictureBox1.Location = new Point(70, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // checkBoxRememberMe
            // 
            checkBoxRememberMe.AutoSize = true;
            checkBoxRememberMe.Location = new Point(45, 235);
            checkBoxRememberMe.Name = "checkBoxRememberMe";
            checkBoxRememberMe.Size = new Size(90, 19);
            checkBoxRememberMe.TabIndex = 2;
            checkBoxRememberMe.Text = "Recordarme";
            checkBoxRememberMe.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(180, 124, 55);
            btnLogin.BackgroundColor = Color.FromArgb(180, 124, 55);
            btnLogin.BorderColor = Color.Empty;
            btnLogin.BorderRadius = 40;
            btnLogin.BorderSize = 0;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(45, 261);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 40);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Entrar";
            btnLogin.TextColor = Color.White;
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += roundedButton1_Click;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(61, 186);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "txtPassword";
            txtPassword.Size = new Size(100, 16);
            txtPassword.TabIndex = 1;
            txtPassword.Text = "Contraseña";
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(64, 129);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Size = new Size(100, 16);
            txtUsuario.TabIndex = 0;
            txtUsuario.KeyDown += txtUsuario_KeyDown;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.White;
            roundedButton2.BackgroundColor = Color.White;
            roundedButton2.BorderColor = Color.Black;
            roundedButton2.BorderRadius = 10;
            roundedButton2.BorderSize = 1;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderColor = Color.Black;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(45, 118);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(150, 40);
            roundedButton2.TabIndex = 3;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.White;
            roundedButton3.BackgroundColor = Color.White;
            roundedButton3.BorderColor = Color.Black;
            roundedButton3.BorderRadius = 10;
            roundedButton3.BorderSize = 1;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderColor = Color.Black;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(45, 174);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(150, 40);
            roundedButton3.TabIndex = 4;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 124, 55);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(357, 27);
            panel2.TabIndex = 1;
            panel2.MouseMove += panel2_MouseMove;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(307, 0);
            button2.Name = "button2";
            button2.Size = new Size(25, 27);
            button2.TabIndex = 4;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(332, 0);
            button1.Name = "button1";
            button1.Size = new Size(25, 27);
            button1.TabIndex = 5;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormLogin2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 242, 242);
            ClientSize = new Size(357, 408);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormLogin2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            TopMost = true;
            WindowState = FormWindowState.Minimized;
            Load += FormLogin2_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtPassword;
        private TextBox txtUsuario;
        private Presentacion.Clases.RoundedButton btnLogin;
        private Presentacion.Clases.RoundedButton roundedButton2;
        private Presentacion.Clases.RoundedButton roundedButton3;
        private CheckBox checkBoxRememberMe;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private Button btnVerPass;
    }
}
