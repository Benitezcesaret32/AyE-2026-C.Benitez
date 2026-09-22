using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp65;
using Microsoft.EntityFrameworkCore;

class AppDbContext : DbContext
{
    public DbSet<Punto> Punto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;database=puntodb;user=root;password=",
            ServerVersion.AutoDetect(
                "server=localhost;database=puntodb;user=root;password="
            )
        );
    }
}