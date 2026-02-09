using Comun;
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
namespace Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();

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
                        treeView1.Nodes.Add(CrearNodo("Laboral", "laboral"));
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
    }
}
