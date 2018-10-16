using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Documento
    {
        public Documento()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdDocumento { get; set; }
        public string Nombre { get; set; }
        public int IdRequisitoLegal { get; set; }
        public int? Cantidad { get; set; }
        public int? Tipo { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
        public virtual RequisitoLegal IdRequisitoLegalNavigation { get; set; }
    }
}
