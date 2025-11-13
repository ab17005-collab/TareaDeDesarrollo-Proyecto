using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Horario
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idHorario;
        private DateTime fecha;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
        private bool disponible;
        private Veterinario veterinario;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdHorario
        {
            get { return idHorario; }
            set { idHorario = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public TimeSpan HoraInicio
        {
            get { return horaInicio; }
            set { horaInicio = value; }
        }

        public TimeSpan HoraFin
        {
            get { return horaFin; }
            set
            {
                if (value <= horaInicio)
                    throw new ArgumentException("La hora de fin debe ser posterior a la hora de inicio.");
                horaFin = value;
            }
        }

        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }

        public Veterinario Veterinario
        {
            get { return veterinario; }
            set { veterinario = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Horario() { }

        public Horario(int idHorario, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, bool disponible, Veterinario veterinario)
        {
            IdHorario = idHorario;
            Fecha = fecha;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Disponible = disponible;
            Veterinario = veterinario;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================

        public void MarcarComoOcupado()
        {
            Disponible = false;
            Console.WriteLine($"El horario del {Fecha.ToShortDateString()} de {HoraInicio} a {HoraFin} ha sido marcado como ocupado.");
        }

        public void MarcarComoDisponible()
        {
            Disponible = true;
            Console.WriteLine($"El horario del {Fecha.ToShortDateString()} de {HoraInicio} a {HoraFin} está disponible nuevamente.");
        }

        public bool EstaDisponible()
        {
            return Disponible;
        }

        public override string ToString()
        {
            string estado = Disponible ? "Disponible" : "Ocupado";
            return $"{Fecha.ToShortDateString()} | {HoraInicio} - {HoraFin} | {estado} | Vet: {Veterinario?.NombreCompleto()}";
        }
    }
}
