//ejercicio 1
Console.WriteLine("ingrese 5 numeros para ponerlos en una lista y luego recorrer para mostrarlos sin usar add");
List<int> numeros = new List<int> { 0, 0, 0, 0, 0 };
for (int i = 0; i < 5; i++)
    {
    Console.WriteLine($"Ingrese el número {i + 1}:");
    int numero = int.Parse(Console.ReadLine());
    numeros[i] = numero;
}
Console.WriteLine("Los números ingresados son:");
for (int i = 0; i < numeros.Count; i++)
    {
    Console.WriteLine(numeros[i]);
}

//ejercicio 2
//Partiendo de una lista de nombres de frutas ya definida, el programa debe pedirle al usuario que ingrese el nombre de una fruta. Luego, debe buscar si esa fruta está en la lista y, si la encuentra, imprimir en qué posición (índice) se encuentra. Si la fruta no está, debe mostrar un mensaje informando que no fue encontrada.
Console.WriteLine("ejercicio 2");
while (true)
{
    List<string> frutas = new List<string> { "manzana", "banana", "naranja", "pera", "uva" };
    Console.WriteLine("Ingrese el nombre de una fruta:");
    string frutaBuscada = Console.ReadLine().ToLower();
    int indice = frutas.IndexOf(frutaBuscada);
    if (indice != -1)
    {
        Console.WriteLine($"La fruta '{frutaBuscada}' se encuentra en la posición (índice) {indice}.");
    }
    else
    {
        Console.WriteLine($"La fruta '{frutaBuscada}' no fue encontrada en la lista.");
    }
    break;
        }
//ejercicio 3
Console.WriteLine("ingrese 10 notas de estudiantes para calcular su suma y promedio sin usar sum y sin count");
List<double> notas = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
for (int i = 0; i < 10;i++)
    {
    Console.WriteLine($"Ingrese la nota del estudiante {i + 1}:");
    double nota = double.Parse(Console.ReadLine());
    notas[i] = nota;
}
double suma = 0;
for (int i = 0; i < notas.Count; i++)
    {
    suma += notas[i];
}
double promedio = suma / 10;
Console.WriteLine($"La suma de las notas es: {suma}");
Console.WriteLine($"El promedio de las notas es: {promedio}");
//ejercicio 4
Console.WriteLine("crea una lista de temperaturas diarias, encuentra y muestra la temperatura maxima y la minima sin usar max ni min");
int dias = 7;
for (int i = 0; i < dias; i++)
    {
    Console.WriteLine($"Ingrese la temperatura del día {i + 1}:");
    double temperatura = double.Parse(Console.ReadLine());
    notas[i] = temperatura;
}
double temperaturaMaxima = notas[0];
double temperaturaMinima = notas[0];
for (int i = 1; i < notas.Count; i++)
    {
    if (notas[i] > temperaturaMaxima)
        {
        temperaturaMaxima = notas[i];
        }
    if (notas[i] < temperaturaMinima)
        {
        temperaturaMinima = notas[i];
        }
    }
Console.WriteLine($"La temperatura máxima es: {temperaturaMaxima}");
Console.WriteLine($"La temperatura mínima es: {temperaturaMinima}");
//ejercicio 5
Console.WriteLine("crea una lista de numeros desordenados y que el programa la ordene de forma ascendente y luego mostrarla");
int
    numerosDesordenados = 10;
for (int i = 0; i < numerosDesordenados; i++)
    {
    Console.WriteLine($"Ingrese el número {i + 1}:");
    int numero = int.Parse(Console.ReadLine());
    notas[i] = numero;
    }
for (int i = 0; i < notas.Count - 1; i++)
{
    for (int j = 0; j < notas.Count - i - 1; j++)
        {
        if (notas[j] > notas[j + 1])
            {
            double temp = notas[j];
            notas[j] = notas[j + 1];
            notas[j + 1] = temp;
            }
    }
}
Console.WriteLine("Los números ordenados de forma ascendente son:");
for (int i = 0; i < notas.Count; i++)
    {
    Console.WriteLine(notas[i]);
}
//ejercicio 6
Console.WriteLine("dado 15 numeros enteros crear un programa que cuente cuantos de esos numeros son pares y cuantos son impares al final mostrar el conteo de ambas");
int numerosEnteros = 15;
int conteoPares = 0;
for (int i = 0; i < numerosEnteros; i++)
    {
    Console.WriteLine($"Ingrese el número entero {i + 1}:");
    int numero = int.Parse(Console.ReadLine());
    if (numero % 2 == 0)
        {
        conteoPares++;
        }
}
int conteoImpares = numerosEnteros - conteoPares;
Console.WriteLine($"Cantidad de números pares: {conteoPares}");
Console.WriteLine($"Cantidad de números impares: {conteoImpares}");

