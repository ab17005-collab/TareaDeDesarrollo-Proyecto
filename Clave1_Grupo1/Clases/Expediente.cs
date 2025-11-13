using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Expediente
    {
        public int IdExpediente { get; set; }
        public int IdMascota { get; set; }          // FK hacia Mascota
        public DateTime FechaCreacion { get; set; }
        public string ObservacionesGenerales { get; set; }
        public string Alergias { get; set; }
        public string EnfermedadesPrevias { get; set; }

        public Expediente() { }

        public Expediente(int idMascota, DateTime fechaCreacion, string observacionesGenerales, string alergias, string enfermedadesPrevias)
        {
            IdMascota = idMascota;
            FechaCreacion = fechaCreacion;
            ObservacionesGenerales = observacionesGenerales;
            Alergias = alergias;
            EnfermedadesPrevias = enfermedadesPrevias;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO expedientes (idMascota, fecha_creacion, observaciones_generales, alergias, enfermedades_previas) " +
                           "VALUES (@idMascota, @fecha, @obs, @alergias, @enfermedades)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idMascota", IdMascota);
            cmd.Parameters.AddWithValue("@fecha", FechaCreacion);
            cmd.Parameters.AddWithValue("@obs", ObservacionesGenerales);
            cmd.Parameters.AddWithValue("@alergias", Alergias);
            cmd.Parameters.AddWithValue("@enfermedades", EnfermedadesPrevias);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarEnBD(MySqlConnection conexion)
        {
            string query = "UPDATE expedientes SET observaciones_generales = @obs, alergias = @alergias, enfermedades_previas = @enfermedades " +
                           "WHERE idExpediente = @idExpediente";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@obs", ObservacionesGenerales);
            cmd.Parameters.AddWithValue("@alergias", Alergias);
            cmd.Parameters.AddWithValue("@enfermedades", EnfermedadesPrevias);
            cmd.Parameters.AddWithValue("@idExpediente", IdExpediente);
            cmd.ExecuteNonQuery();
        }

        public static Expediente BuscarPorId(MySqlConnection conexion, int idExpediente)
        {
            string query = "SELECT * FROM expedientes WHERE idExpediente = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@id", idExpediente);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Expediente exp = new Expediente
                    {
                        IdExpediente = reader.GetInt32("idExpediente"),
                        IdMascota = reader.GetInt32("idMascota"),
                        FechaCreacion = reader.GetDateTime("fecha_creacion"),
                        ObservacionesGenerales = reader.GetString("observaciones_generales"),
                        Alergias = reader.GetString("alergias"),
                        EnfermedadesPrevias = reader.GetString("enfermedades_previas")
                    };
                    return exp;
                }
            }
            return null;
        }
    }
}
