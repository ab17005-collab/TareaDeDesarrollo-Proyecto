using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Cita
    {
        public int IdCita { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Motivo { get; set; }
        public string Diagnostico { get; set; }
        public string Notas { get; set; }
        public int IdMascota { get; set; }
        public int IdCliente { get; set; }
        public int IdVeterinario { get; set; }
        public string Estado { get; set; } // "proxima", "cancelada", etc.

        public Cita() { }

        public Cita(DateTime fechaCita, TimeSpan horaCita, string motivo,
                    string diagnostico, string notas,
                    int idMascota, int idCliente, int idVeterinario, string estado)
        {
            FechaCita = fechaCita;
            HoraCita = horaCita;
            Motivo = motivo;
            Diagnostico = diagnostico;
            Notas = notas;
            IdMascota = idMascota;
            IdCliente = idCliente;
            IdVeterinario = idVeterinario;
            Estado = estado;
        }

        public int GuardarEnBD(MySqlConnection conexion)
        {
            string query = @"
                INSERT INTO citas
                (fecha_cita, motivo, diagnostico, id_mascota, id_cliente, id_veterinario, estado, notas, hora_cita)
                VALUES
                (@fecha_cita, @motivo, @diagnostico, @id_mascota, @id_cliente, @id_veterinario, @estado, @notas, @hora_cita);";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@fecha_cita", FechaCita);
            cmd.Parameters.AddWithValue("@motivo", Motivo);
            cmd.Parameters.AddWithValue("@diagnostico", Diagnostico);
            cmd.Parameters.AddWithValue("@id_mascota", IdMascota);
            cmd.Parameters.AddWithValue("@id_cliente", IdCliente);
            cmd.Parameters.AddWithValue("@id_veterinario", IdVeterinario);
            cmd.Parameters.AddWithValue("@estado", Estado);
            cmd.Parameters.AddWithValue("@notas", Notas);
            cmd.Parameters.AddWithValue("@hora_cita", HoraCita);

            cmd.ExecuteNonQuery();
            IdCita = (int)cmd.LastInsertedId;
            return IdCita;
        }
    }
}
