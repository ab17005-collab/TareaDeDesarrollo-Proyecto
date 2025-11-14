using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clave1_Grupo1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Definir varables globales
        Dictionary<string, string> usuarios = new Dictionary<string, string>();

        private void Form2_Load(object sender, EventArgs e)
        {
        // Agregar algunos usuarios de ejemplo

        // Configurar layout del formulario
        lblUsuarioNoEncontrado.Visible = false;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            
        }

        

private void btnBuscar_Click(object sender, EventArgs e)
    {
        string entrada = txtBarraBusqueda.Text.ToLower().Trim();

        if (string.IsNullOrEmpty(entrada))
        {
            MessageBox.Show("Ingrese un correo electrónico para buscar.",
                "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            string servidor = "localhost";
            string bd = "veterinariapatitasypelos";
            string usuarioBD = "root";
            string passwordBD = "root";

            string cadenaConexion = $"Database={bd}; Data Source={servidor}; User Id={usuarioBD}; Password={passwordBD};";
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);

            conexion.Open();

            string query = @"
            SELECT 
                c.id_cliente,
                u.id_usuario,
                u.nombre,
                u.apellido,
                u.numero_telefonico,
                u.correo_electronico,
                u.direccion
            FROM clientes c
            INNER JOIN usuarios u ON c.id_usuario = u.id_usuario
            WHERE LOWER(u.correo_electronico) = @correo;";

            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@correo", entrada);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblUsuarioNoEncontrado.Visible = false;

                        int idCliente = reader.GetInt32("id_cliente");
                        string nombre = reader.GetString("nombre");
                        string apellido = reader.GetString("apellido");
                        string telefono = reader.GetString("numero_telefonico");
                        string correo = reader.GetString("correo_electronico");
                        string direccion = reader.GetString("direccion");

                        DialogResult msg = MessageBox.Show(
                            "¿Es el usuario correcto?\n\n" +
                            $"ID Cliente: {idCliente}\n" +
                            $"Nombre: {nombre} {apellido}\n" +
                            $"Teléfono: {telefono}\n" +
                            $"Correo: {correo}\n" +
                            $"Dirección: {direccion}",
                            "Confirmar Usuario",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (msg == DialogResult.Yes)
                        {
                                // Guardar datos en el diccionario
                                Dictionary<string, string> usuario = new Dictionary<string, string>
                                {
                                    { "idCliente", idCliente.ToString() },
                                    { "nombre", nombre },
                                    { "apellido", apellido },
                                    { "telefono", telefono },
                                    { "correo", correo },
                                    { "direccion", direccion }
                                };


                                // Abrir Form3 enviando los datos
                                Form3 form3 = new Form3(usuario);
                                form3.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        lblUsuarioNoEncontrado.Visible = true;
                    }
                }
            }

            conexion.Close();
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error al consultar en la base de datos:\n" + ex.Message,
                "Error MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

        private void btnUsuarioNuevo_Click(object sender, EventArgs e)
        {
            frmCrearUsuario formRegister = new frmCrearUsuario();
            formRegister.ShowDialog();
        }

        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbIdCliente_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbTelefono_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbCorreoElectronico_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
