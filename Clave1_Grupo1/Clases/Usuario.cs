using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string NumeroTelefonico { get; set; }
        public string Direccion { get; set; }
        public string Rol { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string apellido, string correo, string contrasena, string telefono, string direccion, string rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correo;
            Contrasena = contrasena;
            NumeroTelefonico = telefono;
            Direccion = direccion;
            Rol = rol;
        }

        public int GuardarEnBD(MySqlConnection conexion)
        {
            string query = @"INSERT INTO usuarios 
                            (nombre, apellido, correo_electronico, contrasena, numero_telefonico, direccion, rol)
                             VALUES (@nombre, @apellido, @correo, @contrasena, @telefono, @direccion, @rol)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nombre", Nombre);
            cmd.Parameters.AddWithValue("@apellido", Apellido);
            cmd.Parameters.AddWithValue("@correo", CorreoElectronico);
            cmd.Parameters.AddWithValue("@contrasena", Contrasena);
            cmd.Parameters.AddWithValue("@telefono", NumeroTelefonico);
            cmd.Parameters.AddWithValue("@direccion", Direccion);
            cmd.Parameters.AddWithValue("@rol", Rol);
            cmd.ExecuteNonQuery();

            return (int)cmd.LastInsertedId; // Devuelve el id_usuario recién insertado
        }
    }
}
