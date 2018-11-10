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
    public class RequisitoLegalController : Controller
    {
        private readonly SistemasLegalesContext db;

        public RequisitoLegalController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<RequisitoLegal>();
            try
            {
                lista = await db.RequisitoLegal.Include(c=> c.OrganismoControl).OrderBy(c => c.Nombre).ToListAsync();
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
                ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                if (id != null)
                {
                    var requisitoLegal = await db.RequisitoLegal.Include(c=> c.OrganismoControl).FirstOrDefaultAsync(c => c.IdRequisitoLegal == id);
                    if (requisitoLegal == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(requisitoLegal);
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
        public async Task<IActionResult> Gestionar(RequisitoLegal requisitoLegal)
        {
            try
            {
                ViewBag.accion = requisitoLegal.IdRequisitoLegal == 0 ? "Crear" : "Editar";
                Action accion = async () =>
                {
                    ViewData["OrganismoControl"] = new SelectList(await db.OrganismoControl.OrderBy(c => c.Nombre).ToListAsync(), "IdOrganismoControl", "Nombre");
                };

                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (requisitoLegal.IdRequisitoLegal == 0)
                    {
                        if (!await db.RequisitoLegal.AnyAsync(c => c.Nombre.ToUpper().Trim() == requisitoLegal.Nombre.ToUpper().Trim() && c.IdOrganismoControl == requisitoLegal.IdOrganismoControl))
                            db.Add(requisitoLegal);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.RequisitoLegal.Where(c => c.Nombre.ToUpper().Trim() == requisitoLegal.Nombre.ToUpper().Trim() && c.IdOrganismoControl == requisitoLegal.IdOrganismoControl).AnyAsync(c => c.IdRequisitoLegal != requisitoLegal.IdRequisitoLegal))
                            db.Update(requisitoLegal);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(requisitoLegal, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}", accion);
                }
                return this.VistaError(requisitoLegal, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}", accion);
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
                var requisitoLegal = await db.RequisitoLegal.FirstOrDefaultAsync(m => m.IdRequisitoLegal == id);
                if (requisitoLegal != null)
                {
                    db.RequisitoLegal.Remove(requisitoLegal);
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