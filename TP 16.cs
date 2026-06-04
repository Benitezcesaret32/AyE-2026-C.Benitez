//ejercicio 1
Console.WriteLine("Ejercicio 1");
string [,] matriz = new string[4, 4]
{
    { "1", "2", "3", "4" },
    { "5", "6", "7", "8" },
    { "9", "10", "11", "12" },
    { "13", "14", "15", "16" }
};
int sumaEsquinas = 0;
    sumaEsquinas += int.Parse(matriz[0, 0]);
    sumaEsquinas += int.Parse(matriz[0, 3]);
    sumaEsquinas += int.Parse(matriz[3, 0]);
    sumaEsquinas += int.Parse(matriz[3, 3]);
Console.WriteLine($"La suma de las esquinas es: {sumaEsquinas}");
//ejercicio 2
Console.WriteLine("Ejercicio 2");
int[,] matriz2 = new int[3, 3]
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};
int sumaDiagonalPrincipal = 0;
int sumaDiagonalSecundaria = 0;
for (int i = 0; i < 3; i++)
{
    sumaDiagonalPrincipal += matriz2[i, i];
    sumaDiagonalSecundaria += matriz2[i, 2 - i];
}
Console.WriteLine($"La suma de la diagonal principal es: {sumaDiagonalPrincipal}");
Console.WriteLine($"La suma de la diagonal secundaria es: {sumaDiagonalSecundaria}");
//ejercicio 3
Console.WriteLine("Ejercicio 3");
Console.WriteLine("Ingrese el tamaño de la matriz de identidad:");
int tamaño = int.Parse(Console.ReadLine());
int[,] matrizIdentidad = new int[tamaño, tamaño];
for (int i = 0; i < tamaño; i++)
{
    for (int j = 0; j < tamaño; j++)
    {
        if (i == j)
        {
            matrizIdentidad[i, j] = 1;
        }
        else
        {
            matrizIdentidad[i, j] = 0;
        }
    }
}
Console.WriteLine("Matriz de identidad:");
for (int i = 0; i < tamaño; i++)
{
    for (int j = 0; j < tamaño; j++)
    {
        Console.Write(matrizIdentidad[i, j] + " ");
    }
    Console.WriteLine();
}
//FIN