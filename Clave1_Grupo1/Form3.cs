using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public partial class Form3 : Form
    {
        static string servidor = "localhost";
        static string bd = "veterinariapatitasypelos";
        static string usuario = "root";
        static string password = "root";

        static string cadenaConexion =
            $"Server={servidor};Port=3306;Database={bd};User Id={usuario};Password={password};" +
            "SslMode=none;AllowPublicKeyRetrieval=True";
        // Definir varables globales
        List<dynamic> _usuario = new List<dynamic>();
        public Form3(List<dynamic> usuario)
        {
            InitializeComponent();
            _usuario = usuario;

        }

        private string ObtenerMotivoSeleccionado()
        {
            // Usa el CheckedListBox que ya tienes: cklistMotivoCita
            var motivos = new List<string>();

            foreach (var item in cklistMotivoCita.CheckedItems)
            {
                motivos.Add(item.ToString());
            }

            // Devuelve todos los motivos separados por coma
            return string.Join(", ", motivos);
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
            txtNombrePro.Text = $"{_usuario[1]} {_usuario[2]}";
            txtEmailContacto.Text = _usuario[5];
            txtTelefonoContacto.Text = _usuario[4];

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
        private void txtNombrePro_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblApellidoPro_Click(object sender, EventArgs e)
        {

        }

        private void lblNombrePro_Click(object sender, EventArgs e)
        {

        }

        private void txtApellidoPro_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDireccion_Click(object sender, EventArgs e)
        {

        }

        private void txtDireccionPro_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTelefono_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefonoContacto_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void txtEmailContacto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cklistMotivoCita_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rtxtSintomas_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void mcCalendarioAgendarCita_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
 

        private void btnAgendarCita_Click(object sender, EventArgs e)
        {
            // 1. Obtener datos del formulario
            string nombre = txtNombrePro.Text.Trim();
            string apellido = txtApellidoPro.Text.Trim();
            string direccion = txtDireccionPro.Text.Trim();
            string telefono = txtTelefonoContacto.Text.Trim();
            string email = txtEmailContacto.Text.Trim();

            DateTime fechaCita = mcCalendarioAgendarCita.SelectionStart;

            string motivo = ObtenerMotivoSeleccionado();
            string sintomas = rtxtSintomas.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Complete los datos del propietario y seleccione un motivo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var cn = new MySqlConnection(cadenaConexion))
                {
                    cn.Open();

                    // 2. Insertar cliente si no existe
                    string sqlCliente =
                        @"INSERT INTO clientes (nombre, apellido, direccion, telefono, email)
                  VALUES (@nombre, @apellido, @direccion, @telefono, @email);
                  SELECT LAST_INSERT_ID();";

                    int idCliente;

                    using (var cmd = new MySqlCommand(sqlCliente, cn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@email", email);

                        idCliente = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Insertar cita
                    string sqlCita =
                        @"INSERT INTO citas (fechaCita, motivoCita, sintomas, idMascota, idCliente)
                  VALUES (@fechaCita, @motivoCita, @sintomas, @idMascota, @idCliente);";

                    using (var cmd = new MySqlCommand(sqlCita, cn))
                    {
                        cmd.Parameters.AddWithValue("@fechaCita", fechaCita);
                        cmd.Parameters.AddWithValue("@motivoCita", motivo);
                        cmd.Parameters.AddWithValue("@sintomas", sintomas);
                        cmd.Parameters.AddWithValue("@idMascota", 0);   // si no manejas mascotas aún
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cita agendada correctamente ✔",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cita: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarCita_Click(object sender, EventArgs e)
        {

        }
    }
}
