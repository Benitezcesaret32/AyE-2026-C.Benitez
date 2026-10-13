using Microsoft.EntityFrameworkCore;

namespace Prueba_Base_de_Datos
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Agente> agentes_valorant => Set<Agente>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=valorant;Uid=root;pwd=;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agente>().ToTable("agentes_valorant");
        }
    }
}