using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Vacuna
    {
        public int IdVacuna { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public int IdMascota { get; set; }

        public Vacuna() { }

        public Vacuna(string nombre, DateTime fechaAplicacion, int idMascota)
        {
            Nombre = nombre;
            FechaAplicacion = fechaAplicacion;
            IdMascota = idMascota;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO vacunas (nombre, fecha_aplicacion, idMascota) VALUES (@nombre, @fecha, @idMascota)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nombre", Nombre);
            cmd.Parameters.AddWithValue("@fecha", FechaAplicacion);
            cmd.Parameters.AddWithValue("@idMascota", IdMascota);
            cmd.ExecuteNonQuery();
        }
    }
}