using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Data;
using AgendaOnline.Models;

namespace AgendaOnline.Controllers
{
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Horarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Horario.Include(h => h.Consultorio).Include(h => h.Especialistas);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Horarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Horario == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario
                .Include(h => h.Consultorio)
                .Include(h => h.Especialistas)
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // GET: Horarios/Create
        public IActionResult Create()
        {
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio");
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId");
            return View();
        }

        // POST: Horarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioId,HorarioConsulta,ConsultorioId,EspecialistaId")] Horario horario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", horario.EspecialistaId);
            return View(horario);
        }

        // GET: Horarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Horario == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", horario.EspecialistaId);
            return View(horario);
        }

        // POST: Horarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioId,HorarioConsulta,ConsultorioId,EspecialistaId")] Horario horario)
        {
            if (id != horario.HorarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horario.HorarioId))
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
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", horario.EspecialistaId);
            return View(horario);
        }

        // GET: Horarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Horario == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario
                .Include(h => h.Consultorio)
                .Include(h => h.Especialistas)
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Horario == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Horario'  is null.");
            }
            var horario = await _context.Horario.FindAsync(id);
            if (horario != null)
            {
                _context.Horario.Remove(horario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id)
        {
          return _context.Horario.Any(e => e.HorarioId == id);
        }
    }
}
