using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{
    public partial class ConfigMaquina : Form
    {
        public ConfigMaquina()
        {
            InitializeComponent();
        }

        //Evento encargado de mostrar/ocultar el campo IP en el caso del servidor
        private void cmbx_cs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((string)cmbx_cs.SelectedItem)=="Servidor")
            {
                //groupbox que muestra solo el texbox para ingresar el puerto del servidor
                gbx_ip.Visible = false;
            }
            else
            {
                //groupbox que muestra solo el texbox para ingresar el puerto e ip servidor
                gbx_ip.Visible = true;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cierra el Form
            this.Close();
        }

        private void ConfigMaquina_Load(object sender, EventArgs e)
        {
            //A;adir items al combobox
            cmbx_cs.Items.Add("Servidor");
            cmbx_cs.Items.Add("Cliente");
            cmbx_cs.SelectedIndex = 0;
        }

        private void txtPuerto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //SINO ES NUMERO NI TECLA PARA BORRAR advierte con un mensaje
            if (!char.IsNumber(e.KeyChar) && e.KeyChar!=(char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                MessageBox.Show("Solo puede ingresar numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        
        private void txt_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //SINO ES NUMERO NI PUNTO DECIMAN NI TECLA PARA BORRAR advierte con un mensaje
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != 46 && e.KeyChar!=(char)Keys.Enter)
            {
                MessageBox.Show("Solo puede ingresar numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;                
            }                
        }
    }
}
