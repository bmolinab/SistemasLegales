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
    public class CiudadController : Controller
    {
        private readonly SistemasLegalesContext db;

        public CiudadController(SistemasLegalesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = new List<Ciudad>();
            try
            {
                lista = await db.Ciudad.OrderBy(c => c.Nombre).ToListAsync();
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
                    var ciudad = await db.Ciudad.FirstOrDefaultAsync(c => c.IdCiudad == id);
                    if (ciudad == null)
                        return this.Redireccionar($"{Mensaje.Error}|{Mensaje.RegistroNoEncontrado}");

                    return View(ciudad);
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
        public async Task<IActionResult> Gestionar(Ciudad ciudad)
        {
            try
            {
                ViewBag.accion = ciudad.IdCiudad == 0 ? "Crear" : "Editar";
                if (ModelState.IsValid)
                {
                    var existeRegistro = false;
                    if (ciudad.IdCiudad == 0)
                    {
                        if (!await db.Ciudad.AnyAsync(c => c.Nombre.ToUpper().Trim() == ciudad.Nombre.ToUpper().Trim()))
                            db.Add(ciudad);
                        else
                            existeRegistro = true;
                    }
                    else
                    {
                        if (!await db.Ciudad.Where(c => c.Nombre.ToUpper().Trim() == ciudad.Nombre.ToUpper().Trim()).AnyAsync(c => c.IdCiudad != ciudad.IdCiudad))
                            db.Update(ciudad);
                        else
                            existeRegistro = true;
                    }
                    if (!existeRegistro)
                    {
                        await db.SaveChangesAsync();
                        return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
                    }
                    else
                        return this.VistaError(ciudad, $"{Mensaje.Error}|{Mensaje.ExisteRegistro}");
                }
                return this.VistaError(ciudad, $"{Mensaje.Error}|{Mensaje.ModeloInvalido}");
            }
            catch (Exception ex)
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
                var ciudad = await db.Ciudad.FirstOrDefaultAsync(m => m.IdCiudad == id);
                if (ciudad != null)
                {
                    db.Ciudad.Remove(ciudad);
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