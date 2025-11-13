using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Disponible { get; set; }

        public Horario() { }

        public Horario(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, bool disponible)
        {
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Disponible = disponible;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO horarios (fecha, hora_inicio, hora_fin, disponible) " +
                           "VALUES (@fecha, @inicio, @fin, @disponible)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@fecha", Fecha);
            cmd.Parameters.AddWithValue("@inicio", HoraInicio);
            cmd.Parameters.AddWithValue("@fin", HoraFin);
            cmd.Parameters.AddWithValue("@disponible", Disponible);
            cmd.ExecuteNonQuery();
        }
    }
}
