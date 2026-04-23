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

