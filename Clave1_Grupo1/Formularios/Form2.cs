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
        List<List<dynamic>> usuarios = new List<List<dynamic>>();
        List<dynamic> usuario = new List<dynamic>();

        private void Form2_Load(object sender, EventArgs e)
        {
        // Agregar algunos usuarios de ejemplo
        usuarios.Add(new List<dynamic> { "2205457100", "Jaime Roberto", "Aldana Beltran", "29", "72399078", "ab17005@ues.edu.sv", "4440 Caynor Circle, Rochelle Park, NJ 07662", "055613056" });
        usuarios.Add(new List<dynamic> { "2205457111", "Maria Fernanda", "Lopez Martinez", "25", "72399079", "NazarOlveraSuarez@superrito.com", "1234 Elm Street, Springfield, IL 62704", "055613057" });

        // Configurar layout del formulario
        lblUsuarioNoEncontrado.Visible = false;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string entrada = txtBarraBusqueda.Text.ToLower().Trim();
            bool usuarioEncontrado = false;

            // Encontrar usuario
            foreach (var usuario in usuarios)
            {
                if (usuario[0].ToString().ToLower() == entrada ||
                    usuario[4].ToString().ToLower() == entrada ||
                    usuario[5].ToString().ToLower() == entrada ||
                    usuario[7].ToString().ToLower() == entrada)
                {
                    usuarioEncontrado = true;
                    this.usuario = usuario;
                    lblUsuarioNoEncontrado.Visible = false;

                    // Confirmar si es el usuario correcto
                    DialogResult resultado = MessageBox.Show(
                                        "¿Es el usuario correcto?\n\n"+
                                        $"ID: {usuario[0]}\n" +
                                        $"Nombre: {usuario[1]} {usuario[2]}\n" +
                                        $"Edad: {usuario[3]}\n" +
                                        $"Teléfono: {usuario[4]}\n" +
                                        $"Email: {usuario[5]}\n" +
                                        $"Dirección: {usuario[6]}\n" +
                                        $"Código de Cliente: {usuario[7]}", 
                                        "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                    // Si es el usuario correcto abrir formulario 3
                    Form3 form3 = new Form3(usuario);
                    form3.Show();
                    this.Hide();
                    }
                }

                // Si no se encuentra el usuario
                if (!usuarioEncontrado)
                {
                    lblUsuarioNoEncontrado.Visible = true;
                }
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
