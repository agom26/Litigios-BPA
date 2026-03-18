using Comun.DatosParaInterfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Casos.Civiles.Estados_civil
{
    public partial class FrmAgregarEstadoCivilTerminado : Form
    {
        public FrmAgregarEstadoCivilTerminado()
        {
            InitializeComponent();
            comboboxEstado.SelectedIndex = 0;
            VerificarEstadoCheckBoxTieneVencimiento();
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
        }

        private void dateTimePickerHoraVencimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            EstadoCivil.LimpiarEstado();
        }

        private void VerificarEstadoCheckBoxTieneVencimiento()
        {
            
        }
        private void checkBoxTieneVencimiento_CheckedChanged(object sender, EventArgs e)
        {
            VerificarEstadoCheckBoxTieneVencimiento();
            ActualizarObservaciones();
        }

        private void ActualizarObservaciones()
        {
            if (comboboxEstado.SelectedItem == null)
                return;

            string fechaEstado = dateTimePickerFechaEstado.Value
                .ToString("dd/MM/yyyy");

            string estado = comboboxEstado.Text;

            string observacion = $"{fechaEstado} {estado}";

            txtObservaciones.Text = observacion;
        }

        private void dateTimePickerFechaEstado_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void comboboxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            
            ActualizarObservaciones();
        }

        private void dateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }
        private string GenerarFormatoBase()
        {
            string fechaEstado = dateTimePickerFechaEstado.Value
                .ToString("dd/MM/yyyy");

            string estado = comboboxEstado.Text;

            string observacion = $"{fechaEstado} {estado}";

            return observacion;
        }

        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {

            string formatoBase = GenerarFormatoBase();
            string formatoCorrecto = formatoBase;
            DateTime fechaEstado;
            DateTime? fechaVencimiento;
            string textoUsuario = txtObservaciones.Text.Trim();

            // Si el usuario borró todo o dañó el formato
            if (!textoUsuario.StartsWith(dateTimePickerFechaEstado.Value.ToString("dd/MM/yyyy")))
            {
                // Reconstruimos el formato y agregamos lo que el usuario escribió
                if (!string.IsNullOrWhiteSpace(textoUsuario))
                    formatoCorrecto += " " + textoUsuario;

                txtObservaciones.Text = formatoCorrecto;
            }
            else
            {
                formatoCorrecto = textoUsuario;
            }

            if (comboboxEstado.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un estado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 🔥 VALIDACIÓN NUEVA: obligar motivo
            if (textoUsuario.Length <= formatoBase.Length)
            {
                MessageBox.Show("Debe escribir el motivo de por qué se envía a terminado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 🔥 VALIDACIÓN EXTRA: evitar que solo pongan espacios
            string textoExtra = textoUsuario.Substring(formatoBase.Length).Trim();
            if (string.IsNullOrWhiteSpace(textoExtra))
            {
                MessageBox.Show("Debe ingresar un motivo válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            fechaEstado = dateTimePickerFechaEstado.Value.Date;
            fechaVencimiento = null;

            EstadoCivil.fechaEstado = fechaEstado;
            EstadoCivil.estado = comboboxEstado.Text;
            EstadoCivil.fechaVencimiento = fechaVencimiento;
            EstadoCivil.observaciones = formatoCorrecto;
            this.Close();
        }
    }
}
