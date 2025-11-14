using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Clave1_Grupo1
{
    public partial class frmCrearUsuario : Form
    {
        static string servidor = "localhost";
        static string bd = "veterinariapatitasypelos";
        static string usuario = "root";
        static string password = "root";
        static string cadenaConexion = $"Database={bd}; Data Source={servidor}; User Id={usuario}; Password={password};";
        static MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);

        public frmCrearUsuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexionBD.Open();

                string nombre = txtNombreUsuario.Text.Trim();
                string apellido = txtApellidoUsuario.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string rol = cboRol.SelectedItem?.ToString();
                string especialidad = txtEspecialidad.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(rol))
                {
                    MessageBox.Show("⚠️ Por favor completa los campos obligatorios (nombre, contraseña, rol).");
                    return;
                }

                if (rol == "Cliente")
                {
                    Cliente cliente = new Cliente(nombre, apellido, correo, contrasena, telefono, direccion, rol);
                    cliente.GuardarEnBD(conexionBD);
                }
                else if (rol == "Veterinario")
                {
                    Veterinario vet = new Veterinario(nombre, apellido, correo, contrasena, telefono, direccion, rol, especialidad);
                    vet.GuardarEnBD(conexionBD);
                }
                else if (rol == "Administrador")
                {
                    Administrador admin = new Administrador(nombre, apellido, correo, contrasena, telefono, direccion, rol);
                    admin.GuardarEnBD(conexionBD);
                }

                MessageBox.Show("✅ Usuario registrado correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar en la base de datos:\n" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }
    }
}
