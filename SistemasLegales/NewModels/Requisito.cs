using System;
using System.Collections.Generic;

namespace SistemasLegales.NewModels
{
    public partial class Requisito
    {
        public Requisito()
        {
            Accion = new HashSet<Accion>();
            DocumentoRequisito = new HashSet<DocumentoRequisito>();
        }

        public int IdRequisito { get; set; }
        public int IdDocumento { get; set; }
        public int IdCiudad { get; set; }
        public int IdProceso { get; set; }
        public int IdActorDuennoProceso { get; set; }
        public int IdActorResponsableGestSeg { get; set; }
        public int IdActorCustodioDocumento { get; set; }
        public DateTime? FechaCumplimiento { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public int IdStatus { get; set; }
        public int DuracionTramite { get; set; }
        public int DiasNotificacion { get; set; }
        public string EmailNotificacion1 { get; set; }
        public string EmailNotificacion2 { get; set; }
        public string Observaciones { get; set; }
        public bool NotificacionEnviada { get; set; }
        public int? IdProyecto { get; set; }
        public bool Finalizado { get; set; }
        public int? Criticidad { get; set; }

        public virtual ICollection<Accion> Accion { get; set; }
        public virtual ICollection<DocumentoRequisito> DocumentoRequisito { get; set; }
        public virtual Actor IdActorCustodioDocumentoNavigation { get; set; }
        public virtual Actor IdActorDuennoProcesoNavigation { get; set; }
        public virtual Actor IdActorResponsableGestSegNavigation { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual Documento IdDocumentoNavigation { get; set; }
        public virtual Proceso IdProcesoNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
    }
}
