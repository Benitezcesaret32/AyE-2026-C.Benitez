using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_FINAL
{
    // ═══════════════════════════════════════════════
    //  ENUMS Y CONSTANTES GLOBALES
    // ═══════════════════════════════════════════════

    enum StatusEffect
    {
        None, Poisoned, Burned, Stunned, Bleeding, Frozen, Cursed,
        Blessed, Enraged, Silenced, Weakened, Hasted, Shielded,
        Regenerating, Empowered, Confused, Petrified, Invisible,
        Berserked, Charmed, Doomed
    }

    enum ElementType { Physical, Fire, Ice, Lightning, Dark, Holy, Poison, Wind, Earth, Water, Arcane, Metal }

    enum Rarity { Common, Uncommon, Rare, Epic, Legendary, Mythic }

    enum ItemType { Potion, Weapon, Armor, Accessory, Scroll, Material, Food, Key, Relic }

    enum DungeonType { Cave, Ruins, Forest, Volcano, IcePeak, ShadowRealm, HolyTemple, Abyss }

    enum WeatherType { Clear, Rainy, Stormy, Foggy, Blizzard, Heatwave, Eclipse, Blessed }

    enum QuestStatus { Available, Active, Completed, Failed }

    enum GuildRank { Rookie, Bronze, Silver, Gold, Platinum, Diamond, Legend }

    enum CraftingCategory { Potions, Weapons, Armor, Accessories, Scrolls, Food }

    enum TalentTree { Combat, Magic, Support, Stealth, Nature }

    enum BiomeType { Plains, Desert, Mountains, Swamp, Tundra, Jungle, Underworld, Celestial }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE CONFIGURACIÓN GLOBAL
    // ═══════════════════════════════════════════════

    static class GameConfig
    {
        public const int MAX_PLAYERS = 4;
        public const int MAX_INVENTORY = 30;
        public const int MAX_LEVEL = 99;
        public const int MAX_TALENT_POINTS = 50;
        public const int MAX_GUILD_LEVEL = 20;
        public const int BASE_CRIT_MULTIPLIER = 2;
        public const float DEFEND_REDUCTION = 0.5f;
        public const float BLESSED_REDUCTION = 0.85f;
        public const int SHOP_REFRESH_COST = 10;
        public const int MAX_PARTY_BUFFS = 5;

        public static readonly string[] EnemyTypes =
        {
            "Goblin", "Orco", "Esqueleto", "Vampiro", "Troll",
            "Bandido", "Lobo Oscuro", "Dragón Menor", "Elemental",
            "Espectro", "Demonio", "Gólem", "Cultista", "Mercenario",
            "Serpiente Gigante", "Araña Venenosa", "Zombi", "Liche",
            "Barón Infernal", "Hidra", "Quimera", "Mantícora", "Fénix Oscuro"
        };

        public static readonly ConsoleColor[] RarityColors =
        {
            ConsoleColor.Gray,       // Common
            ConsoleColor.Green,      // Uncommon
            ConsoleColor.Cyan,       // Rare
            ConsoleColor.Blue,       // Epic
            ConsoleColor.Magenta,    // Legendary
            ConsoleColor.Yellow      // Mythic
        };

        public static readonly string[] RarityStars =
        {
            "☆☆☆☆☆", "★☆☆☆☆", "★★☆☆☆", "★★★☆☆", "★★★★☆", "★★★★★"
        };
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE ESTADÍSTICAS GLOBALES
    // ═══════════════════════════════════════════════

    static class GlobalStats
    {
        public static int TotalDamageDealt = 0;
        public static int TotalDamageReceived = 0;
        public static int TotalHealingDone = 0;
        public static int TotalEnemiesKilled = 0;
        public static int TotalBossesKilled = 0;
        public static int TotalItemsCrafted = 0;
        public static int TotalItemsUsed = 0;
        public static int TotalGoldEarned = 0;
        public static int TotalGoldSpent = 0;
        public static int TotalCriticalHits = 0;
        public static int TotalMissedAttacks = 0;
        public static int TotalQuestsCompleted = 0;
        public static int TotalDungeonsCleared = 0;
        public static int HighestRound = 0;
        public static int HighestDamageInOneTurn = 0;
        public static string HighestDamageDealer = "N/A";
        public static DateTime GameStartTime = DateTime.Now;

        public static void ShowStats()
        {
            Console.Clear();
            Program.PrintBox("📊 ESTADÍSTICAS GLOBALES DE LA PARTIDA", ConsoleColor.Cyan);
            TimeSpan played = DateTime.Now - GameStartTime;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n  ⏱  Tiempo jugado:        {played.Hours:D2}:{played.Minutes:D2}:{played.Seconds:D2}");
            Console.WriteLine($"  🏆 Ronda más alta:       {HighestRound}");
            Console.WriteLine($"\n  ─── COMBATE ───");
            Console.WriteLine($"  ⚔  Daño total infligido: {TotalDamageDealt:N0}");
            Console.WriteLine($"  🛡 Daño total recibido:  {TotalDamageReceived:N0}");
            Console.WriteLine($"  💚 Curación total:       {TotalHealingDone:N0}");
            Console.WriteLine($"  💀 Enemigos eliminados:  {TotalEnemiesKilled}");
            Console.WriteLine($"  👑 Jefes derrotados:     {TotalBossesKilled}");
            Console.WriteLine($"  💥 Golpes críticos:      {TotalCriticalHits}");
            Console.WriteLine($"  ✨ Mayor daño en turno:  {HighestDamageInOneTurn} ({HighestDamageDealer})");
            Console.WriteLine($"\n  ─── ECONOMÍA ───");
            Console.WriteLine($"  🪙 Oro ganado total:     {TotalGoldEarned:N0}");
            Console.WriteLine($"  🛒 Oro gastado total:    {TotalGoldSpent:N0}");
            Console.WriteLine($"\n  ─── PROGRESIÓN ───");
            Console.WriteLine($"  🔨 Objetos fabricados:   {TotalItemsCrafted}");
            Console.WriteLine($"  💊 Objetos usados:       {TotalItemsUsed}");
            Console.WriteLine($"  📜 Misiones completadas: {TotalQuestsCompleted}");
            Console.WriteLine($"  🗺  Mazmorras exploradas: {TotalDungeonsCleared}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para volver...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE TIEMPO Y CLIMA
    // ═══════════════════════════════════════════════

    static class WorldTime
    {
        static readonly Random R = new Random();
        public static int Day { get; private set; } = 1;
        public static int Hour { get; private set; } = 6;
        public static WeatherType CurrentWeather { get; private set; } = WeatherType.Clear;
        public static BiomeType CurrentBiome { get; private set; } = BiomeType.Plains;
        private static int weatherDuration = 0;

        public static string TimeOfDay => Hour switch
        {
            >= 6 and < 12 => "🌅 Mañana",
            >= 12 and < 18 => "☀ Tarde",
            >= 18 and < 22 => "🌆 Atardecer",
            _ => "🌙 Noche"
        };

        public static bool IsNight => Hour >= 22 || Hour < 6;

        public static void Advance(int hours = 1)
        {
            Hour = (Hour + hours) % 24;
            if (Hour == 0) Day++;
            UpdateWeather();
        }

        private static void UpdateWeather()
        {
            if (weatherDuration > 0) { weatherDuration--; return; }
            var weathers = Enum.GetValues<WeatherType>();
            CurrentWeather = weathers[R.Next(weathers.Length)];
            weatherDuration = R.Next(2, 6);
        }

        public static void ChangeBiome(BiomeType biome) => CurrentBiome = biome;

        public static string GetWeatherIcon() => CurrentWeather switch
        {
            WeatherType.Clear => "☀",
            WeatherType.Rainy => "🌧",
            WeatherType.Stormy => "⛈",
            WeatherType.Foggy => "🌫",
            WeatherType.Blizzard => "❄",
            WeatherType.Heatwave => "🔥",
            WeatherType.Eclipse => "🌑",
            WeatherType.Blessed => "✨",
            _ => "?"
        };

        public static (int atkMod, int defMod, int spdMod, string desc) GetWeatherEffects() => CurrentWeather switch
        {
            WeatherType.Rainy => (-2, 0, -1, "Lluvia: -2 ATK, -1 VEL"),
            WeatherType.Stormy => (-5, -3, -3, "Tormenta: -5 ATK, -3 DEF, -3 VEL"),
            WeatherType.Foggy => (-3, 0, 0, "Niebla: -3 ATK (visibilidad reducida)"),
            WeatherType.Blizzard => (-4, -2, -5, "Ventisca: Penalidades severas"),
            WeatherType.Heatwave => (3, -2, -2, "Calor: +3 ATK fuego, -2 DEF, -2 VEL"),
            WeatherType.Eclipse => (0, -5, 0, "Eclipse: Monstruos fortalecidos"),
            WeatherType.Blessed => (3, 3, 3, "Clima Bendito: +3 a todo"),
            _ => (0, 0, 0, "Clima despejado")
        };

        public static string GetBiomeIcon() => CurrentBiome switch
        {
            BiomeType.Plains => "🌾",
            BiomeType.Desert => "🏜",
            BiomeType.Mountains => "⛰",
            BiomeType.Swamp => "🌿",
            BiomeType.Tundra => "❄",
            BiomeType.Jungle => "🌴",
            BiomeType.Underworld => "🔥",
            BiomeType.Celestial => "✨",
            _ => "?"
        };

        public static void ShowWorldInfo()
        {
            var (atkMod, defMod, spdMod, desc) = GetWeatherEffects();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"  {GetBiomeIcon()} Bioma: {CurrentBiome}  |  {GetWeatherIcon()} Clima: {CurrentWeather}  |  {TimeOfDay} (Día {Day})");
            if (atkMod != 0 || defMod != 0 || spdMod != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  ⚠  Efecto climático: {desc}");
            }
            Console.ResetColor();
        }
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE REPUTACIÓN Y GREMIO
    // ═══════════════════════════════════════════════

    class Guild
    {
        public string Name { get; private set; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; }
        public int GuildGold { get; private set; }
        public GuildRank Rank { get; private set; } = GuildRank.Rookie;
        public List<string> Members { get; private set; } = new();
        public List<string> CompletedGuildQuests { get; private set; } = new();
        public int TotalKills { get; set; }
        public int BonusAttack => (Level - 1) * 2;
        public int BonusDefense => (Level - 1);
        public int BonusGoldPercent => Level * 5;

        static readonly Random R = new Random();

        public Guild(string name)
        {
            Name = name;
        }

        public void AddExperience(int xp)
        {
            Experience += xp;
            int needed = Level * 100;
            while (Experience >= needed && Level < GameConfig.MAX_GUILD_LEVEL)
            {
                Experience -= needed;
                Level++;
                needed = Level * 100;
                UpdateRank();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🏰 ¡El Gremio «{Name}» subió a Nivel {Level}! Bonificaciones mejoradas.");
                Console.ResetColor();
            }
        }

        public void AddGold(int amount) => GuildGold += amount;

        private void UpdateRank()
        {
            Rank = Level switch
            {
                >= 18 => GuildRank.Legend,
                >= 15 => GuildRank.Diamond,
                >= 12 => GuildRank.Platinum,
                >= 9 => GuildRank.Gold,
                >= 6 => GuildRank.Silver,
                >= 3 => GuildRank.Bronze,
                _ => GuildRank.Rookie
            };
        }

        public void ShowInfo()
        {
            Console.Clear();
            Program.PrintBox($"🏰 GREMIO: {Name.ToUpper()}", ConsoleColor.Yellow);
            Console.WriteLine($"\n  Rango:       {Rank}");
            Console.WriteLine($"  Nivel:       {Level} / {GameConfig.MAX_GUILD_LEVEL}");
            Console.WriteLine($"  Experiencia: {Experience} / {Level * 100}");
            Console.WriteLine($"  Oro gremial: {GuildGold}🪙");
            Console.WriteLine($"  Miembros:    {string.Join(", ", Members)}");
            Console.WriteLine($"\n  ─── BONIFICACIONES ACTIVAS ───");
            Console.WriteLine($"  ⚔  ATK Bonus:    +{BonusAttack}");
            Console.WriteLine($"  🛡 DEF Bonus:    +{BonusDefense}");
            Console.WriteLine($"  🪙 Oro extra:    +{BonusGoldPercent}%");
            Console.WriteLine($"\n  Misiones gremiales completadas: {CompletedGuildQuests.Count}");
            Console.WriteLine($"  Enemigos eliminados en gremio:  {TotalKills}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para volver...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE MISIONES (QUESTS)
    // ═══════════════════════════════════════════════

    class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public QuestStatus Status { get; set; } = QuestStatus.Available;
        public int GoldReward { get; }
        public int XPReward { get; }
        public int KillsRequired { get; }
        public int CurrentKills { get; set; }
        public string? SpecialRewardItem { get; }
        public int RoundsLimit { get; }
        public int StartedRound { get; set; }
        public Rarity DifficultyRarity { get; }

        public Quest(string name, string desc, int gold, int xp, int kills,
                     int roundsLimit = 99, string? itemReward = null, Rarity diff = Rarity.Common)
        {
            Name = name; Description = desc; GoldReward = gold; XPReward = xp;
            KillsRequired = kills; RoundsLimit = roundsLimit;
            SpecialRewardItem = itemReward; DifficultyRarity = diff;
        }

        public bool IsComplete() => CurrentKills >= KillsRequired;

        public float Progress() => KillsRequired > 0 ? (float)CurrentKills / KillsRequired : 1f;

        public string ProgressBar(int width = 15)
        {
            int filled = (int)(Progress() * width);
            filled = Math.Clamp(filled, 0, width);
            return "[" + new string('█', filled) + new string('░', width - filled) + "]";
        }

        public void ShowInfo()
        {
            ConsoleColor col = DifficultyRarity switch
            {
                Rarity.Rare => ConsoleColor.Cyan,
                Rarity.Epic => ConsoleColor.Blue,
                Rarity.Legendary => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
            Console.ForegroundColor = col;
            Console.WriteLine($"  📜 {Name} [{DifficultyRarity}]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"     {Description}");
            Console.WriteLine($"     Progreso: {ProgressBar()} {CurrentKills}/{KillsRequired} eliminaciones");
            Console.WriteLine($"     Recompensa: {GoldReward}🪙  {XPReward} XP  {(SpecialRewardItem != null ? "+ " + SpecialRewardItem : "")}");
            if (RoundsLimit < 99) Console.WriteLine($"     ⏰ Límite: {RoundsLimit} rondas");
            Console.ResetColor();
        }
    }

    static class QuestDatabase
    {
        public static List<Quest> GetStarterQuests() => new()
        {
            new Quest("Primera Sangre",    "Elimina 3 enemigos en combate.",           50,  100, 3),
            new Quest("Exterminador",      "Elimina 10 enemigos en total.",           120,  200, 10),
            new Quest("Cazador de Jefes",  "Derrota tu primer jefe.",                 200,  500, 1,   itemReward: "Hacha Rúnica", diff: Rarity.Rare),
            new Quest("Acumulador",        "Gana 200 de oro en una sola partida.",    100,  150, 0),
            new Quest("Superviviente",     "Llega a la ronda 10 sin morir.",          300,  600, 0,  10, diff: Rarity.Rare),
            new Quest("Masacre",           "Elimina 25 enemigos.",                    250,  400, 25,  diff: Rarity.Uncommon),
            new Quest("Carnicero",         "Elimina 50 enemigos.",                    500,  800, 50,  diff: Rarity.Rare),
            new Quest("Leyenda Viva",      "Elimina 100 enemigos.",                   1000, 2000, 100, diff: Rarity.Legendary),
            new Quest("Caza Mayor",        "Derrota 3 jefes de horda.",               400,  700, 3,   diff: Rarity.Epic),
            new Quest("Apocalipsis",       "Derrota un Dragón Ancestral.",           1500, 3000, 1,   itemReward: "Escama Ancestral", diff: Rarity.Legendary),
        };
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE CRAFTEO
    // ═══════════════════════════════════════════════

    class CraftingRecipe
    {
        public string ResultItemName { get; }
        public CraftingCategory Category { get; }
        public Dictionary<string, int> Ingredients { get; }
        public int GoldCost { get; }
        public int LevelRequired { get; }
        public Rarity ResultRarity { get; }
        public string Description { get; }

        public CraftingRecipe(string result, CraftingCategory cat,
                              Dictionary<string, int> ingredients, int gold,
                              int level, Rarity rarity, string desc)
        {
            ResultItemName = result; Category = cat; Ingredients = ingredients;
            GoldCost = gold; LevelRequired = level; ResultRarity = rarity; Description = desc;
        }

        public bool CanCraft(Player player)
        {
            if (player.Level < LevelRequired) return false;
            if (player.Gold < GoldCost) return false;
            foreach (var ing in Ingredients)
            {
                int count = player.Inventory.Count(i => i.Name == ing.Key);
                if (count < ing.Value) return false;
            }
            return true;
        }

        public string GetIngredientsList() =>
            string.Join(", ", Ingredients.Select(kv => $"{kv.Value}x {kv.Key}"));
    }

    static class CraftingDatabase
    {
        public static List<CraftingRecipe> AllRecipes => new()
        {
            new CraftingRecipe("Gran Poción", CraftingCategory.Potions,
                new() { ["Poción Menor"] = 3 }, 5, 1, Rarity.Uncommon,
                "Restaura 100 HP"),

            new CraftingRecipe("Elixir Supremo", CraftingCategory.Potions,
                new() { ["Elixir"] = 2, ["Hierba Mágica"] = 1 }, 30, 5, Rarity.Rare,
                "Restaura 150 HP y 80 MP"),

            new CraftingRecipe("Poción de Poder", CraftingCategory.Potions,
                new() { ["Éter"] = 2, ["Cristal de Magia"] = 1 }, 25, 3, Rarity.Rare,
                "+15 ATK durante 3 rondas"),

            new CraftingRecipe("Espada Rúnica Avanzada", CraftingCategory.Weapons,
                new() { ["Hacha Rúnica"] = 1, ["Cristal de Fuego"] = 2, ["Metal Élfico"] = 1 }, 150, 10, Rarity.Epic,
                "+35 ATK, elemento Fuego"),

            new CraftingRecipe("Armadura Divina", CraftingCategory.Armor,
                new() { ["Armadura de Hierro"] = 1, ["Fragmento Sagrado"] = 3, ["Metal Élfico"] = 2 }, 200, 12, Rarity.Epic,
                "+30 DEF, +40 MaxHP, Resistencia Sagrada"),

            new CraftingRecipe("Amuleto del Dragón", CraftingCategory.Accessories,
                new() { ["Escama Ancestral"] = 1, ["Gema del Abismo"] = 2 }, 300, 15, Rarity.Legendary,
                "+50 MaxHP, +10 ATK, Resist. Fuego"),

            new CraftingRecipe("Pergamino Épico", CraftingCategory.Scrolls,
                new() { ["Pergamino de Fuego"] = 2, ["Tinta Arcana"] = 1 }, 40, 4, Rarity.Rare,
                "Hechizo masivo de fuego a todos los enemigos"),

            new CraftingRecipe("Antídoto Superior", CraftingCategory.Potions,
                new() { ["Hierba Mágica"] = 2 }, 10, 1, Rarity.Common,
                "Cura todos los estados negativos"),

            new CraftingRecipe("Explosivo Alquímico", CraftingCategory.Potions,
                new() { ["Cristal de Fuego"] = 1, ["Polvo Volcánico"] = 2 }, 20, 3, Rarity.Uncommon,
                "Inflige 80 daño a todos los enemigos"),

            new CraftingRecipe("Cetro del Maná", CraftingCategory.Weapons,
                new() { ["Cristal de Magia"] = 3, ["Madera Antigua"] = 1 }, 120, 8, Rarity.Rare,
                "+20 ATK, +30 Maná máximo"),

            new CraftingRecipe("Botas del Viento", CraftingCategory.Accessories,
                new() { ["Pluma de Grifo"] = 2, ["Cuero Élfico"] = 1 }, 80, 5, Rarity.Rare,
                "+15 Velocidad, +10% Esquiva"),

            new CraftingRecipe("Corona Oscura", CraftingCategory.Accessories,
                new() { ["Gema del Abismo"] = 1, ["Hueso de Liche"] = 2, ["Fragmento de Sombra"] = 1 }, 250, 14, Rarity.Legendary,
                "+25 ATK Oscuro, Drena 5 HP por turno"),

            new CraftingRecipe("Escudo Sagrado Supremo", CraftingCategory.Armor,
                new() { ["Fragmento Sagrado"] = 4, ["Oro Puro"] = 2 }, 280, 13, Rarity.Legendary,
                "+40 DEF, Refleja 15% del daño"),

            new CraftingRecipe("Ración de Viaje", CraftingCategory.Food,
                new() { ["Carne Cruda"] = 2, ["Hierbas"] = 1 }, 5, 1, Rarity.Common,
                "+30 HP, regenera 5 HP/turno por 3 turnos"),

            new CraftingRecipe("Festín de Campeón", CraftingCategory.Food,
                new() { ["Carne Cruda"] = 4, ["Especias Raras"] = 2, ["Trufa Mágica"] = 1 }, 50, 7, Rarity.Rare,
                "+80 HP, +20 MP, +5 ATK por 5 turnos"),
        };

        public static Item? CraftItem(CraftingRecipe recipe, Player player)
        {
            if (!recipe.CanCraft(player)) return null;

            foreach (var ing in recipe.Ingredients)
            {
                int remaining = ing.Value;
                var toRemove = player.Inventory.Where(i => i.Name == ing.Key).Take(remaining).ToList();
                foreach (var item in toRemove)
                    player.Inventory.Remove(item);
            }

            player.Gold -= recipe.GoldCost;
            GlobalStats.TotalGoldSpent += recipe.GoldCost;
            GlobalStats.TotalItemsCrafted++;

            return recipe.ResultItemName switch
            {
                "Gran Poción" => new Item("Gran Poción", "Restaura 100 HP", ItemType.Potion, Rarity.Uncommon, 40, hp: 100),
                "Elixir Supremo" => new Item("Elixir Supremo", "Restaura 150 HP y 80 MP", ItemType.Potion, Rarity.Rare, 80, hp: 150, mp: 80),
                "Poción de Poder" => new Item("Poción de Poder", "+15 ATK temporal", ItemType.Potion, Rarity.Rare, 60, atk: 15),
                "Espada Rúnica Avanzada" => new Item("Espada Rúnica Avanzada", "+35 ATK de Fuego", ItemType.Weapon, Rarity.Epic, 300, atk: 35),
                "Armadura Divina" => new Item("Armadura Divina", "+30 DEF +40 MaxHP", ItemType.Armor, Rarity.Epic, 350, def: 30, maxHp: 40),
                "Amuleto del Dragón" => new Item("Amuleto del Dragón", "+50 MaxHP +10 ATK", ItemType.Accessory, Rarity.Legendary, 500, atk: 10, maxHp: 50),
                "Antídoto Superior" => new Item("Antídoto Superior", "Cura estados negativos", ItemType.Potion, Rarity.Common, 15),
                "Explosivo Alquímico" => new Item("Explosivo Alquímico", "80 daño a todos", ItemType.Potion, Rarity.Uncommon, 30),
                "Cetro del Maná" => new Item("Cetro del Maná", "+20 ATK +30 MP Max", ItemType.Weapon, Rarity.Rare, 140, atk: 20, mp: 30),
                "Botas del Viento" => new Item("Botas del Viento", "+15 VEL", ItemType.Accessory, Rarity.Rare, 100, spd: 15),
                "Corona Oscura" => new Item("Corona Oscura", "+25 ATK Oscuro", ItemType.Accessory, Rarity.Legendary, 400, atk: 25),
                "Escudo Sagrado Supremo" => new Item("Escudo Sagrado Supremo", "+40 DEF", ItemType.Armor, Rarity.Legendary, 450, def: 40),
                "Ración de Viaje" => new Item("Ración de Viaje", "+30 HP + Regen", ItemType.Food, Rarity.Common, 15, hp: 30),
                "Festín de Campeón" => new Item("Festín de Campeón", "+80 HP +20 MP +5 ATK", ItemType.Food, Rarity.Rare, 80, hp: 80, mp: 20, atk: 5),
                "Pergamino Épico" => new Item("Pergamino Épico", "Fuego masivo", ItemType.Scroll, Rarity.Rare, 70),
                _ => new Item(recipe.ResultItemName, recipe.Description, ItemType.Accessory, recipe.ResultRarity, 100)
            };
        }
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE ÁRBOL DE TALENTOS
    // ═══════════════════════════════════════════════

    class TalentNode
    {
        public string Name { get; }
        public string Description { get; }
        public TalentTree Tree { get; }
        public int Cost { get; }
        public int Tier { get; }
        public bool Unlocked { get; set; }
        public List<string> Prerequisites { get; }

        // Efectos del talento
        public int AtkBonus { get; }
        public int DefBonus { get; }
        public int HPBonus { get; }
        public int MPBonus { get; }
        public int CritBonus { get; }
        public int SpdBonus { get; }
        public float DamageMultiplier { get; }

        public TalentNode(string name, string desc, TalentTree tree, int cost, int tier,
                          int atk = 0, int def = 0, int hp = 0, int mp = 0,
                          int crit = 0, int spd = 0, float dmgMult = 1.0f,
                          List<string>? prereqs = null)
        {
            Name = name; Description = desc; Tree = tree; Cost = cost; Tier = tier;
            AtkBonus = atk; DefBonus = def; HPBonus = hp; MPBonus = mp;
            CritBonus = crit; SpdBonus = spd; DamageMultiplier = dmgMult;
            Prerequisites = prereqs ?? new List<string>();
        }
    }

    class TalentSystem
    {
        public int AvailablePoints { get; set; }
        private List<TalentNode> _nodes;
        private List<string> _unlockedNames = new();

        public TalentSystem()
        {
            _nodes = BuildTalentTree();
        }

        private List<TalentNode> BuildTalentTree() => new()
        {
            // ── ÁRBOL DE COMBATE ──
            new TalentNode("Fuerza Bruta", "+8 ATK base", TalentTree.Combat, 1, 1, atk: 8),
            new TalentNode("Golpe Preciso", "+10% crítico", TalentTree.Combat, 1, 1, crit: 10),
            new TalentNode("Armadura Natural", "+8 DEF", TalentTree.Combat, 1, 1, def: 8),
            new TalentNode("Constitución", "+30 HP máx", TalentTree.Combat, 2, 2, hp: 30,
                prereqs: new(){"Fuerza Bruta"}),
            new TalentNode("Golpe Devastador", "x1.3 daño especial", TalentTree.Combat, 2, 2, dmgMult: 1.3f,
                prereqs: new(){"Golpe Preciso"}),
            new TalentNode("Escudo Vivo", "+15 DEF +20 HP", TalentTree.Combat, 3, 3, def: 15, hp: 20,
                prereqs: new(){"Armadura Natural", "Constitución"}),
            new TalentNode("Masacre", "x1.5 daño en crítico", TalentTree.Combat, 4, 4, crit: 5, dmgMult: 1.5f,
                prereqs: new(){"Golpe Devastador"}),
            new TalentNode("Guerrero Eterno", "+50 HP +10 DEF +10 ATK", TalentTree.Combat, 5, 5, hp: 50, def: 10, atk: 10,
                prereqs: new(){"Escudo Vivo", "Masacre"}),

            // ── ÁRBOL DE MAGIA ──
            new TalentNode("Canal Arcano", "+20 MP máx", TalentTree.Magic, 1, 1, mp: 20),
            new TalentNode("Potencia Mágica", "+12 ATK mágico", TalentTree.Magic, 1, 1, atk: 12),
            new TalentNode("Mente Aguda", "-2 coste de habilidades", TalentTree.Magic, 2, 2, mp: 10,
                prereqs: new(){"Canal Arcano"}),
            new TalentNode("Hechizo Amplificado", "x1.4 daño mágico", TalentTree.Magic, 3, 3, dmgMult: 1.4f,
                prereqs: new(){"Potencia Mágica", "Mente Aguda"}),
            new TalentNode("Maestro Arcano", "+30 MP +20 ATK +x1.2 dmg", TalentTree.Magic, 5, 5,
                mp: 30, atk: 20, dmgMult: 1.2f,
                prereqs: new(){"Hechizo Amplificado"}),

            // ── ÁRBOL DE APOYO ──
            new TalentNode("Empatía", "Curas +20% efectivas", TalentTree.Support, 1, 1, hp: 15),
            new TalentNode("Aura Protectora", "Aliados -10% daño recibido", TalentTree.Support, 2, 2,
                prereqs: new(){"Empatía"}),
            new TalentNode("Mente Grupal", "+5 ATK a todos al inicio", TalentTree.Support, 3, 3,
                prereqs: new(){"Aura Protectora"}),
            new TalentNode("Resiliencia", "Revivir con 30% HP (1/batalla)", TalentTree.Support, 5, 5,
                prereqs: new(){"Mente Grupal"}),

            // ── ÁRBOL DE SIGILO ──
            new TalentNode("Paso Silencioso", "+10 Velocidad", TalentTree.Stealth, 1, 1, spd: 10),
            new TalentNode("Ojo de Halcón", "+15% crítico", TalentTree.Stealth, 1, 1, crit: 15),
            new TalentNode("Sombra Fugaz", "50% esquivar 1 ataque/ronda", TalentTree.Stealth, 3, 3,
                prereqs: new(){"Paso Silencioso"}),
            new TalentNode("Asesino en las Sombras", "x2.5 daño desde sigilo", TalentTree.Stealth, 5, 5,
                dmgMult: 2.5f, prereqs: new(){"Sombra Fugaz", "Ojo de Halcón"}),

            // ── ÁRBOL DE NATURALEZA ──
            new TalentNode("Piel de Árbol", "+12 DEF natural", TalentTree.Nature, 1, 1, def: 12),
            new TalentNode("Regeneración", "Recupera 8 HP/turno", TalentTree.Nature, 2, 2, hp: 20,
                prereqs: new(){"Piel de Árbol"}),
            new TalentNode("Veneno Potenciado", "Veneno hace 15 daño/turno", TalentTree.Nature, 3, 3,
                prereqs: new(){"Regeneración"}),
            new TalentNode("Espíritu del Bosque", "+25 HP regen +20 DEF", TalentTree.Nature, 5, 5,
                def: 20, hp: 30, prereqs: new(){"Veneno Potenciado"}),
        };

        public void ShowTalentMenu(Player player)
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Program.PrintBox($"🌟 ÁRBOL DE TALENTOS — {player.Name} (Puntos: {AvailablePoints})", ConsoleColor.Yellow);
                Console.WriteLine();

                var trees = Enum.GetValues<TalentTree>();
                foreach (var tree in trees)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n  ── {tree} ──");
                    Console.ResetColor();

                    var treeNodes = _nodes.Where(n => n.Tree == tree).OrderBy(n => n.Tier).ToList();
                    foreach (var node in treeNodes)
                    {
                        bool canUnlock = AvailablePoints >= node.Cost &&
                                         !node.Unlocked &&
                                         node.Prerequisites.All(p => _unlockedNames.Contains(p));

                        ConsoleColor col = node.Unlocked ? ConsoleColor.Green :
                                           canUnlock ? ConsoleColor.Yellow : ConsoleColor.DarkGray;

                        Console.ForegroundColor = col;
                        string status = node.Unlocked ? "✔" : canUnlock ? "○" : "✗";
                        Console.WriteLine($"  {status} T{node.Tier} [{node.Cost}pts] {node.Name}: {node.Description}");
                        if (node.Prerequisites.Any() && !node.Unlocked)
                            Console.WriteLine($"       Requiere: {string.Join(", ", node.Prerequisites)}");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("\n  Escribe el nombre exacto del talento para desbloquearlo, o '0' para salir:");
                Console.Write("  → ");
                string input = Console.ReadLine() ?? "0";

                if (input == "0") { open = false; break; }

                var chosen = _nodes.FirstOrDefault(n =>
                    string.Equals(n.Name, input, StringComparison.OrdinalIgnoreCase));

                if (chosen == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ✘ Talento no encontrado.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else if (chosen.Unlocked)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  ✘ Ya desbloqueaste ese talento.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else if (!chosen.Prerequisites.All(p => _unlockedNames.Contains(p)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✘ Requieres: {string.Join(", ", chosen.Prerequisites)}");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else if (AvailablePoints < chosen.Cost)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✘ Necesitas {chosen.Cost} puntos, tienes {AvailablePoints}.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else
                {
                    chosen.Unlocked = true;
                    _unlockedNames.Add(chosen.Name);
                    AvailablePoints -= chosen.Cost;

                    // Aplicar bonus al jugador
                    player.AttackBonus += chosen.AtkBonus;
                    player.Defense += chosen.DefBonus;
                    player.MaxHealth += chosen.HPBonus;
                    player.Health = Math.Min(player.Health + chosen.HPBonus, player.MaxHealth);
                    player.Mana = Math.Min(player.Mana + chosen.MPBonus, 200);
                    player.CritChance += chosen.CritBonus;
                    player.Speed += chosen.SpdBonus;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ ¡Talento «{chosen.Name}» desbloqueado!");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }
        }

        public List<string> GetUnlockedTalents() => _unlockedNames.ToList();
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE MAZMORRAS
    // ═══════════════════════════════════════════════

    class DungeonRoom
    {
        public string Name { get; }
        public string Description { get; }
        public int EnemyCount { get; }
        public bool HasTreasure { get; }
        public bool HasTrap { get; }
        public bool HasShop { get; }
        public bool IsBossRoom { get; }
        public int TrapDamage { get; }

        static readonly Random R = new Random();

        public DungeonRoom(string name, string desc, int enemies, bool treasure, bool trap,
                           bool shop, bool boss, int trapDmg = 0)
        {
            Name = name; Description = desc; EnemyCount = enemies;
            HasTreasure = treasure; HasTrap = trap; HasShop = shop;
            IsBossRoom = boss; TrapDamage = trapDmg;
        }

        public static DungeonRoom GenerateRandom(int floor, DungeonType type)
        {
            string[] names = {
                "Sala Oscura", "Cámara del Eco", "Pasillo Maldito", "Cripta Antigua",
                "Salón del Trono", "Cámara de Tortura", "Biblioteca Secreta",
                "Altar Profano", "Piscina de Sangre", "Galería de los Muertos"
            };

            string name = names[R.Next(names.Length)];
            int enemies = 1 + floor / 2 + R.Next(3);
            bool treasure = R.Next(100) < 35;
            bool trap = R.Next(100) < 25;
            bool shop = R.Next(100) < 20;
            int trapDmg = 10 + floor * 5 + R.Next(20);

            return new DungeonRoom(name, GetRoomDesc(type), enemies, treasure, trap, shop, false, trapDmg);
        }

        private static string GetRoomDesc(DungeonType t) => t switch
        {
            DungeonType.Cave => "Las paredes rezuman humedad. Extraños hongos brillan en la oscuridad.",
            DungeonType.Ruins => "Antiguos frescos adornan paredes derruidas. El olor a polvo es sofocante.",
            DungeonType.Volcano => "El calor es insoportable. La lava fluye por grietas en el suelo.",
            DungeonType.IcePeak => "Estalactitas de hielo cuelgan del techo. El frío penetra hasta los huesos.",
            DungeonType.ShadowRealm => "Las sombras se mueven solas. Una presencia oscura acecha en los rincones.",
            DungeonType.HolyTemple => "Luz sagrada ilumina el lugar. Estatuas de antiguos dioses vigilan.",
            DungeonType.Abyss => "El abismo se abre bajo tus pies. El vacío llama desde las profundidades.",
            _ => "Una habitación ordinaria... o eso parece."
        };
    }

    class Dungeon
    {
        public string Name { get; }
        public DungeonType Type { get; }
        public int Floors { get; }
        public int CurrentFloor { get; private set; } = 1;
        public List<DungeonRoom> Rooms { get; }
        public bool IsCompleted { get; private set; }
        public int GoldReward { get; }
        public int XPReward { get; }
        public Rarity Difficulty { get; }

        static readonly Random R = new Random();

        public Dungeon(string name, DungeonType type, int floors, int gold, int xp, Rarity diff)
        {
            Name = name; Type = type; Floors = floors; GoldReward = gold;
            XPReward = xp; Difficulty = diff;
            Rooms = new List<DungeonRoom>();
            GenerateRooms();
        }

        private void GenerateRooms()
        {
            for (int f = 1; f <= Floors; f++)
            {
                int roomCount = 3 + R.Next(3);
                for (int r = 0; r < roomCount; r++)
                    Rooms.Add(DungeonRoom.GenerateRandom(f, Type));
            }
        }

        public static List<Dungeon> GetAvailableDungeons(int round) => new()
        {
            new Dungeon("Cueva de los Murmullos",  DungeonType.Cave,       3, 150, 300,  Rarity.Common),
            new Dungeon("Ruinas del Imperio Caído", DungeonType.Ruins,      4, 250, 500,  Rarity.Uncommon),
            new Dungeon("Pico del Volcán Eterno",   DungeonType.Volcano,    5, 400, 800,  Rarity.Rare),
            new Dungeon("Cima de Hielo Eterno",     DungeonType.IcePeak,    5, 400, 800,  Rarity.Rare),
            new Dungeon("Reino de las Sombras",     DungeonType.ShadowRealm,6, 600, 1200, Rarity.Epic),
            new Dungeon("Templo del Dios Solar",    DungeonType.HolyTemple, 6, 600, 1200, Rarity.Epic),
            new Dungeon("El Abismo Sin Fondo",      DungeonType.Abyss,      8, 1000, 2000, Rarity.Legendary),
        };

        public void Explore(List<Player> players)
        {
            Console.Clear();
            Program.PrintBox($"🗺 MAZMORRA: {Name.ToUpper()}", ConsoleColor.Magenta);
            Console.WriteLine($"\n  Tipo: {Type}  |  Pisos: {Floors}  |  Dificultad: {Difficulty}");
            Console.WriteLine($"  Recompensa: {GoldReward}🪙  {XPReward} XP");
            Console.WriteLine("\n  Pulsa una tecla para explorar...");
            Console.ReadKey(true);

            foreach (var room in Rooms)
            {
                if (!players.Any(p => p.IsAlive())) break;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ─── {room.Name} ───");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"  {room.Description}");
                Console.ResetColor();
                Console.ReadKey(true);

                // Trampa
                if (room.HasTrap)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  ⚠  ¡TRAMPA! El suelo cede bajo tus pies...");
                    foreach (var p in players.Where(p => p.IsAlive()))
                    {
                        p.TakeDamage(room.TrapDamage, ElementType.Physical);
                        Console.WriteLine($"  {p.Name} recibe {room.TrapDamage} daño de la trampa!");
                    }
                    Console.ResetColor();
                    Console.ReadKey(true);
                }

                // Tienda de mazmorra
                if (room.HasShop)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n  🏪 ¡Encontraste un mercader ambulante!");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    foreach (var p in players.Where(p => p.IsAlive()))
                        Program.StaticShop(p);
                }

                // Enemigos
                if (room.EnemyCount > 0 && !room.IsBossRoom)
                {
                    var enemies = GenerateDungeonEnemies(room.EnemyCount);
                    Program.StaticBattle(enemies, players);
                }

                // Tesoro
                if (room.HasTreasure && players.Any(p => p.IsAlive()))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  💰 ¡Encontraste un cofre del tesoro!");
                    Console.ResetColor();
                    int bonusGold = R.Next(30, 100);
                    foreach (var p in players.Where(p => p.IsAlive()))
                    {
                        p.Gold += bonusGold / players.Count(x => x.IsAlive());
                        var drop = ItemDatabase.GetRandomDrop(5);
                        if (p.Inventory.Count < GameConfig.MAX_INVENTORY)
                        {
                            p.Inventory.Add(drop);
                            Console.WriteLine($"  {p.Name} obtiene [{drop.Rarity}] {drop.Name}!");
                        }
                    }
                    Console.ReadKey(true);
                }
            }

            if (players.Any(p => p.IsAlive()))
            {
                IsCompleted = true;
                GlobalStats.TotalDungeonsCleared++;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Program.PrintBox($"✔ MAZMORRA COMPLETADA: {Name}", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  Recompensa: {GoldReward}🪙  {XPReward} XP");
                Console.ResetColor();
                foreach (var p in players.Where(x => x.IsAlive()))
                {
                    p.Gold += GoldReward / players.Count(x => x.IsAlive());
                    p.GainXP(XPReward / players.Count(x => x.IsAlive()));
                }
                Console.ReadKey(true);
            }
        }

        private List<Enemy> GenerateDungeonEnemies(int count)
        {
            var enemies = new List<Enemy>();
            var types = GameConfig.EnemyTypes;
            for (int i = 0; i < count; i++)
            {
                string name = types[R.Next(types.Length)] + " " + (char)('A' + i);
                int hp = 60 + R.Next(40);
                int atk = 12 + R.Next(8);
                enemies.Add(R.Next(100) < 20 ? new EliteEnemy(name + " ★", hp + 30, atk + 8) : new Enemy(name, hp, atk));
            }
            return enemies;
        }
    }

    // ═══════════════════════════════════════════════
    //  ITEM (EXPANDIDO)
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
        public int MaxMPBonus { get; }
        public int SpdBonus { get; }
        public int CritBonus { get; }
        public StatusEffect? GrantsStatus { get; }
        public ElementType? GrantsElement { get; }
        public bool IsConsumable => Type == ItemType.Potion || Type == ItemType.Scroll || Type == ItemType.Food;

        public Item(string name, string desc, ItemType type, Rarity rarity, int price,
                    int hp = 0, int mp = 0, int atk = 0, int def = 0, int maxHp = 0,
                    int maxMp = 0, int spd = 0, int crit = 0,
                    StatusEffect? status = null, ElementType? element = null)
        {
            Name = name; Description = desc; Type = type; Rarity = rarity; Price = price;
            HPRestore = hp; MPRestore = mp; AtkBonus = atk; DefBonus = def;
            MaxHPBonus = maxHp; MaxMPBonus = maxMp; SpdBonus = spd; CritBonus = crit;
            GrantsStatus = status; GrantsElement = element;
        }

        public ConsoleColor GetRarityColor() => GameConfig.RarityColors[(int)Rarity];
        public string GetRarityStars() => GameConfig.RarityStars[(int)Rarity];
    }

    static class ItemDatabase
    {
        static readonly Random R = new Random();

        static readonly List<Item> AllDrops = new()
        {
            // Pociones
            new Item("Poción Menor",        "Restaura 30 HP",           ItemType.Potion,     Rarity.Common,    10,  hp: 30),
            new Item("Poción Mayor",        "Restaura 70 HP",           ItemType.Potion,     Rarity.Uncommon,  25,  hp: 70),
            new Item("Poción Superior",     "Restaura 120 HP",          ItemType.Potion,     Rarity.Rare,      50,  hp: 120),
            new Item("Éter",                "Restaura 40 MP",           ItemType.Potion,     Rarity.Uncommon,  20,  mp: 40),
            new Item("Éter Mayor",          "Restaura 80 MP",           ItemType.Potion,     Rarity.Rare,      40,  mp: 80),
            new Item("Elixir",              "Restaura 80 HP y 40 MP",   ItemType.Potion,     Rarity.Rare,      50,  hp: 80, mp: 40),
            new Item("Elixir Supremo",      "Restaura 150 HP y 80 MP",  ItemType.Potion,     Rarity.Epic,      120, hp: 150, mp: 80),
            new Item("Antídoto",            "Cura estados negativos",   ItemType.Potion,     Rarity.Common,    15),
            new Item("Poción de Berserker", "Entra en modo Berserk",    ItemType.Potion,     Rarity.Rare,      60,  status: StatusEffect.Berserked),
            new Item("Poción de Agilidad",  "+15 VEL por 3 turnos",     ItemType.Potion,     Rarity.Uncommon,  30,  spd: 15),

            // Armas
            new Item("Daga de Hierro",      "+5 ATK",                   ItemType.Weapon,     Rarity.Common,    30,  atk: 5),
            new Item("Espada de Acero",     "+12 ATK",                  ItemType.Weapon,     Rarity.Uncommon,  60,  atk: 12),
            new Item("Hacha Rúnica",        "+20 ATK",                  ItemType.Weapon,     Rarity.Rare,      120, atk: 20),
            new Item("Lanza del Dragón",    "+25 ATK de Fuego",         ItemType.Weapon,     Rarity.Epic,      200, atk: 25, element: ElementType.Fire),
            new Item("Espada Legendaria",   "+30 ATK +20 MaxHP",        ItemType.Weapon,     Rarity.Legendary, 250, atk: 30, maxHp: 20),
            new Item("Arco de Viento",      "+18 ATK +10 CRT",          ItemType.Weapon,     Rarity.Rare,      130, atk: 18, crit: 10),
            new Item("Cetro del Abismo",    "+22 ATK Oscuro",           ItemType.Weapon,     Rarity.Epic,      180, atk: 22, element: ElementType.Dark),
            new Item("Espadón Rúnico",      "+35 ATK",                  ItemType.Weapon,     Rarity.Epic,      220, atk: 35),
            new Item("Espada del Destino",  "+40 ATK +25 CRT",          ItemType.Weapon,     Rarity.Legendary, 500, atk: 40, crit: 25),
            new Item("Arma Mítica",         "+50 ATK +30 MaxHP",        ItemType.Weapon,     Rarity.Mythic,    999, atk: 50, maxHp: 30),

            // Armaduras
            new Item("Escudo de Bronce",    "+5 DEF",                   ItemType.Armor,      Rarity.Common,    30,  def: 5),
            new Item("Armadura de Hierro",  "+12 DEF",                  ItemType.Armor,      Rarity.Uncommon,  70,  def: 12),
            new Item("Cota de Malla",       "+18 DEF",                  ItemType.Armor,      Rarity.Rare,      110, def: 18),
            new Item("Cota Épica",          "+20 DEF +20 MaxHP",        ItemType.Armor,      Rarity.Epic,      160, def: 20, maxHp: 20),
            new Item("Armadura del Dragón", "+30 DEF +30 MaxHP",        ItemType.Armor,      Rarity.Legendary, 350, def: 30, maxHp: 30),
            new Item("Peto Celestial",      "+40 DEF +50 MaxHP",        ItemType.Armor,      Rarity.Mythic,    999, def: 40, maxHp: 50),

            // Accesorios
            new Item("Amuleto de Vida",     "+30 MaxHP",                ItemType.Accessory,  Rarity.Rare,      90,  maxHp: 30),
            new Item("Anillo de Maná",      "+30 MaxMP",                ItemType.Accessory,  Rarity.Rare,      85,  maxMp: 30),
            new Item("Botas Veloces",       "+10 VEL",                  ItemType.Accessory,  Rarity.Uncommon,  55,  spd: 10),
            new Item("Guantes del Asesino", "+15 CRT",                  ItemType.Accessory,  Rarity.Rare,      100, crit: 15),
            new Item("Diadema Arcana",      "+40 MaxMP +10 ATK",        ItemType.Accessory,  Rarity.Epic,      170, maxMp: 40, atk: 10),
            new Item("Corazón de Dragón",   "+60 MaxHP +15 DEF",        ItemType.Accessory,  Rarity.Legendary, 450, maxHp: 60, def: 15),
            new Item("Ojo del Abismo",      "+20 ATK +20 CRT",          ItemType.Accessory,  Rarity.Legendary, 400, atk: 20, crit: 20),
            new Item("Reliquia Ancestral",  "+30 todo",                 ItemType.Accessory,  Rarity.Mythic,    999, atk: 20, def: 15, maxHp: 50, crit: 10),

            // Pergaminos
            new Item("Pergamino de Fuego",  "Daño fuego a todos",       ItemType.Scroll,     Rarity.Uncommon,  35),
            new Item("Pergamino de Hielo",  "Congela a los enemigos",   ItemType.Scroll,     Rarity.Uncommon,  35),
            new Item("Pergamino de Rayo",   "Rayo en cadena",           ItemType.Scroll,     Rarity.Rare,      55),
            new Item("Pergamino Épico",     "Hechizo devastador",       ItemType.Scroll,     Rarity.Epic,      120),
            new Item("Pergamino de Cura",   "Cura a todo el grupo",     ItemType.Scroll,     Rarity.Rare,      60,  hp: 60),

            // Materiales (para crafteo)
            new Item("Hierba Mágica",       "Material de alquimia",     ItemType.Material,   Rarity.Common,    5),
            new Item("Cristal de Fuego",    "Material de encantamiento",ItemType.Material,   Rarity.Uncommon,  20),
            new Item("Cristal de Magia",    "Material arcano",          ItemType.Material,   Rarity.Uncommon,  25),
            new Item("Metal Élfico",        "Material raro",            ItemType.Material,   Rarity.Rare,      50),
            new Item("Fragmento Sagrado",   "Material divino",          ItemType.Material,   Rarity.Rare,      45),
            new Item("Gema del Abismo",     "Material oscuro",          ItemType.Material,   Rarity.Epic,      100),
            new Item("Escama Ancestral",    "Trofeo de dragón",         ItemType.Material,   Rarity.Legendary, 200),
            new Item("Hueso de Liche",      "Material maldito",         ItemType.Material,   Rarity.Rare,      60),
            new Item("Tinta Arcana",        "Para pergaminos avanzados",ItemType.Material,   Rarity.Uncommon,  30),
            new Item("Madera Antigua",      "De un árbol milenario",    ItemType.Material,   Rarity.Uncommon,  20),
            new Item("Pluma de Grifo",      "Ligera como el viento",    ItemType.Material,   Rarity.Rare,      40),
            new Item("Cuero Élfico",        "Resistente y flexible",    ItemType.Material,   Rarity.Rare,      35),
            new Item("Polvo Volcánico",     "Altamente inflamable",     ItemType.Material,   Rarity.Common,    10),
            new Item("Oro Puro",            "Oro refinado al máximo",   ItemType.Material,   Rarity.Epic,      150),
            new Item("Fragmento de Sombra", "Oscuridad cristalizada",   ItemType.Material,   Rarity.Rare,      55),
            new Item("Carne Cruda",         "Para cocinar",             ItemType.Material,   Rarity.Common,    3),
            new Item("Hierbas",             "Para pociones básicas",    ItemType.Material,   Rarity.Common,    3),
            new Item("Especias Raras",      "De tierras lejanas",       ItemType.Material,   Rarity.Uncommon,  20),
            new Item("Trufa Mágica",        "Hongo encantado",          ItemType.Material,   Rarity.Rare,      45),

            // Comida
            new Item("Ración de Campaña",   "+25 HP",                   ItemType.Food,       Rarity.Common,    8,  hp: 25),
            new Item("Pan de Maná",         "+30 MP",                   ItemType.Food,       Rarity.Common,    8,  mp: 30),
            new Item("Estofado del Héroe",  "+60 HP +20 MP",            ItemType.Food,       Rarity.Uncommon,  30, hp: 60, mp: 20),
            new Item("Festín de Campeón",   "+80 HP +20 MP +5 ATK",     ItemType.Food,       Rarity.Rare,      70, hp: 80, mp: 20, atk: 5),
        };

        public static Item GetRandomDrop(int round)
        {
            var pool = AllDrops.Where(i =>
                (round >= 15 || i.Rarity != Rarity.Mythic) &&
                (round >= 10 || i.Rarity != Rarity.Legendary) &&
                (round >= 5 || (i.Rarity != Rarity.Epic && i.Rarity != Rarity.Legendary)) &&
                i.Type != ItemType.Material
            ).ToList();

            // Más probabilidad de materiales en rondas avanzadas
            if (round >= 3 && R.Next(100) < 20)
                pool.AddRange(AllDrops.Where(i => i.Type == ItemType.Material));

            return pool[R.Next(pool.Count)];
        }

        public static List<Item> GetAllItems() => AllDrops;
    }

    static class ShopDatabase
    {
        static readonly Random R = new Random();

        public static List<Item> GetShopItems(int round)
        {
            var shop = new List<Item>
            {
                new Item("Poción Menor",    "Restaura 30 HP",        ItemType.Potion,   Rarity.Common,   10, hp: 30),
                new Item("Poción Mayor",    "Restaura 70 HP",        ItemType.Potion,   Rarity.Uncommon, 25, hp: 70),
                new Item("Éter",            "Restaura 40 MP",        ItemType.Potion,   Rarity.Uncommon, 20, mp: 40),
                new Item("Elixir",          "+80 HP / +40 MP",       ItemType.Potion,   Rarity.Rare,     50, hp: 80, mp: 40),
                new Item("Antídoto",        "Cura estados negativos",ItemType.Potion,   Rarity.Common,   15),
                new Item("Daga de Hierro",  "+5 ATK",                ItemType.Weapon,   Rarity.Common,   30, atk: 5),
                new Item("Espada de Acero", "+12 ATK",               ItemType.Weapon,   Rarity.Uncommon, 60, atk: 12),
                new Item("Escudo de Bronce","+5 DEF",                ItemType.Armor,    Rarity.Common,   30, def: 5),
                new Item("Armadura de Hierro","+12 DEF",             ItemType.Armor,    Rarity.Uncommon, 70, def: 12),
                new Item("Amuleto de Vida", "+30 MaxHP",             ItemType.Accessory,Rarity.Rare,     90, maxHp: 30),
                new Item("Botas Veloces",   "+10 VEL",               ItemType.Accessory,Rarity.Uncommon, 55, spd: 10),
                new Item("Hierba Mágica",   "Material alquimia",     ItemType.Material, Rarity.Common,    5),
                new Item("Cristal de Magia","Material arcano",       ItemType.Material, Rarity.Uncommon, 25),
                new Item("Ración de Campaña","+25 HP",               ItemType.Food,     Rarity.Common,    8, hp: 25),
            };

            if (round >= 3)
            {
                shop.Add(new Item("Poción Superior", "Restaura 120 HP", ItemType.Potion, Rarity.Rare, 50, hp: 120));
                shop.Add(new Item("Éter Mayor", "Restaura 80 MP", ItemType.Potion, Rarity.Rare, 40, mp: 80));
                shop.Add(new Item("Cristal de Fuego", "Material encantamiento", ItemType.Material, Rarity.Uncommon, 20));
                shop.Add(new Item("Pergamino de Fuego", "Daño fuego a todos", ItemType.Scroll, Rarity.Uncommon, 35));
            }

            if (round >= 5)
            {
                shop.Add(new Item("Hacha Rúnica", "+20 ATK", ItemType.Weapon, Rarity.Rare, 120, atk: 20));
                shop.Add(new Item("Cota de Malla", "+18 DEF", ItemType.Armor, Rarity.Rare, 110, def: 18));
                shop.Add(new Item("Guantes del Asesino", "+15 CRT", ItemType.Accessory, Rarity.Rare, 100, crit: 15));
                shop.Add(new Item("Metal Élfico", "Material raro", ItemType.Material, Rarity.Rare, 50));
                shop.Add(new Item("Fragmento Sagrado", "Material divino", ItemType.Material, Rarity.Rare, 45));
            }

            if (round >= 8)
            {
                shop.Add(new Item("Cota Épica", "+20 DEF +20 HP", ItemType.Armor, Rarity.Epic, 160, def: 20, maxHp: 20));
                shop.Add(new Item("Lanza del Dragón", "+25 ATK de Fuego", ItemType.Weapon, Rarity.Epic, 200, atk: 25));
                shop.Add(new Item("Diadema Arcana", "+40 MaxMP +10 ATK", ItemType.Accessory, Rarity.Epic, 170, maxMp: 40, atk: 10));
                shop.Add(new Item("Elixir Supremo", "+150 HP +80 MP", ItemType.Potion, Rarity.Epic, 120, hp: 150, mp: 80));
            }

            if (round >= 12)
            {
                shop.Add(new Item("Espada Legendaria", "+30 ATK +20 HP", ItemType.Weapon, Rarity.Legendary, 250, atk: 30, maxHp: 20));
                shop.Add(new Item("Armadura del Dragón", "+30 DEF +30 MaxHP", ItemType.Armor, Rarity.Legendary, 350, def: 30, maxHp: 30));
                shop.Add(new Item("Corazón de Dragón", "+60 MaxHP +15 DEF", ItemType.Accessory, Rarity.Legendary, 450, maxHp: 60, def: 15));
                shop.Add(new Item("Escama Ancestral", "Material legendario", ItemType.Material, Rarity.Legendary, 200));
            }

            if (round >= 18)
            {
                shop.Add(new Item("Arma Mítica", "+50 ATK +30 MaxHP", ItemType.Weapon, Rarity.Mythic, 999, atk: 50, maxHp: 30));
                shop.Add(new Item("Peto Celestial", "+40 DEF +50 MaxHP", ItemType.Armor, Rarity.Mythic, 999, def: 40, maxHp: 50));
                shop.Add(new Item("Reliquia Ancestral", "+30 todo", ItemType.Accessory, Rarity.Mythic, 999, atk: 20, def: 15, maxHp: 50, crit: 10));
            }

            // Aleatorizar orden y tomar subset
            return shop.OrderBy(_ => R.Next()).Take(Math.Min(shop.Count, 12)).ToList();
        }
    }

    // ═══════════════════════════════════════════════
    //  CLASE BASE CHARACTER (EXPANDIDA)
    // ═══════════════════════════════════════════════

    abstract class Character
    {
        static readonly Random R = new Random();

        public string Name { get; protected set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public bool Defending { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        private readonly Dictionary<StatusEffect, int> _statusDurations = new();
        private readonly Dictionary<StatusEffect, int> _statusStacks = new();

        protected Character(string name, int hp, int mp, int def = 0, int spd = 10)
        {
            Name = name; MaxHealth = hp; Health = hp; Mana = mp; Defense = def; Speed = spd;
        }

        public bool IsAlive() => Health > 0;

        public virtual void TakeDamage(int dmg, ElementType element = ElementType.Physical)
        {
            if (Defending) dmg = (int)(dmg * GameConfig.DEFEND_REDUCTION);
            dmg = Math.Max(1, dmg - Defense / 5);
            Health = Math.Max(0, Health - dmg);
            GlobalStats.TotalDamageReceived += dmg;
        }

        public void Heal(int amount)
        {
            int actual = Math.Min(MaxHealth - Health, amount);
            Health = Math.Min(MaxHealth, Health + amount);
            GlobalStats.TotalHealingDone += actual;
        }

        public void ApplyStatus(StatusEffect effect, int turns, int stacks = 1)
        {
            if (_statusDurations.ContainsKey(effect))
            {
                _statusDurations[effect] = Math.Max(_statusDurations[effect], turns);
                _statusStacks[effect] = Math.Min((_statusStacks.GetValueOrDefault(effect, 1) + stacks), 5);
            }
            else
            {
                _statusDurations[effect] = turns;
                _statusStacks[effect] = stacks;
            }
        }

        public bool HasStatus(StatusEffect effect) =>
            _statusDurations.ContainsKey(effect) && _statusDurations[effect] > 0;

        public bool HasAnyStatus() => _statusDurations.Any(kv => kv.Value > 0);

        public string GetStatusString() =>
            string.Join(",", _statusDurations
                .Where(kv => kv.Value > 0)
                .Select(kv => kv.Key.ToString()[..Math.Min(3, kv.Key.ToString().Length)]));

        public void TickStatus()
        {
            var keys = _statusDurations.Keys.ToList();
            foreach (var k in keys)
            {
                if (_statusDurations[k] <= 0) continue;
                int stack = _statusStacks.GetValueOrDefault(k, 1);

                switch (k)
                {
                    case StatusEffect.Poisoned:
                        int poisonDmg = 8 * stack;
                        Health = Math.Max(1, Health - poisonDmg);
                        break;
                    case StatusEffect.Burned:
                        int burnDmg = 6 * stack;
                        Health = Math.Max(1, Health - burnDmg);
                        break;
                    case StatusEffect.Bleeding:
                        int bleedDmg = 10 * stack;
                        Health = Math.Max(1, Health - bleedDmg);
                        break;
                    case StatusEffect.Frozen:
                        // Congelado ya no puede atacar (manejado en lógica de turno)
                        break;
                    case StatusEffect.Regenerating:
                        Heal(15 * stack);
                        break;
                    case StatusEffect.Doomed:
                        // Al expirar, muere
                        if (_statusDurations[k] == 1) Health = 0;
                        break;
                }

                _statusDurations[k]--;
            }
        }

        public void ClearStatus() => _statusDurations.Clear();

        public void ClearNegativeStatus()
        {
            var negative = new[] {
                StatusEffect.Poisoned, StatusEffect.Burned, StatusEffect.Stunned,
                StatusEffect.Bleeding, StatusEffect.Frozen, StatusEffect.Cursed,
                StatusEffect.Silenced, StatusEffect.Weakened, StatusEffect.Confused,
                StatusEffect.Petrified, StatusEffect.Doomed
            };
            foreach (var s in negative)
                if (_statusDurations.ContainsKey(s))
                    _statusDurations[s] = 0;
        }

        public int GetStatusCount() => _statusDurations.Count(kv => kv.Value > 0);
    }

    // ═══════════════════════════════════════════════
    //  CLASE BASE PLAYER (EXPANDIDA)
    // ═══════════════════════════════════════════════

    abstract class Player : Character
    {
        public int Gold { get; set; }
        public int XP { get; set; }
        public int Level { get; private set; } = 1;
        public int AttackBonus { get; set; }
        public int BaseAttack { get; protected set; }
        public int CritChance { get; set; } = 10;
        public int SpecialCost { get; protected set; } = 10;
        public int RoundsWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDamageDealt { get; private set; }
        public int TalentPoints { get; set; }
        public bool HasUsedRevive { get; set; } = false;

        public string ClassName { get; protected set; } = "Aventurero";
        public string SpecialName { get; protected set; } = "Especial";
        public string Special2Name { get; protected set; } = "Habilidad 2";
        public ElementType AttackElement { get; protected set; } = ElementType.Physical;

        public List<Item> Inventory { get; } = new List<Item>();
        public TalentSystem Talents { get; } = new TalentSystem();
        public List<Quest> ActiveQuests { get; } = new List<Quest>();
        public int SpecialCost2 { get; protected set; } = 25;

        // Equipamiento
        public Item? EquippedWeapon { get; private set; }
        public Item? EquippedArmor { get; private set; }
        public Item? EquippedAccessory { get; private set; }

        // Resistencias elementales (en %)
        public Dictionary<ElementType, int> ElementResistances { get; } = new();

        // Mascota/compañero
        public Companion? Pet { get; set; }

        static readonly Random R = new Random();

        protected Player(string name, int hp, int mp, int baseAtk, int def = 0, int spd = 10)
            : base(name, hp, mp, def, spd)
        {
            BaseAttack = baseAtk;
        }

        public void Tick()
        {
            int mpRegen = 4 + (Level / 5);
            Mana = Math.Min(Mana + mpRegen, 200);
            Defending = false;
            TickStatus();
            Pet?.Tick(this);
        }

        public int Attack()
        {
            int weatherAtk = WorldTime.GetWeatherEffects().atkMod;
            int total = BaseAttack + AttackBonus + R.Next(-3, 4) + weatherAtk;
            return Math.Max(1, total);
        }

        public override void TakeDamage(int dmg, ElementType element = ElementType.Physical)
        {
            // Aplicar resistencia elemental
            if (ElementResistances.TryGetValue(element, out int resist))
                dmg = (int)(dmg * (1 - resist / 100f));

            if (HasStatus(StatusEffect.Blessed)) dmg = (int)(dmg * GameConfig.BLESSED_REDUCTION);
            if (HasStatus(StatusEffect.Shielded)) dmg = Math.Max(0, dmg - 15);
            if (HasStatus(StatusEffect.Weakened)) dmg = (int)(dmg * 1.2f);

            base.TakeDamage(dmg, element);
        }

        public void GainXP(int xp)
        {
            XP += xp;
            while (XP >= Level * 50 && Level < GameConfig.MAX_LEVEL)
            {
                XP -= Level * 50;
                Level++;
                MaxHealth += 15;
                Health = MaxHealth;
                Mana = Math.Min(Mana + 10, 200);
                BaseAttack += 3;
                Defense += 1;
                Speed += 1;
                TalentPoints += 1;
                Talents.AvailablePoints++;

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  🎉 ¡{Name} subió a Nivel {Level}! HP:{MaxHealth} ATK:{BaseAttack + AttackBonus} +1 Punto de Talento");
                Console.ResetColor();

                OnLevelUp();
            }
        }

        protected virtual void OnLevelUp() { }

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
                Console.WriteLine($"  🎒 INVENTARIO DE {Name.ToUpper()}  (Oro: {Gold}🪙  |  {Inventory.Count}/{GameConfig.MAX_INVENTORY})");
                Console.ResetColor();

                if (Inventory.Count == 0)
                {
                    Console.WriteLine("\n  (Sin objetos)");
                    Console.WriteLine("\n  Pulsa una tecla para volver...");
                    Console.ReadKey(true);
                    return;
                }

                // Agrupar por tipo
                var groups = Inventory.GroupBy(i => i.Type).OrderBy(g => g.Key);
                int globalIdx = 1;
                var indexedItems = new Dictionary<int, Item>();

                foreach (var group in groups)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"\n  ─── {group.Key} ───");
                    Console.ResetColor();
                    foreach (var item in group)
                    {
                        Console.ForegroundColor = item.GetRarityColor();
                        Console.WriteLine($"  {globalIdx,2}. {item.GetRarityStars()} [{item.Rarity}] {item.Name,-28} — {item.Description}");
                        Console.ResetColor();
                        indexedItems[globalIdx] = item;
                        globalIdx++;
                    }
                }

                Console.WriteLine("\n  0. Volver  |  E. Equipar/Desequipar  |  D. Descartar objeto");
                Console.Write("  Usar objeto #: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") { open = false; continue; }

                if (op.ToUpper() == "E")
                {
                    EquipmentMenu();
                    continue;
                }

                if (op.ToUpper() == "D")
                {
                    Console.Write("  ¿Qué objeto descartar? #: ");
                    string dOp = Console.ReadLine() ?? "";
                    if (int.TryParse(dOp, out int dIdx) && indexedItems.ContainsKey(dIdx))
                    {
                        var discard = indexedItems[dIdx];
                        int sellPrice = discard.Price / 4;
                        Inventory.Remove(discard);
                        Gold += sellPrice;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Descartaste {discard.Name}. Recuperaste {sellPrice}🪙.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    continue;
                }

                if (int.TryParse(op, out int idx) && indexedItems.ContainsKey(idx))
                {
                    UseItem(indexedItems[idx]);
                    open = false;
                }
            }
        }

        public void EquipmentMenu()
        {
            Console.Clear();
            Program.PrintBox("⚔ EQUIPAMIENTO", ConsoleColor.Yellow);
            Console.WriteLine($"\n  Arma:      {(EquippedWeapon != null ? $"[{EquippedWeapon.Rarity}] {EquippedWeapon.Name}" : "Sin equipar")}");
            Console.WriteLine($"  Armadura:  {(EquippedArmor != null ? $"[{EquippedArmor.Rarity}] {EquippedArmor.Name}" : "Sin equipar")}");
            Console.WriteLine($"  Accesorio: {(EquippedAccessory != null ? $"[{EquippedAccessory.Rarity}] {EquippedAccessory.Name}" : "Sin equipar")}");

            Console.WriteLine("\n  ─── Equipable del inventario ───");
            var equipable = Inventory.Where(i =>
                i.Type == ItemType.Weapon || i.Type == ItemType.Armor || i.Type == ItemType.Accessory).ToList();

            if (!equipable.Any())
            {
                Console.WriteLine("  (No tienes objetos equipables)");
                Console.ReadKey(true);
                return;
            }

            for (int i = 0; i < equipable.Count; i++)
            {
                Console.ForegroundColor = equipable[i].GetRarityColor();
                Console.WriteLine($"  {i + 1}. [{equipable[i].Rarity}] {equipable[i].Name} — {equipable[i].Description}");
                Console.ResetColor();
            }

            Console.Write("\n  Equipar #: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= equipable.Count)
            {
                EquipItem(equipable[choice - 1]);
            }
        }

        private void EquipItem(Item item)
        {
            // Desequipar el anterior si hay
            if (item.Type == ItemType.Weapon && EquippedWeapon != null)
            {
                UnequipItem(EquippedWeapon);
            }
            else if (item.Type == ItemType.Armor && EquippedArmor != null)
            {
                UnequipItem(EquippedArmor);
            }
            else if (item.Type == ItemType.Accessory && EquippedAccessory != null)
            {
                UnequipItem(EquippedAccessory);
            }

            // Aplicar stats
            AttackBonus += item.AtkBonus;
            Defense += item.DefBonus;
            MaxHealth += item.MaxHPBonus;
            Health = Math.Min(Health + item.MaxHPBonus, MaxHealth);
            CritChance += item.CritBonus;
            Speed += item.SpdBonus;

            if (item.GrantsElement.HasValue) AttackElement = item.GrantsElement.Value;

            // Asignar slot
            if (item.Type == ItemType.Weapon) EquippedWeapon = item;
            else if (item.Type == ItemType.Armor) EquippedArmor = item;
            else if (item.Type == ItemType.Accessory) EquippedAccessory = item;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ Equipaste: {item.Name}");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private void UnequipItem(Item item)
        {
            AttackBonus -= item.AtkBonus;
            Defense -= item.DefBonus;
            MaxHealth -= item.MaxHPBonus;
            Health = Math.Min(Health, MaxHealth);
            CritChance -= item.CritBonus;
            Speed -= item.SpdBonus;

            if (item.Type == ItemType.Weapon) EquippedWeapon = null;
            else if (item.Type == ItemType.Armor) EquippedArmor = null;
            else if (item.Type == ItemType.Accessory) EquippedAccessory = null;
        }

        public void UseItem(Item item)
        {
            GlobalStats.TotalItemsUsed++;
            bool consumed = false;

            if (item.HPRestore > 0) { Heal(item.HPRestore); Console.WriteLine($"  ✔ Recuperas {item.HPRestore} HP."); consumed = item.IsConsumable; }
            if (item.MPRestore > 0) { Mana = Math.Min(Mana + item.MPRestore, 200); Console.WriteLine($"  ✔ Recuperas {item.MPRestore} MP."); consumed = item.IsConsumable; }
            if (item.AtkBonus > 0 && !consumed) { AttackBonus += item.AtkBonus; Console.WriteLine($"  ✔ ATK +{item.AtkBonus} (permanente)."); }
            if (item.DefBonus > 0 && !consumed) { Defense += item.DefBonus; Console.WriteLine($"  ✔ DEF +{item.DefBonus} (permanente)."); }
            if (item.MaxHPBonus > 0 && !consumed) { MaxHealth += item.MaxHPBonus; Health = Math.Min(Health + item.MaxHPBonus, MaxHealth); Console.WriteLine($"  ✔ MaxHP +{item.MaxHPBonus} (permanente)."); }
            if (item.MaxMPBonus > 0 && !consumed) { Mana = Math.Min(Mana + item.MaxMPBonus, 200); Console.WriteLine($"  ✔ MaxMP +{item.MaxMPBonus} (permanente)."); }
            if (item.CritBonus > 0 && !consumed) { CritChance += item.CritBonus; Console.WriteLine($"  ✔ Crítico +{item.CritBonus}%."); }
            if (item.SpdBonus > 0 && !consumed) { Speed += item.SpdBonus; Console.WriteLine($"  ✔ Velocidad +{item.SpdBonus}."); }
            if (item.GrantsStatus.HasValue) { ApplyStatus(item.GrantsStatus.Value, 3); Console.WriteLine($"  ✔ Ganas estado: {item.GrantsStatus.Value}."); consumed = item.IsConsumable; }

            // Efectos especiales de pergaminos
            if (item.Type == ItemType.Scroll)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  📜 Pergamino usado: {item.Name}");
                Console.ResetColor();
                consumed = true;
            }

            // Antídoto
            if (item.Name.Contains("Antídoto"))
            {
                ClearNegativeStatus();
                Console.WriteLine("  ✔ Todos los estados negativos curados.");
                consumed = true;
            }

            if (consumed) Inventory.Remove(item);
            Console.ReadKey(true);
        }

        public void OpenCrafting()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Program.PrintBox($"⚗ TALLER DE CRAFTEO — {Name}", ConsoleColor.Green);
                Console.WriteLine($"  Oro: {Gold}🪙  |  Nivel: {Level}\n");

                var recipes = CraftingDatabase.AllRecipes;
                for (int i = 0; i < recipes.Count; i++)
                {
                    var r = recipes[i];
                    bool canCraft = r.CanCraft(this);
                    Console.ForegroundColor = canCraft ? ConsoleColor.Green : ConsoleColor.DarkGray;
                    Console.WriteLine($"  {i + 1,2}. [{r.ResultRarity}] {r.ResultItemName,-30} Coste: {r.GoldCost}🪙  Nivel: {r.LevelRequired}");
                    Console.WriteLine($"      Ingredientes: {r.GetIngredientsList()}");
                    Console.WriteLine($"      {r.Description}");
                    Console.ResetColor();
                }

                Console.WriteLine("\n  0. Salir");
                Console.Write("  Fabricar #: ");
                string op = Console.ReadLine() ?? "0";
                if (op == "0") { open = false; break; }

                if (int.TryParse(op, out int idx) && idx >= 1 && idx <= recipes.Count)
                {
                    var recipe = recipes[idx - 1];
                    if (!recipe.CanCraft(this))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  ✘ No puedes fabricar este objeto. Revisa nivel, oro e ingredientes.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    else
                    {
                        var result = CraftingDatabase.CraftItem(recipe, this);
                        if (result != null && Inventory.Count < GameConfig.MAX_INVENTORY)
                        {
                            Inventory.Add(result);
                            Console.ForegroundColor = result.GetRarityColor();
                            Console.WriteLine($"\n  ✔ ¡Fabricaste: [{result.Rarity}] {result.Name}!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  ✘ Inventario lleno o error al fabricar.");
                            Console.ResetColor();
                        }
                        Console.ReadKey(true);
                    }
                }
            }
        }

        public void ShowQuests()
        {
            Console.Clear();
            Program.PrintBox($"📜 MISIONES DE {Name.ToUpper()}", ConsoleColor.Yellow);

            if (!ActiveQuests.Any())
            {
                Console.WriteLine("\n  Sin misiones activas. Visita el tablón de misiones.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine();
            foreach (var q in ActiveQuests)
            {
                q.ShowInfo();
                Console.WriteLine();
            }

            Console.ReadKey(true);
        }

        public void UpdateQuestProgress(int kills)
        {
            foreach (var q in ActiveQuests.Where(x => x.Status == QuestStatus.Active))
            {
                q.CurrentKills += kills;
                if (q.IsComplete())
                {
                    q.Status = QuestStatus.Completed;
                    Gold += q.GoldReward;
                    GainXP(q.XPReward);
                    GlobalStats.TotalQuestsCompleted++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  📜 ¡MISIÓN COMPLETADA: {q.Name}! +{q.GoldReward}🪙 +{q.XPReward} XP");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }
        }

        public void AdoptCompanion()
        {
            if (Pet != null)
            {
                Console.WriteLine($"\n  Ya tienes a {Pet.Name} como compañero.");
                Console.ReadKey(true);
                return;
            }

            var companions = Companion.GetAvailable();
            Console.Clear();
            Program.PrintBox("🐾 ADOPTAR COMPAÑERO", ConsoleColor.Cyan);
            Console.WriteLine();
            for (int i = 0; i < companions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  {i + 1}. {companions[i].Icon} {companions[i].Name}: {companions[i].Description}  (Coste: {companions[i].Cost}🪙)");
                Console.ResetColor();
            }
            Console.WriteLine("  0. Cancelar");
            Console.Write("  → ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= companions.Count)
            {
                var chosen = companions[idx - 1];
                if (Gold >= chosen.Cost)
                {
                    Gold -= chosen.Cost;
                    Pet = chosen;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ ¡{chosen.Name} se une a ti como compañero!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ✘ Oro insuficiente.");
                    Console.ResetColor();
                }
                Console.ReadKey(true);
            }
        }

        public virtual void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            Console.WriteLine($"\n  {Name} no tiene habilidad extra disponible.");
        }

        public virtual void Special2(List<Player> allies, List<Enemy> enemies)
        {
            Console.WriteLine($"\n  {Name} no tiene segunda habilidad disponible.");
        }

        public abstract void Special(List<Enemy> enemies);
    }

    // ═══════════════════════════════════════════════
    //  SISTEMA DE COMPAÑEROS/MASCOTAS
    // ═══════════════════════════════════════════════

    class Companion
    {
        public string Name { get; }
        public string Icon { get; }
        public string Description { get; }
        public int Cost { get; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; }

        private readonly Func<Player, List<Enemy>, string> _tickAction;
        static readonly Random R = new Random();

        public Companion(string name, string icon, string desc, int cost, Func<Player, List<Enemy>, string> action)
        {
            Name = name; Icon = icon; Description = desc; Cost = cost; _tickAction = action;
        }

        public void Tick(Player owner)
        {
            if (R.Next(100) < 30)
            {
                // La mascota actúa en combate (solo cuando hay enemigos activos)
                // El efecto se aplica a través del owner
            }
        }

        public string ActInCombat(Player owner, List<Enemy> enemies)
        {
            return _tickAction(owner, enemies);
        }

        public void GainExp(int exp)
        {
            Experience += exp;
            if (Experience >= Level * 30)
            {
                Experience -= Level * 30;
                Level++;
            }
        }

        public static List<Companion> GetAvailable() => new()
        {
            new Companion("Dragoncito", "🐉", "Lanza fuego aleatorio (+15 daño/turno a un enemigo)", 200,
                (owner, enemies) => {
                    if (!enemies.Any()) return "";
                    var target = enemies[R.Next(enemies.Count)];
                    int dmg = 15 + owner.Level;
                    target.TakeDamage(dmg, ElementType.Fire);
                    return $"  🐉 Dragoncito escupe fuego a {target.Name} por {dmg}!";
                }),

            new Companion("Zorrito Lunar", "🦊", "Regenera 8 HP al dueño por turno", 120,
                (owner, enemies) => {
                    owner.Heal(8);
                    return $"  🦊 Zorrito Lunar cura a {owner.Name} por 8 HP.";
                }),

            new Companion("Búho Arcano", "🦉", "Regenera 5 MP por turno al dueño", 100,
                (owner, enemies) => {
                    owner.Mana = Math.Min(owner.Mana + 5, 200);
                    return $"  🦉 Búho Arcano canaliza 5 MP a {owner.Name}.";
                }),

            new Companion("Lobo de Combate", "🐺", "25% chance de atacar junto al dueño (+20 daño)", 180,
                (owner, enemies) => {
                    if (!enemies.Any() || R.Next(100) >= 25) return "";
                    var target = enemies.OrderBy(e => e.Health).First();
                    int dmg = 20 + owner.Level * 2;
                    target.TakeDamage(dmg, ElementType.Physical);
                    return $"  🐺 Lobo de Combate ataca a {target.Name} por {dmg}!";
                }),

            new Companion("Hada Bendita", "🧚", "Aplica Bendición al dueño cada 3 turnos", 150,
                (owner, enemies) => {
                    owner.ApplyStatus(StatusEffect.Blessed, 2);
                    return $"  🧚 Hada Bendita bendice a {owner.Name}!";
                }),
        };
    }

    // ═══════════════════════════════════════════════
    //  ENEMIES (EXPANDIDO)
    // ═══════════════════════════════════════════════

    class Enemy : Character
    {
        protected int BaseAtk;
        public int GoldDrop { get; protected set; }
        public int XPDrop { get; protected set; }
        public ElementType WeakTo { get; protected set; } = ElementType.Physical;
        public ElementType ResistTo { get; protected set; } = ElementType.Physical;
        static readonly Random R = new Random();

        public Enemy(string name, int hp, int atk, int gold = 5, int xp = 10)
            : base(name, hp, 0)
        {
            BaseAtk = atk; GoldDrop = gold; XPDrop = xp;
        }

        public override void TakeDamage(int dmg, ElementType element = ElementType.Physical)
        {
            if (element == WeakTo) dmg = (int)(dmg * 1.5f);
            if (element == ResistTo) dmg = (int)(dmg * 0.5f);
            base.TakeDamage(dmg, element);
        }

        public virtual int Attack() => Math.Max(1, BaseAtk + R.Next(-3, 4));

        public void Tick() => TickStatus();

        public virtual string GetSpecialAttackName() => "Ataque";

        public virtual void UseSpecialAttack(Player target)
        {
            int dmg = (int)(Attack() * 1.5f);
            target.TakeDamage(dmg, ElementType.Physical);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"  ⚠  {Name} usa {GetSpecialAttackName()} en {target.Name} por {dmg}!");
            Console.ResetColor();
        }
    }

    class EliteEnemy : Enemy
    {
        static readonly Random R = new Random();

        public EliteEnemy(string name, int hp, int atk) : base(name, hp, atk, gold: 15, xp: 25)
        {
            Defense = 5;
            WeakTo = (ElementType)R.Next(Enum.GetValues<ElementType>().Length);
        }

        public override int Attack() => base.Attack() + 5;
        public override string GetSpecialAttackName() => "Ataque Élite";
    }

    class BossEnemy : Enemy
    {
        static readonly Random R = new Random();
        protected int phase = 1;
        bool phaseAnnounced = false;

        public BossEnemy(string name, int hp, int atk) : base(name, hp, atk, gold: 80, xp: 200)
        {
            Defense = 10;
        }

        public override int Attack()
        {
            if (Health < MaxHealth * 0.5f && phase == 1)
            {
                phase = 2;
                if (!phaseAnnounced)
                {
                    phaseAnnounced = true;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\n  ⚠  ¡{Name} entra en FASE 2! ¡ATAQUES MÁS PODEROSOS!");
                    Console.ResetColor();
                    BaseAtk = (int)(BaseAtk * 1.3f);
                    Defense += 5;
                    ApplyStatus(StatusEffect.Enraged, 10);
                }
            }
            return base.Attack() * phase + R.Next(0, 10);
        }

        public override string GetSpecialAttackName() => "Golpe Aplastante";
    }

    class LegendaryBoss : BossEnemy
    {
        static readonly Random R = new Random();
        int specialCooldown = 0;

        public LegendaryBoss(string name, int hp, int atk) : base(name, hp, atk)
        {
            Defense = 20;
            GoldDrop = 300;
            XPDrop = 800;
        }

        public override int Attack()
        {
            specialCooldown--;
            if (specialCooldown <= 0)
            {
                specialCooldown = 3;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  ⚡ ¡{Name} prepara un ATAQUE ESPECIAL!");
                Console.ResetColor();
                return base.Attack() + R.Next(20, 40);
            }
            return base.Attack() + R.Next(10, 25);
        }

        public override string GetSpecialAttackName() => "Rugido Ancestral";
    }

    class MythicBoss : LegendaryBoss
    {
        static readonly Random R = new Random();
        int turnCount = 0;

        public MythicBoss(string name, int hp, int atk) : base(name, hp, atk)
        {
            Defense = 30;
            GoldDrop = 600;
            XPDrop = 2000;
        }

        public override int Attack()
        {
            turnCount++;
            int baseDmg = base.Attack();

            // Cada 4 turnos hace un ataque especial devastador
            if (turnCount % 4 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  ☠  ¡{Name} libera el PODER DEL ABISMO!");
                Console.ResetColor();
                return baseDmg + R.Next(40, 70);
            }

            return baseDmg;
        }

        public override string GetSpecialAttackName() => "Furia del Abismo";
    }

    // ═══════════════════════════════════════════════
    //  CLASES DE JUGADOR (EXPANDIDAS)
    // ═══════════════════════════════════════════════

    // ── 1. GUERRERO ──────────────────────────────
    class Warrior : Player
    {
        int rageStacks = 0;
        int battleCry = 0;
        bool warMode = false;
        static readonly Random R = new Random();

        public Warrior(string n) : base(n, 200, 40, 22, def: 14, spd: 8)
        {
            ClassName = "Guerrero"; SpecialName = "Golpe Devastador";
            Special2Name = "Grito de Batalla"; SpecialCost = 15; SpecialCost2 = 20;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.Health).First();
            int baseDmg = (int)((BaseAttack + AttackBonus) * 2.5);
            int rageDmg = rageStacks * 8;
            int totalDmg = baseDmg + rageDmg;

            if (warMode) totalDmg = (int)(totalDmg * 1.4f);

            target.TakeDamage(totalDmg, ElementType.Physical);
            GlobalStats.TotalDamageDealt += totalDmg;

            if (totalDmg > GlobalStats.HighestDamageInOneTurn)
            {
                GlobalStats.HighestDamageInOneTurn = totalDmg;
                GlobalStats.HighestDamageDealer = Name;
            }

            int lifeSteal = totalDmg / 10;
            Heal(lifeSteal);

            rageStacks = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ⚔  {Name} usa GOLPE DEVASTADOR en {target.Name} por {totalDmg}! (Roba {lifeSteal} HP)");
            Console.ResetColor();

            // Chance de aturdir
            if (R.Next(100) < 20)
            {
                target.ApplyStatus(StatusEffect.Stunned, 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  ¡{target.Name} queda aturdido!");
                Console.ResetColor();
            }
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  ✘ Sin maná suficiente.");
                Console.ResetColor();
                return;
            }
            Mana -= SpecialCost2;
            battleCry = 3;
            warMode = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  📢 {Name} lanza el GRITO DE BATALLA!");
            foreach (var p in allies.Where(p => p.IsAlive()))
            {
                p.AttackBonus += 8;
                p.ApplyStatus(StatusEffect.Enraged, 3);
                Console.WriteLine($"  → {p.Name}: +8 ATK y Enraged por 3 turnos");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            rageStacks = Math.Min(rageStacks + 4, 20);
            ApplyStatus(StatusEffect.Enraged, 2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n  😤 {Name} acumula RABIA ({rageStacks} stacks). Modo de guerra: {(warMode ? "✔" : "✗")}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            MaxHealth += 10;
            Defense += 2;
        }
    }

    // ── 2. ARQUERO ──────────────────────────────
    class Archer : Player
    {
        static readonly Random R = new Random();
        bool multishot = false;
        int arrowCount = 5;
        int poisonArrows = 0;

        public Archer(string n) : base(n, 115, 65, 18, def: 5, spd: 16)
        {
            ClassName = "Arquero"; SpecialName = "Lluvia de Flechas";
            Special2Name = "Flecha Envenenada"; SpecialCost = 20; SpecialCost2 = 15; CritChance = 28;
            AttackElement = ElementType.Wind;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int bonusArrows = multishot ? 2 : 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  🏹 {Name} usa LLUVIA DE FLECHAS! ({enemies.Count + bonusArrows} flechas)");
            int totalDmg = 0;
            foreach (var e in enemies)
            {
                int hits = multishot ? 2 : 1;
                for (int h = 0; h < hits; h++)
                {
                    int dmg = (BaseAttack + AttackBonus) + R.Next(5, 20);
                    bool crit = R.Next(100) < CritChance;
                    if (crit) dmg = (int)(dmg * 1.8f);
                    e.TakeDamage(dmg, ElementType.Wind);
                    totalDmg += dmg;
                    Console.WriteLine($"     → {e.Name} recibe {dmg}{(crit ? " 💥CRÍTICO" : "")}");
                }

                if (poisonArrows > 0)
                {
                    e.ApplyStatus(StatusEffect.Poisoned, 3);
                    poisonArrows--;
                }
            }
            GlobalStats.TotalDamageDealt += totalDmg;
            Console.ResetColor();
            multishot = false;
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            poisonArrows = 3;

            var target = enemies.OrderBy(e => e.Health).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 1.5f) + R.Next(10, 25);
            target.TakeDamage(dmg, ElementType.Poison);
            target.ApplyStatus(StatusEffect.Poisoned, 4);
            target.ApplyStatus(StatusEffect.Bleeding, 2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  🏹 {Name} dispara FLECHA ENVENENADA a {target.Name} por {dmg}! (Veneno + Sangrado)");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            multishot = !multishot;
            arrowCount = Math.Min(arrowCount + 2, 10);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(multishot
                ? $"\n  🏹 {Name} activa DISPARO MÚLTIPLE: Lluvia de Flechas golpea 2 veces a cada enemigo."
                : $"\n  🏹 {Name} desactiva DISPARO MÚLTIPLE. Flechas: {arrowCount}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            CritChance += 2;
            Speed += 1;
        }
    }

    // ── 3. MAGO ──────────────────────────────
    class Mage : Player
    {
        static readonly Random R = new Random();
        int spellCharge = 0;
        ElementType currentElement = ElementType.Fire;
        bool overloadReady = false;

        public Mage(string n) : base(n, 88, 140, 12, def: 3, spd: 10)
        {
            ClassName = "Mago"; SpecialName = "Tormenta Arcana";
            Special2Name = "Sobrecargar"; SpecialCost = 25; SpecialCost2 = 30; CritChance = 18;
            AttackElement = ElementType.Fire;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int bonus = spellCharge * 10;
            float overloadMult = overloadReady ? 1.8f : 1.0f;
            spellCharge = 0;
            overloadReady = false;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  🔥 {Name} desata TORMENTA ARCANA [{currentElement}]! (Bonus: {bonus})");

            int totalDmg = 0;
            foreach (var e in enemies)
            {
                int dmg = (int)(((BaseAttack + AttackBonus) * 1.9f + bonus) * overloadMult) + R.Next(10, 30);
                bool crit = R.Next(100) < CritChance;
                if (crit) dmg = (int)(dmg * 1.8f);
                e.TakeDamage(dmg, currentElement);
                totalDmg += dmg;

                // Aplicar efecto elemental
                switch (currentElement)
                {
                    case ElementType.Fire: e.ApplyStatus(StatusEffect.Burned, 2); break;
                    case ElementType.Ice: e.ApplyStatus(StatusEffect.Frozen, 1); break;
                    case ElementType.Lightning: e.ApplyStatus(StatusEffect.Stunned, 1); break;
                    case ElementType.Poison: e.ApplyStatus(StatusEffect.Poisoned, 3); break;
                }

                Console.WriteLine($"     → {e.Name} recibe {dmg}{(crit ? " 💥CRÍTICO" : "")}");
            }

            GlobalStats.TotalDamageDealt += totalDmg;
            if (totalDmg > GlobalStats.HighestDamageInOneTurn)
            {
                GlobalStats.HighestDamageInOneTurn = totalDmg;
                GlobalStats.HighestDamageDealer = Name;
            }
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            overloadReady = true;
            spellCharge = Math.Min(spellCharge + 5, 10);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  ⚡ {Name} SOBRECARGA su próximo hechizo! (x1.8 daño + máxima carga)");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            spellCharge = Math.Min(spellCharge + 2, 10);

            // Cambiar elemento
            var elements = new[] { ElementType.Fire, ElementType.Ice, ElementType.Lightning, ElementType.Arcane, ElementType.Poison };
            currentElement = elements[R.Next(elements.Length)];
            AttackElement = currentElement;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  🔮 {Name} carga energía [{currentElement}]. Carga: {spellCharge}/10. Sobrecarga: {(overloadReady ? "Lista" : "No")}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Mana += 15;
            BaseAttack += 2;
        }
    }

    // ── 4. CURANDERO ──────────────────────────────
    class Healer : Player
    {
        static readonly Random R = new Random();
        int holyCharge = 0;
        bool resurrectionReady = false;

        public Healer(string n) : base(n, 125, 120, 9, def: 7, spd: 9)
        {
            ClassName = "Curandero"; SpecialName = "Luz Sagrada";
            Special2Name = "Resurrección"; SpecialCost = 20; SpecialCost2 = 50;
            AttackElement = ElementType.Holy;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int bonusHeal = holyCharge * 8;
            holyCharge = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✨ {Name} invoca LUZ SAGRADA! (Bonus: {bonusHeal})");

            foreach (var p in Program.Players.Where(p => p.IsAlive()))
            {
                int heal = 50 + R.Next(15) + bonusHeal;
                p.Heal(heal);
                p.ApplyStatus(StatusEffect.Regenerating, 2);
                Console.WriteLine($"  ✔ {p.Name} recupera {heal} HP y Regeneración.");
            }

            // Daño sagrado masivo al jefe o al más fuerte
            var boss = enemies.OrderByDescending(e => e.MaxHealth).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 1.5f) + bonusHeal / 2;
            boss.TakeDamage(dmg, ElementType.Holy);
            Console.WriteLine($"  ✝ Daño sagrado a {boss.Name} por {dmg}.");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }

            var dead = allies.FirstOrDefault(p => !p.IsAlive());
            if (dead == null)
            {
                Console.WriteLine("\n  ✘ No hay aliados caídos que revivir.");
                return;
            }

            Mana -= SpecialCost2;
            dead.Health = (int)(dead.MaxHealth * 0.4f);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  ✨ {Name} usa RESURRECCIÓN en {dead.Name}! ¡Revive con {dead.Health} HP!");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            holyCharge = Math.Min(holyCharge + 3, 15);
            var hurt = allies.Where(p => p.IsAlive() && p.HasAnyStatus()).FirstOrDefault();
            if (hurt != null)
            {
                hurt.ClearNegativeStatus();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🌟 {Name} purifica a {hurt.Name}. Carga sagrada: {holyCharge}");
            }
            else
            {
                int healAmt = 15 + Level * 2;
                foreach (var p in allies.Where(p => p.IsAlive()))
                    p.Heal(healAmt);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n  🌿 AURA SANADORA: Todos recuperan {healAmt} HP. Carga sagrada: {holyCharge}");
            }
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            MaxHealth += 5;
            Mana += 10;
        }
    }

    // ── 5. LADRÓN ──────────────────────────────
    class Thief : Player
    {
        static readonly Random R = new Random();
        bool stealthed = false;
        int comboKills = 0;
        int poisonBlades = 0;

        public Thief(string n) : base(n, 98, 75, 20, def: 4, spd: 22)
        {
            ClassName = "Ladrón"; SpecialName = "Golpe Bajo";
            Special2Name = "Hojas Venenosas"; SpecialCost = 12; SpecialCost2 = 18; CritChance = 32;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderBy(e => e.Health).First();

            float mult = stealthed ? 4.0f : 2.5f;
            if (comboKills > 0) mult += comboKills * 0.3f;

            int dmg = (int)((BaseAttack + AttackBonus) * mult);
            bool crit = R.Next(100) < CritChance + (stealthed ? 30 : 0);
            if (crit) dmg = (int)(dmg * 1.8f);

            target.TakeDamage(dmg, ElementType.Physical);
            GlobalStats.TotalDamageDealt += dmg;

            int stolen = R.Next(5, 20) + comboKills * 3;
            Gold += stolen;
            GlobalStats.TotalGoldEarned += stolen;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n  🗡  {Name} usa GOLPE BAJO ({(stealthed ? "SIGILO x4" : "x2.5")}) en {target.Name} por {dmg}{(crit ? " 💥CRÍTICO" : "")}! Roba {stolen}🪙");
            Console.ResetColor();

            if (!target.IsAlive()) comboKills++;
            stealthed = false;
            poisonBlades = Math.Max(0, poisonBlades - 1);

            if (poisonBlades > 0) target.ApplyStatus(StatusEffect.Poisoned, 3);
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            poisonBlades = 5;

            foreach (var e in enemies)
            {
                int dmg = (int)((BaseAttack + AttackBonus) * 0.8f) + R.Next(10, 20);
                e.TakeDamage(dmg, ElementType.Poison);
                e.ApplyStatus(StatusEffect.Poisoned, 4);
                e.ApplyStatus(StatusEffect.Bleeding, 2);
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n  🗡  {Name} lanza HOJAS VENENOSAS a todos! (Veneno + Sangrado 4 turnos)");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            stealthed = true;
            ApplyStatus(StatusEffect.Invisible, 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  👤 {Name} entra en SIGILO. Combo actual: {comboKills}. Próximo especial: x{4.0f + comboKills * 0.3f:F1} daño.");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            CritChance += 3;
            Speed += 2;
        }
    }

    // ── 6. MONJE ──────────────────────────────
    class Monk : Player
    {
        static readonly Random R = new Random();
        int comboCount = 0;
        bool chiActive = false;
        int meditationStacks = 0;

        public Monk(string n) : base(n, 135, 55, 19, def: 10, spd: 18)
        {
            ClassName = "Monje"; SpecialName = "Ráfaga de Golpes";
            Special2Name = "Chi Dragón"; SpecialCost = 15; SpecialCost2 = 25; CritChance = 20;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.Health).First();
            int hits = 3 + comboCount / 2 + (chiActive ? 3 : 0);
            int totalDmg = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  👊 {Name} usa RÁFAGA DE GOLPES ({hits} impactos){(chiActive ? " ¡CHI ACTIVADO!" : "")}:");

            for (int i = 0; i < hits; i++)
            {
                int dmg = (BaseAttack + AttackBonus) / 2 + R.Next(8, 22);
                bool crit = R.Next(100) < CritChance;
                if (crit) { dmg = (int)(dmg * 1.8f); GlobalStats.TotalCriticalHits++; }
                if (chiActive) dmg = (int)(dmg * 1.5f);
                target.TakeDamage(dmg, ElementType.Physical);
                totalDmg += dmg;
                Console.Write($" {dmg}{(crit ? "!" : "")}");
            }

            Console.WriteLine($"\n  Total: {totalDmg}");
            GlobalStats.TotalDamageDealt += totalDmg;
            Console.ResetColor();
            comboCount = Math.Min(comboCount + 1, 12);
            chiActive = false;
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            chiActive = true;
            meditationStacks += 2;

            // Contraataque automático esta ronda
            ApplyStatus(StatusEffect.Empowered, 2);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  🌀 {Name} concentra el CHI DEL DRAGÓN. Próxima Ráfaga: x1.5 + {3} golpes extra!");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            int hpReg = 25 + Level * 3 + meditationStacks * 5;
            int mpReg = 20 + meditationStacks * 3;
            Heal(hpReg);
            Mana = Math.Min(Mana + mpReg, 200);
            meditationStacks = Math.Min(meditationStacks + 1, 10);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  🧘 {Name} medita (Stack: {meditationStacks}). +{hpReg} HP, +{mpReg} MP. Combo: {comboCount}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Defense += 2;
            Speed += 1;
        }
    }

    // ── 7. BARDO ──────────────────────────────
    class Bard : Player
    {
        static readonly Random R = new Random();
        bool songActive = false;
        int melodyCharge = 0;
        string currentSong = "Ninguna";

        public Bard(string n) : base(n, 108, 95, 12, def: 5, spd: 14)
        {
            ClassName = "Bardo"; SpecialName = "Canción de Batalla";
            Special2Name = "Melodía del Caos"; SpecialCost = 20; SpecialCost2 = 30;
            AttackElement = ElementType.Wind;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            songActive = true;
            currentSong = "Canción de Batalla";
            int bonusAtk = 6 + melodyCharge * 2;
            int bonusHeal = 20 + melodyCharge * 5;
            melodyCharge = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  🎵 {Name} entona CANCIÓN DE BATALLA! (+{bonusAtk} ATK, +{bonusHeal} HP a todos)");

            foreach (var p in Program.Players.Where(p => p.IsAlive()))
            {
                p.AttackBonus += bonusAtk;
                p.Heal(bonusHeal);
                p.ApplyStatus(StatusEffect.Hasted, 2);
                Console.WriteLine($"     → {p.Name}: +{bonusAtk} ATK, +{bonusHeal} HP, Haste");
            }

            // AoE de viento + aturdimiento
            foreach (var e in enemies)
            {
                int dmg = (BaseAttack + AttackBonus) + R.Next(8, 20);
                e.TakeDamage(dmg, ElementType.Wind);
                if (R.Next(100) < 40) e.ApplyStatus(StatusEffect.Stunned, 1);
            }
            Console.WriteLine($"  🎵 Enemigos dañados por el sonido.");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            currentSong = "Melodía del Caos";

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n  🎶 {Name} toca la MELODÍA DEL CAOS!");

            // Efectos aleatorios en enemigos
            foreach (var e in enemies)
            {
                var effects = new[] { StatusEffect.Confused, StatusEffect.Weakened, StatusEffect.Cursed, StatusEffect.Bleeding };
                var effect = effects[R.Next(effects.Length)];
                e.ApplyStatus(effect, 3);
                int dmg = (int)((BaseAttack + AttackBonus) * 1.2f) + R.Next(5, 15);
                e.TakeDamage(dmg, ElementType.Wind);
                Console.WriteLine($"  → {e.Name}: {dmg} daño + {effect}");
            }

            // Buff aleatorio a aliados
            foreach (var p in allies.Where(p => p.IsAlive()))
            {
                var buffs = new[] { StatusEffect.Blessed, StatusEffect.Enraged, StatusEffect.Regenerating, StatusEffect.Hasted };
                p.ApplyStatus(buffs[R.Next(buffs.Length)], 3);
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            melodyCharge = Math.Min(melodyCharge + 2, 10);
            int xpBonus = 15 + melodyCharge * 2;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  🎶 {Name} toca la BALADA DE INSPIRACIÓN. +{xpBonus} XP a todos. Melodía: {melodyCharge}/10");
            foreach (var p in allies.Where(p => p.IsAlive()))
                p.GainXP(xpBonus);
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Mana += 8;
            Speed += 1;
        }
    }

    // ── 8. PALADÍN ──────────────────────────────
    class Paladin : Player
    {
        static readonly Random R = new Random();
        bool divineShield = false;
        int holyStacks = 0;
        bool consecrated = false;

        public Paladin(string n) : base(n, 165, 75, 20, def: 16, spd: 9)
        {
            ClassName = "Paladín"; SpecialName = "Juicio Divino";
            Special2Name = "Tierra Consagrada"; SpecialCost = 20; SpecialCost2 = 35;
            AttackElement = ElementType.Holy;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.MaxHealth).First();
            float mult = 2.8f + holyStacks * 0.3f;
            int dmg = (int)((BaseAttack + AttackBonus) * mult);
            target.TakeDamage(dmg, ElementType.Holy);
            target.ApplyStatus(StatusEffect.Cursed, 3);
            target.ApplyStatus(StatusEffect.Weakened, 2);

            int selfHeal = 30 + holyStacks * 5;
            Heal(selfHeal);
            holyStacks = 0;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  ✝  {Name} usa JUICIO DIVINO en {target.Name} por {dmg}! Recupera {selfHeal} HP.");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            consecrated = !consecrated;

            if (consecrated)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🌟 {Name} consagra el terreno! Todos los aliados reciben +10 HP/turno y +5 DEF.");
                foreach (var p in allies.Where(p => p.IsAlive()))
                {
                    p.ApplyStatus(StatusEffect.Regenerating, 5);
                    p.Defense += 5;
                }
            }
            else
            {
                foreach (var p in allies.Where(p => p.IsAlive())) p.Defense -= 5;
                Console.WriteLine($"\n  La consagración termina.");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            divineShield = !divineShield;
            holyStacks = Math.Min(holyStacks + 2, 10);

            if (divineShield)
            {
                foreach (var p in allies.Where(p => p.IsAlive()))
                {
                    p.ApplyStatus(StatusEffect.Blessed, 3);
                    p.ApplyStatus(StatusEffect.Shielded, 2);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🛡 {Name} activa ESCUDO DIVINO. Todos: Bendición + Escudo. Carga Sagrada: {holyStacks}");
            }
            else
            {
                Console.WriteLine($"\n  🛡 {Name} retira el Escudo Divino. Carga Sagrada: {holyStacks}");
            }
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Defense += 3;
            MaxHealth += 5;
        }
    }

    // ── 9. NIGROMANTE ──────────────────────────────
    class Necromancer : Player
    {
        static readonly Random R = new Random();
        int soulStacks = 0;
        List<UndeadMinion> minions = new();
        bool deathMarkActive = false;

        public Necromancer(string n) : base(n, 98, 130, 14, def: 3, spd: 10)
        {
            ClassName = "Nigromante"; SpecialName = "Drenaje de Almas";
            Special2Name = "Invocar No-Muerto"; SpecialCost = 22; SpecialCost2 = 30;
            AttackElement = ElementType.Dark;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int totalDrained = 0;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  🌑 {Name} usa DRENAJE DE ALMAS! (Almas acumuladas: {soulStacks})");

            foreach (var e in enemies)
            {
                int drain = (int)((BaseAttack + AttackBonus) * 1.3f) + soulStacks * 7;
                if (deathMarkActive) drain = (int)(drain * 1.5f);
                e.TakeDamage(drain, ElementType.Dark);
                totalDrained += drain / 3;
                Console.WriteLine($"     → Drena {drain} de {e.Name}.");

                if (!e.IsAlive() && minions.Count < 3)
                {
                    var minion = new UndeadMinion($"Zombi de {e.Name}", e.MaxHealth / 3, e.MaxHealth / 6);
                    minions.Add(minion);
                    Console.WriteLine($"     💀 ¡{Name} revive a {minion.Name}!");
                }
            }

            Heal(totalDrained);
            GlobalStats.TotalDamageDealt += enemies.Sum(e => (int)((BaseAttack + AttackBonus) * 1.3f) + soulStacks * 7);
            soulStacks = 0;
            deathMarkActive = false;

            Console.WriteLine($"  💜 {Name} absorbe {totalDrained} HP. Súbditos: {minions.Count}");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;

            if (minions.Count >= 3)
            {
                Console.WriteLine("\n  ✘ Límite de súbditos alcanzado (3).");
                return;
            }

            var minion = new UndeadMinion("Esqueleto Arcano", 60 + Level * 5, 8 + Level * 2);
            minions.Add(minion);
            deathMarkActive = true;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  💀 {Name} invoca a {minion.Name}! Súbditos activos: {minions.Count}/3");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            soulStacks = Math.Min(soulStacks + 4, 25);
            deathMarkActive = true;

            foreach (var e in enemies)
            {
                e.ApplyStatus(StatusEffect.Cursed, 2);
                e.ApplyStatus(StatusEffect.Weakened, 1);
            }

            // Minions atacan
            foreach (var m in minions.Where(m => m.IsAlive()))
            {
                if (enemies.Any())
                {
                    var target = enemies.OrderBy(e => e.Health).First();
                    int dmg = m.Attack();
                    target.TakeDamage(dmg, ElementType.Dark);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  💀 {m.Name} ataca a {target.Name} por {dmg}.");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"\n  💀 {Name} lanza MALDICIÓN GRUPAL. Almas: {soulStacks}. Marca de Muerte: activa.");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Mana += 12;
            BaseAttack += 1;
        }
    }

    class UndeadMinion
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; }
        int BaseAtk;
        static readonly Random R = new Random();

        public UndeadMinion(string name, int hp, int atk)
        {
            Name = name; Health = hp; MaxHealth = hp; BaseAtk = atk;
        }

        public bool IsAlive() => Health > 0;
        public int Attack() => BaseAtk + R.Next(-2, 5);
        public void TakeDamage(int dmg) => Health = Math.Max(0, Health - dmg);
    }

    // ── 10. ALQUIMISTA ──────────────────────────────
    class Alchemist : Player
    {
        static readonly Random R = new Random();
        int brewCount = 0;
        int reagentStacks = 0;
        bool turboMode = false;

        public Alchemist(string n) : base(n, 105, 105, 15, def: 6, spd: 13)
        {
            ClassName = "Alquimista"; SpecialName = "Bomba Alquímica";
            Special2Name = "Gran Explosión"; SpecialCost = 18; SpecialCost2 = 35;
            AttackElement = ElementType.Poison;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            brewCount++;
            float mult = turboMode ? 2.5f : 1.6f;
            int splashDmg = (int)((BaseAttack + AttackBonus) * mult) + brewCount * 5 + reagentStacks * 3;
            int totalDmg = 0;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n  ⚗ {Name} lanza BOMBA ALQUÍMICA! (Brew #{brewCount}{(turboMode ? " TURBO" : "")})");

            foreach (var e in enemies)
            {
                e.TakeDamage(splashDmg, ElementType.Poison);
                e.ApplyStatus(StatusEffect.Poisoned, 3 + reagentStacks / 3);
                totalDmg += splashDmg;
                Console.WriteLine($"     → {e.Name} recibe {splashDmg} + VENENO.");
            }

            // Chance de aplicar quemadura también
            if (R.Next(100) < 40 || turboMode)
            {
                foreach (var e in enemies)
                    e.ApplyStatus(StatusEffect.Burned, 2);
                Console.WriteLine($"  🔥 Reacción química: Quemadura aplicada!");
            }

            GlobalStats.TotalDamageDealt += totalDmg;
            turboMode = false;
            reagentStacks = Math.Max(0, reagentStacks - 2);
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            turboMode = true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  💥 {Name} prepara la GRAN EXPLOSIÓN. Próxima bomba: x2.5 daño + todos los efectos!");
            Console.ResetColor();

            // También cura al aliado más dañado como bonus
            var hurt = allies.Where(p => p.IsAlive()).OrderBy(p => (float)p.Health / p.MaxHealth).First();
            int heal = 50 + Level * 3;
            hurt.Heal(heal);
            Console.WriteLine($"  🧪 Antídoto de emergencia para {hurt.Name}: +{heal} HP.");
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            reagentStacks = Math.Min(reagentStacks + 3, 15);

            var hurt = allies.Where(p => p.IsAlive()).OrderBy(p => (float)p.Health / p.MaxHealth).First();
            int healAmt = 40 + Level * 5 + reagentStacks * 2;
            hurt.Heal(healAmt);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ⚗ {Name} prepara POCIÓN IMPROVISADA para {hurt.Name}: +{healAmt} HP. Reactivos: {reagentStacks}/15");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            BaseAttack += 2;
            Mana += 5;
        }
    }

    // ── 11. BERSERKER ──────────────────────────────
    class Berserker : Player
    {
        static readonly Random R = new Random();
        int bloodlust = 0;
        bool frenzyActive = false;
        int selfDamageCount = 0;

        public Berserker(string n) : base(n, 160, 35, 28, def: 6, spd: 14)
        {
            ClassName = "Berserker"; SpecialName = "Furia Sangrienta";
            Special2Name = "Locura de Sangre"; SpecialCost = 10; SpecialCost2 = 20; CritChance = 22;
            AttackElement = ElementType.Physical;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            // El berserker puede hacerse daño para aumentar el daño
            int selfDmg = (int)(MaxHealth * 0.08f);
            Health = Math.Max(1, Health - selfDmg);
            selfDamageCount++;

            float hpRatio = 1 - (float)Health / MaxHealth;
            float mult = 2.0f + hpRatio * 3.0f + bloodlust * 0.2f;
            if (frenzyActive) mult *= 1.5f;

            var target = enemies.OrderByDescending(e => e.MaxHealth).First();
            int dmg = (int)((BaseAttack + AttackBonus) * mult) + R.Next(0, 20);

            bool crit = R.Next(100) < CritChance + (int)(hpRatio * 30);
            if (crit) { dmg = (int)(dmg * 2.0f); GlobalStats.TotalCriticalHits++; }

            target.TakeDamage(dmg, ElementType.Physical);
            bloodlust = Math.Min(bloodlust + 2, 20);
            GlobalStats.TotalDamageDealt += dmg;

            if (dmg > GlobalStats.HighestDamageInOneTurn)
            {
                GlobalStats.HighestDamageInOneTurn = dmg;
                GlobalStats.HighestDamageDealer = Name;
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n  💢 {Name} usa FURIA SANGRIENTA (HP:{Health}/{MaxHealth}, x{mult:F1}) en {target.Name} por {dmg}{(crit ? " 💥DOBLE CRÍTICO" : "")}! (Auto-daño: {selfDmg})");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            frenzyActive = !frenzyActive;
            bloodlust += 5;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(frenzyActive
                ? $"\n  💢 {Name} entra en LOCURA DE SANGRE! x1.5 a Furia, +{bloodlust} Sed de Sangre. ¡Imparable!"
                : $"\n  {Name} calma la Locura de Sangre.");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Se cura con la propia rabia
            int heal = bloodlust * 4 + selfDamageCount * 5;
            Heal(heal);
            ApplyStatus(StatusEffect.Enraged, 2);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n  💢 {Name} canaliza la RABIA. Recupera {heal} HP. Sed de Sangre: {bloodlust}. Frenesí: {(frenzyActive ? "✔" : "✗")}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            BaseAttack += 5;
            MaxHealth -= 5; // Tradeoff
            CritChance += 2;
        }
    }

    // ── 12. DRUIDA ──────────────────────────────
    class Druid : Player
    {
        static readonly Random R = new Random();
        bool wildForm = false;
        int naturePower = 0;
        bool stormActive = false;

        public Druid(string n) : base(n, 132, 100, 15, def: 9, spd: 11)
        {
            ClassName = "Druida"; SpecialName = "Llamada de la Naturaleza";
            Special2Name = "Forma Salvaje"; SpecialCost = 22; SpecialCost2 = 25;
            AttackElement = ElementType.Earth;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int bonus = naturePower * 6;
            naturePower = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  🌿 {Name} invoca la LLAMADA DE LA NATURALEZA! (Poder Natural: {bonus})");

            if (wildForm)
            {
                // En forma salvaje: ataque masivo
                foreach (var e in enemies)
                {
                    int dmg = (int)((BaseAttack + AttackBonus) * 2.2f) + bonus + R.Next(10, 25);
                    e.TakeDamage(dmg, ElementType.Earth);
                    e.ApplyStatus(StatusEffect.Stunned, 1);
                    Console.WriteLine($"  🐻 Ataque bestial a {e.Name}: {dmg} daño + Aturdido");
                }
            }
            else
            {
                // Forma normal: equilibrio cura + daño
                foreach (var p in Program.Players.Where(p => p.IsAlive()))
                {
                    int heal = 35 + bonus / 2;
                    p.Heal(heal);
                    p.ApplyStatus(StatusEffect.Regenerating, 3);
                }
                var strongest = enemies.OrderByDescending(e => e.MaxHealth).First();
                int earthDmg = (int)((BaseAttack + AttackBonus) * 1.5f) + bonus;
                strongest.TakeDamage(earthDmg, ElementType.Earth);
                Console.WriteLine($"  🌳 Cura grupal + {earthDmg} daño a {strongest.Name}.");
            }
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            wildForm = !wildForm;

            if (wildForm)
            {
                BaseAttack += 12;
                Defense -= 4;
                MaxHealth += 30;
                Health = Math.Min(Health + 30, MaxHealth);
                AttackElement = ElementType.Physical;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n  🐻 {Name} se transforma en FORMA SALVAJE! +12 ATK, +30 HP, Elemento Físico.");
            }
            else
            {
                BaseAttack -= 12;
                Defense += 4;
                MaxHealth -= 30;
                Health = Math.Min(Health, MaxHealth);
                AttackElement = ElementType.Earth;
                Console.WriteLine($"\n  🌿 {Name} vuelve a su forma natural.");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            naturePower = Math.Min(naturePower + 3, 15);

            // Regeneración natural para todos
            foreach (var p in allies.Where(p => p.IsAlive()))
            {
                p.Heal(12 + naturePower);
                if (!p.HasStatus(StatusEffect.Regenerating))
                    p.ApplyStatus(StatusEffect.Regenerating, 2);
            }

            // Invoca plantas que dañan a enemigos
            if (stormActive)
            {
                foreach (var e in enemies)
                {
                    int dmg = 8 + Level * 2;
                    e.TakeDamage(dmg, ElementType.Earth);
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n  🌿 Tormenta de Espinas activa! Plantas atacan a todos los enemigos.");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  🌱 {Name} sintoniza con la Naturaleza. Poder: {naturePower}/15. Forma: {(wildForm ? "Salvaje 🐻" : "Natural 🌿")}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            MaxHealth += 8;
            Mana += 8;
        }
    }

    // ── 13. INVOCADOR ──────────────────────────────
    class Summoner : Player
    {
        static readonly Random R = new Random();
        List<SummonedCreature> summons = new();
        int summonPower = 0;
        int maxSummons = 3;

        public Summoner(string n) : base(n, 110, 120, 11, def: 5, spd: 10)
        {
            ClassName = "Invocador"; SpecialName = "Invocación Masiva";
            Special2Name = "Potenciar Invocaciones"; SpecialCost = 28; SpecialCost2 = 20;
            AttackElement = ElementType.Arcane;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  ✨ {Name} usa INVOCACIÓN MASIVA!");

            // Invocar múltiples criaturas
            string[] creatureNames = { "Elemental de Fuego", "Golem de Piedra", "Serpiente Arcana", "Espíritu Lunar", "Fénix Menor" };
            int toSummon = Math.Min(2, maxSummons - summons.Count);

            for (int i = 0; i < toSummon; i++)
            {
                var creature = new SummonedCreature(
                    creatureNames[R.Next(creatureNames.Length)],
                    50 + Level * 8 + summonPower * 5,
                    10 + Level * 3 + summonPower * 2
                );
                summons.Add(creature);
                Console.WriteLine($"  ⚡ ¡Invocado: {creature.Name} (HP:{creature.Health} ATK:{creature.BaseAttack})!");
            }

            // Las invocaciones existentes atacan
            AttackWithSummons(enemies);
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            summonPower = Math.Min(summonPower + 3, 15);

            foreach (var s in summons)
                s.LevelUp();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  ✨ {Name} potencia sus invocaciones. Poder: {summonPower}. Criaturas mejoradas!");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Atacar con invocaciones y recuperar algunas
            AttackWithSummons(enemies);

            summons.RemoveAll(s => !s.IsAlive());

            // Invocar si hay espacio
            if (summons.Count < maxSummons && Mana >= 10)
            {
                string[] names = { "Familiar Arcano", "Sombra Invocada", "Espíritu Guardián" };
                var s = new SummonedCreature(names[R.Next(names.Length)], 30 + Level * 5, 8 + Level * 2);
                summons.Add(s);
                Mana -= 10;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  ✨ {Name} invoca a {s.Name}. Total: {summons.Count}/{maxSummons}");
                Console.ResetColor();
            }
        }

        private void AttackWithSummons(List<Enemy> enemies)
        {
            foreach (var s in summons.Where(s => s.IsAlive()))
            {
                if (!enemies.Any()) break;
                var target = enemies[R.Next(enemies.Count)];
                int dmg = s.Attack();
                target.TakeDamage(dmg, ElementType.Arcane);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  ✨ {s.Name} ataca a {target.Name} por {dmg}.");
                Console.ResetColor();
            }
        }

        protected override void OnLevelUp()
        {
            Mana += 12;
            if (Level % 5 == 0) maxSummons++;
        }
    }

    class SummonedCreature
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int BaseAttack { get; private set; }
        static readonly Random R = new Random();

        public SummonedCreature(string name, int hp, int atk)
        {
            Name = name; Health = hp; MaxHealth = hp; BaseAttack = atk;
        }

        public bool IsAlive() => Health > 0;
        public int Attack() => BaseAttack + R.Next(-2, 6);
        public void TakeDamage(int dmg) => Health = Math.Max(0, Health - dmg);
        public void LevelUp() { BaseAttack += 3; MaxHealth += 15; Health = Math.Min(Health + 15, MaxHealth); }
    }

    // ── 14. CAZADOR DE DEMONIOS ──────────────────────────────
    class DemonHunter : Player
    {
        static readonly Random R = new Random();
        int demonKills = 0;
        bool shadowMode = false;
        bool dualWielding = false;
        int hunterMark = 0;

        public DemonHunter(string n) : base(n, 120, 80, 21, def: 7, spd: 17)
        {
            ClassName = "Cazador de Demonios"; SpecialName = "Marca del Cazador";
            Special2Name = "Modo Umbral"; SpecialCost = 18; SpecialCost2 = 22; CritChance = 25;
            AttackElement = ElementType.Dark;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var target = enemies.OrderByDescending(e => e.MaxHealth).First();
            hunterMark++;

            float mult = 2.0f + demonKills * 0.15f + hunterMark * 0.2f;
            if (shadowMode) mult *= 1.6f;

            int dmg = (int)((BaseAttack + AttackBonus) * mult);
            bool crit = R.Next(100) < CritChance + demonKills;
            if (crit) { dmg = (int)(dmg * 2f); GlobalStats.TotalCriticalHits++; }

            target.TakeDamage(dmg, ElementType.Dark);
            target.ApplyStatus(StatusEffect.Weakened, 3);
            target.ApplyStatus(StatusEffect.Cursed, 2);

            GlobalStats.TotalDamageDealt += dmg;

            if (!target.IsAlive()) demonKills++;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  🏹 {Name} usa MARCA DEL CAZADOR en {target.Name} por {dmg}{(crit ? " 💥CAZADOR CERTERO" : "")}! Demonios cazados: {demonKills}");
            Console.ResetColor();
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            shadowMode = !shadowMode;

            if (shadowMode)
            {
                dualWielding = true;
                ApplyStatus(StatusEffect.Invisible, 3);
                ApplyStatus(StatusEffect.Hasted, 3);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"\n  🌑 {Name} entra en MODO UMBRAL. x1.6 dmg + Invisible + Veloz + Doble Arma!");
            }
            else
            {
                dualWielding = false;
                Console.WriteLine($"\n  {Name} desactiva el Modo Umbral.");
            }
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            // Marcar a todos los enemigos
            foreach (var e in enemies)
                e.ApplyStatus(StatusEffect.Cursed, 2);

            int bonusDmg = demonKills * 5;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  🔍 {Name} marca a todos los enemigos. Demonios cazados: {demonKills} (+{bonusDmg} daño acumulado)");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            CritChance += 2;
            Speed += 2;
        }
    }

    // ── 15. ELEMENTAL ──────────────────────────────
    class Elementalist : Player
    {
        static readonly Random R = new Random();
        ElementType[] elements = { ElementType.Fire, ElementType.Ice, ElementType.Lightning, ElementType.Earth };
        int elementIndex = 0;
        int elementalCharge = 0;
        bool prismMode = false;

        public Elementalist(string n) : base(n, 100, 135, 13, def: 4, spd: 11)
        {
            ClassName = "Elementalista"; SpecialName = "Convergencia Elemental";
            Special2Name = "Prisma Arcano"; SpecialCost = 22; SpecialCost2 = 35; CritChance = 15;
            AttackElement = ElementType.Fire;
        }

        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            int charge = elementalCharge;
            elementalCharge = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;

            if (prismMode)
            {
                // Todos los elementos a la vez
                Console.WriteLine($"\n  🌈 {Name} lanza CONVERGENCIA ELEMENTAL PRISMA!");
                foreach (var e in enemies)
                {
                    int totalDmg = 0;
                    foreach (var elem in elements)
                    {
                        int dmg = (int)((BaseAttack + AttackBonus) * 1.0f) + charge * 3 + R.Next(8, 18);
                        e.TakeDamage(dmg, elem);
                        totalDmg += dmg;
                    }
                    Console.WriteLine($"  → {e.Name}: {totalDmg} daño total (todos los elementos)");
                }
                prismMode = false;
            }
            else
            {
                var currentElem = elements[elementIndex];
                Console.WriteLine($"\n  ⚡ {Name} lanza CONVERGENCIA ELEMENTAL [{currentElem}]! (Carga: {charge})");
                foreach (var e in enemies)
                {
                    int dmg = (int)((BaseAttack + AttackBonus) * 2.0f) + charge * 5 + R.Next(10, 25);
                    e.TakeDamage(dmg, currentElem);
                    Console.WriteLine($"  → {e.Name}: {dmg} daño [{currentElem}]");

                    switch (currentElem)
                    {
                        case ElementType.Fire: e.ApplyStatus(StatusEffect.Burned, 3); break;
                        case ElementType.Ice: e.ApplyStatus(StatusEffect.Frozen, 2); break;
                        case ElementType.Lightning: e.ApplyStatus(StatusEffect.Stunned, 1); break;
                        case ElementType.Earth: e.ApplyStatus(StatusEffect.Stunned, 1); break;
                    }
                }
            }
            Console.ResetColor();
            elementIndex = (elementIndex + 1) % elements.Length;
            AttackElement = elements[elementIndex];
        }

        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2;
            prismMode = true;
            elementalCharge = Math.Min(elementalCharge + 8, 20);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  🌈 {Name} activa el PRISMA ARCANO. Próxima convergencia usa TODOS los elementos!");
            Console.ResetColor();
        }

        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            elementalCharge = Math.Min(elementalCharge + 3, 20);
            elementIndex = (elementIndex + 1) % elements.Length;
            AttackElement = elements[elementIndex];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  💎 {Name} cambia al elemento [{elements[elementIndex]}]. Carga: {elementalCharge}/20. Prisma: {(prismMode ? "✔" : "✗")}");
            Console.ResetColor();
        }

        protected override void OnLevelUp()
        {
            Mana += 15;
            BaseAttack += 1;
        }
    }

    // ═══════════════════════════════════════════════
    //  PROGRAMA PRINCIPAL (EXPANDIDO)
    // ═══════════════════════════════════════════════

    class Program
    {
        static readonly Random Rand = new Random();
        internal static List<Player> Players = new List<Player>();
        internal static Guild? ActiveGuild = null;
        static List<Quest> GlobalQuests = new List<Quest>();
        static int round = 1;
        static int highestRound = 1;
        static WeatherType lastWeather = WeatherType.Clear;

        static void Main()
        {
            Console.Title = "⚔  RPG FINAL EDITION DELUXE  ⚔";
            Console.OutputEncoding = Encoding.UTF8;
            GlobalStats.GameStartTime = DateTime.Now;

            ShowIntro();
            SetupGuild();
            SetupPlayers();
            SetupQuests();
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
            Console.WriteLine("            ━━━  EDICIÓN DEFINITIVA DELUXE v3.0  ━━━\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  15 Clases  |  Mazmorras  |  Crafteo  |  Talentos  |  Misiones  |  Gremios");
            Console.WriteLine("  Clima Dinámico  |  Compañeros  |  Equipamiento  |  Jefes Míticos");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  Pulsa cualquier tecla para comenzar tu leyenda...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── SETUP GREMIO ─────────────────
        static void SetupGuild()
        {
            Console.Clear();
            PrintBox("🏰 CREAR GREMIO", ConsoleColor.Yellow);
            Console.Write("\n  Nombre de tu Gremio (o Enter para 'Los Sin Nombre'): ");
            string guildName = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(guildName)) guildName = "Los Sin Nombre";
            ActiveGuild = new Guild(guildName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ Gremio «{guildName}» fundado. ¡Que comience la leyenda!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── SETUP PLAYERS ─────────────────
        static void SetupPlayers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  Cantidad de jugadores (1-{GameConfig.MAX_PLAYERS}): ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > GameConfig.MAX_PLAYERS)
                n = 1;

            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ── Jugador {i + 1} ──");
                Console.ResetColor();
                var player = CreatePlayer();
                Players.Add(player);
                ActiveGuild?.Members.Add(player.Name);
            }
        }

        static Player CreatePlayer()
        {
            while (true)
            {
                Console.Clear();
                PrintBox("ELIGE TU CLASE", ConsoleColor.Cyan);
                Console.WriteLine();

                var classes = new (string num, string icon, string name, string desc, string stats)[]
                {
                    ("1",  "⚔", "Guerrero",          "Alta defensa y ataque. Vanguardia imparable.",         "HP:200 ATK:22 DEF:14 VEL:8"),
                    ("2",  "🏹","Arquero",            "Gran velocidad y críticos letales. Maestro a distancia.","HP:115 ATK:18 DEF:5  VEL:16"),
                    ("3",  "🔥","Mago",               "Magia devastadora con múltiples elementos.",           "HP:88  ATK:12 DEF:3  VEL:10"),
                    ("4",  "✨","Curandero",          "Sostiene al equipo. Puede resucitar aliados.",         "HP:125 ATK:9  DEF:7  VEL:9"),
                    ("5",  "🗡","Ladrón",             "Sigilo letal, robo de oro y veneno.",                  "HP:98  ATK:20 DEF:4  VEL:22"),
                    ("6",  "👊","Monje",              "Kombos devastadores y meditación regenerativa.",       "HP:135 ATK:19 DEF:10 VEL:18"),
                    ("7",  "🎵","Bardo",              "Buffs grupales y control del campo de batalla.",       "HP:108 ATK:12 DEF:5  VEL:14"),
                    ("8",  "🛡","Paladín",            "Tanque divino con auras protectoras y curas.",        "HP:165 ATK:20 DEF:16 VEL:9"),
                    ("9",  "🌑","Nigromante",         "Drena almas, invoca muertos y maldice enemigos.",     "HP:98  ATK:14 DEF:3  VEL:10"),
                    ("10", "⚗","Alquimista",         "Bombas explosivas y pociones improvisadas.",           "HP:105 ATK:15 DEF:6  VEL:13"),
                    ("11", "💢","Berserker",          "Daño extremo a costa de su propia vida.",              "HP:160 ATK:28 DEF:6  VEL:14"),
                    ("12", "🌿","Druida",             "Control de la naturaleza, transformación animal.",     "HP:132 ATK:15 DEF:9  VEL:11"),
                    ("13", "✨","Invocador",          "Ejército de criaturas mágicas a su servicio.",         "HP:110 ATK:11 DEF:5  VEL:10"),
                    ("14", "🌑","Cazador de Demonios","Especialista en oscuridad y caza de bestias.",         "HP:120 ATK:21 DEF:7  VEL:17"),
                    ("15", "💎","Elementalista",      "Dominio absoluto de los elementos.",                   "HP:100 ATK:13 DEF:4  VEL:11"),
                };

                foreach (var c in classes)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  {c.num,2}. {c.icon} {c.name,-22}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($" {c.desc}");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"      Stats: {c.stats}");
                    Console.ResetColor();
                }

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
                    "11" => new Berserker(name),
                    "12" => new Druid(name),
                    "13" => new Summoner(name),
                    "14" => new DemonHunter(name),
                    "15" => new Elementalist(name),
                    _ => null
                };

                if (player != null)
                {
                    // Aplicar bonus del gremio
                    if (ActiveGuild != null)
                    {
                        player.AttackBonus += ActiveGuild.BonusAttack;
                        player.Defense += ActiveGuild.BonusDefense;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ {player.ClassName} «{name}» creado correctamente.");
                    Console.ResetColor();

                    // Ofrecer compañero inicial
                    Console.Write("  ¿Deseas adoptar un compañero? (s/n): ");
                    if ((Console.ReadLine() ?? "").ToLower() == "s")
                        player.AdoptCompanion();

                    Console.ReadKey(true);
                    return player;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✘ Opción inválida. Intenta de nuevo.");
                Console.ResetColor();
                Console.ReadKey(true);
            }
        }

        static void SetupQuests()
        {
            GlobalQuests = QuestDatabase.GetStarterQuests();
            // Asignar las primeras 3 misiones automáticamente
            foreach (var p in Players)
            {
                foreach (var q in GlobalQuests.Take(3))
                {
                    q.Status = QuestStatus.Active;
                    q.StartedRound = round;
                    p.ActiveQuests.Add(q);
                }
            }
        }

        // ───────────────── GAME LOOP ─────────────────
        static void GameLoop()
        {
            while (Players.Any(p => p.IsAlive()))
            {
                highestRound = Math.Max(highestRound, round);
                GlobalStats.HighestRound = highestRound;
                WorldTime.Advance(2);

                // Cambiar bioma cada 5 rondas
                if (round % 5 == 0)
                {
                    var biomes = Enum.GetValues<BiomeType>();
                    WorldTime.ChangeBiome(biomes[Rand.Next(biomes.Length)]);
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ══════════  RONDA {round}  ══════════");
                Console.ResetColor();

                WorldTime.ShowWorldInfo();

                if (round % 10 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("  ⚠  ¡RONDA DE JEFE MÍTICO! ⚠");
                    Console.ResetColor();
                }
                else if (round % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  ¡RONDA DE JEFE! ⚠");
                    Console.ResetColor();
                }

                // Anunciar si cambió el clima
                if (lastWeather != WorldTime.CurrentWeather)
                {
                    lastWeather = WorldTime.CurrentWeather;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\n  🌤 El clima ha cambiado a: {WorldTime.GetWeatherIcon()} {WorldTime.CurrentWeather}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n  Pulsa una tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey(true);

                List<Enemy> enemies = GenerateEnemies();
                Battle(enemies);
                round++;

                // Actualizar gremio
                if (ActiveGuild != null)
                {
                    ActiveGuild.AddExperience(10 + round * 2);
                    ActiveGuild.TotalKills += enemies.Count;
                }
            }

            ShowGameOver();
        }

        // ───────────────── GENERAR ENEMIGOS (EXPANDIDO) ─────────────────
        static List<Enemy> GenerateEnemies()
        {
            var enemies = new List<Enemy>();
            int scalingHp = round * 10;
            int scalingAtk = round * 3;

            // Modificadores climáticos para enemigos
            if (WorldTime.CurrentWeather == WeatherType.Eclipse)
            {
                scalingHp += 30;
                scalingAtk += 10;
            }
            if (WorldTime.IsNight)
            {
                scalingHp += 15;
                scalingAtk += 5;
            }

            if (round % 10 == 0)
            {
                var boss = new MythicBoss("DIOS DEL ABISMO", 800 + scalingHp, 70 + scalingAtk);
                enemies.Add(boss);
                // Agrega minions al jefe mítico
                for (int i = 0; i < 2; i++)
                    enemies.Add(new EliteEnemy($"Guardián del Abismo {(char)('A' + i)}", 120 + scalingHp / 3, 20 + scalingAtk / 2));
            }
            else if (round % 5 == 0)
            {
                var bossNames = new[] { "SEÑOR DE LA OSCURIDAD", "JEFE DE HORDA", "REY ESQUELETO", "ARCHIMAGO CORRUPTO", "DRAGÓN INMORTAL" };
                string bossName = bossNames[Rand.Next(bossNames.Length)];
                enemies.Add(new LegendaryBoss(bossName, 400 + scalingHp, 45 + scalingAtk));
                GlobalStats.TotalBossesKilled++; // Se contabilizará al morir
            }
            else
            {
                int count = Players.Count + Rand.Next(1, 4);
                string[] types = GameConfig.EnemyTypes;

                for (int i = 0; i < count; i++)
                {
                    string eName = types[Rand.Next(types.Length)] + " " + (char)('A' + i);
                    int ehp = 55 + scalingHp + Rand.Next(35);
                    int eatk = 10 + scalingAtk + Rand.Next(6);
                    int gold = 5 + round;
                    int xp = 10 + round * 2;

                    int roll = Rand.Next(100);
                    if (roll < 8)
                        enemies.Add(new BossEnemy(eName + " ÉLITE", ehp + 80, eatk + 15));
                    else if (roll < 25)
                        enemies.Add(new EliteEnemy(eName + " ★", ehp + 40, eatk + 10));
                    else
                        enemies.Add(new Enemy(eName, ehp, eatk, gold, xp));
                }
            }

            return enemies;
        }

        // ───────────────── BATALLA (EXPANDIDA) ─────────────────
        static void Battle(List<Enemy> enemies)
        {
            int turnCount = 0;

            while (Players.Any(p => p.IsAlive()) && enemies.Count > 0)
            {
                turnCount++;

                // Ordenar por velocidad
                var turnOrder = Players.Where(p => p.IsAlive())
                    .OrderByDescending(p => p.Speed + WorldTime.GetWeatherEffects().spdMod)
                    .ToList();

                foreach (var p in turnOrder)
                {
                    if (!p.IsAlive()) continue;
                    if (p.HasStatus(StatusEffect.Stunned) || p.HasStatus(StatusEffect.Frozen) || p.HasStatus(StatusEffect.Petrified))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n  ⚠  {p.Name} no puede actuar este turno ({p.GetStatusString()}).");
                        Console.ResetColor();
                        p.Tick();
                        Console.ReadKey(true);
                        continue;
                    }

                    p.Tick();

                    // Acción del compañero
                    if (p.Pet != null && enemies.Any() && Rand.Next(100) < 35)
                    {
                        string petAction = p.Pet.ActInCombat(p, enemies);
                        if (!string.IsNullOrEmpty(petAction))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(petAction);
                            Console.ResetColor();
                        }
                    }

                    bool actionTaken = false;
                    while (!actionTaken)
                    {
                        Console.Clear();
                        ShowStatus(enemies);
                        ShowPlayerMenu(p);

                        string op = Console.ReadLine() ?? "";
                        actionTaken = HandlePlayerAction(p, op, enemies);
                    }

                    // Actualizar misiones
                    int newKills = enemies.Count(e => !e.IsAlive());
                    if (newKills > 0)
                    {
                        p.TotalKills += newKills;
                        GlobalStats.TotalEnemiesKilled += newKills;
                        p.UpdateQuestProgress(newKills);
                        if (ActiveGuild != null) ActiveGuild.TotalKills += newKills;
                    }

                    enemies.RemoveAll(e => !e.IsAlive());
                    if (enemies.Count == 0) break;
                }

                if (enemies.Count > 0)
                    EnemyTurn(enemies);

                foreach (var p in Players.Where(p => !p.IsAlive() && !p.HasUsedRevive))
                {
                    // Talent: Resiliencia (resurrección por talento)
                    if (p.Talents.GetUnlockedTalents().Contains("Resiliencia"))
                    {
                        p.Health = (int)(p.MaxHealth * 0.3f);
                        p.HasUsedRevive = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n  ✨ ¡El talento RESILIENCIA revive a {p.Name} con {p.Health} HP!");
                        Console.ResetColor();
                    }
                    else
                    {
                        p.DeathMessage();
                    }
                }
            }

            if (Players.Any(p => p.IsAlive()))
                VictoryScreen(enemies);
        }

        public static void StaticBattle(List<Enemy> enemies, List<Player> players)
        {
            while (players.Any(p => p.IsAlive()) && enemies.Any())
            {
                foreach (var p in players.Where(p => p.IsAlive()))
                {
                    if (!enemies.Any()) break;
                    p.Tick();
                    Console.Clear();
                    ShowStatusStatic(enemies, players);
                    ShowPlayerMenu(p);

                    string op = Console.ReadLine() ?? "1";
                    HandlePlayerActionStatic(p, op, enemies, players);
                    enemies.RemoveAll(e => !e.IsAlive());
                }
                if (enemies.Any())
                    EnemyTurnStatic(enemies, players);
            }
        }

        static void ShowStatusStatic(List<Enemy> enemies, List<Player> players)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ── JUGADORES ──");
            Console.ResetColor();
            foreach (var p in players)
            {
                Console.ForegroundColor = p.IsAlive() ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.WriteLine($"  {p.ClassName[0]} {p.Name,-14} HP:{p.Health}/{p.MaxHealth} MP:{p.Mana} Lvl:{p.Level}");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ── ENEMIGOS ──");
            Console.ResetColor();
            foreach (var e in enemies)
                Console.WriteLine($"  {e.Name,-22} HP:{e.Health}/{e.MaxHealth}");
        }

        static bool HandlePlayerActionStatic(Player p, string op, List<Enemy> enemies, List<Player> players)
        {
            if (op == "1" && enemies.Any())
            {
                var target = enemies[Rand.Next(enemies.Count)];
                int dmg = p.Attack();
                target.TakeDamage(dmg, p.AttackElement);
                Console.WriteLine($"\n  {p.Name} ataca a {target.Name} por {dmg}.");
                Console.ReadKey(true);
                return true;
            }
            if (op == "2" && p.Mana >= p.SpecialCost)
            {
                p.Special(enemies);
                Console.ReadKey(true);
                return true;
            }
            return true;
        }

        static void EnemyTurnStatic(List<Enemy> enemies, List<Player> players)
        {
            var alive = players.Where(p => p.IsAlive()).ToList();
            foreach (var e in enemies)
            {
                if (!alive.Any()) break;
                var target = alive[Rand.Next(alive.Count)];
                int dmg = e.Attack();
                target.TakeDamage(dmg, ElementType.Physical);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"  {e.Name} ataca a {target.Name} por {dmg}.");
                Console.ResetColor();
            }
            Console.ReadKey(true);
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
                        if (crit)
                        {
                            dmg = (int)(dmg * GameConfig.BASE_CRIT_MULTIPLIER);
                            GlobalStats.TotalCriticalHits++;
                        }

                        // Clima modifica daño
                        int weatherMod = WorldTime.GetWeatherEffects().atkMod;
                        dmg = Math.Max(1, dmg + weatherMod);

                        target.TakeDamage(dmg, p.AttackElement);
                        GlobalStats.TotalDamageDealt += dmg;

                        if (dmg > GlobalStats.HighestDamageInOneTurn)
                        {
                            GlobalStats.HighestDamageInOneTurn = dmg;
                            GlobalStats.HighestDamageDealer = p.Name;
                        }

                        Console.ForegroundColor = crit ? ConsoleColor.Yellow : ConsoleColor.White;
                        Console.WriteLine(crit
                            ? $"\n  💥 ¡CRÍTICO! {p.Name} golpea a {target.Name} por {dmg} [{p.AttackElement}]."
                            : $"\n  ⚔  {p.Name} ataca a {target.Name} por {dmg} [{p.AttackElement}].");
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
                        p.ApplyStatus(StatusEffect.Shielded, 1);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n  🛡 {p.Name} adopta postura defensiva. (-50% daño + escudo)");
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
                        StaticShop(p);
                        return true;
                    }

                case "6": // Habilidad pasiva/extra
                    {
                        p.PassiveAction(Players, enemies);
                        Console.ReadKey(true);
                        return true;
                    }

                case "7": // Segunda habilidad
                    {
                        if (p.Mana < p.SpecialCost2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n  ✘ Sin maná para habilidad 2 (necesitas {p.SpecialCost2}).");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            return false;
                        }
                        p.Special2(Players, enemies);
                        Console.ReadKey(true);
                        return true;
                    }

                case "8": // Árbol de Talentos
                    {
                        p.Talents.ShowTalentMenu(p);
                        return false; // No consume turno
                    }

                case "9": // Crafteo
                    {
                        p.OpenCrafting();
                        return false; // No consume turno
                    }

                case "10": // Misiones
                    {
                        p.ShowQuests();
                        return false;
                    }

                case "11": // Mazmorra
                    {
                        OpenDungeonMenu(p);
                        return false;
                    }

                case "12": // Gremio
                    {
                        ActiveGuild?.ShowInfo();
                        return false;
                    }

                case "13": // Estadísticas globales
                    {
                        GlobalStats.ShowStats();
                        return false;
                    }

                case "14": // Ver estadísticas del personaje
                    {
                        ShowDetailedStats(p);
                        return false;
                    }

                case "15": // Adoptar compañero
                    {
                        p.AdoptCompanion();
                        return false;
                    }

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ✘ Opción inválida.");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return false;
            }
        }

        static void OpenDungeonMenu(Player p)
        {
            Console.Clear();
            PrintBox("🗺 MAZMORRAS DISPONIBLES", ConsoleColor.Magenta);
            Console.WriteLine($"  Oro: {p.Gold}🪙  |  Nivel: {p.Level}\n");

            var dungeons = Dungeon.GetAvailableDungeons(round);
            for (int i = 0; i < dungeons.Count; i++)
            {
                ConsoleColor col = dungeons[i].Difficulty switch
                {
                    Rarity.Legendary => ConsoleColor.Magenta,
                    Rarity.Epic => ConsoleColor.Blue,
                    Rarity.Rare => ConsoleColor.Cyan,
                    _ => ConsoleColor.White
                };
                Console.ForegroundColor = col;
                Console.WriteLine($"  {i + 1}. [{dungeons[i].Difficulty}] {dungeons[i].Name}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"     Tipo: {dungeons[i].Type}  |  Pisos: {dungeons[i].Floors}  |  Recompensa: {dungeons[i].GoldReward}🪙 {dungeons[i].XPReward}XP");
                Console.ResetColor();
            }

            Console.WriteLine("  0. Cancelar");
            Console.Write("  → ");

            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= dungeons.Count)
            {
                dungeons[idx - 1].Explore(Players);
            }
        }

        static Enemy? ChooseTarget(List<Enemy> enemies)
        {
            if (enemies.Count == 1) return enemies[0];

            Console.WriteLine("\n  Elige objetivo:");
            for (int i = 0; i < enemies.Count; i++)
            {
                string hpBar = HealthBar(enemies[i].Health, enemies[i].MaxHealth, 10);
                ConsoleColor col = enemies[i] is BossEnemy ? ConsoleColor.DarkRed : ConsoleColor.DarkYellow;
                Console.ForegroundColor = col;
                Console.WriteLine($"  {i + 1}. {enemies[i].Name,-24} HP:{hpBar} {enemies[i].Health}/{enemies[i].MaxHealth}");
                Console.ResetColor();
            }

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
                if (e.HasStatus(StatusEffect.Stunned) || e.HasStatus(StatusEffect.Frozen))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"  {e.Name} no puede actuar este turno.");
                    Console.ResetColor();
                    continue;
                }

                // IA mejorada: targeting basado en tipo de enemigo
                Player target;
                if (e is BossEnemy && Rand.Next(100) < 50)
                    target = alivePlayers.OrderByDescending(p => p.Mana).First();
                else if (e is EliteEnemy && Rand.Next(100) < 40)
                    target = alivePlayers.OrderByDescending(p => p.AttackBonus).First();
                else
                    target = alivePlayers.OrderBy(p => (float)p.Health / p.MaxHealth).First();

                int dmg = e.Attack();
                int weatherDef = WorldTime.GetWeatherEffects().defMod;
                dmg = Math.Max(1, dmg + weatherDef);

                target.TakeDamage(dmg, ElementType.Physical);
                GlobalStats.TotalDamageReceived += dmg;

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"  {e.Name} ataca a {target.Name} por {dmg} daño.");
                Console.ResetColor();

                // Efectos de estado de enemigos élite y superiores
                if (e is EliteEnemy && Rand.Next(100) < 30)
                {
                    var badEffect = new[] { StatusEffect.Poisoned, StatusEffect.Burned, StatusEffect.Bleeding, StatusEffect.Weakened };
                    var effect = badEffect[Rand.Next(badEffect.Length)];
                    target.ApplyStatus(effect, 2);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"  ⚠  {target.Name} sufre {effect}.");
                    Console.ResetColor();
                }

                // Jefes usan ataques especiales
                if (e is BossEnemy && Rand.Next(100) < 30)
                {
                    e.UseSpecialAttack(target);
                }
            }

            Console.ReadKey(true);
        }

        // ───────────────── VICTORIA ─────────────────
        static void VictoryScreen(List<Enemy> killedEnemies)
        {
            Console.Clear();
            PrintBox("✔ VICTORIA", ConsoleColor.Green);

            bool isBossRound = round % 5 == 0;
            int goldEarned = Rand.Next(20, 60) + round * 4;
            int xpEarned = 30 + round * 6;

            if (isBossRound) { goldEarned *= 2; xpEarned *= 2; }
            if (WorldTime.CurrentWeather == WeatherType.Blessed) { goldEarned = (int)(goldEarned * 1.25f); }

            // Bonus de gremio
            if (ActiveGuild != null)
            {
                int guildBonus = (int)(goldEarned * ActiveGuild.BonusGoldPercent / 100f);
                goldEarned += guildBonus;
                ActiveGuild.AddGold(guildBonus);
                ActiveGuild.AddExperience(20 + round);
            }

            GlobalStats.TotalGoldEarned += goldEarned;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Oro ganado: {goldEarned} 🪙");
            Console.WriteLine($"  XP ganada:  {xpEarned}");
            if (isBossRound) Console.WriteLine("  ⭐ Bonus de Jefe x2!");
            Console.ResetColor();

            foreach (var p in Players.Where(p => p.IsAlive()))
            {
                p.Gold += goldEarned;
                p.GainXP(xpEarned);
                p.RoundsWon++;

                // Drop aleatorio con rareza según ronda
                int dropChance = 30 + round * 2;
                if (Rand.Next(100) < Math.Min(dropChance, 80))
                {
                    var drop = ItemDatabase.GetRandomDrop(round);
                    if (p.Inventory.Count < GameConfig.MAX_INVENTORY)
                    {
                        p.Inventory.Add(drop);
                        Console.ForegroundColor = drop.GetRarityColor();
                        Console.WriteLine($"\n  🎁 {p.Name} encontró: {drop.GetRarityStars()} [{drop.Rarity}] {drop.Name}!");
                        Console.ResetColor();
                    }
                }

                // Punt de talento cada 3 rondas
                if (round % 3 == 0)
                {
                    p.TalentPoints++;
                    p.Talents.AvailablePoints++;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"  ✨ {p.Name} gana 1 Punto de Talento (Total disponibles: {p.Talents.AvailablePoints})");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para la siguiente ronda...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── GAME OVER ─────────────────
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
            Console.WriteLine($"\n  Ronda alcanzada:       {round}");
            Console.WriteLine($"  Enemigos eliminados:   {GlobalStats.TotalEnemiesKilled}");
            Console.WriteLine($"  Jefes derrotados:      {GlobalStats.TotalBossesKilled}");
            Console.WriteLine($"  Oro total ganado:      {GlobalStats.TotalGoldEarned}🪙");
            Console.WriteLine($"  Daño total infligido:  {GlobalStats.TotalDamageDealt:N0}");
            Console.WriteLine($"  Mayor golpe:           {GlobalStats.HighestDamageInOneTurn} ({GlobalStats.HighestDamageDealer})");
            Console.WriteLine($"  Misiones completadas:  {GlobalStats.TotalQuestsCompleted}");
            Console.WriteLine($"  Mazmorras exploradas:  {GlobalStats.TotalDungeonsCleared}");

            if (ActiveGuild != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  Gremio «{ActiveGuild.Name}»: Nivel {ActiveGuild.Level} ({ActiveGuild.Rank})");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para salir...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        // ───────────────── TIENDA ─────────────────
        public static void StaticShop(Player p)
        {
            bool inShop = true;
            while (inShop)
            {
                Console.Clear();
                PrintBox($"🏪 TIENDA  (Oro: {p.Gold}🪙)", ConsoleColor.Yellow);

                var items = ShopDatabase.GetShopItems(round);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  Ronda actual: {round}  |  Inventario: {p.Inventory.Count}/{GameConfig.MAX_INVENTORY}");
                Console.ResetColor();
                Console.WriteLine();

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    ConsoleColor col = item.Price > p.Gold ? ConsoleColor.DarkGray : item.GetRarityColor();
                    Console.ForegroundColor = col;
                    Console.WriteLine($"  {i + 1,2}. {item.GetRarityStars()} [{item.Rarity,-10}] {item.Name,-28} {item.Price,5}🪙  — {item.Description}");
                    Console.ResetColor();
                }

                Console.WriteLine($"\n  0. Salir  |  R. Refrescar tienda ({GameConfig.SHOP_REFRESH_COST}🪙)");
                Console.Write("\n  Opción: ");

                string op = Console.ReadLine() ?? "";

                if (op == "0") { inShop = false; continue; }

                if (op.ToUpper() == "R")
                {
                    if (p.Gold >= GameConfig.SHOP_REFRESH_COST)
                    {
                        p.Gold -= GameConfig.SHOP_REFRESH_COST;
                        GlobalStats.TotalGoldSpent += GameConfig.SHOP_REFRESH_COST;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("  ✔ Tienda refrescada.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  ✘ Oro insuficiente para refrescar.");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                    continue;
                }

                if (int.TryParse(op, out int idx) && idx >= 1 && idx <= items.Count)
                {
                    var chosen = items[idx - 1];
                    if (p.Gold >= chosen.Price)
                    {
                        if (p.Inventory.Count >= GameConfig.MAX_INVENTORY)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  ✘ Inventario lleno.");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            continue;
                        }
                        p.Gold -= chosen.Price;
                        GlobalStats.TotalGoldSpent += chosen.Price;
                        p.Inventory.Add(chosen);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n  ✔ Compraste: [{chosen.Rarity}] {chosen.Name}");
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ══ RONDA {round} ══  {WorldTime.GetWeatherIcon()} {WorldTime.CurrentWeather}  {WorldTime.TimeOfDay}  Día {WorldTime.Day}\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ── JUGADORES ──");
            Console.ResetColor();

            foreach (var p in Players)
            {
                string hpBar = HealthBar(p.Health, p.MaxHealth, 12);
                string mpBar = ManaBar(p.Mana, 200, 8);
                string statusStr = p.HasAnyStatus() ? $" [{p.GetStatusString()}]" : "";
                string petStr = p.Pet != null ? $" {p.Pet.Icon}" : "";
                Console.ForegroundColor = p.IsAlive() ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.WriteLine($"  {p.ClassName[0]}  {p.Name,-14} HP:{hpBar}{p.Health,3}/{p.MaxHealth} MP:{mpBar} Lv:{p.Level} 🪙{p.Gold}{petStr}{statusStr}{(p.IsAlive() ? "" : " 💀")}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ── ENEMIGOS ──");
            Console.ResetColor();

            foreach (var e in enemies)
            {
                string hpBar = HealthBar(e.Health, e.MaxHealth, 10);
                string eStatus = e.HasAnyStatus() ? $" [{e.GetStatusString()}]" : "";
                ConsoleColor col = e is MythicBoss ? ConsoleColor.Magenta :
                                   e is LegendaryBoss ? ConsoleColor.DarkMagenta :
                                   e is BossEnemy ? ConsoleColor.DarkRed : ConsoleColor.DarkYellow;
                Console.ForegroundColor = col;
                Console.WriteLine($"  {e.Name,-24} HP:{hpBar}{e.Health,3}/{e.MaxHealth}{eStatus}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        static void ShowPlayerMenu(Player p)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ─── Turno de {p.Name} ({p.ClassName}) Lv.{p.Level} ───");
            Console.ResetColor();
            Console.WriteLine($"  1. ⚔  Atacar  [{p.AttackElement}]");
            Console.WriteLine($"  2. ✨ Especial [{p.SpecialName}]  (Coste: {p.SpecialCost} MP)");
            Console.WriteLine($"  3. 🛡 Defender  (-50% daño recibido)");
            Console.WriteLine($"  4. 🎒 Inventario  ({p.Inventory.Count}/{GameConfig.MAX_INVENTORY})");
            Console.WriteLine($"  5. 🏪 Tienda");
            Console.WriteLine($"  6. 💡 Habilidad Extra");
            Console.WriteLine($"  7. 💫 Segunda Habilidad [{p.Special2Name}]  (Coste: {p.SpecialCost2} MP)");
            Console.WriteLine($"  8. 🌟 Árbol de Talentos  (Puntos: {p.Talents.AvailablePoints})");
            Console.WriteLine($"  9. ⚗  Crafteo");
            Console.WriteLine($"  10. 📜 Misiones");
            Console.WriteLine($"  11. 🗺  Mazmorras");
            Console.WriteLine($"  12. 🏰 Gremio");
            Console.WriteLine($"  13. 📊 Estadísticas globales");
            Console.WriteLine($"  14. 📋 Mi Personaje");
            Console.WriteLine($"  15. 🐾 Adoptar compañero{(p.Pet != null ? $" [{p.Pet.Icon} {p.Pet.Name}]" : "")}");
            Console.Write("\n  Acción: ");
        }

        static void ShowDetailedStats(Player p)
        {
            Console.Clear();
            PrintBox($"📋 {p.Name.ToUpper()} — {p.ClassName}", ConsoleColor.Cyan);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n  ─── ESTADÍSTICAS BASE ───");
            Console.WriteLine($"  Nivel:           {p.Level} / {GameConfig.MAX_LEVEL}");
            Console.WriteLine($"  XP:              {p.XP} / {p.Level * 50}");
            Console.WriteLine($"  HP:              {p.Health} / {p.MaxHealth}");
            Console.WriteLine($"  Maná:            {p.Mana} / 200");
            Console.WriteLine($"  Ataque total:    {p.BaseAttack + p.AttackBonus}  (Base: {p.BaseAttack} + Bonus: {p.AttackBonus})");
            Console.WriteLine($"  Defensa:         {p.Defense}");
            Console.WriteLine($"  Velocidad:       {p.Speed}");
            Console.WriteLine($"  Crítico:         {p.CritChance}%");
            Console.WriteLine($"  Elemento:        {p.AttackElement}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ─── PROGRESIÓN ───");
            Console.WriteLine($"  Oro:             {p.Gold}🪙");
            Console.WriteLine($"  Rondas ganadas:  {p.RoundsWon}");
            Console.WriteLine($"  Enemigos muertos:{p.TotalKills}");
            Console.WriteLine($"  Pts. de Talento: {p.Talents.AvailablePoints}");

            if (p.Pet != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n  ─── COMPAÑERO ───");
                Console.WriteLine($"  {p.Pet.Icon} {p.Pet.Name}  Nivel {p.Pet.Level}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  ─── EQUIPAMIENTO ───");
            Console.WriteLine($"  Arma:      {(p.EquippedWeapon != null ? $"[{p.EquippedWeapon.Rarity}] {p.EquippedWeapon.Name}" : "(ninguna)")}");
            Console.WriteLine($"  Armadura:  {(p.EquippedArmor != null ? $"[{p.EquippedArmor.Rarity}] {p.EquippedArmor.Name}" : "(ninguna)")}");
            Console.WriteLine($"  Accesorio: {(p.EquippedAccessory != null ? $"[{p.EquippedAccessory.Rarity}] {p.EquippedAccessory.Name}" : "(ninguno)")}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  ─── TALENTOS DESBLOQUEADOS ───");
            var unlocked = p.Talents.GetUnlockedTalents();
            if (!unlocked.Any())
                Console.WriteLine("  (Ninguno aún)");
            else
                foreach (var t in unlocked)
                    Console.WriteLine($"  ✔ {t}");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n  ─── INVENTARIO ({p.Inventory.Count}/{GameConfig.MAX_INVENTORY}) ───");
            if (!p.Inventory.Any())
                Console.WriteLine("  (vacío)");
            else
            {
                var grouped = p.Inventory.GroupBy(i => i.Rarity).OrderByDescending(g => g.Key);
                foreach (var g in grouped)
                {
                    Console.ForegroundColor = GameConfig.RarityColors[(int)g.Key];
                    Console.WriteLine($"  [{g.Key}]: {string.Join(", ", g.Select(i => i.Name))}");
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para volver...");
            Console.ResetColor();
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

            string bar = new string('█', filled) + new string('░', width - filled);
            Console.ResetColor();
            return bar;
        }

        static string ManaBar(int current, int max, int width)
        {
            if (max <= 0) return new string('░', width);
            int filled = (int)((float)current / max * width);
            filled = Math.Clamp(filled, 0, width);
            Console.ForegroundColor = ConsoleColor.Blue;
            string bar = new string('█', filled) + new string('░', width - filled);
            Console.ResetColor();
            return bar;
        }

        internal static void PrintBox(string title, ConsoleColor color)
        {
            int len = GetDisplayLength(title);
            string line = new string('═', len + 4);
            Console.ForegroundColor = color;
            Console.WriteLine($"  ╔{line}╗");
            Console.WriteLine($"  ║  {title}  ║");
            Console.WriteLine($"  ╚{line}╝");
            Console.ResetColor();
        }

        static int GetDisplayLength(string s)
        {
            // Cuenta solo caracteres ASCII para evitar desfase con emojis
            int len = 0;
            foreach (char c in s)
                len += (c > 127) ? 2 : 1;
            return Math.Max(len, s.Length);
        }
    }
}