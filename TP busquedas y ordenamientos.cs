namespace ConsoleApp73
{
    using System;

    class Program
    {
        // Lista de 50 elementos desordenados.
        static int[] vector = {
        37, 12, 45, 3, 28,
        19, 50, 7, 31, 22,
        14, 41, 5, 26, 9,
        34, 18, 2, 47, 30,
        11, 39, 24, 6, 43,
        16, 29, 1, 35, 21,
        48, 13, 8, 32, 40,
        17, 4, 27, 46, 10,
        33, 23, 49, 15, 38,
        25, 20, 36, 44, 42
    };

        // RESPUESTAS GENERALES (breve):
        // - ¿Cuál es la búsqueda más eficiente?
        // Búsqueda binaria (si el array está ordenado). Complejidad: O(log n).
        // - ¿Cuál es el ordenamiento más eficiente?
        // QuickSort en promedio. Complejidad promedio: O(n log n).
        // - ¿Qué es la complejidad algorítmica?
        // Medida del coste en tiempo/espacio de un algoritmo según el tamaño n (notación Big-O).

        // --------------------
        // BÚSQUEDAS
        // --------------------

        // Breve: Secuencial simple = recorre cada elemento hasta encontrarlo.
        // Condición: ninguna (funciona en listas desordenadas).
        // Complejidad: O(n).
        static int BusquedaSecuencial(int[] lista, int buscado)
        {
            for (int i = 0; i < lista.Length; i++)
                if (lista[i] == buscado) return i;
            return -1;
        }

        // Breve: Secuencial optimizada = recorre y corta si el valor supera al buscado.
        // Condición: lista ordenada ascendentemente.
        // Complejidad: O(n) (mejor O(1) si está al inicio).
        static int BusquedaSecuencialOptimizada(int[] lista, int buscado)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] == buscado) return i;
                if (lista[i] > buscado) return -1;
            }
            return -1;
        }

        // Breve: Binaria iterativa = divide el rango a la mitad cada paso.
        // Condición: lista ordenada ascendentemente.
        // Complejidad: O(log n).
        static int BusquedaBinaria(int[] lista, int buscado)
        {
            int inicio = 0, fin = lista.Length - 1;
            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                if (lista[medio] == buscado) return medio;
                if (lista[medio] < buscado) inicio = medio + 1;
                else fin = medio - 1;
            }
            return -1;
        }

        // Breve: Binaria recursiva = versión recursiva de la binaria.
        // Condición: lista ordenada ascendentemente.
        // Complejidad: O(log n). Usa pila de llamadas O(log n).
        static int BusquedaBinariaRecursiva(int[] lista, int buscado, int inicio, int fin)
        {
            if (inicio > fin) return -1;
            int medio = (inicio + fin) / 2;
            if (lista[medio] == buscado) return medio;
            if (lista[medio] < buscado) return BusquedaBinariaRecursiva(lista, buscado, medio + 1, fin);
            return BusquedaBinariaRecursiva(lista, buscado, inicio, medio - 1);
        }

        // --------------------
        // ORDENAMIENTOS
        // --------------------

        // Breve: Burbuja clásico = compara vecinos y los intercambia repetidamente.
        // Condición: ninguna; educativo o para arrays pequeños.
        // Complejidad: O(n^2).
        static void BurbujaClasico(int[] lista)
        {
            for (int i = 0; i < lista.Length - 1; i++)
                for (int j = 0; j < lista.Length - 1; j++)
                    if (lista[j] > lista[j + 1])
                    {
                        int aux = lista[j]; lista[j] = lista[j + 1]; lista[j + 1] = aux;
                    }
            Mostrar(lista);
        }

        // Breve: Burbuja optimizado = igual que clásico, pero detecta si no hubo intercambios y termina antes.
        // Condición: ninguna.
        // Complejidad: O(n^2), mejor O(n) si ya está ordenado.
        static void BurbujaOptimizado(int[] lista)
        {
            for (int i = 0; i < lista.Length - 1; i++)
            {
                bool cambio = false;
                for (int j = 0; j < lista.Length - 1 - i; j++)
                    if (lista[j] > lista[j + 1])
                    {
                        int aux = lista[j]; lista[j] = lista[j + 1]; lista[j + 1] = aux;
                        cambio = true;
                    }
                if (!cambio) break;
            }
            Mostrar(lista);
        }

        // Breve: Selección = busca el menor en la parte no ordenada y lo coloca en i.
        // Condición: ninguna.
        // Complejidad: O(n^2).
        static void Seleccion(int[] lista)
        {
            for (int i = 0; i < lista.Length - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < lista.Length; j++)
                    if (lista[j] < lista[menor]) menor = j;
                int aux = lista[i]; lista[i] = lista[menor]; lista[menor] = aux;
            }
            Mostrar(lista);
        }

        // Breve: Inserción = inserta cada elemento en la sublista izquierda ya ordenada.
        // Condición: ninguna; muy eficiente si la lista está casi ordenada.
        // Complejidad: O(n^2), mejor O(n).
        static void Insercion(int[] lista)
        {
            for (int i = 1; i < lista.Length; i++)
            {
                int clave = lista[i];
                int j = i - 1;
                while (j >= 0 && lista[j] > clave)
                {
                    lista[j + 1] = lista[j];
                    j--;
                }
                lista[j + 1] = clave;
            }
            Mostrar(lista);
        }

        // Breve: QuickSort = divide y vence: particiona por un pivote y ordena recursivamente.
        // Condición: ninguna; en promedio es muy eficiente.
        // Complejidad: promedio O(n log n), peor O(n^2).
        static void QuickSort(int[] lista, int inicio, int fin)
        {
            if (inicio >= fin) return;
            int i = inicio, j = fin;
            int pivote = lista[(inicio + fin) / 2];
            while (i <= j)
            {
                while (lista[i] < pivote) i++;
                while (lista[j] > pivote) j--;
                if (i <= j)
                {
                    int aux = lista[i]; lista[i] = lista[j]; lista[j] = aux;
                    i++; j--;
                }
            }
            if (inicio < j) QuickSort(lista, inicio, j);
            if (i < fin) QuickSort(lista, i, fin);
        }

        // Breve: Wrapper QuickSort que ordena y muestra (misma complejidad que QuickSort).
        static void OrdenarConQuick(int[] lista)
        {
            QuickSort(lista, 0, lista.Length - 1);
            Mostrar(lista);
        }

        // Breve: StalinFiltro = recorre y elimina cualquier elemento mayor que el último conservado.
        // Condición: destructivo (aceptar pérdida de elementos); no produce un array completamente ordenado.
        // Complejidad: O(n).
        static void StalinFiltro(int[] lista)
        {
            if (lista.Length == 0) { Console.WriteLine(); return; }
            int ultimo = lista[0];
            Console.Write(ultimo + " ");
            for (int i = 1; i < lista.Length; i++)
                if (lista[i] <= ultimo)
                {
                    ultimo = lista[i];
                    Console.Write(ultimo + " ");
                }
            Console.WriteLine();
        }

        // Breve: Bogo = baraja aleatoriamente hasta que quede ordenado (impráctico).
        // Condición: ninguno útil; extremadamente lento.
        // Complejidad: exponencial/factorial en esperanza.
        static void Bogo(int[] lista)
        {
            Random rnd = new Random();
            while (!Ordenada(lista))
            {
                for (int i = 0; i < lista.Length; i++)
                {
                    int pos = rnd.Next(lista.Length);
                    int aux = lista[i]; lista[i] = lista[pos]; lista[pos] = aux;
                }
            }
            Mostrar(lista);
        }

        // Breve: Comprueba si el vector está ordenado ascendentemente.
        // Complejidad: O(n).
        static bool Ordenada(int[] lista)
        {
            for (int i = 0; i < lista.Length - 1; i++)
                if (lista[i] > lista[i + 1]) return false;
            return true;
        }

        // Breve: Muestra el array por consola.
        // Complejidad: O(n).
        static void Mostrar(int[] lista)
        {
            foreach (int v in lista) Console.Write(v + " ");
            Console.WriteLine();
        }

        // Breve: Copia manual del array sin usar librerías.
        // Complejidad: O(n).
        static int[] Copiar(int[] lista)
        {
            int[] copia = new int[lista.Length];
            for (int i = 0; i < lista.Length; i++) copia[i] = lista[i];
            return copia;
        }

        // MAIN interactivo (menú para búsquedas y ordenamientos)
        static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("VECTOR (50 elementos):");
                Mostrar(vector);

                Console.WriteLine();
                Console.WriteLine("Menú principal:");
                Console.WriteLine("1 - Búsquedas");
                Console.WriteLine("2 - Ordenamientos");
                Console.WriteLine("0 - Salir");
                Console.Write("Opción: ");

                string opcionPrincipal = Console.ReadLine()?.Trim();
                if (opcionPrincipal == "0") break;

                if (opcionPrincipal == "1")
                {
                    EjecutarMenuBusquedas();
                }
                else if (opcionPrincipal == "2")
                {
                    EjecutarMenuOrdenamientos();
                }
                else
                {
                    Console.WriteLine("Opción inválida.");
                }
            }
        }

        static void EjecutarMenuBusquedas()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Elija tipo de búsqueda:");
                Console.WriteLine("1 - Secuencial simple");
                Console.WriteLine("2 - Secuencial optimizada (requiere array ordenado)");
                Console.WriteLine("3 - Binaria iterativa (requiere array ordenado)");
                Console.WriteLine("4 - Binaria recursiva (requiere array ordenado)");
                Console.WriteLine("0 - Volver");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine()?.Trim();
                if (opcion == "0") return;

                if (opcion != "1" && opcion != "2" && opcion != "3" && opcion != "4")
                {
                    Console.WriteLine("Opción inválida.");
                    continue;
                }

                Console.Write("Ingrese número a buscar: ");
                if (!int.TryParse(Console.ReadLine(), out int buscado))
                {
                    Console.WriteLine("Entrada inválida.");
                    continue;
                }

                int posicion = -1;

                if (opcion == "2" || opcion == "3" || opcion == "4")
                {
                    int[] copia = Copiar(vector);
                    QuickSort(copia, 0, copia.Length - 1);
                    Console.WriteLine("Array ordenado (usado para la búsqueda):");
                    Mostrar(copia);

                    if (opcion == "2") posicion = BusquedaSecuencialOptimizada(copia, buscado);
                    else if (opcion == "3") posicion = BusquedaBinaria(copia, buscado);
                    else posicion = BusquedaBinariaRecursiva(copia, buscado, 0, copia.Length - 1);
                }
                else
                {
                    posicion = BusquedaSecuencial(vector, buscado);
                }

                if (posicion >= 0) Console.WriteLine($"Elemento encontrado en posición: {posicion}");
                else Console.WriteLine("Elemento no encontrado.");

                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
            }
        }

        static void EjecutarMenuOrdenamientos()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Elija ordenamiento:");
                Console.WriteLine("1 - Burbuja clásico");
                Console.WriteLine("2 - Burbuja optimizado");
                Console.WriteLine("3 - Selección");
                Console.WriteLine("4 - Inserción");
                Console.WriteLine("5 - QuickSort");
                Console.WriteLine("6 - Stalin (filtro destructivo)");
                Console.WriteLine("7 - Bogo (extremadamente lento, confirmar)");
                Console.WriteLine("0 - Volver");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine()?.Trim();
                if (opcion == "0") return;

                int[] copia = Copiar(vector);

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Burbuja clásico sobre copia del array:");
                        BurbujaClasico(copia);
                        break;
                    case "2":
                        Console.WriteLine("Burbuja optimizado sobre copia del array:");
                        BurbujaOptimizado(copia);
                        break;
                    case "3":
                        Console.WriteLine("Selección sobre copia del array:");
                        Seleccion(copia);
                        break;
                    case "4":
                        Console.WriteLine("Inserción sobre copia del array:");
                        Insercion(copia);
                        break;
                    case "5":
                        Console.WriteLine("QuickSort sobre copia del array:");
                        OrdenarConQuick(copia);
                        break;
                    case "6":
                        Console.WriteLine("Stalin (se mostrará la subsecuencia conservada):");
                        StalinFiltro(copia);
                        break;
                    case "7":
                        if (copia.Length > 8)
                        {
                            Console.WriteLine("Bogo puede tardar muchísimo con arrays grandes. ¿Continuar? (s/n): ");
                            string confirmar = Console.ReadLine()?.Trim().ToLower();
                            if (confirmar != "s")
                            {
                                Console.WriteLine("Bogo cancelado.");
                                break;
                            }
                        }
                        Console.WriteLine("Bogo (barajando hasta quedar ordenado) — esto puede tardar mucho:");
                        Bogo(copia);
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("Presione Enter para continuar...");
                Console.ReadLine();
            }
        }
    }
}
