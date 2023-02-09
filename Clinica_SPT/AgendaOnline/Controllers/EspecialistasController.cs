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
    public class EspecialistasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspecialistasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Especialistas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Especialista.Include(e => e.Especialidade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Especialistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialista == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista
                .Include(e => e.Especialidade)
                .FirstOrDefaultAsync(m => m.EspecialistaId == id);
            if (especialista == null)
            {
                return NotFound();
            }

            return View(especialista);
        }

        // GET: Especialistas/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidade, "EspecialidadeId", "EspecialidadeId");
            return View();
        }

        // POST: Especialistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialistaId,EspecialistaNome,CRM,EspecialidadeId")] Especialista especialista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidade, "EspecialidadeId", "EspecialidadeId", especialista.EspecialidadeId);
            return View(especialista);
        }

        // GET: Especialistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialista == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista.FindAsync(id);
            if (especialista == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidade, "EspecialidadeId", "EspecialidadeId", especialista.EspecialidadeId);
            return View(especialista);
        }

        // POST: Especialistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialistaId,EspecialistaNome,CRM,EspecialidadeId")] Especialista especialista)
        {
            if (id != especialista.EspecialistaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialistaExists(especialista.EspecialistaId))
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
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidade, "EspecialidadeId", "EspecialidadeId", especialista.EspecialidadeId);
            return View(especialista);
        }

        // GET: Especialistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialista == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista
                .Include(e => e.Especialidade)
                .FirstOrDefaultAsync(m => m.EspecialistaId == id);
            if (especialista == null)
            {
                return NotFound();
            }

            return View(especialista);
        }

        // POST: Especialistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialista == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Especialista'  is null.");
            }
            var especialista = await _context.Especialista.FindAsync(id);
            if (especialista != null)
            {
                _context.Especialista.Remove(especialista);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialistaExists(int id)
        {
          return _context.Especialista.Any(e => e.EspecialistaId == id);
        }
    }
}
