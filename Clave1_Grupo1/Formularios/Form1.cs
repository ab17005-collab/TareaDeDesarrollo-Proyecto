using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public partial class Form1 : Form
    {
        // Configuración de conexión
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

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCorreoElectronico.Leave += ValidarEmail_Leave;
        }

        private void ValidarEmail_Leave(object sender, EventArgs e)
        {
            string emailIngresado = txtCorreoElectronico.Text.ToLower().Trim();
            txtCorreoElectronico.Text = emailIngresado;

            TextBox txt = sender as TextBox;
            if (txt != null)
                ValidarEmail(txt);
        }

        private void ValidarEmail(TextBox txt)
        {
            string patronEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex validaEmail = new Regex(patronEmail);

            if (validaEmail.IsMatch(txt.Text))
                erpEmail.SetError(txt, "");
            else
                erpEmail.SetError(txt, "Correo electrónico no válido.");
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreoElectronico.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese el correo y la contraseña.", "Campos vacíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexionBD.Open();

                // Validar si el usuario existe en la tabla 'usuarios'
                string queryUsuario = @"SELECT id_usuario, nombre, apellido, rol 
                                        FROM usuarios 
                                        WHERE correo_electronico = @correo AND contrasena = @contrasena";

                using (MySqlCommand cmd = new MySqlCommand(queryUsuario, conexionBD))
                {
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idUsuario = reader.GetInt32("id_usuario");
                            string nombre = reader.GetString("nombre");
                            string apellido = reader.GetString("apellido");
                            string rol = reader.GetString("rol");
                            reader.Close();

                            MessageBox.Show($"Bienvenido, {nombre} {apellido}.\nRol: {rol}",
                                "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 🔹 En lugar de abrir por rol, se abre directamente el Form2
                            AbrirForm2();
                        }
                        else
                        {
                            MessageBox.Show("Correo o contraseña incorrectos.",
                                "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar con la base de datos:\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexionBD.State == System.Data.ConnectionState.Open)
                    conexionBD.Close();
            }
        }

        private void AbrirForm2()
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void llblCrearUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCrearUsuario formRegister = new frmCrearUsuario();
            formRegister.ShowDialog();
        }
    }
}
