using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Alertas
{
    public partial class FrmLoading : Form
    {
        public Func<Task> ProcesoAsync { get; set; }
        private bool _running = false;

        public FrmLoading(Func<Task> procesoAsync)
        {
            InitializeComponent();
            ProcesoAsync = procesoAsync ?? throw new ArgumentNullException(nameof(procesoAsync));

            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_running) return;
            _running = true;

            await Task.Delay(50); // deja que pinte bien

            try
            {
                await ProcesoAsync();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                DialogResult = DialogResult.Abort;
            }
            finally
            {
                Close();
            }
        }
    }
}
