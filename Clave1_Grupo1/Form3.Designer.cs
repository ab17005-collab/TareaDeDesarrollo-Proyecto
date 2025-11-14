namespace Clave1_Grupo1
{
    partial class Form3
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtEmailContacto = new System.Windows.Forms.TextBox();
            this.txtTelefonoContacto = new System.Windows.Forms.TextBox();
            this.txtDireccionPro = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtApellidoPro = new System.Windows.Forms.TextBox();
            this.lblApellidoPro = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cklistMotivoCita = new System.Windows.Forms.CheckedListBox();
            this.dgvSlotsDisponibles = new System.Windows.Forms.DataGridView();
            this.mcCalendarioAgendarCita = new System.Windows.Forms.MonthCalendar();
            this.txtNombrePro = new System.Windows.Forms.TextBox();
            this.rtxtSintomas = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblNombrePro = new System.Windows.Forms.Label();
            this.btnAgendarCita = new System.Windows.Forms.Button();
            this.btnCancelarCita = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlotsDisponibles)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 10);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1166, 659);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCancelarCita);
            this.tabPage1.Controls.Add(this.btnAgendarCita);
            this.tabPage1.Controls.Add(this.lblNombrePro);
            this.tabPage1.Controls.Add(this.txtEmailContacto);
            this.tabPage1.Controls.Add(this.txtTelefonoContacto);
            this.tabPage1.Controls.Add(this.txtDireccionPro);
            this.tabPage1.Controls.Add(this.lblDireccion);
            this.tabPage1.Controls.Add(this.txtApellidoPro);
            this.tabPage1.Controls.Add(this.lblApellidoPro);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cklistMotivoCita);
            this.tabPage1.Controls.Add(this.dgvSlotsDisponibles);
            this.tabPage1.Controls.Add(this.mcCalendarioAgendarCita);
            this.tabPage1.Controls.Add(this.txtNombrePro);
            this.tabPage1.Controls.Add(this.rtxtSintomas);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lblTelefono);
            this.tabPage1.Controls.Add(this.lblEmail);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1158, 616);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Agendar cita";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // txtEmailContacto
            // 
            this.txtEmailContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailContacto.Location = new System.Drawing.Point(41, 243);
            this.txtEmailContacto.Name = "txtEmailContacto";
            this.txtEmailContacto.ReadOnly = true;
            this.txtEmailContacto.Size = new System.Drawing.Size(360, 22);
            this.txtEmailContacto.TabIndex = 28;
            this.txtEmailContacto.TextChanged += new System.EventHandler(this.txtEmailContacto_TextChanged);
            // 
            // txtTelefonoContacto
            // 
            this.txtTelefonoContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoContacto.Location = new System.Drawing.Point(41, 190);
            this.txtTelefonoContacto.Name = "txtTelefonoContacto";
            this.txtTelefonoContacto.ReadOnly = true;
            this.txtTelefonoContacto.Size = new System.Drawing.Size(360, 22);
            this.txtTelefonoContacto.TabIndex = 27;
            this.txtTelefonoContacto.TextChanged += new System.EventHandler(this.txtTelefonoContacto_TextChanged);
            // 
            // txtDireccionPro
            // 
            this.txtDireccionPro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccionPro.Location = new System.Drawing.Point(41, 137);
            this.txtDireccionPro.Name = "txtDireccionPro";
            this.txtDireccionPro.ReadOnly = true;
            this.txtDireccionPro.Size = new System.Drawing.Size(360, 22);
            this.txtDireccionPro.TabIndex = 26;
            this.txtDireccionPro.TextChanged += new System.EventHandler(this.txtDireccionPro_TextChanged);
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(38, 119);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(181, 15);
            this.lblDireccion.TabIndex = 25;
            this.lblDireccion.Text = "DIRECCION PROPIETARIO:";
            this.lblDireccion.Click += new System.EventHandler(this.lblDireccion_Click);
            // 
            // txtApellidoPro
            // 
            this.txtApellidoPro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoPro.Location = new System.Drawing.Point(41, 84);
            this.txtApellidoPro.Name = "txtApellidoPro";
            this.txtApellidoPro.ReadOnly = true;
            this.txtApellidoPro.Size = new System.Drawing.Size(360, 22);
            this.txtApellidoPro.TabIndex = 24;
            this.txtApellidoPro.TextChanged += new System.EventHandler(this.txtApellidoPro_TextChanged);
            // 
            // lblApellidoPro
            // 
            this.lblApellidoPro.AutoSize = true;
            this.lblApellidoPro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellidoPro.Location = new System.Drawing.Point(38, 66);
            this.lblApellidoPro.Name = "lblApellidoPro";
            this.lblApellidoPro.Size = new System.Drawing.Size(172, 15);
            this.lblApellidoPro.TabIndex = 23;
            this.lblApellidoPro.Text = "APELLIDO PROPIETARIO:";
            this.lblApellidoPro.Click += new System.EventHandler(this.lblApellidoPro_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(43, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "MOTIVO DE LA CITA:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cklistMotivoCita
            // 
            this.cklistMotivoCita.CheckOnClick = true;
            this.cklistMotivoCita.ColumnWidth = 170;
            this.cklistMotivoCita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cklistMotivoCita.FormattingEnabled = true;
            this.cklistMotivoCita.Items.AddRange(new object[] {
            "Esterilización",
            "Vacunación",
            "Cirugía dental",
            "Cirugía Ortopédica",
            "Cirugía General",
            "Dermatología",
            "Medicina general",
            "Otro"});
            this.cklistMotivoCita.Location = new System.Drawing.Point(46, 333);
            this.cklistMotivoCita.MultiColumn = true;
            this.cklistMotivoCita.Name = "cklistMotivoCita";
            this.cklistMotivoCita.Size = new System.Drawing.Size(355, 89);
            this.cklistMotivoCita.TabIndex = 2;
            this.cklistMotivoCita.SelectedIndexChanged += new System.EventHandler(this.cklistMotivoCita_SelectedIndexChanged);
            // 
            // dgvSlotsDisponibles
            // 
            this.dgvSlotsDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSlotsDisponibles.Location = new System.Drawing.Point(711, 66);
            this.dgvSlotsDisponibles.Name = "dgvSlotsDisponibles";
            this.dgvSlotsDisponibles.Size = new System.Drawing.Size(407, 385);
            this.dgvSlotsDisponibles.TabIndex = 18;
            // 
            // mcCalendarioAgendarCita
            // 
            this.mcCalendarioAgendarCita.Location = new System.Drawing.Point(443, 67);
            this.mcCalendarioAgendarCita.Name = "mcCalendarioAgendarCita";
            this.mcCalendarioAgendarCita.TabIndex = 17;
            this.mcCalendarioAgendarCita.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcCalendarioAgendarCita_DateChanged);
            // 
            // txtNombrePro
            // 
            this.txtNombrePro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombrePro.Location = new System.Drawing.Point(41, 33);
            this.txtNombrePro.Name = "txtNombrePro";
            this.txtNombrePro.ReadOnly = true;
            this.txtNombrePro.Size = new System.Drawing.Size(360, 22);
            this.txtNombrePro.TabIndex = 13;
            this.txtNombrePro.TextChanged += new System.EventHandler(this.txtNombrePro_TextChanged);
            // 
            // rtxtSintomas
            // 
            this.rtxtSintomas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtSintomas.Location = new System.Drawing.Point(41, 464);
            this.rtxtSintomas.Name = "rtxtSintomas";
            this.rtxtSintomas.Size = new System.Drawing.Size(360, 128);
            this.rtxtSintomas.TabIndex = 12;
            this.rtxtSintomas.Text = "";
            this.rtxtSintomas.TextChanged += new System.EventHandler(this.rtxtSintomas_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(38, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "SINTOMAS: ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(38, 172);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(159, 15);
            this.lblTelefono.TabIndex = 9;
            this.lblTelefono.Text = "TELEFONO CONTACTO:";
            this.lblTelefono.Click += new System.EventHandler(this.lblTelefono_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(38, 225);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(128, 15);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "EMAIL CONTACTO:";
            this.lblEmail.Click += new System.EventHandler(this.lblEmail_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1158, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ver citas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(787, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "Citas canceladas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(420, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Citas pasadas";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Proximas citas";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(790, 142);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(312, 407);
            this.dataGridView3.TabIndex = 2;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(423, 142);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(312, 407);
            this.dataGridView2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(56, 142);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(312, 407);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1158, 616);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Usuario";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1158, 616);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Expedientes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblNombrePro
            // 
            this.lblNombrePro.AutoSize = true;
            this.lblNombrePro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombrePro.Location = new System.Drawing.Point(38, 15);
            this.lblNombrePro.Name = "lblNombrePro";
            this.lblNombrePro.Size = new System.Drawing.Size(166, 15);
            this.lblNombrePro.TabIndex = 29;
            this.lblNombrePro.Text = "NOMBRE PROPIETARIO:";
            this.lblNombrePro.Click += new System.EventHandler(this.lblNombrePro_Click);
            // 
            // btnAgendarCita
            // 
            this.btnAgendarCita.Location = new System.Drawing.Point(828, 547);
            this.btnAgendarCita.Name = "btnAgendarCita";
            this.btnAgendarCita.Size = new System.Drawing.Size(290, 32);
            this.btnAgendarCita.TabIndex = 30;
            this.btnAgendarCita.Text = "Agendar Cita";
            this.btnAgendarCita.UseVisualStyleBackColor = true;
            this.btnAgendarCita.Click += new System.EventHandler(this.btnAgendarCita_Click);
            // 
            // btnCancelarCita
            // 
            this.btnCancelarCita.Location = new System.Drawing.Point(517, 547);
            this.btnCancelarCita.Name = "btnCancelarCita";
            this.btnCancelarCita.Size = new System.Drawing.Size(290, 32);
            this.btnCancelarCita.TabIndex = 31;
            this.btnCancelarCita.Text = "Cancelar";
            this.btnCancelarCita.UseVisualStyleBackColor = true;
            this.btnCancelarCita.Click += new System.EventHandler(this.btnCancelarCita_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 687);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form3";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlotsDisponibles)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.RichTextBox rtxtSintomas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombrePro;
        private System.Windows.Forms.MonthCalendar mcCalendarioAgendarCita;
        private System.Windows.Forms.DataGridView dgvSlotsDisponibles;
        private System.Windows.Forms.CheckedListBox cklistMotivoCita;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblApellidoPro;
        private System.Windows.Forms.TextBox txtApellidoPro;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccionPro;
        private System.Windows.Forms.TextBox txtTelefonoContacto;
        private System.Windows.Forms.TextBox txtEmailContacto;
        private System.Windows.Forms.Label lblNombrePro;
        private System.Windows.Forms.Button btnAgendarCita;
        private System.Windows.Forms.Button btnCancelarCita;
    }
}