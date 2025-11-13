using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Administrador : Usuario
    {
        // ===========================
        // 🔹 Atributos
        // ===========================
        private int idAdministrador;
        private string nombres;
        private string apellidos;
        private string telefono;
        private string email;

        // ===========================
        // 🔹 Propiedades
        // ===========================
        public int IdAdministrador
        {
            get { return idAdministrador; }
            set { idAdministrador = value; }
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

        // ===========================
        // 🔹 Constructores
        // ===========================
        public Administrador() { }

        public Administrador(int idAdministrador, string nombres, string apellidos,
                             string telefono, string email,
                             int idUsuario, string nombreUsuario, string contrasena)
            : base(idUsuario, nombreUsuario, contrasena, "Administrador")
        {
            this.idAdministrador = idAdministrador;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.email = email;
        }

        // ===========================
        // 🔹 Métodos
        // ===========================

        public void GestionarUsuarios(List<Usuario> usuarios)
        {
            Console.WriteLine("El administrador está gestionando los usuarios del sistema...");
            // Aquí irían las operaciones CRUD sobre usuarios (Agregar, Modificar, Eliminar)
        }

        public void GestionarClientes(List<Cliente> clientes)
        {
            Console.WriteLine("El administrador está gestionando los clientes registrados...");
        }

        public void GestionarMascotas(List<Mascota> mascotas)
        {
            Console.WriteLine("El administrador está gestionando la información de las mascotas...");
        }

        public void GestionarVeterinarios(List<Veterinario> veterinarios)
        {
            Console.WriteLine("El administrador está gestionando los veterinarios de la clínica...");
        }

        public void GestionarCitas(List<Cita> citas)
        {
            Console.WriteLine("El administrador está gestionando las citas médicas...");
        }

        public void GestionarInventario(List<Producto> productos)
        {
            Console.WriteLine("El administrador está gestionando el inventario de productos...");
        }

        public void GenerarReportes()
        {
            Console.WriteLine("Generando reportes del sistema (citas, ventas, historial clínico, etc.)...");
        }

        public string NombreCompleto()
        {
            return $"{nombres} {apellidos}";
        }

        public override string ToString()
        {
            return $"Administrador: {NombreCompleto()} ({NombreUsuario})";
        }
    }
}
