using Microsoft.EntityFrameworkCore;

namespace GoleadoresMundial
{
    public class AppDbContext : DbContext
    {
        public DbSet<Jugador> goleadores_mundial { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexion =
                "Server=localhost;Database=blablabla;Uid=root;Password=;";
            var serverVersion = ServerVersion.AutoDetect(conexion);
            optionsBuilder.UseMySql(conexion, serverVersion);
        }
    }
}
