using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Veterinario : Usuario
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idVeterinario;
        private string nombres;
        private string apellidos;
        private string especialidad;
        private string telefono;
        private string email;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdVeterinario
        {
            get { return idVeterinario; }
            set { idVeterinario = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del veterinario no puede estar vacío.");
                nombres = value;
            }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El apellido del veterinario no puede estar vacío.");
                apellidos = value;
            }
        }

        public string Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
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

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Veterinario() { }

        public Veterinario(int idVeterinario, string nombres, string apellidos,
                           string especialidad, string telefono, string email,
                           int idUsuario, string nombreUsuario, string contrasena)
            : base(idUsuario, nombreUsuario, contrasena, "Veterinario")
        {
            this.idVeterinario = idVeterinario;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.especialidad = especialidad;
            this.telefono = telefono;
            this.email = email;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================

        public void RegistrarConsulta(Mascota mascota, string diagnostico, string tratamiento)
        {
            Console.WriteLine($"Registrando consulta para {mascota.NombreMascota}:");
            Console.WriteLine($"Diagnóstico: {diagnostico}");
            Console.WriteLine($"Tratamiento: {tratamiento}");
            // Aquí podrías crear un objeto de tipo Consulta y guardarlo en la base de datos
        }

        public void RegistrarVacuna(Mascota mascota, string nombreVacuna, DateTime fechaAplicacion)
        {
            Console.WriteLine($"Registrando vacuna '{nombreVacuna}' para {mascota.NombreMascota} en la fecha {fechaAplicacion.ToShortDateString()}");
            // Aquí podrías agregar un registro en la tabla de vacunas del expediente de la mascota
        }

        public void ActualizarExpediente(Mascota mascota, string observaciones)
        {
            Console.WriteLine($"Actualizando expediente de {mascota.NombreMascota}: {observaciones}");
            // Aquí podrías actualizar el historial clínico en la base de datos
        }

        public string NombreCompleto()
        {
            return $"{nombres} {apellidos}";
        }

        public override string ToString()
        {
            return $"Veterinario: {NombreCompleto()} - Especialidad: {Especialidad}";
        }
    }
}
