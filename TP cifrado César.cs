Console.WriteLine("ingrese una frase para cifrarla o descifrarla sin caracteres especiales");
string frase;
while (true)
{
    frase = Console.ReadLine().ToLower();

    bool tieneEspecial = false;
    for (int i = 0; i < frase.Length; i++)
    {
        char letra = frase[i];
        if ((letra < 'a' || letra > 'z') && letra != ' ')
        {
            tieneEspecial = true;
            break;
        }
    }

    if (tieneEspecial)
    {
        Console.WriteLine("La frase contiene caracteres especiales. Por favor, ingrese una frase sin caracteres especiales.");
        continue;
    }

    break;
}

Console.WriteLine("ingrese el numero de desplazamiento: ");
string entradaDesplazamiento = Console.ReadLine();
int desplazamiento;
bool esNumeroValido = int.TryParse(entradaDesplazamiento, out desplazamiento);
if (esNumeroValido)
{
    Console.WriteLine("Número de desplazamiento válido: " + desplazamiento);
}
else
{
    Console.WriteLine("Número de desplazamiento no válido. Por favor, ingrese un número entero.");
    return;
}
static string Cifrar(string texto, int desplazamiento)
{
    desplazamiento = desplazamiento % 26;
    string resultado = "";
    for (int i = 0; i < texto.Length; i++)
    {
        char letra = texto[i];
        if (letra >= 'A' && letra <= 'Z')
        {
            char baseLetra = 'A';
            char nueva = (char)(((letra - baseLetra + desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
        }
        else if (letra >= 'a' && letra <= 'z')
        {
            char baseLetra = 'a';
            char nueva = (char)(((letra - baseLetra + desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
        }
        else
        {
            resultado += letra;
        }
    }
    return resultado;

}

static string Descifrar(string texto, int desplazamiento)
{
    desplazamiento = desplazamiento % 26;
    string resultado = "";
    for (int i = 0; i < texto.Length; i++)
    {
        char letra = texto[i];
        if (letra >= 'A' && letra <= 'Z')
        {
            char baseLetra = 'A';
            char nueva = (char)(((letra - baseLetra - desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
        }
        else if (letra >= 'a' && letra <= 'z')
        {
            char baseLetra = 'a';
            char nueva = (char)(((letra - baseLetra - desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
        }
        else
        {
            resultado += letra;
        }
    }
    return resultado;
}
for (int i = 0; i < frase.Length; i++)
{
    char letra = frase[i];
    if ((letra < 'a' || letra > 'z') && letra != ' ')
    {
        Console.WriteLine("La frase contiene caracteres especiales. Por favor, ingrese una frase sin caracteres especiales.");
        return;
    }
}
Console.WriteLine("ingrese una frase para cifrarla o descifrarla: (c/d)");
string opcion = Console.ReadLine();
if (opcion.ToLower() == "c")
{
    string cifrado = Cifrar(frase, desplazamiento);
    Console.WriteLine("Frase cifrada: " + cifrado);
}
else if (opcion.ToLower() == "d")
{
    string descifrado = Descifrar(frase, desplazamiento);
    Console.WriteLine("Frase descifrada: " + descifrado);
}
