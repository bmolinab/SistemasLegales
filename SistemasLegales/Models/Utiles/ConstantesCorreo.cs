using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Models.Utiles
{
    public static class ConstantesCorreo
    {
        public static string MensajeCorreoSuperior;
        public static string DominioCorreo { get; set; }


        public static string CabeceraNotificacion { get; set; }

        public static string CuerpoNotificacion
        { get; set; }
        public static string UrlNotificacion
        { get; set; }

        public static string FooterNotificacion { get; set; }

        public static string MensajeCREATE { get; set; }
        public static string MensajeTERMINADO { get; set; }
        public static string MensajeFINALIZADO { get; set; }
        public static string MensajeAUTOMATICO { get; set; }
        public static string MensajeNOACEPTADO { get; set; }
        public static string MensajeCADUCAR { get; set; }
    }
}
