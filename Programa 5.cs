string[] nombres = new string[100];
string[] telefonos = new string[100];
int total = 0;
void Agregarcontactos() {
    Console.Write("Nombre: "); nombres[total] = Console.ReadLine();
    Console.Write("Teléfono: "); telefonos[total] = Console.ReadLine();
    total++;
}

void Vercontactos()
{
    for (int i = 0; i < total; i++)
        Console.WriteLine($"{nombres[i]} → {telefonos[i]}");
}

void Buscarcontactos()
{
    Console.Write("Nombre: ");
    string buscar = Console.ReadLine();
    bool encontrado = false;
    for (int i = 0; i < total; i++)
        if (nombres[i] == buscar) { Console.WriteLine($"{nombres[i]} → {telefonos[i]}"); encontrado = true; }
    if (!encontrado) Console.WriteLine("No encontrado.");
}
    while (true)
    {
    Console.WriteLine("1.Agregar contactos 2.Ver contactos  3.Buscar contactos 4.Salir");
    switch (Console.ReadLine())
    {
        case "1":
            Agregarcontactos();
            break;
        case "2":
            Vercontactos();
            break;
        case "3":
            Buscarcontactos();
            break;
        case "4":
            return;
    }
    }
