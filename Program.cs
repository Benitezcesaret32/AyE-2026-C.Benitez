Console.WriteLine("Ingrese su nombre");
    string nombre = Console.ReadLine();
Console.WriteLine("Ingrese su promedio");
    float promedio = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Ingrese la distancia en kilómetros desde su casa a la universidad");
   int distancia = Convert.ToInt32(Console.ReadLine());

bool DeterminarBeca (float promedio, int distancia)
{
    if (promedio >= 8.5 || distancia > 50)
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool resultado = DeterminarBeca(promedio, distancia);
if (resultado == true)
{
    Console.Write("Felicidades, ");
    Console.Write(nombre);
    Console.Write(" tu beca ha sido aprobada.");
}
else
{
    Console.Write("Lo sentimos,");
    Console.Write(nombre);
    Console.Write(" no cumples con los requisitos mínimos.");
}

