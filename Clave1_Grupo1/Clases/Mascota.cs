using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public int IdCliente { get; set; }  // FK a Cliente

        public Mascota() { }

        public Mascota(string nombre, string especie, string raza, int edad, double peso, int idCliente)
        {
            Nombre = nombre;
            Especie = especie;
            Raza = raza;
            Edad = edad;
            Peso = peso;
            IdCliente = idCliente;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO mascotas (nombre, especie, raza, edad, peso, idCliente) " +
                           "VALUES (@nombre, @especie, @raza, @edad, @peso, @idCliente)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nombre", Nombre);
            cmd.Parameters.AddWithValue("@especie", Especie);
            cmd.Parameters.AddWithValue("@raza", Raza);
            cmd.Parameters.AddWithValue("@edad", Edad);
            cmd.Parameters.AddWithValue("@peso", Peso);
            cmd.Parameters.AddWithValue("@idCliente", IdCliente);
            cmd.ExecuteNonQuery();
        }
    }
}
