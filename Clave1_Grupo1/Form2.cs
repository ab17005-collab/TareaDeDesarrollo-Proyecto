using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Clave1_Grupo1
{
    public partial class Form2 : Form
    {
        private static string servidor = "localhost";
        private static string bd = "veterinariapatitasypelos";
        private static string usuarioDb = "root";
        private static string passwordDb = "root";

        private static string cadenaConexion =
            $"Server={servidor};Port=3306;Database={bd};User Id={usuarioDb};Password={passwordDb};" +
            "SslMode=none;AllowPublicKeyRetrieval=True";
        private List<dynamic> usuario = new List<dynamic>();
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        // Configurar layout del formulario
        chkTodos.Checked = true;
        lblUsuarioNoEncontrado.Visible = false;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            // ⭐ NUEVO: abrir el formulario para crear usuario
            using (var frm = new FormCrearUsuario())   // <-- ESTE ES EL NUEVO FORM
            {
                var result = frm.ShowDialog();         // lo mostramos como diálogo modal

                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Usuario creado correctamente.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ⭐ OPCIONAL: aquí podrías recargar la lista de usuarios desde la BD
                    // para que la búsqueda use también el nuevo usuario.
                }
            }
        }        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string entrada = txtBarraBusqueda.Text.Trim();
            lblUsuarioNoEncontrado.Visible = false;

            if (string.IsNullOrWhiteSpace(entrada))
            {
                MessageBox.Show("Ingrese algún dato para buscar (ID, teléfono o correo).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool usuarioEncontrado = false;

            try
            {
                using (var cn = new MySqlConnection(cadenaConexion))
                {
                    cn.Open();

                    // Intentamos convertir la entrada a número (por si es idCliente)
                    int idBuscado = 0;
                    int.TryParse(entrada, out idBuscado);

                    string sql = @"
                SELECT idCliente, nombre, apellido, direccion, telefono, email
                FROM clientes
                WHERE idCliente = @id
                   OR telefono = @texto
                   OR LOWER(email) = LOWER(@texto)
                LIMIT 1;
            ";

                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@id", idBuscado);
                        cmd.Parameters.AddWithValue("@texto", entrada);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuarioEncontrado = true;

                                // Construimos la lista como antes
                                this.usuario = new List<dynamic>
                        {
                            reader["idCliente"],                     // [0] ID
                            reader["nombre"].ToString(),             // [1] Nombre
                            reader["apellido"].ToString(),           // [2] Apellido
                            "N/A",                                   // [3] Edad (ya no la usamos)
                            reader["telefono"]?.ToString(),          // [4] Teléfono
                            reader["email"]?.ToString(),             // [5] Email
                            reader["direccion"]?.ToString(),         // [6] Dirección
                            "N/A"                                    // [7] Código de cliente (placeholder)
                        };

                                DialogResult resultado = MessageBox.Show(
                                    "¿Es el usuario correcto?\n\n" +
                                    $"ID: {this.usuario[0]}\n" +
                                    $"Nombre: {this.usuario[1]} {this.usuario[2]}\n" +
                                    $"Edad: {this.usuario[3]}\n" +
                                    $"Teléfono: {this.usuario[4]}\n" +
                                    $"Email: {this.usuario[5]}\n" +
                                    $"Dirección: {this.usuario[6]}\n" +
                                    $"Código de Cliente: {this.usuario[7]}",
                                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (resultado == DialogResult.Yes)
                                {
                                    Form3 form3 = new Form3(this.usuario);
                                    form3.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                }

                if (!usuarioEncontrado)
                {
                    lblUsuarioNoEncontrado.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar usuario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
