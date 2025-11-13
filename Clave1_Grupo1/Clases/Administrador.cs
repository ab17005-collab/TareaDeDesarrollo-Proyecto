using System;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public class Administrador : Usuario
    {
        public Administrador(string nombreUsuario, string apellidoUsuario, string contrasena, string rol)
            : base(nombreUsuario, apellidoUsuario, contrasena, rol)
        {
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            // Primero guardamos en la tabla 'usuarios'
            int idUsuario = base.GuardarEnBD(conexion);

            // Luego insertamos en la tabla 'administradores'
            string query = @"INSERT INTO administradores 
                             (idUsuario, nombre, apellido, fechaRegistro)
                             VALUES (@idUsuario, @nombre, @apellido, @fechaRegistro)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@nombre", NombreUsuario);
            cmd.Parameters.AddWithValue("@apellido", ApellidoUsuario);
            cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
    }
}
