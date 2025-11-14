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
using MySql.Data.MySqlClient; // Incluir libreria para operaciones con MySQL

namespace Clave1_Grupo1
{
    public partial class Form1 : Form
    {
        static string servidor = "localhost"; //Nombre o ip del servidor de MySQL
        static string bd = "veterinariapatitasypelos"; //Nombre de la base de datos
        static string usuario = "root"; //Usuario de acceso a MySQL
        static string password = "root"; //Contraseña de usuario de acceso a MySQL
                                         //Crearemos la cadena de conexión concatenando las variables
        static string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor +
        "; User Id=" + usuario + "; Password=" + password + "";
        //Instancia para conexión a MySQL, recibe la cadena de conexión
        static MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
        public Form1()
        {
            InitializeComponent();
        }

        // Declaracion de variables globales
        string email = "admin@patitasypelos.com";
        string passwordAdmin = "admin123";

        string emailIngresado;
        string passwordIngresado;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Asignar metodo de validacion al evento Leave del campo de email
            txtEmail.Leave += ValidarEmail_Leave;
        }

        /// <summary>
        /// Este evento se ejecuta al perder el foco de un textBox para validar el correo electrónico
        /// </summary>
        private void ValidarEmail_Leave(object sender, EventArgs e)
        {
            // corregir mayusculas y espacios en blanco
            emailIngresado = txtEmail.Text.ToLower().Trim();
            txtEmail.Text = emailIngresado;

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
            // Evitar que los campos estén vacíos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar credenciales
            emailIngresado = txtEmail.Text.ToLower().Trim();
            passwordIngresado = txtPassword.Text;

            if (emailIngresado == email && passwordIngresado == passwordAdmin)
            {
                // Abrir el formulario principal
                Form2 formularioPrincipal = new Form2();
                formularioPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Intente nuevamente.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
