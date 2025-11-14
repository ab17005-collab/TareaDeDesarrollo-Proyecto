using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public partial class Form3 : Form
    {
        static string cadenaConexion =
            $"Server=localhost; Database=veterinariapatitasypelos; Uid=root; Pwd=root;";

        Dictionary<string, string> _usuario = new Dictionary<string, string>();

        // Diccionario de horarios (key = "8:00:00 AM - 8:30:00 AM", value = disponible?)
        Dictionary<string, bool> horaTurno = new Dictionary<string, bool>();

        // Id del registro de horarios actualmente cargado (si existe)
        int? horarioActualId = null;

        // Clases internas para manejar ítems de combo
        private class VeterinarioItem
        {
            public int IdVeterinario { get; set; }
            public string NombreCompleto { get; set; }
            public override string ToString() => NombreCompleto;
        }

        private class MascotaItem
        {
            public int IdMascota { get; set; }
            public string Nombre { get; set; }
            public string Especie { get; set; }
            public string Raza { get; set; }
            public string Sexo { get; set; }
            public DateTime FechaNacimiento { get; set; }

            public override string ToString() => Nombre;
        }

        public Form3(Dictionary<string, string> usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = $"Cuenta: {_usuario["idCliente"]}";

            txtPropietario.Text = $"{_usuario["nombre"]} {_usuario["apellido"]}";
            txtEmail.Text = _usuario["correo"];
            txtTelefono.Text = _usuario["telefono"];

            CargarMascotas();
            CargarVeterinarios();

            // Configuración del DataGridView
            dgvSlotsDisponibles.Rows.Clear();
            dgvSlotsDisponibles.Columns.Clear();
            dgvSlotsDisponibles.Columns.Add("HorarioMatutino", "Horario Matutino");
            dgvSlotsDisponibles.Columns.Add("HorarioVespertino", "Horario Vespertino");
            dgvSlotsDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Configuración del calendar: ya viene configurado desde el diseñador.
            // Nos aseguramos de tener una fecha seleccionada
            mcCalendarioAgendarCita.MaxSelectionCount = 1;

            // Generar horario por defecto (sin veterinario aún)
            GenerarYLlenarSlots();

            // Suscribir eventos (por si no están en el diseñador)
            cbxVeterinario.SelectedIndexChanged += cbxVeterinario_SelectedIndexChanged;
            mcCalendarioAgendarCita.DateChanged += mcCalendarioAgendarCita_DateChanged;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        // ===================== MASCOTAS =======================

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

                    string query = @"SELECT id_mascota, nombre, especie, raza, sexo, fecha_nacimiento
                                     FROM mascotas
                                     WHERE id_cliente = @idCliente";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idCliente", _usuario["idCliente"]);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool tieneMascotas = false;

                        while (reader.Read())
                        {
                            MascotaItem mascota = new MascotaItem
                            {
                                IdMascota = reader.GetInt32("id_mascota"),
                                Nombre = reader.GetString("nombre"),
                                Especie = reader.GetString("especie"),
                                Raza = reader.GetString("raza"),
                                Sexo = reader.GetString("sexo"),
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento")
                            };

                            cbxMascotas.Items.Add(mascota);
                            tieneMascotas = true;
                        }

                        if (!tieneMascotas)
                        {
                            cbxMascotas.Items.Add("No tiene mascotas registradas");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar mascotas:\n\n" + ex.Message);
                }
            }
        }

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

        // ===================== VETERINARIOS =======================

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
                        SELECT v.id_veterinario, u.nombre, u.apellido
                        FROM veterinarios v
                        INNER JOIN usuarios u ON v.id_usuario = u.id_usuario
                        WHERE u.rol = 'Veterinario'
                        ORDER BY u.nombre ASC;";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    bool existenVets = false;

                    while (reader.Read())
                    {
                        VeterinarioItem item = new VeterinarioItem
                        {
                            IdVeterinario = reader.GetInt32("id_veterinario"),
                            NombreCompleto = $"{reader.GetString("nombre")} {reader.GetString("apellido")}"
                        };

                        cbxVeterinario.Items.Add(item);
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

        private void cbxVeterinario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHorarioParaSeleccion();
        }

        private void mcCalendarioAgendarCita_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarHorarioParaSeleccion();
        }

        // ===================== HORARIOS / DGV =======================

        // Genera horario por defecto para el día, sin guardar en DB
        private void GenerarYLlenarSlots()
        {
            horaTurno.Clear();

            // --- MAÑANA 8:00 AM - 12:00 PM ---
            DateTime inicio = DateTime.Today.AddHours(8);
            DateTime fin = DateTime.Today.AddHours(12);

            while (inicio < fin)
            {
                DateTime mediaHora = inicio.AddMinutes(30);
                string key = $"{inicio:h:mm:ss tt} - {mediaHora:h:mm:ss tt}";
                horaTurno[key] = true;
                inicio = mediaHora;
            }

            // --- TARDE 1:00 PM - 5:00 PM ---
            inicio = DateTime.Today.AddHours(13);
            fin = DateTime.Today.AddHours(17);

            while (inicio < fin)
            {
                DateTime mediaHora = inicio.AddMinutes(30);
                string key = $"{inicio:h:mm:ss tt} - {mediaHora:h:mm:ss tt}";
                horaTurno[key] = true;
                inicio = mediaHora;
            }

            LlenarDgvDesdeHoraTurno();
        }

        // Rellena dgvSlotsDisponibles desde el diccionario horaTurno
        private void LlenarDgvDesdeHoraTurno()
        {
            dgvSlotsDisponibles.Rows.Clear();

            // Ordenar por hora de inicio
            var keysOrdenadas = horaTurno.Keys.ToList();
            keysOrdenadas.Sort((a, b) =>
            {
                var h1 = DateTime.Parse(a.Split('-')[0].Trim(), CultureInfo.InvariantCulture);
                var h2 = DateTime.Parse(b.Split('-')[0].Trim(), CultureInfo.InvariantCulture);
                return h1.CompareTo(h2);
            });

            List<string> matutinos = new List<string>();
            List<string> vespertinos = new List<string>();

            foreach (var key in keysOrdenadas)
            {
                DateTime hInicio = DateTime.Parse(key.Split('-')[0].Trim(), CultureInfo.InvariantCulture);
                if (hInicio.Hour < 12)
                    matutinos.Add(key);
                else
                    vespertinos.Add(key);
            }

            int maxRows = Math.Max(matutinos.Count, vespertinos.Count);

            for (int i = 0; i < maxRows; i++)
            {
                string colAM = i < matutinos.Count ? matutinos[i] : "";
                string colPM = i < vespertinos.Count ? vespertinos[i] : "";

                int rowIndex = dgvSlotsDisponibles.Rows.Add(colAM, colPM);
                DataGridViewRow row = dgvSlotsDisponibles.Rows[rowIndex];

                if (colAM != "" && horaTurno.ContainsKey(colAM))
                    row.Cells[0].Style.BackColor = horaTurno[colAM] ? Color.LightGray : Color.Red;

                if (colPM != "" && horaTurno.ContainsKey(colPM))
                    row.Cells[1].Style.BackColor = horaTurno[colPM] ? Color.LightGray : Color.Red;
            }
        }

        // Carga el horario de la tabla 'horarios' si existe; si no, genera uno nuevo
        private void CargarHorarioParaSeleccion()
        {
            // Necesita veterinario seleccionado
            if (cbxVeterinario.SelectedIndex <= 0)
                return;

            VeterinarioItem vet = cbxVeterinario.SelectedItem as VeterinarioItem;
            if (vet == null) return;

            DateTime fecha = mcCalendarioAgendarCita.SelectionStart.Date;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    con.Open();

                    string query = @"SELECT id_horario, horario 
                                     FROM horarios
                                     WHERE id_veterinario = @idVet AND fecha = @fecha";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idVet", vet.IdVeterinario);
                    cmd.Parameters.AddWithValue("@fecha", fecha);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            horarioActualId = reader.GetInt32("id_horario");
                            string json = reader.IsDBNull(reader.GetOrdinal("horario"))
                                          ? null
                                          : reader.GetString("horario");

                            if (!string.IsNullOrWhiteSpace(json))
                            {
                                horaTurno = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json)
                                            ?? new Dictionary<string, bool>();
                            }
                            else
                            {
                                GenerarYLlenarSlots();
                                return;
                            }
                        }
                        else
                        {
                            // No hay registro: se genera el horario por defecto y no se guarda todavía.
                            horarioActualId = null;
                            GenerarYLlenarSlots();
                            return;
                        }
                    }

                    // Si se cargó desde JSON, se refresca el DGV
                    LlenarDgvDesdeHoraTurno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el horario:\n\n" + ex.Message);
                }
            }
        }

        // ===================== AGENDAR CITA =======================

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas: veterinario, mascota, slot, motivos
            if (cbxVeterinario.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un veterinario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxMascotas.SelectedIndex <= 0 || !(cbxMascotas.SelectedItem is MascotaItem mascota))
            {
                MessageBox.Show("Seleccione una mascota.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvSlotsDisponibles.CurrentCell == null ||
                dgvSlotsDisponibles.CurrentCell.Value == null ||
                string.IsNullOrWhiteSpace(dgvSlotsDisponibles.CurrentCell.Value.ToString()))
            {
                MessageBox.Show("Seleccione un horario en la tabla de slots disponibles.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VeterinarioItem vet = cbxVeterinario.SelectedItem as VeterinarioItem;
            if (vet == null)
            {
                MessageBox.Show("Seleccione un veterinario válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string slotKey = dgvSlotsDisponibles.CurrentCell.Value.ToString();

            if (!horaTurno.ContainsKey(slotKey))
            {
                MessageBox.Show("El horario seleccionado no es válido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!horaTurno[slotKey])
            {
                MessageBox.Show("El horario seleccionado ya está ocupado.", "Horario no disponible",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Motivo (checkedListBox)
            if (clbMotivo.CheckedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un motivo para la cita.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string motivo = string.Join(", ", clbMotivo.CheckedItems.Cast<string>());
            string diagnostico = rtbDiagnostico.Text.Trim();
            string notas = rtbNotas.Text.Trim();

            DateTime fecha = mcCalendarioAgendarCita.SelectionStart.Date;

            // Parsear hora de inicio desde la key
            string horaInicioStr = slotKey.Split('-')[0].Trim(); // "8:00:00 AM"
            DateTime horaInicioDt = DateTime.Parse(horaInicioStr, CultureInfo.InvariantCulture);
            TimeSpan horaCita = horaInicioDt.TimeOfDay;
            DateTime fechaCita = fecha.Date + horaCita;

            int idCliente = int.Parse(_usuario["idCliente"]);
            int idMascota = mascota.IdMascota;
            int idVeterinario = vet.IdVeterinario;

            // 1) Actualizar el diccionario marcando el slot como ocupado
            horaTurno[slotKey] = false;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // 2) Guardar/actualizar JSON en 'horarios'
                    string jsonHorario = JsonConvert.SerializeObject(horaTurno);

                    if (horarioActualId.HasValue)
                    {
                        string updateHorario = @"UPDATE horarios
                                                 SET horario = @horario
                                                 WHERE id_horario = @idHorario";
                        MySqlCommand cmdH = new MySqlCommand(updateHorario, con, tx);
                        cmdH.Parameters.AddWithValue("@horario", jsonHorario);
                        cmdH.Parameters.AddWithValue("@idHorario", horarioActualId.Value);
                        cmdH.ExecuteNonQuery();
                    }
                    else
                    {
                        string insertHorario = @"INSERT INTO horarios (fecha, id_veterinario, horario)
                                                 VALUES (@fecha, @idVet, @horario)";
                        MySqlCommand cmdH = new MySqlCommand(insertHorario, con, tx);
                        cmdH.Parameters.AddWithValue("@fecha", fecha);
                        cmdH.Parameters.AddWithValue("@idVet", idVeterinario);
                        cmdH.Parameters.AddWithValue("@horario", jsonHorario);
                        cmdH.ExecuteNonQuery();
                        horarioActualId = (int)cmdH.LastInsertedId;
                    }

                    // 3) Insertar la cita en la tabla 'citas'
                    string insertCita = @"
                        INSERT INTO citas
                        (fecha_cita, motivo, diagnostico, id_mascota, id_cliente, id_veterinario, estado, notas, hora_cita)
                        VALUES
                        (@fecha_cita, @motivo, @diagnostico, @id_mascota, @id_cliente, @id_veterinario, @estado, @notas, @hora_cita);";

                    MySqlCommand cmdC = new MySqlCommand(insertCita, con, tx);
                    cmdC.Parameters.AddWithValue("@fecha_cita", fechaCita);
                    cmdC.Parameters.AddWithValue("@motivo", motivo);
                    cmdC.Parameters.AddWithValue("@diagnostico", diagnostico);
                    cmdC.Parameters.AddWithValue("@id_mascota", idMascota);
                    cmdC.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmdC.Parameters.AddWithValue("@id_veterinario", idVeterinario);
                    cmdC.Parameters.AddWithValue("@estado", "proxima");
                    cmdC.Parameters.AddWithValue("@notas", notas);
                    cmdC.Parameters.AddWithValue("@hora_cita", horaCita);

                    cmdC.ExecuteNonQuery();
                    int idCita = (int)cmdC.LastInsertedId;

                    tx.Commit();

                    // 4) Refrescar el DGV con los nuevos colores
                    LlenarDgvDesdeHoraTurno();

                    // 5) Construir el diccionario para Form4
                    var resumen = ConstruirDiccionarioResumenCita(
                        fechaCita,
                        horaCita,
                        mascota,
                        vet,
                        motivo,
                        notas,
                        idCita
                    );

                    MessageBox.Show("Cita agendada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 6) Abrir Form4 con el diccionario
                    Form4 f4 = new Form4(resumen);
                    f4.ShowDialog();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error al agendar la cita:\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Construye el diccionario resumen para Form4
        private Dictionary<string, string> ConstruirDiccionarioResumenCita(
            DateTime fechaCita,
            TimeSpan horaCita,
            MascotaItem mascota,
            VeterinarioItem vet,
            string motivo,
            string notas,
            int idCita)
        {
            // Calcular edad de la mascota en años
            int edad = DateTime.Today.Year - mascota.FechaNacimiento.Year;
            if (mascota.FechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;

            string nombreDueno = $"{_usuario["nombre"]} {_usuario["apellido"]}";

            var dict = new Dictionary<string, string>
            {
                ["fecha"] = fechaCita.ToString("dd/MM/yyyy"),
                ["hora"] = new DateTime(horaCita.Ticks).ToString("HH:mm"),
                ["nombreMascota"] = mascota.Nombre,
                ["especieMascota"] = mascota.Especie,
                ["razaMazcota"] = mascota.Raza,
                ["edadMascota"] = $"{edad} años",
                ["sexoMascota"] = mascota.Sexo,
                ["nombreDueno"] = nombreDueno,
                ["correoDueno"] = _usuario["correo"],
                ["telefono"] = _usuario["telefono"],
                ["dirreccionDueno"] = _usuario["direccion"],
                ["nombreVeterinario"] = vet.NombreCompleto,
                ["MotivoCita"] = motivo,
                ["notas"] = notas,
                ["numeroCita"] = idCita.ToString()
            };

            return dict;
        }
    }
}
