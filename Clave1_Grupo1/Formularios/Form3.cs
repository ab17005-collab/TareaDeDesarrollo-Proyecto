using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Clave1_Grupo1
{
    public partial class Form3 : Form
    {
        // Cadena de conexión a MySQL
        static string cadenaConexion =
            $"Server=localhost; Database=veterinariapatitasypelos; Uid=root; Pwd=root;";

        // Datos del usuario (cliente) actual
        Dictionary<string, string> _usuario = new Dictionary<string, string>();

        // Diccionario de horarios (key = "8:00:00 AM - 8:30:00 AM", value = disponible?)
        Dictionary<string, bool> horaTurno = new Dictionary<string, bool>();

        // Id del registro de horarios actualmente cargado (si existe)
        int? horarioActualId = null;

        // Si no es null, el formulario está en modo edición de esta cita
        int? _idCitaEdicion = null;

        // Slot original de la cita al editar (para liberar si cambia)
        string _slotOriginal = null;

        // ====== Clases internas para manejar ítems de combo ======

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

        // ====== Constructores ======

        // Modo normal (crear cita)
        public Form3(Dictionary<string, string> usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        // Modo edición de cita
        public Form3(Dictionary<string, string> usuario, int idCitaEdicion)
        {
            InitializeComponent();
            _usuario = usuario;
            _idCitaEdicion = idCitaEdicion;
        }

        // ====== Load del formulario ======

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = $"Cuenta: {_usuario["idCliente"]}";

            txtPropietario.Text = $"{_usuario["nombre"]} {_usuario["apellido"]}";
            txtEmail.Text = _usuario["correo"];
            txtTelefono.Text = _usuario["telefono"];

            CargarMascotas();
            CargarVeterinarios();

            ConfigurarGridSlots();

            mcCalendarioAgendarCita.MaxSelectionCount = 1;

            // Generar horario por defecto (sin veterinario aún)
            GenerarYLlenarSlots();

            // Eventos (por si no están en el diseñador)
            cbxVeterinario.SelectedIndexChanged += cbxVeterinario_SelectedIndexChanged;
            mcCalendarioAgendarCita.DateChanged += mcCalendarioAgendarCita_DateChanged;

            // Eventos de los grids de citas
            dgvProximasCitas.CellDoubleClick += dgvProximaCitas_CellDoubleClick;
            dgvCitasPasadas.CellDoubleClick += dgvCitasPasadas_CellDoubleClick;
            dgvCitasCanceladas.CellDoubleClick += dgvCitasCanceladas_CellDoubleClick;

            // Cargar listas de citas del cliente
            CargarCitasCliente();

            // Si estamos editando una cita, cargar datos en controles
            if (_idCitaEdicion.HasValue)
            {
                CargarDatosCitaEnFormulario(_idCitaEdicion.Value);
                btnAgendar.Text = "Confirmar cambios";
            }
            else
            {
                btnAgendar.Text = "Agendar cita";
            }
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

        // ===================== HORARIOS / DGV SLOTS =======================

        private void ConfigurarGridSlots()
        {
            dgvSlotsDisponibles.Rows.Clear();
            dgvSlotsDisponibles.Columns.Clear();
            dgvSlotsDisponibles.Columns.Add("HorarioMatutino", "Horario Matutino");
            dgvSlotsDisponibles.Columns.Add("HorarioVespertino", "Horario Vespertino");
            dgvSlotsDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

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
                            horarioActualId = null;
                            GenerarYLlenarSlots();
                            return;
                        }
                    }

                    LlenarDgvDesdeHoraTurno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el horario:\n\n" + ex.Message);
                }
            }
        }

        // ===================== LISTAS DE CITAS DEL CLIENTE =======================

        private void CargarCitasCliente()
        {
            int idCliente = int.Parse(_usuario["idCliente"]);

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();
                LlenarGridCitas(dgvProximasCitas, con, idCliente, "proxima");
                LlenarGridCitas(dgvCitasPasadas, con, idCliente, "pasada");
                LlenarGridCitas(dgvCitasCanceladas, con, idCliente, "cancelada");
            }
        }

        private void LlenarGridCitas(DataGridView dgv, MySqlConnection con, int idCliente, string estado)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("IdCita", "IdCita");
            dgv.Columns["IdCita"].Visible = false;
            dgv.Columns.Add("Mascota", "Mascota");
            dgv.Columns.Add("Fecha", "Fecha");
            dgv.Columns.Add("Hora", "Hora");

            string query = @"
                SELECT c.id_cita, m.nombre AS mascota, c.fecha_cita, c.hora_cita
                FROM citas c
                INNER JOIN mascotas m ON c.id_mascota = m.id_mascota
                WHERE c.id_cliente = @idCliente AND c.estado = @estado
                ORDER BY c.fecha_cita, c.hora_cita;";

            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@estado", estado);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idCita = reader.GetInt32("id_cita");
                        DateTime fecha = reader.GetDateTime("fecha_cita");
                        TimeSpan hora = reader.GetTimeSpan("hora_cita");
                        string nombreMascota = reader.GetString("mascota");

                        dgv.Rows.Add(
                            idCita,
                            nombreMascota,
                            fecha.ToString("dd/MM/yyyy"),
                            new DateTime(hora.Ticks).ToString("HH:mm")
                        );
                    }
                }
            }
        }

        private void dgvProximaCitas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            AbrirDetalleCitaDesdeGrid(dgvProximasCitas, e.RowIndex, true);
        }

        private void dgvCitasPasadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            AbrirDetalleCitaDesdeGrid(dgvCitasPasadas, e.RowIndex, false);
        }

        private void dgvCitasCanceladas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            AbrirDetalleCitaDesdeGrid(dgvCitasCanceladas, e.RowIndex, false);
        }

        private void AbrirDetalleCitaDesdeGrid(DataGridView dgv, int rowIndex, bool permitirEdicion)
        {
            if (rowIndex < 0 || rowIndex >= dgv.Rows.Count) return;
            var row = dgv.Rows[rowIndex];
            if (row.Cells["IdCita"].Value == null) return;

            int idCita = Convert.ToInt32(row.Cells["IdCita"].Value);

            using (Form4 f4 = new Form4(idCita, permitirEdicion, _usuario))
            {
                f4.ShowDialog();
            }

            // Al cerrar Form4, recargar listas y dejar tab de citas activo
            CargarCitasCliente();
            try
            {
                tabCitas.SelectedTab = tabPage2;
            }
            catch { }
        }

        // ===================== CARGAR UNA CITA EN MODO EDICIÓN =======================

        private void CargarDatosCitaEnFormulario(int idCita)
        {
            int idCliente = int.Parse(_usuario["idCliente"]);

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"
                    SELECT c.fecha_cita, c.hora_cita, c.motivo, c.diagnostico, c.notas, c.estado,
                           c.id_mascota, c.id_veterinario,
                           m.nombre AS nombreMascota, m.especie, m.raza, m.sexo, m.fecha_nacimiento
                    FROM citas c
                    INNER JOIN mascotas m ON c.id_mascota = m.id_mascota
                    WHERE c.id_cita = @idCita AND c.id_cliente = @idCliente;";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCita", idCita);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("No se encontró la cita para este cliente.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateTime fechaCita = reader.GetDateTime("fecha_cita");
                    TimeSpan horaCita = reader.GetTimeSpan("hora_cita");
                    string motivo = reader.GetString("motivo");
                    string diagnostico = reader.IsDBNull(reader.GetOrdinal("diagnostico")) ? "" : reader.GetString("diagnostico");
                    string notas = reader.IsDBNull(reader.GetOrdinal("notas")) ? "" : reader.GetString("notas");
                    int idMascota = reader.GetInt32("id_mascota");
                    int idVeterinario = reader.GetInt32("id_veterinario");

                    // Configurar fecha
                    mcCalendarioAgendarCita.SetDate(fechaCita.Date);

                    // Seleccionar mascota
                    for (int i = 0; i < cbxMascotas.Items.Count; i++)
                    {
                        if (cbxMascotas.Items[i] is MascotaItem mItem && mItem.IdMascota == idMascota)
                        {
                            cbxMascotas.SelectedIndex = i;
                            break;
                        }
                    }

                    // Seleccionar veterinario
                    for (int i = 0; i < cbxVeterinario.Items.Count; i++)
                    {
                        if (cbxVeterinario.Items[i] is VeterinarioItem vItem && vItem.IdVeterinario == idVeterinario)
                        {
                            cbxVeterinario.SelectedIndex = i;
                            break;
                        }
                    }

                    // Texto
                    rtbDiagnostico.Text = diagnostico;
                    rtbNotas.Text = notas;

                    // Motivos: se asume que se guardaron como "texto1, texto2, ..."
                    var motivos = motivo.Split(',')
                                        .Select(x => x.Trim())
                                        .Where(x => !string.IsNullOrEmpty(x))
                                        .ToList();

                    for (int i = 0; i < clbMotivo.Items.Count; i++)
                    {
                        string itemText = clbMotivo.Items[i].ToString();
                        clbMotivo.SetItemChecked(i, motivos.Contains(itemText));
                    }

                    // Cargar horario desde BD para (vet, fecha)
                    CargarHorarioParaSeleccion();

                    // Guardar slot original y marcarlo como disponible para poder cambiarlo
                    _slotOriginal = ConstruirSlotDesdeHora(horaCita);

                    if (horaTurno.ContainsKey(_slotOriginal) == false)
                    {
                        horaTurno[_slotOriginal] = true;
                    }
                    else
                    {
                        horaTurno[_slotOriginal] = true;
                    }

                    LlenarDgvDesdeHoraTurno();

                    // Seleccionar en el grid el slot original
                    foreach (DataGridViewRow row in dgvSlotsDisponibles.Rows)
                    {
                        for (int c = 0; c < row.Cells.Count; c++)
                        {
                            if (row.Cells[c].Value != null &&
                                row.Cells[c].Value.ToString() == _slotOriginal)
                            {
                                dgvSlotsDisponibles.CurrentCell = row.Cells[c];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private string ConstruirSlotDesdeHora(TimeSpan hora)
        {
            DateTime inicio = DateTime.Today.Add(hora);
            DateTime fin = inicio.AddMinutes(30);
            return $"{inicio:h:mm:ss tt} - {fin:h:mm:ss tt}";
        }

        // ===================== AGENDAR / EDITAR CITA =======================

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

            if (!_idCitaEdicion.HasValue && !horaTurno[slotKey])
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

            if (_idCitaEdicion.HasValue)
            {
                ActualizarCitaExistente(slotKey, fecha, fechaCita, horaCita,
                                        idCliente, idMascota, idVeterinario,
                                        motivo, diagnostico, notas);
            }
            else
            {
                CrearNuevaCita(slotKey, fecha, fechaCita, horaCita,
                               idCliente, idMascota, idVeterinario,
                               motivo, diagnostico, notas, mascota, vet);
            }
        }

        // Modo NUEVA CITA
        private void CrearNuevaCita(
            string slotKey,
            DateTime fecha,
            DateTime fechaCita,
            TimeSpan horaCita,
            int idCliente,
            int idMascota,
            int idVeterinario,
            string motivo,
            string diagnostico,
            string notas,
            MascotaItem mascota,
            VeterinarioItem vet)
        {
            // Marcar el slot como ocupado
            horaTurno[slotKey] = false;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // Guardar/actualizar JSON en 'horarios'
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

                    // Insertar la cita en la tabla 'citas'
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

                    LlenarDgvDesdeHoraTurno();
                    CargarCitasCliente();

                    MessageBox.Show("Cita agendada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mostrar Form4 con detalle
                    using (Form4 f4 = new Form4(idCita, true, _usuario))
                    {
                        f4.ShowDialog();
                    }

                    // Volver al tab de citas
                    try
                    {
                        tabCitas.SelectedTab = tabPage2;
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error al agendar la cita:\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Modo EDITAR CITA
        private void ActualizarCitaExistente(
            string slotKey,
            DateTime fecha,
            DateTime fechaCita,
            TimeSpan horaCita,
            int idCliente,
            int idMascota,
            int idVeterinario,
            string motivo,
            string diagnostico,
            string notas)
        {
            if (!_idCitaEdicion.HasValue)
                return;

            // Liberar el slot original y ocupar el nuevo
            if (!string.IsNullOrEmpty(_slotOriginal) && horaTurno.ContainsKey(_slotOriginal))
                horaTurno[_slotOriginal] = true;

            horaTurno[slotKey] = false;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
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

                    string updateCita = @"
                        UPDATE citas
                        SET fecha_cita = @fecha_cita,
                            motivo = @motivo,
                            diagnostico = @diagnostico,
                            id_mascota = @id_mascota,
                            id_cliente = @id_cliente,
                            id_veterinario = @id_veterinario,
                            notas = @notas,
                            hora_cita = @hora_cita
                        WHERE id_cita = @id_cita;";

                    MySqlCommand cmdC = new MySqlCommand(updateCita, con, tx);
                    cmdC.Parameters.AddWithValue("@fecha_cita", fechaCita);
                    cmdC.Parameters.AddWithValue("@motivo", motivo);
                    cmdC.Parameters.AddWithValue("@diagnostico", diagnostico);
                    cmdC.Parameters.AddWithValue("@id_mascota", idMascota);
                    cmdC.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmdC.Parameters.AddWithValue("@id_veterinario", idVeterinario);
                    cmdC.Parameters.AddWithValue("@notas", notas);
                    cmdC.Parameters.AddWithValue("@hora_cita", horaCita);
                    cmdC.Parameters.AddWithValue("@id_cita", _idCitaEdicion.Value);

                    cmdC.ExecuteNonQuery();

                    tx.Commit();

                    LlenarDgvDesdeHoraTurno();
                    CargarCitasCliente();

                    MessageBox.Show("Cita modificada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close(); // vuelve a Form4
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error al modificar la cita:\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
