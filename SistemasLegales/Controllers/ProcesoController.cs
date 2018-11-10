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
    public class ProcesoController : Controller
    {
        private readonly SistemasLegalesContext db;

        public ProcesoController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<Proceso>();
            try
            {
                lista = await db.Proceso.OrderBy(c => c.Nombre).ToListAsync();
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
                    var proceso = await db.Proceso.FirstOrDefaultAsync(c => c.IdProceso == id);
                    if (proceso == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(proceso);
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
        public async Task<IActionResult> Gestionar(Proceso proceso)
        {
            try
            {
                ViewBag.accion = proceso.IdProceso == 0 ? "Crear" : "Editar";
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (proceso.IdProceso == 0)
                    {
                        if (!await db.Proceso.AnyAsync(c => c.Nombre.ToUpper().Trim() == proceso.Nombre.ToUpper().Trim()))
                            db.Add(proceso);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.Proceso.Where(c => c.Nombre.ToUpper().Trim() == proceso.Nombre.ToUpper().Trim()).AnyAsync(c => c.IdProceso != proceso.IdProceso))
                            db.Update(proceso);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(proceso, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}");
                }
                return this.VistaError(proceso, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
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
                var proceso = await db.Proceso.FirstOrDefaultAsync(m => m.IdProceso == id);
                if (proceso != null)
                {
                    db.Proceso.Remove(proceso);
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