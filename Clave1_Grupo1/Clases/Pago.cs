using MySql.Data.MySqlClient;
using System;

namespace Clave1_Grupo1
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdCliente { get; set; }
        public double Monto { get; set; }
        public string MetodoPago { get; set; } // Efectivo, Tarjeta, Bitcoin
        public DateTime FechaPago { get; set; }

        public Pago() { }

        public Pago(int idCliente, double monto, string metodoPago, DateTime fechaPago)
        {
            IdCliente = idCliente;
            Monto = monto;
            MetodoPago = metodoPago;
            FechaPago = fechaPago;
        }

        public void GuardarEnBD(MySqlConnection conexion)
        {
            string query = "INSERT INTO pagos (idCliente, monto, metodo_pago, fecha_pago) VALUES (@idCliente, @monto, @metodo, @fecha)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idCliente", IdCliente);
            cmd.Parameters.AddWithValue("@monto", Monto);
            cmd.Parameters.AddWithValue("@metodo", MetodoPago);
            cmd.Parameters.AddWithValue("@fecha", FechaPago);
            cmd.ExecuteNonQuery();
        }
    }
}