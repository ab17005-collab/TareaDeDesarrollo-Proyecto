using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Cliente : Usuario
    {
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Cliente(string nombreUsuario, string apellidoUsuario, string contrasena, string rol,
                       string correo, string telefono, string direccion)
            : base(nombreUsuario, apellidoUsuario, contrasena, rol)
        {
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
            FechaRegistro = DateTime.Now;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            // 1️⃣ Guardar en la tabla "usuarios"
            int idUsuario = base.GuardarEnBD(conexion);

            // 2️⃣ Guardar en la tabla "clientes"
            string query = @"INSERT INTO clientes (idCliente, nombre, apellido, direccion, telefono, email, fechaRegistro)
                             VALUES (@idCliente, @nombre, @apellido, @direccion, @telefono, @correo, @fechaRegistro)";

            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idCliente", idUsuario);
            cmd.Parameters.AddWithValue("@nombre", NombreUsuario);
            cmd.Parameters.AddWithValue("@apellido", ApellidoUsuario);
            cmd.Parameters.AddWithValue("@direccion", Direccion);
            cmd.Parameters.AddWithValue("@telefono", Telefono);
            cmd.Parameters.AddWithValue("@correo", Correo);
            cmd.Parameters.AddWithValue("@fechaRegistro", FechaRegistro);
            cmd.ExecuteNonQuery();
        }
    }
}
