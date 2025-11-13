using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Veterinario : Usuario
    {
        public string Especialidad { get; set; }

        public Veterinario(string nombre, string apellido, string correo, string contrasena, string telefono, string direccion, string rol, string especialidad)
            : base(nombre, apellido, correo, contrasena, telefono, direccion, rol)
        {
            Especialidad = especialidad;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            int idUsuario = base.GuardarEnBD(conexion);

            string query = "INSERT INTO veterinarios (id_usuario) VALUES (@idUsuario)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.ExecuteNonQuery();
        }
    }
}
