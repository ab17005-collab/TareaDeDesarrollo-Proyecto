using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clave1_Grupo1
{
    public partial class Form3 : Form
    {
        // Definir varables globales
        Dictionary<string, string> _usuario = new Dictionary<string, string>();
        public Form3(Dictionary<string, string> usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }


        private void Form3_Load(object sender, EventArgs e)
        {

            // Mostrar el ID en el texto del formulario
            this.Text = $"Cuenta: {_usuario["idCliente"]}";

            // Mostrar los datos del usuario en los textBoxes
            txtPropietario.Text = $"{_usuario["nombre"]} {_usuario["apellido"]}";
            txtEmail.Text = _usuario["correo"];
            txtTelefono.Text = _usuario["telefono"];


            // Mostrar las fechas disponibles en negrita en mcCalendarioAgendarCita
            mcCalendarioAgendarCita.BoldedDates = new DateTime[]
            {
                new DateTime(2025, 10, 28),
                new DateTime(2025 , 10, 29),
                new DateTime(2025, 10, 30)
            };

            // Configurar el data gird view
            dgvSlotsDisponibles.Columns.Add("Time", "Time");
            dgvSlotsDisponibles.Columns.Add("DrMartinez", "Dr. Martinez");
            dgvSlotsDisponibles.Columns.Add("DraLopez", "Dra. Lopez");

            for (int hour = 8; hour < 18; hour++)
            {
                dgvSlotsDisponibles.Rows.Add($"{hour}:00", "", "");
                dgvSlotsDisponibles.Rows.Add($"{hour}:30", "", "");
            }

            // Colorear celdas dinamicamente
            dgvSlotsDisponibles.Rows[3].Cells[1].Style.BackColor = Color.LightGreen; // available
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmNuevaMascota nuevaMascotaForm = new frmNuevaMascota(_usuario);
            nuevaMascotaForm.ShowDialog();

        }

        private void btnNuevaMascota_Click(object sender, EventArgs e)
        {
            int idCliente = int.Parse(_usuario["idCliente"]);
            frmNuevaMascota nuevaMascotaForm = new frmNuevaMascota(idCliente);
            nuevaMascotaForm.ShowDialog();
        }
    }
}
