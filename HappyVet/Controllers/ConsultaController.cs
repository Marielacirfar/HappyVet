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
    public class ConsultaController : Controller
    {
        private readonly HappyVetContext _context;

        public ConsultaController(HappyVetContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var happyVetContext = _context.Consultas
                .Include(c => c.RegistroMascota)
                .Include(c => c.RegistroMascota.TipoAnimal)
                .Include(c => c.RegistroMascota.Raza)
                .Include(c => c.RegistroMascota.Edad)
                .Include(c => c.Vacuna)
                .ThenInclude(cv => cv.Vacuna);



            return View(await happyVetContext.ToListAsync());
        }

        // GET: Consulta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.RegistroMascota)
                .Include(c => c.RegistroMascota.TipoAnimal)
                .Include(c => c.RegistroMascota.Raza)
                .Include(c => c.RegistroMascota.Edad)
                .Include(cv => cv.Vacuna)//
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            var registroMascotas = _context.RegistroMascotas
             .Include(p => p.TipoAnimal)
             .Include(p => p.Edad)
             .Select(x => new
             {
                 x.Id,
                 DescMascotaTipoEdad = x.Descripcion + " - " + x.TipoAnimal.Descripcion + " - " + x.Edad.Descripcion
             });

            ViewData["RegistroMascotaRefId"] = new SelectList(registroMascotas, "Id", "DescMascotaTipoEdad");
            return View();
        }

        // POST: Consulta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHoraConsulta,RegistroMascotaRefId,FechaRegistro,Vacuna")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var registroMascotas = _context.RegistroMascotas
              .Include(p => p.TipoAnimal)
              .Include(p => p.Edad)
              .Select(x => new
              {
                  x.Id,
                  DescPeliculaTipo = x.Descripcion + " - " + x.TipoAnimal.Descripcion + " - " + x.Edad.Descripcion
              });

            ViewData["RegistroMascotaRefId"] = new SelectList(registroMascotas, "Id", "DescRegistroTipo", consulta.RegistroMascotaRefId);
            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddConsultaVacuna(Consulta consulta)
        {
            if (consulta.Vacuna == null)
            {
                consulta.Vacuna = new List<ConsultaVacuna>();
            }
            consulta.Vacuna.Add(new ConsultaVacuna());

            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Descripcion");

            return PartialView("ConsultaVacuna", consulta);




        }


        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["RegistroMascotaRefId"] = new SelectList(_context.RegistroMascotas, "Id", "Descripcion", consulta.RegistroMascotaRefId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHoraConsulta,RegistroMascotaRefId,FechaRegistro")] Consulta consulta)
        {
            if (id != consulta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Id))
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
            ViewData["RegistroMascotaRefId"] = new SelectList(_context.RegistroMascotas, "Id", "Descripcion", consulta.RegistroMascotaRefId);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.RegistroMascota)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'HappyVetContext.Consultas'  is null.");
            }
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
          return (_context.Consultas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
