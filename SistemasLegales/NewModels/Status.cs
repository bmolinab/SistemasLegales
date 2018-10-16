using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Status
    {
        public Status()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdStatus { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
    }
}
