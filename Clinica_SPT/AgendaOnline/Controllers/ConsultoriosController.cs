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
    public class ConsultoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultorios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Consultorio.Include(c => c.Unidade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Consultorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultorio == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .Include(c => c.Unidade)
                .FirstOrDefaultAsync(m => m.IdConsultorio == id);
            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }

        // GET: Consultorios/Create
        public IActionResult Create()
        {
            ViewData["UnidadeId"] = new SelectList(_context.Set<Unidade>(), "UnidadeId", "UnidadeId");
            return View();
        }

        // POST: Consultorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConsultorio,NumSala,Caracteristica,UnidadeId")] Consultorio consultorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnidadeId"] = new SelectList(_context.Set<Unidade>(), "UnidadeId", "UnidadeId", consultorio.UnidadeId);
            return View(consultorio);
        }

        // GET: Consultorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultorio == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio.FindAsync(id);
            if (consultorio == null)
            {
                return NotFound();
            }
            ViewData["UnidadeId"] = new SelectList(_context.Set<Unidade>(), "UnidadeId", "UnidadeId", consultorio.UnidadeId);
            return View(consultorio);
        }

        // POST: Consultorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsultorio,NumSala,Caracteristica,UnidadeId")] Consultorio consultorio)
        {
            if (id != consultorio.IdConsultorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioExists(consultorio.IdConsultorio))
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
            ViewData["UnidadeId"] = new SelectList(_context.Set<Unidade>(), "UnidadeId", "UnidadeId", consultorio.UnidadeId);
            return View(consultorio);
        }

        // GET: Consultorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultorio == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .Include(c => c.Unidade)
                .FirstOrDefaultAsync(m => m.IdConsultorio == id);
            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }

        // POST: Consultorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultorio == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultorio'  is null.");
            }
            var consultorio = await _context.Consultorio.FindAsync(id);
            if (consultorio != null)
            {
                _context.Consultorio.Remove(consultorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioExists(int id)
        {
          return _context.Consultorio.Any(e => e.IdConsultorio == id);
        }
    }
}
