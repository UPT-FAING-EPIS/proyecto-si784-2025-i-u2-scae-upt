namespace SCAE_UPT.Presentacion
{
    partial class frmRegistroVisitante
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRetroceder = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnRegistrarVisitante = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.rbtnCarnetExtranjeria = new System.Windows.Forms.RadioButton();
            this.rbtnDNI = new System.Windows.Forms.RadioButton();
            this.dgvHistorialRegistroVisitante = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialRegistroVisitante)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnRetroceder);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 130);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "REGISTRO VISITANTE";
            // 
            // btnRetroceder
            // 
            this.btnRetroceder.Location = new System.Drawing.Point(33, 54);
            this.btnRetroceder.Name = "btnRetroceder";
            this.btnRetroceder.Size = new System.Drawing.Size(75, 23);
            this.btnRetroceder.TabIndex = 0;
            this.btnRetroceder.Text = "Retroceder";
            this.btnRetroceder.UseVisualStyleBackColor = true;
            this.btnRetroceder.Click += new System.EventHandler(this.btnRetroceder_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtApellido);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.btnRegistrarVisitante);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtDocumento);
            this.panel2.Controls.Add(this.rbtnCarnetExtranjeria);
            this.panel2.Controls.Add(this.rbtnDNI);
            this.panel2.Controls.Add(this.dgvHistorialRegistroVisitante);
            this.panel2.Location = new System.Drawing.Point(12, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(941, 471);
            this.panel2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Apellido:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(253, 73);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(188, 20);
            this.txtApellido.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(54, 73);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(193, 20);
            this.txtNombre.TabIndex = 6;
            // 
            // btnRegistrarVisitante
            // 
            this.btnRegistrarVisitante.Location = new System.Drawing.Point(539, 21);
            this.btnRegistrarVisitante.Name = "btnRegistrarVisitante";
            this.btnRegistrarVisitante.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrarVisitante.TabIndex = 5;
            this.btnRegistrarVisitante.Text = "Registrar";
            this.btnRegistrarVisitante.UseVisualStyleBackColor = true;
            this.btnRegistrarVisitante.Click += new System.EventHandler(this.btnRegistrarVisitante_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Documento:";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(316, 23);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(125, 20);
            this.txtDocumento.TabIndex = 3;
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            this.txtDocumento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocumento_KeyUp);
            // 
            // rbtnCarnetExtranjeria
            // 
            this.rbtnCarnetExtranjeria.AutoSize = true;
            this.rbtnCarnetExtranjeria.Location = new System.Drawing.Point(104, 23);
            this.rbtnCarnetExtranjeria.Name = "rbtnCarnetExtranjeria";
            this.rbtnCarnetExtranjeria.Size = new System.Drawing.Size(108, 17);
            this.rbtnCarnetExtranjeria.TabIndex = 2;
            this.rbtnCarnetExtranjeria.TabStop = true;
            this.rbtnCarnetExtranjeria.Text = "Carnet Extranjeria";
            this.rbtnCarnetExtranjeria.UseVisualStyleBackColor = true;
            this.rbtnCarnetExtranjeria.CheckedChanged += new System.EventHandler(this.rbtnCarnetExtranjeria_CheckedChanged);
            // 
            // rbtnDNI
            // 
            this.rbtnDNI.AutoSize = true;
            this.rbtnDNI.Location = new System.Drawing.Point(33, 23);
            this.rbtnDNI.Name = "rbtnDNI";
            this.rbtnDNI.Size = new System.Drawing.Size(44, 17);
            this.rbtnDNI.TabIndex = 1;
            this.rbtnDNI.TabStop = true;
            this.rbtnDNI.Text = "DNI";
            this.rbtnDNI.UseVisualStyleBackColor = true;
            this.rbtnDNI.CheckedChanged += new System.EventHandler(this.rbtnDNI_CheckedChanged);
            // 
            // dgvHistorialRegistroVisitante
            // 
            this.dgvHistorialRegistroVisitante.AllowUserToAddRows = false;
            this.dgvHistorialRegistroVisitante.AllowUserToDeleteRows = false;
            this.dgvHistorialRegistroVisitante.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialRegistroVisitante.Location = new System.Drawing.Point(54, 112);
            this.dgvHistorialRegistroVisitante.Name = "dgvHistorialRegistroVisitante";
            this.dgvHistorialRegistroVisitante.ReadOnly = true;
            this.dgvHistorialRegistroVisitante.Size = new System.Drawing.Size(843, 338);
            this.dgvHistorialRegistroVisitante.TabIndex = 0;
            // 
            // frmRegistroVisitante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 631);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmRegistroVisitante";
            this.Text = "frmRegistroVisitante";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialRegistroVisitante)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRetroceder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnRegistrarVisitante;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.RadioButton rbtnCarnetExtranjeria;
        private System.Windows.Forms.RadioButton rbtnDNI;
        private System.Windows.Forms.DataGridView dgvHistorialRegistroVisitante;
    }
}