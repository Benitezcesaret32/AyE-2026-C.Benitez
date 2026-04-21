//Primer bloque - Ejercicios básicos.
//ejercicio A
Console.WriteLine("Múltiplos de 3 entre 1 e 100:");

for (int i = 1; i <= 100; i++)
{
    if (i % 3 == 0)
    {
        Console.WriteLine(i);
    }
}
// ejercicio B
Console.WriteLine("ingrese su edad:");
int edad = Convert.ToInt32(Console.ReadLine());
if (edad > 18)
{
    Console.WriteLine("Eres mayor de edad.");
}
else if (edad < 18)
{
    Console.WriteLine("Eres menor de edad.");
}
else //edad == 18
{
    Console.WriteLine("tienes justo 18");
}

//ejercicio C
Console.WriteLine("ingrese una palabra para saber cuantas letras tiene:");
Console.Write("palabra: ");
string palabra = Console.ReadLine() ?? "";
if (palabra == "")
{
    Console.WriteLine("no ingresaste nada");
}
else
{
    int cantidadLetras = Convert.ToInt32(palabra.Length);
    Console.WriteLine("La palabra tiene " + cantidadLetras + " letras.");
}
//ejercio D

// Juego: adivinar una   contraseña con 5 intentos
string contrasenaSecreta = "secreto123";
int intentosMaximos = 5;

Console.WriteLine("Adivina la contraseña. Tienes 5 intentos.");

for (int intento = 1; intento <= intentosMaximos; intento++)
{
    Console.Write("Intento " + intento + " - ingresa la contraseña: ");
    string intentoUsuario = Console.ReadLine() ?? "";

    if (intentoUsuario == contrasenaSecreta)
    {
        Console.WriteLine("¡Correcto! Has adivinado la contraseña.");
        break;
    }
    else
    {
        int intentosRestantes = intentosMaximos - intento;
        if (intentosRestantes > 0)
        {
            Console.WriteLine("Incorrecto. Te quedan " + intentosRestantes + " intento(s).");
        }
        else
        {
            Console.WriteLine("Has agotado los " + intentosMaximos + " intentos. La contraseña era: " + contrasenaSecreta);
        }
    }
}
//ejercicio F
Console.WriteLine();
Console.WriteLine("ingrese su nombre y sera saludado");
Console.Write("nombre: ");
string nombre = Console.ReadLine();
Console.WriteLine("Hola, " + nombre);
//ejercicio H
for (int i = 10; i >= 1; i--)
{
    Console.WriteLine(i);
}
Console.WriteLine("oa");

//ejercicio I
Console.WriteLine("ingrese un número para saber si es par o impar");
Console.Write("número: ");
int numero = Convert.ToInt32(Console.ReadLine());
if (numero % 2 == 0)
{
    Console.WriteLine("El número es par.");
}
else
{
    Console.WriteLine("El número es impar.");
}

//ejercicio J 
Console.WriteLine("ingrese una frase para saber cuantas vocales tiene");
Console.Write("frase: ");
string frase = Console.ReadLine() ?? "";
int contadorVocales = 0;
foreach (char letra in frase)
{
    if ("aeiouAEIOU".Contains(letra))
    {
        contadorVocales++;
    }
}
Console.WriteLine("La frase tiene " + contadorVocales + " vocales.");

//ejercicio K
Console.WriteLine("Ingrese un número para ver su tabla de multiplicar (1 a 12):");
Console.Write("número: ");
int numeroTabla = Convert.ToInt32(Console.ReadLine());
if (numeroTabla < 1 || numeroTabla > 12)
{
    Console.WriteLine("Número inválido. Por favor, ingrese un número entre 1 y 12.");
}
else
{
    Console.WriteLine("Tabla de multiplicar del " + numeroTabla + ":");
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine(numeroTabla + " x " + i + " = " + (numeroTabla * i));
    }
}
//ejercicio L
Console.WriteLine("ingresa números hasta que la suma supere 100.");
int suma = 0;
int contadorL = 0;
for (int i = 1; suma <= 100; i++)
{
    Console.Write("Número " + i + ": ");
    int numeroL = Convert.ToInt32(Console.ReadLine());
    suma += numeroL;
    contadorL++;
}

Console.WriteLine("Se superó 100 después de " + contadorL + " número(s). Suma final: " + suma);

//ejercicio M
Console.WriteLine("Ingrese una palabra:");
Console.Write("palabra: ");
string palabraM = Console.ReadLine() ?? "";
if (palabraM == "")
{
    Console.WriteLine("No ingresaste nada.");
}
else
{
    Console.WriteLine("Cada letra en una línea:");
    for (int i = 0; i < palabraM.Length; i++)
    {
        Console.WriteLine(palabraM[i]);
    }
}
//ejercicio N
Console.WriteLine("Ingrese su edad:");
Console.Write("edad: ");
string entradaN = Console.ReadLine() ?? "";
bool validoN = int.TryParse(entradaN, out _);
if (!validoN)
{
    Console.WriteLine("Entrada no válida.");
}
else
{
    int edadN = Convert.ToInt32(entradaN);
    if (edadN < 0)
    {
        Console.WriteLine("Edad no válida.");
    }
    else
    {
        bool puedeVotar = edadN >= 18;
        bool puedeManejar = edadN >= 16; // manejar desde 16, votar desde 18

        if (puedeVotar && puedeManejar)
        {
            Console.WriteLine("Puedes votar y manejar.");
        }
        else if (puedeVotar)
        {
            Console.WriteLine("Puedes votar, pero no puedes manejar.");
        }
        else if (puedeManejar)
        {
            Console.WriteLine("Puedes manejar, pero no puedes votar.");
        }
        else
        {
            Console.WriteLine("No puedes votar ni manejar.");
        }
    }
}
// ejercicio o
Console.WriteLine("Ejercicio O: Números del 50 al 0 de cinco en cinco (descendente):");
for (int i = 50; i >= 0; i -= 5)
{
    Console.WriteLine(i);
}
// ejercicio P
Console.WriteLine();
Console.WriteLine("Ejercicio P: Verificación de contraseña (escribí la misma contraseña dos veces):");
while (true)
{
    Console.Write("Contraseña: ");
    string contraseña1 = Console.ReadLine();
    Console.Write("Reingrese la contraseña: ");
    string contraseña2 = Console.ReadLine();

    if (contraseña1 == contraseña2)
    {
        Console.WriteLine("Acceso permitido");
        break;
    }
    else
    {
        Console.WriteLine("Las contraseñas no coinciden. Intentá de nuevo.");
    }
}
//ejercicio Q
Console.WriteLine();
Console.WriteLine("Ejercicio Q: Ingresá nombres uno por uno. El programa termina cuando un nombre tenga más de 10 caracteres.");
while (true)
{
    Console.Write("Nombre: ");
    string nombreQ = Console.ReadLine() ?? "";

    if (nombreQ.Length > 10)
    {
        Console.WriteLine("Nombre con más de 10 caracteres recibido. Fin del programa.");
        break;
    }

    Console.WriteLine("Nombre aceptado: " + nombreQ);
}
