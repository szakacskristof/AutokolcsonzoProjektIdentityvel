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
    public class AutoController : Controller
    {
        private readonly KolcsonzoDbContext _context;

        public AutoController(KolcsonzoDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string? rendszam, string? meghajtas)
        {
            var autok = _context.Autok.AsQueryable();
            if (!string.IsNullOrEmpty(rendszam))
            {
                autok = autok
                .Where(p => p.Rendszam!.ToLower().Contains(rendszam.ToLower()));
                ViewData["AktualisRendszamSzuro"] = rendszam;
            }
            if (!string.IsNullOrEmpty(meghajtas))
            {
                autok = autok
                .Where(p => p.Meghajtas!.ToLower().Contains(meghajtas.ToLower()));
                ViewData["AktualisMeghajtasSzuro"] = meghajtas;
            }
            return View(await autok.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Rendszam,Meghajtas,Uj")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autok.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Rendszam,Meghajtas,Uj")] Auto auto)
        {
            if (id != auto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.ID))
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
            return View(auto);
        }

    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auto = await _context.Autok.FindAsync(id);
            if (auto != null)
            {
                _context.Autok.Remove(auto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
            return _context.Autok.Any(e => e.ID == id);
        }
    }
}
