using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Producto
    {
        // ===========================
        // Atributos
        // ===========================
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } // Ej: "Alimento", "Accesorio", "Medicamento"
        public bool Disponible { get; set; }

        // ===========================
        // Constructores
        // ===========================
        public Producto() { }

        public Producto(int idProducto, string nombreProducto, string descripcion,
                        decimal precioUnitario, int stock, string categoria, bool disponible = true)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            PrecioUnitario = precioUnitario;
            Stock = stock;
            Categoria = categoria;
            Disponible = disponible;
        }

        // ===========================
        // Métodos
        // ===========================
        public void ActualizarStock(int cantidadVendida)
        {
            if (cantidadVendida <= 0)
                throw new ArgumentException("La cantidad vendida debe ser mayor que cero.");

            if (cantidadVendida > Stock)
                throw new InvalidOperationException("No hay suficiente stock para completar la venta.");

            Stock -= cantidadVendida;
            if (Stock <= 0)
                Disponible = false;
        }

        public void Reabastecer(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a reabastecer debe ser mayor que cero.");

            Stock += cantidad;
            Disponible = true;
        }

        public void MostrarProducto()
        {
            Console.WriteLine($"ID: {IdProducto}");
            Console.WriteLine($"Nombre: {NombreProducto}");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Categoría: {Categoria}");
            Console.WriteLine($"Precio Unitario: ${PrecioUnitario}");
            Console.WriteLine($"Stock Disponible: {Stock}");
            Console.WriteLine($"Disponible: {(Disponible ? "Sí" : "No")}");
        }
    }
}
