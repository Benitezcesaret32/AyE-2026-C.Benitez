using Microsoft.EntityFrameworkCore;

namespace Pokedex
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=pokedex;User=root;Password=;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>(pokemon =>
            {
                pokemon.ToTable("pokemon");

                pokemon.HasKey(p => p.Id);

                pokemon.Property(p => p.Id)
                    .HasColumnName("id");

                pokemon.Property(p => p.Nombre)
                    .HasColumnName("nombre");

                pokemon.Property(p => p.Tipo1)
                    .HasColumnName("tipo_1");

                pokemon.Property(p => p.Tipo2)
                    .HasColumnName("tipo_2");

                pokemon.Property(p => p.Hp)
                    .HasColumnName("hp");

                pokemon.Property(p => p.Ataque)
                    .HasColumnName("ataque");

                pokemon.Property(p => p.Defensa)
                    .HasColumnName("defensa");

                pokemon.Property(p => p.AtaqueEspecial)
                    .HasColumnName("ataque_especial");

                pokemon.Property(p => p.DefensaEspecial)
                    .HasColumnName("defensa_especial");

                pokemon.Property(p => p.Velocidad)
                    .HasColumnName("velocidad");

                pokemon.Property(p => p.Nivel)
                    .HasColumnName("nivel");
            });
        }
    }
}