namespace battleship
{
    partial class ConfigMaquina
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
            this.cmbx_cs = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.gbx_ip = new System.Windows.Forms.GroupBox();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.gbx_puerto = new System.Windows.Forms.GroupBox();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.gbx_ip.SuspendLayout();
            this.gbx_puerto.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbx_cs
            // 
            this.cmbx_cs.DisplayMember = "Servidor";
            this.cmbx_cs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbx_cs.FormattingEnabled = true;
            this.cmbx_cs.Location = new System.Drawing.Point(33, 29);
            this.cmbx_cs.Name = "cmbx_cs";
            this.cmbx_cs.Size = new System.Drawing.Size(200, 21);
            this.cmbx_cs.TabIndex = 18;
            this.cmbx_cs.ValueMember = "Servidor";
            this.cmbx_cs.SelectedIndexChanged += new System.EventHandler(this.cmbx_cs_SelectedIndexChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(152, 184);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPlay.Location = new System.Drawing.Point(39, 184);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 16;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // gbx_ip
            // 
            this.gbx_ip.Controls.Add(this.txt_IP);
            this.gbx_ip.Location = new System.Drawing.Point(33, 114);
            this.gbx_ip.Name = "gbx_ip";
            this.gbx_ip.Size = new System.Drawing.Size(200, 47);
            this.gbx_ip.TabIndex = 15;
            this.gbx_ip.TabStop = false;
            this.gbx_ip.Text = "Direccion IP";
            this.gbx_ip.Visible = false;
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(6, 18);
            this.txt_IP.MaxLength = 15;
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(188, 20);
            this.txt_IP.TabIndex = 8;
            this.txt_IP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_IP_KeyPress);
            // 
            // gbx_puerto
            // 
            this.gbx_puerto.Controls.Add(this.txtPuerto);
            this.gbx_puerto.Location = new System.Drawing.Point(33, 56);
            this.gbx_puerto.Name = "gbx_puerto";
            this.gbx_puerto.Size = new System.Drawing.Size(200, 52);
            this.gbx_puerto.TabIndex = 14;
            this.gbx_puerto.TabStop = false;
            this.gbx_puerto.Text = "Puerto";
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(6, 19);
            this.txtPuerto.MaxLength = 5;
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Size = new System.Drawing.Size(188, 20);
            this.txtPuerto.TabIndex = 8;
            this.txtPuerto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuerto_KeyPress);
            // 
            // ConfigMaquina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 215);
            this.Controls.Add(this.cmbx_cs);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.gbx_ip);
            this.Controls.Add(this.gbx_puerto);
            this.Name = "ConfigMaquina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfigMaquina";
            this.Load += new System.EventHandler(this.ConfigMaquina_Load);
            this.gbx_ip.ResumeLayout(false);
            this.gbx_ip.PerformLayout();
            this.gbx_puerto.ResumeLayout(false);
            this.gbx_puerto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbx_ip;
        private System.Windows.Forms.GroupBox gbx_puerto;
        public System.Windows.Forms.Button btnPlay;
        public System.Windows.Forms.TextBox txt_IP;
        public System.Windows.Forms.TextBox txtPuerto;
        public System.Windows.Forms.ComboBox cmbx_cs;
    }
}