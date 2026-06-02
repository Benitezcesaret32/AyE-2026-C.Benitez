//ejercicio 1
Console.WriteLine("Crear una matriz 3x3 que contenga números del 1 al 9, luego imprimirla en consola de manera que se vea como una cuadricula con otra forma de hacerlo");
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
Console.WriteLine("Crear una matriz 3x3 que contenga números del 10 al 90, luego imprimir la suma de todos sus números.");
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
Console.WriteLine("Crear una matriz 4x4 y llenarla de elementos, luego hacer un programa que deje al usuario ingresar una coordenada, luego que muestre el elemento que contiene ese mismo índice.");
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
Console.WriteLine("Crear una matriz 4x4 y llenarla de numeros, luego hacer un programa que muestre el numero mas grande de la matriz.");
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
Console.WriteLine("Crear una matriz 4x4 con números, luego hacer un programa que muestre la suma de cada fila y luego que muestre la suma de cada columna");
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
Console.WriteLine("Crear una matriz 4x4 con números, luego hacer un programa que transponga la matriz (intercambia la columnas por las filas y las filas por las columnas)");
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
Console.WriteLine("Escribir un programa que busque un numero ingresado por el usuario y devuelva la cantidad de veces que aparece en la matriz y la matris es esta  [1, 5, 3, 5],    [8, 5, 9, 2],   [4, 5, 6, 7]");
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
Console.WriteLine("Crear una matriz 4x4 con números Hacer un programa que calcule el promedio de todos los números Luego el programa debe de crear otra matriz que sea la misma, con la diferencia de que todos los números menores al promedio van a ser cambiados por el promedio.");
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
