using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clave1_Grupo1
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime Fecha { get; set; }   // Solo la fecha
        public Dictionary<string, bool> Slots { get; set; } = new Dictionary<string, bool>();

        public Horario() { }

        public Horario(int idVet, DateTime fecha, Dictionary<string, bool> slots)
        {
            IdVeterinario = idVet;
            Fecha = fecha.Date;
            Slots = slots;
        }

        public void Insertar(MySqlConnection conexion)
        {
            // Replace this line:
            // string json = JsonSerializer.Serialize(Slots);
            // With this line:
            string json = JsonConvert.SerializeObject(Slots);

            string query = @"INSERT INTO horarios (fecha, id_veterinario, horario)
                             VALUES (@fecha, @idVet, @horario)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@fecha", Fecha);
            cmd.Parameters.AddWithValue("@idVet", IdVeterinario);
            cmd.Parameters.AddWithValue("@horario", json);
            cmd.ExecuteNonQuery();

            IdHorario = (int)cmd.LastInsertedId;
        }

        public void Actualizar(MySqlConnection conexion)
        {
            // Fix: Use JsonConvert.SerializeObject instead of JsonSerializer.Serialize
            string json = JsonConvert.SerializeObject(Slots);

            string query = @"UPDATE horarios
                             SET horario = @horario
                             WHERE id_horario = @idHorario";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@horario", json);
            cmd.Parameters.AddWithValue("@idHorario", IdHorario);
            cmd.ExecuteNonQuery();
        }

        public static Horario Cargar(MySqlConnection conexion, int idVet, DateTime fecha)
        {
            string query = @"SELECT id_horario, horario
                             FROM horarios
                             WHERE id_veterinario = @idVet AND fecha = @fecha";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idVet", idVet);
            cmd.Parameters.AddWithValue("@fecha", fecha.Date);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    int idHorario = reader.GetInt32("id_horario");
                    string json = reader.IsDBNull(reader.GetOrdinal("horario"))
                                  ? "{}"
                                  : reader.GetString("horario");

                    // Replace this line:
                    // var slots = JsonSerializer.Deserialize<Dictionary<string, bool>>(json)
                    // with the following using Newtonsoft.Json.JsonConvert:

                    var slots = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json)
                                ?? new Dictionary<string, bool>();

                    return new Horario
                    {
                        IdHorario = idHorario,
                        IdVeterinario = idVet,
                        Fecha = fecha.Date,
                        Slots = slots
                    };
                }
            }

            return null; // No existe registro
        }
    }
}
