//ejercicio 1
Console.WriteLine("ingrese una frase para contar las vocales que hay en la frase con un for anidado");
string frase = Console.ReadLine();
int contadorVocales = 0;
string vocales = "aeiouAEIOU";
foreach (char letra in frase)
{
    for (int i = 0; i < vocales.Length; i++)
    {
        if (letra == vocales[i])
        {
            contadorVocales++;
            break;
        }
    }
}
Console.WriteLine("La cantidad de vocales en la frase es: " + contadorVocales);

//ejercicio 2
Console.WriteLine("ingrese una frase para invertirla");
string frase2 = Console.ReadLine();
string fraseInvertida = "";
for (int i = 0; i < frase2.Length; i++)
{
    fraseInvertida = frase2[i] + fraseInvertida;
}
Console.WriteLine("La frase invertida es: " + fraseInvertida);

//ejercicio 3
Console.WriteLine("ingrese un numero entero y calcula la suma de los digitos");
string numero = Console.ReadLine();
int suma = 0;
for (int i = 0; i < numero.Length; i++)
{
    suma += Convert.ToInt32(numero[i].ToString());
}
Console.WriteLine("La suma de los digitos es: " + suma);

//ejercicio 4
Console.WriteLine("ingrese un parrafo con una palabra prohibida para remplazarla");
string parrafo = Console.ReadLine();
Console.WriteLine("Ingrese la palabra prohibida:");
string palabraProhibida = Console.ReadLine();
Console.WriteLine("Ingrese la palabra de reemplazo:");
string palabraReemplazo = Console.ReadLine();
string parrafoModificado = parrafo.Replace(palabraProhibida, palabraReemplazo);
Console.WriteLine("El texto resultante es: " + parrafoModificado);

//ejercicio 5
Console.WriteLine("ingrese su nombre y apellido para abreviarlo y poner solo las iniciales");
string nombreCompleto = Console.ReadLine();
string[] partes = nombreCompleto.Split(' ');
string iniciales = "";
for (int i = 0; i < partes.Length; i++)
{
    iniciales += partes[i] + ".";
}
Console.WriteLine("Las iniciales son: " + iniciales);

//ejercicio 6
Console.WriteLine("ingrese una palabra para ver si se lee igual al derecho y al reves(palindromo)");
string palabra = Console.ReadLine();
string palabraInvertida2 = "";
for (int i = 0; i < palabra.Length; i++)
{
    palabraInvertida2 = palabra[i] + palabraInvertida2;
}
if (palabra == palabraInvertida2)
{
    Console.WriteLine("La palabra es un palindromo");
}
else
{
    Console.WriteLine("La palabra no es un palindromo");
}
