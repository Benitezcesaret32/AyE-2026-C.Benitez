//ejercicio 1
Console.WriteLine ("ingrese 0 o 1");
int numero = Convert.ToInt32(Console.ReadLine());
while (numero != 0 && numero != 1)
{
    Console.WriteLine("ingrese 0 o 1");
    numero = Convert.ToInt32(Console.ReadLine());
}

//ejercicio 2
Console.WriteLine("ingrese un numero de dos cifras");
int numero2 = Convert.ToInt32(Console.ReadLine());
while (numero2 < 10 || numero2 > 99)
{
    Console.WriteLine("ingrese un numero de dos cifras");
    numero2 = Convert.ToInt32(Console.ReadLine());
}