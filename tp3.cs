//ejercicio 1
string Ejercicio1(int n)
{
    string resultado;
    if (n > 0)
    {
        resultado = "El número es positivo.";
        return resultado;
    }
    else if (n < 0)
    {
        resultado = "El número es negativo.";
        return resultado;
    }
    else
    {
        resultado = "El número es cero.";
        return resultado;
    }
}

Console.WriteLine(Ejercicio1(Convert.ToInt32(Console.ReadLine())));

//ejercicio 2

string Ejercicio2(int k)
{
    string resultado;
    if (k >= 18)
    {
        resultado = "bienvenido a la fieta";
        return resultado;
    }
    else
    {
        resultado = "lo siento, eres muy joves";
        return resultado;
    }
    
  
}

Console.WriteLine(Ejercicio2(Convert.ToInt32(Console.ReadLine())));

//ejercicio 3


string Ejercicio3(string e)
{
    string contraseña;
    if (e == "phyton123")
    {
        contraseña = "Contraseña correcta Acceso concedido.";
        return contraseña;
    }
    else
    {
        contraseña = "Contraseña incorrecta, Autodestrucción en 5 minutos";
        return contraseña;
    }


}

Console.WriteLine(Ejercicio3(Console.ReadLine()));

//ejercicio 4

string Ejercicio4(int r)
{
    string par;
    if (r % 2 == 0)
    {
        par = "El numero es par";
        return par;
    }
    else
    {
        par = "El numero es impar";
        return par;
    }
}
Console.WriteLine(Ejercicio4(Convert.ToInt32(Console.ReadLine())));

//ejercicio 5

string Ejercicio5(int y, string compro)
{
    string resultado;
    if (y >= 65 && compro == "si")
    {
        resultado = "¡Felicidades! Tienes entrada gratuita al cine";
        return resultado;
    }
    else
    {
        resultado = "Compra la entrada o raja de acá";
        return resultado;
    }
}
Console.WriteLine("Ingrese su edad:");
int edad = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("¿Compraste la palomitas");
string compro = Console.ReadLine();
Console.WriteLine(Ejercicio5(edad, compro));
