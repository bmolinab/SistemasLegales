﻿using SistemasLegales.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemasLegales.Models.Utiles;

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

        [Display(Name = "Fecha de cumplimiento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCumplimiento { get; set; }

        [Display(Name = "Fecha Exigible")]
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

        public bool Finalizado { get; set; }

        public int? Criticidad { get; set; }


        [NotMapped]
        [Display(Name = "Año")]
        public int? Anno { get; set; }

        [NotMapped]
        public bool SemaforoVerde { get; set; }

        [NotMapped]
        public bool SemaforoAmarillo { get; set; }

        [NotMapped]
        public bool SemaforoRojo { get; set; }

        [NotMapped]
        public int IdStatusAnterior { get; set; }

        public virtual ICollection<Accion> Accion { get; set; }
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

        public async Task<bool> EnviarEmailNotificaionRequisitoFinalizado(int idRequisito, IEmailSender emailSender, SistemasLegalesContext db)
        {
            try
            {
                var requisito = await db.Requisito
                                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                                        .Include(c => c.Ciudad)
                                        .Include(c => c.Proceso)
                                        .Include(c => c.ActorDuennoProceso)
                                        .Include(c => c.ActorResponsableGestSeg)
                                        .Include(c => c.ActorCustodioDocumento)
                                        .Include(c => c.Status)
                                        .FirstOrDefaultAsync(c => c.IdRequisito == idRequisito);


                if (requisito!=null)
                {

                var listadoEmails = new List<string>();
               
                listadoEmails.Add(requisito.ActorResponsableGestSeg.Email);
                    var FechaCumplimiento = requisito.FechaCumplimiento != null ? requisito.FechaCumplimiento?.ToString("dd/MM/yyyy") : "No Definido";
                    var FechaCaducidad = requisito.FechaCaducidad != null ? requisito.FechaCaducidad?.ToString("dd/MM/yyyy") : "No Definido";
                    await emailSender.SendEmailAsync(listadoEmails, "Notificación de requisito finalizado.",
                       $@"Se le informa que el requisito a finalizado en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}
                            Organismo de control: {requisito.Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}.
                            Requisito legal: {requisito.Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}.
                            Documento: {requisito.Documento.Nombre}, {System.Environment.NewLine}.
                            Ciudad: {requisito.Ciudad.Nombre}, {System.Environment.NewLine}.
                            Proceso: {requisito.Proceso.Nombre}, {System.Environment.NewLine}.
                            Fecha de cumplimiento: {FechaCumplimiento}, {System.Environment.NewLine}.
                            Fecha de exigible: {FechaCaducidad}, {System.Environment.NewLine}.
                            Status: {requisito.Status.Nombre}, {System.Environment.NewLine}.
                            Observaciones: {requisito.Observaciones}, {System.Environment.NewLine}.
                        ");
                }
                return true;
            }
            catch (Exception)
            { }
            return false;
        }
        public async Task<bool> EnviarEmailNotificaionRequisitoTerminado(UserManager<ApplicationUser> userManager,string url,int idRequisito,IEmailSender emailSender, SistemasLegalesContext db)
        {
            try
            {
                var listaAdministradores=await userManager.GetUsersInRoleAsync(Perfiles.Administrador);

                var requisito = await db.Requisito
                                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                                        .Include(c => c.Ciudad)
                                        .Include(c => c.Proceso)
                                        .Include(c => c.ActorDuennoProceso)
                                        .Include(c => c.ActorResponsableGestSeg)
                                        .Include(c => c.ActorCustodioDocumento)
                                        .Include(c => c.Status)
                                        .FirstOrDefaultAsync(c => c.IdRequisito == idRequisito);


                var listadoEmails = new List<string>();
                foreach (var item in listaAdministradores)
                {
                    listadoEmails.Add(item.Email);
                }
                if (listaAdministradores.Count>0)
                {

                    var FechaCumplimiento = requisito.FechaCumplimiento != null ? requisito.FechaCumplimiento?.ToString("dd/MM/yyyy") : "No Definido";
                    var FechaCaducidad = requisito.FechaCaducidad != null ? requisito.FechaCaducidad?.ToString("dd/MM/yyyy") : "No Definido";

                    await emailSender.SendEmailAsync(listadoEmails, "Notificación de requisito terminado.",
                       $@"Se le informa que un requisito a terminado en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}
                            Organismo de control: {requisito.Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}.
                            Requisito legal: {requisito.Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}.
                            Documento: {requisito.Documento.Nombre}, {System.Environment.NewLine}.
                            Ciudad: {requisito.Ciudad.Nombre}, {System.Environment.NewLine}.
                            Proceso: {requisito.Proceso.Nombre}, {System.Environment.NewLine}.
                            Fecha de cumplimiento: {FechaCumplimiento}, {System.Environment.NewLine}.
                            Fecha de exigible: {FechaCaducidad}, {System.Environment.NewLine}.
                            Status: {requisito.Status.Nombre}, {System.Environment.NewLine}.
                            Observaciones: {requisito.Observaciones}, {System.Environment.NewLine}.
                            Para acceder al requisito haga clic aquí: {url}, {System.Environment.NewLine}.
                        ");
                }
            return true;
            }
            catch (Exception)
            { }
            return false;
        }


        public async Task<bool> EnviarEmailNotificaionNoFinalizado(string url, int idRequisito, IEmailSender emailSender, SistemasLegalesContext db)
        {
            try
            {
                var requisito = await db.Requisito
                                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                                        .Include(c => c.Ciudad)
                                        .Include(c => c.Proceso)
                                        .Include(c => c.ActorDuennoProceso)
                                        .Include(c => c.ActorResponsableGestSeg)
                                        .Include(c => c.ActorCustodioDocumento)
                                        .Include(c => c.Status)
                                        .FirstOrDefaultAsync(c => c.IdRequisito == idRequisito);
                if (requisito != null)
                {

                    var listadoEmails = new List<string>();

                    listadoEmails.Add(requisito.ActorResponsableGestSeg.Email);
                    var FechaCumplimiento = requisito.FechaCumplimiento != null ? requisito.FechaCumplimiento?.ToString("dd/MM/yyyy") : "No Definido";
                    var FechaCaducidad = requisito.FechaCaducidad != null ? requisito.FechaCaducidad?.ToString("dd/MM/yyyy") : "No Definido";
                    await emailSender.SendEmailAsync(listadoEmails, "Notificación de requisito.",
                       $@"Se le informa que un requisito no ha sido aprobado su finalización en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}
                            Organismo de control: {requisito.Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}.
                            Requisito legal: {requisito.Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}.
                            Documento: {requisito.Documento.Nombre}, {System.Environment.NewLine}.
                            Ciudad: {requisito.Ciudad.Nombre}, {System.Environment.NewLine}.
                            Proceso: {requisito.Proceso.Nombre}, {System.Environment.NewLine}.
                            Fecha de cumplimiento: {FechaCumplimiento}, {System.Environment.NewLine}.
                            Fecha de exigible : {FechaCaducidad}, {System.Environment.NewLine}.
                            Status: {requisito.Status.Nombre}, {System.Environment.NewLine}.
                            Observaciones: {requisito.Observaciones}, {System.Environment.NewLine}.
                            Para acceder al requisito haga clic aquí: {url}, {System.Environment.NewLine}.
                        ");
                }
                return true;
            }
            catch (Exception)
            { }
            return false;
        }

        public async Task<bool> EnviarEmailNotificaion(UserManager<ApplicationUser> userManager, IEmailSender emailSender, SistemasLegalesContext db)
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

                        var FechaCumplimiento = requisito.FechaCumplimiento != null ? requisito.FechaCumplimiento?.ToString("dd/MM/yyyy") : "No Definido";
                        var FechaCaducidad = requisito.FechaCaducidad != null ? requisito.FechaCaducidad?.ToString("dd/MM/yyyy") : "No Definido";


                        await emailSender.SendEmailAsync(listadoEmails, "Notificación de caducidad de requisito.",
                        $@"Se le informa que está a punto de caducar un requisito en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}{System.Environment.NewLine}
                            Organismo de control: {requisito.Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Requisito legal: {requisito.Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Documento: {requisito.Documento.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Ciudad: {requisito.Ciudad.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Proceso: {requisito.Proceso.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine}
                            Fecha de cumplimiento: {FechaCumplimiento}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Fecha de exigible: {FechaCaducidad}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Status: {requisito.Status.Nombre}, {System.Environment.NewLine}{System.Environment.NewLine},
                            Observaciones: {requisito.Observaciones}, {System.Environment.NewLine}{System.Environment.NewLine}
                        ");

                        NotificacionEnviada = true;
                        await db.SaveChangesAsync();
                        return true;
                    }
                }

                if (semaforo == 3)
                {
                        var requisito = await db.Requisito
                            .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                            .Include(c => c.Ciudad)
                            .Include(c => c.Proceso)
                            .Include(c => c.ActorDuennoProceso)
                            .Include(c => c.ActorResponsableGestSeg)
                            .Include(c => c.ActorCustodioDocumento)
                            .Include(c => c.Status)
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


                        var listaAdministradores = await userManager.GetUsersInRoleAsync(Perfiles.Administrador);

                        foreach (var item in listaAdministradores)
                        {
                            listadoEmails.Add(item.Email);
                        }

                        var FechaCumplimiento = requisito.FechaCumplimiento != null ? requisito.FechaCumplimiento?.ToString("dd/MM/yyyy") : "No Definido";
                        var FechaCaducidad = requisito.FechaCaducidad != null ? requisito.FechaCaducidad?.ToString("dd/MM/yyyy") : "No Definido";

                        await emailSender.SendEmailAsync(listadoEmails, "Notificación de caducidad de requisito.",
                        $@"Se le informa que está a punto de caducar un requisito en la aplicación Sistemas Legales con los datos siguientes: {System.Environment.NewLine}
                            Organismo de control: {requisito.Documento.RequisitoLegal.OrganismoControl.Nombre}, {System.Environment.NewLine}
                            Requisito legal: {requisito.Documento.RequisitoLegal.Nombre}, {System.Environment.NewLine}
                            Documento: {requisito.Documento.Nombre}, {System.Environment.NewLine}
                            Ciudad: {requisito.Ciudad.Nombre}, {System.Environment.NewLine}
                            Proceso: {requisito.Proceso.Nombre}, {System.Environment.NewLine}
                            Fecha de cumplimiento: {FechaCumplimiento}, {System.Environment.NewLine},
                            Fecha de exigible: {FechaCaducidad}, {System.Environment.NewLine},
                            Status: {requisito.Status.Nombre}, {System.Environment.NewLine},
                            Observaciones: {requisito.Observaciones}, {System.Environment.NewLine}
                        ");

                        NotificacionEnviada = true;
                        await db.SaveChangesAsync();
                        return true;
                }
            }
            catch (Exception)
            { }
            return false;
        }
    }
}
