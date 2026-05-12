//ejercicio 1
Console.WriteLine("ingrese cuantas horas trabajo asi sabemos su salario si trabaja 40 horas o menos se le paga $16 por hora y si trabaja 40 horas o mas se le paga 16 y por las horas extras pagar $20 por hora");
try
{
        Console.WriteLine("Ingrese el número de horas trabajadas:");
        int horasTrabajadas = Convert.ToInt32(Console.ReadLine());
        int salario;
        if (horasTrabajadas <= 40)
        {
            salario = horasTrabajadas * 16;
        }
        else
        {
            int horasExtras = horasTrabajadas - 40;
            salario = (40 * 16) + (horasExtras * 20);
        }
        Console.WriteLine("El salario total es: $" + salario);
}
    catch (FormatException)
    {
        Console.WriteLine("Por favor, ingrese un número válido.");
}
//ejercicio 2
Console.WriteLine("ingrese numeros para sumarlos cuando ingrese 0");
 try
{
        int suma = 0;
        int numero;
        while (true)
        {
            Console.WriteLine("Ingrese un número (0 para terminar):");
            numero = Convert.ToInt32(Console.ReadLine());
            suma += numero;
            if (numero == 0)
            {
                break;
            }
        }
        Console.WriteLine("La suma de los números ingresados es: " + suma);
}
    catch (FormatException)
    {
        Console.WriteLine("Por favor, ingrese un número válido.");
    }

    //ejercicio 3
    Console.WriteLine("ingrese una palabra para contar cuantas vocales tiene y que la funcion no resiva parametros");
    try
    {
        Console.WriteLine("Ingrese una palabra:");
        string palabra = Console.ReadLine();
        int contadorVocales = 0; int contador = 0;
        while (true)
        {
            if (contador == palabra.Length) { break; }
            char letra = palabra[contador];
            if (letra == 'a' || letra == 'e' || letra == 'i' || letra == 'o' || letra == 'u' ||
                letra == 'A' || letra == 'E' || letra == 'I' || letra == 'O' || letra == 'U')
            {
                contadorVocales++;
            }
            contador++;
        }
        Console.WriteLine("La cantidad de vocales en la palabra es: " + contadorVocales);
    }
    catch (FormatException)
    {
        Console.WriteLine("ingrese una palabra valida");
    }
//ejercicio 4
Console.WriteLine("ingrese una palabra que sea palindromo y si no lo es volver a pedir la palabra hasta que lo sea");
try
{
        while (true)
        {
            Console.WriteLine("Ingrese una palabra:");
            string palabra = Console.ReadLine();
            string palabraReversa = "";
            for (int i = palabra.Length - 1; i >= 0; i--)
            {
                palabraReversa += palabra[i];
            }
            if (palabra == palabraReversa)
            {
                Console.WriteLine("La palabra es un palíndromo.");
                break;
            }
            else
            {
                Console.WriteLine("La palabra no es un palíndromo. Intente nuevamente.");
            }
        }
}
    catch (FormatException)
    {
        Console.WriteLine("ingrese una palabra valida");
    }
