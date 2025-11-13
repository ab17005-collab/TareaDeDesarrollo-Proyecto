using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Cliente : Usuario
    {
        // ===========================
        //      ATRIBUTOS
        // ===========================
        private int idCliente;
        private string nombres;
        private string apellidos;
        private string telefono;
        private string email;
        private string direccion;

        // Relaciones
        private List<Mascota> mascotas;
        private List<Cita> citas;
        private List<Compra> compras;

        // ===========================
        //      PROPIEDADES
        // ===========================
        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public List<Mascota> Mascotas
        {
            get { return mascotas; }
            set { mascotas = value; }
        }

        public List<Cita> Citas
        {
            get { return citas; }
            set { citas = value; }
        }

        public List<Compra> Compras
        {
            get { return compras; }
            set { compras = value; }
        }

        // ===========================
        //      CONSTRUCTORES
        // ===========================
        public Cliente()
        {
            mascotas = new List<Mascota>();
            citas = new List<Cita>();
            compras = new List<Compra>();
        }

        public Cliente(int idCliente, string nombres, string apellidos, string telefono,
                       string email, string direccion, string nombreUsuario, string contraseña)
        //    : base(nombreUsuario, contraseña, "Cliente")
        {
            this.idCliente = idCliente;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;

            mascotas = new List<Mascota>();
            citas = new List<Cita>();
            compras = new List<Compra>();
        }

        // ===========================
        //      MÉTODOS
        // ===========================

        /// <summary>
        /// Registra una nueva mascota para este cliente.
        /// </summary>
        public void RegistrarMascota(Mascota nuevaMascota)
        {
            if (nuevaMascota != null)
            {
                mascotas.Add(nuevaMascota);
                //Console.WriteLine($"Mascota '{nuevaMascota.Nombre}' registrada correctamente para {nombres} {apellidos}.");
            }
        }

        /// <summary>
        /// Agenda una nueva cita para una mascota del cliente.
        /// </summary>
        public void AgendarCita(Cita nuevaCita)
        {
            if (nuevaCita != null)
            {
                citas.Add(nuevaCita);
                //Console.WriteLine($"Cita agendada para la mascota {nuevaCita.Mascota.Nombre} el {nuevaCita.Fecha.ToShortDateString()}.");
            }
        }

        /// <summary>
        /// Cancela una cita existente.
        /// </summary>
        public void CancelarCita(int idCita)
        {
            Cita cita = citas.Find(c => c.IdCita == idCita);
            if (cita != null)
            {
                cita.Estado = "Cancelada";
                Console.WriteLine($"La cita {idCita} ha sido cancelada.");
            }
            else
            {
                Console.WriteLine($"No se encontró la cita con ID {idCita}.");
            }
        }

        /// <summary>
        /// Registra una compra realizada por el cliente.
        /// </summary>
        public void RealizarCompra(Compra nuevaCompra)
        {
            if (nuevaCompra != null)
            {
                compras.Add(nuevaCompra);
                Console.WriteLine($"Compra registrada correctamente. Total: ${nuevaCompra.Total}");
            }
        }

        /// <summary>
        /// Devuelve el nombre completo del cliente.
        /// </summary>
        public string NombreCompleto()
        {
            return $"{nombres} {apellidos}";
        }
    }
}
