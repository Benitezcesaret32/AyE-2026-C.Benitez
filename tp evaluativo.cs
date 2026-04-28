using System.Diagnostics;

Console.WriteLine("ingrese un numero para calcular su factoriales sin numero negativo ni caracteres especiales");
int numero;
while (true)
{
    numero = Convert.ToInt32(Console.ReadLine());
    bool tieneEspecial = false;
    for (int i = 0; i < numero; i++)
        numero = Convert.ToInt32(Console.ReadLine().ToLower());
    if (tieneEspecial)
        {
        Console.WriteLine("No es válido. Por favor, ingrese un número sin caracteres especiales.");
        continue;
    }
    if (numero >= 0)
    {
        break;
    }
    else
    {
        Console.WriteLine("No es válido. Por favor, ingrese un número no negativo.");
    }
}
int intentos = 3;
while (intentos > 0)
{
    string input = Console.ReadLine();
    if (Convert.ToInt32(input) >= 0)
    {
        int numeroInput = Convert.ToInt32(input);
        long factorial = 1;
        for (int i = 1; i <= numeroInput; i++)
        {
            factorial *= i;
        }
        Console.WriteLine($"El factorial de {numeroInput} es {factorial}");
        intentos = 3;
    }
    else
    {
        intentos--;
        if (intentos > 0)
        {
            Console.WriteLine($"No es válido. Intentos restantes: {intentos}");
        }
        else
        {
            Console.WriteLine("No tiene más intentos.");
            break;
        }
    }
}
