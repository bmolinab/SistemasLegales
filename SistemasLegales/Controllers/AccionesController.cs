using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;

namespace SistemasLegales.Controllers
{
    public class AccionesController : Controller
    {
        private readonly SistemasLegalesContext _context;

        public AccionesController(SistemasLegalesContext context)
        {
            _context = context;    
        }

        // GET: Acciones
        public async Task<IActionResult> Index()
        {
            var sistemasLegalesContext = _context.Accion.Include(a => a.IdRequisitoNavigation);
            return View(await sistemasLegalesContext.ToListAsync());
        }

        // GET: Acciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accion = await _context.Accion
                .Include(a => a.IdRequisitoNavigation)
                .SingleOrDefaultAsync(m => m.IdAccion == id);
            if (accion == null)
            {
                return NotFound();
            }

            return View(accion);
        }

        // GET: Acciones/Create
        public IActionResult Create()
        {
            ViewData["IdRequisito"] = new SelectList(_context.Requisito, "IdRequisito", "EmailNotificacion1");
            return View();
        }

        // POST: Acciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAccion,Detalle,Fecha,IdRequisito")] Accion accion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdRequisito"] = new SelectList(_context.Requisito, "IdRequisito", "EmailNotificacion1", accion.IdRequisito);
            return View(accion);
        }

        // GET: Acciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accion = await _context.Accion.SingleOrDefaultAsync(m => m.IdAccion == id);
            if (accion == null)
            {
                return NotFound();
            }
            ViewData["IdRequisito"] = new SelectList(_context.Requisito, "IdRequisito", "EmailNotificacion1", accion.IdRequisito);
            return View(accion);
        }

        // POST: Acciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAccion,Detalle,Fecha,IdRequisito")] Accion accion)
        {
            if (id != accion.IdAccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccionExists(accion.IdAccion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["IdRequisito"] = new SelectList(_context.Requisito, "IdRequisito", "EmailNotificacion1", accion.IdRequisito);
            return View(accion);
        }

        // GET: Acciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accion = await _context.Accion
                .Include(a => a.IdRequisitoNavigation)
                .SingleOrDefaultAsync(m => m.IdAccion == id);
            if (accion == null)
            {
                return NotFound();
            }

            return View(accion);
        }

        // POST: Acciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accion = await _context.Accion.SingleOrDefaultAsync(m => m.IdAccion == id);
            _context.Accion.Remove(accion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AccionExists(int id)
        {
            return _context.Accion.Any(e => e.IdAccion == id);
        }
    }
}
