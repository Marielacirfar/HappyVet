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
    public class TamañoController : Controller
    {
        private readonly HappyVetContext _context;

        public TamañoController(HappyVetContext context)
        {
            _context = context;
        }

        // GET: Tamaño
        public async Task<IActionResult> Index()
        {
              return _context.Tamaños != null ? 
                          View(await _context.Tamaños.ToListAsync()) :
                          Problem("Entity set 'HappyVetContext.Tamaños'  is null.");
        }

        // GET: Tamaño/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tamaños == null)
            {
                return NotFound();
            }

            var tamaño = await _context.Tamaños
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamaño == null)
            {
                return NotFound();
            }

            return View(tamaño);
        }

        // GET: Tamaño/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tamaño/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Tamaño tamaño)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tamaño);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tamaño);
        }

        // GET: Tamaño/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tamaños == null)
            {
                return NotFound();
            }

            var tamaño = await _context.Tamaños.FindAsync(id);
            if (tamaño == null)
            {
                return NotFound();
            }
            return View(tamaño);
        }

        // POST: Tamaño/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Tamaño tamaño)
        {
            if (id != tamaño.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tamaño);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TamañoExists(tamaño.Id))
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
            return View(tamaño);
        }

        // GET: Tamaño/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tamaños == null)
            {
                return NotFound();
            }

            var tamaño = await _context.Tamaños
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamaño == null)
            {
                return NotFound();
            }

            return View(tamaño);
        }

        // POST: Tamaño/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tamaños == null)
            {
                return Problem("Entity set 'HappyVetContext.Tamaños'  is null.");
            }
            var tamaño = await _context.Tamaños.FindAsync(id);
            if (tamaño != null)
            {
                _context.Tamaños.Remove(tamaño);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TamañoExists(int id)
        {
          return (_context.Tamaños?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
