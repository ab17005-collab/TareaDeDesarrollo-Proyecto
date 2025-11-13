using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string apellido, string correo, string contrasena, string telefono, string direccion, string rol)
            : base(nombre, apellido, correo, contrasena, telefono, direccion, rol)
        {
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            int idUsuario = base.GuardarEnBD(conexion);

            string query = "INSERT INTO administradores (id_usuario) VALUES (@idUsuario)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.ExecuteNonQuery();
        }
    }
}
