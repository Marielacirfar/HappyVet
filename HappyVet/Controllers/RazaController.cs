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
    public class RazaController : Controller
    {
        private readonly HappyVetContext _context;

        public RazaController(HappyVetContext context)
        {
            _context = context;
        }

        // GET: Raza
        public async Task<IActionResult> Index()
        {
              return _context.Razas != null ? 
                          View(await _context.Razas.ToListAsync()) :
                          Problem("Entity set 'HappyVetContext.Razas'  is null.");
        }

        // GET: Raza/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raza == null)
            {
                return NotFound();
            }

            return View(raza);
        }

        // GET: Raza/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Raza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Raza raza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raza);
        }

        // GET: Raza/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas.FindAsync(id);
            if (raza == null)
            {
                return NotFound();
            }
            return View(raza);
        }

        // POST: Raza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Raza raza)
        {
            if (id != raza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazaExists(raza.Id))
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
            return View(raza);
        }

        // GET: Raza/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raza == null)
            {
                return NotFound();
            }

            return View(raza);
        }

        // POST: Raza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Razas == null)
            {
                return Problem("Entity set 'HappyVetContext.Razas'  is null.");
            }
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                _context.Razas.Remove(raza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazaExists(int id)
        {
          return (_context.Razas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
