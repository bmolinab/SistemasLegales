using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Extensores;
using SistemasLegales.Models.Utiles;
using SistemasLegales.Services;

namespace SistemasLegales.Controllers
{
    [Authorize]
    public class RequisitoController : Controller
    {
        private readonly SistemasLegalesContext db;
        private readonly IEmailSender emailSender;
        private readonly IUploadFileService uploadFileService;

        public RequisitoController(SistemasLegalesContext context, IEmailSender emailSender, IUploadFileService uploadFileService)
        {
            db = context;
            this.emailSender = emailSender;
            this.uploadFileService = uploadFileService;
        }

        private async Task<List<Requisito>> ListarRequisitos()
        {
            return await db.Requisito
                    .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                    .Include(c => c.Documento)
                    .Include(c => c.Ciudad)
                    .Include(c => c.Proceso)
                    .Include(c=> c.Proyecto)
                    .OrderBy(c => c.IdDocumento).ThenBy(c=> c.Documento.IdRequisitoLegal).ThenBy(c=> c.Documento.RequisitoLegal.IdOrganismoControl).ThenBy(c => c.IdCiudad).ThenBy(c => c.IdProceso).ThenBy(c=> c.IdProyecto).ToListAsync();
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Index()
        {
            var lista = new List<Requisito>();
            try
            {
                var listadoOrganismoControl = await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync();
                listadoOrganismoControl.Insert(0, new OrganismoControl { IdOrganismoControl = -1, Nombre = "Todos" });
                ViewData["OrganismoControl"] = new SelectList(listadoOrganismoControl, "IdOrganismoControl", "Nombre");

                var listadoActores = await db.Actor.OrderBy(c => c.Nombres).ToListAsync();
                listadoActores.Insert(0, new Actor { IdActor = -1, Nombres = "Todos" });
                ViewData["Actor"] = new SelectList(listadoActores, "IdActor", "Nombres");

                var listadoProyectos = await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync();
                listadoProyectos.Insert(0, new Proyecto { IdProyecto = -1, Nombre = "Todos" });
                ViewData["Proyecto"] = new SelectList(listadoProyectos, "IdProyecto", "Nombre");


            }
            catch (Exception)
            {
                TempData["Mensaje"] = $"{Mensaje.Error}|{Mensaje.ErrorListado}";
            }
            return View(lista);
        }

        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Gestionar(int? id)
        {
            try
            {
                ViewBag.accion = id == null ? "Crear" : "Editar";
                ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");

                if (id != null)
                {
                    var requisito = await db.Requisito.Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl).FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre", requisito.Documento.RequisitoLegal.IdOrganismoControl);
                    ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                    ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                    return View(requisito);
                }
                ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal((ViewData["OrganismoControl"] as SelectList).FirstOrDefault() != null ? int.Parse((ViewData["OrganismoControl"] as SelectList).FirstOrDefault().Value) : -1);
                ViewData["Documento"] = await ObtenerSelectListDocumento((ViewData["RequisitoLegal"] as SelectList).FirstOrDefault() != null ? int.Parse((ViewData["RequisitoLegal"] as SelectList).FirstOrDefault().Value) : -1);
                return View();
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Gestionar(Requisito requisito, IFormFile file)
        {
            try
            {
                Requisito miRequisito = new Requisito();
                ViewBag.accion = requisito.IdRequisito == 0 ? "Crear" : "Editar";var tt = Request.Form;
                ModelState.Remove("Documento.Nombre");
                ModelState.Remove("Documento.RequisitoLegal.Nombre");
                if (ModelState.IsValid)
                {
                    if (requisito.IdRequisito == 0)
                    {

                         miRequisito = new Requisito
                        {
                            IdDocumento = requisito.IdDocumento,
                            IdCiudad = requisito.IdCiudad,
                            IdProceso = requisito.IdProceso,
                            IdProyecto = requisito.IdProyecto,
                            IdActorDuennoProceso = requisito.IdActorDuennoProceso,
                            IdActorResponsableGestSeg = requisito.IdActorResponsableGestSeg,
                            IdActorCustodioDocumento = requisito.IdActorCustodioDocumento,
                            FechaCumplimiento = requisito.FechaCumplimiento,
                            FechaCaducidad = requisito.FechaCaducidad,
                            IdStatus = requisito.IdStatus,
                            DuracionTramite = requisito.DuracionTramite,
                            DiasNotificacion = requisito.DiasNotificacion,
                            EmailNotificacion1 = requisito.EmailNotificacion1,
                            EmailNotificacion2 = requisito.EmailNotificacion2,
                            Observaciones = requisito.Observaciones,
                            NotificacionEnviada = false
                        };

                        db.Add(miRequisito);
                    }
                    else
                    {
                        miRequisito = await db.Requisito.FirstOrDefaultAsync(c => c.IdRequisito == requisito.IdRequisito);
                        miRequisito.IdDocumento = requisito.IdDocumento;
                        miRequisito.IdCiudad = requisito.IdCiudad;
                        miRequisito.IdProceso = requisito.IdProceso;
                        miRequisito.IdProyecto = requisito.IdProyecto;
                        miRequisito.IdActorDuennoProceso = requisito.IdActorDuennoProceso;
                        miRequisito.IdActorResponsableGestSeg = requisito.IdActorResponsableGestSeg;
                        miRequisito.IdActorCustodioDocumento = requisito.IdActorCustodioDocumento;
                        miRequisito.FechaCumplimiento = requisito.FechaCumplimiento;
                        miRequisito.FechaCaducidad = requisito.FechaCaducidad;
                        miRequisito.IdStatus = requisito.IdStatus;
                        miRequisito.DuracionTramite = requisito.DuracionTramite;
                        miRequisito.DiasNotificacion = requisito.DiasNotificacion;
                        miRequisito.EmailNotificacion1 = requisito.EmailNotificacion1;
                        miRequisito.EmailNotificacion2 = requisito.EmailNotificacion2;
                        miRequisito.Observaciones = requisito.Observaciones;
                    }
                    await db.SaveChangesAsync();

                    var responseFile = true;
                    if (file != null)
                    {
                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);

                        if (data.Length > 0)
                        {
                            var activoFijoDocumentoTransfer = new DocumentoRequisitoTransfer { Nombre = file.FileName, Fichero = data, IdRequisito = miRequisito.IdRequisito };
                            responseFile = await uploadFileService.UploadFiles(activoFijoDocumentoTransfer);
                        }
                    }
                    await requisito.EnviarEmailNotificaion(emailSender, db);
                    return this.Redireccionar(responseFile ? $"{Mensaje.Informacion}|{Mensaje.Satisfactorio}" : $"{Mensaje.Aviso}|{Mensaje.ErrorUploadFiles}");
                }
                ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                ViewData["RequisitoLegal"] = await ObtenerSelectListRequisitoLegal(requisito?.Documento?.RequisitoLegal?.IdOrganismoControl ?? -1);
                ViewData["Documento"] = await ObtenerSelectListDocumento(requisito?.Documento?.IdRequisitoLegal ?? -1);
                ViewData["Ciudad"] = new SelectList(await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync(), "IdCiudad", "Nombre");
                ViewData["Proceso"] = new SelectList(await db.Proceso.OrderBy(c => c.Nombre).ToListAsync(), "IdProceso", "Nombre");
                ViewData["Proyecto"] = new SelectList(await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync(), "IdProyecto", "Nombre");
                ViewData["Actor"] = new SelectList(await db.Actor.OrderBy(c => c.Nombres).ToListAsync(), "IdActor", "Nombres");
                ViewData["Status"] = new SelectList(await db.Status.ToListAsync(), "IdStatus", "Nombre");
                return this.VistaError(requisito, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.Excepcion}");
            }
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Detalles(int? id)
        {
            try
            {
                if (id != null)
                {
                    var requisito = await db.Requisito
                        .Include(c => c.Documento).ThenInclude(c => c.RequisitoLegal.OrganismoControl)
                            .Include(c => c.Documento)
                            .Include(c => c.Ciudad)
                            .Include(c => c.Proceso)
                            .Include(c=> c.Proyecto)
                            .Include(c => c.ActorDuennoProceso)
                            .Include(c => c.ActorResponsableGestSeg)
                            .Include(c => c.ActorCustodioDocumento)
                            .Include(c => c.Status)
                            .Include(c=> c.DocumentoRequisito)
                        .FirstOrDefaultAsync(c => c.IdRequisito == id);
                    if (requisito == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(requisito);
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Gestion")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var requisito = await db.Requisito.FirstOrDefaultAsync(m => m.IdRequisito == id);
                if (requisito != null)
                {
                    db.Requisito.Remove(requisito);
                    await db.SaveChangesAsync();
                    return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                }
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.BorradoNoSatisfactorio}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> ListadoResult(Requisito requisito)
        {
            var listaRequisitos = new List<Requisito>();
            try
            {
                var lista = await ListarRequisitos();

                if (requisito?.Documento?.RequisitoLegal?.IdOrganismoControl != -1)
                    lista = lista.Where(c => c.Documento.RequisitoLegal.IdOrganismoControl == requisito.Documento.RequisitoLegal.IdOrganismoControl).ToList();

                if (requisito.IdActorResponsableGestSeg != -1)
                    lista = lista.Where(c => c.IdActorResponsableGestSeg == requisito.IdActorResponsableGestSeg).ToList();

                if (requisito.IdProyecto != -1)
                    lista = lista.Where(c => c.IdProyecto == requisito.IdProyecto).ToList();

                if (requisito.Anno != null)
                    lista = lista.Where(c => c.FechaCumplimiento.Year == requisito.Anno).ToList();

                foreach (var item in lista)
                {
                    int semaforo = item.ObtenerSemaforo();
                    var validarSemaforo = false;

                    if (requisito.SemaforoVerde)
                    {
                        if (semaforo == 1)
                            validarSemaforo = true;
                    }
                    if (requisito.SemaforoAmarillo)
                    {
                        if (semaforo == 2)
                            validarSemaforo = true;
                    }
                    if (requisito.SemaforoRojo)
                    {
                        if (semaforo == 3)
                            validarSemaforo = true;
                    }

                    if (validarSemaforo)
                        listaRequisitos.Add(item);
                }
                return PartialView("_Listado", listaRequisitos);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> DescargarArchivo(int id)
        {
            try
            {
                var documentoRequisitoTransfer = await uploadFileService.GetFileDocumentoRequisito(id);
                return File(documentoRequisitoTransfer.Fichero, MimeTypes.GetMimeType(documentoRequisitoTransfer.Nombre), documentoRequisitoTransfer.Nombre);
            }
            catch (Exception)
            { }
            return StatusCode(500);
        }

        #region AJAX_RequisitoLegal
        public async Task<SelectList> ObtenerSelectListRequisitoLegal(int idOrganismoControl)
        {
            try
            {
                var listaRequisitoLegal = idOrganismoControl != -1 ? await db.RequisitoLegal.Where(c => c.IdOrganismoControl == idOrganismoControl).ToListAsync() : new List<RequisitoLegal>();
                return new SelectList(listaRequisitoLegal, "IdRequisitoLegal", "Nombre");
            }
            catch (Exception)
            {
                return new SelectList(new List<RequisitoLegal>());
            }
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> RequisitoLegal_SelectResult(int idOrganismoControl)
        {
            ViewBag.RequisitoLegal = await ObtenerSelectListRequisitoLegal(idOrganismoControl);
            return PartialView("_RequisitoLegalSelect", new Requisito());
        }
        #endregion

        #region AJAX_Documento
        public async Task<SelectList> ObtenerSelectListDocumento(int idRequisitoLegal)
        {
            try
            {
                var listaDocumento = idRequisitoLegal != -1 ? await db.Documento.Where(c=> c.IdRequisitoLegal == idRequisitoLegal).ToListAsync() : new List<Documento>();
                return new SelectList(listaDocumento, "IdDocumento", "Nombre");
            }
            catch (Exception)
            {
                return new SelectList(new List<Documento>());
            }
        }

        [HttpPost]
        [Authorize(Policy = "GerenciaGestion")]
        public async Task<IActionResult> Documento_SelectResult(int idRequisitoLegal)
        {
            ViewBag.Documento = await ObtenerSelectListDocumento(idRequisitoLegal);
            return PartialView("_DocumentoSelect", new Requisito());
        }
        #endregion
    }
}