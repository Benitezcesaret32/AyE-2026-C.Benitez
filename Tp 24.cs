
using System;
using System.IO;

string rutaSalida = "trabajo.txt";

using (StreamWriter writer = new StreamWriter(rutaSalida, append: false))
{
    // Ejercicio 1
    writer.WriteLine("Ejercicio 1");
    int num1 = 45;

    if (num1 >= 10 && num1 <= 99)
    {
        writer.WriteLine("Unidades: " + (num1 % 10));
        writer.WriteLine("Decenas: " + (num1 / 10));
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 2
    writer.WriteLine("Ejercicio 2");
    int num2 = 357;

    if (num2 >= 100 && num2 <= 999)
    {
        writer.WriteLine("Valor: " + num2);
        writer.WriteLine("Cifra central: " + ((num2 / 10) % 10));
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 3
    writer.WriteLine("Ejercicio 3");
    int precio = 1000;

    if (precio > 0)
    {
        int descuento = precio * 18 / 100;
        int final = precio - descuento;
        writer.WriteLine("Precio con descuento: $" + final);
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 4
    writer.WriteLine("Ejercicio 4");
    int num4 = 27;

    if (num4 >= 10 && num4 <= 99)
    {
        int perm = (num4 % 10) * 10 + (num4 / 10);
        writer.WriteLine("Permutado: " + perm);
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 5
    writer.WriteLine("Ejercicio 5");
    int fecha = 151024;

    if (fecha >= 100000 && fecha <= 999999)
    {
        int mes = (fecha / 100) % 100;

        if (mes >= 1 && mes <= 12)
            writer.WriteLine("Mes: " + mes);
        else
            writer.WriteLine("Mes inválido");
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 6
    writer.WriteLine("Ejercicio 6");
    int lado = 5;

    if (lado > 0)
    {
        writer.WriteLine("Perímetro: " + (lado * 4));
        writer.WriteLine("Superficie: " + (lado * lado));
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 7
    writer.WriteLine("Ejercicio 7");
    int valor = 2000;
    int horas = 8;

    if (valor > 0 && horas >= 0)
    {
        writer.WriteLine("Sueldo: " + (valor * horas));
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 8
    writer.WriteLine("Ejercicio 8");
    int f = 86;

    int c = (f - 32) * 5 / 9;
    writer.WriteLine("Celsius: " + c);

    // Ejercicio 9
    writer.WriteLine("Ejercicio 9");
    int h1 = 10;
    int m1 = 30;
    int s1 = 0;
    int h2 = 11;
    int m2 = 0;
    int s2 = 0;

    if (h1 >= 0 && h1 < 24 && m1 >= 0 && m1 < 60 &&
        s1 >= 0 && s1 < 60 && h2 >= 0 && h2 < 24 &&
        m2 >= 0 && m2 < 60 && s2 >= 0 && s2 < 60)
    {
        int t1 = h1 * 3600 + m1 * 60 + s1;
        int t2 = h2 * 3600 + m2 * 60 + s2;

        writer.WriteLine("Intervalo: " + (t2 - t1) + " segundos");
    }
    else
    {
        writer.WriteLine("Error");
    }

    // Ejercicio 10
    writer.WriteLine("Ejercicio 10");
    int num10 = 456;

    if (num10 >= 100 && num10 <= 999)
    {
        writer.WriteLine("Unidades: " + (num10 % 10));
        writer.WriteLine("Decenas: " + ((num10 / 10) % 10));
        writer.WriteLine("Centenas: " + (num10 / 100));
    }
    else
    {
        writer.WriteLine("Error");
    }
}

using (StreamReader reader = new StreamReader(rutaSalida))
{
    string? linea;

    while ((linea = reader.ReadLine()) != null)
    {
        Console.WriteLine(linea);
    }
}
