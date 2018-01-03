using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{
    public partial class cuadro : UserControl
    {
        
        public enum Estado {LIBRE,OCUPADO}
        
        public Estado estado;
        public int posX;
        public int posY;
        public int idBarco;

        public cuadro()
        {
            InitializeComponent();
        }
    }
}
