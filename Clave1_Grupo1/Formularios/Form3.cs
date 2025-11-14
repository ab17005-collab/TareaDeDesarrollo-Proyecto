using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Clave1_Grupo1
{
    public partial class Form3 : Form
    {
        static string cadenaConexion =
            $"Server=localhost; Database=veterinariapatitasypelos; Uid=root; Pwd=root;";

        Dictionary<string, string> _usuario = new Dictionary<string, string>();

        public Form3(Dictionary<string, string> usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        // Campos globales
        Dictionary<string, bool> horaTurno = new Dictionary<string, bool>();

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = $"Cuenta: {_usuario["idCliente"]}";

            txtPropietario.Text = $"{_usuario["nombre"]} {_usuario["apellido"]}";
            txtEmail.Text = _usuario["correo"];
            txtTelefono.Text = _usuario["telefono"];

            CargarMascotas();    // Cargar mascotas al iniciar el form

            CargarVeterinarios(); // Cargar veterinarios en el combo box al iniciar el form

            GenerarYLlenarSlots(); // Generar y llenar slots disponibles

            dgvSlotsDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        // MÉTODO PARA CARGAR LAS MASCOTAS DEL CLIENTE DESDE BD
        private void CargarMascotas()
        {
            cbxMascotas.Items.Clear();
            cbxMascotas.Items.Add("Seleccionar");
            cbxMascotas.SelectedIndex = 0;
            cbxMascotas.DropDownStyle = ComboBoxStyle.DropDownList;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    con.Open();

                    string query = "SELECT nombre FROM mascotas WHERE id_cliente = @idCliente";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idCliente", _usuario["idCliente"]);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    bool tieneMascotas = false;

                    while (reader.Read())
                    {
                        cbxMascotas.Items.Add(reader.GetString("nombre"));
                        tieneMascotas = true;
                    }

                    if (!tieneMascotas)
                    {
                        cbxMascotas.Items.Add("No tiene mascotas registradas");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar mascotas:\n\n" + ex.Message);
                }
            }
        }

        // ABRIR FORM DE NUEVA MASCOTA Y RECARGAR AL CERRAR
        private void btnNuevaMascota_Click(object sender, EventArgs e)
        {
            int idCliente = int.Parse(_usuario["idCliente"]);

            frmNuevaMascota nuevaMascotaForm = new frmNuevaMascota(idCliente);

            nuevaMascotaForm.FormClosed += (s, args) => CargarMascotas();
            nuevaMascotaForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btnNuevaMascota_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // MÉTODO PARA GENERAR Y LLENAR LOS SLOTS DE TURNOS DISPONIBLES
        private void GenerarYLlenarSlots()
        {
            horaTurno.Clear();
            dgvSlotsDisponibles.Rows.Clear();

            // Crear columnas solo si no existen
            if (dgvSlotsDisponibles.Columns.Count == 0)
            {
                dgvSlotsDisponibles.Columns.Add("HorarioMatutino", "Horario Matutino");
                dgvSlotsDisponibles.Columns.Add("HorarioVespertino", "Horario Vespertino");
            }

            List<string> matutinos = new List<string>();
            List<string> vespertinos = new List<string>();

            // --- HORARIO DE LA MAÑANA ---
            DateTime inicio = DateTime.Today.AddHours(8);  // 8:00 AM
            DateTime fin = DateTime.Today.AddHours(12);    // 12:00 PM

            while (inicio < fin)
            {
                DateTime mediaHora = inicio.AddMinutes(30);
                string key = $"{inicio:h:mm:ss tt} - {mediaHora:h:mm:ss tt}";

                horaTurno[key] = true;  // Default: disponible

                matutinos.Add(key);
                inicio = mediaHora;
            }

            // --- HORARIO DE LA TARDE ---
            inicio = DateTime.Today.AddHours(13); // 1:00 PM
            fin = DateTime.Today.AddHours(17);    // 5:00 PM

            while (inicio < fin)
            {
                DateTime mediaHora = inicio.AddMinutes(30);
                string key = $"{inicio:h:mm:ss tt} - {mediaHora:h:mm:ss tt}";

                horaTurno[key] = true;  // Default: disponible

                vespertinos.Add(key);
                inicio = mediaHora;
            }

            // --- LLENAR EL DATAGRID ---
            int maxRows = Math.Max(matutinos.Count, vespertinos.Count);

            for (int i = 0; i < maxRows; i++)
            {
                string colAM = i < matutinos.Count ? matutinos[i] : "";
                string colPM = i < vespertinos.Count ? vespertinos[i] : "";

                int rowIndex = dgvSlotsDisponibles.Rows.Add(colAM, colPM);

                // --- Pintar el color según disponibilidad ---
                DataGridViewRow row = dgvSlotsDisponibles.Rows[rowIndex];

                bool isAM = colAM != "" && horaTurno.ContainsKey(colAM);
                bool isPM = colPM != "" && horaTurno.ContainsKey(colPM);

                // Si hay intervalo AM, pintar según true/false
                if (isAM)
                {
                    row.Cells[0].Style.BackColor = horaTurno[colAM] ? Color.LightGray : Color.Red;
                }

                // Si hay intervalo PM, pintar según true/false
                if (isPM)
                {
                    row.Cells[1].Style.BackColor = horaTurno[colPM] ? Color.LightGray : Color.Red;
                }
            }
        }

        // MÉTODO PARA CARGAR VETERINARIOS EN EL COMBOBOX
        private void CargarVeterinarios()
        {
            cbxVeterinario.Items.Clear();
            cbxVeterinario.Items.Add("Seleccionar");
            cbxVeterinario.SelectedIndex = 0;
            cbxVeterinario.DropDownStyle = ComboBoxStyle.DropDownList;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    con.Open();

                    string query = @"
                SELECT u.id_usuario, u.nombre, u.apellido
                FROM usuarios u
                INNER JOIN veterinarios v ON u.id_usuario = v.id_usuario
                WHERE u.rol = 'Veterinario'
                ORDER BY u.nombre ASC;";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    bool existenVets = false;

                    while (reader.Read())
                    {
                        string nombreCompleto = $"{reader.GetString("nombre")} {reader.GetString("apellido")}";
                        cbxVeterinario.Items.Add(nombreCompleto);
                        existenVets = true;
                    }

                    if (!existenVets)
                    {
                        cbxVeterinario.Items.Add("No hay veterinarios disponibles");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los veterinarios:\n\n" + ex.Message);
                }
            }
        }


        private void btnAgendar_Click(object sender, EventArgs e)
        {

        }
    }
}
