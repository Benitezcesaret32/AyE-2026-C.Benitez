//ejercicio 1
Console.WriteLine(" Mini Torneo Pokémon");
string[] pokemones = { "Mewtwo", "Lugia", "Ho-Oh", "Rayquaza", "Groudon", "Kyogre" };
string[] tipos = { "Psychic", "Psychic", "Fire", "Dragon", "Ground", "Water" };
Random random = new Random();
string[] teamNames = { "Red", "Green", "Blue", "Yellow" };
string[,] teamPokemons = new string[4, 6];
string[,] teamTipos = new string[4, 6];
int[,] teamNiveles = new int[4, 6];
for (int t = 0; t < 4; t++)
{
    bool[] usado = new bool[pokemones.Length];
    for (int i = 0; i < 6; i++)
    {
        int index = random.Next(pokemones.Length);
        while (usado[index])
        {
            index = random.Next(pokemones.Length);
        }

        usado[index] = true;
        teamPokemons[t, i] = pokemones[index];
        teamTipos[t, i] = tipos[index];
        teamNiveles[t, i] = random.Next(50, 81);
    }
}
int[] teamScores = new int[4];
for (int t = 0; t < 4; t++)
{
    int sum = 0;
    for (int i = 0; i < 6; i++)
    {
        sum += teamNiveles[t, i];
    }
    teamScores[t] = sum;
}
string winnerRGName = teamScores[0] > teamScores[1] ? "Red" : "Green";
int winnerRGScore = teamScores[0] > teamScores[1] ? teamScores[0] : teamScores[1];
string winnerBYName = teamScores[2] > teamScores[3] ? "Blue" : "Yellow";
int winnerBYScore = teamScores[2] > teamScores[3] ? teamScores[2] : teamScores[3];
string champion = winnerRGScore > winnerBYScore ? winnerRGName : winnerBYName;
for (int t = 0; t < 4; t++)
{
    Console.WriteLine($"{teamNames[t]} Team:");
    for (int i = 0; i < 6; i++)
    {
        Console.WriteLine($"  {i + 1}. {teamPokemons[t, i]} ({teamTipos[t, i]}, Nivel {teamNiveles[t, i]})");
    }
    Console.WriteLine($"  Total Nivel: {teamScores[t]}");
    Console.WriteLine();
}
Console.WriteLine($"Ganador de Red vs Green: {winnerRGName} (Nivel: {winnerRGScore})");
Console.WriteLine($"Ganador de Blue vs Yellow: {winnerBYName} (Nivel: {winnerBYScore})");
Console.WriteLine($"Campeón del torneo: {champion}");

//ejercicio 2
Console.WriteLine("Hacer una función recursiva que muestre los números del 50 al 0 de forma descendente, de cinco en cinco");
int MostrarNumeros(int numero)
{
    Console.WriteLine(numero);
    if (numero <= 0) return numero;
    return MostrarNumeros(numero - 5);
}
MostrarNumeros(50);


