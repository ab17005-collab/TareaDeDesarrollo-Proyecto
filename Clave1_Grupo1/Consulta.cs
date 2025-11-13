using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Consulta
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idConsulta;
        private DateTime fechaConsulta;
        private string diagnostico;
        private string tratamiento;
        private Veterinario veterinario;
        private Mascota mascota;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdConsulta
        {
            get { return idConsulta; }
            set { idConsulta = value; }
        }

        public DateTime FechaConsulta
        {
            get { return fechaConsulta; }
            set { fechaConsulta = value; }
        }

        public string Diagnostico
        {
            get { return diagnostico; }
            set { diagnostico = value; }
        }

        public string Tratamiento
        {
            get { return tratamiento; }
            set { tratamiento = value; }
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
        public Consulta() { }

        public Consulta(int idConsulta, DateTime fechaConsulta, string diagnostico, string tratamiento, Veterinario veterinario, Mascota mascota)
        {
            IdConsulta = idConsulta;
            FechaConsulta = fechaConsulta;
            Diagnostico = diagnostico;
            Tratamiento = tratamiento;
            Veterinario = veterinario;
            Mascota = mascota;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public override string ToString()
        {
            return $"{FechaConsulta.ToShortDateString()} - {Diagnostico} (Vet: {Veterinario?.NombreCompleto()})";
        }
    }
}
