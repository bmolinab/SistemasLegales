using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Extensores;
using SistemasLegales.Models.Utiles;

namespace SistemasLegales.Controllers
{
    [Authorize(Policy = "Gestion")]
    public class OrganismoControlController : Controller
    {
        private readonly SistemasLegalesContext db;

        public OrganismoControlController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<OrganismoControl>();
            try
            {
                lista = await db.OrganismoControl.OrderBy(c=> c.Nombre).ToListAsync();
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
                if (id != null)
                {
                    var organismoControl = await db.OrganismoControl.FirstOrDefaultAsync(c => c.IdOrganismoControl == id);
                    if (organismoControl == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(organismoControl);
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
        public async Task<IActionResult> Gestionar(OrganismoControl organismoControl)
        {
            try
            {
                ViewBag.accion = organismoControl.IdOrganismoControl == 0 ? "Crear" : "Editar";
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (organismoControl.IdOrganismoControl == 0)
                    {
                        if (!await db.OrganismoControl.AnyAsync(c => c.Nombre.ToUpper().Trim() == organismoControl.Nombre.ToUpper().Trim()))
                            db.Add(organismoControl);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.OrganismoControl.Where(c => c.Nombre.ToUpper().Trim() == organismoControl.Nombre.ToUpper().Trim()).AnyAsync(c => c.IdOrganismoControl != organismoControl.IdOrganismoControl))
                            db.Update(organismoControl);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(organismoControl, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}");
                }
                return this.VistaError(organismoControl, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
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
                var organismoControl = await db.OrganismoControl.FirstOrDefaultAsync(m => m.IdOrganismoControl == id);
                if (organismoControl != null)
                {
                    db.OrganismoControl.Remove(organismoControl);
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