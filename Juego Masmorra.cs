namespace ConsoleApp43
{
    internal class Program
    {
        public class Personaje
        {
            public string Nombre { get; set; }
            public int Poder { get; set; }
            public string[] Items { get; }

            public Personaje(string nombre, int poder)
            {
                Nombre = nombre;
                Poder = poder;
                Items = new string[20];
            }

            // Añade un ítem
            public bool AñadirItem(string item, int poderItem)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    // Si está vacío lo guardo
                    if (Items[i] == null || Items[i] == "")
                    {
                        Items[i] = item;
                        Poder += poderItem;
                        return true;
                    }
                }
                return false;
            }

            // Devuelve los ítems como texto simple
            public string ListadoItems()
            {
                string resultado = "";
                foreach (var it in Items)
                {
                    if (it != null && it != "")
                    {
                        if (resultado.Length > 0)
                            resultado += ", ";
                        resultado += it;
                    }
                }
                return resultado.Length == 0 ? "(ninguno)" : resultado;
            }
        }

        public struct Sala
        {
            public int Dificultad { get; }
            public string Item { get; }
            public int PoderItem { get; }

            public Sala(int dificultad, string item, int poderItem)
            {
                Dificultad = dificultad;
                Item = item;
                PoderItem = poderItem;
            }
        }

        static void Main(string[] args)
        {
            var rnd = new Random();

            Console.Write("Nombre del personaje 1: ");
            string nombre1 = Console.ReadLine() ?? "Jugador1";
            Console.Write("Poder inicial de " + nombre1 + " (entero): ");
            int poder1;
            if (!int.TryParse(Console.ReadLine(), out poder1)) poder1 = 50;

            Console.Write("Nombre del personaje 2: ");
            string nombre2 = Console.ReadLine() ?? "Jugador2";
            Console.Write("Poder inicial de " + nombre2 + " (entero): ");
            int poder2;
            if (!int.TryParse(Console.ReadLine(), out poder2)) poder2 = 50;

            var p1 = new Personaje(nombre1, poder1);
            var p2 = new Personaje(nombre2, poder2);

            string[] posiblesItems = new[] { "Armadura", "Arma", "Poción", "Amuleto" };

            const int turnos = 20;
            for (int turno = 1; turno <= turnos; turno++)
            {
                Console.WriteLine($"--- Turno {turno} ---");

                // turno p1
                Sala sala1 = GenerarSala(rnd, posiblesItems, p1);
                IntentarRobar(p1, sala1);

                // turno p2
                Sala sala2 = GenerarSala(rnd, posiblesItems, p2);
                IntentarRobar(p2, sala2);

                Console.WriteLine($"{p1.Nombre}: Poder={p1.Poder} | Ítems: {p1.ListadoItems()}");
                Console.WriteLine($"{p2.Nombre}: Poder={p2.Poder} | Ítems: {p2.ListadoItems()}");
            }

            Console.WriteLine("=== Resultado final ===");
            Console.WriteLine($"{p1.Nombre}: Poder final = {p1.Poder} | Ítems: {p1.ListadoItems()}");
            Console.WriteLine($"{p2.Nombre}: Poder final = {p2.Poder} | Ítems: {p2.ListadoItems()}");

            if (p1.Poder > p2.Poder)
                Console.WriteLine($"Ganador: {p1.Nombre}");
            else if (p2.Poder > p1.Poder)
                Console.WriteLine($"Ganador: {p2.Nombre}");
            else
                Console.WriteLine("Empate.");
        }

        // Genera sala con dificultad proporcional al poder.
        // El máximo crece según el nivel del jugador.
        static Sala GenerarSala(Random rnd, string[] posiblesItems, Personaje jugador)
        {
            // cuanto se aleja la dificultad: poder/3 o 5 mínimo
            int spread = jugador.Poder / 3;
            if (spread < 5) spread = 5;

            int min = jugador.Poder - spread;
            int max = jugador.Poder + spread;

            // el máximo sube con el nivel
            int adicional = jugador.Poder / 2;
            max = max + adicional;

            if (min < 1) min = 1;
            if (min > max) min = max;

            int dificultad = rnd.Next(min, max + 1);
            string item = posiblesItems[rnd.Next(posiblesItems.Length)];
            int poderItem = rnd.Next(1, 31);
            return new Sala(dificultad, item, poderItem);
        }

        static void IntentarRobar(Personaje p, Sala sala)
        {
            Console.WriteLine($"{p.Nombre} entra en la sala (Dificultad {sala.Dificultad}) y encuentra '{sala.Item}' (+{sala.PoderItem} poder).");
            if (sala.Dificultad < p.Poder)
            {
                bool added = p.AñadirItem(sala.Item, sala.PoderItem);
                if (added)
                    Console.WriteLine($"{p.Nombre} roba el ítem '{sala.Item}' y gana {sala.PoderItem} de poder.");
                else
                    Console.WriteLine($"{p.Nombre} podría robar, pero ya tiene 20 ítems.");
            }
            else
            {
                Console.WriteLine($"{p.Nombre} no puede robar: dificultad demasiado alta.");
            }
        }
    }
}
