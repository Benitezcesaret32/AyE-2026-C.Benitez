namespace consoleApp46
{
    internal struct Pokemon
    {
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public int PS { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int AtaqueEspecial { get; set; }
        public int DefensaEspecial { get; set; }
        public int Velocidad { get; set; }

        public string Estado { get; set; }

        public Pokemon(string nombre, int nivel, int ps, int ataque, int defensa, int ataqueEspecial, int defensaEspecial, int velocidad, string estado = "Normal")
        {
            Nombre = nombre;
            Nivel = nivel;
            PS = ps;
            Ataque = ataque;
            Defensa = defensa;
            AtaqueEspecial = ataqueEspecial;
            DefensaEspecial = defensaEspecial;
            Velocidad = velocidad;

            if (string.IsNullOrWhiteSpace(estado))
            {
                Estado = "Normal";
            }
            else
            {
                Estado = estado;
            }
        }
    }

    internal struct Entrenador
    {
        public string Nombre { get; set; }
        public int Pokedolares { get; set; }
        public string[] Medallas { get; set; }
        public Pokemon[] Pokemones { get; set; }

        public Entrenador(string nombre, int pokedolares, string[] medallas, Pokemon[] pokemones)
        {
            Nombre = nombre;
            Pokedolares = pokedolares;

            if (medallas == null)
            {
                Medallas = new string[0];
            }
            else
            {
                Medallas = medallas;
            }

            if (pokemones == null)
            {
                Pokemones = new Pokemon[0];
            }
            else
            {
                Pokemones = pokemones;
            }
        }

        public int NivelTotal()
        {
            if (Pokemones.Length == 0)
            {
                return 0;
            }

            int total = 0;
            for (int i = 0; i < Pokemones.Length; i = i + 1)
            {
                total = total + Pokemones[i].Nivel;
            }

            return total;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Pokemon pikachu = new Pokemon("Pikachu", nivel: 35, ps: 100, ataque: 55, defensa: 40, ataqueEspecial: 50, defensaEspecial: 50, velocidad: 90);
            Pokemon charmander = new Pokemon("Charmander", nivel: 30, ps: 90, ataque: 52, defensa: 43, ataqueEspecial: 60, defensaEspecial: 50, velocidad: 65, estado: "Quemado");
            Pokemon bulbasaur = new Pokemon("Bulbasaur", nivel: 28, ps: 95, ataque: 49, defensa: 49, ataqueEspecial: 65, defensaEspecial: 65, velocidad: 45);
            Pokemon squirtle = new Pokemon("Squirtle", nivel: 29, ps: 92, ataque: 48, defensa: 65, ataqueEspecial: 50, defensaEspecial: 64, velocidad: 43);
            Pokemon pidgeotto = new Pokemon("Pidgeotto", nivel: 27, ps: 88, ataque: 60, defensa: 55, ataqueEspecial: 50, defensaEspecial: 50, velocidad: 70);
            Pokemon onix = new Pokemon("Onix", nivel: 26, ps: 100, ataque: 45, defensa: 160, ataqueEspecial: 30, defensaEspecial: 45, velocidad: 70);

            Pokemon staryu = new Pokemon("Staryu", nivel: 25, ps: 80, ataque: 45, defensa: 55, ataqueEspecial: 65, defensaEspecial: 65, velocidad: 85);
            Pokemon starmie = new Pokemon("Starmie", nivel: 32, ps: 95, ataque: 75, defensa: 60, ataqueEspecial: 100, defensaEspecial: 85, velocidad: 115);
            Pokemon psyduck = new Pokemon("Psyduck", nivel: 24, ps: 75, ataque: 52, defensa: 48, ataqueEspecial: 65, defensaEspecial: 50, velocidad: 55);
            Pokemon goldeen = new Pokemon("Goldeen", nivel: 22, ps: 70, ataque: 67, defensa: 60, ataqueEspecial: 35, defensaEspecial: 50, velocidad: 63);
            Pokemon horsea = new Pokemon("Horsea", nivel: 20, ps: 60, ataque: 40, defensa: 70, ataqueEspecial: 70, defensaEspecial: 50, velocidad: 60);
            Pokemon lapras = new Pokemon("Lapras", nivel: 34, ps: 150, ataque: 85, defensa: 80, ataqueEspecial: 85, defensaEspecial: 95, velocidad: 60);

            string[] medallasA = new string[2] { "Roca", "Trueno" };
            Pokemon[] pokesA = new Pokemon[6] { pikachu, charmander, bulbasaur, squirtle, pidgeotto, onix };
            Entrenador entrenadorA = new Entrenador(
                nombre: "Ash",
                pokedolares: 1500,
                medallas: medallasA,
                pokemones: pokesA
            );

            string[] medallasB = new string[1] { "Arcoiris" };
            Pokemon[] pokesB = new Pokemon[6] { staryu, starmie, psyduck, goldeen, horsea, lapras };
            Entrenador entrenadorB = new Entrenador(
                nombre: "Misty",
                pokedolares: 1200,
                medallas: medallasB,
                pokemones: pokesB
            );

            CompareEntrenadores(entrenadorA, entrenadorB);
        }

        static void CompareEntrenadores(Entrenador a, Entrenador b)
        {
            int totalA = a.NivelTotal();
            int totalB = b.NivelTotal();

            Console.WriteLine("Nivel total de " + a.Nombre + ": " + totalA);
            Console.WriteLine("Nivel total de " + b.Nombre + ": " + totalB);

            if (totalA > totalB)
            {
                Console.WriteLine("El entrenador con más nivel es: " + a.Nombre);
            }
            else if (totalB > totalA)
            {
                Console.WriteLine("El entrenador con más nivel es: " + b.Nombre);
            }
            else
            {
                Console.WriteLine("Ambos entrenadores tienen el mismo nivel total.");
            }
        }
    }
}

