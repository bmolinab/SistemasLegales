using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Proceso
    {
        public Proceso()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdProceso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
    }
}
