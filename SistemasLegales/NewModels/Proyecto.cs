using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
    }
}
