using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class DetalleCompra
    {
        // ===========================
        // Atributos
        // ===========================
        public int IdDetalle { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; private set; }

        // ===========================
        // Relaciones
        // ===========================
        public Producto Producto { get; set; }

        // ===========================
        // Constructores
        // ===========================
        public DetalleCompra() { }

        public DetalleCompra(int idDetalle, Producto producto, int cantidad)
        {
            IdDetalle = idDetalle;
            Producto = producto;
            Cantidad = cantidad;
            CalcularSubtotal();
        }

        // ===========================
        // Métodos
        // ===========================
        public void CalcularSubtotal()
        {
            if (Producto == null)
                throw new InvalidOperationException("No se ha asignado un producto al detalle.");

            Subtotal = Producto.PrecioUnitario * Cantidad;
        }

        public void MostrarDetalle()
        {
            Console.WriteLine($"Producto: {Producto?.NombreProducto}");
            Console.WriteLine($"Cantidad: {Cantidad}");
            Console.WriteLine($"Precio Unitario: ${Producto?.PrecioUnitario}");
            Console.WriteLine($"Subtotal: ${Subtotal}");
        }
    }
}
