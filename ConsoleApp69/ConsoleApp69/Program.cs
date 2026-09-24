namespace Pokedex
{
    internal class Program
    {
        static ArbolBinario arbol = new ArbolBinario();

        static void Main(string[] args)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.Clear();
                Console.WriteLine("             POKEDEX");
                Console.WriteLine("1 - Consultar todos los Pokemon");
                Console.WriteLine("2 - Consultar Pokemon por ID");
                Console.WriteLine("3 - Agregar Pokemon");
                Console.WriteLine("4 - Actualizar Pokemon");
                Console.WriteLine("5 - Eliminar Pokemon");
                Console.WriteLine("6 - Crear arbol binario");
                Console.WriteLine("7 - Mostrar arbol");
                Console.WriteLine("8 - Buscar Pokemon en el arbol");
                Console.WriteLine("0 - Salir");
                Console.Write("Ingrese una opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        ConsultarTodos();
                        break;

                    case 2:
                        ConsultarPorId();
                        break;

                    case 3:
                        AgregarPokemon();
                        break;

                    case 4:
                        ActualizarPokemon();
                        break;

                    case 5:
                        EliminarPokemon();
                        break;

                    case 6:
                        CrearArbol();
                        break;

                    case 7:
                        MostrarArbol();
                        break;

                    case 8:
                        BuscarEnArbol();
                        break;

                    case 0:
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opcion incorrecta.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        static void ConsultarTodos()
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Pokemon> pokemons = db.Pokemons
                    .OrderBy(p => p.Id)
                    .ToList();

                Console.WriteLine("POKEMON");

                foreach (Pokemon pokemon in pokemons)
                {
                    Console.WriteLine(pokemon);
                }

                Console.WriteLine();
                Console.WriteLine("Cantidad: " + pokemons.Count);
            }
        }

        static void ConsultarPorId()
        {
            Console.Write("Ingrese el ID del Pokemon: ");
            int id = LeerEntero();

            using (AppDbContext db = new AppDbContext())
            {
                Pokemon pokemon = db.Pokemons.Find(id);

                if (pokemon == null)
                {
                    Console.WriteLine("No se encontro ese Pokemon.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Pokemon encontrado:");
                    Console.WriteLine(pokemon);
                }
            }
        }

        static void AgregarPokemon()
        {
            Console.WriteLine("AGREGAR POKEMON");

            Pokemon pokemon = new Pokemon();

            Console.Write("ID: ");
            pokemon.Id = LeerEntero();

            Console.Write("Nombre: ");
            pokemon.Nombre = Console.ReadLine();

            Console.Write("Tipo 1: ");
            pokemon.Tipo1 = Console.ReadLine();

            Console.Write("Tipo 2 (dejar vacio si no tiene): ");
            string tipo2 = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tipo2))
            {
                pokemon.Tipo2 = null;
            }
            else
            {
                pokemon.Tipo2 = tipo2;
            }

            Console.Write("HP: ");
            pokemon.Hp = LeerEntero();

            Console.Write("Ataque: ");
            pokemon.Ataque = LeerEntero();

            Console.Write("Defensa: ");
            pokemon.Defensa = LeerEntero();

            Console.Write("Ataque especial: ");
            pokemon.AtaqueEspecial = LeerEntero();

            Console.Write("Defensa especial: ");
            pokemon.DefensaEspecial = LeerEntero();

            Console.Write("Velocidad: ");
            pokemon.Velocidad = LeerEntero();

            Console.Write("Nivel: ");
            pokemon.Nivel = LeerEntero();

            using (AppDbContext db = new AppDbContext())
            {
                Pokemon existente = db.Pokemons.Find(pokemon.Id);

                if (existente != null)
                {
                    Console.WriteLine("Ya existe un Pokemon con ese ID.");
                    return;
                }

                db.Pokemons.Add(pokemon);
                db.SaveChanges();
            }

            Console.WriteLine();
            Console.WriteLine("Pokemon agregado correctamente.");
        }

        static void ActualizarPokemon()
        {
            Console.WriteLine("ACTUALIZAR POKEMON");

            Console.Write("Ingrese el ID del Pokemon: ");
            int id = LeerEntero();

            using (AppDbContext db = new AppDbContext())
            {
                Pokemon pokemon = db.Pokemons.Find(id);

                if (pokemon == null)
                {
                    Console.WriteLine("No se encontro ese Pokemon.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Pokemon actual:");
                Console.WriteLine(pokemon);

                Console.WriteLine();
                Console.WriteLine("Ingrese los nuevos datos:");

                Console.Write("Nombre: ");
                pokemon.Nombre = Console.ReadLine();

                Console.Write("Tipo 1: ");
                pokemon.Tipo1 = Console.ReadLine();

                Console.Write("Tipo 2 (dejar vacio si no tiene): ");
                string tipo2 = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(tipo2))
                {
                    pokemon.Tipo2 = null;
                }
                else
                {
                    pokemon.Tipo2 = tipo2;
                }

                Console.Write("HP: ");
                pokemon.Hp = LeerEntero();

                Console.Write("Ataque: ");
                pokemon.Ataque = LeerEntero();

                Console.Write("Defensa: ");
                pokemon.Defensa = LeerEntero();

                Console.Write("Ataque especial: ");
                pokemon.AtaqueEspecial = LeerEntero();

                Console.Write("Defensa especial: ");
                pokemon.DefensaEspecial = LeerEntero();

                Console.Write("Velocidad: ");
                pokemon.Velocidad = LeerEntero();

                Console.Write("Nivel: ");
                pokemon.Nivel = LeerEntero();

                db.SaveChanges();
            }

            Console.WriteLine();
            Console.WriteLine("Pokemon actualizado correctamente.");
        }

        static void EliminarPokemon()
        {
            Console.WriteLine("ELIMINAR POKEMON");

            Console.Write("Ingrese el ID del Pokemon: ");
            int id = LeerEntero();

            using (AppDbContext db = new AppDbContext())
            {
                Pokemon pokemon = db.Pokemons.Find(id);

                if (pokemon == null)
                {
                    Console.WriteLine("No se encontro ese Pokemon.");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Pokemon a eliminar:");
                Console.WriteLine(pokemon);

                Console.WriteLine();
                Console.Write("¿Seguro que quiere eliminarlo? (s/n): ");

                string respuesta = Console.ReadLine();

                if (respuesta.ToLower() == "s")
                {
                    db.Pokemons.Remove(pokemon);
                    db.SaveChanges();

                    Console.WriteLine("Pokemon eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine("Operacion cancelada.");
                }
            }
        }

        static void CrearArbol()
        {
            Console.WriteLine("CREAR ARBOL");

            arbol.Limpiar();

            using (AppDbContext db = new AppDbContext())
            {
                List<Pokemon> pokemons = db.Pokemons
                    .OrderBy(p => p.Id)
                    .ToList();

                foreach (Pokemon pokemon in pokemons)
                {
                    arbol.Insertar(pokemon);
                }

                Console.WriteLine("Arbol creado correctamente.");
                Console.WriteLine("Pokemon cargados: " + pokemons.Count);
            }
        }

        static void MostrarArbol()
        {
            Console.WriteLine("ARBOL BINARIO");

            if (arbol.Raiz == null)
            {
                Console.WriteLine("El arbol esta vacio.");
                Console.WriteLine("Primero seleccione la opcion 6.");
                return;
            }

            arbol.MostrarEnOrden();
        }

        static void BuscarEnArbol()
        {
            Console.WriteLine("BUSCAR EN ARBOL");

            if (arbol.Raiz == null)
            {
                Console.WriteLine("El arbol esta vacio.");
                Console.WriteLine("Primero seleccione la opcion 6.");
                return;
            }

            Console.Write("Ingrese el ID que quiere buscar: ");
            int id = LeerEntero();

            Pokemon pokemon = arbol.Buscar(id);

            Console.WriteLine();

            if (pokemon == null)
            {
                Console.WriteLine("No se encontro ese Pokemon.");
            }
            else
            {
                Console.WriteLine("Pokemon encontrado:");
                Console.WriteLine(pokemon);
            }
        }

        static int LeerEntero()
        {
            int numero;

            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Ingrese un numero valido: ");
            }

            return numero;
        }
    }
}