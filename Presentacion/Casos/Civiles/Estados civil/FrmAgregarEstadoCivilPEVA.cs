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
    public partial class FrmAgregarEstadoCivilPEVA : Form
    {
        public FrmAgregarEstadoCivilPEVA()
        {
            InitializeComponent();
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

            if (estado == "Publicación de orden de remate")
            {
                dateTimePickerFecha1.Enabled = true;
                dateTimePickerFecha2.Enabled = true;
                dateTimePickerFecha3.Enabled = true;

                string f1 = dateTimePickerFecha1.Value.ToString("dd/MM/yyyy");
                string f2 = dateTimePickerFecha2.Value.ToString("dd/MM/yyyy");
                string f3 = dateTimePickerFecha3.Value.ToString("dd/MM/yyyy");

                observacion += $" | Publicaciones: {f1}, {f2}, {f3}";
            }
            else
            {
                dateTimePickerFecha1.Enabled = false;
                dateTimePickerFecha2.Enabled = false;
                dateTimePickerFecha3.Enabled = false;
            }


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
                estado == "Excepciones" ||
                estado == "Publicación de orden de remate" ||
                estado == "Remate" ||
                estado == "Incidente de liquidación" ||
                estado == "Incidente de liquidación/Recurso de Apelación" ||
                estado == "Incidente de liquidación/Recurso de Ampliación y Aclaración" ||
                estado == "Incidente de liquidación/Recurso de Ampliación y Aclaración/Apertura a prueba" ||
                estado == "Traslativa de dominio" ||
                estado == "Entrega de bienes"
                ;

            checkBoxTieneVencimiento.Checked = requiereVencimiento;
        }

        private void dateTimePickerFechaEstado_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void comboboxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            VerificarSiEstadoTieneVencimientoAutomatico();
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

            EstadoCivil.fechaEstado = fechaEstado;
            EstadoCivil.estado = comboboxEstado.Text;
            EstadoCivil.fechaVencimiento = fechaVencimiento;
            EstadoCivil.observaciones = formatoCorrecto;
            this.Close();
        }

        private void dateTimePickerFecha1_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void dateTimePickerFecha2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }

        private void dateTimePickerFecha3_ValueChanged(object sender, EventArgs e)
        {
            ActualizarObservaciones();
        }
    }
}
