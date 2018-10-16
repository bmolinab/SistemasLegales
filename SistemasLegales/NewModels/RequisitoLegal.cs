using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class RequisitoLegal
    {
        public RequisitoLegal()
        {
            Documento = new HashSet<Documento>();
        }

        public int IdRequisitoLegal { get; set; }
        public string Nombre { get; set; }
        public int IdOrganismoControl { get; set; }

        public virtual ICollection<Documento> Documento { get; set; }
        public virtual OrganismoControl IdOrganismoControlNavigation { get; set; }
    }
}
