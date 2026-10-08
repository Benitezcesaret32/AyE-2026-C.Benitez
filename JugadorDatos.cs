using System.Collections.Generic;
using System.Linq;

namespace GoleadoresMundial
{
    public class JugadorDatos
    {
        public List<Jugador> Consultar()
        {
            using (AppDbContext conexion = new AppDbContext())
            {
                return conexion.goleadores_mundial.ToList();
            }
        }

        public void Agregar(Jugador jugador)
        {
            using (AppDbContext conexion = new AppDbContext())
            {
                conexion.goleadores_mundial.Add(jugador);
                conexion.SaveChanges();
            }
        }

        public void Actualizar(Jugador jugador)
        {
            using (AppDbContext conexion = new AppDbContext())
            {
                conexion.goleadores_mundial.Update(jugador);
                conexion.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (AppDbContext conexion = new AppDbContext())
            {
                Jugador jugador =
                    conexion.goleadores_mundial.Find(id);

                if (jugador != null)
                {
                    conexion.goleadores_mundial.Remove(jugador);
                    conexion.SaveChanges();
                }
            }
        }
    }
}
