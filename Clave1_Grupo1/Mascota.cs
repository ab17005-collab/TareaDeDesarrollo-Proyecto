using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Mascota
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idMascota;
        private string nombreMascota;
        private string especie;
        private string raza;
        private int edad;
        private string sexo;
        private Cliente dueno;
        private Expediente expediente;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdMascota
        {
            get { return idMascota; }
            set { idMascota = value; }
        }

        public string NombreMascota
        {
            get { return nombreMascota; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la mascota no puede estar vacío.");
                nombreMascota = value;
            }
        }

        public string Especie
        {
            get { return especie; }
            set { especie = value; }
        }

        public string Raza
        {
            get { return raza; }
            set { raza = value; }
        }

        public int Edad
        {
            get { return edad; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("La edad no puede ser negativa.");
                edad = value;
            }
        }

        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public Cliente Dueno
        {
            get { return dueno; }
            set { dueno = value; }
        }

        public Expediente Expediente
        {
            get { return expediente; }
            set { expediente = value; }
        }

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Mascota() { }

        public Mascota(int idMascota, string nombreMascota, string especie, string raza, int edad, string sexo, Cliente dueno)
        {
            IdMascota = idMascota;
            NombreMascota = nombreMascota;
            Especie = especie;
            Raza = raza;
            Edad = edad;
            Sexo = sexo;
            Dueno = dueno;
            Expediente = new Expediente(this);
        }

        // ===========================
        // 🔹 Métodos
        // ===========================
        public void MostrarInformacion()
        {
            Console.WriteLine($"Mascota: {NombreMascota} ({Especie}, {Raza}) - Edad: {Edad} años - Dueño: {Dueno?.NombreCompleto()}");
        }

        public override string ToString()
        {
            return $"{NombreMascota} - {Especie} ({Raza})";
        }
    }
}
