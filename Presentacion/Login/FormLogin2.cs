
using Comun;
using Dominio.Entidades;
using Dominio;          
using Comun.Models;   

using System.Runtime.InteropServices;
namespace Presentacion
{
    public partial class FormLogin2 : Form
    {
        private UserModel userModel = new UserModel();
        private bool verPassword = false;

        public FormLogin2()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        // Importar la función SendMessage de la user32.dll
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        
        private void RecordarSesion(string user, string pass)
        {
            if (checkBoxRememberMe.Checked)
            {
                Properties.Settings.Default.Usuario = user;
                Properties.Settings.Default.Contrasena = pass;
                Properties.Settings.Default.Recordar = checkBoxRememberMe.Checked;
            }
            else
            {
                Properties.Settings.Default.Usuario = "";
                Properties.Settings.Default.Contrasena = "";
                Properties.Settings.Default.Recordar = false;
            }
            Properties.Settings.Default.Save();
            Presentacion.Properties.Settings.Default.Reload();
        }
        

        private async Task Login()
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            btnLogin.Enabled = false;
            txtUsuario.Enabled = false;
            txtPassword.Enabled = false;

            try
            {
                var response = await userModel.LoginUser(user, pass);

                if (response != null && response.success)
                {

                    RecordarSesion(user, pass);
                    UserSession.Usuario = response.data.usuario;
                    UserSession.Nombres = response.data.nombres;
                    UserSession.Apellidos= response.data.apellidos;
                    UserSession.Modulos=response.data.modulos;

                    MenuPrincipal menuPrincipal = new MenuPrincipal();
                    this.Hide();
                    menuPrincipal.ShowDialog();


                }
                else
                {
                    MessageBox.Show(response.message, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Enabled = true;
                    txtUsuario.Enabled = true;
                    btnLogin.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private async void roundedButton1_Click(object sender, EventArgs e)
        {
            await Login();
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Detecta la tecla Enter
            {
                e.SuppressKeyPress = true; // Evita el sonido de beep
                btnLogin.PerformClick();   // Simula click en el botón login
            }
        }

        private void FormLogin2_Load(object sender, EventArgs e)
        {
            btnVerPass.Image = Properties.Resources.ojo_cerrado;
            txtPassword.UseSystemPasswordChar = true;

            if (Presentacion.Properties.Settings.Default.Recordar == true)
            {
                txtUsuario.Text = Presentacion.Properties.Settings.Default.Usuario;
                txtPassword.Text = Presentacion.Properties.Settings.Default.Contrasena;
                checkBoxRememberMe.Checked = Presentacion.Properties.Settings.Default.Recordar;
                btnLogin.Focus();
            }
            else
            {
                txtUsuario.Text = "";
                txtPassword.Text = "";
                checkBoxRememberMe.Checked = false;
            }
        }

        private void btnVerPass_Click(object sender, EventArgs e)
        {
            verPassword = !verPassword;

            if (verPassword == false)
            {
                btnVerPass.Image = Properties.Resources.ojo_cerrado;
                txtPassword.UseSystemPasswordChar = true;
            }
            else if (verPassword == true)
            {
                btnVerPass.Image = Properties.Resources.ojo_abierto;
                txtPassword.UseSystemPasswordChar = false;
            }

        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Detecta la tecla Enter
            {
                e.SuppressKeyPress = true; // Evita el sonido de beep
                btnLogin.PerformClick();   // Simula click en el botón login
            }
        }
    }
}
