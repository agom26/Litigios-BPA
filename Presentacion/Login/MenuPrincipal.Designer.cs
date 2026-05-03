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
            lblUserContraido = new Label();
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
            panelBotonesPrincipales = new Panel();
            btnReportes = new Presentacion.Clases.RoundedButton();
            btnUsuarios = new Presentacion.Clases.RoundedButton();
            btnPlazos = new Presentacion.Clases.RoundedButton();
            btnVencimientos = new Presentacion.Clases.RoundedButton();
            btnParticipantes = new Presentacion.Clases.RoundedButton();
            btnContenciosoAdministrativo = new Presentacion.Clases.RoundedButton();
            btnConstitucional = new Presentacion.Clases.RoundedButton();
            btnCivil = new Presentacion.Clases.RoundedButton();
            btnLaboral = new Presentacion.Clases.RoundedButton();
            btnInicio = new Presentacion.Clases.RoundedButton();
            panelColapsarMenu = new Panel();
            panel1 = new Panel();
            pictureBoxLogoBPA = new PictureBox();
            pictureBoxLogoLegalia = new PictureBox();
            btnMenu = new Presentacion.Clases.RoundedButton();
            timerMenu = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            rDropDownMenuLaboral2 = new Presentacion.Clases.RDropDownMenu(components);
            primerInstanciaToolStripMenuItem = new ToolStripMenuItem();
            recursosContraResolucionesToolStripMenuItem = new ToolStripMenuItem();
            segundaInstanicaToolStripMenuItem = new ToolStripMenuItem();
            rDropDownMenuCivil = new Presentacion.Clases.RDropDownMenu(components);
            procesoDeEjecuciónToolStripMenuItem = new ToolStripMenuItem();
            víaDeApremioToolStripMenuItem = new ToolStripMenuItem();
            comúnToolStripMenuItem = new ToolStripMenuItem();
            segundaInstanciaToolStripMenuItem1 = new ToolStripMenuItem();
            juicioSumarioToolStripMenuItem = new ToolStripMenuItem();
            primerInstanciaToolStripMenuItem1 = new ToolStripMenuItem();
            segundaInstanciaToolStripMenuItem2 = new ToolStripMenuItem();
            juicioOralToolStripMenuItem = new ToolStripMenuItem();
            primerInstanciaToolStripMenuItem2 = new ToolStripMenuItem();
            recursosContraResolucionesToolStripMenuItem1 = new ToolStripMenuItem();
            segundaInstanciaToolStripMenuItem3 = new ToolStripMenuItem();
            terToolStripMenuItem = new ToolStripMenuItem();
            rDropDownMenuConstitucional = new Presentacion.Clases.RDropDownMenu(components);
            amparoToolStripMenuItem = new ToolStripMenuItem();
            terminadosToolStripMenuItem = new ToolStripMenuItem();
            rDropDownMenuContencioso = new Presentacion.Clases.RDropDownMenu(components);
            generalToolStripMenuItem = new ToolStripMenuItem();
            tributarioToolStripMenuItem = new ToolStripMenuItem();
            recursoDeCasaciónToolStripMenuItem = new ToolStripMenuItem();
            terminadosToolStripMenuItem1 = new ToolStripMenuItem();
            rDropDownMenuPersonas = new Presentacion.Clases.RDropDownMenu(components);
            demandadosAutoridadImpugnadaToolStripMenuItem = new ToolStripMenuItem();
            demandantesSolicitantesToolStripMenuItem = new ToolStripMenuItem();
            tercerosInteresadosToolStripMenuItem = new ToolStripMenuItem();
            contactosDeEmpresaToolStripMenuItem = new ToolStripMenuItem();
            panelDatosUsuarioContraido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserContraido).BeginInit();
            panelDatosUsuarioExpandido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).BeginInit();
            panelDatosUsuario.SuspendLayout();
            panelMenu.SuspendLayout();
            panelTreeView.SuspendLayout();
            panelBotonesPrincipales.SuspendLayout();
            panelColapsarMenu.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoBPA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoLegalia).BeginInit();
            rDropDownMenuLaboral2.SuspendLayout();
            rDropDownMenuCivil.SuspendLayout();
            rDropDownMenuConstitucional.SuspendLayout();
            rDropDownMenuContencioso.SuspendLayout();
            rDropDownMenuPersonas.SuspendLayout();
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
            treeView1.Size = new Size(264, 310);
            treeView1.TabIndex = 0;
            treeView1.DrawNode += treeView1_DrawNode;
            treeView1.NodeMouseHover += treeView1_NodeMouseHover;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.MouseLeave += treeView1_MouseLeave;
            treeView1.MouseMove += treeView1_MouseMove;
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
            panelChildForm.BackColor = Color.FromArgb(250, 249, 246);
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(265, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(886, 475);
            panelChildForm.TabIndex = 1;
            // 
            // panelDatosUsuarioContraido
            // 
            panelDatosUsuarioContraido.BackColor = Color.White;
            panelDatosUsuarioContraido.Controls.Add(lblUserContraido);
            panelDatosUsuarioContraido.Controls.Add(pictureBoxUserContraido);
            panelDatosUsuarioContraido.Controls.Add(btnLogOut2);
            panelDatosUsuarioContraido.Dock = DockStyle.Left;
            panelDatosUsuarioContraido.Location = new Point(0, 0);
            panelDatosUsuarioContraido.Name = "panelDatosUsuarioContraido";
            panelDatosUsuarioContraido.Size = new Size(150, 115);
            panelDatosUsuarioContraido.TabIndex = 1;
            // 
            // lblUserContraido
            // 
            lblUserContraido.AutoSize = true;
            lblUserContraido.Font = new Font("Segoe UI", 9F);
            lblUserContraido.Location = new Point(45, 24);
            lblUserContraido.Name = "lblUserContraido";
            lblUserContraido.Size = new Size(10, 15);
            lblUserContraido.TabIndex = 4;
            lblUserContraido.Text = ".";
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
            btnLogOut2.Size = new Size(126, 40);
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
            panelDatosUsuario.Controls.Add(panelDatosUsuarioContraido);
            panelDatosUsuario.Controls.Add(panelDatosUsuarioExpandido);
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
            panelMenu.Controls.Add(panelBotonesPrincipales);
            panelMenu.Controls.Add(panelColapsarMenu);
            panelMenu.Controls.Add(panelDatosUsuario);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(265, 475);
            panelMenu.TabIndex = 3;
            panelMenu.MouseLeave += panelMenu_MouseLeave;
            // 
            // panelTreeView
            // 
            panelTreeView.Controls.Add(treeView1);
            panelTreeView.Dock = DockStyle.Left;
            panelTreeView.Location = new Point(140, 50);
            panelTreeView.Name = "panelTreeView";
            panelTreeView.Size = new Size(264, 310);
            panelTreeView.TabIndex = 4;
            // 
            // panelBotonesPrincipales
            // 
            panelBotonesPrincipales.BackColor = Color.White;
            panelBotonesPrincipales.Controls.Add(btnReportes);
            panelBotonesPrincipales.Controls.Add(btnUsuarios);
            panelBotonesPrincipales.Controls.Add(btnPlazos);
            panelBotonesPrincipales.Controls.Add(btnVencimientos);
            panelBotonesPrincipales.Controls.Add(btnParticipantes);
            panelBotonesPrincipales.Controls.Add(btnContenciosoAdministrativo);
            panelBotonesPrincipales.Controls.Add(btnConstitucional);
            panelBotonesPrincipales.Controls.Add(btnCivil);
            panelBotonesPrincipales.Controls.Add(btnLaboral);
            panelBotonesPrincipales.Controls.Add(btnInicio);
            panelBotonesPrincipales.Dock = DockStyle.Left;
            panelBotonesPrincipales.Location = new Point(0, 50);
            panelBotonesPrincipales.Margin = new Padding(1);
            panelBotonesPrincipales.Name = "panelBotonesPrincipales";
            panelBotonesPrincipales.Size = new Size(140, 310);
            panelBotonesPrincipales.TabIndex = 0;
            // 
            // btnReportes
            // 
            btnReportes.AutoSize = true;
            btnReportes.BackColor = Color.White;
            btnReportes.BackgroundColor = Color.White;
            btnReportes.BorderColor = Color.FromArgb(214, 205, 188);
            btnReportes.BorderRadius = 25;
            btnReportes.BorderSize = 1;
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.ForeColor = Color.White;
            btnReportes.Image = Properties.Resources.grafico_de_barras;
            btnReportes.Location = new Point(0, 360);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(140, 40);
            btnReportes.TabIndex = 6;
            btnReportes.TextColor = Color.White;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.AutoSize = true;
            btnUsuarios.BackColor = Color.White;
            btnUsuarios.BackgroundColor = Color.White;
            btnUsuarios.BorderColor = Color.FromArgb(214, 205, 188);
            btnUsuarios.BorderRadius = 25;
            btnUsuarios.BorderSize = 1;
            btnUsuarios.Dock = DockStyle.Top;
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Image = Properties.Resources.perfil__3_;
            btnUsuarios.Location = new Point(0, 320);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(140, 40);
            btnUsuarios.TabIndex = 7;
            btnUsuarios.TextColor = Color.White;
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnPlazos
            // 
            btnPlazos.AutoSize = true;
            btnPlazos.BackColor = Color.White;
            btnPlazos.BackgroundColor = Color.White;
            btnPlazos.BorderColor = Color.FromArgb(214, 205, 188);
            btnPlazos.BorderRadius = 25;
            btnPlazos.BorderSize = 1;
            btnPlazos.Dock = DockStyle.Top;
            btnPlazos.FlatAppearance.BorderSize = 0;
            btnPlazos.FlatStyle = FlatStyle.Flat;
            btnPlazos.ForeColor = Color.White;
            btnPlazos.Image = Properties.Resources.reloj_de_arena;
            btnPlazos.Location = new Point(0, 280);
            btnPlazos.Name = "btnPlazos";
            btnPlazos.Size = new Size(140, 40);
            btnPlazos.TabIndex = 9;
            btnPlazos.TextColor = Color.White;
            btnPlazos.UseVisualStyleBackColor = false;
            btnPlazos.Click += btnPlazos_Click;
            // 
            // btnVencimientos
            // 
            btnVencimientos.AutoSize = true;
            btnVencimientos.BackColor = Color.White;
            btnVencimientos.BackgroundColor = Color.White;
            btnVencimientos.BorderColor = Color.FromArgb(214, 205, 188);
            btnVencimientos.BorderRadius = 25;
            btnVencimientos.BorderSize = 1;
            btnVencimientos.Dock = DockStyle.Top;
            btnVencimientos.FlatAppearance.BorderSize = 0;
            btnVencimientos.FlatStyle = FlatStyle.Flat;
            btnVencimientos.ForeColor = Color.White;
            btnVencimientos.Image = Properties.Resources.despertador;
            btnVencimientos.Location = new Point(0, 240);
            btnVencimientos.Name = "btnVencimientos";
            btnVencimientos.Size = new Size(140, 40);
            btnVencimientos.TabIndex = 8;
            btnVencimientos.TextColor = Color.White;
            btnVencimientos.UseVisualStyleBackColor = false;
            btnVencimientos.Click += btnVencimientos_Click;
            // 
            // btnParticipantes
            // 
            btnParticipantes.AutoSize = true;
            btnParticipantes.BackColor = Color.White;
            btnParticipantes.BackgroundColor = Color.White;
            btnParticipantes.BorderColor = Color.FromArgb(214, 205, 188);
            btnParticipantes.BorderRadius = 25;
            btnParticipantes.BorderSize = 1;
            btnParticipantes.Dock = DockStyle.Top;
            btnParticipantes.FlatAppearance.BorderSize = 0;
            btnParticipantes.FlatStyle = FlatStyle.Flat;
            btnParticipantes.ForeColor = Color.White;
            btnParticipantes.Image = Properties.Resources.equipo__1_;
            btnParticipantes.Location = new Point(0, 200);
            btnParticipantes.Name = "btnParticipantes";
            btnParticipantes.Size = new Size(140, 40);
            btnParticipantes.TabIndex = 5;
            btnParticipantes.TextColor = Color.White;
            btnParticipantes.UseVisualStyleBackColor = false;
            btnParticipantes.Click += btnParticipantes_Click;
            // 
            // btnContenciosoAdministrativo
            // 
            btnContenciosoAdministrativo.AutoSize = true;
            btnContenciosoAdministrativo.BackColor = Color.White;
            btnContenciosoAdministrativo.BackgroundColor = Color.White;
            btnContenciosoAdministrativo.BorderColor = Color.FromArgb(214, 205, 188);
            btnContenciosoAdministrativo.BorderRadius = 25;
            btnContenciosoAdministrativo.BorderSize = 1;
            btnContenciosoAdministrativo.Dock = DockStyle.Top;
            btnContenciosoAdministrativo.FlatAppearance.BorderSize = 0;
            btnContenciosoAdministrativo.FlatStyle = FlatStyle.Flat;
            btnContenciosoAdministrativo.ForeColor = Color.White;
            btnContenciosoAdministrativo.Image = Properties.Resources.corte__1_;
            btnContenciosoAdministrativo.Location = new Point(0, 160);
            btnContenciosoAdministrativo.Name = "btnContenciosoAdministrativo";
            btnContenciosoAdministrativo.Size = new Size(140, 40);
            btnContenciosoAdministrativo.TabIndex = 4;
            btnContenciosoAdministrativo.TextColor = Color.White;
            btnContenciosoAdministrativo.UseVisualStyleBackColor = false;
            btnContenciosoAdministrativo.Click += btnContenciosoAdministrativo_Click;
            // 
            // btnConstitucional
            // 
            btnConstitucional.AutoSize = true;
            btnConstitucional.BackColor = Color.White;
            btnConstitucional.BackgroundColor = Color.White;
            btnConstitucional.BorderColor = Color.FromArgb(214, 205, 188);
            btnConstitucional.BorderRadius = 25;
            btnConstitucional.BorderSize = 1;
            btnConstitucional.Dock = DockStyle.Top;
            btnConstitucional.FlatAppearance.BorderSize = 0;
            btnConstitucional.FlatStyle = FlatStyle.Flat;
            btnConstitucional.ForeColor = Color.White;
            btnConstitucional.Image = Properties.Resources.balanza;
            btnConstitucional.Location = new Point(0, 120);
            btnConstitucional.Name = "btnConstitucional";
            btnConstitucional.Size = new Size(140, 40);
            btnConstitucional.TabIndex = 3;
            btnConstitucional.TextColor = Color.White;
            btnConstitucional.UseVisualStyleBackColor = false;
            btnConstitucional.Click += btnConstitucional_Click;
            // 
            // btnCivil
            // 
            btnCivil.AutoSize = true;
            btnCivil.BackColor = Color.White;
            btnCivil.BackgroundColor = Color.White;
            btnCivil.BorderColor = Color.FromArgb(214, 205, 188);
            btnCivil.BorderRadius = 25;
            btnCivil.BorderSize = 1;
            btnCivil.Dock = DockStyle.Top;
            btnCivil.FlatAppearance.BorderSize = 0;
            btnCivil.FlatStyle = FlatStyle.Flat;
            btnCivil.ForeColor = Color.White;
            btnCivil.Image = Properties.Resources.documento_legal;
            btnCivil.Location = new Point(0, 80);
            btnCivil.Name = "btnCivil";
            btnCivil.Size = new Size(140, 40);
            btnCivil.TabIndex = 2;
            btnCivil.TextColor = Color.White;
            btnCivil.UseVisualStyleBackColor = false;
            btnCivil.Click += btnCivil_Click;
            // 
            // btnLaboral
            // 
            btnLaboral.AutoSize = true;
            btnLaboral.BackColor = Color.White;
            btnLaboral.BackgroundColor = Color.White;
            btnLaboral.BorderColor = Color.FromArgb(214, 205, 188);
            btnLaboral.BorderRadius = 25;
            btnLaboral.BorderSize = 1;
            btnLaboral.Dock = DockStyle.Top;
            btnLaboral.FlatAppearance.BorderSize = 0;
            btnLaboral.FlatStyle = FlatStyle.Flat;
            btnLaboral.ForeColor = Color.White;
            btnLaboral.Image = Properties.Resources.contratista;
            btnLaboral.Location = new Point(0, 40);
            btnLaboral.Name = "btnLaboral";
            btnLaboral.Size = new Size(140, 40);
            btnLaboral.TabIndex = 1;
            btnLaboral.TextColor = Color.White;
            btnLaboral.UseVisualStyleBackColor = false;
            btnLaboral.Click += btnLaboral_Click;
            // 
            // btnInicio
            // 
            btnInicio.AutoSize = true;
            btnInicio.BackColor = Color.White;
            btnInicio.BackgroundColor = Color.White;
            btnInicio.BorderColor = Color.FromArgb(214, 205, 188);
            btnInicio.BorderRadius = 25;
            btnInicio.BorderSize = 1;
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.ForeColor = Color.White;
            btnInicio.Image = Properties.Resources.hogar__1_;
            btnInicio.Location = new Point(0, 0);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(140, 40);
            btnInicio.TabIndex = 0;
            btnInicio.TextColor = Color.White;
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click;
            // 
            // panelColapsarMenu
            // 
            panelColapsarMenu.BackColor = Color.White;
            panelColapsarMenu.Controls.Add(panel1);
            panelColapsarMenu.Controls.Add(btnMenu);
            panelColapsarMenu.Dock = DockStyle.Top;
            panelColapsarMenu.Location = new Point(0, 0);
            panelColapsarMenu.Name = "panelColapsarMenu";
            panelColapsarMenu.Size = new Size(265, 50);
            panelColapsarMenu.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBoxLogoBPA);
            panel1.Controls.Add(pictureBoxLogoLegalia);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(108, 50);
            panel1.TabIndex = 1;
            // 
            // pictureBoxLogoBPA
            // 
            pictureBoxLogoBPA.BackgroundImage = (Image)resources.GetObject("pictureBoxLogoBPA.BackgroundImage");
            pictureBoxLogoBPA.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxLogoBPA.Dock = DockStyle.Right;
            pictureBoxLogoBPA.Location = new Point(58, 0);
            pictureBoxLogoBPA.Name = "pictureBoxLogoBPA";
            pictureBoxLogoBPA.Padding = new Padding(2);
            pictureBoxLogoBPA.Size = new Size(50, 50);
            pictureBoxLogoBPA.TabIndex = 3;
            pictureBoxLogoBPA.TabStop = false;
            // 
            // pictureBoxLogoLegalia
            // 
            pictureBoxLogoLegalia.BackgroundImage = Properties.Resources.huella_dactilar;
            pictureBoxLogoLegalia.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxLogoLegalia.Dock = DockStyle.Left;
            pictureBoxLogoLegalia.Location = new Point(0, 0);
            pictureBoxLogoLegalia.Name = "pictureBoxLogoLegalia";
            pictureBoxLogoLegalia.Padding = new Padding(2);
            pictureBoxLogoLegalia.Size = new Size(50, 50);
            pictureBoxLogoLegalia.TabIndex = 2;
            pictureBoxLogoLegalia.TabStop = false;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.White;
            btnMenu.BackgroundColor = Color.White;
            btnMenu.BorderColor = Color.Empty;
            btnMenu.BorderRadius = 0;
            btnMenu.BorderSize = 0;
            btnMenu.Dock = DockStyle.Right;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.ForeColor = Color.White;
            btnMenu.Image = Properties.Resources.doble_flecha_izq;
            btnMenu.Location = new Point(235, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(30, 50);
            btnMenu.TabIndex = 0;
            btnMenu.TextColor = Color.White;
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnMenu_Click_1;
            // 
            // timerMenu
            // 
            timerMenu.Tick += timerMenu_Tick;
            // 
            // rDropDownMenuLaboral2
            // 
            rDropDownMenuLaboral2.IsMainMenu = false;
            rDropDownMenuLaboral2.Items.AddRange(new ToolStripItem[] { primerInstanciaToolStripMenuItem, recursosContraResolucionesToolStripMenuItem, segundaInstanicaToolStripMenuItem });
            rDropDownMenuLaboral2.MenuItemHeight = 25;
            rDropDownMenuLaboral2.MenuItemTextColor = Color.Empty;
            rDropDownMenuLaboral2.Name = "rDropDownMenuLaboral2";
            rDropDownMenuLaboral2.PrimaryColor = Color.Empty;
            rDropDownMenuLaboral2.Size = new Size(228, 70);
            // 
            // primerInstanciaToolStripMenuItem
            // 
            primerInstanciaToolStripMenuItem.Name = "primerInstanciaToolStripMenuItem";
            primerInstanciaToolStripMenuItem.Size = new Size(227, 22);
            primerInstanciaToolStripMenuItem.Text = "Primer Instancia";
            primerInstanciaToolStripMenuItem.Click += primerInstanciaToolStripMenuItem_Click_1;
            // 
            // recursosContraResolucionesToolStripMenuItem
            // 
            recursosContraResolucionesToolStripMenuItem.Name = "recursosContraResolucionesToolStripMenuItem";
            recursosContraResolucionesToolStripMenuItem.Size = new Size(227, 22);
            recursosContraResolucionesToolStripMenuItem.Text = "Recursos contra resoluciones";
            recursosContraResolucionesToolStripMenuItem.Click += recursosContraResolucionesToolStripMenuItem_Click_1;
            // 
            // segundaInstanicaToolStripMenuItem
            // 
            segundaInstanicaToolStripMenuItem.Name = "segundaInstanicaToolStripMenuItem";
            segundaInstanicaToolStripMenuItem.Size = new Size(227, 22);
            segundaInstanicaToolStripMenuItem.Text = "Segunda Instancia";
            segundaInstanicaToolStripMenuItem.Click += segundaInstanicaToolStripMenuItem_Click;
            // 
            // rDropDownMenuCivil
            // 
            rDropDownMenuCivil.IsMainMenu = false;
            rDropDownMenuCivil.Items.AddRange(new ToolStripItem[] { procesoDeEjecuciónToolStripMenuItem, juicioSumarioToolStripMenuItem, juicioOralToolStripMenuItem, terToolStripMenuItem });
            rDropDownMenuCivil.MenuItemHeight = 25;
            rDropDownMenuCivil.MenuItemTextColor = Color.Empty;
            rDropDownMenuCivil.Name = "rDropDownMenuCivil";
            rDropDownMenuCivil.PrimaryColor = Color.Empty;
            rDropDownMenuCivil.Size = new Size(187, 92);
            // 
            // procesoDeEjecuciónToolStripMenuItem
            // 
            procesoDeEjecuciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { víaDeApremioToolStripMenuItem, comúnToolStripMenuItem, segundaInstanciaToolStripMenuItem1 });
            procesoDeEjecuciónToolStripMenuItem.Name = "procesoDeEjecuciónToolStripMenuItem";
            procesoDeEjecuciónToolStripMenuItem.Size = new Size(186, 22);
            procesoDeEjecuciónToolStripMenuItem.Text = "Proceso de ejecución";
            // 
            // víaDeApremioToolStripMenuItem
            // 
            víaDeApremioToolStripMenuItem.Name = "víaDeApremioToolStripMenuItem";
            víaDeApremioToolStripMenuItem.Size = new Size(170, 22);
            víaDeApremioToolStripMenuItem.Text = "Vía de Apremio";
            víaDeApremioToolStripMenuItem.Click += víaDeApremioToolStripMenuItem_Click_1;
            // 
            // comúnToolStripMenuItem
            // 
            comúnToolStripMenuItem.Name = "comúnToolStripMenuItem";
            comúnToolStripMenuItem.Size = new Size(170, 22);
            comúnToolStripMenuItem.Text = "Común";
            comúnToolStripMenuItem.Click += comúnToolStripMenuItem_Click_1;
            // 
            // segundaInstanciaToolStripMenuItem1
            // 
            segundaInstanciaToolStripMenuItem1.Name = "segundaInstanciaToolStripMenuItem1";
            segundaInstanciaToolStripMenuItem1.Size = new Size(170, 22);
            segundaInstanciaToolStripMenuItem1.Text = "Segunda Instancia";
            segundaInstanciaToolStripMenuItem1.Click += segundaInstanciaToolStripMenuItem1_Click_1;
            // 
            // juicioSumarioToolStripMenuItem
            // 
            juicioSumarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { primerInstanciaToolStripMenuItem1, segundaInstanciaToolStripMenuItem2 });
            juicioSumarioToolStripMenuItem.Name = "juicioSumarioToolStripMenuItem";
            juicioSumarioToolStripMenuItem.Size = new Size(186, 22);
            juicioSumarioToolStripMenuItem.Text = "Juicio Sumario";
            // 
            // primerInstanciaToolStripMenuItem1
            // 
            primerInstanciaToolStripMenuItem1.Name = "primerInstanciaToolStripMenuItem1";
            primerInstanciaToolStripMenuItem1.Size = new Size(170, 22);
            primerInstanciaToolStripMenuItem1.Text = "Primer Instancia";
            primerInstanciaToolStripMenuItem1.Click += primerInstanciaToolStripMenuItem1_Click_1;
            // 
            // segundaInstanciaToolStripMenuItem2
            // 
            segundaInstanciaToolStripMenuItem2.Name = "segundaInstanciaToolStripMenuItem2";
            segundaInstanciaToolStripMenuItem2.Size = new Size(170, 22);
            segundaInstanciaToolStripMenuItem2.Text = "Segunda Instancia";
            segundaInstanciaToolStripMenuItem2.Click += segundaInstanciaToolStripMenuItem2_Click_1;
            // 
            // juicioOralToolStripMenuItem
            // 
            juicioOralToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { primerInstanciaToolStripMenuItem2, recursosContraResolucionesToolStripMenuItem1, segundaInstanciaToolStripMenuItem3 });
            juicioOralToolStripMenuItem.Name = "juicioOralToolStripMenuItem";
            juicioOralToolStripMenuItem.Size = new Size(186, 22);
            juicioOralToolStripMenuItem.Text = "Juicio Oral";
            // 
            // primerInstanciaToolStripMenuItem2
            // 
            primerInstanciaToolStripMenuItem2.Name = "primerInstanciaToolStripMenuItem2";
            primerInstanciaToolStripMenuItem2.Size = new Size(227, 22);
            primerInstanciaToolStripMenuItem2.Text = "Primer Instancia";
            primerInstanciaToolStripMenuItem2.Click += primerInstanciaToolStripMenuItem2_Click_1;
            // 
            // recursosContraResolucionesToolStripMenuItem1
            // 
            recursosContraResolucionesToolStripMenuItem1.Name = "recursosContraResolucionesToolStripMenuItem1";
            recursosContraResolucionesToolStripMenuItem1.Size = new Size(227, 22);
            recursosContraResolucionesToolStripMenuItem1.Text = "Recursos contra resoluciones";
            recursosContraResolucionesToolStripMenuItem1.Click += recursosContraResolucionesToolStripMenuItem1_Click_1;
            // 
            // segundaInstanciaToolStripMenuItem3
            // 
            segundaInstanciaToolStripMenuItem3.Name = "segundaInstanciaToolStripMenuItem3";
            segundaInstanciaToolStripMenuItem3.Size = new Size(227, 22);
            segundaInstanciaToolStripMenuItem3.Text = "Segunda Instancia";
            segundaInstanciaToolStripMenuItem3.Click += segundaInstanciaToolStripMenuItem3_Click_1;
            // 
            // terToolStripMenuItem
            // 
            terToolStripMenuItem.Name = "terToolStripMenuItem";
            terToolStripMenuItem.Size = new Size(186, 22);
            terToolStripMenuItem.Text = "Terminados";
            terToolStripMenuItem.Click += terToolStripMenuItem_Click_1;
            // 
            // rDropDownMenuConstitucional
            // 
            rDropDownMenuConstitucional.IsMainMenu = false;
            rDropDownMenuConstitucional.Items.AddRange(new ToolStripItem[] { amparoToolStripMenuItem, terminadosToolStripMenuItem });
            rDropDownMenuConstitucional.MenuItemHeight = 25;
            rDropDownMenuConstitucional.MenuItemTextColor = Color.Empty;
            rDropDownMenuConstitucional.Name = "rDropDownMenuConstitucional";
            rDropDownMenuConstitucional.PrimaryColor = Color.Empty;
            rDropDownMenuConstitucional.Size = new Size(137, 48);
            // 
            // amparoToolStripMenuItem
            // 
            amparoToolStripMenuItem.Name = "amparoToolStripMenuItem";
            amparoToolStripMenuItem.Size = new Size(136, 22);
            amparoToolStripMenuItem.Text = "Amparo";
            amparoToolStripMenuItem.Click += amparoToolStripMenuItem_Click;
            // 
            // terminadosToolStripMenuItem
            // 
            terminadosToolStripMenuItem.Name = "terminadosToolStripMenuItem";
            terminadosToolStripMenuItem.Size = new Size(136, 22);
            terminadosToolStripMenuItem.Text = "Terminados";
            terminadosToolStripMenuItem.Click += terminadosToolStripMenuItem_Click_1;
            // 
            // rDropDownMenuContencioso
            // 
            rDropDownMenuContencioso.IsMainMenu = false;
            rDropDownMenuContencioso.Items.AddRange(new ToolStripItem[] { generalToolStripMenuItem, tributarioToolStripMenuItem, recursoDeCasaciónToolStripMenuItem, terminadosToolStripMenuItem1 });
            rDropDownMenuContencioso.MenuItemHeight = 25;
            rDropDownMenuContencioso.MenuItemTextColor = Color.Empty;
            rDropDownMenuContencioso.Name = "rDropDownMenuContencioso";
            rDropDownMenuContencioso.PrimaryColor = Color.Empty;
            rDropDownMenuContencioso.Size = new Size(184, 92);
            // 
            // generalToolStripMenuItem
            // 
            generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            generalToolStripMenuItem.Size = new Size(183, 22);
            generalToolStripMenuItem.Text = "General";
            generalToolStripMenuItem.Click += generalToolStripMenuItem_Click;
            // 
            // tributarioToolStripMenuItem
            // 
            tributarioToolStripMenuItem.Name = "tributarioToolStripMenuItem";
            tributarioToolStripMenuItem.Size = new Size(183, 22);
            tributarioToolStripMenuItem.Text = "Tributario";
            tributarioToolStripMenuItem.Click += tributarioToolStripMenuItem_Click;
            // 
            // recursoDeCasaciónToolStripMenuItem
            // 
            recursoDeCasaciónToolStripMenuItem.Name = "recursoDeCasaciónToolStripMenuItem";
            recursoDeCasaciónToolStripMenuItem.Size = new Size(183, 22);
            recursoDeCasaciónToolStripMenuItem.Text = "Recurso de Casación";
            recursoDeCasaciónToolStripMenuItem.Click += recursoDeCasaciónToolStripMenuItem_Click;
            // 
            // terminadosToolStripMenuItem1
            // 
            terminadosToolStripMenuItem1.Name = "terminadosToolStripMenuItem1";
            terminadosToolStripMenuItem1.Size = new Size(183, 22);
            terminadosToolStripMenuItem1.Text = "Terminados";
            terminadosToolStripMenuItem1.Click += terminadosToolStripMenuItem1_Click;
            // 
            // rDropDownMenuPersonas
            // 
            rDropDownMenuPersonas.IsMainMenu = false;
            rDropDownMenuPersonas.Items.AddRange(new ToolStripItem[] { demandadosAutoridadImpugnadaToolStripMenuItem, demandantesSolicitantesToolStripMenuItem, tercerosInteresadosToolStripMenuItem, contactosDeEmpresaToolStripMenuItem });
            rDropDownMenuPersonas.MenuItemHeight = 25;
            rDropDownMenuPersonas.MenuItemTextColor = Color.Empty;
            rDropDownMenuPersonas.Name = "rDropDownMenuPersonas";
            rDropDownMenuPersonas.PrimaryColor = Color.Empty;
            rDropDownMenuPersonas.Size = new Size(273, 92);
            // 
            // demandadosAutoridadImpugnadaToolStripMenuItem
            // 
            demandadosAutoridadImpugnadaToolStripMenuItem.Name = "demandadosAutoridadImpugnadaToolStripMenuItem";
            demandadosAutoridadImpugnadaToolStripMenuItem.Size = new Size(272, 22);
            demandadosAutoridadImpugnadaToolStripMenuItem.Text = "Demandados / Autoridad Impugnada";
            demandadosAutoridadImpugnadaToolStripMenuItem.Click += demandadosAutoridadImpugnadaToolStripMenuItem_Click;
            // 
            // demandantesSolicitantesToolStripMenuItem
            // 
            demandantesSolicitantesToolStripMenuItem.Name = "demandantesSolicitantesToolStripMenuItem";
            demandantesSolicitantesToolStripMenuItem.Size = new Size(272, 22);
            demandantesSolicitantesToolStripMenuItem.Text = "Demandantes / Solicitantes";
            demandantesSolicitantesToolStripMenuItem.Click += demandantesSolicitantesToolStripMenuItem_Click;
            // 
            // tercerosInteresadosToolStripMenuItem
            // 
            tercerosInteresadosToolStripMenuItem.Name = "tercerosInteresadosToolStripMenuItem";
            tercerosInteresadosToolStripMenuItem.Size = new Size(272, 22);
            tercerosInteresadosToolStripMenuItem.Text = "Terceros Interesados";
            tercerosInteresadosToolStripMenuItem.Click += tercerosInteresadosToolStripMenuItem_Click;
            // 
            // contactosDeEmpresaToolStripMenuItem
            // 
            contactosDeEmpresaToolStripMenuItem.Name = "contactosDeEmpresaToolStripMenuItem";
            contactosDeEmpresaToolStripMenuItem.Size = new Size(272, 22);
            contactosDeEmpresaToolStripMenuItem.Text = "Contactos de Empresa";
            contactosDeEmpresaToolStripMenuItem.Click += contactosDeEmpresaToolStripMenuItem_Click;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(1151, 475);
            Controls.Add(panelChildForm);
            Controls.Add(panelMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuPrincipal";
            Text = "LEGALIA";
            WindowState = FormWindowState.Maximized;
            FormClosing += MenuPrincipal_FormClosing_1;
            Load += MenuPrincipal_Load;
            ResizeEnd += MenuPrincipal_ResizeEnd;
            panelDatosUsuarioContraido.ResumeLayout(false);
            panelDatosUsuarioContraido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserContraido).EndInit();
            panelDatosUsuarioExpandido.ResumeLayout(false);
            panelDatosUsuarioExpandido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUser).EndInit();
            panelDatosUsuario.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelTreeView.ResumeLayout(false);
            panelBotonesPrincipales.ResumeLayout(false);
            panelBotonesPrincipales.PerformLayout();
            panelColapsarMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoBPA).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoLegalia).EndInit();
            rDropDownMenuLaboral2.ResumeLayout(false);
            rDropDownMenuCivil.ResumeLayout(false);
            rDropDownMenuConstitucional.ResumeLayout(false);
            rDropDownMenuContencioso.ResumeLayout(false);
            rDropDownMenuPersonas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView1;
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
        public ImageList imageList1;
        private PictureBox pictureBoxLogoLegalia;
        private PictureBox pictureBoxLogoBPA;
        private Panel panel1;
        private Label lblUserContraido;
        private Panel panelBotonesPrincipales;
        private Clases.RoundedButton btnInicio;
        private Clases.RoundedButton btnLaboral;
        private Clases.RoundedButton btnCivil;
        private Clases.RoundedButton btnConstitucional;
        private Clases.RoundedButton btnContenciosoAdministrativo;
        private Clases.RoundedButton btnParticipantes;
        private Clases.RoundedButton btnReportes;
        private Clases.RoundedButton btnUsuarios;
        private Clases.RoundedButton btnVencimientos;
        private Clases.RoundedButton btnPlazos;
        private Clases.RDropDownMenu rDropDownMenuLaboral2;
        private ToolStripMenuItem primerInstanciaToolStripMenuItem;
        private ToolStripMenuItem recursosContraResolucionesToolStripMenuItem;
        private ToolStripMenuItem segundaInstanicaToolStripMenuItem;
        private Clases.RDropDownMenu rDropDownMenuCivil;
        private ToolStripMenuItem procesoDeEjecuciónToolStripMenuItem;
        private ToolStripMenuItem víaDeApremioToolStripMenuItem;
        private ToolStripMenuItem comúnToolStripMenuItem;
        private ToolStripMenuItem segundaInstanciaToolStripMenuItem1;
        private ToolStripMenuItem juicioSumarioToolStripMenuItem;
        private ToolStripMenuItem primerInstanciaToolStripMenuItem1;
        private ToolStripMenuItem segundaInstanciaToolStripMenuItem2;
        private ToolStripMenuItem juicioOralToolStripMenuItem;
        private ToolStripMenuItem primerInstanciaToolStripMenuItem2;
        private ToolStripMenuItem recursosContraResolucionesToolStripMenuItem1;
        private ToolStripMenuItem segundaInstanciaToolStripMenuItem3;
        private ToolStripMenuItem terToolStripMenuItem;
        private Clases.RDropDownMenu rDropDownMenuConstitucional;
        private ToolStripMenuItem amparoToolStripMenuItem;
        private ToolStripMenuItem terminadosToolStripMenuItem;
        private Clases.RDropDownMenu rDropDownMenuContencioso;
        private Clases.RDropDownMenu rDropDownMenuPersonas;
        private ToolStripMenuItem generalToolStripMenuItem;
        private ToolStripMenuItem tributarioToolStripMenuItem;
        private ToolStripMenuItem recursoDeCasaciónToolStripMenuItem;
        private ToolStripMenuItem terminadosToolStripMenuItem1;
        private ToolStripMenuItem demandadosAutoridadImpugnadaToolStripMenuItem;
        private ToolStripMenuItem demandantesSolicitantesToolStripMenuItem;
        private ToolStripMenuItem tercerosInteresadosToolStripMenuItem;
        private ToolStripMenuItem contactosDeEmpresaToolStripMenuItem;
    }
}