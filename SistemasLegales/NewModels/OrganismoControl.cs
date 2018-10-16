using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class OrganismoControl
    {
        public OrganismoControl()
        {
            RequisitoLegal = new HashSet<RequisitoLegal>();
        }

        public int IdOrganismoControl { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<RequisitoLegal> RequisitoLegal { get; set; }
    }
}
