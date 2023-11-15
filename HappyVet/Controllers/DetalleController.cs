using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HappyVet.Models;
using HappyVet.Repos.Models;

namespace HappyVet.Controllers
{
    public class DetalleController : Controller
    {
        private readonly HappyVetContext _context;

        public DetalleController(HappyVetContext context)
        {
            _context = context;
        }

        // GET: Detalle
        public async Task<IActionResult> Index()
        {
            var happyVetContext = _context.Detalles.Include(d => d.ListaPrecio).Include(d => d.Vacuna);
            return View(await happyVetContext.ToListAsync());
        }

        // GET: Detalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles
                .Include(d => d.ListaPrecio)
                .Include(d => d.Vacuna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // GET: Detalle/Create
        public IActionResult Create()
        {
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion");
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Descripcion");
            return View();
        }

        // POST: Detalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PorcentajeDescuento,VacunaRefId,ListaPrecioRefId,TarifaPrecio,FechaRegistro")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                if (detalle.ListaPrecioRefId != null && detalle.ListaPrecioRefId != 0)
                {
                    var listaPrecio = await _context.ListaPrecios.FindAsync(detalle.ListaPrecioRefId);
                    detalle.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * detalle.PorcentajeDescuento / 100);
                }
                else
                    detalle.TarifaPrecio = 0;
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", detalle.ListaPrecioRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Descripcion", detalle.VacunaRefId);
            return View(detalle);
        }

        // GET: Detalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", detalle.ListaPrecioRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Descripcion", detalle.VacunaRefId);
            return View(detalle);
        }

        // POST: Detalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,PorcentajeDescuento,VacunaRefId,ListaPrecioRefId,TarifaPrecio,FechaRegistro")] Detalle detalle)
        {
            if (id != detalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (detalle.ListaPrecioRefId != null && detalle.ListaPrecioRefId != 0)
                    {
                        var listaPrecio = await _context.ListaPrecios.FindAsync(detalle.ListaPrecioRefId);
                        detalle.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * detalle.PorcentajeDescuento / 100);
                    }
                    else
                        detalle.TarifaPrecio = 0;
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleExists(detalle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", detalle.ListaPrecioRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Descripcion", detalle.VacunaRefId);
            return View(detalle);
        }

        // GET: Detalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detalles == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles
                .Include(d => d.ListaPrecio)
                .Include(d => d.Vacuna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // POST: Detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detalles == null)
            {
                return Problem("Entity set 'HappyVetContext.Detalles'  is null.");
            }
            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle != null)
            {
                _context.Detalles.Remove(detalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleExists(int id)
        {
          return (_context.Detalles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
