using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clave1_Grupo1
{
    public partial class Form4 : Form
    {
        private Dictionary<string, string> _datosCita;

        public Form4(Dictionary<string, string> datosCita)
        {
            InitializeComponent();
            _datosCita = datosCita;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Aquí puedes mostrar los datos en labels, textboxes, etc.
            // Ejemplo si tienes label lblResumen:
            // lblResumen.Text = $"Cita #{_datosCita["numeroCita"]} - {_datosCita["fecha"]} { _datosCita["hora"]}";

        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            lblNombreMascota.Text = _datosCita["nombreMascota"];
            lblEspecie.Text = _datosCita["especieMascota"];
            lblRaza.Text = _datosCita["razaMazcota"];
            lblEdad.Text = _datosCita["edadMascota"];
            lblSexo.Text = _datosCita["sexoMascota"];
            lblNombreDueno.Text = _datosCita["nombreDueno"];
            lblTelefono.Text = "📞 " + _datosCita["telefono"];
            lblCorreo.Text = "📧 " + _datosCita["correoDueno"];
            lblDireccion.Text = "📍 " + _datosCita["dirreccionDueno"];
            lblFechaCita.Text = "📆 " + _datosCita["fecha"];
            lblHoraCita.Text = "🕑 " + _datosCita["hora"];
            lblMotivo.Text = _datosCita["MotivoCita"];
            lblNotas.Text = _datosCita["notas"];
            lblCitaId.Text = "Cita #: " + _datosCita["numeroCita"];
            //lblDiagnostico.Text = _datosCita["diagnostico"];
            lblVeterinario.Text = _datosCita["nombreVeterinario"];


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
