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
    public class AgendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Agenda.Include(a => a.Horario).Include(a => a.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agenda == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agenda
                .Include(a => a.Horario)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // GET: Agendas/Create
        public IActionResult Create()
        {
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "HorarioId", "HorarioId");
            ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "PacienteId", "PacienteId");
            return View();
        }

        // POST: Agendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendaId,PacienteId,HorarioId")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "HorarioId", "HorarioId", agenda.HorarioId);
            ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "PacienteId", "PacienteId", agenda.PacienteId);
            return View(agenda);
        }

        // GET: Agendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agenda == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agenda.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "HorarioId", "HorarioId", agenda.HorarioId);
            ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "PacienteId", "PacienteId", agenda.PacienteId);
            return View(agenda);
        }

        // POST: Agendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgendaId,PacienteId,HorarioId")] Agenda agenda)
        {
            if (id != agenda.AgendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaExists(agenda.AgendaId))
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
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "HorarioId", "HorarioId", agenda.HorarioId);
            ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "PacienteId", "PacienteId", agenda.PacienteId);
            return View(agenda);
        }

        // GET: Agendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agenda == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agenda
                .Include(a => a.Horario)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // POST: Agendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agenda == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Agenda'  is null.");
            }
            var agenda = await _context.Agenda.FindAsync(id);
            if (agenda != null)
            {
                _context.Agenda.Remove(agenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaExists(int id)
        {
          return _context.Agenda.Any(e => e.AgendaId == id);
        }
    }
}
