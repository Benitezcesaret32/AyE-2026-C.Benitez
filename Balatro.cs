Console.WriteLine("crear un menu que al usuario le de las opciones de pedir, descartar o dejar de jugar, si elije pedir se le da 8 cartas, si elije descartar pedir cuantas va a descartar yy luego pedirle una por una cuales quiere descartar, y si elije dejar de jugar se termina el programa");
while (true)
{
    Console.WriteLine("Elige una opción: 1. Pedir cartas, 2. Descartar cartas, 3. Dejar de jugar");
    string opcion = Console.ReadLine();
    if (opcion == "1")
    {
        Console.WriteLine("Has pedido 8 cartas.");
    }
    else if (opcion == "2")
    {
        Console.WriteLine("¿Cuántas cartas quieres descartar?");
        int cantidadDescartar = int.Parse(Console.ReadLine());
        Console.WriteLine($"Has decidido descartar {cantidadDescartar} cartas.");
        for (int i = 0; i < cantidadDescartar; i++)
        {
            Console.WriteLine($"Ingresa el nombre de la carta {i + 1} que deseas descartar:");
            string cartaDescartada = Console.ReadLine();
            Console.WriteLine($"Has descartado la carta: {cartaDescartada}");
        }
    }
    else if (opcion == "3")
    {
        Console.WriteLine("Has decidido dejar de jugar. ¡Gracias por jugar!");
        break;
    }
    else
    {
        Console.WriteLine("Opción no válida. Por favor, elige una opción válida.");
    }
}
