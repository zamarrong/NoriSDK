using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;

namespace NoriSDK
{
    [Table(Name = "basculas")]
    public class Bascula
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string nombre { get; set; }
        [Column]
        public string puerto { get; set; }
        [Column]
        public int baud_rate { get; set; }
        [Column]
        public int stop_bits { get; set; }
        [Column]
        public int data_bits { get; set; }
        [Column]
        public string comando { get; set; }
        [Column]
        public bool activo { get; set; }
        [Column]
        public int usuario_creacion_id { get; set; }
        [Column]
        public DateTime fecha_creacion { get; set; }
        [Column]
        public int usuario_actualizacion_id { get; set; }
        [Column]
        public DateTime fecha_actualizacion { get; set; }
        //Uso interno
        private SerialPort puerto_serie { get; set; }
        public string datos { get; set; }

        public static Table<Bascula> Basculas()
        {
            return Nori.CrearContexto().GetTable<Bascula>();
        }

        public Bascula()
        {
            baud_rate = 9600;
            stop_bits = 1;
            data_bits = 8;
            comando = "P";
            activo = true;
            usuario_creacion_id = 1;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = 1;
            fecha_actualizacion = DateTime.Now;
        }

        public static Bascula Obtener(int id)
        {
            try
            {
                return Basculas().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new Bascula();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = Basculas();
                Tabla.InsertOnSubmit(this);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Actualizar()
        {
            try
            {
                usuario_actualizacion_id = Global.Usuario.id;
                fecha_actualizacion = DateTime.Now;
                var Tabla = Basculas();
                Bascula estacion = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(estacion);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public decimal ObtenerPeso()
        {
            try
            {
                var watch = Stopwatch.StartNew();
                datos = string.Empty;
                EscribirComando(comando);
                while (datos.IsNullOrEmpty() && watch.ElapsedMilliseconds < (10000)){}
                watch.Stop();
                return decimal.Parse(RemoveExtraText(datos));
            }
            catch
            {
                return 0;
            }
        }

        private string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890.";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }

        public bool Inicializar()
        {
            try
            {
                puerto_serie = new SerialPort(puerto, baud_rate);

                if (!puerto_serie.IsOpen)
                {
                    puerto_serie.Parity = Parity.None;
                    puerto_serie.StopBits = (StopBits)stop_bits;
                    puerto_serie.DataBits = data_bits;
                    puerto_serie.Handshake = Handshake.None;
                    puerto_serie.ReadTimeout = 4800;

                    puerto_serie.DataReceived += new SerialDataReceivedEventHandler(LeerBascula);
                    puerto_serie.ErrorReceived += new SerialErrorReceivedEventHandler(Error);

                    puerto_serie.Open();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                if (puerto_serie.IsOpen)
                    puerto_serie.Close();

                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        private void Error (object sender, SerialErrorReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialError.Frame:
                    Global.Error = new Nori.Error("Error de trama");
                    break;
                case SerialError.Overrun:
                    Global.Error = new Nori.Error("Saturación de buffer");
                    break;
                case SerialError.RXOver:
                    Global.Error = new Nori.Error("Desbordamiento de buffer de entrada");
                    break;
                case SerialError.RXParity:
                    Global.Error = new Nori.Error("Error de paridad");
                    break;
                case SerialError.TXFull:
                    Global.Error = new Nori.Error("Buffer lleno");
                    break;

            }
            Global.Error = new Nori.Error("Error desconocido en báscula");
        }

        private void LeerBascula(object sender, SerialDataReceivedEventArgs e)
        {
            datos = puerto_serie.ReadExisting();
        }

        private void EscribirComando(string comando)
        {
            //byte[] inBuffer = new byte[] { 80 };
            //puerto_serie.Write(inBuffer, 0, inBuffer.Length);
            puerto_serie.Write(comando);
        }
    }
}
