namespace Pokedex
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Tipo1 { get; set; } = "";
        public string? Tipo2 { get; set; }
        public int Hp { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int AtaqueEspecial { get; set; }
        public int DefensaEspecial { get; set; }
        public int Velocidad { get; set; }
        public int Nivel { get; set; }

        public override string ToString()
        {
            return "ID: " + Id +
                   " | Nombre: " + Nombre +
                   " | Tipo 1: " + Tipo1 +
                   " | Tipo 2: " + (Tipo2 ?? "-") +
                   " | HP: " + Hp +
                   " | Ataque: " + Ataque +
                   " | Defensa: " + Defensa +
                   " | Ataque Especial: " + AtaqueEspecial +
                   " | Defensa Especial: " + DefensaEspecial +
                   " | Velocidad: " + Velocidad +
                   " | Nivel: " + Nivel;
        }
    }
}
