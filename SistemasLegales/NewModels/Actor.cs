using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Actor
    {
        public Actor()
        {
            RequisitoIdActorCustodioDocumentoNavigation = new HashSet<Requisito>();
            RequisitoIdActorDuennoProcesoNavigation = new HashSet<Requisito>();
            RequisitoIdActorResponsableGestSegNavigation = new HashSet<Requisito>();
        }

        public int IdActor { get; set; }
        public string Nombres { get; set; }
        public string Departamento { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Requisito> RequisitoIdActorCustodioDocumentoNavigation { get; set; }
        public virtual ICollection<Requisito> RequisitoIdActorDuennoProcesoNavigation { get; set; }
        public virtual ICollection<Requisito> RequisitoIdActorResponsableGestSegNavigation { get; set; }
    }
}
