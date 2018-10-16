using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
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
