using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElGiganteWebApp.Models;

namespace ElGiganteWebApp.Controllers
{
    public class PaisController : Controller
    {
        private readonly ElGiganteContext _context;

        public PaisController(ElGiganteContext context)
        {
            _context = context;
        }

        // GET: Pais
        public async Task<IActionResult> Index()
        {
              return _context.Pais != null ? 
                          View(await _context.Pais.ToListAsync()) :
                          Problem("Entity set 'ElGiganteContext.Pais'  is null.");
        }

        // GET: Pais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pais == null)
            {
                return NotFound();
            }

            var pai = await _context.Pais
                .FirstOrDefaultAsync(m => m.IdPais == id);
            if (pai == null)
            {
                return NotFound();
            }

            return View(pai);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPais,NombrePais")] Pai pai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pai);
        }

        // GET: Pais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pais == null)
            {
                return NotFound();
            }

            var pai = await _context.Pais.FindAsync(id);
            if (pai == null)
            {
                return NotFound();
            }
            return View(pai);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPais,NombrePais")] Pai pai)
        {
            if (id != pai.IdPais)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaiExists(pai.IdPais))
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
            return View(pai);
        }

        // GET: Pais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pais == null)
            {
                return NotFound();
            }

            var pai = await _context.Pais
                .FirstOrDefaultAsync(m => m.IdPais == id);
            if (pai == null)
            {
                return NotFound();
            }

            return View(pai);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pais == null)
            {
                return Problem("Entity set 'ElGiganteContext.Pais'  is null.");
            }
            var pai = await _context.Pais.FindAsync(id);
            if (pai != null)
            {
                _context.Pais.Remove(pai);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaiExists(int id)
        {
          return (_context.Pais?.Any(e => e.IdPais == id)).GetValueOrDefault();
        }
    }
}
