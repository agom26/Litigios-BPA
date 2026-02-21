using Comun;
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
namespace Presentacion
{
    public partial class MenuPrincipal : Form
    {
        bool menuExpandido = true;
        
        public MenuPrincipal()
        {
            InitializeComponent();
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.ShowLines = false;
            treeView1.ShowPlusMinus = false;
            treeView1.ShowRootLines = false;
            treeView1.FullRowSelect = true;
            treeView1.HideSelection = false;
            treeView1.BackColor = Color.White;
            treeView1.ForeColor = Color.FromArgb(45, 45, 45);
            treeView1.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;

            treeView1.DrawNode += treeView1_DrawNode; // Evento para dibujar nodos personalizados


            panelDatosUsuarioExpandido.Visible = true;
            panelDatosUsuarioContraido.Visible = false;
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


                FormLogin2 login = new FormLogin2();
                login.Show();


                this.Dispose();
            }
        }

        private void CargarMenuPorModulos()
        {
            treeView1.Nodes.Clear();

            foreach (string modulo in UserSession.Modulos)
            {
                switch (modulo.ToLower())
                {
                    case "laboral":
                        TreeNode laboral = CrearNodo("Laboral", "laboral");
                        laboral.Nodes.Add(CrearNodo("Primer Instancia", "laboral"));
                        laboral.Nodes.Add(CrearNodo("Recursos contra soluciones", "laboral"));
                        laboral.Nodes.Add(CrearNodo("Segunda Instancia", "laboral"));
                        treeView1.Nodes.Add(laboral);
                        break;

                    case "civil":
                        treeView1.Nodes.Add(CrearNodo("Civil", "civil"));
                        break;

                    case "constitucional":
                        treeView1.Nodes.Add(CrearNodo("Constitucional", "constitucional"));
                        break;

                    case "contencioso administrativo":
                        TreeNode contencioso = CrearNodo("Contencioso Administrativo", "contencioso");
                        contencioso.Nodes.Add(CrearNodo("General", "contencioso"));
                        contencioso.Nodes.Add(CrearNodo("Tributario", "contencioso"));
                        treeView1.Nodes.Add(contencioso);
                        break;

                    case "personas involucradas":

                        TreeNode personas = CrearNodo("Personas involucradas", "personas");
                        personas.Nodes.Add(CrearNodo("Demandados / Autoridad Responsable", "personas"));
                        personas.Nodes.Add(CrearNodo("Demandantes / Actor", "personas"));
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
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            CargarMenuPorModulos();
            CargarDatosUsuario();
            AjustarAnchoTreeView();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
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
                else if (nodo.Text == "Demandados / Autoridad Responsable" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
                {
                    openChildForm(new Demandados());

                }
                else if (nodo.Text == "Demandantes / Actor" && nodo.Parent != null && nodo.Parent.Text == "Personas involucradas")
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
                    openChildForm(new Laboral_primer_instancia());
                }
                else if (nodo.Text == "Inicio")
                {
                }
            }
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
            Application.Exit();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LogoutAction();
        }

        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            timerMenu.Start();

        }

        private void timerMenu_Tick(object sender, EventArgs e)
        {
            if (menuExpandido)
            {
                panelMenu.Width -= 20;

                panelTreeView.Visible = false;

                panelDatosUsuarioExpandido.Visible = false;
                if (panelMenu.Width <= 60)
                {
                    panelDatosUsuarioContraido.Visible = true;
                    menuExpandido = false;

                    timerMenu.Stop();
                }
            }
            else
            {
                panelMenu.Width += 20;


                panelDatosUsuarioContraido.Visible = false;

                if (panelMenu.Width >= 250)
                {
                    panelTreeView.Visible = true;
                    panelDatosUsuarioExpandido.Visible = true;
                    panelTreeView.Visible = true;
                    menuExpandido = true;
                    timerMenu.Stop();
                }
            }
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
            treeView1.SelectedNode = e.Node;
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
                : Color.White;
            Color foreColor = (e.State & TreeNodeStates.Selected) != 0
                ? Color.White
                : Color.FromArgb(45, 45, 45);

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

            /*
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(0, 120, 215)),
                    e.Bounds);
                TextRenderer.DrawText(
                    e.Graphics,
                    e.Node.Text,
                    treeView1.Font,
                    e.Bounds,
                    Color.White);
            }
            else
            {
                e.DrawDefault = true;
            }*/
        }
    }
}
