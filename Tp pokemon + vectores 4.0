namespace ConsoleApp51
{
    internal struct Pokemon
    {
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public int PS { get; set; }
        public int PSActual { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int AtaqueEspecial { get; set; }
        public int DefensaEspecial { get; set; }
        public int Velocidad { get; set; }
        public string Estado { get; set; }

        public double Peligrosidad
        {
            get
            {
                return (Ataque + AtaqueEspecial + Velocidad) / 3.0;
            }
        }

        public Pokemon(string nombre, int nivel, int ps, int ataque, int defensa, int ataqueEspecial, int defensaEspecial, int velocidad, string estado = "Normal")
        {
            Nombre = nombre;
            Nivel = nivel;
            PS = ps;
            PSActual = ps;
            Ataque = ataque;
            Defensa = defensa;
            AtaqueEspecial = ataqueEspecial;
            DefensaEspecial = defensaEspecial;
            Velocidad = velocidad;
            Estado = string.IsNullOrWhiteSpace(estado) ? "Normal" : estado;
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
            Medallas = medallas ?? new string[0];
            Pokemones = pokemones ?? new Pokemon[0];
        }

        public int NivelTotal()
        {
            int total = 0;
            for (int i = 0; i < Pokemones.Length; i = i + 1)
            {
                total = total + Pokemones[i].Nivel;
            }
            return total;
        }

        public int ContarPeligrosos(double umbral)
        {
            int cuenta = 0;
            for (int i = 0; i < Pokemones.Length; i = i + 1)
            {
                if (Pokemones[i].Peligrosidad > umbral)
                {
                    cuenta = cuenta + 1;
                }
            }
            return cuenta;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Pokemon pikachu = new Pokemon("Pikachu", 35, 100, 55, 40, 50, 50, 90);
            Pokemon charmander = new Pokemon("Charmander", 30, 90, 52, 43, 60, 50, 65, "Quemado");
            Pokemon bulbasaur = new Pokemon("Bulbasaur", 28, 95, 49, 49, 65, 65, 45);
            Pokemon squirtle = new Pokemon("Squirtle", 29, 92, 48, 65, 50, 64, 43);
            Pokemon pidgeotto = new Pokemon("Pidgeotto", 27, 88, 60, 55, 50, 50, 70);
            Pokemon onix = new Pokemon("Onix", 26, 100, 45, 160, 30, 45, 70);

            Pokemon starmie = new Pokemon("Starmie", 32, 95, 75, 60, 100, 85, 115);
            Pokemon staryu = new Pokemon("Staryu", 25, 80, 45, 55, 65, 65, 85);
            Pokemon psyduck = new Pokemon("Psyduck", 24, 75, 52, 48, 65, 50, 55);
            Pokemon goldeen = new Pokemon("Goldeen", 22, 70, 67, 60, 35, 50, 63);
            Pokemon horsea = new Pokemon("Horsea", 20, 60, 40, 70, 70, 50, 60);
            Pokemon lapras = new Pokemon("Lapras", 34, 150, 85, 80, 85, 95, 60);

            Pokemon[] pokesA = new Pokemon[6] { pikachu, charmander, bulbasaur, squirtle, pidgeotto, onix };
            Pokemon[] pokesB = new Pokemon[6] { staryu, starmie, psyduck, goldeen, horsea, lapras };

            Entrenador entrenadorA = new Entrenador("Ash", 500, new string[2] { "Roca", "Trueno" }, pokesA);
            Entrenador entrenadorB = new Entrenador("Misty", 320, new string[1] { "Arcoiris" }, pokesB);
            Console.WriteLine("Intento de compra de poción por " + entrenadorB.Nombre);
            entrenadorB = ComprarPocion(entrenadorB);
            Console.WriteLine(entrenadorB.Nombre + " ahora tiene pokedolares: " + entrenadorB.Pokedolares);
            Entrenador masNivel = EntrenadorConMasNivel(entrenadorA, entrenadorB);
            Console.WriteLine("Entrenador con más nivel total: " + masNivel.Nombre);
            Entrenador masPeligrosos = EntrenadorConMasPeligrosos(entrenadorA, entrenadorB);
            int cantidadA = entrenadorA.ContarPeligrosos(60.0);
            int cantidadB = entrenadorB.ContarPeligrosos(60.0);
            Console.WriteLine("Peligrosos: " + entrenadorA.Nombre + " tiene " + cantidadA + ", " + entrenadorB.Nombre + " tiene " + cantidadB);
            Console.WriteLine("Entrenador con más pokemones peligrosos (>60): " + masPeligrosos.Nombre);
            entrenadorB = CurarEquipo(entrenadorB);
            Console.WriteLine(entrenadorB.Nombre + " curado. PSActual primer pokémon: " + entrenadorB.Pokemones[0].PSActual);

            Console.WriteLine();
            Console.WriteLine("Comienza batalla entre " + entrenadorA.Nombre + " y " + entrenadorB.Nombre);
            Entrenador ganador = Batalla(entrenadorA, entrenadorB);
            Console.WriteLine("Ganador: " + ganador.Nombre);
        }

        static Entrenador ComprarPocion(Entrenador entrenador)
        {
            const int PRECIO_POCION = 200;
            Console.WriteLine("Deseas comprar una poción por " + PRECIO_POCION + " pokedolares (s/n)");
            string respuesta = Console.ReadLine();
            if (respuesta == null)
            {
                respuesta = string.Empty;
            }
            respuesta = respuesta.Trim().ToLowerInvariant();
            if (respuesta == "s" || respuesta == "si")
            {
                if (entrenador.Pokedolares < PRECIO_POCION)
                {
                    Console.WriteLine("No se pudo comprar: fondos insuficientes.");
                    return entrenador;
                }
                entrenador.Pokedolares = entrenador.Pokedolares - PRECIO_POCION;
                Console.WriteLine("Compra realizada.");
                return entrenador;
            }
            Console.WriteLine("Compra cancelada.");
            return entrenador;
        }

        static Entrenador EntrenadorConMasNivel(Entrenador a, Entrenador b)
        {
            int totalA = a.NivelTotal();
            int totalB = b.NivelTotal();
            if (totalA >= totalB)
            {
                return a;
            }
            return b;
        }

        static Entrenador EntrenadorConMasPeligrosos(Entrenador a, Entrenador b)
        {
            int cuentaA = a.ContarPeligrosos(60.0);
            int cuentaB = b.ContarPeligrosos(60.0);
            if (cuentaA >= cuentaB)
            {
                return a;
            }
            return b;
        }

        static Entrenador CurarEquipo(Entrenador entrenador)
        {
            for (int i = 0; i < entrenador.Pokemones.Length; i = i + 1)
            {
                Pokemon p = entrenador.Pokemones[i];
                p.PSActual = p.PS;
                entrenador.Pokemones[i] = p;
            }
            return entrenador;
        }

        static Entrenador Batalla(Entrenador a, Entrenador b)
        {
            if (a.Pokemones.Length == 0)
            {
                return b;
            }
            if (b.Pokemones.Length == 0)
            {
                return a;
            }

            for (int i = 0; i < a.Pokemones.Length; i = i + 1)
            {
                Pokemon p = a.Pokemones[i];
                if (p.PSActual <= 0)
                {
                    p.PSActual = p.PS;
                    a.Pokemones[i] = p;
                }
            }

            for (int i = 0; i < b.Pokemones.Length; i = i + 1)
            {
                Pokemon p = b.Pokemones[i];
                if (p.PSActual <= 0)
                {
                    p.PSActual = p.PS;
                    b.Pokemones[i] = p;
                }
            }

            int idxA = 0;
            int idxB = 0;
            bool turnoA = a.Pokemones[0].Velocidad >= b.Pokemones[0].Velocidad;

            while (idxA < a.Pokemones.Length && idxB < b.Pokemones.Length)
            {
                Pokemon currentA = a.Pokemones[idxA];
                Pokemon currentB = b.Pokemones[idxB];

                Console.WriteLine();
                Console.WriteLine(a.Nombre + " -> " + currentA.Nombre + " (PS: " + currentA.PSActual + "/" + currentA.PS + ")");
                Console.WriteLine(b.Nombre + " -> " + currentB.Nombre + " (PS: " + currentB.PSActual + "/" + currentB.PS + ")");

                if (turnoA)
                {
                    Console.WriteLine(a.Nombre + " elige: 1) Ataque  2) AtaqueEspecial");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        input = "1";
                    }
                    input = input.Trim();
                    int opcion;
                    try
                    {
                        opcion = Convert.ToInt32(input);
                    }
                    catch
                    {
                        opcion = 1;
                    }
                    bool especial = opcion == 2;
                    int danho = CalcularDanho(currentA, currentB, especial);
                    currentB.PSActual = currentB.PSActual - danho;
                    Console.WriteLine(currentA.Nombre + " hace " + danho + " a " + currentB.Nombre + " (restan: " + Math.Max(0, currentB.PSActual) + ")");
                    b.Pokemones[idxB] = currentB;
                    if (currentB.PSActual <= 0)
                    {
                        Console.WriteLine(currentB.Nombre + " derrotado.");
                        idxB = idxB + 1;
                        if (idxB >= b.Pokemones.Length)
                        {
                            return a;
                        }
                        Console.WriteLine(b.Nombre + " envía a " + b.Pokemones[idxB].Nombre);
                    }
                }
                else
                {
                    Console.WriteLine(b.Nombre + " elige: 1) Ataque  2) AtaqueEspecial");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        input = "1";
                    }
                    input = input.Trim();
                    int opcion;
                    try
                    {
                        opcion = Convert.ToInt32(input);
                    }
                    catch
                    {
                        opcion = 1;
                    }
                    bool especial = opcion == 2;
                    int danho = CalcularDanho(currentB, currentA, especial);
                    currentA.PSActual = currentA.PSActual - danho;
                    Console.WriteLine(currentB.Nombre + " hace " + danho + " a " + currentA.Nombre + " (restan: " + Math.Max(0, currentA.PSActual) + ")");
                    a.Pokemones[idxA] = currentA;
                    if (currentA.PSActual <= 0)
                    {
                        Console.WriteLine(currentA.Nombre + " derrotado.");
                        idxA = idxA + 1;
                        if (idxA >= a.Pokemones.Length)
                        {
                            return b;
                        }
                        Console.WriteLine(a.Nombre + " envía a " + a.Pokemones[idxA].Nombre);
                    }
                }

                turnoA = !turnoA;
            }

            if (idxA < a.Pokemones.Length)
            {
                return a;
            }
            return b;
        }

        static int CalcularDanho(Pokemon atacante, Pokemon defensor, bool especial)
        {
            double ataqueValor = especial ? atacante.AtaqueEspecial : atacante.Ataque;
            double defensaValor = especial ? defensor.DefensaEspecial : defensor.Defensa;
            double factor = 1.0 - (defensaValor / 100.0);
            if (factor < 0.05)
            {
                factor = 0.05;
            }
            int danho = (int)Math.Round(ataqueValor * factor);
            if (danho < 1)
            {
                danho = 1;
            }
            return danho;
        }
    }
}
