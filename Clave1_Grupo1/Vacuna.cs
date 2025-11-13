using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Vacuna
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idVacuna;
        private string nombreVacuna;
        private DateTime fechaAplicacion;
        private Veterinario veterinario;
        private Mascota mascota;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdVacuna
        {
            get { return idVacuna; }
            set { idVacuna = value; }
        }

        public string NombreVacuna
        {
            get { return nombreVacuna; }
            set { nombreVacuna = value; }
        }

        public DateTime FechaAplicacion
        {
            get { return fechaAplicacion; }
            set { fechaAplicacion = value; }
        }

        public Veterinario Veterinario
        {
            get { return veterinario; }
            set { veterinario = value; }
        }

        public Mascota Mascota
        {
            get { return mascota; }
            set { mascota = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Vacuna() { }

        public Vacuna(int idVacuna, string nombreVacuna, DateTime fechaAplicacion, Veterinario veterinario, Mascota mascota)
        {
            IdVacuna = idVacuna;
            NombreVacuna = nombreVacuna;
            FechaAplicacion = fechaAplicacion;
            Veterinario = veterinario;
            Mascota = mascota;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public override string ToString()
        {
            return $"{NombreVacuna} aplicada el {FechaAplicacion.ToShortDateString()} por {Veterinario?.NombreCompleto()}";
        }
    }
}
