Console.WriteLine("ingrese un numero para calcular su factoriales");
int intentos = 3;
while (intentos > 0)
{
    string input = Console.ReadLine();
    if (Convert.ToInt32(input) >= 0)
    {
        int numero = Convert.ToInt32(input);
        long factorial = 1;
        for (int i = 1; i <= numero; i++)
        {
            factorial *= i;
        }
        Console.WriteLine($"El factorial de {numero} es {factorial}");
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