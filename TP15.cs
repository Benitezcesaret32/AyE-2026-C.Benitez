//ejercicio 1
Console.WriteLine("Ejercicio 1 ");
int[,] matriz =
{
    {1, 2, 3},
    {4, 5, 6},
    {7, 8, 9}
};
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        Console.Write($"{matriz[i, j],3}");
    }
    Console.WriteLine();
}
Console.WriteLine();
//ejercicio 2
Console.WriteLine("Ejercicio 2");
int[,] matriz2 =
{
    {10, 20, 30},
    {40, 50, 60},
    {70, 80, 90}
};
int suma = 0;
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        suma += matriz2[i, j];
    }
}
Console.WriteLine($"La suma de los números en la matriz es: " + suma);
//ejercicio 3
Console.WriteLine("Ejercicio 3");
int[,] matriz3 =
{
    {1, 2, 3, 4},
    {5, 6, 7, 8},
    {9, 10, 11, 12},
    {13, 14, 15, 16}
};
Console.WriteLine("Ingrese la coordenada (fila y columna) para obtener el elemento correspondiente (ejemplo: 1 2):");
string[] input = Console.ReadLine().Split(' ');
int fila = int.Parse(input[0]);
int columna = int.Parse(input[1]);
if (fila >= 0 && fila < 4 && columna >= 0 && columna < 4)
{
    Console.WriteLine($"El elemento en la coordenada ({fila}, {columna}) es: {matriz3[fila, columna]}");
}
else
{
    Console.WriteLine("Coordenada inválida. Por favor ingrese valores entre 0 y 3.");
}
//ejercicio 4
Console.WriteLine("Ejercicio 4");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
int maximo = matriz3[0, 0];
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        if (matriz3[i, j] > maximo)
        {
            maximo = matriz3[i, j];
        }
    }
}
Console.WriteLine($"El número más grande en la matriz es: {maximo}");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
Console.WriteLine();
//ejercicio 5
Console.WriteLine("Ejercicio 5");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
for (int i = 0; i < 4; i++)
{
    int sumaFila = 0;
    for (int j = 0; j < 4; j++)
    {
        sumaFila += matriz3[i, j];
    }
    Console.WriteLine($"La suma de la fila {i} es: {sumaFila}");
}
for (int j = 0; j < 4; j++)
{
    int sumaColumna = 0;
    for (int i = 0; i < 4; i++)
    {
        sumaColumna += matriz3[i, j];
    }
    Console.WriteLine($"La suma de la columna {j} es: {sumaColumna}");
}
Console.WriteLine();
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
Console.WriteLine();
//ejercicio 6
Console.WriteLine("Ejercicio 6");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
int[,]
    transpuesta = new int[4, 4];
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        transpuesta[j, i] = matriz3[i, j];
    }
}
Console.WriteLine("Matriz transpuesta:");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{transpuesta[i, j],3}");
    }
    Console.WriteLine();
}
Console.WriteLine();
//ejercicio 7
Console.WriteLine("Ejercicio 7");
int[,] matriz4 =
{
    {1, 5, 3, 5},
    {8, 5, 9, 2},
    {4, 5, 6, 7}
};
Console.WriteLine("Ingrese un número para buscar en la matriz:");
int numeroBuscado = int.Parse(Console.ReadLine());
int contador = 0;
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 4; j++)
    {
        if (matriz4[i, j] == numeroBuscado)
        {
            contador++;
        }
    }
}
Console.WriteLine($"El número {numeroBuscado} aparece {contador} veces en la matriz.");
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz4[i, j],3}");
    }
    Console.WriteLine();
}
//ejercicio 8
Console.WriteLine("Ejercicio 8");
Console.WriteLine();
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matriz3[i, j],3}");
    }
    Console.WriteLine();
}
int sumaTotal = 0;
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        sumaTotal += matriz3[i, j];
    }
}
int promedio = (sumaTotal + 8) / 16;
Console.WriteLine($"El promedio de los números en la matriz es: {promedio}");
int[,] matrizPromedio = new int[4, 4];
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        matrizPromedio[i, j] = matriz3[i, j] < promedio ? promedio : matriz3[i, j];
    }
}
Console.WriteLine("Matriz con números menores al promedio reemplazados por el promedio:");
for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write($"{matrizPromedio[i, j],3}");
    }
    Console.WriteLine();
}
Console.WriteLine();
//FIN
