// ejercision 1
try
{
    string Ejercicio1(int N)
    {
        string resultado;
        if (N > 0)
        {
            resultado = "El número es positivo.";
            return resultado;
        }
        else if (N < 0)
        {
            resultado = "El número es negativo";
            return resultado;
        }
        else
        {
            resultado = "El número es cero";
            return resultado;
        }
    }

    Console.WriteLine("Ingrese un número");
    Console.WriteLine(Ejercicio1(Convert.ToInt32(Console.ReadLine())));
}
catch (InvalidCastException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (FormatException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (OverflowException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}

// ejercicio 2
try
{
    string Ejercicio2(int Q)
    {
        string resultado;
        if (Q >= 18)
        {
            resultado = "¡Bienvenido a la fiesta!.";
            return resultado;
        }
        else
        {
            resultado = "Lo siento, eres muy joven";
            return resultado;
        }
    }

    Console.WriteLine("Ingrese su edad");
    Console.WriteLine(Ejercicio2(Convert.ToInt32(Console.ReadLine())));
}
catch (InvalidCastException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad válida.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad válida.");
}
catch (FormatException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad válida.");
}
catch (OverflowException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad válida.");
}

// ejercicio 3

try
{
    string Ejercicio3(string contraseña)
    {
        string resultado;
        if (contraseña == "python123")
        {
            resultado = "¡Contraseña correcta! Acceso concedido.";
            return resultado;
        }
        else
        {
            resultado = "¡Contraseña incorrecta, Autodestrucción en 5 minutos!";
            return resultado;
        }
    }

    Console.WriteLine("Ingrese la contraseña");
    Console.WriteLine(Ejercicio3(Console.ReadLine()));
}
catch (InvalidCastException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una contraseña válida.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una contraseña válida.");
}
catch (FormatException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una contraseña válida.");
}
catch (OverflowException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una contraseña válida.");
}

// ejercicio 4
try
{
    string Ejercicio4(int numero)
    {
        string resultado;
        if (numero % 2 == 0)
        {
            resultado = "El número es par.";
            return resultado;
        }
        else
        {
            resultado = "El número es impar.";
            return resultado;
        }
    }

    Console.WriteLine("Ingrese un número");
    Console.WriteLine(Ejercicio4(Convert.ToInt32(Console.ReadLine())));
}
catch (InvalidCastException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (FormatException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}
catch (OverflowException)
{
    Console.WriteLine("¡Error! Por favor, ingresa un número válido.");
}

// ejercicio 5


try
{
    string Ejercicio5(int edad, string compro)
    {
        string resultado;
        if (edad >= 65 && compro == "si")
        {
            resultado = "¡Felicidades! Tienes entrada gratuita al cine.";
            return resultado;
        }
        else
        {
            resultado = "Compra la entrada o raja de acá";
            return resultado;
        }
    }

    Console.WriteLine("Ingrese su edad");
    int edad = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(" ha comprado palomitas de maíz?");
    string compro = Console.ReadLine();
    Console.WriteLine(Ejercicio5(edad, compro));
}
catch (InvalidCastException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad/entrada válida.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad/entrada válida.");
}
catch (FormatException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad/entrada válida.");
}
catch (OverflowException)
{
    Console.WriteLine("¡Error! Por favor, ingresa una edad/entrada válida.");
}