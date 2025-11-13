using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave1_Grupo1
{
    public class Usuario
    {
        // =====================
        // 🔹 Atributos / Campos
        // =====================
        private int idUsuario;
        private string nombreUsuario;
        private string contrasena;
        private string rol; // Puede ser "Administrador", "Veterinario" o "Cliente"

        // =====================
        // 🔹 Propiedades
        // =====================
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de usuario no puede estar vacío.");
                nombreUsuario = value;
            }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La contraseña no puede estar vacía.");
                contrasena = value;
            }
        }

        public string Rol
        {
            get { return rol; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El rol no puede estar vacío.");
                rol = value;
            }
        }

        // =====================
        // 🔹 Constructores
        // =====================
        public Usuario() { }

        public Usuario(int idUsuario, string nombreUsuario, string contrasena, string rol)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
        }

        // =====================
        // 🔹 Métodos
        // =====================
        public bool IniciarSesion(string usuario, string clave)
        {
            // En una aplicación real, esto se validaría contra la base de datos
            return (usuario == NombreUsuario && clave == Contrasena);
        }

        public void CambiarContrasena(string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(nuevaContrasena))
                throw new ArgumentException("La nueva contraseña no puede estar vacía.");

            Contrasena = nuevaContrasena;
        }

        public override string ToString()
        {
            return $"Usuario: {NombreUsuario} ({Rol})";
        }
    }
}
