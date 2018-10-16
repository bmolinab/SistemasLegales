using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdCiudad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
    }
}
