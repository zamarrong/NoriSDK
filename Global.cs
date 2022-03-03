using System;

namespace NoriSDK
{
    internal static class Global
    {
        internal static string CadenaConexion = string.Empty;
        internal static Configuracion Configuracion;
        internal static Estacion Estacion;
        internal static Usuario Usuario;
        internal static string Sesion = string.Empty;
        internal static Exception Error = new Exception("Error desconocido");
    }
}