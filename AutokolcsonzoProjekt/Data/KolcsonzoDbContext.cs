using AutokolcsonzoProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace AutokolcsonzoProjekt.Data
{
    public class KolcsonzoDbContext:DbContext
    {
        private string connstr = "server=localhost;database=autokolcsonzo;user=root;password=";

        public DbSet<Auto> Autok { get; set; }
            public DbSet<Berles> Berlesek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connstr, ServerVersion.AutoDetect(connstr));
        }
    }
}
