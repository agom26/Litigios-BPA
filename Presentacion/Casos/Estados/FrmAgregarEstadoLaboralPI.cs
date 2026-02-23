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

namespace Presentacion.Casos.Estados
{
    public partial class FrmAgregarEstadoLaboralPI : Form
    {
        public FrmAgregarEstadoLaboralPI()
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
            EstadoLaboral.LimpiarEstadoLaboral();
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
                estado == "Juicio Verbal y Período Conciliatorio" ||
                estado == "Segunda Audiencia" ||
                estado == "Tercera Audiencia para recepción de pruebas" ||
                estado == "Audiencia de Incidentes" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de revocatoria" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Apelación" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Apelación";

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

            EstadoLaboral.fechaEstado = fechaEstado;
            EstadoLaboral.estado = comboboxEstado.Text;
            EstadoLaboral.fechaVencimiento = fechaVencimiento;
            EstadoLaboral.observaciones = formatoCorrecto;
            this.Close();
        }
    }
}
