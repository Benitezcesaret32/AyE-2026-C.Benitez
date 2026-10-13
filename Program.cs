using System;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Base_de_Datos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            while (opcion != 6)
            {
                Console.WriteLine();
                Console.WriteLine("1. Consultar agentes y buscar por ID");
                Console.WriteLine("2. Agregar agente");
                Console.WriteLine("3. Actualizar agente");
                Console.WriteLine("4. Eliminar agente");
                Console.WriteLine("5. Ordenar agentes alfabeticamente");
                Console.WriteLine("6. Salir");
                Console.WriteLine("Ingrese una opcion:");

                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1)
                {
                    ConsultarAgentes();
                }
                else if (opcion == 2)
                {
                    AgregarAgente();
                }
                else if (opcion == 3)
                {
                    Console.WriteLine("Ingrese el ID del agente:");
                    int id = int.Parse(Console.ReadLine());

                    ActualizarAgente(id);
                }
                else if (opcion == 4)
                {
                    Console.WriteLine("Ingrese el ID del agente:");
                    int id = int.Parse(Console.ReadLine());

                    EliminarAgente(id);
                }
                else if (opcion == 5)
                {
                    OrdenarAgentes();
                }
                else if (opcion == 6)
                {
                    Console.WriteLine("Programa finalizado");
                }
                else
                {
                    Console.WriteLine("Opcion incorrecta");
                }
            }
        }

        static void ConsultarAgentes()
        {
            using var db = new AppDbContext();

            Agente[] agentes = db.agentes_valorant.ToArray();

            for (int i = 0; i < agentes.Length; i++)
            {
                Console.WriteLine(
                    agentes[i].id + " - " +
                    agentes[i].nombre_agente + " - " +
                    agentes[i].tipo_agente + " - " +
                    agentes[i].nacionalidad + " - " +
                    agentes[i].fecha_lanzamiento
                );
            }

            ArbolBinario arbol = new ArbolBinario();

            for (int i = 0; i < agentes.Length; i++)
            {
                arbol.Insertar(agentes[i]);
            }

            Console.WriteLine("Ingrese el ID del agente que desea buscar:");
            int id = int.Parse(Console.ReadLine());

            Agente buscado = arbol.Buscar(id);

            if (buscado != null)
            {
                Console.WriteLine("Agente encontrado: " + buscado.nombre_agente);
                Console.WriteLine("Tipo: " + buscado.tipo_agente);
                Console.WriteLine("Nacionalidad: " + buscado.nacionalidad);
            }
            else
            {
                Console.WriteLine("No se encontro el agente");
            }
        }

        static void AgregarAgente()
        {
            using var db = new AppDbContext();

            Agente nuevo = new Agente();

            Console.WriteLine("Nombre del agente:");
            nuevo.nombre_agente = Console.ReadLine();

            Console.WriteLine("Tipo de agente:");
            nuevo.tipo_agente = Console.ReadLine();

            Console.WriteLine("Nacionalidad:");
            nuevo.nacionalidad = Console.ReadLine();

            Console.WriteLine("Fecha de lanzamiento:");
            nuevo.fecha_lanzamiento = Console.ReadLine();

            db.agentes_valorant.Add(nuevo);

            db.SaveChanges();

            Console.WriteLine("Agente agregado");
        }

        static void ConsultarAgente(int id)
        {
            using var db = new AppDbContext();

            Agente agente = db.agentes_valorant.Find(id);

            if (agente != null)
            {
                Console.WriteLine(agente.nombre_agente);
                Console.WriteLine(agente.tipo_agente);
                Console.WriteLine(agente.nacionalidad);
                Console.WriteLine(agente.fecha_lanzamiento);
            }
            else
            {
                Console.WriteLine("No se encontro el agente");
            }
        }

        static void ActualizarAgente(int id)
        {
            using var db = new AppDbContext();

            Agente agente = db.agentes_valorant.Find(id);

            if (agente != null)
            {
                Console.WriteLine("Nuevo nombre:");
                agente.nombre_agente = Console.ReadLine();

                Console.WriteLine("Nuevo tipo:");
                agente.tipo_agente = Console.ReadLine();

                Console.WriteLine("Nueva nacionalidad:");
                agente.nacionalidad = Console.ReadLine();

                Console.WriteLine("Nueva fecha de lanzamiento:");
                agente.fecha_lanzamiento = Console.ReadLine();

                db.SaveChanges();

                Console.WriteLine("Agente actualizado");
            }
            else
            {
                Console.WriteLine("No se encontro el agente");
            }
        }

        static void EliminarAgente(int id)
        {
            using var db = new AppDbContext();

            Agente agente = db.agentes_valorant.Find(id);

            if (agente != null)
            {
                db.agentes_valorant.Remove(agente);

                db.SaveChanges();

                Console.WriteLine("Agente eliminado");
            }
            else
            {
                Console.WriteLine("No se encontro el agente");
            }
        }

        static void OrdenarAgentes()
        {
            using var db = new AppDbContext();

            Agente[] agentes = db.agentes_valorant.ToArray();

            for (int i = 0; i < agentes.Length - 1; i++)
            {
                for (int j = 0; j < agentes.Length - 1 - i; j++)
                {
                    if (agentes[j].nombre_agente.CompareTo(agentes[j + 1].nombre_agente) > 0)
                    {
                        Agente auxiliar = agentes[j];

                        agentes[j] = agentes[j + 1];

                        agentes[j + 1] = auxiliar;
                    }
                }
            }

            Console.WriteLine("Agentes ordenados alfabeticamente:");

            for (int i = 0; i < agentes.Length; i++)
            {
                Console.WriteLine(agentes[i].nombre_agente);
            }
        }
    }
}