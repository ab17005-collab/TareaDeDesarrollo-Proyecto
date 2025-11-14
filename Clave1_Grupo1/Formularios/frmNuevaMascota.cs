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
            cbxSexo.Items.Clear();
            cbxSexo.Items.Add("Seleccionar");
            cbxSexo.Items.Add("Macho");
            cbxSexo.Items.Add("Hembra");
            cbxSexo.SelectedIndex = 0;
            cbxSexo.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear objeto Mascota con los datos del formulario
                Mascota nuevaMascota = new Mascota
                {
                    Nombre = txtNombre.Text.Trim(),
                    Especie = txtEspecie.Text.Trim(),
                    Raza = txtRaza.Text.Trim(),
                    Sexo = cbxSexo.SelectedItem.ToString(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    IdCliente = _idCliente
                };

                // Guardar en base de datos
                GuardarMascotaEnBD(nuevaMascota);

                MessageBox.Show("Mascota registrada exitosamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void GuardarMascotaEnBD(Mascota mascota)
        {
            string query = @"INSERT INTO mascotas 
            (nombre, especie, raza, sexo, fecha_nacimiento, id_cliente)
            VALUES (@nombre, @especie, @raza, @sexo, @fecha_nacimiento, @id_cliente)";

            using (MySqlConnection con = new MySqlConnection(cadenaConexion))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@especie", mascota.Especie);
                cmd.Parameters.AddWithValue("@raza", mascota.Raza);
                cmd.Parameters.AddWithValue("@sexo", mascota.Sexo);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", mascota.FechaNacimiento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@id_cliente", mascota.IdCliente);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}
