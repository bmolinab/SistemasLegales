using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Extensores;
using SistemasLegales.Models.Utiles;
namespace SistemasLegales.Controllers
{
    [Authorize(Policy = "Administracion")]
    public class DocumentoController : Controller
    {
        private readonly SistemasLegalesContext db;

        public DocumentoController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<Documento>();
            try
            {
                lista = await db.Documento.Include(c=> c.RequisitoLegal).ThenInclude(c=> c.OrganismoControl).OrderBy(c => c.Nombre).ToListAsync();
            }
            catch (Exception)
            {
                TempData["Mensaje"] = $"{Mensaje.Error}|{Mensaje.ErrorListado}";
            }
            return View(lista);
        }
        
        public async Task<IActionResult> Gestionar(int? id)
        {
            try
            {
                ViewBag.accion = id == null ? "Crear" : "Editar";
                ViewData["RequisitoLegal"] = new SelectList(await db.RequisitoLegal.OrderBy(c => c.Nombre).Select(y=> new RequisitoLegal {IdRequisitoLegal=y.IdRequisitoLegal,Nombre=y.Nombre.Length>100?y.Nombre.Substring(0,100).ToString() + " ...":y.Nombre }).ToListAsync(), "IdRequisitoLegal", "Nombre");
                if (id != null)
                {
                    var documento = await db.Documento.Include(c => c.RequisitoLegal).ThenInclude(c => c.OrganismoControl).FirstOrDefaultAsync(c => c.IdDocumento == id);
                    if (documento == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(documento);
                }
                return View();
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.ErrorCargarDatos}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gestionar(Documento documento)
        {
            try
            {
                ViewBag.accion = documento.IdDocumento == 0 ? "Crear" : "Editar";
                Action accion = async () =>
                {
                    ViewData["RequisitoLegal"] = new SelectList(await db.RequisitoLegal.OrderBy(c => c.Nombre).Select(y => new RequisitoLegal { IdRequisitoLegal = y.IdRequisitoLegal, Nombre = y.Nombre.Length > 100 ? y.Nombre.Substring(0, 100).ToString() + " ..." : y.Nombre }).ToListAsync(), "IdRequisitoLegal", "Nombre");
                };
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (documento.IdDocumento == 0)
                    {
                        if (!await db.Documento.AnyAsync(c => c.Nombre.ToUpper().Trim() == documento.Nombre.ToUpper().Trim() && c.IdRequisitoLegal == documento.IdRequisitoLegal))
                            db.Add(documento);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.Documento.Where(c => c.Nombre.ToUpper().Trim() == documento.Nombre.ToUpper().Trim() && c.IdRequisitoLegal == documento.IdRequisitoLegal).AnyAsync(c => c.IdDocumento != documento.IdDocumento))
                            db.Update(documento);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(documento, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}", accion);
                }
                return this.VistaError(documento, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}", accion);
            }
            catch (Exception)
            {
                return this.Redireccionar($"{Mensaje.Error}|{Mensaje.Excepcion}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var documento = await db.Documento.FirstOrDefaultAsync(m => m.IdDocumento == id);
                if (documento != null)
                {
                    db.Documento.Remove(documento);
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
    }
}