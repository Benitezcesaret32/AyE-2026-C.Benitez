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
        resultado = "Compra la entrada o fuera del cine";
        return resultado;
    }
}
Console.WriteLine("Ingrese un numero de 1 al 5.");
Console.WriteLine("Para terminar, ingrese 0 o cualquier numero que no sea de 1 al 5");
int x = Convert.ToInt32(Console.ReadLine());
switch (x)
{
    case 1:
        Console.WriteLine("Ingrese un numero");
        Console.WriteLine(Ejercicio1(Convert.ToInt32(Console.ReadLine())));
        break;
    case 2:
        Console.WriteLine("Ingrese su edad");
        Console.WriteLine(Ejercicio2(Convert.ToInt32(Console.ReadLine())));
        break;
    case 3:
        Console.WriteLine("Ingrese la contraseña");
        Console.WriteLine(Ejercicio3(Console.ReadLine()));
        break;
    case 4:
        Console.WriteLine("Ingrese un número");
        Console.WriteLine(Ejercicio4(Convert.ToInt32(Console.ReadLine())));
        break;
    case 5:
        Console.WriteLine("Ingrese su edad");
        int edad = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(" ha comprado palomitas de maíz?");
        string compro = Console.ReadLine();
        Console.WriteLine(Ejercicio5(edad, compro));
        break;
    default:
        Console.WriteLine("Tarea terminada");
        break;
}
