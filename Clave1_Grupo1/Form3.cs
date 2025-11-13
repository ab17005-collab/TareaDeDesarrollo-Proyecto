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
        List<dynamic> _usuario = new List<dynamic>();
        public Form3(List<dynamic> usuario)
        {
            InitializeComponent();
            _usuario = usuario;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /*MessageBox.Show(
                $"Bienvenido al sistema, {_usuario[1]} {_usuario[2]}!\n\n" +
                $"ID: {_usuario[0]}\n" +
                $"Nombre: {_usuario[1]} {_usuario[2]}\n" +
                $"Edad: {_usuario[3]}\n" +
                $"Teléfono: {_usuario[4]}\n" +
                $"Email: {_usuario[5]}\n" +
                $"Dirección: {_usuario[6]}\n" +
                $"Código de Cliente: {_usuario[7]}"
            );*/

            // Mostrar el ID en el texto del formulario
            this.Text = $"Cuenta: {_usuario[0]}";

            // Mostrar los datos del usuario en los textBoxes
            txtPropietario.Text = $"{_usuario[1]} {_usuario[2]}";
            txtEmail.Text = _usuario[5];
            txtTelefono.Text = _usuario[4];

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
            frmNuevaMascota nuevaMascotaForm = new frmNuevaMascota();
            nuevaMascotaForm.ShowDialog();

        }

        private void btnNuevaMascota_Click(object sender, EventArgs e)
        {
            frmNuevaMascota nuevaMascotaForm = new frmNuevaMascota();
            nuevaMascotaForm.ShowDialog();
        }
    }
}
