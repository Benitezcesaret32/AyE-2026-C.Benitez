using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ConsoleApp67;

public class ProductoBD
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Codigo { get; set; }
    public double Precio { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<ProductoBD> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "Server=localhost;Database=inventario;User=root;Password=;",
            ServerVersion.AutoDetect("Server=localhost;Database=inventario;User=root;Password=;")
        );
    }
}
