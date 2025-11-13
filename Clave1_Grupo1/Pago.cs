using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Pago
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idPago;
        private DateTime fechaPago;
        private double monto;
        private string metodoPago; // Ej: "Efectivo", "Tarjeta", "Transferencia"
        private bool confirmado;
        private Cliente cliente;
        private Consulta consulta;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }

        public double Monto
        {
            get { return monto; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El monto del pago no puede ser negativo.");
                monto = value;
            }
        }

        public string MetodoPago
        {
            get { return metodoPago; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El método de pago no puede estar vacío.");
                metodoPago = value;
            }
        }

        public bool Confirmado
        {
            get { return confirmado; }
            set { confirmado = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Consulta Consulta
        {
            get { return consulta; }
            set { consulta = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Pago() { }

        public Pago(int idPago, DateTime fechaPago, double monto, string metodoPago, bool confirmado, Cliente cliente, Consulta consulta)
        {
            IdPago = idPago;
            FechaPago = fechaPago;
            Monto = monto;
            MetodoPago = metodoPago;
            Confirmado = confirmado;
            Cliente = cliente;
            Consulta = consulta;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public void ConfirmarPago()
        {
            if (Confirmado)
            {
                Console.WriteLine("El pago ya fue confirmado previamente.");
                return;
            }

            Confirmado = true;
            Console.WriteLine($"✅ Pago de ${Monto:F2} confirmado el {FechaPago.ToShortDateString()} para {Cliente?.NombreCompleto()}.");
        }

        public void CancelarPago()
        {
            if (!Confirmado)
            {
                Console.WriteLine("El pago aún no había sido confirmado. No es necesario cancelarlo.");
                return;
            }

            Confirmado = false;
            Console.WriteLine($"❌ Pago cancelado para la consulta #{Consulta?.IdConsulta} del cliente {Cliente?.NombreCompleto()}.");
        }

        public override string ToString()
        {
            string estado = Confirmado ? "Confirmado" : "Pendiente";
            return $"Pago #{IdPago}: ${Monto:F2} - {MetodoPago} - {estado} - {FechaPago.ToShortDateString()}";
        }
    }
}
