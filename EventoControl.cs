using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NoriSDK
{
    [Table(Name = "eventos_controles")]
    public class EventoControl
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        [Column]
        public string formulario { get; set; }
        [Column]
        public string control { get; set; }
        [Column]
        public string control_destino { get; set; }
        [Column]
        public string evento { get; set; }
        [Column]
        public string query { get; set; }
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

        public static Table<EventoControl> EventosControles()
        {
            return Nori.CrearContexto().GetTable<EventoControl>();
        }

        public EventoControl()
        {
            evento = "Click";
            activo = true;
            usuario_creacion_id = Global.Usuario.id;
            fecha_creacion = DateTime.Now;
            usuario_actualizacion_id = Global.Usuario.id;
            fecha_actualizacion = DateTime.Now;
        }

        public static EventoControl Obtener(int id)
        {
            try
            {
                return EventosControles().Where(x => x.id == id).First();
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return new EventoControl();
            }
        }

        public bool Agregar()
        {
            try
            {
                var Tabla = EventosControles();
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
                var Tabla = EventosControles();
                EventoControl evento_control = Tabla.Where(x => x.id == id).First();
                this.CopyProperties(evento_control);
                Tabla.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.Error = new Nori.Error(ex.Message);
                return false;
            }
        }

        public static void SuscribirEventos(Form f)
        {
            List<EventoControl> eventos = EventosControles().Where(x => x.formulario == f.Name && x.activo == true).ToList();

            foreach (EventoControl evento in eventos)
            {
                Control c = f.Controls.Find(evento.control, true)[0];
                Control d  = f.Controls.Find(evento.control_destino, true)[0];
                switch (evento.evento)
                {
                    case "Click":
                        c.Click += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                    case "DoubleClick":
                        c.DoubleClick += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                    case "TextChanged":
                        c.TextChanged += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                    case "Enter":
                        c.Enter += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                    case "GotFocus":
                        c.GotFocus += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                    case "LostFocus":
                        c.LostFocus += (object sender, EventArgs e) => { EjecutarEvento(d, f, evento.id); };
                        break;
                }
            }
        }

        private static void EjecutarEvento(Control c, Form f, int id)
        {
            object p = new { tpo = 1, fields = "funcion" };
            object o = new { id = id };
            DataTable eventos = Utilidades.Busqueda("eventos_controles", o, p);

            EventoControl evento = Obtener(id);
            string query = evento.query;

            var regex = new Regex("{.*?}");
            var matches = regex.Matches(query);

            foreach (var match in matches)
            {
                string m = match.ToString();
                Control control = f.Controls.Find(m.Trim('{', '}'), true)[0];
                string valor = control.Text;
                query = query.Replace(m, valor);
            }

            string result = Utilidades.EjecutarEscalarString(query);

            c.Text = result;
        }
    }
}