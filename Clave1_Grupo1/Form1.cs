using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // Incluir libreria para operaciones con expresiones regulares
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Clave1_Grupo1
{
    public partial class Form1 : Form
    {
        // Configuración de conexión a la base de datos
        static string servidor = "localhost";
        static string bd = "veterinariapatitasypelos";
        static string usuario = "root";
        static string password = "root";

        static string cadenaConexion = $"Database={bd}; Data Source={servidor}; User Id={usuario}; Password={password};";
        static MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
        public Form1()
        {
            InitializeComponent();
        }


        string emailIngresado;
        string passwordIngresado;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Asignar metodo de validacion al evento Leave del campo de email
            txtUsuario.Leave += ValidarEmail_Leave;
        }

        /// <summary>
        /// Este evento se ejecuta al perder el foco de un textBox para validar el correo electrónico
        /// </summary>
        private void ValidarEmail_Leave(object sender, EventArgs e)
        {
            // corregir mayusculas y espacios en blanco
            emailIngresado = txtUsuario.Text.ToLower().Trim();
            txtUsuario.Text = emailIngresado;

            // Identificar el TextBox que disparó el evento
            TextBox txt = sender as TextBox;

            if (txt != null)
            {
                // Llamar al método de validación
                ValidarEmail(txt);
            }

        }

        private void ValidarEmail(TextBox txt)
        {
            string patronEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"; // Patrón básico para validar email
            Regex validaEmail = new Regex(patronEmail);

            if (validaEmail.IsMatch(txt.Text))
            {
                // Si el email es válido, limpiar cualquier mensaje de error
                erpEmail.SetError(txt, "");
            }
            else
            {
                // Si el email no es válido, mostrar mensaje de error
                erpEmail.SetError(txt, "Correo electrónico no válido.");
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese el usuario y la contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexionBD.Open();

                string query = "SELECT idUsuario, nombre_usuario, contrasena, rol FROM usuarios WHERE nombre_usuario = @usuario AND contrasena = @contrasena;";
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string rol = reader.GetString("rol");
                    int idUsuario = reader.GetInt32("idUsuario");
                    reader.Close();

                    MessageBox.Show($"Bienvenido, {nombreUsuario}. Rol: {rol}", "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AbrirFormularioPorRol(rol, idUsuario, nombreUsuario);
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar con la base de datos:\n" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void AbrirFormularioPorRol(string rol, int idUsuario, string nombreUsuario)
        {
            Form siguiente = null;

            switch (rol)
            {
                case "Administrador":
                    //siguiente = new frmMenuAdministrador(idUsuario, nombreUsuario);
                    break;

                case "Veterinario":
                    //siguiente = new frmMenuVeterinario(idUsuario, nombreUsuario);
                    break;

                case "Cliente":
                    //siguiente = new frmMenuCliente(idUsuario, nombreUsuario);
                    break;

                default:
                    MessageBox.Show("Rol no reconocido. Contacte al administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            this.Hide();
            siguiente.ShowDialog();
            this.Show(); // Regresar al login al cerrar el otro formulario
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void llblCrearUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abrir el formulario de creación de usuario
            frmCrearUsuario formRegister = new frmCrearUsuario();
            formRegister.ShowDialog();
        }
    }
}
