using System;
using System.Collections.Generic;

namespace GoleadoresMundial
{
    class Program
    {
        static JugadorDatos datos = new JugadorDatos();

        static List<Jugador> goleadores_mundial =
            new List<Jugador>();

        static ArbolBinario arbol =
            new ArbolBinario();

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("     GOLEADORES DEL MUNDIAL");
                Console.WriteLine("================================");
                Console.WriteLine("1 - Consultar jugadores");
                Console.WriteLine("2 - Agregar jugador");
                Console.WriteLine("3 - Actualizar jugador");
                Console.WriteLine("4 - Eliminar jugador");
                Console.WriteLine("5 - Crear árbol binario");
                Console.WriteLine("6 - Buscar en árbol");
                Console.WriteLine("7 - Lista de jugadores");
                Console.WriteLine("8 - Ordenar por nombre");
                Console.WriteLine("9 - Ordenar por mundiales");
                Console.WriteLine("0 - Salir");
                Console.WriteLine("================================");

                Console.Write("Opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        Consultar();
                        break;

                    case 2:
                        Agregar();
                        break;

                    case 3:
                        Actualizar();
                        break;

                    case 4:
                        Eliminar();
                        break;

                    case 5:
                        CrearArbol();
                        break;

                    case 6:
                        BuscarArbol();
                        break;

                    case 7:
                        CargarLista();
                        break;

                    case 8:
                        OrdenarNombre();
                        break;

                    case 9:
                        OrdenarMundiales();
                        break;

                    case 0:
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Presione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 0);
        }

        static void Consultar()
        {
            try
            {
                goleadores_mundial = datos.Consultar();

                foreach (Jugador jugador in goleadores_mundial)
                {
                    Console.WriteLine(jugador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Agregar()
        {
            Jugador jugador = new Jugador();

            Console.Write("Nombre: ");
            jugador.nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            jugador.apellido = Console.ReadLine();

            Console.Write("País: ");
            jugador.pais = Console.ReadLine();

            Console.Write("Posición: ");
            jugador.posicion = Console.ReadLine();

            Console.Write("Goles: ");
            int.TryParse(
                Console.ReadLine(),
                out int goles);

            jugador.goles = goles;

            Console.Write("Mundiales jugados: ");
            int.TryParse(
                Console.ReadLine(),
                out int mundiales);

            jugador.mundiales_jugados = mundiales;

            try
            {
                datos.Agregar(jugador);
                Console.WriteLine(
                    "Jugador agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Actualizar()
        {
            Jugador jugador = new Jugador();

            Console.Write("ID del jugador: ");
            int.TryParse(
                Console.ReadLine(),
                out int id);

            jugador.id = id;

            Console.Write("Nuevo nombre: ");
            jugador.nombre = Console.ReadLine();

            Console.Write("Nuevo apellido: ");
            jugador.apellido = Console.ReadLine();

            Console.Write("Nuevo país: ");
            jugador.pais = Console.ReadLine();

            Console.Write("Nueva posición: ");
            jugador.posicion = Console.ReadLine();

            Console.Write("Nuevos goles: ");
            int.TryParse(
                Console.ReadLine(),
                out int goles);

            jugador.goles = goles;

            Console.Write("Nuevos mundiales: ");
            int.TryParse(
                Console.ReadLine(),
                out int mundiales);

            jugador.mundiales_jugados = mundiales;

            try
            {
                datos.Actualizar(jugador);

                Console.WriteLine(
                    "Jugador actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Eliminar()
        {
            Console.Write("ID del jugador: ");

            int.TryParse(
                Console.ReadLine(),
                out int id);

            try
            {
                datos.Eliminar(id);

                Console.WriteLine(
                    "Jugador eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void CrearArbol()
        {
            goleadores_mundial = datos.Consultar();

            arbol = new ArbolBinario();

            foreach (Jugador jugador in goleadores_mundial)
            {
                arbol.Insertar(jugador);
            }

            Console.WriteLine(
                "Árbol creado correctamente.");

            Console.WriteLine();
            Console.WriteLine(
                "Jugadores del árbol:");

            arbol.Mostrar();
        }

        static void BuscarArbol()
        {
            Console.Write("Apellido a buscar: ");

            string apellido =
                Console.ReadLine();

            Jugador jugador =
                arbol.Buscar(apellido);

            if (jugador != null)
            {
                Console.WriteLine();
                Console.WriteLine("Jugador encontrado:");
                Console.WriteLine(jugador);
            }
            else
            {
                Console.WriteLine(
                    "Jugador no encontrado.");
            }
        }

        static void CargarLista()
        {
            goleadores_mundial = datos.Consultar();

            Console.WriteLine(
                "Lista cargada correctamente.");

            Console.WriteLine(
                "Cantidad de jugadores: " +
                goleadores_mundial.Count);

            Console.WriteLine();

            foreach (Jugador jugador in goleadores_mundial)
            {
                Console.WriteLine(jugador);
            }
        }

        static void OrdenarNombre()
        {
            goleadores_mundial = datos.Consultar();

            Ordenamientos.BurbujaAlfabetico(
                goleadores_mundial);

            Console.WriteLine(
                "Jugadores ordenados alfabéticamente:");

            Console.WriteLine();

            foreach (Jugador jugador in goleadores_mundial)
            {
                Console.WriteLine(jugador);
            }
        }

        static void OrdenarMundiales()
        {
            goleadores_mundial = datos.Consultar();

            Ordenamientos.BurbujaMundiales(
                goleadores_mundial);

            Console.WriteLine(
                "Jugadores ordenados por mundiales:");

            Console.WriteLine();

            foreach (Jugador jugador in goleadores_mundial)
            {
                Console.WriteLine(jugador);
            }
        }
    }
}


