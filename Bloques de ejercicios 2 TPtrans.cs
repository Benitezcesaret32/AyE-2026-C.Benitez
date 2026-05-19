//segundo bloque
//ejercicio a
Console.WriteLine("Juego adivinar numero");
Random r = new Random();
int secreto =r.Next(1, 11);
int num = 0;

while (num != secreto)
{
    Console.Write("Adivina (1-10): ");
    num = int.Parse(Console.ReadLine());

    if (num > secreto)
        Console.WriteLine("Es menor");
    else if (num < secreto)
        Console.WriteLine("Es mayor");
}

Console.WriteLine("Correcto");

//ejercicio b
Console.WriteLine("Divisores");
Console.Write("Numero: ");
int n = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= n; i++)
{
    if (n % i == 0)
        Console.WriteLine(i);
}

//ejercicio c
Console.WriteLine("Primos hasta 20");
for (int i = 2; i <= 20; i++)
{
    bool primo = true;

    for (int j = 2; j < i; j++)
    {
        if (i % j == 0)
            primo = false;
    }

    if (primo)
        Console.WriteLine(i);
}

//ejercicio d
Console.WriteLine("Calculadora");
Console.Write("Numero 1: ");
int a = Convert.ToInt32(Console.ReadLine());

Console.Write("Numero 2: ");
int b = Convert.ToInt32(Console.ReadLine());

Console.Write("Operacion (+ - * /): ");
string op = Console.ReadLine();

if (op == "+")
    Console.WriteLine(a + b);
if (op == "-")
    Console.WriteLine(a - b);
if (op == "*")
    Console.WriteLine(a * b);
if (op == "/")
    Console.WriteLine(a / b);

//ejercicio e
Console.WriteLine("Factorial");
Console.Write("Numero: ");
int e = Convert.ToInt32(Console.ReadLine());

int fact = 1;

for (int i = 1; i <= e; i++)
{
    fact = fact * i;
}

Console.WriteLine(fact);

//ejercicio f
Console.WriteLine("Sistema de turnos");
string nombre;
int turno = 1;

Console.Write("Nombre (fin para salir): ");
nombre = Console.ReadLine();

while (nombre != "fin")
{
    Console.WriteLine("Turno: " + turno);
    turno++;

    Console.Write("Nombre: ");
    nombre = Console.ReadLine();
}
//ejercicio g
Console.WriteLine("Mostrá cuántos números pares hay entre 1 y 100.");
int count = 0;
for (int i = 1; i <= 100; i++)
{
    if (i % 2 == 0)
    {
        count++;
    }
}
Console.WriteLine($"Hay {count} números pares entre 1 y 100.");
//ejercicio h
Console.WriteLine("ingrese un programa que convierta temperaturas de Celsius a Fahrenheit, y que permita convertir varias veces hasta que el usuario decida salir.");
while (true)
{
    Console.Write("Ingrese la temperatura en Celsius (o 'salir' para terminar): ");
    string input = Console.ReadLine();
    if (input.ToLower() == "salir")
    {
        break;
    }
    
    try
    {
        double celsius = Convert.ToInt32(input);
        double fahrenheit = (celsius * 9 / 5) + 32;
        Console.WriteLine($"{celsius} grados Celsius son {fahrenheit} grados Fahrenheit.");
    }
    catch (FormatException)
    {
        Console.WriteLine("Entrada no válida. Por favor, ingrese un número o 'salir'.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Número fuera de rango. Por favor, ingrese un valor más pequeño o más grande según corresponda.");
    }
}
//ejercio i
Console.WriteLine("ingrese un número y decile si es primo o no.");
Console.Write("Ingrese un número: ");
int number = Convert.ToInt32(Console.ReadLine());
bool esPrimo = true;
if (number <= 1)
{
    esPrimo = false;
}
else
{
    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0)
        {
            esPrimo = false;
            break;
        }
    }
}
if (esPrimo)
{
    Console.WriteLine($"{number} es un número primo.");
}
else
{
    Console.WriteLine($"{number} no es un número primo.");
}
//ejercicio j
Console.WriteLine("ingrese una lista de nombres (hasta que el usuario escriba “fin”) y después saludá a cada uno.");
while (true)
{
    Console.Write("Ingrese un nombre (o 'fin' para terminar): ");
    string nombreLista = Console.ReadLine();
    if (nombreLista.ToLower() == "fin")
    {
        break;
    }
    Console.WriteLine($"Hola, {nombreLista}!");

}
//ejercicio k
Console.WriteLine("Armá un sistema que reciba nombres hasta que se repita uno. Cuando eso pase, mostrale al usuario cuántos ingresó antes del duplicado.");
string[] nombresUnicos = new string[100];
int countNombres = 0;
while (true)
{
    Console.Write("Ingrese un nombre: ");
    string nombreUnico = Console.ReadLine();
    if (Array.Exists(nombresUnicos, n => n == nombreUnico))
    {
        Console.WriteLine($"El nombre '{nombreUnico}' ya fue ingresado. Total de nombres únicos ingresados: {countNombres}");
        break;
    }
    else
    {
        nombresUnicos[countNombres] = nombreUnico;
        countNombres++;
    }
}
Console.WriteLine($"Total de nombres únicos ingresados antes del duplicado: {countNombres}");
//ejercicio L
Console.WriteLine("Mostrá todos los números entre 100 y 200 que sean múltiplos de 7 y terminan en 3 por separado");
for (int i = 100; i <= 200; i++)
{
    if (i % 7 == 0 && i % 10 == 3)
    {
        Console.WriteLine(i);
    }
}
//ejercicio m
Console.WriteLine("ingrese precios hasta que el total supere $1000. Al final, mostrá cuántos productos se cargaron.");
double total = 0;
int countProductos = 0; countProductos = 0;
for  (int i = 0;  i <= (number); i++) { countProductos++; }
while (total <= 1000)
{
    Console.Write("Ingrese el precio del producto: ");
    string input = Console.ReadLine();
    if (input.ToLower() == "fin")
    {
        break;
    }
    
    try
    {
        int precio = Convert.ToInt32(input);
        total += precio;
        countProductos++;
    }
    catch (FormatException)
    {
        Console.WriteLine("Entrada no válida. Por favor, ingrese un número o 'fin'.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Número fuera de rango. Por favor, ingrese un valor más pequeño o más grande según corresponda.");
    }
}
Console.WriteLine($"Total de productos cargados: {countProductos}");
//ejercicio n
Console.WriteLine("Hacé un programa que simule un formulario: pedí nombre, edad y mail. Validá que la edad sea un número y que el mail tenga “@”.");
Console.Write("Ingrese su nombre: ");
string nombre = Console.ReadLine();
int edad;
while (true)
{
    Console.Write("Ingrese su edad: ");
    string inputEdad = Console.ReadLine();
    try
    {
        edad = Convert.ToInt32(inputEdad);
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Entrada no válida. Por favor, ingrese un número para la edad.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Número fuera de rango. Por favor, ingrese un valor más pequeño o más grande según corresponda.");
    }
}
Console.Write("Ingrese su mail: ");
string mail = Console.ReadLine();
while (!mail.Contains("@"))
{
    Console.WriteLine("Mail no válido. Por favor, ingrese un mail que contenga '@'.");
    Console.Write("Ingrese su mail: ");
    mail = Console.ReadLine();
}
Console.WriteLine($"Nombre: {nombre}, Edad: {edad}, Mail: {mail}");
//ejericio o
Console.WriteLine("ingrese un numero y mostra cada digito por separado");
Console.Write("Ingrese un número: ");
string numero = Console.ReadLine();
foreach (char digito in numero)
{
    Console.WriteLine(digito);
}
//ejercicio p
Console.WriteLine("Simulá una “piedra, papel o tijera” de 3 rondas. Al final, mostrale al usuario quién ganó más veces.");
string[] opciones = { "piedra", "papel", "tijera" };
int puntajeUsuario = 0;
int puntajeComputadora = 0;
Random random = new Random();
for (int ronda = 1; ronda <= 3; ronda++)
{
    Console.Write($"Ronda {ronda} - Elige piedra, papel o tijera: ");
    string eleccionUsuario = Console.ReadLine().ToLower();
    while (!opciones.Contains(eleccionUsuario))
    {
        Console.Write("Elección no válida. Por favor, elige piedra, papel o tijera: ");
        eleccionUsuario = Console.ReadLine().ToLower();
    }
    
    int indiceComputadora = random.Next(opciones.Length);
    string eleccionComputadora = opciones[indiceComputadora];
    Console.WriteLine($"La computadora eligió: {eleccionComputadora}");
    
    if (eleccionUsuario == eleccionComputadora)
    {
        Console.WriteLine("Empate!");
    }
    else if ((eleccionUsuario == "piedra" && eleccionComputadora == "tijera") ||
             (eleccionUsuario == "papel" && eleccionComputadora == "piedra") ||
             (eleccionUsuario == "tijera" && eleccionComputadora == "papel"))
    {
        Console.WriteLine("¡Ganaste esta ronda!");
        puntajeUsuario++;
    }
    else
    {
        Console.WriteLine("La computadora gana esta ronda.");
        puntajeComputadora++;
    }
}
if (puntajeUsuario > puntajeComputadora)
{
    Console.WriteLine("¡Ganaste más rondas que la computadora!");
}
else if (puntajeUsuario < puntajeComputadora)
{
    Console.WriteLine("La computadora ganó más rondas.");
}
else
{
    Console.WriteLine("¡Empate en rondas!");
}
//ejercicio q
Console.WriteLine("Generá 10 números aleatorios entre 1 y 100. Mostrá cuántos son mayores a 50.");
Random rand = new Random();
int countMayores50 = 0;
for (int i = 0; i < 10; i++)
{
    int numeroAleatorio = rand.Next(1, 101);
    Console.WriteLine($"Número generado: {numeroAleatorio}");
    if (numeroAleatorio > 50)
    {
        countMayores50++;
    }
}
Console.WriteLine($"Cantidad de números mayores a 50: {countMayores50}");
//ejercicio r
Console.WriteLine("ingresar 5 nombres y su nota y Mostrá el promedio general, y quién obtuvo la mejor nota.");
string[] nombres = new string[5];
for (int i = 0; i < 5; i++)
{
    Console.Write($"Ingrese el nombre del estudiante {i + 1}: ");
    nombres[i] = Console.ReadLine();
}
int[] notas = new int[5];
for (int i = 0; i < 5; i++)
{
    while (true)
    {
        Console.Write($"Ingrese la nota de {nombres[i]}: ");
        string inputNota = Console.ReadLine();
        try
        {
            notas[i] = Convert.ToInt32(inputNota);
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Entrada no válida. Por favor, ingrese un número para la nota.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Número fuera de rango. Por favor, ingrese un valor más pequeño o más grande según corresponda.");
        }
    }
}
int sumaNotas = notas.Sum();
double promedio = (double)sumaNotas / notas.Length;
int mejorNota = notas.Max();
int indiceMejorNota = -1;
for (int i = 0; i < notas.Length; i++)
{
    if (notas[i] == mejorNota)
    {
        indiceMejorNota = i;
        break;
    }
}
Console.WriteLine($"El promedio general es: {promedio}");
Console.WriteLine($"La mejor nota es: {mejorNota}, obtenida por: {nombres[indiceMejorNota]}");
//ejercicio s
Console.WriteLine("Mostrá las letras del abecedario, pero en orden inverso (de la Z a la A)");
for (char letra = 'Z'; letra >= 'A'; letra--)
{
    Console.Write(letra + " ");
}
//FIN

