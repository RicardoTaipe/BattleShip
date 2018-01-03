namespace battleship
{
    partial class frm_PantallaPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msJugar = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPantallaMensajes = new System.Windows.Forms.TextBox();
            this.bgw_Escuchar = new System.ComponentModel.BackgroundWorker();
            this.bgw_Enviar = new System.ComponentModel.BackgroundWorker();
            this.btnListo = new System.Windows.Forms.Button();
            this.pbx_barco5 = new System.Windows.Forms.PictureBox();
            this.pbx_barco2b = new System.Windows.Forms.PictureBox();
            this.pbx_barco4 = new System.Windows.Forms.PictureBox();
            this.pbx_barco2a = new System.Windows.Forms.PictureBox();
            this.pbx_barco3 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco2b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco2a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msJugar,
            this.salirToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // msJugar
            // 
            this.msJugar.Image = global::battleship.Properties.Resources.play1;
            this.msJugar.Name = "msJugar";
            this.msJugar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.msJugar.Size = new System.Drawing.Size(152, 22);
            this.msJugar.Text = "Jugar";
            this.msJugar.Click += new System.EventHandler(this.msJugar_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = global::battleship.Properties.Resources.exit;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "Exit";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPantallaMensajes);
            this.groupBox1.Location = new System.Drawing.Point(16, 481);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 130);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mensajes";
            // 
            // txtPantallaMensajes
            // 
            this.txtPantallaMensajes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPantallaMensajes.Enabled = false;
            this.txtPantallaMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPantallaMensajes.Location = new System.Drawing.Point(6, 19);
            this.txtPantallaMensajes.Multiline = true;
            this.txtPantallaMensajes.Name = "txtPantallaMensajes";
            this.txtPantallaMensajes.ReadOnly = true;
            this.txtPantallaMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPantallaMensajes.Size = new System.Drawing.Size(741, 105);
            this.txtPantallaMensajes.TabIndex = 0;
            // 
            // bgw_Escuchar
            // 
            this.bgw_Escuchar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Escuchar_DoWork);
            // 
            // bgw_Enviar
            // 
            this.bgw_Enviar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Enviar_DoWork_1);
            // 
            // btnListo
            // 
            this.btnListo.BackgroundImage = global::battleship.Properties.Resources.boton_play;
            this.btnListo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnListo.Location = new System.Drawing.Point(12, 216);
            this.btnListo.Name = "btnListo";
            this.btnListo.Size = new System.Drawing.Size(161, 97);
            this.btnListo.TabIndex = 11;
            this.btnListo.UseVisualStyleBackColor = true;
            this.btnListo.Click += new System.EventHandler(this.btnListo_Click);
            // 
            // pbx_barco5
            // 
            this.pbx_barco5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_barco5.Image = global::battleship.Properties.Resources._5c;
            this.pbx_barco5.Location = new System.Drawing.Point(12, 54);
            this.pbx_barco5.Name = "pbx_barco5";
            this.pbx_barco5.Size = new System.Drawing.Size(190, 30);
            this.pbx_barco5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_barco5.TabIndex = 2;
            this.pbx_barco5.TabStop = false;
            // 
            // pbx_barco2b
            // 
            this.pbx_barco2b.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_barco2b.Image = global::battleship.Properties.Resources._2c2;
            this.pbx_barco2b.Location = new System.Drawing.Point(103, 162);
            this.pbx_barco2b.Name = "pbx_barco2b";
            this.pbx_barco2b.Size = new System.Drawing.Size(70, 30);
            this.pbx_barco2b.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_barco2b.TabIndex = 6;
            this.pbx_barco2b.TabStop = false;
            // 
            // pbx_barco4
            // 
            this.pbx_barco4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_barco4.Image = global::battleship.Properties.Resources._4c;
            this.pbx_barco4.Location = new System.Drawing.Point(12, 90);
            this.pbx_barco4.Name = "pbx_barco4";
            this.pbx_barco4.Size = new System.Drawing.Size(150, 30);
            this.pbx_barco4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_barco4.TabIndex = 3;
            this.pbx_barco4.TabStop = false;
            // 
            // pbx_barco2a
            // 
            this.pbx_barco2a.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_barco2a.Image = global::battleship.Properties.Resources._2c2;
            this.pbx_barco2a.Location = new System.Drawing.Point(12, 162);
            this.pbx_barco2a.Name = "pbx_barco2a";
            this.pbx_barco2a.Size = new System.Drawing.Size(70, 30);
            this.pbx_barco2a.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_barco2a.TabIndex = 5;
            this.pbx_barco2a.TabStop = false;
            // 
            // pbx_barco3
            // 
            this.pbx_barco3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_barco3.Image = global::battleship.Properties.Resources._3c;
            this.pbx_barco3.Location = new System.Drawing.Point(12, 126);
            this.pbx_barco3.Name = "pbx_barco3";
            this.pbx_barco3.Size = new System.Drawing.Size(110, 30);
            this.pbx_barco3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_barco3.TabIndex = 4;
            this.pbx_barco3.TabStop = false;
            // 
            // frm_PantallaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(792, 623);
            this.Controls.Add(this.btnListo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbx_barco5);
            this.Controls.Add(this.pbx_barco2b);
            this.Controls.Add(this.pbx_barco4);
            this.Controls.Add(this.pbx_barco2a);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pbx_barco3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_PantallaPrincipal";
            this.Text = "BattleShip";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco2b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco2a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_barco3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbx_barco5;
        private System.Windows.Forms.PictureBox pbx_barco4;
        private System.Windows.Forms.PictureBox pbx_barco3;
        private System.Windows.Forms.PictureBox pbx_barco2a;
        private System.Windows.Forms.PictureBox pbx_barco2b;
        private System.Windows.Forms.ToolStripMenuItem msJugar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPantallaMensajes;
        private System.Windows.Forms.Button btnListo;
        private System.ComponentModel.BackgroundWorker bgw_Escuchar;
        private System.ComponentModel.BackgroundWorker bgw_Enviar;
    }
}

