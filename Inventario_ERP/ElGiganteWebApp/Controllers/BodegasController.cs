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
    public class BodegasController : Controller
    {
        private readonly ElGiganteContext _context;

        public BodegasController(ElGiganteContext context)
        {
            _context = context;
        }

        // GET: Bodegas
        public async Task<IActionResult> Index()
        {
              return _context.Bodegas != null ? 
                          View(await _context.Bodegas.ToListAsync()) :
                          Problem("Entity set 'ElGiganteContext.Bodegas'  is null.");
        }

        // GET: Bodegas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return NotFound();
            }

            var bodega = await _context.Bodegas
                .FirstOrDefaultAsync(m => m.IdBodega == id);
            if (bodega == null)
            {
                return NotFound();
            }

            return View(bodega);
        }

        // GET: Bodegas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bodegas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBodega,NombreAlmacen")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }

        // GET: Bodegas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return NotFound();
            }

            var bodega = await _context.Bodegas.FindAsync(id);
            if (bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        // POST: Bodegas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBodega,NombreAlmacen")] Bodega bodega)
        {
            if (id != bodega.IdBodega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodegaExists(bodega.IdBodega))
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
            return View(bodega);
        }

        // GET: Bodegas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return NotFound();
            }

            var bodega = await _context.Bodegas
                .FirstOrDefaultAsync(m => m.IdBodega == id);
            if (bodega == null)
            {
                return NotFound();
            }

            return View(bodega);
        }

        // POST: Bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bodegas == null)
            {
                return Problem("Entity set 'ElGiganteContext.Bodegas'  is null.");
            }
            var bodega = await _context.Bodegas.FindAsync(id);
            if (bodega != null)
            {
                _context.Bodegas.Remove(bodega);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodegaExists(int id)
        {
          return (_context.Bodegas?.Any(e => e.IdBodega == id)).GetValueOrDefault();
        }
    }
}
