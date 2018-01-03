using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PaqueteSerializado
{
    [Serializable]
    public class Mensaje
    {

        public enum Tipo { LISTO1, OK1, LISTO2, OK2, ATAQUE, RESULTADO, FIN, DEFAULT };

        
        private int ataqueX;
        private int ataqueY;
        private Tipo tipoMensaje;
        private String descripcion;
        private bool miTurno;

        public Tipo TipoMensaje
        {
            get
            {
                return tipoMensaje;
            }

            set
            {
                tipoMensaje = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public int AtaqueX
        {
            get
            {
                return ataqueX;
            }

            set
            {
                ataqueX = value;
            }
        }

        public int AtaqueY
        {
            get
            {
                return ataqueY;
            }

            set
            {
                ataqueY = value;
            }
        }

        public bool MiTurno { get => miTurno; set => miTurno = value; }

        public Mensaje()
        {

        }
        //Constructor para los mensajes OK1,OK2,LISTO1,LISTO2,FIN,FALLO
        public Mensaje(Tipo tipoMensaje, string descripcion)
        {
            this.TipoMensaje = tipoMensaje;
            this.Descripcion = descripcion;
        }
        //Constructor para el mensaje ATAQUE
        public Mensaje(Tipo tipoMensaje, int ataqueX, int ataqueY, bool miTurno)
        {
            this.TipoMensaje = tipoMensaje;
            this.AtaqueX = ataqueX;
            this.AtaqueY = ataqueY;
            this.miTurno = miTurno;
        }
        //Constructor para el mensaje RESULTADO
        public Mensaje(Tipo tipoMensaje, int ataqueX, int ataqueY, string descripcion)
        {
            this.TipoMensaje = tipoMensaje;
            this.AtaqueX = ataqueX;
            this.AtaqueY = ataqueY;
            this.Descripcion = descripcion;
        }

        public Mensaje(Tipo tipoMensaje, string descripcion, bool miTurno)
        {
            this.tipoMensaje = tipoMensaje;
            this.descripcion = descripcion;
            this.miTurno = miTurno;
        }

        //Metodos estaticos que crear un tipo de mensaje OK1,OK2,LISTO1,LISTO2,FIN,FALLO
        public static Mensaje crearMensaje(Tipo tipoMensaje, string descripcion,bool turno)
        {
            Mensaje mensaje = new Mensaje(tipoMensaje, descripcion,turno);
            return mensaje;
        }
        //Metodos estaticos que crear un tipo de mensaje ATAQUE
        public static Mensaje crearMensaje(Tipo tipoMensaje, int ataqueX, int ataqueY,bool turno)
        {
            Mensaje mensaje = new Mensaje(tipoMensaje, ataqueX, ataqueY,turno);
            return mensaje;
        }
        //Metodos estaticos que crear un tipo de mensaje RESULTADO
        public static Mensaje crearMensaje(Tipo tipoMensaje, int ataqueX, int ataqueY, string descripcion)
        {
            Mensaje mensaje = new Mensaje(tipoMensaje, ataqueX, ataqueY, descripcion);
            return mensaje;
        }


        //Metodo sobrescrito para imprimir los atributos del objeto mensaje
        public override string ToString()
        {
            string respuesta = "";

            switch (tipoMensaje)
            {
                case Tipo.LISTO1:
                case Tipo.OK1:
                case Tipo.LISTO2:
                case Tipo.OK2:
                case Tipo.RESULTADO:
                case Tipo.FIN:
                    respuesta = tipoMensaje + " " + descripcion;
                    break;
                case Tipo.ATAQUE:
                    respuesta = tipoMensaje + " X=" + ataqueX+" Y="+AtaqueY;
                    break;
            }
            return respuesta;
        }
    }
}
