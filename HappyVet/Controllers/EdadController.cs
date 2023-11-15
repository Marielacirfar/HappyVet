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
    public class EdadController : Controller
    {
        private readonly HappyVetContext _context;

        public EdadController(HappyVetContext context)
        {
            _context = context;
        }

        // GET: Edad
        public async Task<IActionResult> Index()
        {
              return _context.Edades != null ? 
                          View(await _context.Edades.ToListAsync()) :
                          Problem("Entity set 'HappyVetContext.Edades'  is null.");
        }

        // GET: Edad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Edades == null)
            {
                return NotFound();
            }

            var edad = await _context.Edades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edad == null)
            {
                return NotFound();
            }

            return View(edad);
        }

        // GET: Edad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Edad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Edad edad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(edad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(edad);
        }

        // GET: Edad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Edades == null)
            {
                return NotFound();
            }

            var edad = await _context.Edades.FindAsync(id);
            if (edad == null)
            {
                return NotFound();
            }
            return View(edad);
        }

        // POST: Edad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Edad edad)
        {
            if (id != edad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(edad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EdadExists(edad.Id))
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
            return View(edad);
        }

        // GET: Edad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Edades == null)
            {
                return NotFound();
            }

            var edad = await _context.Edades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edad == null)
            {
                return NotFound();
            }

            return View(edad);
        }

        // POST: Edad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Edades == null)
            {
                return Problem("Entity set 'HappyVetContext.Edades'  is null.");
            }
            var edad = await _context.Edades.FindAsync(id);
            if (edad != null)
            {
                _context.Edades.Remove(edad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EdadExists(int id)
        {
          return (_context.Edades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
