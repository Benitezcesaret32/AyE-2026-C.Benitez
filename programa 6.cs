Console.WriteLine("Buscador de tesoros - Matriz 10x10");
string[,] matriz = new string[10, 10];
bool[,] intentosHechos = new bool[10, 10];
Random random = new Random();

for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        matriz[i, j] = "Nada";
    }
}

int colocados = 0;
while (colocados < 3)
{
    int x = random.Next(0, 10);
    int y = random.Next(0, 10);
    if (matriz[x, y] != "Tesoro")
    {
        matriz[x, y] = "Tesoro";
        colocados++;
    }
}

void MostrarMapa(bool mostrarTesoros = false)
{
    Console.WriteLine();
    Console.WriteLine("Mapa (E = Encontrado, X = Intento, . = Sin probar):");
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            string c;
            if (matriz[i, j] == "Encontrado")
            {
                c = "E";
            }
            else if (intentosHechos[i, j])
            {
                c = "X";
            }
            else if (mostrarTesoros && matriz[i, j] == "Tesoro")
            {
                c = "T";
            }
            else
            {
                c = ".";
            }

            Console.Write(c + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

int intentos = 5;
int tesorosEncontrados = 0;

while (intentos > 0 && tesorosEncontrados < 3)
{
    MostrarMapa();
    Console.WriteLine($"Intentos restantes: {intentos} | Tesoros encontrados: {tesorosEncontrados}/3");
    Console.Write("Ingrese la coordenada X (0-9): ");
    string? inputX = Console.ReadLine();
    Console.Write("Ingrese la coordenada Y (0-9): ");
    string? inputY = Console.ReadLine();

    if (inputX == null || inputX.Length == 0 || inputY == null || inputY.Length == 0)
    {
        Console.WriteLine("Coordenadas inválidas. Introduce números entre 0 y 9.");
        continue;
    }

    int x;
    int y;
    try
    {
        x = Convert.ToInt32(inputX);
        y = Convert.ToInt32(inputY);
    }
    catch
    {
        Console.WriteLine("Coordenadas inválidas. Introduce números entre 0 y 9.");
        continue;
    }

    if (x < 0 || x > 9 || y < 0 || y > 9)
    {
        Console.WriteLine("Coordenadas fuera de rango. Usa valores entre 0 y 9.");
        continue;
    }

    if (intentosHechos[x, y])
    {
        Console.WriteLine("Ya has intentado esta casilla.");
        continue;
    }

    intentosHechos[x, y] = true;
    string estado = matriz[x, y];
    if (estado == "Tesoro")
    {
        Console.WriteLine("¡Encontraste un tesoro!");
        matriz[x, y] = "Encontrado";
        tesorosEncontrados++;
        intentos = 5;
    }
    else
    {
        Console.WriteLine("No hay nada aquí.");
        intentos--;
    }
}

Console.WriteLine();
if (tesorosEncontrados == 3)
{
    Console.WriteLine("¡Felicidades! Has encontrado todos los tesoros.");
}
else
{
    Console.WriteLine("¡Has perdido! No te quedan más intentos.");
}

Console.WriteLine();
Console.WriteLine("Mapa final (T = Tesoro sin encontrar, E = Encontrado, . = Nada):");
MostrarMapa(mostrarTesoros: true);

