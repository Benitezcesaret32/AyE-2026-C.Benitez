namespace ConsoleApp44;
public struct Jugador
{
    public string Nombre { get; }
    public string Apellido { get; }
    public int CantGoles { get; }
    public int CantDisparosAlArco { get; }
    public int NumeroCamiseta { get; }
    public string Posicion { get; }
    public string Equipo { get; }

    public Jugador(string nombre, string apellido, int cantGoles, int cantDisparosAlArco, int numeroCamiseta, string posicion, string equipo)
    {
        Nombre = nombre;
        Apellido = apellido;
        CantGoles = cantGoles;
        CantDisparosAlArco = cantDisparosAlArco;
        NumeroCamiseta = numeroCamiseta;
        Posicion = posicion;
        Equipo = equipo;
    }

    public double IndiceAtaque()
    {
        if (CantDisparosAlArco <= 0) return 0.0;
        return (double)CantGoles / CantDisparosAlArco * 100.0;
    }

    public new string ToString()
    {
        return Nombre + " " + Apellido + " | Equipo: " + Equipo + " | Nº " + NumeroCamiseta + " | Pos: " + Posicion + " | Goles: " + CantGoles + " | Disparos: " + CantDisparosAlArco + " | Índice: " + IndiceAtaque().ToString("F2") + "%";
    }
}

class Program
{
    static void Main()
    {
        List<Jugador> jugadores = new List<Jugador>
        {
            new Jugador("Lionel", "Messi", 30, 80, 10, "Delantero", "PSG"),
            new Jugador("Cristiano", "Ronaldo", 25, 90, 7, "Delantero", "Al-Nassr"),
            new Jugador("Kylian", "Mbappé", 28, 70, 7, "Delantero", "PSG"),
            new Jugador("Jugador", "SinDisparos", 0, 0, 99, "Mediocampista", "EquipoX")
        };

        Console.WriteLine("Listado de jugadores y sus índices de ataque:");
        foreach (Jugador j in jugadores)
        {
            Console.WriteLine(j.ToString());
        }

        bool hay = false;
        Jugador mejor = new Jugador("", "", 0, 0, 0, "", "");
        double mejorIndice = 0.0;

        foreach (Jugador actual in jugadores)
        {
            double indice = actual.IndiceAtaque();
            if (hay || indice > mejorIndice)
            {
                mejor = actual;
                mejorIndice = indice;
                hay = true;
            }
        }

        if (hay)
        {
            Console.WriteLine("No hay jugadores en la lista.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mejor índice de ataque:");
            Console.WriteLine(mejor.ToString());
        }
    }
}
