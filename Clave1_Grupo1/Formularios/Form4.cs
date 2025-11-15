using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Clave1_Grupo1
{
    public partial class Form4 : Form
    {
        static string cadenaConexion =
            $"Server=localhost; Database=veterinariapatitasypelos; Uid=root; Pwd=root;";

        private int _idCita;
        private bool _permitirEdicion;
        private Dictionary<string, string> _usuario;

        private string _estadoActual;

        public Form4(int idCita, bool permitirEdicion, Dictionary<string, string> usuario)
        {
            InitializeComponent();
            _idCita = idCita;
            _permitirEdicion = permitirEdicion;
            _usuario = usuario;
        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            CargarDatosCita();
        }

        // ================= Cargar datos de la cita desde BD =================

        private void CargarDatosCita()
        {
            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"
                    SELECT c.id_cita, c.fecha_cita, c.hora_cita, c.motivo, c.diagnostico,
                           c.estado, c.notas,
                           m.nombre AS nombreMascota, m.especie, m.raza, m.sexo, m.fecha_nacimiento,
                           dueno.nombre AS nombreDueno, dueno.apellido AS apellidoDueno,
                           dueno.correo_electronico, dueno.numero_telefonico, dueno.direccion,
                           vetU.nombre AS nombreVet, vetU.apellido AS apellidoVet
                    FROM citas c
                    INNER JOIN mascotas m        ON c.id_mascota    = m.id_mascota
                    INNER JOIN clientes cli      ON c.id_cliente    = cli.id_cliente
                    INNER JOIN usuarios dueno    ON cli.id_usuario  = dueno.id_usuario
                    INNER JOIN veterinarios v    ON c.id_veterinario = v.id_veterinario
                    INNER JOIN usuarios vetU     ON v.id_usuario    = vetU.id_usuario
                    WHERE c.id_cita = @idCita;";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCita", _idCita);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("No se encontró la cita seleccionada.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    DateTime fechaCita = reader.GetDateTime("fecha_cita");
                    TimeSpan horaCita = reader.GetTimeSpan("hora_cita");
                    string motivo = reader.GetString("motivo");
                    string diagnostico = reader.IsDBNull(reader.GetOrdinal("diagnostico"))
                                         ? ""
                                         : reader.GetString("diagnostico");
                    string notas = reader.IsDBNull(reader.GetOrdinal("notas"))
                                   ? ""
                                   : reader.GetString("notas");
                    _estadoActual = reader.GetString("estado");

                    string nombreMascota = reader.GetString("nombreMascota");
                    string especie = reader.GetString("especie");
                    string raza = reader.GetString("raza");
                    string sexo = reader.GetString("sexo");
                    DateTime fechaNac = reader.GetDateTime("fecha_nacimiento");

                    string nombreDueno = reader.GetString("nombreDueno") + " " + reader.GetString("apellidoDueno");
                    string correo = reader.GetString("correo_electronico");
                    string telefono = reader.GetString("numero_telefonico");
                    string direccion = reader.GetString("direccion");

                    string nombreVeterinario = reader.GetString("nombreVet") + " " + reader.GetString("apellidoVet");

                    int edad = DateTime.Today.Year - fechaNac.Year;
                    if (fechaNac.Date > DateTime.Today.AddYears(-edad)) edad--;

                    // Asignar a labels (asegúrate de que existan estos controles)
                    lblCitaId.Text = "Cita #: " + _idCita;
                    lblNombreMascota.Text = nombreMascota;
                    lblEspecie.Text = especie;
                    lblRaza.Text = raza;
                    lblEdad.Text = $"{edad} años";
                    lblSexo.Text = sexo;
                    lblNombreDueno.Text = nombreDueno;
                    lblTelefono.Text = "📞 " + telefono;
                    lblCorreo.Text = "📧 " + correo;
                    lblDireccion.Text = "📍 " + direccion;
                    lblFechaCita.Text = "📆 " + fechaCita.ToString("dd/MM/yyyy");
                    lblHoraCita.Text = "🕑 " + new DateTime(horaCita.Ticks).ToString("HH:mm");
                    lblMotivo.Text = motivo;
                    lblNotas.Text = notas;
                    lblDiagnostico.Text = diagnostico;
                    lblVeterinario.Text = "Dr. " + nombreVeterinario;
                    lblEstado.Text = "Estado: " + _estadoActual;
                }
            }

            // Habilitar / deshabilitar botones según estado y origen
            bool editable = _permitirEdicion && _estadoActual == "proxima";
            btnCanelarCita.Enabled = editable;
            btnModificarReagendar.Enabled = editable;
        }

        // ================= CANCELAR CITA ======================

        private void btnCanelarCita_Click(object sender, EventArgs e)
        {
            if (!btnCanelarCita.Enabled) return;

            var r = MessageBox.Show("¿Está seguro que desea cancelar esta cita?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            {
                con.Open();
                MySqlTransaction tx = con.BeginTransaction();

                try
                {
                    // Traer info de la cita para liberar el horario
                    string selectCita = @"SELECT fecha_cita, hora_cita, id_veterinario
                                          FROM citas
                                          WHERE id_cita = @idCita;";
                    MySqlCommand cmdSel = new MySqlCommand(selectCita, con, tx);
                    cmdSel.Parameters.AddWithValue("@idCita", _idCita);

                    DateTime fechaCita;
                    TimeSpan horaCita;
                    int idVet;

                    using (var reader = cmdSel.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new Exception("La cita ya no existe.");

                        fechaCita = reader.GetDateTime("fecha_cita").Date;
                        horaCita = reader.GetTimeSpan("hora_cita");
                        idVet = reader.GetInt32("id_veterinario");
                    }

                    // Actualizar estado de la cita
                    string updateCita = @"UPDATE citas
                                          SET estado = 'cancelada'
                                          WHERE id_cita = @idCita;";
                    MySqlCommand cmdUp = new MySqlCommand(updateCita, con, tx);
                    cmdUp.Parameters.AddWithValue("@idCita", _idCita);
                    cmdUp.ExecuteNonQuery();

                    // Liberar el slot en horarios
                    string selHorario = @"SELECT id_horario, horario
                                          FROM horarios
                                          WHERE id_veterinario = @idVet AND fecha = @fecha;";
                    MySqlCommand cmdHorSel = new MySqlCommand(selHorario, con, tx);
                    cmdHorSel.Parameters.AddWithValue("@idVet", idVet);
                    cmdHorSel.Parameters.AddWithValue("@fecha", fechaCita);

                    int? idHorario = null;
                    Dictionary<string, bool> dic = null;

                    using (var reader = cmdHorSel.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idHorario = reader.GetInt32("id_horario");
                            string json = reader.IsDBNull(reader.GetOrdinal("horario"))
                                          ? null
                                          : reader.GetString("horario");
                            if (!string.IsNullOrWhiteSpace(json))
                                dic = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
                        }
                    }

                    if (idHorario.HasValue && dic != null)
                    {
                        DateTime inicio = DateTime.Today.Add(horaCita);
                        DateTime fin = inicio.AddMinutes(30);
                        string slotKey = $"{inicio:h:mm:ss tt} - {fin:h:mm:ss tt}";

                        if (dic.ContainsKey(slotKey))
                            dic[slotKey] = true;

                        string newJson = JsonConvert.SerializeObject(dic);
                        string updHorario = @"UPDATE horarios
                                              SET horario = @horario
                                              WHERE id_horario = @id;";
                        MySqlCommand cmdHorUpd = new MySqlCommand(updHorario, con, tx);
                        cmdHorUpd.Parameters.AddWithValue("@horario", newJson);
                        cmdHorUpd.Parameters.AddWithValue("@id", idHorario.Value);
                        cmdHorUpd.ExecuteNonQuery();
                    }

                    tx.Commit();

                    MessageBox.Show("La cita ha sido cancelada.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _estadoActual = "cancelada";
                    btnCanelarCita.Enabled = false;
                    btnModificarReagendar.Enabled = false;
                    lblEstado.Text = "Estado: cancelada";
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error al cancelar la cita:\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= MODIFICAR / REAGENDAR ======================

        private void btnModificarReagendar_Click(object sender, EventArgs e)
        {
            if (!btnModificarReagendar.Enabled) return;

            // Abrir Form3 en modo edición
            using (Form3 frm = new Form3(_usuario, _idCita))
            {
                frm.ShowDialog();
            }

            // Al cerrar Form3, recargar los datos (pueden haber cambiado)
            CargarDatosCita();
        }

        // ================= BOTONES DE CIERRE ======================

        private void btnListo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
