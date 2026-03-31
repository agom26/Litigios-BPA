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

namespace Presentacion.Casos.Contenciosos.Estados_contenciosos
{
    public partial class FrmAgregarEstadoContenciosoGPI : Form
    {
        public string? expedienteC { get; set; }
        public string? motivoC { get; set;}

        public FrmAgregarEstadoContenciosoGPI()
        {
            InitializeComponent();
            comboboxEstado.SelectedIndex = -1;
            VerificarEstadoCheckBoxTieneVencimiento();
            AjustarFilasSegunEstado();
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
            EstadoContencioso.LimpiarEstado();
        }

        private void VerificarEstadoCheckBoxTieneVencimiento()
        {
            if (checkBoxTieneVencimiento.Checked)
            {
                dateTimePickerFechaVencimiento.Enabled = true;
                dateTimePickerHoraVencimiento.Enabled = true;
            }
            else
            {
                dateTimePickerFechaVencimiento.Enabled = false;
                dateTimePickerHoraVencimiento.Enabled = false;
            }
        }

        private void AjustarFilasSegunEstado()
        {
            /*if (comboboxEstado.SelectedItem == null)
                return;*/

            string estado = comboboxEstado.Text;

            bool esSentencia = estado == "Sentencia/Recurso de Casación";

            if (esSentencia)
            {
                // Mostrar ambas filas
                tableLayoutPanel1.RowStyles[0].Height = 63.52F;
                tableLayoutPanel1.RowStyles[1].Height = 36.48F;

                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            }
            else
            {
                // Ocultar la fila 0 y dejar solo la fila 1
                tableLayoutPanel1.RowStyles[0].Height = 0F;
                tableLayoutPanel1.RowStyles[1].Height = 100F;

                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            }

            // 🔥 Esto oculta/mostrar controles de la fila 0
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (tableLayoutPanel1.GetRow(ctrl) == 0)
                {
                    ctrl.Visible = esSentencia;
                }
            }
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

            if (checkBoxTieneVencimiento.Checked)
            {
                DateTime fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;

                string fechaVencimientoTexto =
                    fechaVencimiento.ToString("dd/MM/yyyy HH:mm");

                observacion += $" | Fecha de vencimiento: {fechaVencimientoTexto}";
            }

            txtObservaciones.Text = observacion;
        }

        private void VerificarSiEstadoTieneVencimientoAutomatico()
        {
            if (comboboxEstado.SelectedItem == null)
                return;

            string estado = comboboxEstado.Text;

            bool requiereVencimiento =
                estado == "Apertura a prueba" ||
                estado == "Vista" ||
                estado == "Sentencia/Recurso de Casación";

            checkBoxTieneVencimiento.Checked = requiereVencimiento;
        }

        private void dateTimePickerFechaEstado_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void comboboxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomatico();
            AjustarFilasSegunEstado();
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

            if (checkBoxTieneVencimiento.Checked)
            {
                DateTime fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;

                string fechaVencimientoTexto =
                    fechaVencimiento.ToString("dd/MM/yyyy HH:mm");

                observacion += $" | Fecha de vencimiento: {fechaVencimientoTexto}";
            }

            return observacion;
        }

        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            string formatoCorrecto = GenerarFormatoBase();
            DateTime fechaEstado;
            DateTime? fechaVencimiento;
            string textoUsuario = txtObservaciones.Text.Trim();
            string estadoSeleccionado = comboboxEstado.Text;

            if (estadoSeleccionado == "Sentencia/Recurso de Casación")
            {
                // Validar expediente
                if (string.IsNullOrWhiteSpace(txtExpediente.Text))
                {
                    MessageBox.Show("Debe ingresar el expediente para recurso de casación.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtExpediente.Focus();
                    return;
                }
                else
                {
                    expedienteC = txtExpediente.Text.Trim();
                }

                // Validar motivo
                if (comboBoxMotivoRecursoCasacion.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un motivo de recurso.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    comboBoxMotivoRecursoCasacion.Focus();
                    return;
                }
                else
                {
                    if(comboBoxMotivoRecursoCasacion.Text == "De forma")
                    {
                        motivoC = "FORMA";
                    }
                    else if(comboBoxMotivoRecursoCasacion.Text == "De fondo")
                    {
                        motivoC = "FONDO";
                    }
                    else if(comboBoxMotivoRecursoCasacion.Text == "De fondo y forma")
                    {
                        motivoC= "FORMA Y FONDO";
                    }
                }
            }

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

            if (checkBoxTieneVencimiento.Checked)
            {
                fechaEstado = dateTimePickerFechaEstado.Value.Date;
               

                fechaVencimiento =
                    dateTimePickerFechaVencimiento.Value.Date +
                    dateTimePickerHoraVencimiento.Value.TimeOfDay;
                
                if (fechaVencimiento <= fechaEstado)
                {
                    MessageBox.Show("La fecha de vencimiento debe ser mayor a la fecha del estado.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

            }
            else
            {
                fechaEstado = dateTimePickerFechaEstado.Value.Date;
                fechaVencimiento = null;
            }

            EstadoContencioso.fechaEstado = fechaEstado;
            EstadoContencioso.estado = comboboxEstado.Text;
            EstadoContencioso.fechaVencimiento = fechaVencimiento;
            EstadoContencioso.observaciones = formatoCorrecto;
            this.Close();
        }
    }
}
