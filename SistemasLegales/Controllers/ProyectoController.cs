using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Extensores;
using SistemasLegales.Models.Utiles;

namespace SistemasLegales.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly SistemasLegalesContext db;

        public ProyectoController(SistemasLegalesContext context)
        {
            db = context;   
            
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<Proyecto>();
            try
            {
                lista = await db.Proyecto.OrderBy(c => c.Nombre).ToListAsync();
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
                    var proyecto = await db.Proyecto.FirstOrDefaultAsync(c => c.IdProyecto == id);
                    if (proyecto == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(proyecto);
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
        public async Task<IActionResult> Gestionar(Proyecto proyecto)
        {
            try
            {
                ViewBag.accion = proyecto.IdProyecto == 0 ? "Crear" : "Editar";
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (proyecto.IdProyecto == 0)
                    {
                        if (!await db.Proyecto.AnyAsync(c => c.Nombre.ToUpper().Trim() == proyecto.Nombre.ToUpper().Trim()))
                            db.Add(proyecto);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.Proyecto.Where(c => c.Nombre.ToUpper().Trim() == proyecto.Nombre.ToUpper().Trim()).AnyAsync(c => c.IdProyecto != proyecto.IdProyecto))
                            db.Update(proyecto);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(proyecto, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}");
                }
                return this.VistaError(proyecto, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
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
                var proyecto = await db.Proyecto.FirstOrDefaultAsync(m => m.IdProyecto == id);
                if (proyecto != null)
                {
                    db.Proyecto.Remove(proyecto);
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
