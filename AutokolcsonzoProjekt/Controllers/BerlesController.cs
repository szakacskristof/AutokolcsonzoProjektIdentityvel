using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutokolcsonzoProjekt.Data;
using AutokolcsonzoProjekt.Models;

namespace AutokolcsonzoProjekt.Controllers
{
    public class BerlesController : Controller
    {
        private readonly KolcsonzoDbContext _context;

        public BerlesController(KolcsonzoDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(DateTime? datum, decimal? tav)
        {
            var berlesek = _context.Berlesek.Include(n => n.Auto).AsQueryable();
            if (datum.HasValue)
            {
                berlesek = berlesek
                .Where(n => n.Datum == datum);
                ViewData["AktualisDatumSzuro"] = datum.Value.ToString("yyyy-MM-dd");
            }
            if (tav != null && tav > 0)
            {
                berlesek = berlesek
                .Where(b => b.Tav == tav);
                ViewData["AktualisTavSzuro"] = tav;
            }

            return View(await berlesek.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var berles = await _context.Berlesek
                .Include(b => b.Auto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (berles == null)
            {
                return NotFound();
            }

            return View(berles);
        }

        
        public IActionResult Create()
        {
            ViewData["AutoID"] = new SelectList(_context.Autok, "ID", "Meghajtas");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AutoID,Datum,Tol,Ig,Tav")] Berles berles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(berles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoID"] = new SelectList(_context.Autok, "ID", "Meghajtas", berles.AutoID);
            return View(berles);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var berles = await _context.Berlesek.FindAsync(id);
            if (berles == null)
            {
                return NotFound();
            }
            ViewData["AutoID"] = new SelectList(_context.Autok, "ID", "Meghajtas", berles.AutoID);
            return View(berles);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AutoID,Datum,Tol,Ig,Tav")] Berles berles)
        {
            if (id != berles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(berles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BerlesExists(berles.ID))
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
            ViewData["AutoID"] = new SelectList(_context.Autok, "ID", "Meghajtas", berles.AutoID);
            return View(berles);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var berles = await _context.Berlesek
                .Include(b => b.Auto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (berles == null)
            {
                return NotFound();
            }

            return View(berles);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var berles = await _context.Berlesek.FindAsync(id);
            if (berles != null)
            {
                _context.Berlesek.Remove(berles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BerlesExists(int id)
        {
            return _context.Berlesek.Any(e => e.ID == id);
        }
    }
}
