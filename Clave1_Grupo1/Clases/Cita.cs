using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }
        public int IdHorario { get; set; }
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; } // Programada, Reprogramada, Cancelada
        public string Motivo { get; set; }

        public Cita() { }

        public Cita(int idMascota, int idVeterinario, int idHorario, DateTime fechaCita, string estado, string motivo)
        {
            IdMascota = idMascota;
            IdVeterinario = idVeterinario;
            IdHorario = idHorario;
            FechaCita = fechaCita;
            Estado = estado;
            Motivo = motivo;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO citas (idMascota, idVeterinario, idHorario, fecha_cita, estado, motivo) " +
                           "VALUES (@idMascota, @idVeterinario, @idHorario, @fecha, @estado, @motivo)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idMascota", IdMascota);
            cmd.Parameters.AddWithValue("@idVeterinario", IdVeterinario);
            cmd.Parameters.AddWithValue("@idHorario", IdHorario);
            cmd.Parameters.AddWithValue("@fecha", FechaCita);
            cmd.Parameters.AddWithValue("@estado", Estado);
            cmd.Parameters.AddWithValue("@motivo", Motivo);
            cmd.ExecuteNonQuery();
        }
    }
}
