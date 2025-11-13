using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Clave1_Grupo1
{
    public class Cita
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idCita;
        private DateTime fechaCita;
        private Horario horario;
        private string motivo;
        private string estado; // Programada, Reprogramada, Cancelada, Completada
        private Mascota mascota;
        private Cliente cliente;
        private Veterinario veterinario;
        private Pago pago;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdCita
        {
            get { return idCita; }
            set { idCita = value; }
        }

        public DateTime FechaCita
        {
            get { return fechaCita; }
            set { fechaCita = value; }
        }

        public Horario Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public string Estado
        {
            get { return estado; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El estado de la cita no puede estar vacío.");
                estado = value;
            }
        }

        public Mascota Mascota
        {
            get { return mascota; }
            set { mascota = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Veterinario Veterinario
        {
            get { return veterinario; }
            set { veterinario = value; }
        }

        public Pago Pago
        {
            get { return pago; }
            set { pago = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Cita() { }

        public Cita(int idCita, DateTime fechaCita, Horario horario,
                    string motivo, Mascota mascota, Cliente cliente, Veterinario veterinario)
        {
            IdCita = idCita;
            FechaCita = fechaCita;
            Horario = horario;
            Motivo = motivo;
            Estado = "Programada";
            Mascota = mascota;
            Cliente = cliente;
            Veterinario = veterinario;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public void Programar()
        {
            Estado = "Programada";
            Console.WriteLine($"Cita programada para {Mascota?.NombreMascota} el {FechaCita.ToShortDateString()} ({Horario?.HoraInicio} - {Horario?.HoraFin})");
        }

        public void Reprogramar(DateTime nuevaFecha, Horario nuevoHorario)
        {
            FechaCita = nuevaFecha;
            Horario = nuevoHorario;
            Estado = "Reprogramada";
            Console.WriteLine($"Cita reprogramada para {Mascota?.NombreMascota} el {nuevaFecha.ToShortDateString()} ({nuevoHorario?.HoraInicio} - {nuevoHorario?.HoraFin})");
        }

        public void Cancelar(string motivoCancelacion = "Cancelada por el cliente")
        {
            Estado = "Cancelada";
            Motivo = motivoCancelacion;
            Console.WriteLine($"Cita cancelada: {Motivo}");
        }

        public void Completar(Pago pago)
        {
            Estado = "Completada";
            Pago = pago;
            Console.WriteLine($"Cita completada y pago registrado ({pago.MetodoPago} - ${pago.Monto}).");
        }

        public string ObtenerResumen()
        {
            return $"Cita #{IdCita} - {Mascota?.NombreMascota} ({Cliente?.NombreCompleto()})\n" +
                   $"Fecha: {FechaCita.ToShortDateString()} | Estado: {Estado}\n" +
                   $"Veterinario: {Veterinario?.NombreCompleto()}";
        }

        public override string ToString()
        {
            return $"Cita #{IdCita} - {Mascota?.NombreMascota} ({Estado})";
        }
    }
}
