﻿Console.WriteLine("ingrese una frase para cifrarla o descifrarla sin caracteres especiales");
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
int desplazamiento = Convert.ToInt32(Console.ReadLine());

static string Cifrar(string texto, int desplazamiento)
{
    desplazamiento = desplazamiento % 26;
    string resultado = "";
    for (int i = 0; i < texto.Length; i++)
    {
        char letra = texto[i];
        if (letra >= 'a' && letra <= 'z')
        {
            char baseLetra = 'a';
            char nueva = (char)(((letra - baseLetra + desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
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
        if (letra >= 'a' && letra <= 'z')
        {
            char baseLetra = 'a';
            char nueva = (char)(((letra - baseLetra - desplazamiento + 26) % 26) + baseLetra);
            resultado += nueva;
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
string opcion = Console.ReadLine().ToLower();
if (opcion == "c")
{
    string cifrado = Cifrar(frase, desplazamiento);
    Console.WriteLine("Frase cifrada: " + cifrado);
}
else if (opcion == "d")
{
    string descifrado = Descifrar(frase, desplazamiento);
    Console.WriteLine("Frase descifrada: " + descifrado);
}
