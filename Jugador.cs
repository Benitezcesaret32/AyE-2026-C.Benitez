namespace GoleadoresMundial
{
    public class Jugador
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string pais { get; set; }

        public string posicion { get; set; }

        public int goles { get; set; }

        public int mundiales_jugados { get; set; }

        public override string ToString()
        {
            return id + " - " +
                   nombre + " " +
                   apellido +
                   " | " + pais +
                   " | " + posicion +
                   " | Goles: " + goles +
                   " | Mundiales: " +
                   mundiales_jugados;
        }
    }
}
