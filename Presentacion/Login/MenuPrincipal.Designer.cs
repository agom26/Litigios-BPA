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
            panelDatosUsuarioContraido = new Panel();
            pictureBoxUserContraido = new PictureBox();
            btnLogOut2 = new Presentacion.Clases.RoundedButton();
            panelDatosUsuarioExpandido = new Panel();
            lblUsuario = new Label();
            pictureBoxUser = new PictureBox();
            lblNombre = new Label();
            btnLogOut = new Presentacion.Clases.RoundedButton();
            panelDatosUsuario = new Panel();
            panelMenu = new Panel();
            panelTreeView = new Panel();
            panelColapsarMenu = new Panel();
            btnMenu = new Presentacion.Clases.RoundedButton();
            timerMenu = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            panelDatosUsuarioContraido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserContraido).BeginInit();
            panelDatosUsuarioExpandido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).BeginInit();
            panelDatosUsuario.SuspendLayout();
            panelMenu.SuspendLayout();
            panelTreeView.SuspendLayout();
            panelColapsarMenu.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.White;
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
            treeView1.Size = new Size(264, 323);
            treeView1.TabIndex = 0;
            treeView1.DrawNode += treeView1_DrawNode;
            treeView1.NodeMouseHover += treeView1_NodeMouseHover;
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
            panelChildForm.Location = new Point(265, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(886, 475);
            panelChildForm.TabIndex = 1;
            // 
            // panelDatosUsuarioContraido
            // 
            panelDatosUsuarioContraido.BackColor = Color.White;
            panelDatosUsuarioContraido.Controls.Add(pictureBoxUserContraido);
            panelDatosUsuarioContraido.Controls.Add(btnLogOut2);
            panelDatosUsuarioContraido.Dock = DockStyle.Left;
            panelDatosUsuarioContraido.Location = new Point(0, 0);
            panelDatosUsuarioContraido.Name = "panelDatosUsuarioContraido";
            panelDatosUsuarioContraido.Size = new Size(50, 115);
            panelDatosUsuarioContraido.TabIndex = 1;
            // 
            // pictureBoxUserContraido
            // 
            pictureBoxUserContraido.Image = Properties.Resources.perfil__1_;
            pictureBoxUserContraido.InitialImage = Properties.Resources.perfil;
            pictureBoxUserContraido.Location = new Point(8, 13);
            pictureBoxUserContraido.Name = "pictureBoxUserContraido";
            pictureBoxUserContraido.Size = new Size(31, 31);
            pictureBoxUserContraido.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUserContraido.TabIndex = 3;
            pictureBoxUserContraido.TabStop = false;
            pictureBoxUserContraido.MouseHover += pictureBox1_MouseHover;
            // 
            // btnLogOut2
            // 
            btnLogOut2.BackColor = Color.FromArgb(44, 40, 36);
            btnLogOut2.BackgroundColor = Color.FromArgb(44, 40, 36);
            btnLogOut2.BorderColor = Color.Empty;
            btnLogOut2.BorderRadius = 25;
            btnLogOut2.BorderSize = 1;
            btnLogOut2.FlatAppearance.BorderSize = 0;
            btnLogOut2.FlatStyle = FlatStyle.Flat;
            btnLogOut2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogOut2.ForeColor = Color.White;
            btnLogOut2.Image = Properties.Resources.salida;
            btnLogOut2.Location = new Point(5, 50);
            btnLogOut2.Name = "btnLogOut2";
            btnLogOut2.Size = new Size(39, 40);
            btnLogOut2.TabIndex = 2;
            btnLogOut2.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut2.TextColor = Color.White;
            btnLogOut2.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogOut2.UseVisualStyleBackColor = false;
            btnLogOut2.Click += btnLogOut2_Click;
            // 
            // panelDatosUsuarioExpandido
            // 
            panelDatosUsuarioExpandido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDatosUsuarioExpandido.BackColor = Color.White;
            panelDatosUsuarioExpandido.Controls.Add(lblUsuario);
            panelDatosUsuarioExpandido.Controls.Add(pictureBoxUser);
            panelDatosUsuarioExpandido.Controls.Add(lblNombre);
            panelDatosUsuarioExpandido.Controls.Add(btnLogOut);
            panelDatosUsuarioExpandido.Location = new Point(0, 0);
            panelDatosUsuarioExpandido.Name = "panelDatosUsuarioExpandido";
            panelDatosUsuarioExpandido.Size = new Size(265, 115);
            panelDatosUsuarioExpandido.TabIndex = 0;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F);
            lblUsuario.Location = new Point(81, 42);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(38, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "label1";
            // 
            // pictureBoxUser
            // 
            pictureBoxUser.Image = Properties.Resources.perfil__1_;
            pictureBoxUser.InitialImage = Properties.Resources.perfil;
            pictureBoxUser.Location = new Point(27, 9);
            pictureBoxUser.Name = "pictureBoxUser";
            pictureBoxUser.Size = new Size(48, 48);
            pictureBoxUser.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUser.TabIndex = 0;
            pictureBoxUser.TabStop = false;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.Location = new Point(81, 9);
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
            btnLogOut.Location = new Point(16, 63);
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
            // panelDatosUsuario
            // 
            panelDatosUsuario.BackColor = Color.White;
            panelDatosUsuario.Controls.Add(panelDatosUsuarioExpandido);
            panelDatosUsuario.Controls.Add(panelDatosUsuarioContraido);
            panelDatosUsuario.Dock = DockStyle.Bottom;
            panelDatosUsuario.Location = new Point(0, 360);
            panelDatosUsuario.Name = "panelDatosUsuario";
            panelDatosUsuario.Size = new Size(265, 115);
            panelDatosUsuario.TabIndex = 2;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.White;
            panelMenu.Controls.Add(panelTreeView);
            panelMenu.Controls.Add(panelColapsarMenu);
            panelMenu.Controls.Add(panelDatosUsuario);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(265, 475);
            panelMenu.TabIndex = 3;
            // 
            // panelTreeView
            // 
            panelTreeView.Controls.Add(treeView1);
            panelTreeView.Dock = DockStyle.Left;
            panelTreeView.Location = new Point(0, 37);
            panelTreeView.Name = "panelTreeView";
            panelTreeView.Size = new Size(264, 323);
            panelTreeView.TabIndex = 4;
            // 
            // panelColapsarMenu
            // 
            panelColapsarMenu.BackColor = Color.White;
            panelColapsarMenu.Controls.Add(btnMenu);
            panelColapsarMenu.Dock = DockStyle.Top;
            panelColapsarMenu.Location = new Point(0, 0);
            panelColapsarMenu.Name = "panelColapsarMenu";
            panelColapsarMenu.Size = new Size(265, 37);
            panelColapsarMenu.TabIndex = 3;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.White;
            btnMenu.BackgroundColor = Color.White;
            btnMenu.BorderColor = Color.Empty;
            btnMenu.BorderRadius = 37;
            btnMenu.BorderSize = 0;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.ForeColor = Color.White;
            btnMenu.Image = Properties.Resources.menu;
            btnMenu.Location = new Point(0, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(44, 37);
            btnMenu.TabIndex = 0;
            btnMenu.TextColor = Color.White;
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnMenu_Click_1;
            // 
            // timerMenu
            // 
            timerMenu.Tick += timerMenu_Tick;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1151, 475);
            Controls.Add(panelChildForm);
            Controls.Add(panelMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuPrincipal";
            WindowState = FormWindowState.Maximized;
            FormClosing += MenuPrincipal_FormClosing_1;
            Load += MenuPrincipal_Load;
            panelDatosUsuarioContraido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserContraido).EndInit();
            panelDatosUsuarioExpandido.ResumeLayout(false);
            panelDatosUsuarioExpandido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).EndInit();
            panelDatosUsuario.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelTreeView.ResumeLayout(false);
            panelColapsarMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView1;
        private ImageList imageList1;
        private Panel panelChildForm;
        private Panel panelDatosUsuario;
        private PictureBox pictureBoxUser;
        private Clases.RoundedButton btnLogOut;
        private Label lblNombre;
        private Label lblUsuario;
        private Panel panelMenu;
        private Panel panelColapsarMenu;
        private Clases.RoundedButton btnMenu;
        private Panel panelTreeView;
        private System.Windows.Forms.Timer timerMenu;
        private Panel panelDatosUsuarioExpandido;
        private Panel panelDatosUsuarioContraido;
        private Clases.RoundedButton btnLogOut2;
        private PictureBox pictureBoxUserContraido;
        private ToolTip toolTip1;
    }
}