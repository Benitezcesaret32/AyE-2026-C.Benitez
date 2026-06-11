using System;

Console.WriteLine("Balatro 2 - versión con matrices");

string[] palos = { "corazones", "diamantes", "treboles", "picas" };
string[] valores = { "As", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

string[,] mazo = new string[4, 13];
for (int p = 0; p < 4; p++)
{
    for (int v = 0; v < 13; v++)
    {
        mazo[p, v] = valores[v] + " de " + palos[p];
    }
}

string[,] cartas = new string[8, 1];

string[,] descartadas = new string[4, 13];

Random rnd = new Random();

for (int i = 0; i < 8; i++)
{
    int restantes = 0;
    for (int p = 0; p < 4; p++)
        for (int v = 0; v < 13; v++)
            if (mazo[p, v] != null) restantes++;

    if (restantes == 0)
    {
        cartas[i, 0] = null;
        continue;
    }

    int objetivo = rnd.Next(restantes);
    int contador = 0;
    for (int p = 0; p < 4 && contador <= objetivo; p++)
    {
        for (int v = 0; v < 13; v++)
        {
            if (mazo[p, v] == null) continue;
            if (contador == objetivo)
            {
                cartas[i, 0] = mazo[p, v];
                mazo[p, v] = null;
                contador++;
                break;
            }
            contador++;
        }
    }
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("¿Qué quieres hacer? (1: Ver mano, 2: Descartar cartas, 3: Ver descartadas, 4: Salir)");
    string opcion = Console.ReadLine();

    if (opcion == "1")
    {
        Console.WriteLine("Tus cartas:");
        for (int i = 0; i < 8; i++)
        {
            string shown = cartas[i, 0] == null || cartas[i, 0] == "" ? "(vacía)" : cartas[i, 0];
            Console.WriteLine($"{i + 1}: {shown}");
        }
    }
    else if (opcion == "2")
    {
        int cantidad;
        while (true)
        {
            Console.WriteLine("¿Cuántas cartas quieres descartar? (1-8)");
            string entrada = Console.ReadLine();
            try
            {
                cantidad = int.Parse(entrada);
                if (cantidad < 1 || cantidad > 8)
                {
                    Console.WriteLine("Número fuera de rango. Intenta de nuevo.");
                    continue;
                }
                break;
            }
            catch
            {
                Console.WriteLine("Entrada no válida. Escribe un número.");
            }
        }

        for (int d = 0; d < cantidad; d++)
        {
            Console.WriteLine("Selecciona la carta a descartar (por número):");
            for (int i = 0; i < 8; i++)
            {
                string shown = cartas[i, 0] == null || cartas[i, 0] == "" ? "(vacía)" : cartas[i, 0];
                Console.WriteLine($"{i + 1}: {shown}");
            }

            int selIndex;
            while (true)
            {
                string sel = Console.ReadLine();
                try
                {
                    int parsed = int.Parse(sel);
                    if (parsed < 1 || parsed > 8)
                    {
                        Console.WriteLine("Índice fuera de rango. Intenta de nuevo.");
                        continue;
                    }
                    selIndex = parsed - 1;
                    if (cartas[selIndex, 0] == null || cartas[selIndex, 0] == "")
                    {
                        Console.WriteLine("Esa posición está vacía. Elige otra.");
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Entrada inválida. Escribe un número.");
                }
            }

            bool guardado = false;
            for (int p = 0; p < 4 && !guardado; p++)
            {
                for (int v = 0; v < 13; v++)
                {
                    if (descartadas[p, v] == null)
                    {
                        descartadas[p, v] = cartas[selIndex, 0];
                        guardado = true;
                        break;
                    }
                }
            }

            int restantes = 0;
            for (int p = 0; p < 4; p++)
                for (int v = 0; v < 13; v++)
                    if (mazo[p, v] != null) restantes++;

            if (restantes == 0)
            {
                Console.WriteLine("No quedan cartas en el mazo. La posición quedará vacía.");
                cartas[selIndex, 0] = null;
            }
            else
            {
                int objetivo = rnd.Next(restantes);
                int contador = 0;
                bool robado = false;
                for (int p = 0; p < 4 && !robado; p++)
                {
                    for (int v = 0; v < 13; v++)
                    {
                        if (mazo[p, v] == null) continue;
                        if (contador == objetivo)
                        {
                            cartas[selIndex, 0] = mazo[p, v];
                            mazo[p, v] = null;
                            robado = true;
                            break;
                        }
                        contador++;
                    }
                }
            }

            Console.WriteLine("Carta descartada y reemplazada.");
        }
    }
    else if (opcion == "3")
    {
        Console.WriteLine("Cartas descartadas:");
        int contador = 0;
        for (int p = 0; p < 4; p++)
        {
            for (int v = 0; v < 13; v++)
            {
                if (descartadas[p, v] != null && descartadas[p, v] != "")
                {
                    contador++;
                    Console.WriteLine($"{contador}: {descartadas[p, v]}");
                }
            }
        }
        if (contador == 0) Console.WriteLine("(ninguna)");
    }
    else if (opcion == "4")
    {
        Console.WriteLine("¡Gracias por jugar!");
        break;
    }
    else
    {
        Console.WriteLine("Opción no válida. Elige 1, 2, 3 o 4.");
    }
}





