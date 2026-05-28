Console.WriteLine("crea un menu donde pueda pedir 8 cartas o descartar o salir, si descartas tenes que poner cuantas descarta y cuales cartas y luego agregar una listas de las descartadas sin add y sin count y sin remove y tenes que usar random");

List<string> cartas = new List<string>();
List<string> descartadas = new List<string>();
foreach (var cart in cartas)
{
    Console.WriteLine(cart);
}
string[] mazo = { "As de corazones", "2 de corazones", "3 de corazones", "4 de corazones", "5 de corazones", "6 de corazones", "7 de corazones", "8 de corazones", "9 de corazones", "10 de corazones", "J de corazones", "Q de corazones", "K de corazones",
                    "As de diamantes", "2 de diamantes", "3 de diamantes", "4 de diamantes", "5 de diamantes", "6 de diamantes", "7 de diamantes", "8 de diamantes", "9 de diamantes", "10 de diamantes", "J de diamantes", "Q de diamantes", "K de diamantes",
                    "As de treboles", "2 de treboles", "3 de treboles", "4 de treboles", "5 de treboles", "6 de treboles", "7 de treboles", "8 de treboles", "9 de treboles", "10 de treboles", "J de treboles", "Q de treboles", "K de treboles",
                    "As de picas", "2 de picas", "3 de picas", "4 de picas", "5 de picas", "6 de picas", "7 de picas", "8 de picas", "9 de picas", "10 de picas", "J de picas", "Q de picas","K  picas" };
Random random = new Random();
while (true)
{
    Console.WriteLine("1. Pedir cartas");
    Console.WriteLine("2. Descartar cartas");
    Console.WriteLine("3. Salir");
    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();
    if (opcion == "1")
    {
        while (cartas.Count < 8)
        {
            int index = random.Next(mazo.Length);
            string carta = mazo[index];
            if (!cartas.Contains(carta))
            {
                cartas.Add(carta);
            }
        }
        Console.WriteLine("Cartas pedidas:");
        foreach (var cart in cartas)
        {
            Console.WriteLine(cart);
        }
    }
    else if (opcion == "2")
    {
        Console.Write("¿Cuántas cartas desea descartar? ");
        int cantidadDescartar = int.Parse(Console.ReadLine());
        for (int i = 0; i < cantidadDescartar; i++)
        {
            Console.Write("Ingrese la carta a descartar: ");
            string cartaDescartar = Console.ReadLine();
            if (cartas.Contains(cartaDescartar))
            {
                descartadas.Add(cartaDescartar);
                cartas.Remove(cartaDescartar);
            }
            else
            {
                Console.WriteLine("La carta no está en su mano.");
            }
        }
        Console.WriteLine("Cartas descartadas:");
        foreach (var cart in descartadas)
        {
            Console.WriteLine(cart);
        }
    }
    else if (opcion == "3")
    {
        break;
    }
    else
    {
        Console.WriteLine("Opción no válida.");
    }
}