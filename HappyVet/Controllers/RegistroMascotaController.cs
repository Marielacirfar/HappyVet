using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HappyVet.Models;
using HappyVet.Repos.Models;
using HappyVet.ViewModels;

namespace HappyVet.Controllers
{
    public class RegistroMascotaController : Controller
    {
        private readonly HappyVetContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public RegistroMascotaController(HappyVetContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: RegistroMascota
        public async Task<IActionResult> Index()
        {
            var happyVetContext = _context.RegistroMascotas.Include(r => r.Edad).Include(r => r.Raza).Include(r => r.Tamaño).Include(r => r.TipoAnimal);
            return View(await happyVetContext.ToListAsync());
        }

        // GET: RegistroMascota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroMascotas == null)
            {
                return NotFound();
            }

            var registroMascota = await _context.RegistroMascotas
                .Include(r => r.Edad)
                .Include(r => r.Raza)
                .Include(r => r.Tamaño)
                .Include(r => r.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroMascota == null)
            {
                return NotFound();
            }

            return View(registroMascota);
        }

        // GET: RegistroMascota/Create
        public IActionResult Create()
        {
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Descripcion");
            ViewData["RazaRefId"] = new SelectList(_context.Razas, "Id", "Descripcion");
            ViewData["TamañoRefId"] = new SelectList(_context.Tamaños, "Id", "Descripcion");
            ViewData["TipoAnimalRefId"] = new SelectList(_context.TipoAnimales, "Id", "Descripcion");
            return View();
        }

        // POST: RegistroMascota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroMascotasViewModels model)
        {
            string unifiqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                RegistroMascota registroMascota = new RegistroMascota()
                {
                    ImagemMascota = unifiqueFileName,             
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    FechaIngreso = model.FechaIngreso,
                    EdadRefId = model.EdadRefId,
                    RazaRefId = model.RazaRefId,
                    TamañoRefId = model.TamañoRefId,
                    TipoAnimalRefId = model.TipoAnimalRefId,
                };
                _context.Add(registroMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Descripcion", model.EdadRefId);
            ViewData["RazaRefId"] = new SelectList(_context.Razas, "Id", "Descripcion", model.RazaRefId);
            ViewData["TamañoRefId"] = new SelectList(_context.Tamaños, "Id", "Descripcion", model.TamañoRefId);
            ViewData["TipoAnimalRefId"] = new SelectList(_context.TipoAnimales, "Id", "Descripcion", model.TipoAnimalRefId);
            return View(model);
        }
        private string UploadedFile(RegistroMascotasViewModels model)
        {
            string uniqueFileName = null;

            if (model.Imagem != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagem.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: RegistroMascota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroMascotas == null)
            {
                return NotFound();
            }

            var registroMascota = await _context.RegistroMascotas.FindAsync(id);
            RegistroMascotasViewModels registroMascotasViewModels = new RegistroMascotasViewModels()
            {
                Descripcion = registroMascota.Descripcion,
                FechaRegistro = registroMascota.FechaRegistro,
                FechaIngreso = registroMascota.FechaIngreso,
                EdadRefId = registroMascota.EdadRefId,
                RazaRefId = registroMascota.RazaRefId,
                TamañoRefId = registroMascota.TamañoRefId,
                TipoAnimalRefId = registroMascota.TipoAnimalRefId,

            };
            if (registroMascota == null)
            {
                return NotFound();
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Descripcion", registroMascota.EdadRefId);
            ViewData["RazaRefId"] = new SelectList(_context.Razas, "Id", "Descripcion", registroMascota.RazaRefId);
            ViewData["TamañoRefId"] = new SelectList(_context.Tamaños, "Id", "Descripcion", registroMascota.TamañoRefId);
            ViewData["TipoAnimalRefId"] = new SelectList(_context.TipoAnimales, "Id", "Descripcion", registroMascota.TipoAnimalRefId);
            return View(registroMascotasViewModels);
        }

        // POST: RegistroMascota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RegistroMascotasViewModels model)
        {
            string uniqueFileName = UploadedFile(model);

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var reagistroMascota = await _context.RegistroMascotas.FindAsync(id);
                   
                    reagistroMascota.ImagemMascota = uniqueFileName;
                    reagistroMascota.Descripcion = model.Descripcion;
                    reagistroMascota.FechaRegistro = model.FechaRegistro;
                    reagistroMascota.FechaIngreso = model.FechaIngreso;
                    reagistroMascota.EdadRefId = model.EdadRefId;
                    reagistroMascota.RazaRefId = model.RazaRefId;
                    reagistroMascota.TamañoRefId = model.TamañoRefId;
                    reagistroMascota.TipoAnimalRefId = model.TipoAnimalRefId;
                    _context.Update(reagistroMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroMascotaExists(model.Id))
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
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Descripcion", model.EdadRefId);
            ViewData["RazaRefId"] = new SelectList(_context.Razas, "Id", "Descripcion", model.RazaRefId);
            ViewData["TamañoRefId"] = new SelectList(_context.Tamaños, "Id", "Descripcion", model.TamañoRefId);
            ViewData["TipoAnimalRefId"] = new SelectList(_context.TipoAnimales, "Id", "Descripcion", model.TipoAnimalRefId);
            return View(model);
        }

        // GET: RegistroMascota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroMascotas == null)
            {
                return NotFound();
            }

            var registroMascota = await _context.RegistroMascotas
                .Include(r => r.Edad)
                .Include(r => r.Raza)
                .Include(r => r.Tamaño)
                .Include(r => r.TipoAnimal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroMascota == null)
            {
                return NotFound();
            }

            return View(registroMascota);
        }

        // POST: RegistroMascota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroMascotas == null)
            {
                return Problem("Entity set 'HappyVetContext.RegistroMascotas'  is null.");
            }
            var registroMascota = await _context.RegistroMascotas.FindAsync(id);
            if (registroMascota != null)
            {
                _context.RegistroMascotas.Remove(registroMascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroMascotaExists(int id)
        {
          return (_context.RegistroMascotas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
