﻿//Ejercicio 1
Console.WriteLine("Ejercicio 1: Carga y Recorrido Básico");
Console.WriteLine("Ingrese 5 números:");
int[] numeros = new int[5];
for (int i = 0; i < numeros.Length; i++)
{
    Console.Write($"Número {i + 1}: ");
    int numero = int.Parse(Console.ReadLine());
    numeros[i] = numero;
}
Console.WriteLine("Los números ingresados son:");
foreach (int numero in numeros)
{
    Console.WriteLine(numero);
}

//Ejercicio 2
Console.WriteLine("Ejercicio 2: Búsqueda y Conteo");
string[] frutas = new[] { "manzana", "banana", "naranja", "uva", "pera" };
Console.WriteLine("Ingrese el nombre de una fruta:");
string frutaBuscada = Console.ReadLine();
int indice = Array.IndexOf(frutas, frutaBuscada);
if (indice != -1)
{
    Console.WriteLine($"La fruta {frutaBuscada} se encuentra en la posición {indice}.");
}
else
{
    Console.WriteLine($"La fruta {frutaBuscada} no fue encontrada.");
}

//Ejercicio 3
Console.WriteLine("Ejercicio 3: Suma y Promedio");
double[] notas = new double[10];
Console.WriteLine("Ingrese las notas de los estudiantes:");
for (int i = 0; i < notas.Length; i++)
{
    Console.Write($"Nota {i + 1}: ");
    double nota = double.Parse(Console.ReadLine());
    notas[i] = nota;
}
double suma = 0;
foreach (double nota in notas)
{
    suma += nota;
}
double promedio = suma / notas.Length;
Console.WriteLine($"La suma total de las notas es: {suma}");
Console.WriteLine($"El promedio de las notas es: {promedio}");

//Ejercicio 4

Console.WriteLine("Ejercicio 4: Encontrar el Valor Máximo y Mínimo");
double[] temperaturas = new[] { 32.0, 28.0, 30.0, 35.0, 31.0, 29.0, 27.0 };
double max = temperaturas[0];
double min = temperaturas[0];
foreach (double temp in temperaturas)
{
    if (temp > max) max = temp;
    if (temp < min) min = temp;
}
Console.WriteLine($"La temperatura máxima es: {max}");
Console.WriteLine($"La temperatura mínima es: {min}");

//Ejercicio 5
Console.WriteLine("Ejercicio 5: Ordenamiento");
int[] numerosDesordenados = new[] { 5, 2, 9, 1, 4, 6 };
for (int i = 0; i < numerosDesordenados.Length; i++)
{
    for (int j = 0; j < numerosDesordenados.Length - 1; j++)
    {
        if (numerosDesordenados[j] > numerosDesordenados[j + 1])
        {
            int temp = numerosDesordenados[j];
            numerosDesordenados[j] = numerosDesordenados[j + 1];
            numerosDesordenados[j + 1] = temp;
        }
    }
}
Console.WriteLine("Números ordenados:");
foreach (int numero in numerosDesordenados)
{
    Console.WriteLine(numero);
}

//Ejercicio 6
Console.WriteLine("Ejercicio 6: Pares e Impares");
int[] numerosEnteros = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
int pares = 0;
int impares = 0;
foreach (int numero in numerosEnteros)
{
    if (numero % 2 == 0) pares++;
    else impares++;
}
Console.WriteLine($"Cantidad de números pares: {pares}");
Console.WriteLine($"Cantidad de números impares: {impares}");
