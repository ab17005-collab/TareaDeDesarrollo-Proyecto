using System;
using System.Collections.Generic;
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
    }
}
