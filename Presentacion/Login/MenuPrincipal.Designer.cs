namespace Presentacion
{
    partial class MenuPrincipal
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
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Inicio", 12, 12);
            TreeNode treeNode2 = new TreeNode("Primera Instancia");
            TreeNode treeNode3 = new TreeNode("Segunda Instancia");
            TreeNode treeNode4 = new TreeNode("Laboral", 1, 1, new TreeNode[] { treeNode2, treeNode3 });
            TreeNode treeNode5 = new TreeNode("Juicio Sumario");
            TreeNode treeNode6 = new TreeNode("Proceso de ejecución");
            TreeNode treeNode7 = new TreeNode("Civil", 2, 2, new TreeNode[] { treeNode5, treeNode6 });
            TreeNode treeNode8 = new TreeNode("Constitucional", 3, 3);
            TreeNode treeNode9 = new TreeNode("Administrativo");
            TreeNode treeNode10 = new TreeNode("Administrativo Tributario");
            TreeNode treeNode11 = new TreeNode("Contencioso Administrativo", 4, 4, new TreeNode[] { treeNode9, treeNode10 });
            TreeNode treeNode12 = new TreeNode("Demandados / Autoridades resp.", 7, 7);
            TreeNode treeNode13 = new TreeNode("Demandantes / Actores", 8, 8);
            TreeNode treeNode14 = new TreeNode("Terceros interesados", 10, 10);
            TreeNode treeNode15 = new TreeNode("Abogado responsable", 9, 9);
            TreeNode treeNode16 = new TreeNode("Participantes del caso", 5, 5, new TreeNode[] { treeNode12, treeNode13, treeNode14, treeNode15 });
            TreeNode treeNode17 = new TreeNode("Reportes", 11, 11);
            TreeNode treeNode18 = new TreeNode("Usuarios", 6, 6);
            TreeNode treeNode19 = new TreeNode("Vencimientos", 13, 13);
            TreeNode treeNode20 = new TreeNode("Plazos", 14, 14);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            treeView1 = new TreeView();
            imageList1 = new ImageList(components);
            panelChildForm = new Panel();
            panel1 = new Panel();
            lblUsuario = new Label();
            lblNombre = new Label();
            btnLogOut = new Presentacion.Clases.RoundedButton();
            pictureBoxUser = new PictureBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.Dock = DockStyle.Left;
            treeView1.ImageIndex = 0;
            treeView1.ImageList = imageList1;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 12;
            treeNode1.Name = "Nodo1";
            treeNode1.SelectedImageIndex = 12;
            treeNode1.Text = "Inicio";
            treeNode2.Name = "Nodo8";
            treeNode2.Text = "Primera Instancia";
            treeNode3.Name = "Nodo14";
            treeNode3.Text = "Segunda Instancia";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Nodo9";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "Laboral";
            treeNode5.Name = "Nodo15";
            treeNode5.Text = "Juicio Sumario";
            treeNode6.Name = "Nodo16";
            treeNode6.Text = "Proceso de ejecución";
            treeNode7.ImageIndex = 2;
            treeNode7.Name = "Nodo10";
            treeNode7.SelectedImageIndex = 2;
            treeNode7.Text = "Civil";
            treeNode8.ImageIndex = 3;
            treeNode8.Name = "Nodo11";
            treeNode8.SelectedImageIndex = 3;
            treeNode8.Text = "Constitucional";
            treeNode9.Name = "Nodo4";
            treeNode9.Text = "Administrativo";
            treeNode10.Name = "Nodo19";
            treeNode10.Text = "Administrativo Tributario";
            treeNode11.ImageIndex = 4;
            treeNode11.Name = "Nodo12";
            treeNode11.SelectedImageIndex = 4;
            treeNode11.Text = "Contencioso Administrativo";
            treeNode12.ImageIndex = 7;
            treeNode12.Name = "Nodo6";
            treeNode12.SelectedImageIndex = 7;
            treeNode12.Text = "Demandados / Autoridades resp.";
            treeNode13.ImageIndex = 8;
            treeNode13.Name = "Nodo7";
            treeNode13.SelectedImageIndex = 8;
            treeNode13.Text = "Demandantes / Actores";
            treeNode14.ImageIndex = 10;
            treeNode14.Name = "Nodo17";
            treeNode14.SelectedImageIndex = 10;
            treeNode14.Text = "Terceros interesados";
            treeNode15.ImageIndex = 9;
            treeNode15.Name = "Nodo18";
            treeNode15.SelectedImageIndex = 9;
            treeNode15.Text = "Abogado responsable";
            treeNode16.ImageIndex = 5;
            treeNode16.Name = "Nodo5";
            treeNode16.SelectedImageIndex = 5;
            treeNode16.Text = "Participantes del caso";
            treeNode17.ImageIndex = 11;
            treeNode17.Name = "Nodo0";
            treeNode17.SelectedImageIndex = 11;
            treeNode17.Text = "Reportes";
            treeNode18.ImageIndex = 6;
            treeNode18.Name = "Nodo13";
            treeNode18.SelectedImageIndex = 6;
            treeNode18.Text = "Usuarios";
            treeNode19.ImageIndex = 13;
            treeNode19.Name = "Nodo2";
            treeNode19.SelectedImageIndex = 13;
            treeNode19.Text = "Vencimientos";
            treeNode20.ImageIndex = 14;
            treeNode20.Name = "Nodo3";
            treeNode20.SelectedImageIndex = 14;
            treeNode20.Text = "Plazos";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode4, treeNode7, treeNode8, treeNode11, treeNode16, treeNode17, treeNode18, treeNode19, treeNode20 });
            treeView1.SelectedImageIndex = 0;
            treeView1.Size = new Size(242, 360);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "icons8-folder-24.png");
            imageList1.Images.SetKeyName(1, "laboral");
            imageList1.Images.SetKeyName(2, "civil");
            imageList1.Images.SetKeyName(3, "constitucional");
            imageList1.Images.SetKeyName(4, "contencioso");
            imageList1.Images.SetKeyName(5, "personas");
            imageList1.Images.SetKeyName(6, "usuarios");
            imageList1.Images.SetKeyName(7, "icons8-prisoner-24.png");
            imageList1.Images.SetKeyName(8, "icons8-court-24 (1).png");
            imageList1.Images.SetKeyName(9, "icons8-briefcase-24.png");
            imageList1.Images.SetKeyName(10, "icons8-handshake-24.png");
            imageList1.Images.SetKeyName(11, "reportes");
            imageList1.Images.SetKeyName(12, "inicio");
            imageList1.Images.SetKeyName(13, "vencimientos");
            imageList1.Images.SetKeyName(14, "plazos");
            // 
            // panelChildForm
            // 
            panelChildForm.BackColor = Color.FromArgb(208, 221, 238);
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(243, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(908, 475);
            panelChildForm.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblUsuario);
            panel1.Controls.Add(lblNombre);
            panel1.Controls.Add(btnLogOut);
            panel1.Controls.Add(pictureBoxUser);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 360);
            panel1.Name = "panel1";
            panel1.Size = new Size(243, 115);
            panel1.TabIndex = 2;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F);
            lblUsuario.Location = new Point(75, 36);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(38, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "label1";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.Location = new Point(75, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(38, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "label1";
            // 
            // btnLogOut
            // 
            btnLogOut.BackColor = Color.FromArgb(44, 40, 36);
            btnLogOut.BackgroundColor = Color.FromArgb(44, 40, 36);
            btnLogOut.BorderColor = Color.Empty;
            btnLogOut.BorderRadius = 25;
            btnLogOut.BorderSize = 1;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogOut.ForeColor = Color.White;
            btnLogOut.Image = Properties.Resources.salida;
            btnLogOut.ImageAlign = ContentAlignment.MiddleRight;
            btnLogOut.Location = new Point(12, 63);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(215, 40);
            btnLogOut.TabIndex = 1;
            btnLogOut.Text = "    CERRAR SESIÓN";
            btnLogOut.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut.TextColor = Color.White;
            btnLogOut.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Click += roundedButton1_Click;
            // 
            // pictureBoxUser
            // 
            pictureBoxUser.Image = Properties.Resources.perfil__1_;
            pictureBoxUser.InitialImage = Properties.Resources.perfil;
            pictureBoxUser.Location = new Point(21, 3);
            pictureBoxUser.Name = "pictureBoxUser";
            pictureBoxUser.Size = new Size(48, 48);
            pictureBoxUser.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUser.TabIndex = 0;
            pictureBoxUser.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(treeView1);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(243, 475);
            panel2.TabIndex = 3;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1151, 475);
            Controls.Add(panelChildForm);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuPrincipal";
            WindowState = FormWindowState.Maximized;
            FormClosing += MenuPrincipal_FormClosing_1;
            Load += MenuPrincipal_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView1;
        private ImageList imageList1;
        private Panel panelChildForm;
        private Panel panel1;
        private PictureBox pictureBoxUser;
        private Clases.RoundedButton btnLogOut;
        private Label lblNombre;
        private Label lblUsuario;
        private Panel panel2;
    }
}