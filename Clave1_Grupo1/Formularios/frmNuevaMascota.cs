using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Clave1_Grupo1
{
    public partial class frmNuevaMascota : Form
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

        // definir variable para idCliente (Clave Foránea) del dueño de la mascota
        int _idCliente;

        public frmNuevaMascota(int idCliente)
        {
            InitializeComponent();
            _idCliente = idCliente;
        }

        private void frmNuevaMascota_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Abrir la conexión
                conexionBD.Open();

                // 2. Crear el comando SQL
                MySqlCommand consulta = new MySqlCommand();
                consulta.Connection = conexionBD;

                // **IMPORTANTE: Usar Parámetros para SEGURIDAD y evitar errores de SQL Injection**
                consulta.CommandText = "INSERT INTO mascotas (idMascota, nombre, especie, raza, sexo, fechaNacimiento, idCliente) " +
                                       "VALUES (0, @nombre, @especie, @raza, @sexo, @fechaNacimiento, @idCliente)";

                // Asignar los valores a los parámetros
                consulta.Parameters.AddWithValue("@nombre", txtNombre.Text);
                consulta.Parameters.AddWithValue("@especie", txtEspecie.Text);
                consulta.Parameters.AddWithValue("@raza", txtRaza.Text);
                consulta.Parameters.AddWithValue("@sexo", txtSexo.Text);
                consulta.Parameters.AddWithValue("@fechaNacimiento", txtFechaNacimiento.Text);
                consulta.Parameters.AddWithValue("@idCliente", _idCliente); // Valor de la Clave Foránea

                // 3. Ejecutar la consulta
                int filasAfectadas = consulta.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Mascota guardada exitosamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la mascota.");
                }

            }
            catch (MySqlException ex)
            {
                // Mostrar un error más específico
                MessageBox.Show("Error al guardar en la base de datos. Asegúrate que el idCliente existe. \n\nDetalle: " + ex.Message);
            }
            // El bloque finally permanece igual
            finally
            {
                conexionBD.Close(); //se cierra la conexion
            }
        }
    }
}
