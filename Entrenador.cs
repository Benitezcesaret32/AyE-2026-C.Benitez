using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sprites.Pokemon;

namespace sprites
{
    internal class Entrenador
    {
        public string nombre { get; set; }
        public int pokedolares { get; set; }
        public string[] medallas { get; set; }
        public Pokemon[] equipo { get; set; }
        public Sprite ash { get; set; }

        public Entrenador(string nombre, int pokedolares, string[] medallas, Pokemon[] equipo, Sprite ash)
        {
            this.nombre = nombre;
            this.pokedolares = pokedolares;
            this.medallas = medallas;
            this.equipo = equipo;
            this.ash = ash;
        }

    }
}
