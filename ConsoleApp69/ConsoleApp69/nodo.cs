namespace Pokedex
{
    public class Nodo
    {
        public Pokemon Pokemon { get; set; }

        public Nodo Izquierda { get; set; }

        public Nodo Derecha { get; set; }

        public Nodo(Pokemon pokemon)
        {
            Pokemon = pokemon;
            Izquierda = null;
            Derecha = null;
        }
    }
}
