using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime FechaConsulta { get; set; }

        public Consulta() { }

        public Consulta(int idMascota, int idVeterinario, string diagnostico, string tratamiento, DateTime fechaConsulta)
        {
            IdMascota = idMascota;
            IdVeterinario = idVeterinario;
            Diagnostico = diagnostico;
            Tratamiento = tratamiento;
            FechaConsulta = fechaConsulta;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO consultas (idMascota, idVeterinario, diagnostico, tratamiento, fecha_consulta) " +
                           "VALUES (@idMascota, @idVeterinario, @diagnostico, @tratamiento, @fecha)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idMascota", IdMascota);
            cmd.Parameters.AddWithValue("@idVeterinario", IdVeterinario);
            cmd.Parameters.AddWithValue("@diagnostico", Diagnostico);
            cmd.Parameters.AddWithValue("@tratamiento", Tratamiento);
            cmd.Parameters.AddWithValue("@fecha", FechaConsulta);
            cmd.ExecuteNonQuery();
        }
    }
}
