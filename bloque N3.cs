// Ejercicio A
Console.WriteLine("Ejercicio A: Simulación de votación y que termine cuando coloque terminar");
Console.WriteLine("1. Nicolas Maduro");
Console.WriteLine("2. Adolf Hitler");
Console.WriteLine("3. Claudia Sheinbaum");
int votoNicolas = 0;
int votoAdolf = 0;
int votoClaudia = 0;
bool continuarVotando = true;
while (continuarVotando)
{
    Console.WriteLine("Ingrese su voto (1, 2, 3) o 'terminar' para finalizar:");
    string input = Console.ReadLine();
    if (input.ToLower() == "terminar")
    {
        continuarVotando = false;
    }
    else
    {
        switch (input)
        {
            case "1":
                votoNicolas++;
                break;
            case "2":
                votoAdolf++;
                break;
            case "3":
                votoClaudia++;
                break;
            default:
                Console.WriteLine("Voto no válido. Por favor, ingrese 1, 2, 3 o 'terminar'.");
                break;
        }
    }
}
Console.WriteLine("Resultados de la votación:");
Console.WriteLine($"Nicolas Maduro: {votoNicolas} votos");
Console.WriteLine($"Adolf Hitler: {votoAdolf} votos");
Console.WriteLine($"Claudia Sheinbaum: {votoClaudia} votos");

// Ejercicio B
Console.WriteLine("Ejercicio B: Mostrar números impares desde 1 hasta el número ingresado");
Console.WriteLine("Ingrese un número:");
int numero = Convert.ToInt32(Console.ReadLine());
if (numero < 1)
{
    Console.WriteLine("No se puede ingresar un número menor que 1.");
}
else
{
    Console.WriteLine($"Números impares desde 1 hasta {numero}:");
    for (int i = 1; i <= numero; i++)
    {
        if (i % 2 != 0)
        {
            Console.WriteLine(i);
        }
    }
}

// Ejercicio C
Console.WriteLine("Ejercicio C: Contar la cantidad de veces que aparece una letra en una palabra");
Console.WriteLine("Ingrese una palabra:");
string palabra = Console.ReadLine();
Console.WriteLine("Ingrese una letra:");
char letra = Console.ReadLine()[0];
int contador = 0;
foreach (char c in palabra)
{
    if (c == letra)
    {
        contador++;
    }
}
Console.WriteLine($"La letra '{letra}' aparece {contador} veces en la palabra '{palabra}'.");

// Ejercicio D
Console.WriteLine("Ejercicio D: Simulación de una cuenta bancaria");
int saldo = 1000;
bool continuar = true;
while (continuar)
{
    Console.WriteLine($"Saldo actual: ${saldo}");
    Console.WriteLine("Seleccione una opción: 1. Depositar dinero 2. Retirar dinero 3. Salir");
    string opcion = Console.ReadLine();
    switch (opcion)
    {
        case "1":
            Console.WriteLine("Ingrese la cantidad a depositar:");
            int deposito = Convert.ToInt32(Console.ReadLine());
            saldo += deposito;
            break;
        case "2":
            Console.WriteLine("Ingrese la cantidad a retirar:");
            int retiro = Convert.ToInt32(Console.ReadLine());
            if (retiro > saldo)
            {
                Console.WriteLine("No se puede retirar más de lo que tiene en el saldo.");
            }
            else
            {
                saldo -= retiro;
            }
            break;
        case "3":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opción no válida. Por favor, seleccione 1, 2 o 3.");
            break;
    }
}

// Ejercicio E

Console.WriteLine("Ejercicio E: ");
int contador2 = 0;
int numeroE = 1;
while (contador < 50)
{
    if ((numeroE % 2 == 0 || numeroE % 5 == 0) && !(numeroE % 2 == 0 && numeroE % 5 == 0))
    {
        Console.WriteLine(numeroE);
        contador++;
    }
    numeroE++;
}

// ejecico F
Console.WriteLine("ejercio F:");
Console.Write("Ingrese una palabra:");
string palabra;
try{
    palabra = Console.ReadLine().ToLower();
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó ninguna palabra:");
    return;
}
for (int i = 0; i < palabra.Length; i++)
{
    char letra = palabra[i];
    if (char.IsLetter(letra))
    {
        int posicion = letra - 'a' + 1;
        Console.Write(posicion + " ");
    }
    else
    {
        Console.WriteLine("No es una letra ");
    }
}

// ejercicio G
Console.WriteLine("");
Console.WriteLine("ejercio g:");
Random random = new Random();
int numeroSecreto = random.Next(1, 100);
int intentos = 0;
int adivinanza = 0;
Console.WriteLine("Adivina el número secreto entre 1 y 100:");
while (adivinanza != numeroSecreto)
{
    try
    {
        adivinanza = Convert.ToInt16(Console.ReadLine());
        intentos++;
        if (adivinanza < numeroSecreto)
        {
            Console.WriteLine("Más grande");
        }
        else if (adivinanza > numeroSecreto)
        {
            Console.WriteLine("Más chico");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Por favor, ingresa un número válido.");
        return;
    }
}
Console.WriteLine($"¡Felicidades! Adivinaste el número secreto en {intentos} intentos.");

// ejercicio H
Console.WriteLine("ejercio H:Pedile al usuario una frase y devolvé la misma frase, pero con cada palabra invertida");
Console.Write("Ingrese una frase:");
string frase;
try
{
    frase = Console.ReadLine();
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó ninguna frase:");
    return;
}
foreach (string palabra2 in frase.Split(' '))
{
    char[] caracteres = palabra2.ToCharArray();
    Array.Reverse(caracteres);
    Console.Write(new string(caracteres) + " ");
}

// ejercicio I
Console.WriteLine("ejercio I: Mostrá cuántas veces aparece cada letra en una palabra.");
Console.Write("Ingrese una palabra:");
string palabra3;
try
{
    palabra3 = Console.ReadLine().ToLower();
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó ninguna palabra:");
    return;
}
for (char letra = 'a'; letra <= 'z'; letra++)
{
    int contador = 0;
    foreach (char c in palabra3)
    {
        if (c == letra)
        {
            contador++;
        }
    }
    if (contador > 0)
    {
        Console.WriteLine($"La letra '{letra}' aparece {contador} veces.");
    }
}

// ejercicio J
Console.WriteLine("ejercio J:");
string usuarioCorrecto = "admin";
string contraseñaCorrecta = "password";
int intentos2 = 0;
while (intentos2 < 3)
{
    Console.Write("Ingrese su nombre de usuario:");
    string usuario;
    try
    {
        usuario = Console.ReadLine().ToLower();
    }
    catch (FormatException)
    {
        Console.WriteLine("No se ingresó ningún nombre de usuario:");
        return;
    }
    Console.Write("Ingrese su contraseña:");
    string contraseña;
    try
    {
        contraseña = Console.ReadLine().ToLower();
    }
    catch (FormatException)
    {
        Console.WriteLine("No se ingresó ninguna contraseña:");
        return;
    }
    if (usuario == usuarioCorrecto && contraseña == contraseñaCorrecta)
    {
        Console.WriteLine($"¡Bienvenido, {usuario}!");
        break;
    }
    else
    {
        intentos2++;
        Console.WriteLine("Nombre de usuario o contraseña incorrectos. Intente nuevamente.");
        if (intentos2 == 3)
        {
            Console.WriteLine("Has alcanzado el número máximo de intentos. Acceso bloqueado.");
        }
    }
}

// ejercicio K
Console.WriteLine("ejercio K:");
int numero;
Console.Write("Ingrese un número entre 0 y 100:");
try {
    numero = Convert.ToInt16(Console.ReadLine());
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó un número válido:");
    return;
}
if (numero >= 0 && numero <= 25)
{
    Console.WriteLine("El número está en el rango de 0 a 25.");
}
else if (numero > 25 && numero <= 50)
{
    Console.WriteLine("El número está en el rango de 26 a 50.");
}
else if (numero > 50 && numero <= 75)
{
    Console.WriteLine("El número está en el rango de 51 a 75.");
}
else if (numero > 75 && numero <= 100)
{
    Console.WriteLine("El número está en el rango de 76 a 100.");
}
else
{
    Console.WriteLine("El número ingresado está fuera del rango permitido.");
}

// ejercicio L
Console.WriteLine("ejercio L:");
int peso;
Console.Write("Ingrese su peso en kg:");
try
{
    peso = Convert.ToInt16(Console.ReadLine());
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó un peso válido:");
    return;
}
Console.Write("Ingrese su altura en metros:");
int altura;
try
{
    altura = Convert.ToInt16(Console.ReadLine());
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó una altura válida:");
    return;
}
int imc = peso / (altura * altura);
Console.WriteLine($"Su IMC es: {imc:F2}");
if (imc < 18.5)
{
    Console.WriteLine("Categoría: Bajo peso");
}
else if (imc >= 18.5 && imc < 24.9)
{
    Console.WriteLine("Categoría: Normal");
}
else if (imc >= 25 && imc < 29.9)
{
    Console.WriteLine("Categoría: Sobrepeso");
}
else if (imc >= 30 && imc < 34.9)
{
    Console.WriteLine("Categoría: Obesidad");
}
else
{
    Console.WriteLine("Categoría: Obesidad mórbida");
}

// ejercicio M:
Console.WriteLine("ejercio M:");
Console.Write("Ingrese una palabra:");
string palabra4;
try
{
    palabra4 = Console.ReadLine();
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó ninguna palabra:");
    return;
}
for (int i = 1; i <= palabra4.Length; i++)
{
    Console.WriteLine(palabra4.Substring(0, i));
}

// ejercicio N:
Console.WriteLine("ejercio N:");
string frase2;
try
{
    frase2 = Console.ReadLine().ToLower();
}
catch (FormatException)
{
    Console.WriteLine("No se ingresó ninguna frase:");
    return;
}
string[] vocales = { "a", "e", "i", "o", "u" };
int indiceVocal = 0;
while (frase2.ToLower() != "agusfortnite2008")
{
    string fraseModificada = frase2;
    foreach (string vocal in vocales)
    {
        fraseModificada = fraseModificada.Replace(vocal, vocales[indiceVocal]);
        fraseModificada = fraseModificada.Replace(vocal.ToLower(), vocales[indiceVocal].ToLower());
    }
    Console.WriteLine(fraseModificada);
    indiceVocal = (indiceVocal + 1) % vocales.Length;
    try
    {
        frase2 = Console.ReadLine().ToLower();
    }
    catch (FormatException)
    {
        Console.WriteLine("No se ingresó ninguna frase:");
        return;
    }
}
