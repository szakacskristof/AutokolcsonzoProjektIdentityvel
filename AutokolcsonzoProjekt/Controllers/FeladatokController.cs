using AutokolcsonzoProjekt.Data;
using AutokolcsonzoProjekt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutokolcsonzoProjekt.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class FeladatokController : Controller
    {
        private readonly KolcsonzoDbContext _context;

        public FeladatokController(KolcsonzoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Feladat2()
        {
            var results = _context.Autok
                .Where(a => a.Uj == true && a.Meghajtas == "elektromos")
                .OrderBy(a => a.Rendszam)
                .Select(a => a.Rendszam);

            return View(results);
        }
        public IActionResult Feladat3()
        {
            DateTime keresettdatum = new DateTime(2018, 6, 20);
            TimeSpan idopont=new TimeSpan(22,30,0);


            var results = _context.Berlesek
                
                .Where(x => x.Datum==keresettdatum &&  x.Tol <= idopont|| x.Ig>=idopont).Include(n=>n.Auto).Select(x=> new Feladat3ViewModel { Rendszam=x.Auto.Rendszam}).Distinct().ToList();


            return View(results);
        }
        public IActionResult Feladat4()
        {
            var results = _context.Berlesek
                .GroupBy(b => b.Auto.Rendszam)
                .Select(g => new Feladat4ViewModel
                {
                    Rendszam = g.Key,
                    OsszesTav = (int)g.Sum(b => b.Tav)
                })
                .OrderByDescending(x => x.OsszesTav)
                .ToList();

            return View(results);
        }
        public IActionResult Feladat5()
        {
            var elsoNap = _context.Berlesek.Min(b => b.Datum);
            var utolsoNap = _context.Berlesek.Max(b => b.Datum);

            var eredmeny = new
            {
                ElsoNap = elsoNap,
                UtolsoNap = utolsoNap
            };

            return View(eredmeny);
        }
        public IActionResult Feladat6()
        {
            var elektromosDb = _context.Autok.Count(a => a.Meghajtas == "elektromos");
            var hibridDb = _context.Autok.Count(a => a.Meghajtas == "hibrid");

            string tobb = elektromosDb >= hibridDb ? "elektromos" : "hibrid";

            return Content(tobb);
        }









    }
}
