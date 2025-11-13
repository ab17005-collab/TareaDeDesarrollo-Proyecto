using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Usuario() { }

        public Usuario(string nombreUsuario, string apellidoUsuario, string contrasena, string rol)
        {
            NombreUsuario = nombreUsuario;
            ApellidoUsuario = apellidoUsuario;
            Contrasena = contrasena;
            Rol = rol;
            FechaRegistro = DateTime.Now;
        }

        public int GuardarEnBD(MySqlConnection conexion)
        {
            string query = @"INSERT INTO usuarios (nombre_usuario, contrasena, rol, fecha_registro)
                             VALUES (@nombreUsuario, @contrasena, @rol, @fechaRegistro)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nombreUsuario", $"{NombreUsuario} {ApellidoUsuario}");
            cmd.Parameters.AddWithValue("@contrasena", Contrasena);
            cmd.Parameters.AddWithValue("@rol", Rol);
            cmd.Parameters.AddWithValue("@fechaRegistro", FechaRegistro);
            cmd.ExecuteNonQuery();

            return (int)cmd.LastInsertedId; // Devuelve el ID generado (id_usuario)
        }
    }
}
