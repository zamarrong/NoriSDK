using System.Linq;

namespace NoriSDK
{
    public class PuntoVenta
    {
        public static char EstadoCaja(int usuario_id = 0)
        {
            try
            {
                if (usuario_id == 0)
                    usuario_id = Global.Usuario.id;
                
                int ultima_apertura = Flujo.Flujos().Where(x => x.codigo == "INACA" && x.usuario_creacion_id == usuario_id).OrderByDescending(x => x.id).Select(x => new { x.id }).First().id;

                int ultimo_cierre = 0;
                try { ultimo_cierre = Flujo.Flujos().Where(x => x.codigo == "RECCA" && x.usuario_creacion_id == usuario_id).OrderByDescending(x => x.id).Select(x => new { x.id }).First().id; } catch { }

                if (ultima_apertura > ultimo_cierre)
                    return 'A';
                else
                    return 'C';
            }
            catch
            {
                return 'C';
            }
        }

        public static Flujo FondoInicial()
        {
            try
            {
                return Flujo.Flujos().Where(x => x.codigo == "INACA" && x.usuario_creacion_id == Global.Usuario.id).OrderByDescending(x => x.id).First();
            }
            catch
            {
                return null;
            }
        }

        public static bool FondoInicialRetirado(int fondo_inicial_id)
        {
            try
            {
                return Flujo.Flujos().Any(x => x.codigo == "REACA" && x.usuario_creacion_id == Global.Usuario.id && x.id > fondo_inicial_id);
            }
            catch
            {
                return false;
            }
        }
    }
}
