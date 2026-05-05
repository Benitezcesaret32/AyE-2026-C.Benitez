Console.WriteLine("Juego del ahorcado - 7 intentos");
string palabra = "pepito";
char[] oculto = new char[palabra.Length];
for (int i = 0; i < oculto.Length; i++) oculto[i] = '_';
int intentos = 7;

while (intentos > 0)
{
    bool quedanGuiones = false;
    for (int i = 0; i < oculto.Length; i++)
    {
        if (oculto[i] == '_') { quedanGuiones = true; break; }
    }
    if (!quedanGuiones) break;

    Console.Write("Palabra: ");
    for (int i = 0; i < oculto.Length; i++) Console.Write(oculto[i]);
    Console.WriteLine();
    Console.WriteLine($"Intentos restantes: {intentos}");
    Console.Write("Ingresa una letra: ");

    string input = Console.ReadLine() ?? "";
    if (input.Length == 0)
    {
        Console.WriteLine("Entrada vacía. Intenta otra letra.");
        continue;
    }

    char letra = input[0];
    bool acierto = false;
    for (int i = 0; i < palabra.Length; i++)
    {
        if (palabra[i] == letra)
        {
            oculto[i] = letra;
            acierto = true;
        }
    }

    if (!acierto)
    {
        intentos--;
        Console.WriteLine("Letra incorrecta.");
    }
}

bool completado = true;
for (int i = 0; i < oculto.Length; i++)
{
    if (oculto[i] == '_') { completado = false; break; }
}

if (completado)
    Console.WriteLine($"¡Felicidades! Has adivinado la palabra: {palabra}");
else
    Console.WriteLine($"Has perdido. La palabra era: {palabra}");