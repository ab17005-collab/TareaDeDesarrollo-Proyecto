using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Compra
    {
        // ===========================
        // Atributos
        // ===========================
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; private set; }
        public string MetodoPago { get; set; } // Efectivo, Tarjeta, Bitcoin

        // ===========================
        // Relaciones
        // ===========================
        public Cliente Cliente { get; set; }
        public List<DetalleCompra> Detalles { get; set; }

        // ===========================
        // Constructores
        // ===========================
        public Compra()
        {
            Detalles = new List<DetalleCompra>();
            FechaCompra = DateTime.Now;
        }

        public Compra(int idCompra, Cliente cliente, string metodoPago)
        {
            IdCompra = idCompra;
            Cliente = cliente;
            MetodoPago = metodoPago;
            FechaCompra = DateTime.Now;
            Detalles = new List<DetalleCompra>();
        }

        // ===========================
        // Métodos
        // ===========================
        public void AgregarDetalle(DetalleCompra detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException(nameof(detalle));

            Detalles.Add(detalle);
            detalle.Producto.ActualizarStock(detalle.Cantidad);
            CalcularTotal();
        }

        public void CalcularTotal()
        {
            Total = Detalles.Sum(d => d.Subtotal);
        }

        public void MostrarCompra()
        {
            Console.WriteLine($"Compra ID: {IdCompra}");
            Console.WriteLine($"Fecha: {FechaCompra}");
            //Console.WriteLine($"Cliente: {Cliente?.NombreCompleto}");
            Console.WriteLine($"Método de pago: {MetodoPago}");
            Console.WriteLine($"Total: ${Total}");
            Console.WriteLine("Detalles de la compra:");

            foreach (var detalle in Detalles)
            {
                detalle.MostrarDetalle();
                Console.WriteLine("--------------------------------");
            }
        }
    }
}
