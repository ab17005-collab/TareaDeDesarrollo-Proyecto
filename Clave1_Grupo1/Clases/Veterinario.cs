using System;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public class Veterinario : Usuario
    {
        public string Especialidad { get; set; }

        public Veterinario(string nombreUsuario, string apellidoUsuario, string contrasena, string rol, string especialidad)
            : base(nombreUsuario, apellidoUsuario, contrasena, rol)
        {
            Especialidad = especialidad;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            // Primero guardamos en la tabla 'usuarios'
            int idUsuario = base.GuardarEnBD(conexion);

            // Ahora insertamos en la tabla 'veterinarios'
            string query = @"INSERT INTO veterinarios 
                             (idUsuario, nombre, apellido, especialidad, fechaRegistro)
                             VALUES (@idUsuario, @nombre, @apellido, @especialidad, @fechaRegistro)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@nombre", NombreUsuario);
            cmd.Parameters.AddWithValue("@apellido", ApellidoUsuario);
            cmd.Parameters.AddWithValue("@especialidad", Especialidad);
            cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
    }
}

