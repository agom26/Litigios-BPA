using Comun;
using Presentacion.Alertas;
using Presentacion.Casos.Laborales;
using Presentacion.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Comun.Models;
using Presentacion.Casos.Civiles.Juicio_Sumario;
using Presentacion.Casos.Civiles;
using Presentacion.Casos.Civiles.Proceso_ejecucion;
using Presentacion.Casos.Civiles.Juicio_Oral;
using Presentacion.Casos.Contenciosos;
using Presentacion.Casos.Constitucionales.Constitucional_amparo;
using Presentacion.Casos.Constitucionales.Constitucional_Terminado;
using Presentacion.Reportes;
using Presentacion.Plazos;
using Presentacion.Alertas_y_notificaciones;
using Presentacion.Dashboard;
using Presentacion.Clases;
namespace Presentacion
{

    public partial class MenuPrincipal : Form
    {
        bool menuExpandido = true;

        private readonly List<string> ordenModulos = new List<string>
        {
            "laboral",
            "civil",
            "constitucional",
            "contencioso administrativo",
            "personas involucradas",
            "vencimientos",
            "plazos",
            "usuarios"
        };

        public MenuPrincipal()
        {
            InitializeComponent();
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.ShowLines = false;
            treeView1.ShowPlusMinus = false;
            treeView1.ShowRootLines = false;
            treeView1.FullRowSelect = true;
            treeView1.HideSelection = false;
            treeView1.BackColor = Color.FromArgb(255, 255, 255);
            //Color.FromArgb(243, 237, 228);
            treeView1.ForeColor = Color.FromArgb(255, 255, 255); //Color.FromArgb(243, 237, 228);
            treeView1.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;

            treeView1.DrawNode += treeView1_DrawNode; // Evento para dibujar nodos personalizados


            panelDatosUsuarioExpandido.Visible = true;
            panelDatosUsuarioContraido.Visible = false;
            lblUserContraido.Text = UserSession.Usuario;
            toolTip1.SetToolTip(pictureBoxUserContraido,
                $"Usuario: {UserSession.Usuario}\n" +
                $"Nombre: {UserSession.Nombres} {UserSession.Apellidos}");
        }

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;

            // No dock para que respete su tamaño original
            childForm.Dock = DockStyle.Fill;
            activeForm.Location = new System.Drawing.Point(0, 0);
            // No modificar tamaño para respetar el diseño original
            // (si quieres podrías ajustar con lógica extra)

            panelChildForm.Controls.Clear();
            panelChildForm.Controls.Add(childForm);

            panelChildForm.AutoScroll = true;  // IMPORTANTE: para scrollbars

            childForm.BringToFront();
            childForm.Show();


        }

        private async Task AbrirFormularioConLoaderAsync(Form frm)
        {
            try
            {
                if (frm is IAsyncLoadable asyncForm)
                {
                    using (var loading = new FrmLoading(async () => await asyncForm.LoadAsync()))
                    {
                        var result = loading.ShowDialog(this);

                        if (result != DialogResult.OK)
                        {
                            frm.Dispose();
                            return;
                        }
                    }
                }

                openChildForm(frm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                frm.Dispose();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {


        }

        private void dtgUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void LogoutAction()
        {

            DialogResult result = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {

                UserSession.Logout();

                this.Close();

            }
        }

        private void CargarMenuPorModulos()
        {
            treeView1.Nodes.Clear();

            var modulosUsuario = UserSession.Modulos
                .Select(m => m.clave_slug.ToLower())
                .ToList();

            treeView1.Nodes.Add(CrearNodo("Inicio", "inicio"));
            foreach (string modulo in ordenModulos)
            {
                if (!modulosUsuario.Contains(modulo))
                    continue;

                switch (modulo)
                {
                    case "laboral":
                        TreeNode laboral = CrearNodo("Laboral", "laboral");
                        laboral.Nodes.Add(CrearNodo("Primer Instancia", "laboral"));
                        laboral.Nodes.Add(CrearNodo("Recursos contra resoluciones", "laboral"));
                        laboral.Nodes.Add(CrearNodo("Segunda Instancia", "laboral"));
                        laboral.Nodes.Add(CrearNodo("Terminados", "laboral"));
                        treeView1.Nodes.Add(laboral);
                        break;

                    case "civil":
                        TreeNode civil = CrearNodo("Civil", "civil");

                        TreeNode ejecucion = CrearNodo("Proceso de ejecución", "civil");
                        ejecucion.Nodes.Add(CrearNodo("Vía de Apremio", "civil"));
                        ejecucion.Nodes.Add(CrearNodo("Común", "civil"));
                        ejecucion.Nodes.Add(CrearNodo("Segunda Instancia", "civil"));

                        TreeNode sumario = CrearNodo("Juicio Sumario", "civil");
                        sumario.Nodes.Add(CrearNodo("Primer Instancia", "civil"));
                        sumario.Nodes.Add(CrearNodo("Segunda Instancia", "civil"));

                        TreeNode oral = CrearNodo("Juicio oral", "civil");
                        oral.Nodes.Add(CrearNodo("Primer Instancia", "civil"));
                        oral.Nodes.Add(CrearNodo("Recursos contra resoluciones", "civil"));
                        oral.Nodes.Add(CrearNodo("Segunda Instancia", "civil"));

                        TreeNode terminados = CrearNodo("Terminados", "civil");

                        civil.Nodes.Add(ejecucion);
                        civil.Nodes.Add(sumario);
                        civil.Nodes.Add(oral);
                        civil.Nodes.Add(terminados);

                        treeView1.Nodes.Add(civil);
                        break;

                    case "constitucional":
                        TreeNode constitucional = CrearNodo("Constitucional", "constitucional");
                        constitucional.Nodes.Add(CrearNodo("Amparo", "constitucional"));
                        constitucional.Nodes.Add(CrearNodo("Terminados", "constitucional"));
                        treeView1.Nodes.Add(constitucional);
                        break;

                    case "contencioso administrativo":
                        TreeNode contencioso = CrearNodo("Contencioso Administrativo", "contencioso");
                        contencioso.Nodes.Add(CrearNodo("General", "contencioso"));
                        contencioso.Nodes.Add(CrearNodo("Tributario", "contencioso"));
                        contencioso.Nodes.Add(CrearNodo("Recurso de Casación", "contencioso"));
                        contencioso.Nodes.Add(CrearNodo("Terminados", "contencioso"));
                        treeView1.Nodes.Add(contencioso);
                        break;

                    case "personas involucradas":
                        TreeNode personas = CrearNodo("Personas involucradas", "personas");
                        personas.Nodes.Add(CrearNodo("Demandados / Autoridad Impugnada", "personas"));
                        personas.Nodes.Add(CrearNodo("Demandantes / Solicitantes", "personas"));
                        personas.Nodes.Add(CrearNodo("Terceros Interesados", "personas"));
                        personas.Nodes.Add(CrearNodo("Contactos de Empresa", "personas"));
                        treeView1.Nodes.Add(personas);
                        break;

                    case "vencimientos":
                        treeView1.Nodes.Add(CrearNodo("Vencimientos", "vencimientos"));
                        break;

                    case "plazos":
                        treeView1.Nodes.Add(CrearNodo("Plazos", "plazos"));
                        break;

                    case "usuarios":
                        treeView1.Nodes.Add(CrearNodo("Usuarios", "usuarios"));
                        break;
                }
            }

            treeView1.Nodes.Add(CrearNodo("Reportes", "reportes"));
            treeView1.ExpandAll();
        }

        private TreeNode CrearNodo(string texto, string iconKey)
        {
            return new TreeNode(texto)
            {
                ImageKey = iconKey,
                SelectedImageKey = iconKey
            };
        }

        private void CargarDatosUsuario()
        {
            lblNombre.Text = UserSession.Nombres + " \n" + UserSession.Apellidos;
            lblUsuario.Text = UserSession.Usuario;
        }

        private async void MenuPrincipal_Load(object sender, EventArgs e)
        {
            CargarMenuPorModulos();
            CargarDatosUsuario();
            AjustarAnchoTreeView();
            if (this.IsDisposed) return;

            await AbrirFormularioConLoaderAsync(new FrmDashboard());
        }

        private async void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode nodo = e.Node;

            if (nodo != null)
            {
                // Por ejemplo, si el nodo es "Usuarios"
                if (nodo.Text == "Usuarios")
                {
                    openChildForm(new Usuarios());
                }
                else if (nodo.Text == "Personas involucradas")
                {
                    if (nodo.IsExpanded)
                        nodo.Collapse();
                    else
                        nodo.Expand();
                }
                else if (nodo.Text == "Demandados / Autoridad Impugnada" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
                {
                    openChildForm(new Demandados());

                }
                else if (nodo.Text == "Demandantes / Solicitantes" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
                {
                    openChildForm(new Demandantes());

                }
                else if (nodo.Text == "Terceros Interesados" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
                {
                    openChildForm(new TercerosInteresados());

                }
                else if (nodo.Text == "Contactos de Empresa" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
                {
                    openChildForm(new ContactoEmpresa());

                }
                else if (nodo.Text == "Primer Instancia" && nodo.Parent != null &&
                    nodo.Parent.Text == "Laboral")
                {
                    await AbrirFormularioConLoaderAsync(new Laboral_primer_instancia());

                }
                else if (nodo.Text == "Recursos contra resoluciones" && nodo.Parent != null &&
                    nodo.Parent.Text == "Laboral")
                {
                    await AbrirFormularioConLoaderAsync(new Laboral_primer_instancia_recursos_resolucion());
                }
                else if (nodo.Text == "Segunda Instancia" && nodo.Parent != null &&
                    nodo.Parent.Text == "Laboral")
                {
                    await AbrirFormularioConLoaderAsync(new Laboral_segunda_instancia());
                }
                else if (nodo.Text == "Terminados" && nodo.Parent != null &&
                    nodo.Parent.Text == "Laboral")
                {
                    await AbrirFormularioConLoaderAsync(new Laboral_terminados());
                }
                else if (nodo.Text == "Primer Instancia"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Juicio Sumario"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_primer_instancia());
                }
                else if (nodo.Text == "Segunda Instancia"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Juicio Sumario"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Presentacion.Casos.Civiles.Juicio_Sumario.Civil_segunda_instancia());
                }
                else if (nodo.Text == "Vía de Apremio"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Proceso de ejecución"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_via_apremio());
                }
                else if (nodo.Text == "Común"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Proceso de ejecución"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_comun());
                }
                else if (nodo.Text == "Segunda Instancia"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Proceso de ejecución"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Presentacion.Casos.Civiles.Proceso_ejecucion.Civil_segunda_instancia());
                }
                else if (nodo.Text == "Primer Instancia"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Juicio oral"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_oral_primer_instancia());
                }
                else if (nodo.Text == "Recursos contra resoluciones"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Juicio oral"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_primer_instancia_recursos_resolucion());
                }
                else if (nodo.Text == "Segunda Instancia"
                    && nodo.Parent != null
                    && nodo.Parent.Parent != null
                    && nodo.Parent.Text == "Juicio oral"
                    && nodo.Parent.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_oral_segunda_instancia());
                }
                else if (nodo.Text == "Terminados" && nodo.Parent != null &&
                    nodo.Parent.Text == "Civil")
                {
                    await AbrirFormularioConLoaderAsync(new Civil_Terminados2());
                }
                else if (nodo.Text == "General" && nodo.Parent != null &&
                  nodo.Parent.Text == "Contencioso Administrativo")
                {
                    await AbrirFormularioConLoaderAsync(new Contencioso_General_PI());
                }
                else if (nodo.Text == "Tributario" && nodo.Parent != null &&
                  nodo.Parent.Text == "Contencioso Administrativo")
                {
                    await AbrirFormularioConLoaderAsync(new Contencioso_Tributario_PI());
                }
                else if (nodo.Text == "Recurso de Casación" && nodo.Parent != null &&
                  nodo.Parent.Text == "Contencioso Administrativo")
                {
                    await AbrirFormularioConLoaderAsync(new Contencioso_RecursoCasacion());
                }
                else if (nodo.Text == "Terminados" && nodo.Parent != null &&
                  nodo.Parent.Text == "Contencioso Administrativo")
                {
                    await AbrirFormularioConLoaderAsync(new Contencioso_Terminado());
                }
                else if (nodo.Text == "Amparo" && nodo.Parent != null &&
                  nodo.Parent.Text == "Constitucional")
                {
                    await AbrirFormularioConLoaderAsync(new Constitucional_amparo());
                }
                else if (nodo.Text == "Terminados" && nodo.Parent != null &&
                  nodo.Parent.Text == "Constitucional")
                {
                    await AbrirFormularioConLoaderAsync(new Constitucional_Terminado());
                }
                else if (nodo.Text == "Reportes")
                {
                    await AbrirFormularioConLoaderAsync(new FrmReportes());
                }
                else if (nodo.Text == "Plazos")
                {
                    await AbrirFormularioConLoaderAsync(new FrmPlazos());
                }
                else if (nodo.Text == "Vencimientos")
                {
                    await AbrirFormularioConLoaderAsync(new FrmAlertas());
                }
                else if (nodo.Text == "Inicio")
                {
                    await AbrirFormularioConLoaderAsync(new FrmDashboard());
                }
            }
        }

        public async Task AbrirPlazos()
        {
            await AbrirFormularioConLoaderAsync(new FrmPlazos());
        }

        public async Task AbrirAlertas()
        {
            await AbrirFormularioConLoaderAsync(new FrmAlertas());
        }

        private void roundedButton23_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton22_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton24_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton25_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_FormClosing_1(object sender, FormClosingEventArgs e)
        {

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LogoutAction();
        }

        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            menuExpandido = panelMenu.Width > 200;
            timerMenu.Start();

        }

        private void timerMenu_Tick(object sender, EventArgs e)
        {
            panelMenu.SuspendLayout();

            if (menuExpandido)
            {
                panelMenu.Width -= 20;

                if (panelMenu.Width <= 150)
                {
                    timerMenu.Stop();
                    AplicarModoContraido(); // <-- tu lógica del primer método
                }
            }
            else
            {
                panelMenu.Width += 20;

                if (panelMenu.Width >= 250)
                {
                    timerMenu.Stop();
                    AplicarModoExpandido(); // <-- tu lógica del primer método
                }
            }

            panelMenu.ResumeLayout();
        }

        private void AplicarModoContraido()
        {
            // Ocultar cosas mientras ajustamos (evita parpadeo)
            panelMenu.Visible = false;
            btnMenu.Image = Properties.Resources.doble_flecha_derecha;
            panelMenu.SuspendLayout();
            panelTreeView.SuspendLayout();
            panelDatosUsuarioExpandido.SuspendLayout();
            panelDatosUsuarioContraido.SuspendLayout();
            // Tamaño final
            panelMenu.Width = 140;

            // Panels visibles
            panelTreeView.Visible = false;
            panelDatosUsuarioExpandido.Visible = false;
            panelDatosUsuarioContraido.Visible = true;

            // Ajustar botones (iconos centrados, sin texto)
            foreach (var pnl in new[] { panelMenu, panelTreeView })
            {
                foreach (RoundedButton btn in pnl.Controls.OfType<RoundedButton>())
                {
                    btn.Text = "";
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = Padding.Empty;
                }
            }

            panelDatosUsuarioContraido.Visible = true;

            panelColapsarMenu.Visible = true;
            panelMenu.Controls.SetChildIndex(panelColapsarMenu, 0);
            // Reanudar layouts
            panelDatosUsuarioContraido.ResumeLayout(true);
            panelDatosUsuarioExpandido.ResumeLayout(true);
            panelTreeView.ResumeLayout(true);
            panelMenu.ResumeLayout(true);
            panelMenu.Refresh();

            panelMenu.Visible = true;
            panelMenu.Refresh();
        }

        private void AplicarModoExpandido()
        {
            //panelMenu.Visible = false;

            btnMenu.Image = Properties.Resources.doble_flecha_izq;
            panelMenu.SuspendLayout();
            panelTreeView.SuspendLayout();
            panelDatosUsuarioExpandido.SuspendLayout();
            panelDatosUsuarioContraido.SuspendLayout();
            panelColapsarMenu.SuspendLayout();
            // Tamaño final
            panelMenu.Width = 250;

            // Panels visibles
            panelTreeView.Visible = true;
            panelDatosUsuarioExpandido.Visible = true;
            panelColapsarMenu.Visible = true;
            panelDatosUsuarioContraido.Visible = false;

            // Restaurar botones (texto + icono alineado izquierda)
            foreach (var pnl in new[] { panelMenu, panelTreeView })
            {
                foreach (RoundedButton btn in pnl.Controls.OfType<RoundedButton>())
                {
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;

                    // Recupera texto desde Tag (IMPORTANTE)
                    if (btn.Tag != null)
                        btn.Text = btn.Tag.ToString();

                    btn.Padding = new Padding(10, 0, 0, 0);
                }
            }

            panelColapsarMenu.Dock = DockStyle.Top;
            panelColapsarMenu.Height = 50; // por ejemplo
            panelMenu.Controls.SetChildIndex(panelColapsarMenu, 0);

            panelColapsarMenu.SendToBack();
            // Reanudar layouts
            panelDatosUsuarioContraido.ResumeLayout(true);
            panelDatosUsuarioExpandido.ResumeLayout(true);
            panelTreeView.ResumeLayout(true);
            panelColapsarMenu.ResumeLayout(true);
            panelMenu.ResumeLayout(true);


            //panelMenu.Visible = true;
            panelMenu.Refresh();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxUserContraido,
        $"Usuario: {UserSession.Usuario}\n" +
        $"Nombre: {UserSession.Nombres} {UserSession.Apellidos}");
        }

        private void btnLogOut2_Click(object sender, EventArgs e)
        {
            LogoutAction();
        }

        private void treeView1_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //treeView1.SelectedNode = e.Node;
        }

        private void AjustarAnchoTreeView()
        {
            int anchoMax = 0;
            foreach (TreeNode nodo in treeView1.Nodes)
            {
                anchoMax = Math.Max(anchoMax, MedirAnchoNodo(nodo));
            }
            treeView1.Width = Math.Min(anchoMax + 50, panelMenu.Width); // 50px extra para margen
        }

        private int MedirAnchoNodo(TreeNode nodo)
        {
            int ancho = TextRenderer.MeasureText(nodo.Text, treeView1.Font).Width + 20 + (nodo.Level * 20);
            foreach (TreeNode hijo in nodo.Nodes)
            {
                ancho = Math.Max(ancho, MedirAnchoNodo(hijo));
            }
            return ancho;
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // Colores para seleccionado y no seleccionado
            Color backColor = (e.State & TreeNodeStates.Selected) != 0
                ? Color.FromArgb(0, 120, 215)  // Azul moderno
                : Color.FromArgb(255, 255, 255);//Color.FromArgb(243, 237, 228); 
            Color foreColor = (e.State & TreeNodeStates.Selected) != 0
                ? Color.FromArgb(255, 255, 255)
                //Color.FromArgb(243, 237, 228)
                : Color.FromArgb(45, 45, 45);

            //Color.FromArgb(243, 237, 228);
            // Dibujar fondo
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Calcular el ancho real del texto
            Size textSize = TextRenderer.MeasureText(e.Node.Text, treeView1.Font);

            // Rectángulo extendido según ancho del texto
            Rectangle textRect = new Rectangle(
                e.Bounds.X + 1 + (e.Node.Level * 1), // Sangría por nivel
                e.Bounds.Y,
                textSize.Width + 10,                  // Ancho suficiente para texto completo
                e.Bounds.Height
            );

            // Dibujar el texto
            TextRenderer.DrawText(
                e.Graphics,
                e.Node.Text,
                treeView1.Font,
                textRect,
                foreColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );

            e.DrawDefault = false;

        }

        private async void MenuPrincipal_ResizeEnd(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            await AbrirFormularioConLoaderAsync(new FrmDashboard());
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.GetNodeAt(e.Location);

            if (node != null && treeView1.SelectedNode != node)
            {
                treeView1.SelectedNode = node;
            }
        }

        private void treeView1_MouseLeave(object sender, EventArgs e)
        {
            treeView1.SelectedNode = null;
        }
    }
}
