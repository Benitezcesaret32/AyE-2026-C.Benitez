using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_FINAL
{
    // ═══════════════════════════════════════════════
    //  ENUMS Y CONSTANTES GLOBALES
    // ═══════════════════════════════════════════════

    enum StatusEffect { None, Poisoned, Burned, Stunned, Bleeding, Frozen, Cursed, Blessed, Enraged }
    enum ElementType { Physical, Fire, Ice, Lightning, Dark, Holy, Poison, Wind }
    enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
    enum ItemType { Potion, Weapon, Armor, Accessory, Scroll }

    // ═══════════════════════════════════════════════
    //  PROGRAMA PRINCIPAL
    // ═══════════════════════════════════════════════

    class Program
    {
        static readonly Random Rand = new Random();
        internal static List<Player> Players = new List<Player>();
        static int round = 1;
        static int totalKills = 0;
        static int highestRound = 1;

        static void Main()
        {
            Console.Title = "⚔  RPG FINAL EDITION  ⚔";
            Console.OutputEncoding = Encoding.UTF8;

            ShowIntro();
            SetupPlayers();
            GameLoop();
        }

        // ───────────────── INTRO ─────────────────
        static void ShowIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
  ██████╗ ██████╗  ██████╗     ███████╗██╗███╗   ██╗ █████╗ ██╗
  ██╔══██╗██╔══██╗██╔════╝     ██╔════╝██║████╗  ██║██╔══██╗██║
  ██████╔╝██████╔╝██║  ███╗    █████╗  ██║██╔██╗ ██║███████║██║
  ██╔══██╗██╔═══╝ ██║   ██║    ██╔══╝  ██║██║╚██╗██║██╔══██║██║
  ██║  ██║██║     ╚██████╔╝    ██║     ██║██║ ╚████║██║  ██║███████╗
  ╚═╝  ╚═╝╚═╝      ╚═════╝     ╚═╝     ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝
            ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                  ━━━  EDICIÓN DEFINITIVA  ━━━\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Pulsa cualquier tecla para comenzar tu aventura...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── SETUP ─────────────────
        static void SetupPlayers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("  Cantidad de jugadores (1-4): ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 4)
                n = 1;

            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ── Jugador {i + 1} ──");
                Console.ResetColor();
                Players.Add(CreatePlayer());
            }
        }

        static Player CreatePlayer()
        {
            while (true)
            {
                Console.Clear();
                PrintBox("ELIGE TU CLASE", ConsoleColor.Cyan);
                Console.WriteLine();

                var classes = new (string num, string icon, string name, string desc)[]
                {
                    ("1",  "⚔", "Guerrero",   "Alta defensa y ataque. Infaltable en la vanguardia."),
                    ("2",  "🏹","Arquero",    "Gran velocidad y críticos letales desde la distancia."),
                    ("3",  "🔥","Mago",       "Magia devastadora. Frágil pero imparable."),
                    ("4",  "✨","Curandero",  "Sostiene al equipo con curas y auras de apoyo."),
                    ("5",  "🗡","Ladrón",     "Golpes rápidos, robo de oro y evasión superior."),
                    ("6",  "👊","Monje",      "Kombos de golpes y resistencia a efectos negativos."),
                    ("7",  "🎵","Bardo",      "Buffs y debuffs grupales. El corazón del equipo."),
                    ("8",  "🛡","Paladín",    "Mezcla de fuerza y magia sagrada. Tankeo divino."),
                    ("9",  "🌑","Nigromante","Invoca muertos y drena vida enemiga."),
                    ("10", "⚡","Alquimista", "Lanza pociones explosivas y debilita rivales."),
                };

                foreach (var c in classes)
                    Console.WriteLine($"  {c.num,2}. {c.icon} {c.name,-14} - {c.desc}");

                Console.Write("\n  Opción: ");
                string op = Console.ReadLine() ?? "";

                Console.Write("  Nombre del personaje: ");
                string name = Console.ReadLine() ?? "Héroe";
                if (string.IsNullOrWhiteSpace(name)) name = "Héroe";

                Player? player = op switch
                {
                    "1" => new Warrior(name),
                    "2" => new Archer(name),
                    "3" => new Mage(name),
                    "4" => new Healer(name),
                    "5" => new Thief(name),
                    "6" => new Monk(name),
                    "7" => new Bard(name),
                    "8" => new Paladin(name),
                    "9" => new Necromancer(name),
                    "10" => new Alchemist(name),
                    _ => null
                };

                if (player != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ {player.ClassName} «{name}» creado correctamente.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return player;
                }
            }
        }

        // ───────────────── GAME LOOP ─────────────────
        static void GameLoop()
        {
            while (Players.Any(p => p.IsAlive()))
            {
                Console.Clear();
                highestRound = Math.Max(highestRound, round);
                List<Enemy> enemies = GenerateEnemies();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ══════════  RONDA {round}  ══════════");
                Console.ResetColor();

                if (round % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  ¡RONDA DE JEFE!  ⚠");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  Pulsa una tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey(true);

                Battle(enemies);
                round++;
            }

            ShowGameOver();
        }

        // ───────────────── GENERAR ENEMIGOS ─────────────────
        static List<Enemy> GenerateEnemies()
        {
            var enemies = new List<Enemy>();
            int scalingHp = round * 8;
            int scalingAtk = round * 2;

            if (round % 10 == 0)
            {
                enemies.Add(new LegendaryBoss("DRAGÓN ANCESTRAL", 500 + scalingHp, 50 + scalingAtk));
            }
            else if (round % 5 == 0)
            {
                enemies.Add(new BossEnemy("JEFE DE HORDA", 280 + scalingHp, 35 + scalingAtk));
            }
            else
            {
                int count = Players.Count + Rand.Next(1, 3);

                string[] types = { "Goblin", "Orco", "Esqueleto", "Vampiro", "Troll", "Bandido", "Lobo Oscuro" };
                for (int i = 0; i < count; i++)
                {
                    string eName = types[Rand.Next(types.Length)] + " " + (char)('A' + i);
                    int ehp = 50 + scalingHp + Rand.Next(30);
                    int eatk = 8 + scalingAtk + Rand.Next(5);

                    enemies.Add(Rand.Next(100) < 15
                        ? new EliteEnemy(eName + " ★", ehp + 40, eatk + 10)
                        : new Enemy(eName, ehp, eatk));
                }
            }

            return enemies;
        }

        // ───────────────── BATALLA ─────────────────
        static void Battle(List<Enemy> enemies)
        {
            int turnCount = 0;

            while (Players.Any(p => p.IsAlive()) && enemies.Count > 0)
            {
                turnCount++;

                // Turno de los jugadores
                foreach (var p in Players.ToList())
                {
                    if (!p.IsAlive()) continue;

                    p.Tick();

                    bool actionTaken = false;
                    while (!actionTaken)
                    {
                        Console.Clear();
                        ShowStatus(enemies);
                        ShowPlayerMenu(p);

                        string op = Console.ReadLine() ?? "";
                        actionTaken = HandlePlayerAction(p, op, enemies);
                    }

                    enemies.RemoveAll(e => !e.IsAlive());
                    if (enemies.Count == 0) break;
                }

                // Turno de los enemigos
                if (enemies.Count > 0)
                    EnemyTurn(enemies);

                // Remover jugadores muertos (queda registro)
                foreach (var p in Players.Where(p => !p.IsAlive()))
                    p.DeathMessage();
            }

            if (Players.Any(p => p.IsAlive()))
            {
                VictoryScreen(enemies);
            }
        }

        static bool HandlePlayerAction(Player p, string op, List<Enemy> enemies)
        {
            switch (op)
            {
                case "1": // Ataque normal
                    {
                        Enemy? target = ChooseTarget(enemies);
                        if (target == null) return false;

                        int dmg = p.Attack();
                        bool crit = Rand.Next(100) < p.CritChance;
                        if (crit) dmg = (int)(dmg * 1.8);

                        target.TakeDamage(dmg, p.AttackElement);

                        Console.ForegroundColor = crit ? ConsoleColor.Yellow : ConsoleColor.White;
                        Console.WriteLine(crit
                            ? $"\n  💥 ¡CRÍTICO! {p.Name} golpea a {target.Name} por {dmg}."
                            : $"\n  ⚔  {p.Name} ataca a {target.Name} por {dmg}.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        return true;
                    }

                case "2": // Habilidad especial
                    {
                        if (p.Mana < p.SpecialCost)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n  ✘ Sin maná (necesitas {p.SpecialCost}, tienes {p.Mana}).");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            return false;
                        }

                        p.Special(enemies);
                        enemies.RemoveAll(e => !e.IsAlive());
                        Console.ReadKey(true);
                        return true;
                    }

                case "3": // Defender
                    {
                        p.Defending = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n  🛡 {p.Name} adopta postura defensiva.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        return true;
                    }

                case "4": // Inventario
                    {
                        p.OpenInventory();
                        return true;
                    }

                case "5": // Tienda
                    {
                        Shop(p);
                        return true;
                    }

                case "6": // Habilidad pasiva/extra
                    {
                        p.PassiveAction(Players, enemies);
                        Console.ReadKey(true);
                        return true;
                    }

                case "7": // Mostrar estadísticas
                    {
                        ShowDetailedStats(p);
                        return false; // No consume turno
                    }

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ✘ Opción inválida.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return false;
            }
        }

        static Enemy? ChooseTarget(List<Enemy> enemies)
        {
            if (enemies.Count == 1) return enemies[0];

            Console.WriteLine("\n  Elige objetivo:");
            for (int i = 0; i < enemies.Count; i++)
                Console.WriteLine($"  {i + 1}. {enemies[i].Name} (HP:{enemies[i].Health}/{enemies[i].MaxHealth})");

            Console.Write("  → ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= enemies.Count)
                return enemies[idx - 1];

            return enemies[Rand.Next(enemies.Count)];
        }

        static void EnemyTurn(List<Enemy> enemies)
        {
            var alivePlayers = Players.Where(p => p.IsAlive()).ToList();
            if (alivePlayers.Count == 0) return;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ─── Turno de los enemigos ───\n");
            Console.ResetColor();

            foreach (var e in enemies)
            {
                e.Tick();
                if (!e.IsAlive()) continue;

                // Los enemigos atacan al jugador con menos HP (IA básica)
                Player target;

                // Bosses también pueden atacar al de más maná
                if (e is BossEnemy && Rand.Next(100) < 40)
                    target = alivePlayers.OrderByDescending(p => p.Mana).First();
                else
                    target = alivePlayers.OrderBy(p => p.Health).First();

                int dmg = e.Attack();

                if (target.Defending) dmg = (int)(dmg * 0.5);
                if (target.HasStatus(StatusEffect.Blessed)) dmg = (int)(dmg * 0.85);

                target.TakeDamage(dmg, ElementType.Physical);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"  {e.Name} ataca a {target.Name} por {dmg} daño.");
                Console.ResetColor();

                // Aplicar efecto de estado aleatorio de algunos enemigos
                if (e is EliteEnemy && Rand.Next(100) < 30)
                {
                    var badEffect = new[] { StatusEffect.Poisoned, StatusEffect.Burned, StatusEffect.Bleeding };
                    var effect = badEffect[Rand.Next(badEffect.Length)];
                    target.ApplyStatus(effect, 2);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"  ⚠  {target.Name} sufre {effect}.");
                    Console.ResetColor();
                }
            }

            Console.ReadKey(true);
        }

        // ───────────────── VICTORIA ─────────────────
        static void VictoryScreen(List<Enemy> killedEnemies)
        {
            totalKills += killedEnemies.Count + killedEnemies.Count; // approx

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            PrintBox("✔ VICTORIA", ConsoleColor.Green);
            Console.ResetColor();

            int goldEarned = Rand.Next(15, 50) + round * 3;
            int xpEarned = 25 + round * 5;

            if (round % 5 == 0) { goldEarned *= 2; xpEarned *= 2; }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Oro ganado: {goldEarned} 🪙");
            Console.WriteLine($"  XP ganada:  {xpEarned}");
            Console.ResetColor();

            foreach (var p in Players.Where(p => p.IsAlive()))
            {
                p.Gold += goldEarned;
                p.GainXP(xpEarned);
                p.RoundsWon++;

                // Drop aleatorio
                if (Rand.Next(100) < 25 + round)
                {
                    var drop = ItemDatabase.GetRandomDrop(round);
                    p.Inventory.Add(drop);
                    Console.ForegroundColor = drop.Rarity == Rarity.Legendary ? ConsoleColor.Magenta
                                            : drop.Rarity == Rarity.Epic ? ConsoleColor.Blue
                                            : ConsoleColor.Cyan;
                    Console.WriteLine($"\n  🎁 {p.Name} encontró: [{drop.Rarity}] {drop.Name}!");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n  Pulsa una tecla para la siguiente ronda...");
            Console.ReadKey(true);
        }

        static void ShowGameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
   ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗
  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗
  ██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝
  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗
  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║
   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝
            ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n  Ronda alcanzada: {round}");
            Console.WriteLine($"  Enemigos eliminados: ~{totalKills}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para salir...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── TIENDA ─────────────────
        static void Shop(Player p)
        {
            bool inShop = true;
            while (inShop)
            {
                Console.Clear();
                PrintBox($"🏪 TIENDA  (Oro: {p.Gold}🪙)", ConsoleColor.Yellow);

                var items = ShopDatabase.GetShopItems(round);

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    string rarityColor = item.Rarity switch
                    {
                        Rarity.Legendary => "★★★",
                        Rarity.Epic => "★★ ",
                        Rarity.Rare => "★  ",
                        _ => "   "
                    };
                    Console.ForegroundColor = item.Price > p.Gold ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.WriteLine($"  {i + 1}. {rarityColor} {item.Name,-25} {item.Price,4}🪙  — {item.Description}");
                }

                Console.ResetColor();
                Console.WriteLine("\n  0. Salir de la tienda");
                Console.Write("\n  Opción: ");

                string op = Console.ReadLine() ?? "";
                if (op == "0") { inShop = false; continue; }

                if (int.TryParse(op, out int idx) && idx >= 1 && idx <= items.Count)
                {
                    var chosen = items[idx - 1];
                    if (p.Gold >= chosen.Price)
                    {
                        p.Gold -= chosen.Price;
                        p.Inventory.Add(chosen);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n  ✔ Compraste: {chosen.Name}");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  ✘ Oro insuficiente.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                }
            }
        }

        // ───────────────── HUD ─────────────────
        static void ShowStatus(List<Enemy> enemies)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  ══════ RONDA {round} ══════\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ── JUGADORES ──");
            Console.ResetColor();
            foreach (var p in Players)
            {
                string hpBar = HealthBar(p.Health, p.MaxHealth, 15);
                string statusStr = p.HasAnyStatus() ? $" [{p.GetStatusString()}]" : "";
                string alive = p.IsAlive() ? "" : " 💀";
                Console.ForegroundColor = p.IsAlive() ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.WriteLine($"  {p.ClassName[0]}  {p.Name,-14} HP:{hpBar} {p.Health,3}/{p.MaxHealth}  MP:{p.Mana,3}  Lvl:{p.Level}  🪙{p.Gold}{statusStr}{alive}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ── ENEMIGOS ──");
            Console.ResetColor();
            foreach (var e in enemies)
            {
                string hpBar = HealthBar(e.Health, e.MaxHealth, 12);
                Console.ForegroundColor = e is BossEnemy ? ConsoleColor.DarkRed : ConsoleColor.DarkYellow;
                Console.WriteLine($"  {e.Name,-22} HP:{hpBar} {e.Health,3}/{e.MaxHealth}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        static void ShowPlayerMenu(Player p)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ─── Turno de {p.Name} ({p.ClassName}) ───");
            Console.ResetColor();
            Console.WriteLine("  1. ⚔  Atacar");
            Console.WriteLine($"  2. ✨ Especial  [{p.SpecialName}]  (Coste: {p.SpecialCost} MP)");
            Console.WriteLine("  3. 🛡 Defender  (-50% daño recibido)");
            Console.WriteLine("  4. 🎒 Inventario");
            Console.WriteLine("  5. 🏪 Tienda");
            Console.WriteLine("  6. 💡 Habilidad Extra");
            Console.WriteLine("  7. 📊 Ver estadísticas");
            Console.Write("\n  Acción: ");
        }

        static void ShowDetailedStats(Player p)
        {
            Console.Clear();
            PrintBox($"📊 ESTADÍSTICAS DE {p.Name.ToUpper()}", ConsoleColor.Cyan);
            Console.WriteLine($"\n  Clase:         {p.ClassName}");
            Console.WriteLine($"  Nivel:         {p.Level}");
            Console.WriteLine($"  XP:            {p.XP} / {p.Level * 50}");
            Console.WriteLine($"  HP:            {p.Health} / {p.MaxHealth}");
            Console.WriteLine($"  Maná:          {p.Mana}");
            Console.WriteLine($"  Ataque base:   {p.BaseAttack + p.AttackBonus}");
            Console.WriteLine($"  Defensa:       {p.Defense}");
            Console.WriteLine($"  Velocidad:     {p.Speed}");
            Console.WriteLine($"  Crítico:       {p.CritChance}%");
            Console.WriteLine($"  Elemento:      {p.AttackElement}");
            Console.WriteLine($"  Oro:           {p.Gold}");
            Console.WriteLine($"  Rondas ganadas:{p.RoundsWon}");
            Console.WriteLine($"\n  Inventario ({p.Inventory.Count} objetos):");
            if (p.Inventory.Count == 0)
                Console.WriteLine("    (vacío)");
            else
                foreach (var i in p.Inventory)
                    Console.WriteLine($"    - [{i.Rarity}] {i.Name}");

            Console.WriteLine("\n  Pulsa una tecla para volver...");
            Console.ReadKey(true);
        }

        // ───────────────── UTILIDADES ─────────────────
        static string HealthBar(int current, int max, int width)
        {
            if (max <= 0) return new string('░', width);
            int filled = (int)((float)current / max * width);
            filled = Math.Clamp(filled, 0, width);
            float pct = (float)current / max;

            Console.ForegroundColor = pct > 0.6f ? ConsoleColor.Green
                                    : pct > 0.3f ? ConsoleColor.Yellow
                                    : ConsoleColor.Red;

            string bar = "[" + new string('█', filled) + new string('░', width - filled) + "]";
            Console.ResetColor();
            return bar;
        }

        static void PrintBox(string title, ConsoleColor color)
        {
            string line = new string('═', title.Length + 4);
            Console.ForegroundColor = color;
            Console.WriteLine($"  ╔{line}╗");
            Console.WriteLine($"  ║  {title}  ║");
            Console.WriteLine($"  ╚{line}╝");
            Console.ResetColor();
        }
    }

    // ═══════════════════════════════════════════════
    //  ITEM SYSTEM
    // ═══════════════════════════════════════════════

    class Item
    {
        public string Name { get; }
        public string Description { get; }
        public ItemType Type { get; }
        public Rarity Rarity { get; }
        public int Price { get; }
        public int HPRestore { get; }
        public int MPRestore { get; }
        public int AtkBonus { get; }
        public int DefBonus { get; }
        public int MaxHPBonus { get; }

        public Item(string name, string desc, ItemType type, Rarity rarity, int price,
                    int hp = 0, int mp = 0, int atk = 0, int def = 0, int maxHp = 0)
        {
            Name = name; Description = desc; Type = type; Rarity = rarity; Price = price;
            HPRestore = hp; MPRestore = mp; AtkBonus = atk; DefBonus = def; MaxHPBonus = maxHp;
        }
    }

    static class ItemDatabase
    {
        static readonly Random R = new Random();
        static readonly List<Item> AllDrops = new()
        {
            new Item("Poción Menor",    "Restaura 30 HP",           ItemType.Potion,     Rarity.Common,    10,  hp: 30),
            new Item("Poción Mayor",    "Restaura 70 HP",           ItemType.Potion,     Rarity.Uncommon,  25,  hp: 70),
            new Item("Éter",            "Restaura 40 MP",           ItemType.Potion,     Rarity.Uncommon,  20,  mp: 40),
            new Item("Elixir",          "Restaura 80 HP y 40 MP",   ItemType.Potion,     Rarity.Rare,      50,  hp: 80, mp: 40),
            new Item("Daga de Hierro",  "+5 ATK",                   ItemType.Weapon,     Rarity.Common,    30,  atk: 5),
            new Item("Espada de Acero", "+12 ATK",                  ItemType.Weapon,     Rarity.Uncommon,  60,  atk: 12),
            new Item("Hacha Rúnica",    "+20 ATK",                  ItemType.Weapon,     Rarity.Rare,     120,  atk: 20),
            new Item("Espada Legendaria","+30 ATK + 20 MaxHP",     ItemType.Weapon,     Rarity.Legendary,250,  atk: 30, maxHp: 20),
            new Item("Escudo de Bronce","+5 DEF",                   ItemType.Armor,      Rarity.Common,    30,  def: 5),
            new Item("Armadura de Hierro","+12 DEF",                ItemType.Armor,      Rarity.Uncommon,  70,  def: 12),
            new Item("Cota Épica",      "+20 DEF +20 MaxHP",        ItemType.Armor,      Rarity.Epic,     160,  def: 20, maxHp: 20),
            new Item("Amuleto de Vida", "+30 MaxHP",                ItemType.Accessory,  Rarity.Rare,      90,  maxHp: 30),
            new Item("Pergamino de Fuego","Especial de fuego",      ItemType.Scroll,     Rarity.Uncommon,  35),
        };

        public static Item GetRandomDrop(int round)
        {
            var pool = AllDrops.Where(i =>
                round >= 5 || i.Rarity == Rarity.Common || i.Rarity == Rarity.Uncommon).ToList();
            if (round >= 10)
                pool = AllDrops.ToList();

            return pool[R.Next(pool.Count)];
        }
    }

    static class ShopDatabase
    {
        public static List<Item> GetShopItems(int round)
        {
            var shop = new List<Item>
            {
                new Item("Poción Menor",    "Restaura 30 HP",        ItemType.Potion,   Rarity.Common,   10, hp: 30),
                new Item("Poción Mayor",    "Restaura 70 HP",        ItemType.Potion,   Rarity.Uncommon, 25, hp: 70),
                new Item("Éter",            "Restaura 40 MP",        ItemType.Potion,   Rarity.Uncommon, 20, mp: 40),
                new Item("Elixir",          "+80 HP / +40 MP",       ItemType.Potion,   Rarity.Rare,     50, hp: 80, mp: 40),
                new Item("Daga de Hierro",  "+5 ATK",                ItemType.Weapon,   Rarity.Common,   30, atk: 5),
                new Item("Espada de Acero", "+12 ATK",               ItemType.Weapon,   Rarity.Uncommon, 60, atk: 12),
                new Item("Escudo de Bronce","+5 DEF",                ItemType.Armor,    Rarity.Common,   30, def: 5),
                new Item("Armadura de Hierro","+12 DEF",             ItemType.Armor,    Rarity.Uncommon, 70, def: 12),
                new Item("Amuleto de Vida", "+30 MaxHP",             ItemType.Accessory,Rarity.Rare,     90, maxHp: 30),
            };

            if (round >= 5)
            {
                shop.Add(new Item("Hacha Rúnica", "+20 ATK", ItemType.Weapon, Rarity.Rare, 120, atk: 20));
                shop.Add(new Item("Cota Épica", "+20 DEF +20 HP", ItemType.Armor, Rarity.Epic, 160, def: 20, maxHp: 20));
            }

            if (round >= 10)
            {
                shop.Add(new Item("Espada Legendaria", "+30 ATK +20 HP", ItemType.Weapon, Rarity.Legendary, 250, atk: 30, maxHp: 20));
            }

            return shop;
        }
    }

    // ═══════════════════════════════════════════════
    //  CLASE BASE CHARACTER
    // ═══════════════════════════════════════════════

    abstract class Character
    {
        static readonly Random R = new Random();

        public string Name { get; protected set; }
        public int Health { get; set; }
        public int MaxHealth { get; protected set; }
        public int Mana { get; set; }
        public bool Defending { get; set; }
        public int Defense { get; protected set; }
        public int Speed { get; protected set; }

        private readonly Dictionary<StatusEffect, int> _statusDurations = new();

        protected Character(string name, int hp, int mp, int def = 0, int spd = 10)
        {
            Name = name; MaxHealth = hp; Health = hp; Mana = mp; Defense = def; Speed = spd;
        }

        public bool IsAlive() => Health > 0;

        public void TakeDamage(int dmg, ElementType element = ElementType.Physical)
        {
            if (Defending) dmg = (int)(dmg * 0.5);
            dmg = Math.Max(1, dmg - Defense / 5);
            Health = Math.Max(0, Health - dmg);
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
        }

        public void ApplyStatus(StatusEffect effect, int turns)
        {
            if (_statusDurations.ContainsKey(effect))
                _statusDurations[effect] = Math.Max(_statusDurations[effect], turns);
            else
                _statusDurations[effect] = turns;
        }

        public bool HasStatus(StatusEffect effect) => _statusDurations.ContainsKey(effect) && _statusDurations[effect] > 0;
        public bool HasAnyStatus() => _statusDurations.Any(kv => kv.Value > 0);

        public string GetStatusString()
        {
            return string.Join(",", _statusDurations
                .Where(kv => kv.Value > 0)
                .Select(kv => kv.Key.ToString().Substring(0, 3)));
        }

        public void TickStatus()
        {
            var keys = _statusDurations.Keys.ToList();
            foreach (var k in keys)
            {
                if (k == StatusEffect.Poisoned && _statusDurations[k] > 0) Health = Math.Max(1, Health - 8);
                if (k == StatusEffect.Burned && _statusDurations[k] > 0) Health = Math.Max(1, Health - 6);
                if (k == StatusEffect.Bleeding && _statusDurations[k] > 0) Health = Math.Max(1, Health - 10);
                if (_statusDurations[k] > 0) _statusDurations[k]--;
            }
        }

        public void ClearStatus() => _statusDurations.Clear();
    }

    // ═══════════════════════════════════════════════
    //  CLASE BASE PLAYER
    // ═══════════════════════════════════════════════

    abstract class Player : Character
    {
        public int Gold { get; set; }
        public int XP { get; set; }
        public int Level { get; private set; } = 1;
        public int AttackBonus { get; set; }
        public int BaseAttack { get; protected set; }
        public int CritChance { get; protected set; } = 10;
        public int SpecialCost { get; protected set; } = 10;
        public int RoundsWon { get; set; }

        public string ClassName { get; protected set; } = "Aventurero";
        public string SpecialName { get; protected set; } = "Especial";
        public ElementType AttackElement { get; protected set; } = ElementType.Physical;

        public List<Item> Inventory { get; } = new List<Item>();

        static readonly Random R = new Random();

        protected Player(string name, int hp, int mp, int baseAtk, int def = 0, int spd = 10)
            : base(name, hp, mp, def, spd)
        {
            BaseAttack = baseAtk;
        }

        public void Tick()
        {
            Mana = Math.Min(Mana + 4, 150);
            Defending = false;
            TickStatus();
        }

        public int Attack() => BaseAttack + AttackBonus + R.Next(-3, 4);

        public void GainXP(int xp)
        {
            XP += xp;
            while (XP >= Level * 50)
            {
                XP -= Level * 50;
                Level++;
                MaxHealth += 15;
                Health = MaxHealth;
                Mana += 10;
                BaseAttack += 3;
                Defense += 1;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  🎉 {Name} subió a nivel {Level}! HP Max:{MaxHealth}, ATK:{BaseAttack}");
                Console.ResetColor();
            }
        }

        public void DeathMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n  💀 {Name} ha caído en batalla...");
            Console.ResetColor();
        }

        public void OpenInventory()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  🎒 INVENTARIO DE {Name.ToUpper()}  (Oro: {Gold}🪙)");
                Console.ResetColor();

                if (Inventory.Count == 0)
                {
                    Console.WriteLine("\n  (Sin objetos)");
                    Console.WriteLine("\n  Pulsa una tecla para volver...");
                    Console.ReadKey(true);
                    return;
                }

                for (int i = 0; i < Inventory.Count; i++)
                {
                    var item = Inventory[i];
                    Console.WriteLine($"  {i + 1}. [{item.Rarity}] {item.Name} — {item.Description}");
                }

                Console.WriteLine("\n  0. Volver");
                Console.Write("  Usar objeto #: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") { open = false; continue; }

                if (int.TryParse(op, out int idx) && idx >= 1 && idx <= Inventory.Count)
                {
                    UseItem(Inventory[idx - 1]);
                    open = false;
                }
            }
        }

        private void UseItem(Item item)
        {
            if (item.HPRestore > 0) { Heal(item.HPRestore); Console.WriteLine($"  ✔ Recuperas {item.HPRestore} HP."); }
            if (item.MPRestore > 0) { Mana = Math.Min(Mana + item.MPRestore, 150); Console.WriteLine($"  ✔ Recuperas {item.MPRestore} MP."); }
            if (item.AtkBonus > 0) { AttackBonus += item.AtkBonus; Console.WriteLine($"  ✔ ATK +{item.AtkBonus} (permanente)."); }
            if (item.DefBonus > 0) { Defense += item.DefBonus; Console.WriteLine($"  ✔ DEF +{item.DefBonus} (permanente)."); }
            if (item.MaxHPBonus > 0) { MaxHealth += item.MaxHPBonus; Health = Math.Min(Health + item.MaxHPBonus, MaxHealth); Console.WriteLine($"  ✔ MaxHP +{item.MaxHPBonus} (permanente)."); }

            if (item.Type == ItemType.Potion || item.Type == ItemType.Scroll)
                Inventory.Remove(item);

            Console.ReadKey(true);
        }

        public virtual void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            Console.WriteLine($"\n  {Name} no tiene habilidad extra disponible.");
        }

        public abstract void Special(List<Enemy> enemies);
    }

    // ═══════════════════════════════════════════════
    //  ENEMIES
    // ═══════════════════════════════════════════════

    class Enemy : Character
    {
        protected int BaseAtk;
        static readonly Random R = new Random();

        public Enemy(string name, int hp, int atk) : base(name, hp, 0)
        {
            BaseAtk = atk;
        }

        public virtual int Attack() => BaseAtk + R.Next(-3, 4);

        public void Tick() => TickStatus();
    }

    class EliteEnemy : Enemy
    {
        public EliteEnemy(string name, int hp, int atk) : base(name, hp, atk)
        {
            Defense = 5;
        }

        public override int Attack() => base.Attack() + 5;
    }

    class BossEnemy : Enemy
    {
        static readonly Random R = new Random();
        int phase = 1;

        public BossEnemy(string name, int hp, int atk) : base(name, hp, atk)
        {
            Defense = 10;
        }

        public override int Attack()
        {
            if (Health < MaxHealth / 2 && phase == 1)
            {
                phase = 2;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n  ⚠  ¡{Name} entra en FASE 2!");
                Console.ResetColor();
            }
            return base.Attack() * phase + R.Next(0, 10);
        }
    }

    class LegendaryBoss : BossEnemy
    {
        static readonly Random R = new Random();

        public LegendaryBoss(string name, int hp, int atk) : base(name, hp, atk)
        {
            Defense = 20;
        }

        public override int Attack() => base.Attack() + R.Next(10, 25);
    }

    // ═══════════════════════════════════════════════
    //  CLASES DE JUGADOR
    // ═══════════════════════════════════════════════

    // ── 1. GUERRERO ──────────────────────────────
    class Warrior : Player
    {
        int rageStacks = 0;
        static readonly Random R = new Random();

        public Warrior(string n) : base(n, 180, 40, 22, def: 12, spd: 8)
        {
            ClassName = "Guerrero"; SpecialName = "Golpe Devastador"; SpecialCost = 15;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.Health).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 2.5) + rageStacks * 5;
            target.TakeDamage(dmg, ElementType.Physical);
            rageStacks = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ⚔  {Name} usa GOLPE DEVASTADOR en {target.Name} por {dmg}!");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            rageStacks = Math.Min(rageStacks + 3, 15);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n  😤 {Name} acumula RABIA ({rageStacks} stacks). Su próximo Especial será más fuerte.");
            Console.ResetColor();
        }
    }

    // ── 2. ARQUERO ──────────────────────────────
    class Archer : Player
    {
        static readonly Random R = new Random();
        bool multishot = false;

        public Archer(string n) : base(n, 110, 60, 18, def: 5, spd: 15)
        {
            ClassName = "Arquero"; SpecialName = "Lluvia de Flechas"; SpecialCost = 20; CritChance = 25;
            AttackElement = ElementType.Wind;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  🏹 {Name} usa LLUVIA DE FLECHAS!");
            foreach (var e in enemies)
            {
                int dmg = (BaseAttack + AttackBonus) + R.Next(5, 20);
                e.TakeDamage(dmg, ElementType.Wind);
                Console.WriteLine($"     → {e.Name} recibe {dmg} de daño.");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            multishot = !multishot;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(multishot
                ? $"\n  🏹 {Name} activa DISPARO MÚLTIPLE: el siguiente ataque golpea a 2 enemigos."
                : $"\n  🏹 {Name} desactiva DISPARO MÚLTIPLE.");
            Console.ResetColor();
        }
    }

    // ── 3. MAGO ──────────────────────────────
    class Mage : Player
    {
        static readonly Random R = new Random();
        int spellCharge = 0;

        public Mage(string n) : base(n, 85, 130, 12, def: 3, spd: 10)
        {
            ClassName = "Mago"; SpecialName = "Tormenta Arcana"; SpecialCost = 25; CritChance = 15;
            AttackElement = ElementType.Fire;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int bonus = spellCharge * 8;
            spellCharge = 0;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  🔥 {Name} desata TORMENTA ARCANA! (Bonus cargado: {bonus})");
            foreach (var e in enemies)
            {
                int dmg = (int)((BaseAttack + AttackBonus) * 1.8) + bonus + R.Next(10, 25);
                e.TakeDamage(dmg, ElementType.Fire);
                Console.WriteLine($"     → {e.Name} recibe {dmg} de fuego.");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            spellCharge = Math.Min(spellCharge + 2, 10);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  🔮 {Name} carga energía arcana. Carga: {spellCharge}/10.");
            Console.ResetColor();
        }
    }

    // ── 4. CURANDERO ──────────────────────────────
    class Healer : Player
    {
        static readonly Random R = new Random();
        bool massHealReady = false;

        public Healer(string n) : base(n, 120, 110, 9, def: 6, spd: 9)
        {
            ClassName = "Curandero"; SpecialName = "Luz Sagrada"; SpecialCost = 20;
            AttackElement = ElementType.Holy;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            // Cura a todos los aliados vivos
            foreach (var p in Program.Players.Where(p => p.IsAlive()))
            {
                int heal = 45 + R.Next(10);
                p.Heal(heal);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  ✨ {Name} cura a {p.Name} por {heal} HP.");
                Console.ResetColor();
            }
            // Daño sagrado al objetivo más poderoso
            var boss = enemies.OrderByDescending(e => e.MaxHealth).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 1.2);
            boss.TakeDamage(dmg, ElementType.Holy);
            Console.WriteLine($"  ✝ Daño sagrado a {boss.Name} por {dmg}.");
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Limpiar efectos negativos de un aliado
            var hurt = allies.Where(p => p.IsAlive() && p.HasAnyStatus()).FirstOrDefault();
            if (hurt != null)
            {
                hurt.ClearStatus();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🌟 {Name} purifica a {hurt.Name} de todos los efectos negativos.");
                Console.ResetColor();
            }
            else
            {
                // Regen pasiva
                foreach (var p in allies.Where(p => p.IsAlive()))
                    p.Heal(10);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n  🌿 {Name} lanza Aura Sanadora. Todos recuperan 10 HP.");
                Console.ResetColor();
            }
        }
    }

    // ── 5. LADRÓN ──────────────────────────────
    class Thief : Player
    {
        static readonly Random R = new Random();
        bool stealthed = false;

        public Thief(string n) : base(n, 95, 70, 19, def: 4, spd: 20)
        {
            ClassName = "Ladrón"; SpecialName = "Golpe Bajo"; SpecialCost = 12; CritChance = 30;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderBy(e => e.Health).First();
            int baseDmg = (int)((BaseAttack + AttackBonus) * (stealthed ? 3.5 : 2.2));
            target.TakeDamage(baseDmg, ElementType.Physical);

            // Robo de oro
            int stolen = R.Next(5, 15);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n  🗡  {Name} usa GOLPE BAJO en {target.Name} por {baseDmg}! Roba {stolen}🪙");
            Console.ResetColor();
            Gold += stolen;
            stealthed = false;
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            stealthed = true;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  👤 {Name} entra en SIGILO. Próximo especial hace 3.5x daño y es invisible.");
            Console.ResetColor();
        }
    }

    // ── 6. MONJE ──────────────────────────────
    class Monk : Player
    {
        static readonly Random R = new Random();
        int comboCount = 0;

        public Monk(string n) : base(n, 130, 50, 18, def: 9, spd: 17)
        {
            ClassName = "Monje"; SpecialName = "Ráfaga de Golpes"; SpecialCost = 15; CritChance = 18;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.Health).First();
            int hits = 3 + comboCount / 2;
            int totalDmg = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  👊 {Name} usa RÁFAGA DE GOLPES ({hits} impactos):");
            for (int i = 0; i < hits; i++)
            {
                int dmg = (BaseAttack + AttackBonus) / 2 + R.Next(8, 18);
                target.TakeDamage(dmg, ElementType.Physical);
                totalDmg += dmg;
                Console.Write($" {dmg}");
            }
            Console.WriteLine($"\n  Total: {totalDmg}");
            Console.ResetColor();
            comboCount = Math.Min(comboCount + 1, 10);
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Meditación: recupera HP y MP
            int hpReg = 20 + Level * 3;
            int mpReg = 15;
            Heal(hpReg);
            Mana = Math.Min(Mana + mpReg, 150);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  🧘 {Name} medita. Recupera {hpReg} HP y {mpReg} MP.");
            Console.ResetColor();
        }
    }

    // ── 7. BARDO ──────────────────────────────
    class Bard : Player
    {
        static readonly Random R = new Random();
        bool songActive = false;

        public Bard(string n) : base(n, 105, 90, 12, def: 5, spd: 13)
        {
            ClassName = "Bardo"; SpecialName = "Canción de Batalla"; SpecialCost = 20;
            AttackElement = ElementType.Wind;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            songActive = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  🎵 {Name} entona CANCIÓN DE BATALLA!");

            foreach (var p in Program.Players.Where(p => p.IsAlive()))
            {
                p.AttackBonus += 6;
                p.Heal(20);
                Console.WriteLine($"     → {p.Name}: +6 ATK, +20 HP");
            }

            // Debuff a todos los enemigos
            foreach (var e in enemies)
            {
                e.TakeDamage(12, ElementType.Wind);
                e.ApplyStatus(StatusEffect.Stunned, 1);
            }
            Console.WriteLine($"  Enemigos debilitados y aturdidos.");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Balada de inspiración: sube XP del grupo
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  🎶 {Name} toca la BALADA DE INSPIRACIÓN. +10 XP a todos.");
            foreach (var p in allies.Where(p => p.IsAlive()))
                p.GainXP(10);
            Console.ResetColor();
        }
    }

    // ── 8. PALADÍN ──────────────────────────────
    class Paladin : Player
    {
        static readonly Random R = new Random();
        bool divineShield = false;

        public Paladin(string n) : base(n, 155, 70, 19, def: 14, spd: 9)
        {
            ClassName = "Paladín"; SpecialName = "Juicio Divino"; SpecialCost = 20;
            AttackElement = ElementType.Holy;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.MaxHealth).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 2.8);
            target.TakeDamage(dmg, ElementType.Holy);
            target.ApplyStatus(StatusEffect.Cursed, 3);

            // Auto-cura
            Heal(30);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  ✝  {Name} usa JUICIO DIVINO en {target.Name} por {dmg}! Recupera 30 HP.");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            divineShield = !divineShield;
            if (divineShield)
            {
                foreach (var p in allies.Where(p => p.IsAlive()))
                    p.ApplyStatus(StatusEffect.Blessed, 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🛡 {Name} activa ESCUDO DIVINO. Todos los aliados reciben Bendición (3 turnos, -15% daño).");
            }
            else
            {
                Console.WriteLine($"\n  🛡 {Name} retira el Escudo Divino.");
            }
            Console.ResetColor();
        }
    }

    // ── 9. NIGROMANTE ──────────────────────────────
    class Necromancer : Player
    {
        static readonly Random R = new Random();
        int soulStacks = 0;

        public Necromancer(string n) : base(n, 95, 120, 13, def: 3, spd: 10)
        {
            ClassName = "Nigromante"; SpecialName = "Drenaje de Almas"; SpecialCost = 22;
            AttackElement = ElementType.Dark;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int totalDrained = 0;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  🌑 {Name} usa DRENAJE DE ALMAS!");
            foreach (var e in enemies)
            {
                int drain = (int)((BaseAttack + AttackBonus) * 1.2) + soulStacks * 5;
                e.TakeDamage(drain, ElementType.Dark);
                totalDrained += drain / 3;
                Console.WriteLine($"     → Drena {drain} de {e.Name}.");
            }

            Heal(totalDrained);
            soulStacks = 0;
            Console.WriteLine($"  💜 {Name} absorbe {totalDrained} HP.");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            soulStacks = Math.Min(soulStacks + 3, 20);
            // Maldición grupal
            foreach (var e in enemies)
                e.ApplyStatus(StatusEffect.Cursed, 2);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  💀 {Name} lanza MALDICIÓN GRUPAL. Acumula {soulStacks} almas para el Drenaje.");
            Console.ResetColor();
        }
    }

    // ── 10. ALQUIMISTA ──────────────────────────────
    class Alchemist : Player
    {
        static readonly Random R = new Random();
        int brewCount = 0;

        public Alchemist(string n) : base(n, 100, 100, 14, def: 6, spd: 12)
        {
            ClassName = "Alquimista"; SpecialName = "Bomba Alquímica"; SpecialCost = 18;
            AttackElement = ElementType.Poison;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            brewCount++;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n  ⚗ {Name} lanza BOMBA ALQUÍMICA! (Brews: {brewCount})");

            int splashDmg = (int)((BaseAttack + AttackBonus) * 1.6) + brewCount * 4;
            foreach (var e in enemies)
            {
                e.TakeDamage(splashDmg, ElementType.Poison);
                e.ApplyStatus(StatusEffect.Poisoned, 3);
                Console.WriteLine($"     → {e.Name} recibe {splashDmg} + VENENO (3 turnos).");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Poción improvisada para el aliado más dañado
            var hurt = allies.Where(p => p.IsAlive())
                             .OrderBy(p => (float)p.Health / p.MaxHealth)
                             .First();
            int healAmt = 35 + Level * 5;
            hurt.Heal(healAmt);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ⚗ {Name} prepara POCIÓN IMPROVISADA para {hurt.Name}: +{healAmt} HP.");
            Console.ResetColor();
        }
    }

}
