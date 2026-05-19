// Ejercicio 1
void MostrarNumeros(int numero)
{
    if (numero <= 10)
    {
        Console.WriteLine(numero);
        MostrarNumeros(numero + 1);
    }
}
MostrarNumeros(0);

// Ejercicio 2
void MostrarHola(int veces)
{
    if (veces < 5)
    {
        Console.WriteLine("Hola Mundo");
        MostrarHola(veces + 1);
    }
}
MostrarHola(0);

// Ejercicio 3
void MostrarPares(int numeroPar)
{
    if (numeroPar <= 20)
    {
        Console.WriteLine(numeroPar);
        MostrarPares(numeroPar + 2);
    }
}
MostrarPares(2);

// Ejercicio 4
void MostrarMultiplos7(int multiplo)
{
    if (multiplo <= 70)
    {
        Console.WriteLine(multiplo);
        MostrarMultiplos7(multiplo + 7);
    }
}
MostrarMultiplos7(7);

// Ejercicio 5
void MostrarSuma(int numero, int suma)
{
    if (numero <= 5)
    {
        suma = suma + numero;
        Console.WriteLine(suma);
        MostrarSuma(numero + 1, suma);
    }
}

MostrarSuma(1, 0);
