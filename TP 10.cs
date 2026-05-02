// Ejercicio 1
Console.WriteLine("Ejercicio 1");
Console.WriteLine("Ingrese un número de dos cifras:");
int num1 = Convert.ToInt32(Console.ReadLine());

if (num1 >= 10 && num1 <= 99)
{
    Console.WriteLine("Unidades: " + (num1 % 10));
    Console.WriteLine("Decenas: " + (num1 / 10));
}
else Console.WriteLine("Error");


// Ejercicio 2
Console.WriteLine("Ejercicio 2");
Console.WriteLine("Indicar un número de tres cifras:");
int num2 = Convert.ToInt32(Console.ReadLine());

if (num2 >= 100 && num2 <= 999)
{
    Console.WriteLine("Valor: " + num2);
    Console.WriteLine("Cifra central: " + ((num2 / 10) % 10));
}
else Console.WriteLine("Error");


// Ejercicio 3 (sin decimales)
Console.WriteLine("Ejercicio 3");
Console.WriteLine("Indicar precio de lista:");
int precio = Convert.ToInt32(Console.ReadLine());

if (precio > 0)
{
    int descuento = precio * 18 / 100;
    int final = precio - descuento;
    Console.WriteLine("Precio con descuento: $" + final);
}
else Console.WriteLine("Error");


// Ejercicio 4
Console.WriteLine("Ejercicio 4");
Console.WriteLine("Número de dos cifras:");
int num4 = Convert.ToInt32(Console.ReadLine());

if (num4 >= 10 && num4 <= 99)
{
    int perm = (num4 % 10) * 10 + (num4 / 10);
    Console.WriteLine("Permutado: " + perm);
}
else Console.WriteLine("Error");


// Ejercicio 5
Console.WriteLine("Ejercicio 5");
Console.WriteLine("Fecha (6 dígitos):");
int fecha = Convert.ToInt32(Console.ReadLine());

if (fecha >= 100000 && fecha <= 999999)
{
    int mes = (fecha / 100) % 100;

    if (mes >= 1 && mes <= 12)
        Console.WriteLine("Mes: " + mes);
    else
        Console.WriteLine("Mes inválido");
}
else Console.WriteLine("Error");


// Ejercicio 6
Console.WriteLine("Ejercicio 6");
Console.WriteLine("Lado del cuadrado:");
int lado = Convert.ToInt32(Console.ReadLine());

if (lado > 0)
{
    Console.WriteLine("Perímetro: " + (lado * 4));
    Console.WriteLine("Superficie: " + (lado * lado));
}
else Console.WriteLine("Error");


// Ejercicio 7
Console.WriteLine("Ejercicio 7");
Console.WriteLine("Valor por hora:");
int valor = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Horas trabajadas:");
int horas = Convert.ToInt32(Console.ReadLine());

if (valor > 0 && horas >= 0)
{
    Console.WriteLine("Sueldo: " + (valor * horas));
}
else Console.WriteLine("Error");


// Ejercicio 8 (aproximado)
Console.WriteLine("Ejercicio 8");
Console.WriteLine("Temperatura en Fahrenheit:");
int f = Convert.ToInt32(Console.ReadLine());

int c = (f - 32) * 5 / 9;
Console.WriteLine("Celsius: " + c);


// Ejercicio 9
Console.WriteLine("Ejercicio 9");
Console.WriteLine("Dados la hora, minutos y los segundos de dos instantes, la computadora muestra cuantos segundos contiene el intervalo que abarcan.:");
Console.WriteLine("Hora1:");
int h1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Min1:");
int m1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Seg1:");
int s1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Hora2:");
int h2 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Min2:");
int m2 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Seg2:");
int s2 = Convert.ToInt32(Console.ReadLine());

if (h1 >= 0 && h1 < 24 && m1 >= 0 && m1 < 60 && s1 >= 0 && s1 < 60 &&
    h2 >= 0 && h2 < 24 && m2 >= 0 && m2 < 60 && s2 >= 0 && s2 < 60)
{
    int t1 = h1 * 3600 + m1 * 60 + s1;
    int t2 = h2 * 3600 + m2 * 60 + s2;

    Console.WriteLine("Intervalo: " + (t2 - t1) + " segundos");
}
else Console.WriteLine("Error");


// Ejercicio 10
Console.WriteLine("Ejercicio 10");
Console.WriteLine("Número de 3 cifras:");
int num10 = Convert.ToInt32(Console.ReadLine());

if (num10 >= 100 && num10 <= 999)
{
    Console.WriteLine("Unidades: " + (num10 % 10));
    Console.WriteLine("Decenas: " + ((num10 / 10) % 10));
    Console.WriteLine("Centenas: " + (num10 / 100));
}
else Console.WriteLine("Error");
