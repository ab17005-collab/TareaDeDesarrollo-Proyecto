namespace Clave1_Grupo1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBarraBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnCrearUsuario = new System.Windows.Forms.Button();
            this.lblUsuarioNoEncontrado = new System.Windows.Forms.Label();
            this.btnUsuarioNuevo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbTodos = new System.Windows.Forms.RadioButton();
            this.rdbIdCliente = new System.Windows.Forms.RadioButton();
            this.rdbTelefono = new System.Windows.Forms.RadioButton();
            this.rdbCorreoElectronico = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtBarraBusqueda
            // 
            this.txtBarraBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarraBusqueda.Location = new System.Drawing.Point(191, 184);
            this.txtBarraBusqueda.Name = "txtBarraBusqueda";
            this.txtBarraBusqueda.Size = new System.Drawing.Size(382, 32);
            this.txtBarraBusqueda.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(48, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "PATITAS Y PELOS";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBuscar.Location = new System.Drawing.Point(579, 184);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(77, 32);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.LightSeaGreen;
            this.linkLabel1.Location = new System.Drawing.Point(666, 43);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(68, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Cerrar sesion";
            // 
            // btnCrearUsuario
            // 
            this.btnCrearUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearUsuario.Location = new System.Drawing.Point(191, 38);
            this.btnCrearUsuario.Name = "btnCrearUsuario";
            this.btnCrearUsuario.Size = new System.Drawing.Size(93, 23);
            this.btnCrearUsuario.TabIndex = 7;
            this.btnCrearUsuario.Text = "Sin usuario";
            this.btnCrearUsuario.UseVisualStyleBackColor = true;
            this.btnCrearUsuario.Click += new System.EventHandler(this.btnCrearUsuario_Click);
            // 
            // lblUsuarioNoEncontrado
            // 
            this.lblUsuarioNoEncontrado.AutoSize = true;
            this.lblUsuarioNoEncontrado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(24)))), ((int)(((byte)(61)))));
            this.lblUsuarioNoEncontrado.Location = new System.Drawing.Point(229, 219);
            this.lblUsuarioNoEncontrado.Name = "lblUsuarioNoEncontrado";
            this.lblUsuarioNoEncontrado.Size = new System.Drawing.Size(308, 13);
            this.lblUsuarioNoEncontrado.TabIndex = 14;
            this.lblUsuarioNoEncontrado.Text = "Usuario no encontraco, por favor verifique los datos ingresados.";
            this.lblUsuarioNoEncontrado.Visible = false;
            // 
            // btnUsuarioNuevo
            // 
<<<<<<< HEAD:Clave1_Grupo1/Form2.Designer.cs
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(304, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Crear usuario";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCrearUsuario_Click);
=======
            this.btnUsuarioNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarioNuevo.Location = new System.Drawing.Point(304, 38);
            this.btnUsuarioNuevo.Name = "btnUsuarioNuevo";
            this.btnUsuarioNuevo.Size = new System.Drawing.Size(114, 23);
            this.btnUsuarioNuevo.TabIndex = 15;
            this.btnUsuarioNuevo.Text = "Usuario Nuevo";
            this.btnUsuarioNuevo.UseVisualStyleBackColor = true;
            this.btnUsuarioNuevo.Click += new System.EventHandler(this.btnUsuarioNuevo_Click);
>>>>>>> CodigoClases:Clave1_Grupo1/Formularios/Form2.Designer.cs
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(161, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 32);
            this.label3.TabIndex = 16;
            this.label3.Text = "🔍";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Filtros:";
            // 
            // rdbTodos
            // 
            this.rdbTodos.AutoSize = true;
            this.rdbTodos.Location = new System.Drawing.Point(232, 259);
            this.rdbTodos.Name = "rdbTodos";
            this.rdbTodos.Size = new System.Drawing.Size(55, 17);
            this.rdbTodos.TabIndex = 18;
            this.rdbTodos.TabStop = true;
            this.rdbTodos.Text = "Todos";
            this.rdbTodos.UseVisualStyleBackColor = true;
            this.rdbTodos.CheckedChanged += new System.EventHandler(this.rdbTodos_CheckedChanged);
            // 
            // rdbIdCliente
            // 
            this.rdbIdCliente.AutoSize = true;
            this.rdbIdCliente.Location = new System.Drawing.Point(305, 259);
            this.rdbIdCliente.Name = "rdbIdCliente";
            this.rdbIdCliente.Size = new System.Drawing.Size(113, 17);
            this.rdbIdCliente.TabIndex = 19;
            this.rdbIdCliente.TabStop = true;
            this.rdbIdCliente.Text = "Numero de cuenta";
            this.rdbIdCliente.UseVisualStyleBackColor = true;
            this.rdbIdCliente.CheckedChanged += new System.EventHandler(this.rdbIdCliente_CheckedChanged);
            // 
            // rdbTelefono
            // 
            this.rdbTelefono.AutoSize = true;
            this.rdbTelefono.Location = new System.Drawing.Point(438, 259);
            this.rdbTelefono.Name = "rdbTelefono";
            this.rdbTelefono.Size = new System.Drawing.Size(67, 17);
            this.rdbTelefono.TabIndex = 20;
            this.rdbTelefono.TabStop = true;
            this.rdbTelefono.Text = "Teléfono";
            this.rdbTelefono.UseVisualStyleBackColor = true;
            this.rdbTelefono.CheckedChanged += new System.EventHandler(this.rdbTelefono_CheckedChanged);
            // 
            // rdbCorreoElectronico
            // 
            this.rdbCorreoElectronico.AutoSize = true;
            this.rdbCorreoElectronico.Location = new System.Drawing.Point(522, 259);
            this.rdbCorreoElectronico.Name = "rdbCorreoElectronico";
            this.rdbCorreoElectronico.Size = new System.Drawing.Size(111, 17);
            this.rdbCorreoElectronico.TabIndex = 21;
            this.rdbCorreoElectronico.TabStop = true;
            this.rdbCorreoElectronico.Text = "Correo electronico";
            this.rdbCorreoElectronico.UseVisualStyleBackColor = true;
            this.rdbCorreoElectronico.CheckedChanged += new System.EventHandler(this.rdbCorreoElectronico_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rdbCorreoElectronico);
            this.Controls.Add(this.rdbTelefono);
            this.Controls.Add(this.rdbIdCliente);
            this.Controls.Add(this.rdbTodos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUsuarioNuevo);
            this.Controls.Add(this.lblUsuarioNoEncontrado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBarraBusqueda);
            this.Name = "Form2";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBarraBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnCrearUsuario;
        private System.Windows.Forms.Label lblUsuarioNoEncontrado;
        private System.Windows.Forms.Button btnUsuarioNuevo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbTodos;
        private System.Windows.Forms.RadioButton rdbIdCliente;
        private System.Windows.Forms.RadioButton rdbTelefono;
        private System.Windows.Forms.RadioButton rdbCorreoElectronico;
    }
}