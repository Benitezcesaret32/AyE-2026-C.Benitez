//ejercicio 1
Console.WriteLine("ingrese un numero primo");
bool esPrimo = true;
int numero = Convert.ToInt32(Console.ReadLine());
for (int i = 2; i < numero; i++)
{
  if (numero % i == 0)
  {
    esPrimo = false;
    break;
  }
}
if (esPrimo)
{
    Console.WriteLine("el numero es primo");
}
else
{
    Console.WriteLine("el numero no es primo");
}
//ejercicio 2
Console.WriteLine("ingrese un numero entero para ver su factorial");
int numero2 = Convert.ToInt32(Console.ReadLine());
int factorial = 1;
for (int i = 1; i <= numero2; i++)
{
    factorial *= i;
}
Console.WriteLine("el factorial de " + numero2 + " es: " + factorial);
//ejercicio 3
Console.WriteLine("ejercicio 3: Secuencia Fibonacci");
Console.WriteLine("ingrese un numero entero para ver la secuencia Fibonacci hasta ese numero");
int numero3 = Convert.ToInt32(Console.ReadLine());
int a = 0, b = 1, c = 0;
for (int i = 0; i < numero3; i++)
{
    Console.Write(a + " ");
    c = a + b;
    a = b;
    b = c;
}
//ejercicio 4
Console.WriteLine("ejercicio 4: menu intractivo");
Console.WriteLine("elegir una opcion");
Console.WriteLine("1. saludar");
Console.WriteLine("2. despedirse");
Console.WriteLine("3. salir");
bool salir = false;
while (salir == false)
{
    int opcion = Convert.ToInt32(Console.ReadLine());
    switch (opcion)
    {
        case 1:
            Console.WriteLine("hola, bienvenido!");
            break;
        case 2:
            Console.WriteLine("adios, hasta luego!");
            break;
        case 3:
            Console.WriteLine("saliendo del programa...");
            salir = true;
            break;
        default:
            Console.WriteLine("opcion no valida, por favor elija una opcion del menu");
            break;
    }
}           
