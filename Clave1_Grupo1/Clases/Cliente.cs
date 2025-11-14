using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Cliente : Usuario
    {
        public Cliente(string nombre, string apellido, string correo, string contrasena, string telefono, string direccion, string rol)
            : base(nombre, apellido, correo, contrasena, telefono, direccion, rol)
        {
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            // Guardar primero en la tabla usuarios
            int idUsuario = base.GuardarEnBD(conexion);

            // Luego en la tabla clientes
            string query = "INSERT INTO clientes (id_usuario) VALUES (@idUsuario)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.ExecuteNonQuery();
        }
    }
}
