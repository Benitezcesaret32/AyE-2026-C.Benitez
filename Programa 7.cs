Console.WriteLine("crear un TaTeTi para jugar de a 2 personas y que funcione y se vea desde la consola y que el jugador 1 sea X y que el jugador 2 sea O");
int jugadorActual = 1;
char[,] tablero = new char[3, 3]
{
    { ' ', ' ', ' ' },
    { ' ', ' ', ' ' },
    { ' ', ' ', ' ' }
};

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Jugador 1: X");
    Console.WriteLine("Jugador 2: O");
    Console.WriteLine();
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            Console.Write(tablero[i, j]);
            if (j < 2) Console.Write("|");
        }
        Console.WriteLine();
        if (i < 2) Console.WriteLine("-----");
    }
    Console.WriteLine();
    Console.WriteLine($"Turno del Jugador {jugadorActual}");
    Console.Write("Ingrese fila (1-3): ");
    int fila = int.Parse(Console.ReadLine());
    Console.Write("Ingrese columna (1-3): ");
    int columna = int.Parse(Console.ReadLine());

    fila = fila - 1;
    columna = columna - 1;

    if (fila < 0 || fila > 2 || columna < 0 || columna > 2)
    {
        Console.WriteLine("Entrada inválida: la fila y la columna deben ser 1, 2 o 3.");
        continue;
    }

    if (tablero[fila, columna] != ' ')
    {
        Console.WriteLine("Esa casilla ya está ocupada.");
        continue;
    }

    char marca = jugadorActual == 1 ? 'X' : 'O';
    tablero[fila, columna] = marca;

    bool HayGanador(char[,] t, char m)
    {
        for (int i = 0; i < 3; i++)
            if (t[i, 0] == m && t[i, 1] == m && t[i, 2] == m) return true;

        for (int j = 0; j < 3; j++)
            if (t[0, j] == m && t[1, j] == m && t[2, j] == m) return true;

        if (t[0, 0] == m && t[1, 1] == m && t[2, 2] == m) return true;
        if (t[0, 2] == m && t[1, 1] == m && t[2, 0] == m) return true;

        return false;
    }

    bool EstaLleno(char[,] t)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (t[i, j] == ' ') return false;
        return true;
    }

    if (HayGanador(tablero, marca))
    {
        Console.WriteLine();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tablero[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("-----");
        }
        Console.WriteLine();
        if (marca == 'X')
            Console.WriteLine("Jugador 1 (X) gana. Jugador 2 pierde.");
        else
            Console.WriteLine("Jugador 2 (O) gana. Jugador 1 pierde.");
        break;
    }

    if (EstaLleno(tablero))
    {
        Console.WriteLine();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tablero[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("-----");
        }
        Console.WriteLine();
        Console.WriteLine("Empate.");
        break;
    }

    jugadorActual = jugadorActual == 1 ? 2 : 1;
}

