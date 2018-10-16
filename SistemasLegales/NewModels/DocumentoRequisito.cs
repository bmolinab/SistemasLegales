using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class DocumentoRequisito
    {
        public int IdDocumentoRequisito { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Url { get; set; }
        public int? IdRequisito { get; set; }

        public virtual Requisito IdRequisitoNavigation { get; set; }
    }
}
