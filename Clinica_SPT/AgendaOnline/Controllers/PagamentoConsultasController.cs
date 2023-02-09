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
    public class PagamentoConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagamentoConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PagamentoConsultas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PagamentoConsulta.Include(p => p.Especialista);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PagamentoConsultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagamentoConsulta == null)
            {
                return NotFound();
            }

            var pagamentoConsulta = await _context.PagamentoConsulta
                .Include(p => p.Especialista)
                .FirstOrDefaultAsync(m => m.IdPagamento == id);
            if (pagamentoConsulta == null)
            {
                return NotFound();
            }

            return View(pagamentoConsulta);
        }

        // GET: PagamentoConsultas/Create
        public IActionResult Create()
        {
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId");
            return View();
        }

        // POST: PagamentoConsultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPagamento,EspecialistaId,DataInicial,DataFinal,Valor")] PagamentoConsulta pagamentoConsulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamentoConsulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", pagamentoConsulta.EspecialistaId);
            return View(pagamentoConsulta);
        }

        // GET: PagamentoConsultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagamentoConsulta == null)
            {
                return NotFound();
            }

            var pagamentoConsulta = await _context.PagamentoConsulta.FindAsync(id);
            if (pagamentoConsulta == null)
            {
                return NotFound();
            }
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", pagamentoConsulta.EspecialistaId);
            return View(pagamentoConsulta);
        }

        // POST: PagamentoConsultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPagamento,EspecialistaId,DataInicial,DataFinal,Valor")] PagamentoConsulta pagamentoConsulta)
        {
            if (id != pagamentoConsulta.IdPagamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamentoConsulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoConsultaExists(pagamentoConsulta.IdPagamento))
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
            ViewData["EspecialistaId"] = new SelectList(_context.Especialista, "EspecialistaId", "EspecialistaId", pagamentoConsulta.EspecialistaId);
            return View(pagamentoConsulta);
        }

        // GET: PagamentoConsultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagamentoConsulta == null)
            {
                return NotFound();
            }

            var pagamentoConsulta = await _context.PagamentoConsulta
                .Include(p => p.Especialista)
                .FirstOrDefaultAsync(m => m.IdPagamento == id);
            if (pagamentoConsulta == null)
            {
                return NotFound();
            }

            return View(pagamentoConsulta);
        }

        // POST: PagamentoConsultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagamentoConsulta == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PagamentoConsulta'  is null.");
            }
            var pagamentoConsulta = await _context.PagamentoConsulta.FindAsync(id);
            if (pagamentoConsulta != null)
            {
                _context.PagamentoConsulta.Remove(pagamentoConsulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoConsultaExists(int id)
        {
          return _context.PagamentoConsulta.Any(e => e.IdPagamento == id);
        }
    }
}
