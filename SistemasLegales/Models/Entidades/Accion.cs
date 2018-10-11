using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Models.Entidades
{
    public partial class Accion
    {
        public int IdAccion { get; set; }
        public string Detalle { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdRequisito { get; set; }

        public virtual Requisito IdRequisitoNavigation { get; set; }
    }
}
