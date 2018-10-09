using SistemasLegales.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SistemasLegales.Models.Entidades
{
    public partial class Requisito : IValidatableObject
    {
        public Requisito()
        {
            DocumentoRequisito = new HashSet<DocumentoRequisito>();
        }

        public int IdRequisito { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdDocumento { get; set; }
        public virtual Documento Documento { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "Debe seleccionar la {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar la {0}.")]
        public int IdCiudad { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        [Display(Name = "Proceso")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdProceso { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Proyecto")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int? IdProyecto { get; set; }
        public virtual Proyecto Proyecto { get; set; }

        [Display(Name = "Dueño del proceso")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdActorDuennoProceso { get; set; }
        public virtual Actor ActorDuennoProceso { get; set; }

        [Display(Name = "Responsable de gest y seg")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdActorResponsableGestSeg { get; set; }
        public virtual Actor ActorResponsableGestSeg { get; set; }

        [Display(Name = "Custodio de documento")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdActorCustodioDocumento { get; set; }
        public virtual Actor ActorCustodioDocumento { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}")]
        [Display(Name = "Fecha de cumplimiento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCumplimiento { get; set; }

        [Display(Name = "Fecha de caducidad")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCaducidad { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdStatus { get; set; }
        public virtual Status Status { get; set; }

        [Display(Name = "Duración del trámite (Días)")]
        [Required(ErrorMessage = "Debe introducir la {0}.")]
        public int DuracionTramite { get; set; }

        [Display(Name = "Nro. días para notificación")]
        [Required(ErrorMessage = "Debe introducir el {0}.")]
        public int DiasNotificacion { get; set; }
        
        [EmailAddress(ErrorMessage = "El {0} es inválido.")]
        [Display(Name = "Correo notificación 1")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        public string EmailNotificacion1 { get; set; }
        
        [EmailAddress(ErrorMessage = "El {0} es inválido.")]
        [Display(Name = "Correo notificación 2")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        public string EmailNotificacion2 { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Las {0} no pueden tener más de {1} y menos de {2} caracteres.")]
        public string Observaciones { get; set; }

        [Display(Name = "Notificación enviada")]
        public bool NotificacionEnviada { get; set; }

        [NotMapped]
        [Display(Name = "Año")]
        public int? Anno { get; set; }

        [NotMapped]
        public bool SemaforoVerde { get; set; }

        [NotMapped]
        public bool SemaforoAmarillo { get; set; }

        [NotMapped]
        public bool SemaforoRojo { get; set; }

        public virtual ICollection<DocumentoRequisito> DocumentoRequisito { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var requisito = (Requisito)validationContext.ObjectInstance;
            if (requisito.EmailNotificacion1 == requisito.EmailNotificacion2)
                yield return new ValidationResult($"El Correo notificación 2 no puede ser igual al Correo notificación 1.", new[] { "EmailNotificacion2" });

            if (requisito.FechaCaducidad != null)
            {
                if (requisito.FechaCumplimiento > requisito.FechaCaducidad)
                    yield return new ValidationResult($"La Fecha de cumplimiento no puede ser mayor que la Fecha de caducidad.", new[] { "FechaCumplimiento" });
            }
            yield return ValidationResult.Success;
        }

        /// <summary>
        /// Retorna el semáforo para el requisito en la forma 1. Verde, 2. Amarillo, 3.Rojo
        /// </summary>
        /// <returns></returns>
        public int ObtenerSemaforo()
        {
            int myNegInt = System.Math.Abs(DiasNotificacion) * (-1);
            if (FechaCaducidad == null)
                return 1;

            var fechaInicioNotificacion = FechaCaducidad.Value.AddDays(myNegInt);
            var fechaCaducidad = new DateTime(FechaCaducidad.Value.Year, FechaCaducidad.Value.Month, FechaCaducidad.Value.Day, 23, 59, 59);
            return DateTime.Now < fechaInicioNotificacion ? 1 : (DateTime.Now >= fechaInicioNotificacion && DateTime.Now <= fechaCaducidad) ? 2 : 3;
        }
        
        public async Task<bool> EnviarEmailNotificaion(IEmailSender emailSender, SistemasLegalesContext db)
        {
            try
            {
                var semaforo = ObtenerSemaforo();
                if (semaforo == 2)
                {
                    if (!NotificacionEnviada)
                    {
                        var requisito = await db.Requisito
                            .Include(c => c.Documento).ThenInclude(c=> c.RequisitoLegal.OrganismoControl)
                            .Include(c => c.Ciudad)
                            .Include(c => c.Proceso)
                            .Include(c=> c.ActorDuennoProceso)
                            .Include(c=> c.ActorResponsableGestSeg)
                            .Include(c=> c.ActorCustodioDocumento)
                            .Include(c=> c.Status)
                            .FirstOrDefaultAsync(c => c.IdRequisito == IdRequisito);

                        var listadoEmails = new List<string>()
                        {
                            ActorDuennoProceso.Email,
                            ActorResponsableGestSeg.Email,
                            ActorCustodioDocumento.Email
                        };

                        if (!String.IsNullOrEmpty(EmailNotificacion1))
                            listadoEmails.Add(EmailNotificacion1);

                        if (!String.IsNullOrEmpty(EmailNotificacion2))
                            listadoEmails.Add(EmailNotificacion2);

                        await emailSender.SendEmailAsync(listadoEmails, "Notificación de caducidad de requisito.",
                        $@"Se le informa que está a punto de caducar un requisito en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}{System.Environment.NewLine}
                            Organismo de control: {Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Requisito legal: {Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Documento: {Documento.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Ciudad: {Ciudad.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Proceso: {Proceso.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Fecha de cumplimiento: {FechaCumplimiento.ToString("dd/MM/yyyy")}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Fecha de caducidad: {FechaCaducidad.Value.ToString("dd/MM/yyyy")}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Status: {Status.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Observaciones: {Observaciones}, {System.Environment.NewLine}{System.Environment.NewLine}
                        ");

                        NotificacionEnviada = true;
                        await db.SaveChangesAsync();
                        return true;
                    }
                }
            }
            catch (Exception)
            { }
            return false;
        }
    }
}
