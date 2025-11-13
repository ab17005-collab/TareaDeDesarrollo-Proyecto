using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Expediente
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idExpediente;
        private Mascota mascota;
        private List<Consulta> consultas;
        private List<Vacuna> vacunas;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdExpediente
        {
            get { return idExpediente; }
            set { idExpediente = value; }
        }

        public Mascota Mascota
        {
            get { return mascota; }
            set { mascota = value; }
        }

        public List<Consulta> Consultas
        {
            get { return consultas; }
            set { consultas = value; }
        }

        public List<Vacuna> Vacunas
        {
            get { return vacunas; }
            set { vacunas = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Expediente()
        {
            consultas = new List<Consulta>();
            vacunas = new List<Vacuna>();
        }

        public Expediente(Mascota mascota) : this()
        {
            this.mascota = mascota;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public void AgregarConsulta(Consulta consulta)
        {
            consultas.Add(consulta);
            Console.WriteLine("Consulta agregada al expediente.");
        }

        public void AgregarVacuna(Vacuna vacuna)
        {
            vacunas.Add(vacuna);
            Console.WriteLine("Vacuna registrada en el expediente.");
        }

        public void MostrarHistorial()
        {
            Console.WriteLine($"--- Expediente de {Mascota?.NombreMascota} ---");
            Console.WriteLine("Consultas:");
            foreach (var c in consultas)
                Console.WriteLine($"• {c}");

            Console.WriteLine("\nVacunas:");
            foreach (var v in vacunas)
                Console.WriteLine($"• {v}");
        }
    }
}
