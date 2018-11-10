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
    [Authorize(Policy = "Administracion")]
    public class ActorController : Controller
    {
        private readonly SistemasLegalesContext db;

        public ActorController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<Actor>();
            try
            {
                lista = await db.Actor.OrderBy(c => c.Nombres).ToListAsync();
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
                    var actor = await db.Actor.FirstOrDefaultAsync(c => c.IdActor == id);
                    if (actor == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(actor);
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
        public async Task<IActionResult> Gestionar(Actor actor)
        {
            try
            {
                ViewBag.accion = actor.IdActor == 0 ? "Crear" : "Editar";
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (actor.IdActor == 0)
                    {
                        if (!await db.Actor.AnyAsync(c => c.Nombres.ToUpper().Trim() == actor.Nombres.ToUpper().Trim()))
                            db.Add(actor);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.Actor.Where(c => c.Nombres.ToUpper().Trim() == actor.Nombres.ToUpper().Trim()).AnyAsync(c => c.IdActor != actor.IdActor))
                            db.Update(actor);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(actor, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}");
                }
                return this.VistaError(actor, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
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
                var actor = await db.Actor.FirstOrDefaultAsync(m => m.IdActor == id);
                if (actor != null)
                {
                    db.Actor.Remove(actor);
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