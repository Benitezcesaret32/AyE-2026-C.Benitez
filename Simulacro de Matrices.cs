//ejercicio 1
Console.WriteLine(" El objetivo es generar una plantilla de 23 jugadores con sus posiciones y calcular el valor total de puntos de rendimiento del equipo");
string[] nombres = new string[] {
    "Alejandro","Bruno","Carmen","Diego","Elena","Federico","Gabriela","Hugo","Irene","Joaquín",
    "Karla","Leo","Marta","Noel","Olivia","Pablo","Rocío","Santiago","Tomás","Úrsula",
    "Víctor","Yago","Zoe"
};

string[] posiciones = new string[] { "Delantero", "Mediocampista", "Defensor", "Arquero" };

Random rnd = new Random();

string[,] equipoA = new string[23, 3];
string[,] equipoB = new string[23, 3];

for (int i = 0; i < 23; i++)
{
    string nombre = nombres[rnd.Next(nombres.Length)];
    int valor = rnd.Next(50, 101);
    string posicion = posiciones[rnd.Next(posiciones.Length)];

    equipoA[i, 0] = nombre;
    equipoA[i, 1] = valor.ToString();
    equipoA[i, 2] = posicion;
}

for (int i = 0; i < 23; i++)
{
    string nombre = nombres[rnd.Next(nombres.Length)];
    int valor = rnd.Next(50, 101);
    string posicion = posiciones[rnd.Next(posiciones.Length)];

    equipoB[i, 0] = nombre;
    equipoB[i, 1] = valor.ToString();
    equipoB[i, 2] = posicion;
}

int totalA = 0;
for (int i = 0; i < 23; i++) totalA += int.Parse(equipoA[i, 1]);

int totalB = 0;
for (int i = 0; i < 23; i++) totalB += int.Parse(equipoB[i, 1]);

Console.WriteLine("Valoración total Equipo A: " + totalA);
Console.WriteLine("Valoración total Equipo B: " + totalB);

if (totalA > totalB)
    Console.WriteLine("Resultado: Equipo A tiene más chances de ganar.");
else if (totalB > totalA)
    Console.WriteLine("Resultado: Equipo B tiene más chances de ganar.");
else
    Console.WriteLine("Resultado: Empate en valoración.");

//ejercicio 2
Console.WriteLine("Hacer un programa que usando una función recursiva muestre la potencia por un numero elegido de un numero elegido por el usuario");
Console.WriteLine("");
Console.Write("Ingrese la base (entero): ");
string entradaBase = Console.ReadLine();
int baseNum = int.Parse(entradaBase);

Console.Write("Ingrese el exponente (entero no negativo): ");
string entradaExp = Console.ReadLine();
int exponente = int.Parse(entradaExp);

if (exponente < 0)
{
    Console.WriteLine("No se pueden calcular exponentes negativos con enteros. Introduce exponente >= 0.");
}
else
{
    int resultado = Potencia(baseNum, exponente);
    Console.WriteLine("Resultado: " + baseNum + "^" + exponente + " = " + resultado);
}

static int Potencia(int b, int e)
{
    if (e == 0) return 1;
    if (e % 2 == 0)
    {
        int mitad = Potencia(b, e / 2);
        return mitad * mitad;
    }
    return b * Potencia(b, e - 1);
}