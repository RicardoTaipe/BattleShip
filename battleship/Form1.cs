using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//A;adir la referencia al mensaje serializado
using PaqueteSerializado;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace battleship
{
    public partial class frm_PantallaPrincipal : Form
    {
        
        //Controles de usuario para graficar los tableros remoto y local
        cuadro[,] tablerolocal = new cuadro[10, 10];
        cuadro[,] tableroremoto = new cuadro[10, 10];
        //Dimensiones para el tablero
        const int ANCHO = 10;//pixeles
        const int ALTO = 10;
        //Margen de separacion entre el barco y el cuadro del tablero
        const int PADDING_X= 5;//pixeles
        const int PADDING_Y = 5;

        //Punto de referencia para arrastrar los barcos sobre el tablero local
        private Point firstPoint = new Point();
        private Point lastPoint = new Point();

        private TcpClient client;
        public Mensaje receive;
        public Mensaje mensajetoSend;

        string ipServer = "";
        int puertoServer;
        string modo = "";
        bool miTurno;
        bool miTurnoServidor;
        bool miTurnoCliente;

        public frm_PantallaPrincipal()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Hilo Manejador de eventos para mover y rotar los barcos
            Thread hilo = new Thread(init);
            hilo.Start();
            //Graficar los tableros
            //Metodo que grafica los tableros con la poscion inicial en X e Y como argumentos
            habilitarBarcosyPlay(false);
            graficarTablero("local", 300, 40);
            graficarTablero("remoto", 800, 40);            
        }
        //habilita/deshabilita los barcos y el boton listo
        private void habilitarBarcosyPlay(bool v)
        {
            pbx_barco5.Enabled = v;
            pbx_barco4.Enabled = v;
            pbx_barco3.Enabled = v;
            pbx_barco2a.Enabled = v;
            pbx_barco2b.Enabled = v;
            btnListo.Enabled = v;
        }

        //Selecciona si actua como cliente o servidor
        private void msJugar_Click(object sender, EventArgs e)
        {
            ConfigMaquina config = new ConfigMaquina();
            DialogResult resultado = config.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                //Obtiene el valor del combobox si es cliente o servidor
                modo = ((string)config.cmbx_cs.SelectedItem);
                switch (modo)
                {
                    case "Servidor":
                        if (config.txtPuerto.Text != "")
                        {                            
                            puertoServer = Convert.ToInt16(config.txtPuerto.Text);
                            habilitarBarcosyPlay(true);
                            TcpListener listener = new TcpListener(IPAddress.Any, puertoServer);
                            listener.Start();
                            txtPantallaMensajes.AppendText("Esperando por Cliente\n");
                            client = listener.AcceptTcpClient();                            
                            bgw_Escuchar.RunWorkerAsync();
                            bgw_Enviar.WorkerSupportsCancellation = true;
                            miTurno=true;
                            miTurnoServidor = true;
                        }
                        break;
                    case "Cliente":
                        if (config.txtPuerto.Text != "" && config.txt_IP.Text != "")
                        {
                            ipServer = config.txt_IP.Text;
                            puertoServer = Convert.ToInt16(config.txtPuerto.Text);
                            habilitarBarcosyPlay(true);
                            miTurno = false;
                            miTurnoCliente = false;
                            client = new TcpClient();
                            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(ipServer),puertoServer);
                            try
                            {
                                client.Connect(IpEnd);
                                if (client.Connected)
                                {
                                    txtPantallaMensajes.AppendText("Establecio conexion con el servidor\n");
                                    bgw_Escuchar.RunWorkerAsync();
                                    bgw_Enviar.WorkerSupportsCancellation = true;
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message.ToString());
                            }
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un modo \"Cliente o Servidor para iniciar el juego\"", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        
        //Escucha de mensaje
        private void bgw_Escuchar_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    byte[] bytesReceived = new byte[1024];
                    client.Client.Receive(bytesReceived);
                    //Convertir los bytes del objeto serializado en un objeto Mensaje
                    receive = DeserializarObjeto(bytesReceived);
                    //Analisis de Mensajes
                    if (receive.TipoMensaje == Mensaje.Tipo.LISTO1) {                        
                        miTurnoCliente = true;
                        miTurnoServidor = true;
                        mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.OK1, "", true);
                        bgw_Enviar.RunWorkerAsync();
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.LISTO2)
                    {                        
                        miTurnoServidor = true;
                        miTurnoCliente = false;
                        mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.OK2, "", true);
                        bgw_Enviar.RunWorkerAsync();
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.ATAQUE && modo=="Servidor")
                    {                     
                        if (tablerolocal[receive.AtaqueY,receive.AtaqueX].estado==cuadro.Estado.OCUPADO)
                        {
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].BackColor = Color.Red;
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].BackColor = Color.Red;
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].idBarco = 0;
                            mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.RESULTADO,receive.AtaqueX,receive.AtaqueY,"ACERTO");
                            miTurnoServidor = false;
                            bgw_Enviar.RunWorkerAsync();
                        }
                        else
                        {
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].BackColor = Color.White;
                            mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.RESULTADO, receive.AtaqueX, receive.AtaqueY, "FALLO");
                            bgw_Enviar.RunWorkerAsync();
                            miTurnoServidor = true;
                        }
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.ATAQUE && modo == "Cliente")
                    {
                        if (tablerolocal[receive.AtaqueY, receive.AtaqueX].estado == cuadro.Estado.OCUPADO)
                        {
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].BackColor = Color.Red;
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].idBarco = 0;
                            mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.RESULTADO, receive.AtaqueX, receive.AtaqueY, "ACERTO");                            
                            bgw_Enviar.RunWorkerAsync();
                            miTurnoCliente = false;
                        }
                        else
                        {
                            tablerolocal[receive.AtaqueY, receive.AtaqueX].BackColor = Color.White;
                            mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.RESULTADO, receive.AtaqueX, receive.AtaqueY, "FALLO");
                            miTurnoCliente = true;
                            bgw_Enviar.RunWorkerAsync();
                        }
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.RESULTADO && modo=="Servidor")
                    {
                        if (receive.Descripcion=="ACERTO")
                        {
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].BackColor = Color.Red;
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].estado = cuadro.Estado.OCUPADO;
                            miTurnoServidor = true;
                        }
                        else
                        {
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].BackColor = Color.White;
                        }
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.RESULTADO && modo=="Cliente")
                    {
                        if (receive.Descripcion == "ACERTO")
                        {
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].BackColor = Color.Red;
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].estado =cuadro.Estado.OCUPADO;
                            miTurnoCliente = true;
                        }
                        else
                        {
                            tableroremoto[receive.AtaqueY, receive.AtaqueX].BackColor = Color.White;                            
                        }
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.FIN && modo=="Servidor")
                    {
                        MessageBox.Show(receive.Descripcion);
                        //Reiniciar aplicacion
                        Application.Restart();
                        Environment.Exit(0);
                        break;
                    }
                    if (receive.TipoMensaje == Mensaje.Tipo.FIN && modo=="Cliente")
                    {
                        //Actualizar la pantalla de mensajes                    
                        MessageBox.Show(receive.Descripcion);
                        //Reiniciar Application
                        Application.Restart();
                        Environment.Exit(0);

                        break;
                    }
                    if (receive.TipoMensaje != Mensaje.Tipo.DEFAULT && receive.TipoMensaje != Mensaje.Tipo.FIN)
                    {
                        //Actualizar la pantalla de mensajes                    
                        this.txtPantallaMensajes.Invoke(new MethodInvoker(delegate () {
                            txtPantallaMensajes.AppendText("⇒: " + receive.ToString() + Environment.NewLine);
                        }));
                    }

                    //Verificar si el juego se termina
                    if (juegoTerminado() == true)
                    {
                        MessageBox.Show("Ha ganado Juego Terminado");
                        mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.FIN, "Ha perdido Juego Terminado ", true);
                        bgw_Enviar.RunWorkerAsync();
                    }                    
                    
                    receive.TipoMensaje = Mensaje.Tipo.DEFAULT;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString()+"En metodo de esucha");
                    
                }
            }
        }

        //Enviar
        private void bgw_Enviar_DoWork_1(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                client.Client.Send(SerializarObjeto(mensajetoSend));
                this.txtPantallaMensajes.Invoke(new MethodInvoker(delegate () {
                    txtPantallaMensajes.AppendText("⇐: " + mensajetoSend.ToString() + Environment.NewLine);
                }));
            }
            else
            {
                MessageBox.Show("sending failed");
            }
            bgw_Enviar.CancelAsync();
        }

        //Verificar el juegoTerminado
        private bool juegoTerminado()
        {
            int total = 0;
            for (int j = 0; j < ALTO; j++)
            {
                for (int i = 0; i < ANCHO; i++)
                {
                    if (tableroremoto[j, i].estado == cuadro.Estado.OCUPADO)
                    {
                        total += 1;
                    }
                }
            }
            if (total==16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Serializar el objeto
        private byte[] SerializarObjeto(Mensaje datos)
        {
            byte[] bytestoSend = new byte[1024];
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            Mensaje mensaje = new Mensaje();
            mensaje = datos;
            bf.Serialize(stream, mensaje);
            return bytestoSend = stream.ToArray();
        }
        //Deserializa el objeto que llega
        private Mensaje DeserializarObjeto(byte[] datos)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(datos);
            Mensaje mensaje = (Mensaje)bf.Deserialize(stream);
            return mensaje;
        }

        //Evento encargado de enviar posicion remota cuando se da clic en el tablero remoto
        private void EnviarPosicionRemota(object sender, EventArgs e)
        {
            if (modo=="Cliente" && miTurnoCliente==true)
            {
                int x = ((cuadro)sender).posX;
                int y = ((cuadro)sender).posY;
                mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.ATAQUE, x, y, true);
                miTurnoCliente = false;
                bgw_Enviar.RunWorkerAsync();
            }
            else if (modo=="Servidor" && miTurnoServidor==true)
            {
                int x = ((cuadro)sender).posX;
                int y = ((cuadro)sender).posY;
                mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.ATAQUE, x, y, true);
                miTurnoServidor = false;
                bgw_Enviar.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Todavia no es tu turno","Informacón",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

        //Metodo para graficar el tablero local y remoto
        public void graficarTablero(string tipo,int posicionX,int posicionY)
        {
            if (tipo=="remoto")
            {
                for (int j = 0; j < ALTO; j++)
                {
                    for (int i= 0; i < ANCHO; i++)
                    {
                        tableroremoto[j, i] = new cuadro();
                        tableroremoto[j, i].Parent = this;
                        tableroremoto[j, i].Location = new Point(i * 40 + posicionX, j * 40 + posicionY);
                        tableroremoto[j, i].posX = i;
                        tableroremoto[j, i].posY = j;
                        tableroremoto[j, i].Enabled = false;
                        tableroremoto[j, i].Click += EnviarPosicionRemota;
                        tableroremoto[j, i].estado = cuadro.Estado.LIBRE;
                        tableroremoto[j, i].BackColor = Color.FromArgb(12, 205, 255);
                        tableroremoto[j, i].BackgroundImageLayout = ImageLayout.Center;
                    }
                }
            }
            else if (tipo=="local")
            {
                for (int j = 0; j < ALTO; j++)
                {
                    for (int i = 0; i < ANCHO; i++)
                    {
                        tablerolocal[j, i] = new cuadro();
                        tablerolocal[j, i].Parent = this;
                        tablerolocal[j, i].Location = new Point(i * 40 + posicionX, j * 40 + posicionY);
                        tablerolocal[j, i].posX = i;
                        tablerolocal[j, i].posY = j;
                        tablerolocal[j, i].estado=cuadro.Estado.LIBRE;
                        tablerolocal[j, i].BackColor = Color.FromArgb(12, 205, 255);
                        tablerolocal[j, i].BackgroundImageLayout = ImageLayout.Center;                        
                    }
                }
            }
        }

        //Metodo de Determinar que posicion ocupa el barco en el tablero
        private void AlmacenarPosiciones()
        {
            for (int j = 0; j < ALTO; j++)
            {
                for (int i = 0; i < ANCHO; i++)
                {
                    if (tablerolocal[j, i].Bounds.IntersectsWith(pbx_barco5.Bounds))
                    {
                        tablerolocal[j, i].estado = cuadro.Estado.OCUPADO;
                        tablerolocal[j, i].idBarco = 5;
                    }
                    if (tablerolocal[j, i].Bounds.IntersectsWith(pbx_barco4.Bounds))
                    {
                        tablerolocal[j, i].estado = cuadro.Estado.OCUPADO;
                        tablerolocal[j, i].idBarco = 4;
                    }
                    if (tablerolocal[j, i].Bounds.IntersectsWith(pbx_barco3.Bounds))
                    {
                        tablerolocal[j, i].estado = cuadro.Estado.OCUPADO;
                        tablerolocal[j, i].idBarco = 3;
                    }
                    if (tablerolocal[j, i].Bounds.IntersectsWith(pbx_barco2a.Bounds))
                    {
                        tablerolocal[j, i].estado = cuadro.Estado.OCUPADO;
                        tablerolocal[j, i].idBarco = 2;
                    }
                    if (tablerolocal[j, i].Bounds.IntersectsWith(pbx_barco2b.Bounds))
                    {
                        tablerolocal[j, i].estado = cuadro.Estado.OCUPADO;
                        tablerolocal[j, i].idBarco = 1;
                    }
                    Console.Write(" " + tablerolocal[j, i].idBarco+" "+ tablerolocal[j, i].estado);
                }
                Console.WriteLine();
            }
        }

        //Evento que enviara el mensaje Indicando que el jugador esta listo
        private void btnListo_Click(object sender, EventArgs e)
        {
            //Hilo encargado de determinar que posicion ocupa cada barco en el tablero
            AlmacenarPosiciones();       

            //Metodo encargado de verificar que todos los barcos estan en el tablero
            if (VerificarBarcos()==false)
            {
                MessageBox.Show("Faltan barcos por ubicar","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                if (modo=="Servidor" && miTurnoServidor==true)
                {
                    mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.LISTO1, "Estoy Listo",true);
                    bgw_Enviar.RunWorkerAsync();
                    miTurnoServidor = false;
                    btnListo.Enabled = false;
                    activarTableroRemoto();
                }
                else if (modo=="Cliente" && miTurnoCliente==true)
                {
                    mensajetoSend = Mensaje.crearMensaje(Mensaje.Tipo.LISTO2, "Estoy Listo",true);
                    bgw_Enviar.RunWorkerAsync();
                    miTurnoCliente = false;
                    btnListo.Enabled = false;
                    activarTableroRemoto();
                }
                else
                {
                    MessageBox.Show("Esperar al host ","Informacion",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        //Metodo para ctivar el tablero remoto.
        private void activarTableroRemoto()
        {
            for (int j = 0; j < ALTO; j++)
            {
                for (int i = 0; i < ANCHO; i++)
                {
                    tableroremoto[j, i].Enabled = true;
                }
            }
        }
        //Metodo que verifica que todos los barcos esten en el tablero
        private bool VerificarBarcos()
        {
            int k = 0;
            for (int j = 0; j < ALTO; j++)
            {
                for (int i = 0; i < ANCHO; i++)
                {
                    if (tablerolocal[j, i].idBarco == 0)
                    {
                        k+=1;
                    }
                }
            }
            if (k==84)
            {
                return true;
            } else
            {
                return false;
            }
        }
        //Opcion exit del Menu que cierra por completo la aplicacion
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //mETODO ENCARGADO DE INICIAR LOS EVENTOS PARA LOS BARCOS
        public void init()
        {   /////////////////////////////////////////////////////////////////////////////////////////////  
            //Evento encargado de Rotar el barco de 5 espacios
            pbx_barco5.MouseDown += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left) { firstPoint = Control.MousePosition; }
                if(ee.Button == MouseButtons.Right)
                {
                    //Se almacena la imagen temporalmente se la gira y se la asigna denuevo al picturebox
                    Image img = pbx_barco5.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbx_barco5.Image = img;
                }
            };
            //Evento encargado de mover el barco de 5 espacios
            pbx_barco5.MouseMove += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
                    pbx_barco5.Location = new Point(pbx_barco5.Location.X - res.X, pbx_barco5.Location.Y - res.Y);
                    firstPoint = temp;
                    lastPoint = pbx_barco5.Location;
                }
            };
            //Evento encargado de setear el barco dentro de las cuadriculas
            // sino se regresa a la posicion inicial
            pbx_barco5.MouseUp +=(ss,ee)=>{                
                if (ee.Button==MouseButtons.Left)
                {
                    Point temp = lastPoint;
                    bool barcoColocado = false;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (tablerolocal[j, i].Bounds.Contains(temp))
                            {
                                pbx_barco5.Location = new Point(tablerolocal[j, i].Location.X+PADDING_X, tablerolocal[j, i].Location.Y + PADDING_Y);
                                barcoColocado = true;
                                goto Verificar;
                            }
                        }
                    }
                    Verificar:
                    if (!barcoColocado)
                    {
                        pbx_barco5.Location = Properties.Settings.Default.puntoInicial5;
                    }
                }
            };
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //Evento encargado de Rotar el barco de 4 espacios
            pbx_barco4.MouseDown += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left) { firstPoint = Control.MousePosition; }
                if (ee.Button == MouseButtons.Right)
                {
                    Image img = pbx_barco4.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbx_barco4.Image = img;
                }
            };
            //Evento encargado de mover el barco de 4 espacios
            pbx_barco4.MouseMove += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
                    pbx_barco4.Location = new Point(pbx_barco4.Location.X - res.X, pbx_barco4.Location.Y - res.Y);
                    firstPoint = temp;
                    lastPoint = pbx_barco4.Location;
                }
            };
            //Evento encargado de setear el barco dentro de las cuadriculas
            // sino se regresa a la posicion inicial
            pbx_barco4.MouseUp += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = lastPoint;
                    bool barcoColocado = false;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (tablerolocal[j, i].Bounds.Contains(temp))
                            {
                                pbx_barco4.Location = new Point(tablerolocal[j, i].Location.X + PADDING_X, tablerolocal[j, i].Location.Y + PADDING_Y);
                                barcoColocado = true;
                                goto Verificar;
                            }
                        }
                    }
                    Verificar:
                    if (!barcoColocado)
                    {
                        pbx_barco4.Location = Properties.Settings.Default.puntoInicial4;
                    }
                }
            };
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Evento encargado de Rotar el barco de 3 espacios
            pbx_barco3.MouseDown += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left) { firstPoint = Control.MousePosition; }
                if (ee.Button == MouseButtons.Right)
                {
                    Image img = pbx_barco3.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbx_barco3.Image = img;
                }
            };
            //Evento encargado de mover el barco de 3 espacios
            pbx_barco3.MouseMove += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
                    pbx_barco3.Location = new Point(pbx_barco3.Location.X - res.X, pbx_barco3.Location.Y - res.Y);
                    firstPoint = temp;
                    lastPoint = pbx_barco3.Location;
                }
            };
            //Evento encargado de setear el barco dentro de las cuadriculas
            // sino se regresa a la posicion inicial
            pbx_barco3.MouseUp += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = lastPoint;
                    bool barcoColocado = false;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (tablerolocal[j, i].Bounds.Contains(temp))
                            {
                                pbx_barco3.Location = new Point(tablerolocal[j, i].Location.X + PADDING_X, tablerolocal[j, i].Location.Y + PADDING_Y);
                                barcoColocado = true;
                                goto Verificar;
                            }
                        }
                    }
                    Verificar:
                    if (!barcoColocado)
                    {
                        pbx_barco3.Location = Properties.Settings.Default.puntoInicial3;
                    }
                }
            };
            ////////////////////////////////////////////////////////////////////////////////////////////
            //Evento encargado de Rotar el barco de 2a espacios
            pbx_barco2a.MouseDown += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left) { firstPoint = Control.MousePosition; }
                if (ee.Button == MouseButtons.Right)
                {
                    Image img = pbx_barco2a.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbx_barco2a.Image = img;
                }
            };
            //Evento encargado de mover el barco de 2a espacios
            pbx_barco2a.MouseMove += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
                    pbx_barco2a.Location = new Point(pbx_barco2a.Location.X - res.X, pbx_barco2a.Location.Y - res.Y);
                    firstPoint = temp;
                    lastPoint = pbx_barco2a.Location;
                }
            };
            
            //Evento encargado de setear el barco dentro de las cuadriculas
            // sino se regresa a la posicion inicial
            pbx_barco2a.MouseUp += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = lastPoint;
                    bool barcoColocado = false;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (tablerolocal[j, i].Bounds.Contains(temp))
                            {
                                pbx_barco2a.Location = new Point(tablerolocal[j, i].Location.X + PADDING_X, tablerolocal[j, i].Location.Y + PADDING_Y);
                                barcoColocado = true;
                                goto Verificar;
                            }
                        }
                    }
                    Verificar:
                    if (!barcoColocado)
                    {
                        pbx_barco2a.Location = Properties.Settings.Default.puntoInicial2a;
                      }
                }
            };
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Evento encargado de Rotar el barco de 2b espacios
            pbx_barco2b.MouseDown += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left) { firstPoint = Control.MousePosition; }
                if (ee.Button == MouseButtons.Right)
                {
                    Image img = pbx_barco2b.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pbx_barco2b.Image = img;
                }
            };
            //Evento encargado de mover el barco de 2b espacios
            pbx_barco2b.MouseMove += (ss, ee) => {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
                    pbx_barco2b.Location = new Point(pbx_barco2b.Location.X - res.X, pbx_barco2b.Location.Y - res.Y);
                    firstPoint = temp;
                    lastPoint = pbx_barco2b.Location;
                }
            };

            //Evento encargado de setear el barco dentro de las cuadriculas
            // sino se regresa a la posicion inicial
            pbx_barco2b.MouseUp += (ss, ee) => {
                if (ee.Button == MouseButtons.Left)
                {
                    Point temp = lastPoint;
                    bool barcoColocado = false;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (tablerolocal[j, i].Bounds.Contains(temp))
                            {
                                pbx_barco2b.Location = new Point(tablerolocal[j, i].Location.X + PADDING_X, tablerolocal[j, i].Location.Y + PADDING_Y);
                                barcoColocado = true;
                                goto Verificar;
                            }
                        }
                    }
                    Verificar:
                    if (!barcoColocado)
                    {
                        pbx_barco2b.Location = Properties.Settings.Default.puntoInicial2b;
                    }
                }
            };
        }
    }
}
