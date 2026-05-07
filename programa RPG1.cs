using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
    //  ♪ SISTEMA DE SONIDO — VERSIÓN ÉPICA DELUXE
    //
    //  MEJORAS:
    //  - Música de ambiente más rica y variada (armonías, melodías más largas)
    //  - Batalla normal: tema épico con ostinato y melodía heroica
    //  - Batalla de boss: tema de terror/épico con descenso dramático
    //  - Batalla de boss mítico: caos total, notas bajas, tremolo, golpes
    //  - Victoria: fanfares orquestales con intro+cuerpo+remate distintos
    //  - Derrota: réquiems dramáticos con acorde final diferente por rango
    //  - Efectos de combate mejorados
    // ═══════════════════════════════════════════════

    static class SoundSystem
    {
        // ── Estado ──────────────────────────────────────────────────
        public static bool Enabled { get; private set; } = true;

        static Thread? _bgThread;
        static bool _bgRunning = false;
        static int _bgRound = 0;
        static bool _bossRound = false;
        static bool _mythicRound = false;

        // ── Toggle ───────────────────────────────────────────────────
        public static void Toggle()
        {
            Enabled = !Enabled;
            if (!Enabled) StopAmbient();
            Console.ForegroundColor = Enabled ? ConsoleColor.Green : ConsoleColor.DarkGray;
            Console.WriteLine($"\n  {(Enabled ? "🔊 Sonido activado" : "🔇 Sonido desactivado")}");
            Console.ResetColor();
        }

        // ── Primitivas ───────────────────────────────────────────────
        static void B(int freq, int ms)
        {
            if (!Enabled) return;
            try { Console.Beep(Math.Clamp(freq, 37, 32767), Math.Max(40, ms)); }
            catch { }
        }
        static void S(int ms) => Thread.Sleep(ms);

        static void PlayNotes(int[] notes, int[] dur)
        {
            for (int i = 0; i < notes.Length && _bgRunning && Enabled; i++)
                B(notes[i], dur[i]);
        }

        static void PlayNotesOnce(int[] notes, int[] dur)
        {
            for (int i = 0; i < notes.Length && Enabled; i++)
                B(notes[i], dur[i]);
        }

        // ════════════════════════════════════════════════════════════
        //  MÚSICA DE AMBIENTE — loops en hilo propio
        // ════════════════════════════════════════════════════════════

        public static void StartAmbient(int round, bool isBoss)
        {
            StopAmbient();
            if (!Enabled) return;
            _bgRound = round;
            _bossRound = isBoss;
            _mythicRound = round % 10 == 0;
            _bgRunning = true;
            _bgThread = new Thread(AmbientLoop) { IsBackground = true };
            _bgThread.Start();
        }

        public static void StopAmbient()
        {
            _bgRunning = false;
            _bgThread?.Join(400);
            _bgThread = null;
        }

        static void AmbientLoop()
        {
            while (_bgRunning && Enabled)
            {
                if (_mythicRound) AmbientBossMythic();
                else if (_bossRound) AmbientBossNormal();
                else AmbientByRound(_bgRound);
            }
        }

        // ─── AMBIENTES POR FASE ────────────────────────────────────

        static void AmbientByRound(int r)
        {
            if (r <= 4) AmbientVillage();
            else if (r <= 9) AmbientForest();
            else if (r <= 14) AmbientDungeon();
            else if (r <= 19) AmbientEpic();
            else AmbientAbyss();
        }

        // Rondas 1-4: Aldea tranquila — Do mayor, motivo de 2 frases
        static void AmbientVillage()
        {
            // Frase A
            int[] a = { 523, 587, 659, 698, 784, 698, 659, 587 };
            int[] ad = { 180, 160, 160, 160, 220, 160, 160, 180 };
            PlayNotes(a, ad); if (!_bgRunning) return; S(120);
            // Frase B (respuesta)
            int[] b = { 784, 880, 988, 880, 784, 698, 659, 523 };
            int[] bd = { 180, 160, 220, 160, 180, 160, 180, 400 };
            PlayNotes(b, bd); if (!_bgRunning) return; S(500);
        }

        // Rondas 5-9: Bosque — La menor, misterioso con armonías
        static void AmbientForest()
        {
            // Melodía
            int[] m = { 440, 494, 523, 494, 440, 392, 415, 440 };
            int[] md = { 200, 180, 200, 180, 200, 180, 200, 220 };
            PlayNotes(m, md); if (!_bgRunning) return; S(100);
            // Contrapunto bajo
            int[] b = { 220, 247, 262, 247, 220, 196, 208, 220 };
            int[] bd = { 200, 180, 200, 180, 200, 180, 200, 450 };
            PlayNotes(b, bd); if (!_bgRunning) return; S(400);
        }

        // Rondas 10-14: Caverna — Re menor, tenso con notas largas
        static void AmbientDungeon()
        {
            int[] m = { 294, 311, 330, 311, 294, 262, 277, 294 };
            int[] md = { 220, 200, 240, 200, 220, 200, 220, 260 };
            PlayNotes(m, md); if (!_bgRunning) return; S(80);
            int[] b = { 147, 156, 165, 156, 147, 131, 139, 147 };
            int[] bd = { 220, 200, 240, 200, 220, 200, 220, 500 };
            PlayNotes(b, bd); if (!_bgRunning) return; S(300);
        }

        // Rondas 15-19: Tierras épicas — Mi frigio, ritmo heroico
        static void AmbientEpic()
        {
            // Ostinato rítmico
            for (int i = 0; i < 2 && _bgRunning; i++) { B(330, 120); S(40); B(330, 80); S(40); B(349, 120); S(40); B(330, 80); S(40); }
            if (!_bgRunning) return;
            // Melodía épica
            int[] m = { 330, 392, 440, 494, 523, 494, 440, 392, 330 };
            int[] md = { 160, 140, 160, 180, 220, 160, 140, 160, 400 };
            PlayNotes(m, md); if (!_bgRunning) return; S(200);
        }

        // Rondas 20+: Abismo — Si menor, oscuro y lento
        static void AmbientAbyss()
        {
            int[] m = { 247, 262, 277, 262, 247, 233, 220, 208, 220 };
            int[] md = { 260, 240, 280, 240, 260, 240, 260, 280, 600 };
            PlayNotes(m, md); if (!_bgRunning) return; S(100);
            // Pulso grave amenazante
            for (int i = 0; i < 4 && _bgRunning; i++) { B(110, 180); S(220); }
            if (!_bgRunning) return; S(300);
        }

        // ─── BATALLA DE BOSS ───────────────────────────────────────
        // Boss normal: riff heroico/tenso con ostinato de cuerdas
        static void AmbientBossNormal()
        {
            // Ostinato de bajo agresivo (imitando cuerdas en pizzicato)
            for (int i = 0; i < 2 && _bgRunning; i++)
            {
                B(220, 100); S(30); B(247, 100); S(30); B(220, 80); S(30); B(196, 120); S(50);
            }
            if (!_bgRunning) return;
            // Melodía de batalla heroica — Mi menor
            int[] mel = { 330, 349, 392, 349, 330, 311, 294, 311, 330, 392, 440, 392, 330 };
            int[] dur = { 130, 110, 150, 110, 130, 110, 130, 110, 130, 130, 200, 130, 350 };
            PlayNotes(mel, dur); if (!_bgRunning) return; S(80);
            // Golpes de percusión simulados (notas graves muy cortas)
            for (int i = 0; i < 4 && _bgRunning; i++) { B(80, 60); S(120); }
            if (!_bgRunning) return; S(100);
        }

        // Boss mítico (ronda x10): caos absoluto — cromatismos, graves, tremolo
        static void AmbientBossMythic()
        {
            // Tremolo amenazante grave
            for (int i = 0; i < 6 && _bgRunning; i++) { B(110, 70); S(30); B(117, 70); S(30); }
            if (!_bgRunning) return;
            // Descenso cromático de terror
            int[] desc = { 220, 208, 196, 185, 175, 165, 156, 147, 139, 131 };
            int[] dd = { 150, 140, 140, 140, 140, 140, 140, 140, 140, 600 };
            PlayNotes(desc, dd); if (!_bgRunning) return; S(60);
            // Acorde disonante (golpe)
            B(185, 80); S(20); B(196, 80); S(20); B(233, 80); S(40);
            if (!_bgRunning) return;
            // Motivo de 3 notas repetido con urgencia
            for (int i = 0; i < 3 && _bgRunning; i++)
            {
                B(165, 100); S(30); B(175, 100); S(30); B(156, 160); S(80);
            }
            if (!_bgRunning) return; S(150);
        }

        // ════════════════════════════════════════════════════════════
        //  VICTORIA — fanfares orquestales únicas por rango de ronda
        // ════════════════════════════════════════════════════════════

        public static void PlayVictory(int round)
        {
            StopAmbient();
            if (!Enabled) return;
            if (round <= 4) VictoryEasy();
            else if (round <= 9) VictoryMedium();
            else if (round <= 14) VictoryHard();
            else if (round <= 19) VictoryEpic();
            else VictoryLegendary();
        }

        // Rondas 1-4: Fanfare animada corta — Do mayor ascendente + remate
        static void VictoryEasy()
        {
            // Intro: 3 golpes de trompeta
            B(523, 100); S(30); B(659, 100); S(30); B(784, 150); S(80);
            // Cuerpo
            int[] n = { 784, 880, 988, 880, 988, 1047 };
            int[] d = { 100, 90, 110, 90, 100, 400 };
            PlayNotesOnce(n, d);
        }

        // Rondas 5-9: Fanfare con dos frases y eco final
        static void VictoryMedium()
        {
            // Frase ascendente
            int[] a = { 523, 587, 659, 784, 659, 784, 880 };
            int[] ad = { 90, 80, 90, 110, 80, 90, 140 };
            PlayNotesOnce(a, ad); S(60);
            // Frase de respuesta más alta
            int[] b = { 880, 988, 1047, 988, 1047, 1175, 1047 };
            int[] bd = { 90, 90, 110, 80, 90, 120, 450 };
            PlayNotesOnce(b, bd);
        }

        // Rondas 10-14: Fanfare heroica con triada + corona
        static void VictoryHard()
        {
            // Redoble de tambor simulado
            for (int i = 0; i < 4; i++) { B(220, 50); S(50); }
            S(60);
            // Triada ascendente Do mayor
            int[] tr = { 523, 659, 784, 1047 };
            int[] trd = { 140, 130, 140, 200 };
            PlayNotesOnce(tr, trd); S(80);
            // Melodía heroica
            int[] m = { 1047, 988, 1047, 1175, 988, 1047, 1319, 1175, 1568 };
            int[] md = { 100, 80, 100, 120, 80, 100, 120, 100, 600 };
            PlayNotesOnce(m, md);
        }

        // Rondas 15-19: Himno épico con intro dramática + fanfare larga
        static void VictoryEpic()
        {
            // Intro dramática (4 notas de metales)
            B(392, 180); S(40); B(440, 160); S(40); B(523, 200); S(60);
            B(392, 100); S(20); B(523, 300); S(100);
            // Fanfare extendida Do mayor
            int[] n = { 523, 659, 784, 988, 1175, 988, 1175, 1319, 1175, 1319, 1568 };
            int[] d = { 90, 90, 90, 100, 110, 90, 100, 110, 90, 100, 500 };
            PlayNotesOnce(n, d); S(80);
            // Remate final doble
            B(1568, 100); S(30); B(1319, 80); S(30); B(1568, 600);
        }

        // Rondas 20+: Legado legendario — intro+fanfare+coda épica
        static void VictoryLegendary()
        {
            // Intro solemne (3 golpes graves)
            B(262, 200); S(60); B(330, 200); S(60); B(392, 300); S(100);
            // Fanfare de gloria
            int[] n = { 523, 587, 659, 784, 880, 988, 1047, 988, 1047, 1175, 1319, 1175, 1319 };
            int[] d = { 80, 80, 80, 90, 90, 100, 110, 90, 100, 110, 120, 100, 180 };
            PlayNotesOnce(n, d); S(60);
            // Coda ascendente final
            int[] c = { 1319, 1568, 1319, 1568, 2093 };
            int[] cd = { 100, 120, 80, 120, 900 };
            PlayNotesOnce(c, cd);
        }

        // Victoria de boss
        public static void PlayBossVictory(int round)
        {
            StopAmbient();
            if (!Enabled) return;
            if (round % 10 == 0)
                BossVictoryMythic();
            else
                BossVictoryNormal();
        }

        // Victoria de boss normal: marcha del campeón
        static void BossVictoryNormal()
        {
            // Tambores (bajos cortos)
            for (int i = 0; i < 3; i++) { B(196, 80); S(80); }
            S(60);
            // Fanfare de campeón
            int[] n = { 523, 659, 784, 988, 784, 988, 1175, 988, 1175, 1568 };
            int[] d = { 100, 100, 100, 120, 90, 100, 120, 90, 100, 600 };
            PlayNotesOnce(n, d);
        }

        // Victoria de boss mítico: himno máximo épico
        static void BossVictoryMythic()
        {
            // Intro épica grave
            B(196, 250); S(50); B(247, 250); S(50); B(294, 300); S(100);
            // Escala gloriosa completa
            int[] sc = { 262, 330, 392, 523, 659, 784, 1047, 784, 1047, 1319, 1568 };
            int[] sd = { 150, 140, 140, 150, 140, 150, 200, 120, 150, 170, 800 };
            PlayNotesOnce(sc, sd); S(100);
            // Remate legendario triple
            B(1568, 150); S(40); B(1319, 120); S(30); B(1568, 180); S(40); B(2093, 1000);
        }

        // ════════════════════════════════════════════════════════════
        //  DERROTA — réquiems dramáticos únicos por rango de ronda
        // ════════════════════════════════════════════════════════════

        public static void PlayDefeat(int round)
        {
            StopAmbient();
            if (!Enabled) return;
            if (round <= 4) DefeatEarly();
            else if (round <= 9) DefeatMedium();
            else if (round <= 14) DefeatHard();
            else if (round <= 19) DefeatEpic();
            else DefeatLegendary();
        }

        // Rondas 1-4: Descenso suave — La mayor a La menor
        static void DefeatEarly()
        {
            int[] n = { 440, 415, 392, 370, 349, 330 };
            int[] d = { 220, 200, 220, 220, 260, 600 };
            PlayNotesOnce(n, d);
        }

        // Rondas 5-9: Descenso dramático con pausa
        static void DefeatMedium()
        {
            int[] n = { 494, 466, 440, 415, 392, 370, 349 };
            int[] d = { 200, 180, 200, 200, 200, 220, 150 };
            PlayNotesOnce(n, d); S(120);
            // Acorde final triste
            B(294, 200); S(40); B(349, 200); S(40); B(440, 600);
        }

        // Rondas 10-14: Marcha fúnebre marcial
        static void DefeatHard()
        {
            // Golpes graves de tambor fúnebre
            B(131, 200); S(200); B(131, 200); S(600);
            // Melodía descendente oscura
            int[] n = { 294, 277, 262, 247, 233, 220, 208, 196 };
            int[] d = { 220, 200, 220, 220, 220, 220, 240, 500 };
            PlayNotesOnce(n, d); S(100);
            // Eco final
            B(131, 150); S(300); B(131, 600);
        }

        // Rondas 15-19: Réquiem épico con intro solemne
        static void DefeatEpic()
        {
            // Intro solemne 3 notas descendentes
            B(392, 300); S(100); B(330, 300); S(100); B(262, 400); S(150);
            // Melodía del réquiem
            int[] n = { 523, 494, 466, 440, 415, 392, 370, 349, 330, 311, 294 };
            int[] d = { 220, 200, 220, 220, 200, 220, 220, 220, 220, 240, 700 };
            PlayNotesOnce(n, d); S(80);
            // Golpe final de órgano
            B(196, 200); S(40); B(247, 200); S(40); B(294, 800);
        }

        // Rondas 20+: Réquiem legendario — máxima solemnidad y drama
        static void DefeatLegendary()
        {
            // Acorde de apertura solemne (3 voces)
            B(196, 400); S(80); B(247, 400); S(80); B(294, 500); S(200);
            // Melodía del réquiem legendario lenta
            int[] n = { 330, 311, 294, 277, 262, 247, 233, 220, 208, 196 };
            int[] d = { 280, 260, 280, 280, 280, 280, 280, 300, 300, 400 };
            PlayNotesOnce(n, d); S(150);
            // Segunda frase más grave
            int[] n2 = { 196, 185, 175, 165, 156, 147, 139, 131 };
            int[] d2 = { 300, 280, 300, 300, 300, 300, 320, 1200 };
            PlayNotesOnce(n2, d2);
        }

        // ════════════════════════════════════════════════════════════
        //  INTRO DEL JUEGO — tema principal épico
        // ════════════════════════════════════════════════════════════

        public static void PlayIntro()
        {
            if (!Enabled) return;
            // Arpegios ascendentes de obertura
            int[] ar = { 262, 330, 392, 523, 392, 330, 262 };
            int[] ard = { 100, 90, 90, 120, 80, 80, 100 };
            PlayNotesOnce(ar, ard); S(80);
            // Tema principal
            int[] m = { 523, 659, 784, 1047, 784, 659, 784, 880, 784, 523 };
            int[] md = { 120, 110, 110, 160, 110, 110, 110, 120, 110, 400 };
            PlayNotesOnce(m, md); S(60);
            // Remate final
            B(784, 100); S(30); B(1047, 300);
        }

        // ════════════════════════════════════════════════════════════
        //  EFECTOS DE COMBATE (mejorados)
        // ════════════════════════════════════════════════════════════

        public static void PlayLevelUp()
        {
            if (!Enabled) return;
            // Escala pentatónica ascendente festiva
            B(523, 70); B(659, 70); B(784, 70); B(1047, 80); B(1319, 200);
        }

        public static void PlayGuildLevelUp()
        {
            if (!Enabled) return;
            B(659, 70); B(784, 70); B(988, 70); B(1175, 80); B(1568, 300);
        }

        public static void PlayBossAlert()
        {
            if (!Enabled) return;
            // 3 golpes de alarma + nota sostenida grave
            for (int i = 0; i < 3; i++) { B(220, 130); S(70); }
            S(60); B(165, 100); S(30); B(150, 600);
        }

        // Ataque normal: corte metálico
        public static void PlayAttack()
        {
            if (!Enabled) return;
            B(880, 50); S(10); B(660, 50);
        }

        // Crítico: impacto + destello agudo
        public static void PlayCritical()
        {
            if (!Enabled) return;
            B(1100, 50); S(15); B(1320, 60); S(15); B(1760, 120);
        }

        // Especial: barrido de 3 notas ascendentes
        public static void PlaySpecial()
        {
            if (!Enabled) return;
            B(440, 70); S(20); B(660, 70); S(20); B(880, 100); S(20); B(1100, 140);
        }

        // Ataque enemigo: golpe sordo
        public static void PlayEnemyAttack()
        {
            if (!Enabled) return;
            B(180, 90); S(20); B(160, 80);
        }

        // Curación: acorde de Do mayor suave
        public static void PlayHeal()
        {
            if (!Enabled) return;
            B(523, 70); S(15); B(659, 70); S(15); B(784, 100); S(15); B(1047, 180);
        }

        // Defender: escudo sólido
        public static void PlayDefend()
        {
            if (!Enabled) return;
            B(330, 80); S(20); B(277, 120);
        }

        // Muerte de enemigo: descenso rápido
        public static void PlayEnemyDeath()
        {
            if (!Enabled) return;
            B(220, 80); S(20); B(175, 80); S(20); B(147, 150);
        }

        // Muerte del jugador: drama
        public static void PlayPlayerDeath()
        {
            if (!Enabled) return;
            B(262, 180); S(50); B(220, 180); S(50); B(175, 300); S(100); B(131, 600);
        }

        // Revivir: renacimiento épico
        public static void PlayRevive()
        {
            if (!Enabled) return;
            B(392, 70); S(20); B(523, 70); S(20);
            B(659, 70); S(20); B(784, 70); S(20);
            B(1047, 80); S(20); B(1319, 80); S(20); B(1568, 350);
        }

        // Ítems / UI
        public static void PlayItemUse() { if (Enabled) { B(784, 55); S(10); B(988, 75); } }
        public static void PlayItemBuy() { if (Enabled) { B(523, 55); S(10); B(659, 55); S(10); B(784, 90); } }
        public static void PlayItemCraft() { if (Enabled) { B(440, 70); S(15); B(550, 70); S(15); B(659, 70); S(15); B(880, 140); } }
        public static void PlayMenuSelect() { if (Enabled) B(600, 45); }
        public static void PlayShopRefresh() { if (Enabled) { B(440, 55); S(10); B(550, 55); S(10); B(659, 80); } }

        // Talento: fanfare corta brillante
        public static void PlayTalentUnlock()
        {
            if (!Enabled) return;
            B(784, 70); S(15); B(988, 70); S(15); B(1175, 70); S(15); B(1568, 200);
        }

        // Misión completa: trompeta de 4 notas
        public static void PlayQuestComplete()
        {
            if (!Enabled) return;
            B(523, 90); S(20); B(659, 90); S(20); B(784, 90); S(20); B(1047, 320);
        }

        // Tesoro: campanitas brillantes
        public static void PlayTreasure()
        {
            if (!Enabled) return;
            B(1047, 70); S(15); B(1319, 70); S(15);
            B(1568, 70); S(15); B(2093, 220);
        }

        // Trampa: alarma descendente
        public static void PlayTrap()
        {
            if (!Enabled) return;
            B(440, 90); S(20); B(370, 90); S(20); B(294, 180);
        }

        // Entrar a mazmorra: fanfare oscura de 3 notas
        public static void PlayDungeonEnter()
        {
            if (!Enabled) return;
            B(196, 220); S(40); B(175, 200); S(40); B(156, 400); S(60); B(131, 500);
        }

        // Estado de status
        public static void PlayStatusTick(StatusEffect s)
        {
            if (!Enabled) return;
            switch (s)
            {
                case StatusEffect.Poisoned: B(175, 90); break;
                case StatusEffect.Burned: B(660, 70); S(10); B(550, 70); break;
                case StatusEffect.Bleeding: B(140, 110); break;
                case StatusEffect.Regenerating: B(880, 70); S(10); B(988, 90); break;
                default: B(300, 55); break;
            }
        }

        // Elemento
        public static void PlayElemental(ElementType el)
        {
            if (!Enabled) return;
            switch (el)
            {
                case ElementType.Fire: B(600, 70); S(10); B(750, 100); break;
                case ElementType.Ice: B(1100, 55); S(10); B(1320, 80); break;
                case ElementType.Lightning: B(1400, 35); S(10); B(1600, 35); S(10); B(1760, 80); break;
                case ElementType.Dark: B(147, 110); S(20); B(131, 150); break;
                case ElementType.Holy: B(1047, 70); S(10); B(1319, 70); S(10); B(1568, 130); break;
                case ElementType.Poison: B(196, 90); S(10); B(175, 120); break;
                case ElementType.Earth: B(165, 100); S(20); B(147, 120); break;
                case ElementType.Wind: B(880, 55); S(10); B(1047, 55); S(10); B(1175, 80); break;
                default: PlayAttack(); break;
            }
        }
    }

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
            "Goblin","Orco","Esqueleto","Vampiro","Troll","Bandido","Lobo Oscuro",
            "Dragón Menor","Elemental","Espectro","Demonio","Gólem","Cultista","Mercenario",
            "Serpiente Gigante","Araña Venenosa","Zombi","Liche",
            "Barón Infernal","Hidra","Quimera","Mantícora","Fénix Oscuro"
        };

        public static readonly ConsoleColor[] RarityColors =
        {
            ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Cyan,
            ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Yellow
        };

        public static readonly string[] RarityStars =
        {
            "☆☆☆☆☆","★☆☆☆☆","★★☆☆☆","★★★☆☆","★★★★☆","★★★★★"
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
            WeatherType.Foggy => (-3, 0, 0, "Niebla: -3 ATK"),
            WeatherType.Blizzard => (-4, -2, -5, "Ventisca: Penalidades severas"),
            WeatherType.Heatwave => (3, -2, -2, "Calor: +3 ATK fuego, -2 DEF"),
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"  {GetBiomeIcon()} Bioma: {CurrentBiome}  |  {GetWeatherIcon()} Clima: {CurrentWeather}  |  {TimeOfDay} (Día {Day})");
            var (a, d, s, desc) = GetWeatherEffects();
            if (a != 0 || d != 0 || s != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  ⚠  {desc}");
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

        public Guild(string name) { Name = name; }

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
                SoundSystem.PlayGuildLevelUp();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  🏰 ¡El Gremio «{Name}» subió a Nivel {Level}!");
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
            Console.WriteLine($"  ⚔  ATK Bonus: +{BonusAttack}  🛡 DEF Bonus: +{BonusDefense}  🪙 Oro: +{BonusGoldPercent}%");
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
            int filled = Math.Clamp((int)(Progress() * width), 0, width);
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
            Console.WriteLine($"     Progreso: {ProgressBar()} {CurrentKills}/{KillsRequired}");
            Console.WriteLine($"     Recompensa: {GoldReward}🪙  {XPReward} XP  {(SpecialRewardItem != null ? "+ " + SpecialRewardItem : "")}");
            if (RoundsLimit < 99) Console.WriteLine($"     ⏰ Límite: {RoundsLimit} rondas");
            Console.ResetColor();
        }
    }

    static class QuestDatabase
    {
        public static List<Quest> GetStarterQuests() => new()
        {
            new Quest("Primera Sangre",   "Elimina 3 enemigos.",              50,  100,   3),
            new Quest("Exterminador",     "Elimina 10 enemigos.",            120,  200,  10),
            new Quest("Cazador de Jefes","Derrota tu primer jefe.",          200,  500,   1, itemReward:"Hacha Rúnica",     diff:Rarity.Rare),
            new Quest("Superviviente",   "Llega a la ronda 10.",             300,  600,   0, 10, diff:Rarity.Rare),
            new Quest("Masacre",         "Elimina 25 enemigos.",             250,  400,  25, diff:Rarity.Uncommon),
            new Quest("Carnicero",       "Elimina 50 enemigos.",             500,  800,  50, diff:Rarity.Rare),
            new Quest("Leyenda Viva",    "Elimina 100 enemigos.",           1000, 2000, 100, diff:Rarity.Legendary),
            new Quest("Caza Mayor",      "Derrota 3 jefes de horda.",        400,  700,   3, diff:Rarity.Epic),
            new Quest("Apocalipsis",     "Derrota un Dragón Ancestral.",    1500, 3000,   1, itemReward:"Escama Ancestral", diff:Rarity.Legendary),
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
                if (player.Inventory.Count(i => i.Name == ing.Key) < ing.Value) return false;
            return true;
        }

        public string GetIngredientsList() =>
            string.Join(", ", Ingredients.Select(kv => $"{kv.Value}x {kv.Key}"));
    }

    static class CraftingDatabase
    {
        public static List<CraftingRecipe> AllRecipes => new()
        {
            new CraftingRecipe("Gran Poción",         CraftingCategory.Potions,    new(){["Poción Menor"]=3},                            5,   1, Rarity.Uncommon,  "Restaura 100 HP"),
            new CraftingRecipe("Elixir Supremo",      CraftingCategory.Potions,    new(){["Elixir"]=2,["Hierba Mágica"]=1},             30,   5, Rarity.Rare,      "Restaura 150 HP y 80 MP"),
            new CraftingRecipe("Poción de Poder",     CraftingCategory.Potions,    new(){["Éter"]=2,["Cristal de Magia"]=1},            25,   3, Rarity.Rare,      "+15 ATK durante 3 rondas"),
            new CraftingRecipe("Espada Rúnica Avanzada",CraftingCategory.Weapons,  new(){["Hacha Rúnica"]=1,["Cristal de Fuego"]=2,["Metal Élfico"]=1}, 150, 10, Rarity.Epic, "+35 ATK, elemento Fuego"),
            new CraftingRecipe("Armadura Divina",     CraftingCategory.Armor,      new(){["Armadura de Hierro"]=1,["Fragmento Sagrado"]=3,["Metal Élfico"]=2}, 200, 12, Rarity.Epic, "+30 DEF, +40 MaxHP"),
            new CraftingRecipe("Amuleto del Dragón",  CraftingCategory.Accessories,new(){["Escama Ancestral"]=1,["Gema del Abismo"]=2}, 300,  15, Rarity.Legendary, "+50 MaxHP, +10 ATK"),
            new CraftingRecipe("Pergamino Épico",     CraftingCategory.Scrolls,    new(){["Pergamino de Fuego"]=2,["Tinta Arcana"]=1},   40,   4, Rarity.Rare,      "Hechizo masivo de fuego"),
            new CraftingRecipe("Antídoto Superior",   CraftingCategory.Potions,    new(){["Hierba Mágica"]=2},                          10,   1, Rarity.Common,    "Cura todos los estados negativos"),
            new CraftingRecipe("Explosivo Alquímico", CraftingCategory.Potions,    new(){["Cristal de Fuego"]=1,["Polvo Volcánico"]=2},  20,   3, Rarity.Uncommon,  "Inflige 80 daño a todos los enemigos"),
            new CraftingRecipe("Cetro del Maná",      CraftingCategory.Weapons,    new(){["Cristal de Magia"]=3,["Madera Antigua"]=1},  120,   8, Rarity.Rare,      "+20 ATK, +30 Maná máximo"),
            new CraftingRecipe("Botas del Viento",    CraftingCategory.Accessories,new(){["Pluma de Grifo"]=2,["Cuero Élfico"]=1},       80,   5, Rarity.Rare,      "+15 Velocidad"),
            new CraftingRecipe("Corona Oscura",       CraftingCategory.Accessories,new(){["Gema del Abismo"]=1,["Hueso de Liche"]=2,["Fragmento de Sombra"]=1}, 250, 14, Rarity.Legendary, "+25 ATK Oscuro"),
            new CraftingRecipe("Escudo Sagrado Supremo",CraftingCategory.Armor,    new(){["Fragmento Sagrado"]=4,["Oro Puro"]=2},       280,  13, Rarity.Legendary, "+40 DEF, Refleja 15% daño"),
            new CraftingRecipe("Ración de Viaje",     CraftingCategory.Food,       new(){["Carne Cruda"]=2,["Hierbas"]=1},                5,   1, Rarity.Common,    "+30 HP, regen"),
            new CraftingRecipe("Festín de Campeón",   CraftingCategory.Food,       new(){["Carne Cruda"]=4,["Especias Raras"]=2,["Trufa Mágica"]=1}, 50, 7, Rarity.Rare, "+80 HP, +20 MP, +5 ATK"),
        };

        public static Item? CraftItem(CraftingRecipe recipe, Player player)
        {
            if (!recipe.CanCraft(player)) return null;
            foreach (var ing in recipe.Ingredients)
            {
                var toRemove = player.Inventory.Where(i => i.Name == ing.Key).Take(ing.Value).ToList();
                foreach (var item in toRemove) player.Inventory.Remove(item);
            }
            player.Gold -= recipe.GoldCost;
            GlobalStats.TotalGoldSpent += recipe.GoldCost;
            GlobalStats.TotalItemsCrafted++;
            SoundSystem.PlayItemCraft();

            return recipe.ResultItemName switch
            {
                "Gran Poción" => new Item("Gran Poción", "Restaura 100 HP", ItemType.Potion, Rarity.Uncommon, 40, hp: 100),
                "Elixir Supremo" => new Item("Elixir Supremo", "150 HP y 80 MP", ItemType.Potion, Rarity.Rare, 80, hp: 150, mp: 80),
                "Poción de Poder" => new Item("Poción de Poder", "+15 ATK temporal", ItemType.Potion, Rarity.Rare, 60, atk: 15),
                "Espada Rúnica Avanzada" => new Item("Espada Rúnica Avanzada", "+35 ATK de Fuego", ItemType.Weapon, Rarity.Epic, 300, atk: 35),
                "Armadura Divina" => new Item("Armadura Divina", "+30 DEF +40 MaxHP", ItemType.Armor, Rarity.Epic, 350, def: 30, maxHp: 40),
                "Amuleto del Dragón" => new Item("Amuleto del Dragón", "+50 MaxHP +10 ATK", ItemType.Accessory, Rarity.Legendary, 500, atk: 10, maxHp: 50),
                "Antídoto Superior" => new Item("Antídoto Superior", "Cura estados", ItemType.Potion, Rarity.Common, 15),
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
        public int AtkBonus { get; }
        public int DefBonus { get; }
        public int HPBonus { get; }
        public int MPBonus { get; }
        public int CritBonus { get; }
        public int SpdBonus { get; }
        public float DamageMultiplier { get; }

        public TalentNode(string name, string desc, TalentTree tree, int cost, int tier,
                          int atk = 0, int def = 0, int hp = 0, int mp = 0,
                          int crit = 0, int spd = 0, float dmgMult = 1.0f, List<string>? prereqs = null)
        {
            Name = name; Description = desc; Tree = tree; Cost = cost; Tier = tier;
            AtkBonus = atk; DefBonus = def; HPBonus = hp; MPBonus = mp;
            CritBonus = crit; SpdBonus = spd; DamageMultiplier = dmgMult;
            Prerequisites = prereqs ?? new();
        }
    }

    class TalentSystem
    {
        public int AvailablePoints { get; set; }
        private List<TalentNode> _nodes;
        private List<string> _unlockedNames = new();

        public TalentSystem() { _nodes = BuildTalentTree(); }

        private List<TalentNode> BuildTalentTree() => new()
        {
            new TalentNode("Fuerza Bruta",       "+8 ATK base",                  TalentTree.Combat,  1,1, atk:8),
            new TalentNode("Golpe Preciso",      "+10% crítico",                 TalentTree.Combat,  1,1, crit:10),
            new TalentNode("Armadura Natural",   "+8 DEF",                       TalentTree.Combat,  1,1, def:8),
            new TalentNode("Constitución",       "+30 HP máx",                   TalentTree.Combat,  2,2, hp:30,  prereqs:new(){"Fuerza Bruta"}),
            new TalentNode("Golpe Devastador",   "x1.3 daño especial",           TalentTree.Combat,  2,2, dmgMult:1.3f, prereqs:new(){"Golpe Preciso"}),
            new TalentNode("Escudo Vivo",        "+15 DEF +20 HP",               TalentTree.Combat,  3,3, def:15,hp:20, prereqs:new(){"Armadura Natural","Constitución"}),
            new TalentNode("Masacre",            "x1.5 daño en crítico",         TalentTree.Combat,  4,4, crit:5,dmgMult:1.5f, prereqs:new(){"Golpe Devastador"}),
            new TalentNode("Guerrero Eterno",    "+50 HP +10 DEF +10 ATK",       TalentTree.Combat,  5,5, hp:50,def:10,atk:10, prereqs:new(){"Escudo Vivo","Masacre"}),
            new TalentNode("Canal Arcano",       "+20 MP máx",                   TalentTree.Magic,   1,1, mp:20),
            new TalentNode("Potencia Mágica",    "+12 ATK mágico",               TalentTree.Magic,   1,1, atk:12),
            new TalentNode("Mente Aguda",        "-2 coste de habilidades",      TalentTree.Magic,   2,2, mp:10, prereqs:new(){"Canal Arcano"}),
            new TalentNode("Hechizo Amplificado","x1.4 daño mágico",             TalentTree.Magic,   3,3, dmgMult:1.4f, prereqs:new(){"Potencia Mágica","Mente Aguda"}),
            new TalentNode("Maestro Arcano",     "+30 MP +20 ATK +x1.2 dmg",     TalentTree.Magic,   5,5, mp:30,atk:20,dmgMult:1.2f, prereqs:new(){"Hechizo Amplificado"}),
            new TalentNode("Empatía",            "Curas +20% efectivas",         TalentTree.Support, 1,1, hp:15),
            new TalentNode("Aura Protectora",    "Aliados -10% daño recibido",   TalentTree.Support, 2,2, prereqs:new(){"Empatía"}),
            new TalentNode("Mente Grupal",       "+5 ATK a todos al inicio",     TalentTree.Support, 3,3, prereqs:new(){"Aura Protectora"}),
            new TalentNode("Resiliencia",        "Revivir con 30% HP (1/batalla)",TalentTree.Support,5,5, prereqs:new(){"Mente Grupal"}),
            new TalentNode("Paso Silencioso",    "+10 Velocidad",                TalentTree.Stealth, 1,1, spd:10),
            new TalentNode("Ojo de Halcón",      "+15% crítico",                 TalentTree.Stealth, 1,1, crit:15),
            new TalentNode("Sombra Fugaz",       "50% esquivar 1 ataque/ronda",  TalentTree.Stealth, 3,3, prereqs:new(){"Paso Silencioso"}),
            new TalentNode("Asesino en las Sombras","x2.5 daño desde sigilo",   TalentTree.Stealth, 5,5, dmgMult:2.5f, prereqs:new(){"Sombra Fugaz","Ojo de Halcón"}),
            new TalentNode("Piel de Árbol",      "+12 DEF natural",              TalentTree.Nature,  1,1, def:12),
            new TalentNode("Regeneración",       "Recupera 8 HP/turno",          TalentTree.Nature,  2,2, hp:20, prereqs:new(){"Piel de Árbol"}),
            new TalentNode("Veneno Potenciado",  "Veneno hace 15 daño/turno",    TalentTree.Nature,  3,3, prereqs:new(){"Regeneración"}),
            new TalentNode("Espíritu del Bosque","+25 HP regen +20 DEF",         TalentTree.Nature,  5,5, def:20,hp:30, prereqs:new(){"Veneno Potenciado"}),
        };

        public void ShowTalentMenu(Player player)
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Program.PrintBox($"🌟 ÁRBOL DE TALENTOS — {player.Name} (Puntos: {AvailablePoints})", ConsoleColor.Yellow);
                foreach (var tree in Enum.GetValues<TalentTree>())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n  ── {tree} ──");
                    Console.ResetColor();
                    foreach (var node in _nodes.Where(n => n.Tree == tree).OrderBy(n => n.Tier))
                    {
                        bool canUnlock = AvailablePoints >= node.Cost && !node.Unlocked
                                         && node.Prerequisites.All(p => _unlockedNames.Contains(p));
                        ConsoleColor col = node.Unlocked ? ConsoleColor.Green : canUnlock ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
                        Console.ForegroundColor = col;
                        string st = node.Unlocked ? "✔" : canUnlock ? "○" : "✗";
                        Console.WriteLine($"  {st} T{node.Tier} [{node.Cost}pts] {node.Name}: {node.Description}");
                        if (node.Prerequisites.Any() && !node.Unlocked)
                            Console.WriteLine($"       Requiere: {string.Join(", ", node.Prerequisites)}");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("\n  Escribe el nombre del talento para desbloquearlo, o '0' para salir:");
                Console.Write("  → ");
                string input = Console.ReadLine() ?? "0";
                if (input == "0") { open = false; break; }

                var chosen = _nodes.FirstOrDefault(n => string.Equals(n.Name, input, StringComparison.OrdinalIgnoreCase));
                if (chosen == null) { Warn("Talento no encontrado."); }
                else if (chosen.Unlocked) { Warn("Ya desbloqueaste ese talento."); }
                else if (!chosen.Prerequisites.All(p => _unlockedNames.Contains(p))) { Warn($"Requieres: {string.Join(", ", chosen.Prerequisites)}"); }
                else if (AvailablePoints < chosen.Cost) { Warn($"Necesitas {chosen.Cost} puntos, tienes {AvailablePoints}."); }
                else
                {
                    chosen.Unlocked = true; _unlockedNames.Add(chosen.Name); AvailablePoints -= chosen.Cost;
                    player.AttackBonus += chosen.AtkBonus; player.Defense += chosen.DefBonus;
                    player.MaxHealth += chosen.HPBonus; player.Health = Math.Min(player.Health + chosen.HPBonus, player.MaxHealth);
                    player.Mana = Math.Min(player.Mana + chosen.MPBonus, 200);
                    player.CritChance += chosen.CritBonus; player.Speed += chosen.SpdBonus;
                    SoundSystem.PlayTalentUnlock();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ ¡Talento «{chosen.Name}» desbloqueado!");
                    Console.ResetColor(); Console.ReadKey(true);
                }
            }
        }

        void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✘ {msg}");
            Console.ResetColor(); Console.ReadKey(true);
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

        public DungeonRoom(string name, string desc, int enemies, bool treasure, bool trap, bool shop, bool boss, int trapDmg = 0)
        {
            Name = name; Description = desc; EnemyCount = enemies; HasTreasure = treasure;
            HasTrap = trap; HasShop = shop; IsBossRoom = boss; TrapDamage = trapDmg;
        }

        public static DungeonRoom GenerateRandom(int floor, DungeonType type)
        {
            string[] names = { "Sala Oscura","Cámara del Eco","Pasillo Maldito","Cripta Antigua",
                                "Salón del Trono","Biblioteca Secreta","Altar Profano","Galería de los Muertos" };
            int trapDmg = 10 + floor * 5 + R.Next(20);
            return new DungeonRoom(names[R.Next(names.Length)], GetDesc(type),
                1 + floor / 2 + R.Next(3), R.Next(100) < 35, R.Next(100) < 25, R.Next(100) < 20, false, trapDmg);
        }

        static string GetDesc(DungeonType t) => t switch
        {
            DungeonType.Cave => "Las paredes rezuman humedad. Hongos brillan en la oscuridad.",
            DungeonType.Ruins => "Antiguos frescos adornan paredes derruidas.",
            DungeonType.Volcano => "El calor es insoportable. La lava fluye por grietas.",
            DungeonType.IcePeak => "Estalactitas de hielo cuelgan del techo.",
            DungeonType.ShadowRealm => "Las sombras se mueven solas. Una presencia oscura acecha.",
            DungeonType.HolyTemple => "Luz sagrada ilumina el lugar. Estatuas vigilan.",
            DungeonType.Abyss => "El abismo se abre bajo tus pies.",
            _ => "Una habitación ordinaria... o eso parece."
        };
    }

    class Dungeon
    {
        public string Name { get; }
        public DungeonType Type { get; }
        public int Floors { get; }
        public List<DungeonRoom> Rooms { get; }
        public bool IsCompleted { get; private set; }
        public int GoldReward { get; }
        public int XPReward { get; }
        public Rarity Difficulty { get; }

        static readonly Random R = new Random();

        public Dungeon(string name, DungeonType type, int floors, int gold, int xp, Rarity diff)
        {
            Name = name; Type = type; Floors = floors; GoldReward = gold; XPReward = xp; Difficulty = diff;
            Rooms = new List<DungeonRoom>();
            for (int f = 1; f <= floors; f++)
                for (int r = 0; r < 3 + R.Next(3); r++) Rooms.Add(DungeonRoom.GenerateRandom(f, type));
        }

        public static List<Dungeon> GetAvailableDungeons(int round) => new()
        {
            new Dungeon("Cueva de los Murmullos",   DungeonType.Cave,        3, 150,  300, Rarity.Common),
            new Dungeon("Ruinas del Imperio Caído", DungeonType.Ruins,       4, 250,  500, Rarity.Uncommon),
            new Dungeon("Pico del Volcán Eterno",   DungeonType.Volcano,     5, 400,  800, Rarity.Rare),
            new Dungeon("Cima de Hielo Eterno",     DungeonType.IcePeak,     5, 400,  800, Rarity.Rare),
            new Dungeon("Reino de las Sombras",     DungeonType.ShadowRealm, 6, 600, 1200, Rarity.Epic),
            new Dungeon("Templo del Dios Solar",    DungeonType.HolyTemple,  6, 600, 1200, Rarity.Epic),
            new Dungeon("El Abismo Sin Fondo",      DungeonType.Abyss,       8,1000, 2000, Rarity.Legendary),
        };

        public void Explore(List<Player> players)
        {
            SoundSystem.PlayDungeonEnter();
            Console.Clear();
            Program.PrintBox($"🗺 MAZMORRA: {Name.ToUpper()}", ConsoleColor.Magenta);
            Console.WriteLine($"\n  Tipo: {Type}  |  Pisos: {Floors}  |  Dificultad: {Difficulty}");
            Console.WriteLine($"  Recompensa: {GoldReward}🪙  {XPReward} XP");
            Console.WriteLine("\n  Pulsa una tecla para explorar...");
            Console.ReadKey(true);

            SoundSystem.StartAmbient(99, false);

            foreach (var room in Rooms)
            {
                if (!players.Any(p => p.IsAlive())) break;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ─── {room.Name} ───");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"  {room.Description}");
                Console.ResetColor(); Console.ReadKey(true);

                if (room.HasTrap)
                {
                    SoundSystem.PlayTrap();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  ⚠  ¡TRAMPA!");
                    foreach (var p in players.Where(p => p.IsAlive()))
                    {
                        p.TakeDamage(room.TrapDamage, ElementType.Physical);
                        Console.WriteLine($"  {p.Name} recibe {room.TrapDamage} daño!");
                    }
                    Console.ResetColor(); Console.ReadKey(true);
                }

                if (room.HasShop)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n  🏪 ¡Mercader ambulante!");
                    Console.ResetColor(); Console.ReadKey(true);
                    foreach (var p in players.Where(p => p.IsAlive())) Program.StaticShop(p);
                }

                if (room.EnemyCount > 0 && !room.IsBossRoom)
                {
                    var enemies = GenerateDungeonEnemies(room.EnemyCount);
                    Program.StaticBattle(enemies, players);
                }

                if (room.HasTreasure && players.Any(p => p.IsAlive()))
                {
                    SoundSystem.PlayTreasure();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  💰 ¡Cofre del tesoro!");
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

            SoundSystem.StopAmbient();

            if (players.Any(p => p.IsAlive()))
            {
                IsCompleted = true;
                GlobalStats.TotalDungeonsCleared++;
                SoundSystem.PlayVictory(5);
                Console.Clear();
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
            for (int i = 0; i < count; i++)
            {
                string name = GameConfig.EnemyTypes[R.Next(GameConfig.EnemyTypes.Length)] + " " + (char)('A' + i);
                int hp = 60 + R.Next(40), atk = 12 + R.Next(8);
                enemies.Add(R.Next(100) < 20 ? new EliteEnemy(name + " ★", hp + 30, atk + 8) : new Enemy(name, hp, atk));
            }
            return enemies;
        }
    }

    // ═══════════════════════════════════════════════
    //  ITEM
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
            new Item("Poción Menor",       "Restaura 30 HP",           ItemType.Potion,    Rarity.Common,    10, hp:30),
            new Item("Poción Mayor",       "Restaura 70 HP",           ItemType.Potion,    Rarity.Uncommon,  25, hp:70),
            new Item("Poción Superior",    "Restaura 120 HP",          ItemType.Potion,    Rarity.Rare,      50, hp:120),
            new Item("Éter",               "Restaura 40 MP",           ItemType.Potion,    Rarity.Uncommon,  20, mp:40),
            new Item("Éter Mayor",         "Restaura 80 MP",           ItemType.Potion,    Rarity.Rare,      40, mp:80),
            new Item("Elixir",             "Restaura 80 HP y 40 MP",   ItemType.Potion,    Rarity.Rare,      50, hp:80, mp:40),
            new Item("Elixir Supremo",     "Restaura 150 HP y 80 MP",  ItemType.Potion,    Rarity.Epic,     120, hp:150, mp:80),
            new Item("Antídoto",           "Cura estados negativos",   ItemType.Potion,    Rarity.Common,    15),
            new Item("Poción de Berserker","Entra en modo Berserk",    ItemType.Potion,    Rarity.Rare,      60, status:StatusEffect.Berserked),
            new Item("Poción de Agilidad", "+15 VEL por 3 turnos",     ItemType.Potion,    Rarity.Uncommon,  30, spd:15),
            new Item("Daga de Hierro",     "+5 ATK",                   ItemType.Weapon,    Rarity.Common,    30, atk:5),
            new Item("Espada de Acero",    "+12 ATK",                  ItemType.Weapon,    Rarity.Uncommon,  60, atk:12),
            new Item("Hacha Rúnica",       "+20 ATK",                  ItemType.Weapon,    Rarity.Rare,     120, atk:20),
            new Item("Lanza del Dragón",   "+25 ATK de Fuego",         ItemType.Weapon,    Rarity.Epic,     200, atk:25, element:ElementType.Fire),
            new Item("Espada Legendaria",  "+30 ATK +20 MaxHP",        ItemType.Weapon,    Rarity.Legendary,250, atk:30, maxHp:20),
            new Item("Arco de Viento",     "+18 ATK +10 CRT",          ItemType.Weapon,    Rarity.Rare,     130, atk:18, crit:10),
            new Item("Cetro del Abismo",   "+22 ATK Oscuro",           ItemType.Weapon,    Rarity.Epic,     180, atk:22, element:ElementType.Dark),
            new Item("Espadón Rúnico",     "+35 ATK",                  ItemType.Weapon,    Rarity.Epic,     220, atk:35),
            new Item("Espada del Destino", "+40 ATK +25 CRT",          ItemType.Weapon,    Rarity.Legendary,500, atk:40, crit:25),
            new Item("Arma Mítica",        "+50 ATK +30 MaxHP",        ItemType.Weapon,    Rarity.Mythic,   999, atk:50, maxHp:30),
            new Item("Escudo de Bronce",   "+5 DEF",                   ItemType.Armor,     Rarity.Common,    30, def:5),
            new Item("Armadura de Hierro", "+12 DEF",                  ItemType.Armor,     Rarity.Uncommon,  70, def:12),
            new Item("Cota de Malla",      "+18 DEF",                  ItemType.Armor,     Rarity.Rare,     110, def:18),
            new Item("Cota Épica",         "+20 DEF +20 MaxHP",        ItemType.Armor,     Rarity.Epic,     160, def:20, maxHp:20),
            new Item("Armadura del Dragón","+30 DEF +30 MaxHP",        ItemType.Armor,     Rarity.Legendary,350, def:30, maxHp:30),
            new Item("Peto Celestial",     "+40 DEF +50 MaxHP",        ItemType.Armor,     Rarity.Mythic,   999, def:40, maxHp:50),
            new Item("Amuleto de Vida",    "+30 MaxHP",                ItemType.Accessory, Rarity.Rare,      90, maxHp:30),
            new Item("Anillo de Maná",     "+30 MaxMP",                ItemType.Accessory, Rarity.Rare,      85, maxMp:30),
            new Item("Botas Veloces",      "+10 VEL",                  ItemType.Accessory, Rarity.Uncommon,  55, spd:10),
            new Item("Guantes del Asesino","+15 CRT",                  ItemType.Accessory, Rarity.Rare,     100, crit:15),
            new Item("Diadema Arcana",     "+40 MaxMP +10 ATK",        ItemType.Accessory, Rarity.Epic,     170, maxMp:40, atk:10),
            new Item("Corazón de Dragón",  "+60 MaxHP +15 DEF",        ItemType.Accessory, Rarity.Legendary,450, maxHp:60, def:15),
            new Item("Ojo del Abismo",     "+20 ATK +20 CRT",          ItemType.Accessory, Rarity.Legendary,400, atk:20, crit:20),
            new Item("Reliquia Ancestral", "+30 todo",                 ItemType.Accessory, Rarity.Mythic,   999, atk:20, def:15, maxHp:50, crit:10),
            new Item("Pergamino de Fuego", "Daño fuego a todos",       ItemType.Scroll,    Rarity.Uncommon,  35),
            new Item("Pergamino de Hielo", "Congela enemigos",         ItemType.Scroll,    Rarity.Uncommon,  35),
            new Item("Pergamino de Rayo",  "Rayo en cadena",           ItemType.Scroll,    Rarity.Rare,      55),
            new Item("Pergamino Épico",    "Hechizo devastador",       ItemType.Scroll,    Rarity.Epic,     120),
            new Item("Pergamino de Cura",  "Cura a todo el grupo",     ItemType.Scroll,    Rarity.Rare,      60, hp:60),
            new Item("Hierba Mágica",      "Material de alquimia",     ItemType.Material,  Rarity.Common,     5),
            new Item("Cristal de Fuego",   "Material encantamiento",   ItemType.Material,  Rarity.Uncommon,  20),
            new Item("Cristal de Magia",   "Material arcano",          ItemType.Material,  Rarity.Uncommon,  25),
            new Item("Metal Élfico",       "Material raro",            ItemType.Material,  Rarity.Rare,      50),
            new Item("Fragmento Sagrado",  "Material divino",          ItemType.Material,  Rarity.Rare,      45),
            new Item("Gema del Abismo",    "Material oscuro",          ItemType.Material,  Rarity.Epic,     100),
            new Item("Escama Ancestral",   "Trofeo de dragón",         ItemType.Material,  Rarity.Legendary,200),
            new Item("Hueso de Liche",     "Material maldito",         ItemType.Material,  Rarity.Rare,      60),
            new Item("Tinta Arcana",       "Para pergaminos avanzados",ItemType.Material,  Rarity.Uncommon,  30),
            new Item("Madera Antigua",     "De un árbol milenario",    ItemType.Material,  Rarity.Uncommon,  20),
            new Item("Pluma de Grifo",     "Ligera como el viento",    ItemType.Material,  Rarity.Rare,      40),
            new Item("Cuero Élfico",       "Resistente y flexible",    ItemType.Material,  Rarity.Rare,      35),
            new Item("Polvo Volcánico",    "Altamente inflamable",     ItemType.Material,  Rarity.Common,    10),
            new Item("Oro Puro",           "Oro refinado al máximo",   ItemType.Material,  Rarity.Epic,     150),
            new Item("Fragmento de Sombra","Oscuridad cristalizada",   ItemType.Material,  Rarity.Rare,      55),
            new Item("Carne Cruda",        "Para cocinar",             ItemType.Material,  Rarity.Common,     3),
            new Item("Hierbas",            "Para pociones básicas",    ItemType.Material,  Rarity.Common,     3),
            new Item("Especias Raras",     "De tierras lejanas",       ItemType.Material,  Rarity.Uncommon,  20),
            new Item("Trufa Mágica",       "Hongo encantado",          ItemType.Material,  Rarity.Rare,      45),
            new Item("Ración de Campaña",  "+25 HP",                   ItemType.Food,      Rarity.Common,     8, hp:25),
            new Item("Pan de Maná",        "+30 MP",                   ItemType.Food,      Rarity.Common,     8, mp:30),
            new Item("Estofado del Héroe", "+60 HP +20 MP",            ItemType.Food,      Rarity.Uncommon,  30, hp:60, mp:20),
            new Item("Festín de Campeón",  "+80 HP +20 MP +5 ATK",     ItemType.Food,      Rarity.Rare,      70, hp:80, mp:20, atk:5),
        };

        public static Item GetRandomDrop(int round)
        {
            var pool = AllDrops.Where(i =>
                (round >= 15 || i.Rarity != Rarity.Mythic) &&
                (round >= 10 || i.Rarity != Rarity.Legendary) &&
                (round >= 5 || (i.Rarity != Rarity.Epic && i.Rarity != Rarity.Legendary)) &&
                i.Type != ItemType.Material
            ).ToList();
            if (round >= 3 && new Random().Next(100) < 20)
                pool.AddRange(AllDrops.Where(i => i.Type == ItemType.Material));
            return pool[new Random().Next(pool.Count)];
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
                new Item("Poción Menor",    "Restaura 30 HP",       ItemType.Potion,   Rarity.Common,   10, hp:30),
                new Item("Poción Mayor",    "Restaura 70 HP",       ItemType.Potion,   Rarity.Uncommon, 25, hp:70),
                new Item("Éter",            "Restaura 40 MP",       ItemType.Potion,   Rarity.Uncommon, 20, mp:40),
                new Item("Elixir",          "+80 HP / +40 MP",      ItemType.Potion,   Rarity.Rare,     50, hp:80, mp:40),
                new Item("Antídoto",        "Cura estados",         ItemType.Potion,   Rarity.Common,   15),
                new Item("Daga de Hierro",  "+5 ATK",               ItemType.Weapon,   Rarity.Common,   30, atk:5),
                new Item("Espada de Acero", "+12 ATK",              ItemType.Weapon,   Rarity.Uncommon, 60, atk:12),
                new Item("Escudo de Bronce","+5 DEF",               ItemType.Armor,    Rarity.Common,   30, def:5),
                new Item("Armadura de Hierro","+12 DEF",            ItemType.Armor,    Rarity.Uncommon, 70, def:12),
                new Item("Amuleto de Vida", "+30 MaxHP",            ItemType.Accessory,Rarity.Rare,     90, maxHp:30),
                new Item("Botas Veloces",   "+10 VEL",              ItemType.Accessory,Rarity.Uncommon, 55, spd:10),
                new Item("Hierba Mágica",   "Material",             ItemType.Material, Rarity.Common,    5),
                new Item("Cristal de Magia","Material arcano",      ItemType.Material, Rarity.Uncommon, 25),
                new Item("Ración de Campaña","+25 HP",              ItemType.Food,     Rarity.Common,    8, hp:25),
            };
            if (round >= 3) { shop.Add(new Item("Poción Superior", "Restaura 120 HP", ItemType.Potion, Rarity.Rare, 50, hp: 120)); shop.Add(new Item("Éter Mayor", "Restaura 80 MP", ItemType.Potion, Rarity.Rare, 40, mp: 80)); shop.Add(new Item("Pergamino de Fuego", "Fuego a todos", ItemType.Scroll, Rarity.Uncommon, 35)); }
            if (round >= 5) { shop.Add(new Item("Hacha Rúnica", "+20 ATK", ItemType.Weapon, Rarity.Rare, 120, atk: 20)); shop.Add(new Item("Cota de Malla", "+18 DEF", ItemType.Armor, Rarity.Rare, 110, def: 18)); shop.Add(new Item("Guantes del Asesino", "+15 CRT", ItemType.Accessory, Rarity.Rare, 100, crit: 15)); }
            if (round >= 8) { shop.Add(new Item("Cota Épica", "+20 DEF +20 HP", ItemType.Armor, Rarity.Epic, 160, def: 20, maxHp: 20)); shop.Add(new Item("Lanza del Dragón", "+25 ATK Fuego", ItemType.Weapon, Rarity.Epic, 200, atk: 25)); shop.Add(new Item("Elixir Supremo", "+150 HP +80 MP", ItemType.Potion, Rarity.Epic, 120, hp: 150, mp: 80)); }
            if (round >= 12) { shop.Add(new Item("Espada Legendaria", "+30 ATK +20 HP", ItemType.Weapon, Rarity.Legendary, 250, atk: 30, maxHp: 20)); shop.Add(new Item("Armadura del Dragón", "+30 DEF +30 HP", ItemType.Armor, Rarity.Legendary, 350, def: 30, maxHp: 30)); shop.Add(new Item("Corazón de Dragón", "+60 MaxHP +15 DEF", ItemType.Accessory, Rarity.Legendary, 450, maxHp: 60, def: 15)); }
            if (round >= 18) { shop.Add(new Item("Arma Mítica", "+50 ATK +30 HP", ItemType.Weapon, Rarity.Mythic, 999, atk: 50, maxHp: 30)); shop.Add(new Item("Peto Celestial", "+40 DEF +50 HP", ItemType.Armor, Rarity.Mythic, 999, def: 40, maxHp: 50)); shop.Add(new Item("Reliquia Ancestral", "+30 todo", ItemType.Accessory, Rarity.Mythic, 999, atk: 20, def: 15, maxHp: 50, crit: 10)); }
            return shop.OrderBy(_ => R.Next()).Take(Math.Min(shop.Count, 12)).ToList();
        }
    }

    // ═══════════════════════════════════════════════
    //  CHARACTER BASE
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

        private readonly Dictionary<StatusEffect, int> _dur = new();
        private readonly Dictionary<StatusEffect, int> _stk = new();

        protected Character(string name, int hp, int mp, int def = 0, int spd = 10)
        { Name = name; MaxHealth = hp; Health = hp; Mana = mp; Defense = def; Speed = spd; }

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
            if (_dur.ContainsKey(effect))
            {
                _dur[effect] = Math.Max(_dur[effect], turns);
                _stk[effect] = Math.Min((_stk.GetValueOrDefault(effect, 1) + stacks), 5);
            }
            else { _dur[effect] = turns; _stk[effect] = stacks; }
        }

        public bool HasStatus(StatusEffect e) => _dur.ContainsKey(e) && _dur[e] > 0;
        public bool HasAnyStatus() => _dur.Any(kv => kv.Value > 0);
        public string GetStatusString() =>
            string.Join(",", _dur.Where(kv => kv.Value > 0).Select(kv => kv.Key.ToString()[..Math.Min(3, kv.Key.ToString().Length)]));

        public void TickStatus()
        {
            foreach (var k in _dur.Keys.ToList())
            {
                if (_dur[k] <= 0) continue;
                int stk = _stk.GetValueOrDefault(k, 1);
                switch (k)
                {
                    case StatusEffect.Poisoned: { int d = 8 * stk; Health = Math.Max(1, Health - d); SoundSystem.PlayStatusTick(k); break; }
                    case StatusEffect.Burned: { int d = 6 * stk; Health = Math.Max(1, Health - d); SoundSystem.PlayStatusTick(k); break; }
                    case StatusEffect.Bleeding: { int d = 10 * stk; Health = Math.Max(1, Health - d); SoundSystem.PlayStatusTick(k); break; }
                    case StatusEffect.Regenerating: { Heal(15 * stk); SoundSystem.PlayStatusTick(k); break; }
                    case StatusEffect.Doomed: { if (_dur[k] == 1) Health = 0; break; }
                }
                _dur[k]--;
            }
        }

        public void ClearStatus() => _dur.Clear();
        public void ClearNegativeStatus()
        {
            foreach (var s in new[]{ StatusEffect.Poisoned,StatusEffect.Burned,StatusEffect.Stunned,
                StatusEffect.Bleeding,StatusEffect.Frozen,StatusEffect.Cursed,StatusEffect.Silenced,
                StatusEffect.Weakened,StatusEffect.Confused,StatusEffect.Petrified,StatusEffect.Doomed })
                if (_dur.ContainsKey(s)) _dur[s] = 0;
        }
        public int GetStatusCount() => _dur.Count(kv => kv.Value > 0);
    }

    // ═══════════════════════════════════════════════
    //  PLAYER BASE
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
        public int SpecialCost2 { get; protected set; } = 25;
        public int RoundsWon { get; set; }
        public int TotalKills { get; set; }
        public int TalentPoints { get; set; }
        public bool HasUsedRevive { get; set; } = false;
        public string ClassName { get; protected set; } = "Aventurero";
        public string SpecialName { get; protected set; } = "Especial";
        public string Special2Name { get; protected set; } = "Habilidad 2";
        public ElementType AttackElement { get; protected set; } = ElementType.Physical;
        public List<Item> Inventory { get; } = new();
        public TalentSystem Talents { get; } = new();
        public List<Quest> ActiveQuests { get; } = new();
        public Item? EquippedWeapon { get; private set; }
        public Item? EquippedArmor { get; private set; }
        public Item? EquippedAccessory { get; private set; }
        public Dictionary<ElementType, int> ElementResistances { get; } = new();
        public Companion? Pet { get; set; }

        static readonly Random R = new Random();

        protected Player(string name, int hp, int mp, int baseAtk, int def = 0, int spd = 10)
            : base(name, hp, mp, def, spd) { BaseAttack = baseAtk; }

        public void Tick()
        {
            Mana = Math.Min(Mana + 4 + (Level / 5), 200);
            Defending = false;
            TickStatus();
            Pet?.Tick(this);
        }

        public int Attack()
        {
            int weatherAtk = WorldTime.GetWeatherEffects().atkMod;
            return Math.Max(1, BaseAttack + AttackBonus + R.Next(-3, 4) + weatherAtk);
        }

        public override void TakeDamage(int dmg, ElementType element = ElementType.Physical)
        {
            if (ElementResistances.TryGetValue(element, out int resist)) dmg = (int)(dmg * (1 - resist / 100f));
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
                XP -= Level * 50; Level++;
                MaxHealth += 15; Health = MaxHealth;
                Mana = Math.Min(Mana + 10, 200); BaseAttack += 3; Defense += 1; Speed += 1;
                TalentPoints++; Talents.AvailablePoints++;
                SoundSystem.PlayLevelUp();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n  🎉 ¡{Name} subió a Nivel {Level}! HP:{MaxHealth} ATK:{BaseAttack + AttackBonus} +1 Talento");
                Console.ResetColor();
                OnLevelUp();
            }
        }

        protected virtual void OnLevelUp() { }
        public void DeathMessage() { SoundSystem.PlayPlayerDeath(); Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"\n  💀 {Name} ha caído en batalla..."); Console.ResetColor(); }

        public void OpenInventory()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  🎒 INVENTARIO DE {Name.ToUpper()}  (Oro: {Gold}🪙  |  {Inventory.Count}/{GameConfig.MAX_INVENTORY})");
                Console.ResetColor();

                if (!Inventory.Any()) { Console.WriteLine("\n  (Sin objetos)"); Console.ReadKey(true); return; }

                var groups = Inventory.GroupBy(i => i.Type).OrderBy(g => g.Key);
                int idx = 1;
                var map = new Dictionary<int, Item>();
                foreach (var g in groups)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"\n  ─── {g.Key} ───");
                    Console.ResetColor();
                    foreach (var item in g)
                    {
                        Console.ForegroundColor = item.GetRarityColor();
                        Console.WriteLine($"  {idx,2}. {item.GetRarityStars()} [{item.Rarity}] {item.Name,-28} — {item.Description}");
                        Console.ResetColor();
                        map[idx++] = item;
                    }
                }

                Console.WriteLine("\n  0. Volver  |  E. Equipar  |  D. Descartar");
                Console.Write("  Usar #: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") { open = false; }
                else if (op.ToUpper() == "E") { EquipmentMenu(); }
                else if (op.ToUpper() == "D")
                {
                    Console.Write("  Descartar #: ");
                    if (int.TryParse(Console.ReadLine(), out int di) && map.ContainsKey(di))
                    {
                        var d = map[di]; int sp = d.Price / 4;
                        Inventory.Remove(d); Gold += sp;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Descartaste {d.Name}. +{sp}🪙");
                        Console.ResetColor(); Console.ReadKey(true);
                    }
                }
                else if (int.TryParse(op, out int i2) && map.ContainsKey(i2))
                { UseItem(map[i2]); open = false; }
            }
        }

        public void EquipmentMenu()
        {
            Console.Clear();
            Program.PrintBox("⚔ EQUIPAMIENTO", ConsoleColor.Yellow);
            Console.WriteLine($"\n  Arma:      {(EquippedWeapon != null ? $"[{EquippedWeapon.Rarity}] {EquippedWeapon.Name}" : "Sin equipar")}");
            Console.WriteLine($"  Armadura:  {(EquippedArmor != null ? $"[{EquippedArmor.Rarity}] {EquippedArmor.Name}" : "Sin equipar")}");
            Console.WriteLine($"  Accesorio: {(EquippedAccessory != null ? $"[{EquippedAccessory.Rarity}] {EquippedAccessory.Name}" : "Sin equipar")}");
            var eq = Inventory.Where(i => i.Type == ItemType.Weapon || i.Type == ItemType.Armor || i.Type == ItemType.Accessory).ToList();
            if (!eq.Any()) { Console.WriteLine("  (No hay equipables)"); Console.ReadKey(true); return; }
            for (int i = 0; i < eq.Count; i++)
            {
                Console.ForegroundColor = eq[i].GetRarityColor();
                Console.WriteLine($"  {i + 1}. [{eq[i].Rarity}] {eq[i].Name} — {eq[i].Description}");
                Console.ResetColor();
            }
            Console.Write("\n  Equipar #: ");
            if (int.TryParse(Console.ReadLine(), out int ch) && ch >= 1 && ch <= eq.Count) EquipItem(eq[ch - 1]);
        }

        void EquipItem(Item item)
        {
            if (item.Type == ItemType.Weapon && EquippedWeapon != null) UnequipItem(EquippedWeapon);
            else if (item.Type == ItemType.Armor && EquippedArmor != null) UnequipItem(EquippedArmor);
            else if (item.Type == ItemType.Accessory && EquippedAccessory != null) UnequipItem(EquippedAccessory);
            AttackBonus += item.AtkBonus; Defense += item.DefBonus;
            MaxHealth += item.MaxHPBonus; Health = Math.Min(Health + item.MaxHPBonus, MaxHealth);
            CritChance += item.CritBonus; Speed += item.SpdBonus;
            if (item.GrantsElement.HasValue) AttackElement = item.GrantsElement.Value;
            if (item.Type == ItemType.Weapon) EquippedWeapon = item;
            else if (item.Type == ItemType.Armor) EquippedArmor = item;
            else EquippedAccessory = item;
            SoundSystem.PlayItemBuy();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ Equipaste: {item.Name}");
            Console.ResetColor(); Console.ReadKey(true);
        }

        void UnequipItem(Item item)
        {
            AttackBonus -= item.AtkBonus; Defense -= item.DefBonus;
            MaxHealth -= item.MaxHPBonus; Health = Math.Min(Health, MaxHealth);
            CritChance -= item.CritBonus; Speed -= item.SpdBonus;
            if (item.Type == ItemType.Weapon) EquippedWeapon = null;
            else if (item.Type == ItemType.Armor) EquippedArmor = null;
            else EquippedAccessory = null;
        }

        public void UseItem(Item item)
        {
            GlobalStats.TotalItemsUsed++;
            SoundSystem.PlayItemUse();
            bool consumed = false;
            if (item.HPRestore > 0) { Heal(item.HPRestore); Console.WriteLine($"  ✔ +{item.HPRestore} HP."); SoundSystem.PlayHeal(); consumed = item.IsConsumable; }
            if (item.MPRestore > 0) { Mana = Math.Min(Mana + item.MPRestore, 200); Console.WriteLine($"  ✔ +{item.MPRestore} MP."); consumed = item.IsConsumable; }
            if (item.AtkBonus > 0 && !consumed) { AttackBonus += item.AtkBonus; Console.WriteLine($"  ✔ ATK +{item.AtkBonus}."); }
            if (item.DefBonus > 0 && !consumed) { Defense += item.DefBonus; Console.WriteLine($"  ✔ DEF +{item.DefBonus}."); }
            if (item.MaxHPBonus > 0 && !consumed) { MaxHealth += item.MaxHPBonus; Health = Math.Min(Health + item.MaxHPBonus, MaxHealth); Console.WriteLine($"  ✔ MaxHP +{item.MaxHPBonus}."); }
            if (item.MaxMPBonus > 0 && !consumed) { Mana = Math.Min(Mana + item.MaxMPBonus, 200); Console.WriteLine($"  ✔ MaxMP +{item.MaxMPBonus}."); }
            if (item.CritBonus > 0 && !consumed) { CritChance += item.CritBonus; Console.WriteLine($"  ✔ Crítico +{item.CritBonus}%."); }
            if (item.SpdBonus > 0 && !consumed) { Speed += item.SpdBonus; Console.WriteLine($"  ✔ Velocidad +{item.SpdBonus}."); }
            if (item.GrantsStatus.HasValue) { ApplyStatus(item.GrantsStatus.Value, 3); Console.WriteLine($"  ✔ Estado: {item.GrantsStatus.Value}."); consumed = item.IsConsumable; }
            if (item.Type == ItemType.Scroll) { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  📜 {item.Name} usado!"); Console.ResetColor(); consumed = true; }
            if (item.Name.Contains("Antídoto")) { ClearNegativeStatus(); Console.WriteLine("  ✔ Estados negativos curados."); consumed = true; }
            if (consumed) Inventory.Remove(item);
            Console.ReadKey(true);
        }

        public void OpenCrafting()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Program.PrintBox($"⚗ CRAFTEO — {Name}", ConsoleColor.Green);
                Console.WriteLine($"  Oro: {Gold}🪙  |  Nivel: {Level}\n");
                var recipes = CraftingDatabase.AllRecipes;
                for (int i = 0; i < recipes.Count; i++)
                {
                    bool can = recipes[i].CanCraft(this);
                    Console.ForegroundColor = can ? ConsoleColor.Green : ConsoleColor.DarkGray;
                    Console.WriteLine($"  {i + 1,2}. [{recipes[i].ResultRarity}] {recipes[i].ResultItemName,-30} {recipes[i].GoldCost}🪙 Lv{recipes[i].LevelRequired}");
                    Console.WriteLine($"      Ing: {recipes[i].GetIngredientsList()}  |  {recipes[i].Description}");
                    Console.ResetColor();
                }
                Console.WriteLine("\n  0. Salir");
                Console.Write("  Fabricar #: ");
                string op = Console.ReadLine() ?? "0";
                if (op == "0") { open = false; break; }
                if (int.TryParse(op, out int idx) && idx >= 1 && idx <= recipes.Count)
                {
                    var r = recipes[idx - 1];
                    if (!r.CanCraft(this)) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ No puedes fabricar. Revisa nivel, oro e ingredientes."); Console.ResetColor(); Console.ReadKey(true); }
                    else
                    {
                        var result = CraftingDatabase.CraftItem(r, this);
                        if (result != null && Inventory.Count < GameConfig.MAX_INVENTORY)
                        {
                            Inventory.Add(result);
                            Console.ForegroundColor = result.GetRarityColor();
                            Console.WriteLine($"\n  ✔ ¡Fabricaste: [{result.Rarity}] {result.Name}!");
                            Console.ResetColor();
                        }
                        else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ Inventario lleno."); Console.ResetColor(); }
                        Console.ReadKey(true);
                    }
                }
            }
        }

        public void ShowQuests()
        {
            Console.Clear();
            Program.PrintBox($"📜 MISIONES DE {Name.ToUpper()}", ConsoleColor.Yellow);
            if (!ActiveQuests.Any()) { Console.WriteLine("\n  Sin misiones activas."); Console.ReadKey(true); return; }
            Console.WriteLine();
            foreach (var q in ActiveQuests) { q.ShowInfo(); Console.WriteLine(); }
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
                    Gold += q.GoldReward; GainXP(q.XPReward); GlobalStats.TotalQuestsCompleted++;
                    SoundSystem.PlayQuestComplete();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  📜 ¡MISIÓN COMPLETADA: {q.Name}! +{q.GoldReward}🪙 +{q.XPReward}XP");
                    Console.ResetColor(); Console.ReadKey(true);
                }
            }
        }

        public void AdoptCompanion()
        {
            if (Pet != null) { Console.WriteLine($"\n  Ya tienes a {Pet.Name}."); Console.ReadKey(true); return; }
            var companions = Companion.GetAvailable();
            Console.Clear(); Program.PrintBox("🐾 ADOPTAR COMPAÑERO", ConsoleColor.Cyan); Console.WriteLine();
            for (int i = 0; i < companions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  {i + 1}. {companions[i].Icon} {companions[i].Name}: {companions[i].Description}  ({companions[i].Cost}🪙)");
                Console.ResetColor();
            }
            Console.WriteLine("  0. Cancelar"); Console.Write("  → ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= companions.Count)
            {
                var c = companions[idx - 1];
                if (Gold >= c.Cost) { Gold -= c.Cost; Pet = c; Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  ✔ ¡{c.Name} se une a ti!"); Console.ResetColor(); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ Oro insuficiente."); Console.ResetColor(); }
                Console.ReadKey(true);
            }
        }

        public virtual void PassiveAction(List<Player> allies, List<Enemy> enemies) { Console.WriteLine($"\n  {Name} no tiene habilidad extra."); }
        public virtual void Special2(List<Player> allies, List<Enemy> enemies) { Console.WriteLine($"\n  {Name} no tiene segunda habilidad."); }
        public abstract void Special(List<Enemy> enemies);
    }

    // ═══════════════════════════════════════════════
    //  COMPAÑEROS
    // ═══════════════════════════════════════════════

    class Companion
    {
        public string Name { get; }
        public string Icon { get; }
        public string Description { get; }
        public int Cost { get; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; }
        static readonly Random R = new();
        readonly Func<Player, List<Enemy>, string> _action;

        public Companion(string name, string icon, string desc, int cost, Func<Player, List<Enemy>, string> action)
        { Name = name; Icon = icon; Description = desc; Cost = cost; _action = action; }

        public void Tick(Player owner) { }
        public string ActInCombat(Player owner, List<Enemy> enemies) => _action(owner, enemies);
        public void GainExp(int exp) { Experience += exp; if (Experience >= Level * 30) { Experience -= Level * 30; Level++; } }

        public static List<Companion> GetAvailable() => new()
        {
            new("Dragoncito","🐉","Lanza fuego (+15 daño/turno a un enemigo)",200,
                (o,e)=>{ if(!e.Any()) return ""; var t=e[R.Next(e.Count)]; int d=15+o.Level; t.TakeDamage(d,ElementType.Fire); return $"  🐉 Dragoncito quema a {t.Name} por {d}!"; }),
            new("Zorrito Lunar","🦊","Regenera 8 HP al dueño/turno",120,
                (o,e)=>{ o.Heal(8); return $"  🦊 Zorrito cura 8 HP a {o.Name}."; }),
            new("Búho Arcano","🦉","Regenera 5 MP/turno",100,
                (o,e)=>{ o.Mana=Math.Min(o.Mana+5,200); return $"  🦉 Búho +5 MP a {o.Name}."; }),
            new("Lobo de Combate","🐺","25% de atacar junto contigo (+20 daño)",180,
                (o,e)=>{ if(!e.Any()||R.Next(100)>=25) return ""; var t=e.OrderBy(x=>x.Health).First(); int d=20+o.Level*2; t.TakeDamage(d,ElementType.Physical); return $"  🐺 Lobo ataca a {t.Name} por {d}!"; }),
            new("Hada Bendita","🧚","Aplica Bendición al dueño cada turno",150,
                (o,e)=>{ o.ApplyStatus(StatusEffect.Blessed,2); return $"  🧚 Hada bendice a {o.Name}!"; }),
        };
    }

    // ═══════════════════════════════════════════════
    //  ENEMIES
    // ═══════════════════════════════════════════════

    class Enemy : Character
    {
        protected int BaseAtk;
        public int GoldDrop { get; protected set; }
        public int XPDrop { get; protected set; }
        public ElementType WeakTo { get; protected set; } = ElementType.Physical;
        public ElementType ResistTo { get; protected set; } = ElementType.Physical;
        static readonly Random R = new();

        public Enemy(string name, int hp, int atk, int gold = 5, int xp = 10)
            : base(name, hp, 0) { BaseAtk = atk; GoldDrop = gold; XPDrop = xp; }

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
        static readonly Random R = new();
        public EliteEnemy(string name, int hp, int atk) : base(name, hp, atk, 15, 25)
        { Defense = 5; WeakTo = (ElementType)R.Next(Enum.GetValues<ElementType>().Length); }
        public override int Attack() => base.Attack() + 5;
        public override string GetSpecialAttackName() => "Ataque Élite";
    }

    class BossEnemy : Enemy
    {
        static readonly Random R = new();
        protected int phase = 1;
        bool phaseAnnounced = false;
        public BossEnemy(string name, int hp, int atk) : base(name, hp, atk, 80, 200) { Defense = 10; }
        public override int Attack()
        {
            if (Health < MaxHealth * 0.5f && phase == 1)
            {
                phase = 2; if (!phaseAnnounced)
                {
                    phaseAnnounced = true;
                    SoundSystem.PlayBossAlert();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\n  ⚠  ¡{Name} entra en FASE 2!");
                    Console.ResetColor(); BaseAtk = (int)(BaseAtk * 1.3f); Defense += 5; ApplyStatus(StatusEffect.Enraged, 10);
                }
            }
            return base.Attack() * phase + R.Next(0, 10);
        }
        public override string GetSpecialAttackName() => "Golpe Aplastante";
    }

    class LegendaryBoss : BossEnemy
    {
        static readonly Random R = new();
        int specialCooldown = 0;
        public LegendaryBoss(string name, int hp, int atk) : base(name, hp, atk) { Defense = 20; GoldDrop = 300; XPDrop = 800; }
        public override int Attack()
        {
            specialCooldown--;
            if (specialCooldown <= 0) { specialCooldown = 3; Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n  ⚡ ¡{Name} prepara un ATAQUE ESPECIAL!"); Console.ResetColor(); return base.Attack() + R.Next(20, 40); }
            return base.Attack() + R.Next(10, 25);
        }
        public override string GetSpecialAttackName() => "Rugido Ancestral";
    }

    class MythicBoss : LegendaryBoss
    {
        static readonly Random R = new();
        int turnCount = 0;
        public MythicBoss(string name, int hp, int atk) : base(name, hp, atk) { Defense = 30; GoldDrop = 600; XPDrop = 2000; }
        public override int Attack()
        {
            turnCount++;
            int b = base.Attack();
            if (turnCount % 4 == 0) { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ☠  ¡{Name} libera el PODER DEL ABISMO!"); Console.ResetColor(); return b + R.Next(40, 70); }
            return b;
        }
        public override string GetSpecialAttackName() => "Furia del Abismo";
    }

    // ═══════════════════════════════════════════════
    //  CLASES DE JUGADOR (15 clases completas)
    // ═══════════════════════════════════════════════

    class Warrior : Player
    {
        int rageStacks = 0; bool warMode = false; static readonly Random R = new();
        public Warrior(string n) : base(n, 200, 40, 22, def: 14, spd: 8) { ClassName = "Guerrero"; SpecialName = "Golpe Devastador"; Special2Name = "Grito de Batalla"; SpecialCost = 15; SpecialCost2 = 20; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var t = enemies.OrderByDescending(e => e.Health).First();
            int dmg = (int)((BaseAttack + AttackBonus) * 2.5) + rageStacks * 8;
            if (warMode) dmg = (int)(dmg * 1.4f);
            t.TakeDamage(dmg, ElementType.Physical); GlobalStats.TotalDamageDealt += dmg;
            if (dmg > GlobalStats.HighestDamageInOneTurn) { GlobalStats.HighestDamageInOneTurn = dmg; GlobalStats.HighestDamageDealer = Name; }
            Heal(dmg / 10); rageStacks = 0;
            SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ⚔  {Name} GOLPE DEVASTADOR → {t.Name} por {dmg}! (Roba {dmg / 10} HP)");
            Console.ResetColor();
            if (R.Next(100) < 20) { t.ApplyStatus(StatusEffect.Stunned, 1); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"  ¡{t.Name} queda aturdido!"); Console.ResetColor(); }
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n  ✘ Sin maná."); Console.ResetColor(); return; }
            Mana -= SpecialCost2; warMode = true; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n  📢 {Name} GRITO DE BATALLA!");
            foreach (var p in allies.Where(p => p.IsAlive())) { p.AttackBonus += 8; p.ApplyStatus(StatusEffect.Enraged, 3); Console.WriteLine($"  → {p.Name}: +8 ATK"); }
            Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            rageStacks = Math.Min(rageStacks + 4, 20); ApplyStatus(StatusEffect.Enraged, 2);
            Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"\n  😤 {Name} acumula RABIA ({rageStacks} stacks)."); Console.ResetColor();
        }
        protected override void OnLevelUp() { MaxHealth += 10; Defense += 2; }
    }

    class Archer : Player
    {
        bool multishot = false; int poisonArrows = 0; static readonly Random R = new();
        public Archer(string n) : base(n, 115, 65, 18, def: 5, spd: 16) { ClassName = "Arquero"; SpecialName = "Lluvia de Flechas"; Special2Name = "Flecha Envenenada"; SpecialCost = 20; SpecialCost2 = 15; CritChance = 28; AttackElement = ElementType.Wind; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  🏹 {Name} LLUVIA DE FLECHAS!");
            int total = 0;
            foreach (var e in enemies) { int hits = multishot ? 2 : 1; for (int h = 0; h < hits; h++) { int dmg = (BaseAttack + AttackBonus) + R.Next(5, 20); bool crit = R.Next(100) < CritChance; if (crit) dmg = (int)(dmg * 1.8f); e.TakeDamage(dmg, ElementType.Wind); total += dmg; Console.WriteLine($"     → {e.Name}: {dmg}{(crit ? " 💥" : "")}"); } if (poisonArrows > 0) { e.ApplyStatus(StatusEffect.Poisoned, 3); poisonArrows--; } }
            GlobalStats.TotalDamageDealt += total; Console.ResetColor(); multishot = false;
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2; poisonArrows = 3; SoundSystem.PlaySpecial();
            var t = enemies.OrderBy(e => e.Health).First(); int dmg = (int)((BaseAttack + AttackBonus) * 1.5f) + R.Next(10, 25); t.TakeDamage(dmg, ElementType.Poison); t.ApplyStatus(StatusEffect.Poisoned, 4); t.ApplyStatus(StatusEffect.Bleeding, 2);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  🏹 FLECHA ENVENENADA → {t.Name} por {dmg}! (Veneno+Sangrado)"); Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { multishot = !multishot; Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine(multishot ? $"\n  🏹 {Name} DISPARO MÚLTIPLE: x2 flechas." : $"\n  🏹 {Name} desactiva disparo múltiple."); Console.ResetColor(); }
        protected override void OnLevelUp() { CritChance += 2; Speed += 1; }
    }

    class Mage : Player
    {
        int spellCharge = 0; ElementType currentElement = ElementType.Fire; bool overloadReady = false; static readonly Random R = new();
        public Mage(string n) : base(n, 88, 140, 12, def: 3, spd: 10) { ClassName = "Mago"; SpecialName = "Tormenta Arcana"; Special2Name = "Sobrecargar"; SpecialCost = 25; SpecialCost2 = 30; CritChance = 18; AttackElement = ElementType.Fire; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int bonus = spellCharge * 10; float mult = overloadReady ? 1.8f : 1.0f; spellCharge = 0; overloadReady = false;
            SoundSystem.PlayElemental(currentElement);
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  🔥 {Name} TORMENTA ARCANA [{currentElement}]!");
            int total = 0;
            foreach (var e in enemies) { int dmg = (int)(((BaseAttack + AttackBonus) * 1.9f + bonus) * mult) + R.Next(10, 30); bool crit = R.Next(100) < CritChance; if (crit) dmg = (int)(dmg * 1.8f); e.TakeDamage(dmg, currentElement); total += dmg; switch (currentElement) { case ElementType.Fire: e.ApplyStatus(StatusEffect.Burned, 2); break; case ElementType.Ice: e.ApplyStatus(StatusEffect.Frozen, 1); break; case ElementType.Lightning: e.ApplyStatus(StatusEffect.Stunned, 1); break; case ElementType.Poison: e.ApplyStatus(StatusEffect.Poisoned, 3); break; } Console.WriteLine($"     → {e.Name}: {dmg}{(crit ? " 💥" : "")}"); }
            GlobalStats.TotalDamageDealt += total; if (total > GlobalStats.HighestDamageInOneTurn) { GlobalStats.HighestDamageInOneTurn = total; GlobalStats.HighestDamageDealer = Name; }
            Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; overloadReady = true; spellCharge = Math.Min(spellCharge + 5, 10); Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine($"\n  ⚡ {Name} SOBRECARGA. x1.8 próximo hechizo!"); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { spellCharge = Math.Min(spellCharge + 2, 10); var els = new[] { ElementType.Fire, ElementType.Ice, ElementType.Lightning, ElementType.Arcane, ElementType.Poison }; currentElement = els[R.Next(els.Length)]; AttackElement = currentElement; Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine($"\n  🔮 {Name} carga [{currentElement}]. Carga:{spellCharge}/10"); Console.ResetColor(); }
        protected override void OnLevelUp() { Mana += 15; BaseAttack += 2; }
    }

    class Healer : Player
    {
        int holyCharge = 0; static readonly Random R = new();
        public Healer(string n) : base(n, 125, 120, 9, def: 7, spd: 9) { ClassName = "Curandero"; SpecialName = "Luz Sagrada"; Special2Name = "Resurrección"; SpecialCost = 20; SpecialCost2 = 50; AttackElement = ElementType.Holy; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int bonus = holyCharge * 8; holyCharge = 0; SoundSystem.PlayHeal();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  ✨ {Name} LUZ SAGRADA!");
            foreach (var p in Program.Players.Where(p => p.IsAlive())) { int h = 50 + R.Next(15) + bonus; p.Heal(h); p.ApplyStatus(StatusEffect.Regenerating, 2); Console.WriteLine($"  ✔ {p.Name} +{h} HP + Regen."); }
            var boss = enemies.OrderByDescending(e => e.MaxHealth).First(); int dmg = (int)((BaseAttack + AttackBonus) * 1.5f) + bonus / 2; boss.TakeDamage(dmg, ElementType.Holy); Console.WriteLine($"  ✝ {boss.Name} recibe {dmg}."); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            var dead = allies.FirstOrDefault(p => !p.IsAlive()); if (dead == null) { Console.WriteLine("\n  ✘ No hay caídos."); return; }
            Mana -= SpecialCost2; dead.Health = (int)(dead.MaxHealth * 0.4f); SoundSystem.PlayRevive();
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ✨ {Name} RESURRECCIÓN: {dead.Name} revive con {dead.Health} HP!"); Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies)
        {
            holyCharge = Math.Min(holyCharge + 3, 15);
            var hurt = allies.Where(p => p.IsAlive() && p.HasAnyStatus()).FirstOrDefault();
            if (hurt != null) { hurt.ClearNegativeStatus(); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  🌟 {Name} purifica a {hurt.Name}."); }
            else { int h = 15 + Level * 2; foreach (var p in allies.Where(p => p.IsAlive())) p.Heal(h); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine($"\n  🌿 AURA SANADORA: todos +{15 + Level * 2} HP."); }
            Console.ResetColor();
        }
        protected override void OnLevelUp() { MaxHealth += 5; Mana += 10; }
    }

    class Thief : Player
    {
        bool stealthed = false; int comboKills = 0; int poisonBlades = 0; static readonly Random R = new();
        public Thief(string n) : base(n, 98, 75, 20, def: 4, spd: 22) { ClassName = "Ladrón"; SpecialName = "Golpe Bajo"; Special2Name = "Hojas Venenosas"; SpecialCost = 12; SpecialCost2 = 18; CritChance = 32; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost;
            var t = enemies.OrderBy(e => e.Health).First();
            float mult = stealthed ? 4.0f : 2.5f; if (comboKills > 0) mult += comboKills * 0.3f;
            int dmg = (int)((BaseAttack + AttackBonus) * mult); bool crit = R.Next(100) < CritChance + (stealthed ? 30 : 0);
            if (crit) { dmg = (int)(dmg * 1.8f); GlobalStats.TotalCriticalHits++; }
            t.TakeDamage(dmg, ElementType.Physical); GlobalStats.TotalDamageDealt += dmg;
            int stolen = R.Next(5, 20) + comboKills * 3; Gold += stolen; GlobalStats.TotalGoldEarned += stolen;
            SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n  🗡  {Name} GOLPE BAJO ({(stealthed ? "SIGILO x4" : "x2.5")}) → {t.Name}: {dmg}{(crit ? " 💥" : "")} Roba {stolen}🪙");
            Console.ResetColor();
            if (!t.IsAlive()) comboKills++; stealthed = false;
            if (poisonBlades > 0) { t.ApplyStatus(StatusEffect.Poisoned, 3); poisonBlades--; }
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2; poisonBlades = 5; SoundSystem.PlaySpecial();
            foreach (var e in enemies) { int dmg = (int)((BaseAttack + AttackBonus) * 0.8f) + R.Next(10, 20); e.TakeDamage(dmg, ElementType.Poison); e.ApplyStatus(StatusEffect.Poisoned, 4); e.ApplyStatus(StatusEffect.Bleeding, 2); }
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine($"\n  🗡  {Name} HOJAS VENENOSAS a todos! (Veneno+Sangrado)"); Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { stealthed = true; ApplyStatus(StatusEffect.Invisible, 2); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"\n  👤 {Name} entra en SIGILO. Combo:{comboKills}"); Console.ResetColor(); }
        protected override void OnLevelUp() { CritChance += 3; Speed += 2; }
    }

    class Monk : Player
    {
        int comboCount = 0; bool chiActive = false; int meditationStacks = 0; static readonly Random R = new();
        public Monk(string n) : base(n, 135, 55, 19, def: 10, spd: 18) { ClassName = "Monje"; SpecialName = "Ráfaga de Golpes"; Special2Name = "Chi Dragón"; SpecialCost = 15; SpecialCost2 = 25; CritChance = 20; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; var t = enemies.OrderByDescending(e => e.Health).First();
            int hits = 3 + comboCount / 2 + (chiActive ? 3 : 0); int total = 0;
            SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  👊 {Name} RÁFAGA DE GOLPES ({hits} impactos):");
            for (int i = 0; i < hits; i++) { int dmg = (BaseAttack + AttackBonus) / 2 + R.Next(8, 22); bool crit = R.Next(100) < CritChance; if (crit) { dmg = (int)(dmg * 1.8f); GlobalStats.TotalCriticalHits++; } if (chiActive) dmg = (int)(dmg * 1.5f); t.TakeDamage(dmg, ElementType.Physical); total += dmg; Console.Write($" {dmg}{(crit ? "!" : "")}"); }
            Console.WriteLine($"\n  Total:{total}"); GlobalStats.TotalDamageDealt += total; Console.ResetColor(); comboCount = Math.Min(comboCount + 1, 12); chiActive = false;
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; chiActive = true; meditationStacks += 2; ApplyStatus(StatusEffect.Empowered, 2); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  🌀 {Name} CHI DEL DRAGÓN. Próxima Ráfaga: x1.5 +3 golpes!"); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { int hp = 25 + Level * 3 + meditationStacks * 5; int mp = 20 + meditationStacks * 3; Heal(hp); Mana = Math.Min(Mana + mp, 200); meditationStacks = Math.Min(meditationStacks + 1, 10); SoundSystem.PlayHeal(); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  🧘 {Name} medita. +{hp} HP, +{mp} MP. Combo:{comboCount}"); Console.ResetColor(); }
        protected override void OnLevelUp() { Defense += 2; Speed += 1; }
    }

    class Bard : Player
    {
        int melodyCharge = 0; static readonly Random R = new();
        public Bard(string n) : base(n, 108, 95, 12, def: 5, spd: 14) { ClassName = "Bardo"; SpecialName = "Canción de Batalla"; Special2Name = "Melodía del Caos"; SpecialCost = 20; SpecialCost2 = 30; AttackElement = ElementType.Wind; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int bAtk = 6 + melodyCharge * 2; int bHeal = 20 + melodyCharge * 5; melodyCharge = 0; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  🎵 {Name} CANCIÓN DE BATALLA! (+{bAtk} ATK, +{bHeal} HP)");
            foreach (var p in Program.Players.Where(p => p.IsAlive())) { p.AttackBonus += bAtk; p.Heal(bHeal); p.ApplyStatus(StatusEffect.Hasted, 2); Console.WriteLine($"     → {p.Name}: +{bAtk} ATK, +{bHeal} HP"); }
            foreach (var e in enemies) { int dmg = (BaseAttack + AttackBonus) + R.Next(8, 20); e.TakeDamage(dmg, ElementType.Wind); if (R.Next(100) < 40) e.ApplyStatus(StatusEffect.Stunned, 1); }
            Console.WriteLine("  🎵 Enemigos afectados."); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"\n  🎶 {Name} MELODÍA DEL CAOS!");
            foreach (var e in enemies) { var efs = new[] { StatusEffect.Confused, StatusEffect.Weakened, StatusEffect.Cursed, StatusEffect.Bleeding }; e.ApplyStatus(efs[R.Next(efs.Length)], 3); int dmg = (int)((BaseAttack + AttackBonus) * 1.2f) + R.Next(5, 15); e.TakeDamage(dmg, ElementType.Wind); Console.WriteLine($"  → {e.Name}: {dmg} daño + efecto"); }
            foreach (var p in allies.Where(p => p.IsAlive())) { var buf = new[] { StatusEffect.Blessed, StatusEffect.Enraged, StatusEffect.Regenerating, StatusEffect.Hasted }; p.ApplyStatus(buf[R.Next(buf.Length)], 3); }
            Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { melodyCharge = Math.Min(melodyCharge + 2, 10); int xp = 15 + melodyCharge * 2; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  🎶 {Name} BALADA DE INSPIRACIÓN. +{xp} XP a todos."); foreach (var p in allies.Where(p => p.IsAlive())) p.GainXP(xp); Console.ResetColor(); }
        protected override void OnLevelUp() { Mana += 8; Speed += 1; }
    }

    class Paladin : Player
    {
        bool divineShield = false; int holyStacks = 0; bool consecrated = false; static readonly Random R = new();
        public Paladin(string n) : base(n, 165, 75, 20, def: 16, spd: 9) { ClassName = "Paladín"; SpecialName = "Juicio Divino"; Special2Name = "Tierra Consagrada"; SpecialCost = 20; SpecialCost2 = 35; AttackElement = ElementType.Holy; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; var t = enemies.OrderByDescending(e => e.MaxHealth).First();
            float mult = 2.8f + holyStacks * 0.3f; int dmg = (int)((BaseAttack + AttackBonus) * mult);
            t.TakeDamage(dmg, ElementType.Holy); t.ApplyStatus(StatusEffect.Cursed, 3); t.ApplyStatus(StatusEffect.Weakened, 2);
            int sh = 30 + holyStacks * 5; Heal(sh); holyStacks = 0; SoundSystem.PlayElemental(ElementType.Holy);
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ✝  {Name} JUICIO DIVINO → {t.Name}: {dmg}! +{sh} HP self."); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies)
        {
            if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; }
            Mana -= SpecialCost2; consecrated = !consecrated;
            if (consecrated) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  🌟 {Name} TIERRA CONSAGRADA! Aliados: +Regen +5 DEF"); foreach (var p in allies.Where(p => p.IsAlive())) { p.ApplyStatus(StatusEffect.Regenerating, 5); p.Defense += 5; } }
            else { foreach (var p in allies.Where(p => p.IsAlive())) p.Defense -= 5; Console.WriteLine($"\n  La consagración termina."); }
            Console.ResetColor();
        }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { divineShield = !divineShield; holyStacks = Math.Min(holyStacks + 2, 10); if (divineShield) { foreach (var p in allies.Where(p => p.IsAlive())) { p.ApplyStatus(StatusEffect.Blessed, 3); p.ApplyStatus(StatusEffect.Shielded, 2); } Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  🛡 {Name} ESCUDO DIVINO. Todos: Bendición+Escudo. Carga:{holyStacks}"); } else Console.WriteLine($"\n  🛡 {Name} retira el Escudo. Carga:{holyStacks}"); Console.ResetColor(); }
        protected override void OnLevelUp() { Defense += 3; MaxHealth += 5; }
    }

    class Necromancer : Player
    {
        int soulStacks = 0; List<UndeadMinion> minions = new(); bool deathMarkActive = false; static readonly Random R = new();
        public Necromancer(string n) : base(n, 98, 130, 14, def: 3, spd: 10) { ClassName = "Nigromante"; SpecialName = "Drenaje de Almas"; Special2Name = "Invocar No-Muerto"; SpecialCost = 22; SpecialCost2 = 30; AttackElement = ElementType.Dark; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int totalDrained = 0; SoundSystem.PlayElemental(ElementType.Dark);
            Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine($"\n  🌑 {Name} DRENAJE DE ALMAS! (Almas:{soulStacks})");
            foreach (var e in enemies) { int drain = (int)((BaseAttack + AttackBonus) * 1.3f) + soulStacks * 7; if (deathMarkActive) drain = (int)(drain * 1.5f); e.TakeDamage(drain, ElementType.Dark); totalDrained += drain / 3; Console.WriteLine($"     → {e.Name}: {drain}"); if (!e.IsAlive() && minions.Count < 3) { var m = new UndeadMinion($"Zombi de {e.Name}", e.MaxHealth / 3, e.MaxHealth / 6); minions.Add(m); Console.WriteLine($"     💀 ¡Revive a {m.Name}!"); } }
            Heal(totalDrained); GlobalStats.TotalDamageDealt += enemies.Sum(e => (int)((BaseAttack + AttackBonus) * 1.3f) + soulStacks * 7); soulStacks = 0; deathMarkActive = false;
            Console.WriteLine($"  💜 Absorbe {totalDrained} HP. Súbditos:{minions.Count}"); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } if (minions.Count >= 3) { Console.WriteLine("\n  ✘ Límite de súbditos."); return; } Mana -= SpecialCost2; var m = new UndeadMinion("Esqueleto Arcano", 60 + Level * 5, 8 + Level * 2); minions.Add(m); deathMarkActive = true; Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"\n  💀 {Name} invoca {m.Name}! Súbditos:{minions.Count}/3"); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { soulStacks = Math.Min(soulStacks + 4, 25); deathMarkActive = true; foreach (var e in enemies) { e.ApplyStatus(StatusEffect.Cursed, 2); e.ApplyStatus(StatusEffect.Weakened, 1); } foreach (var m in minions.Where(m => m.IsAlive())) { if (enemies.Any()) { var t = enemies.OrderBy(e => e.Health).First(); int d = m.Attack(); t.TakeDamage(d, ElementType.Dark); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"  💀 {m.Name} ataca a {t.Name}: {d}"); Console.ResetColor(); } } Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine($"\n  💀 {Name} MALDICIÓN GRUPAL. Almas:{soulStacks}"); Console.ResetColor(); }
        protected override void OnLevelUp() { Mana += 12; BaseAttack += 1; }
    }

    class UndeadMinion
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; }
        int BaseAtk; static readonly Random R = new();
        public UndeadMinion(string name, int hp, int atk) { Name = name; Health = hp; MaxHealth = hp; BaseAtk = atk; }
        public bool IsAlive() => Health > 0; public int Attack() => BaseAtk + R.Next(-2, 5); public void TakeDamage(int d) => Health = Math.Max(0, Health - d);
    }

    class Alchemist : Player
    {
        int brewCount = 0; int reagentStacks = 0; bool turboMode = false; static readonly Random R = new();
        public Alchemist(string n) : base(n, 105, 105, 15, def: 6, spd: 13) { ClassName = "Alquimista"; SpecialName = "Bomba Alquímica"; Special2Name = "Gran Explosión"; SpecialCost = 18; SpecialCost2 = 35; AttackElement = ElementType.Poison; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; brewCount++; float mult = turboMode ? 2.5f : 1.6f; int dmg = (int)((BaseAttack + AttackBonus) * mult) + brewCount * 5 + reagentStacks * 3; int total = 0;
            SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine($"\n  ⚗ {Name} BOMBA ALQUÍMICA! (Brew #{brewCount}{(turboMode ? " TURBO" : "")})");
            foreach (var e in enemies) { e.TakeDamage(dmg, ElementType.Poison); e.ApplyStatus(StatusEffect.Poisoned, 3 + reagentStacks / 3); total += dmg; Console.WriteLine($"     → {e.Name}: {dmg} + VENENO"); }
            if (R.Next(100) < 40 || turboMode) { foreach (var e in enemies) e.ApplyStatus(StatusEffect.Burned, 2); Console.WriteLine("  🔥 Reacción: Quemadura!"); }
            GlobalStats.TotalDamageDealt += total; turboMode = false; reagentStacks = Math.Max(0, reagentStacks - 2); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; turboMode = true; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  💥 {Name} GRAN EXPLOSIÓN. Próxima bomba: x2.5!"); Console.ResetColor(); var hurt = allies.Where(p => p.IsAlive()).OrderBy(p => (float)p.Health / p.MaxHealth).First(); int heal = 50 + Level * 3; hurt.Heal(heal); SoundSystem.PlayHeal(); Console.WriteLine($"  🧪 Antídoto emergencia para {hurt.Name}: +{heal} HP."); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { reagentStacks = Math.Min(reagentStacks + 3, 15); var hurt = allies.Where(p => p.IsAlive()).OrderBy(p => (float)p.Health / p.MaxHealth).First(); int h = 40 + Level * 5 + reagentStacks * 2; hurt.Heal(h); SoundSystem.PlayHeal(); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  ⚗ {Name} POCIÓN IMPROVISADA para {hurt.Name}: +{h} HP. Reactivos:{reagentStacks}"); Console.ResetColor(); }
        protected override void OnLevelUp() { BaseAttack += 2; Mana += 5; }
    }

    class Berserker : Player
    {
        int bloodlust = 0; bool frenzyActive = false; int selfDamageCount = 0; static readonly Random R = new();
        public Berserker(string n) : base(n, 160, 35, 28, def: 6, spd: 14) { ClassName = "Berserker"; SpecialName = "Furia Sangrienta"; Special2Name = "Locura de Sangre"; SpecialCost = 10; SpecialCost2 = 20; CritChance = 22; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int selfDmg = (int)(MaxHealth * 0.08f); Health = Math.Max(1, Health - selfDmg); selfDamageCount++;
            float hpRatio = 1 - (float)Health / MaxHealth; float mult = 2.0f + hpRatio * 3.0f + bloodlust * 0.2f; if (frenzyActive) mult *= 1.5f;
            var t = enemies.OrderByDescending(e => e.MaxHealth).First(); int dmg = (int)((BaseAttack + AttackBonus) * mult) + R.Next(0, 20);
            bool crit = R.Next(100) < CritChance + (int)(hpRatio * 30); if (crit) { dmg = (int)(dmg * 2.0f); GlobalStats.TotalCriticalHits++; }
            t.TakeDamage(dmg, ElementType.Physical); bloodlust = Math.Min(bloodlust + 2, 20); GlobalStats.TotalDamageDealt += dmg;
            if (dmg > GlobalStats.HighestDamageInOneTurn) { GlobalStats.HighestDamageInOneTurn = dmg; GlobalStats.HighestDamageDealer = Name; }
            SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"\n  💢 {Name} FURIA SANGRIENTA (x{mult:F1}) → {t.Name}: {dmg}{(crit ? " 💥 DOBLE" : "")}! (Auto:{selfDmg})"); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; frenzyActive = !frenzyActive; bloodlust += 5; Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(frenzyActive ? $"\n  💢 {Name} LOCURA DE SANGRE! x1.5 a Furia. ¡Imparable!" : $"\n  {Name} calma la Locura."); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { int h = bloodlust * 4 + selfDamageCount * 5; Heal(h); ApplyStatus(StatusEffect.Enraged, 2); Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"\n  💢 {Name} canaliza RABIA. +{h} HP. Sed:{bloodlust}"); Console.ResetColor(); }
        protected override void OnLevelUp() { BaseAttack += 5; MaxHealth -= 5; CritChance += 2; }
    }

    class Druid : Player
    {
        bool wildForm = false; int naturePower = 0; static readonly Random R = new();
        public Druid(string n) : base(n, 132, 100, 15, def: 9, spd: 11) { ClassName = "Druida"; SpecialName = "Llamada de la Naturaleza"; Special2Name = "Forma Salvaje"; SpecialCost = 22; SpecialCost2 = 25; AttackElement = ElementType.Earth; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int bonus = naturePower * 6; naturePower = 0; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  🌿 {Name} LLAMADA DE LA NATURALEZA!");
            if (wildForm) { foreach (var e in enemies) { int dmg = (int)((BaseAttack + AttackBonus) * 2.2f) + bonus + R.Next(10, 25); e.TakeDamage(dmg, ElementType.Earth); e.ApplyStatus(StatusEffect.Stunned, 1); Console.WriteLine($"  🐻 {e.Name}: {dmg} + Aturdido"); } }
            else { foreach (var p in Program.Players.Where(p => p.IsAlive())) { int h = 35 + bonus / 2; p.Heal(h); p.ApplyStatus(StatusEffect.Regenerating, 3); } var st = enemies.OrderByDescending(e => e.MaxHealth).First(); int ed = (int)((BaseAttack + AttackBonus) * 1.5f) + bonus; st.TakeDamage(ed, ElementType.Earth); Console.WriteLine($"  🌳 Cura grupal + {ed} → {st.Name}"); }
            Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; wildForm = !wildForm; if (wildForm) { BaseAttack += 12; Defense -= 4; MaxHealth += 30; Health = Math.Min(Health + 30, MaxHealth); AttackElement = ElementType.Physical; Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine($"\n  🐻 {Name} FORMA SALVAJE! +12 ATK +30 HP"); } else { BaseAttack -= 12; Defense += 4; MaxHealth -= 30; Health = Math.Min(Health, MaxHealth); AttackElement = ElementType.Earth; Console.WriteLine($"\n  🌿 {Name} vuelve a forma natural."); } Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { naturePower = Math.Min(naturePower + 3, 15); foreach (var p in allies.Where(p => p.IsAlive())) { p.Heal(12 + naturePower); if (!p.HasStatus(StatusEffect.Regenerating)) p.ApplyStatus(StatusEffect.Regenerating, 2); } Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  🌱 {Name} sintoniza Naturaleza. Poder:{naturePower}/15. Forma:{(wildForm ? "Salvaje 🐻" : "Natural 🌿")}"); Console.ResetColor(); }
        protected override void OnLevelUp() { MaxHealth += 8; Mana += 8; }
    }

    class Summoner : Player
    {
        List<SummonedCreature> summons = new(); int summonPower = 0; int maxSummons = 3; static readonly Random R = new();
        public Summoner(string n) : base(n, 110, 120, 11, def: 5, spd: 10) { ClassName = "Invocador"; SpecialName = "Invocación Masiva"; Special2Name = "Potenciar Invocaciones"; SpecialCost = 28; SpecialCost2 = 20; AttackElement = ElementType.Arcane; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; SoundSystem.PlaySpecial();
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ✨ {Name} INVOCACIÓN MASIVA!");
            string[] cnames = { "Elemental de Fuego", "Golem de Piedra", "Serpiente Arcana", "Espíritu Lunar", "Fénix Menor" };
            int toSummon = Math.Min(2, maxSummons - summons.Count);
            for (int i = 0; i < toSummon; i++) { var c = new SummonedCreature(cnames[R.Next(cnames.Length)], 50 + Level * 8 + summonPower * 5, 10 + Level * 3 + summonPower * 2); summons.Add(c); Console.WriteLine($"  ⚡ Invocado: {c.Name} HP:{c.Health}"); }
            AttackWithSummons(enemies); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; summonPower = Math.Min(summonPower + 3, 15); foreach (var s in summons) s.LevelUp(); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ✨ {Name} potencia invocaciones. Poder:{summonPower}"); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { AttackWithSummons(enemies); summons.RemoveAll(s => !s.IsAlive()); if (summons.Count < maxSummons && Mana >= 10) { string[] ns = { "Familiar Arcano", "Sombra Invocada", "Espíritu Guardián" }; var s = new SummonedCreature(ns[R.Next(ns.Length)], 30 + Level * 5, 8 + Level * 2); summons.Add(s); Mana -= 10; Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"\n  ✨ {Name} invoca {s.Name}. Total:{summons.Count}/{maxSummons}"); Console.ResetColor(); } }
        void AttackWithSummons(List<Enemy> enemies) { foreach (var s in summons.Where(s => s.IsAlive())) { if (!enemies.Any()) break; var t = enemies[R.Next(enemies.Count)]; int d = s.Attack(); t.TakeDamage(d, ElementType.Arcane); Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ✨ {s.Name} → {t.Name}: {d}"); Console.ResetColor(); } }
        protected override void OnLevelUp() { Mana += 12; if (Level % 5 == 0) maxSummons++; }
    }

    class SummonedCreature
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int BaseAttack { get; private set; }
        static readonly Random R = new();
        public SummonedCreature(string n, int hp, int atk) { Name = n; Health = hp; MaxHealth = hp; BaseAttack = atk; }
        public bool IsAlive() => Health > 0; public int Attack() => BaseAttack + R.Next(-2, 6); public void TakeDamage(int d) => Health = Math.Max(0, Health - d); public void LevelUp() { BaseAttack += 3; MaxHealth += 15; Health = Math.Min(Health + 15, MaxHealth); }
    }

    class DemonHunter : Player
    {
        int demonKills = 0; bool shadowMode = false; int hunterMark = 0; static readonly Random R = new();
        public DemonHunter(string n) : base(n, 120, 80, 21, def: 7, spd: 17) { ClassName = "Cazador de Demonios"; SpecialName = "Marca del Cazador"; Special2Name = "Modo Umbral"; SpecialCost = 18; SpecialCost2 = 22; CritChance = 25; AttackElement = ElementType.Dark; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; var t = enemies.OrderByDescending(e => e.MaxHealth).First(); hunterMark++;
            float mult = 2.0f + demonKills * 0.15f + hunterMark * 0.2f; if (shadowMode) mult *= 1.6f;
            int dmg = (int)((BaseAttack + AttackBonus) * mult); bool crit = R.Next(100) < CritChance + demonKills; if (crit) { dmg = (int)(dmg * 2f); GlobalStats.TotalCriticalHits++; }
            t.TakeDamage(dmg, ElementType.Dark); t.ApplyStatus(StatusEffect.Weakened, 3); t.ApplyStatus(StatusEffect.Cursed, 2);
            GlobalStats.TotalDamageDealt += dmg; if (!t.IsAlive()) demonKills++;
            SoundSystem.PlayElemental(ElementType.Dark);
            Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"\n  🏹 {Name} MARCA DEL CAZADOR → {t.Name}: {dmg}{(crit ? " 💥" : "")} Demonios:{demonKills}"); Console.ResetColor();
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; shadowMode = !shadowMode; if (shadowMode) { ApplyStatus(StatusEffect.Invisible, 3); ApplyStatus(StatusEffect.Hasted, 3); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"\n  🌑 {Name} MODO UMBRAL. x1.6 dmg + Invisible + Veloz!"); } else Console.WriteLine($"\n  {Name} desactiva Modo Umbral."); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { foreach (var e in enemies) e.ApplyStatus(StatusEffect.Cursed, 2); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"\n  🔍 {Name} marca a todos los enemigos. Demonios:{demonKills}"); Console.ResetColor(); }
        protected override void OnLevelUp() { CritChance += 2; Speed += 2; }
    }

    class Elementalist : Player
    {
        ElementType[] elements = { ElementType.Fire, ElementType.Ice, ElementType.Lightning, ElementType.Earth };
        int elementIndex = 0; int elementalCharge = 0; bool prismMode = false; static readonly Random R = new();
        public Elementalist(string n) : base(n, 100, 135, 13, def: 4, spd: 11) { ClassName = "Elementalista"; SpecialName = "Convergencia Elemental"; Special2Name = "Prisma Arcano"; SpecialCost = 22; SpecialCost2 = 35; CritChance = 15; AttackElement = ElementType.Fire; }
        public override void Special(List<Enemy> enemies)
        {
            Mana -= SpecialCost; int charge = elementalCharge; elementalCharge = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (prismMode) { Console.WriteLine($"\n  🌈 {Name} CONVERGENCIA PRISMA!"); foreach (var e in enemies) { int total = 0; foreach (var el in elements) { int dmg = (int)((BaseAttack + AttackBonus) * 1.0f) + charge * 3 + R.Next(8, 18); e.TakeDamage(dmg, el); total += dmg; } Console.WriteLine($"  → {e.Name}: {total} total"); } prismMode = false; }
            else { var cur = elements[elementIndex]; SoundSystem.PlayElemental(cur); Console.WriteLine($"\n  ⚡ {Name} CONVERGENCIA [{cur}]!"); foreach (var e in enemies) { int dmg = (int)((BaseAttack + AttackBonus) * 2.0f) + charge * 5 + R.Next(10, 25); e.TakeDamage(dmg, cur); Console.WriteLine($"  → {e.Name}: {dmg} [{cur}]"); switch (cur) { case ElementType.Fire: e.ApplyStatus(StatusEffect.Burned, 3); break; case ElementType.Ice: e.ApplyStatus(StatusEffect.Frozen, 2); break; case ElementType.Lightning: case ElementType.Earth: e.ApplyStatus(StatusEffect.Stunned, 1); break; } } }
            Console.ResetColor(); elementIndex = (elementIndex + 1) % elements.Length; AttackElement = elements[elementIndex];
        }
        public override void Special2(List<Player> allies, List<Enemy> enemies) { if (Mana < SpecialCost2) { Console.WriteLine("\n  ✘ Sin maná."); return; } Mana -= SpecialCost2; prismMode = true; elementalCharge = Math.Min(elementalCharge + 8, 20); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  🌈 {Name} PRISMA ARCANO activo. Próxima convergencia: todos los elementos!"); Console.ResetColor(); }
        public override void PassiveAction(List<Player> allies, List<Enemy> enemies) { elementalCharge = Math.Min(elementalCharge + 3, 20); elementIndex = (elementIndex + 1) % elements.Length; AttackElement = elements[elementIndex]; Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  💎 {Name} cambia a [{elements[elementIndex]}]. Carga:{elementalCharge}/20"); Console.ResetColor(); }
        protected override void OnLevelUp() { Mana += 15; BaseAttack += 1; }
    }

    // ═══════════════════════════════════════════════
    //  PROGRAMA PRINCIPAL
    // ═══════════════════════════════════════════════

    class Program
    {
        static readonly Random Rand = new();
        internal static List<Player> Players = new();
        internal static Guild? ActiveGuild = null;
        static List<Quest> GlobalQuests = new();
        static int round = 1;
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
  ╚═╝  ╚═╝╚═╝      ╚═════╝     ╚═╝     ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n            ━━━  EDICIÓN DEFINITIVA DELUXE v5.0 — ÉPICA TOTAL  ━━━\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  15 Clases | Mazmorras | Crafteo | Talentos | Misiones | Gremios");
            Console.WriteLine("  ♪ Música épica de batalla | Fanfares orquestales | Réquiems dramáticos");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Sonido: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ON  ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(escribe 'S' para desactivar o cualquier tecla para continuar)");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.KeyChar == 'S' || key.KeyChar == 's') SoundSystem.Toggle();
            else SoundSystem.PlayIntro();
        }

        static void SetupGuild()
        {
            Console.Clear(); PrintBox("🏰 CREAR GREMIO", ConsoleColor.Yellow);
            Console.Write("\n  Nombre de tu Gremio (Enter = 'Los Sin Nombre'): ");
            string guildName = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(guildName)) guildName = "Los Sin Nombre";
            ActiveGuild = new Guild(guildName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ Gremio «{guildName}» fundado.");
            Console.ResetColor(); Console.ReadKey(true);
        }

        static void SetupPlayers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  Cantidad de jugadores (1-{GameConfig.MAX_PLAYERS}): ");
            Console.ResetColor();
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > GameConfig.MAX_PLAYERS) n = 1;
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
                Console.Clear(); PrintBox("ELIGE TU CLASE", ConsoleColor.Cyan); Console.WriteLine();
                var classes = new (string num, string icon, string name, string stats)[]
                {
                    ("1","⚔","Guerrero","HP:200 ATK:22 DEF:14 VEL:8"),
                    ("2","🏹","Arquero","HP:115 ATK:18 DEF:5  VEL:16"),
                    ("3","🔥","Mago","HP:88  ATK:12 DEF:3  VEL:10"),
                    ("4","✨","Curandero","HP:125 ATK:9  DEF:7  VEL:9"),
                    ("5","🗡","Ladrón","HP:98  ATK:20 DEF:4  VEL:22"),
                    ("6","👊","Monje","HP:135 ATK:19 DEF:10 VEL:18"),
                    ("7","🎵","Bardo","HP:108 ATK:12 DEF:5  VEL:14"),
                    ("8","🛡","Paladín","HP:165 ATK:20 DEF:16 VEL:9"),
                    ("9","🌑","Nigromante","HP:98  ATK:14 DEF:3  VEL:10"),
                    ("10","⚗","Alquimista","HP:105 ATK:15 DEF:6  VEL:13"),
                    ("11","💢","Berserker","HP:160 ATK:28 DEF:6  VEL:14"),
                    ("12","🌿","Druida","HP:132 ATK:15 DEF:9  VEL:11"),
                    ("13","✨","Invocador","HP:110 ATK:11 DEF:5  VEL:10"),
                    ("14","🌑","Cazador de Demonios","HP:120 ATK:21 DEF:7  VEL:17"),
                    ("15","💎","Elementalista","HP:100 ATK:13 DEF:4  VEL:11"),
                };
                foreach (var c in classes)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  {c.num,2}. {c.icon} {c.name,-22}");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($" {c.stats}");
                    Console.ResetColor();
                }
                Console.Write("\n  Opción: ");
                string op = Console.ReadLine() ?? "";
                Console.Write("  Nombre: ");
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
                    if (ActiveGuild != null) { player.AttackBonus += ActiveGuild.BonusAttack; player.Defense += ActiveGuild.BonusDefense; }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✔ {player.ClassName} «{name}» creado.");
                    Console.ResetColor();
                    Console.Write("  ¿Adoptar compañero? (s/n): ");
                    if ((Console.ReadLine() ?? "").ToLower() == "s") player.AdoptCompanion();
                    Console.ReadKey(true);
                    return player;
                }
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ Opción inválida."); Console.ResetColor(); Console.ReadKey(true);
            }
        }

        static void SetupQuests()
        {
            GlobalQuests = QuestDatabase.GetStarterQuests();
            foreach (var p in Players)
                foreach (var q in GlobalQuests.Take(3)) { q.Status = QuestStatus.Active; q.StartedRound = round; p.ActiveQuests.Add(q); }
        }

        static void GameLoop()
        {
            while (Players.Any(p => p.IsAlive()))
            {
                GlobalStats.HighestRound = Math.Max(GlobalStats.HighestRound, round);
                WorldTime.Advance(2);
                if (round % 5 == 0) WorldTime.ChangeBiome(Enum.GetValues<BiomeType>()[Rand.Next(8)]);

                bool isBoss = round % 5 == 0;
                bool isMythic = round % 10 == 0;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  ══════════  RONDA {round}  ══════════");
                Console.ResetColor();
                WorldTime.ShowWorldInfo();

                if (isMythic)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("  ☠  ¡RONDA DE JEFE MÍTICO! ☠");
                    Console.ResetColor(); SoundSystem.PlayBossAlert();
                }
                else if (isBoss)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  ¡RONDA DE JEFE! ⚠");
                    Console.ResetColor(); SoundSystem.PlayBossAlert();
                }

                if (lastWeather != WorldTime.CurrentWeather)
                {
                    lastWeather = WorldTime.CurrentWeather;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\n  🌤 Clima: {WorldTime.GetWeatherIcon()} {WorldTime.CurrentWeather}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"\n  ♪ Música: {GetAmbientName(round, isBoss, isMythic)}  |  🔊 Sonido: {(SoundSystem.Enabled ? "ON" : "OFF")} (opción 16)");
                Console.WriteLine("  Pulsa una tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey(true);

                SoundSystem.StartAmbient(round, isBoss);

                List<Enemy> enemies = GenerateEnemies();
                Battle(enemies);

                SoundSystem.StopAmbient();

                round++;

                if (ActiveGuild != null) { ActiveGuild.AddExperience(10 + round * 2); ActiveGuild.TotalKills += enemies.Count; }
            }

            SoundSystem.StopAmbient();
            SoundSystem.PlayDefeat(round);
            ShowGameOver();
        }

        static string GetAmbientName(int r, bool boss, bool mythic)
        {
            if (mythic) return "Caos del Abismo Mítico ☠";
            if (boss) return "Batalla del Jefe ⚔ (épica)";
            if (r <= 4) return "Aldea Tranquila 🌾";
            if (r <= 9) return "Bosque Misterioso 🌳";
            if (r <= 14) return "Caverna Oscura 🕳";
            if (r <= 19) return "Tierras Épicas 🔥";
            return "El Abismo Sin Fondo 🌑";
        }

        static List<Enemy> GenerateEnemies()
        {
            var enemies = new List<Enemy>();
            int shp = round * 10 + (WorldTime.CurrentWeather == WeatherType.Eclipse ? 30 : 0) + (WorldTime.IsNight ? 15 : 0);
            int satk = round * 3 + (WorldTime.CurrentWeather == WeatherType.Eclipse ? 10 : 0) + (WorldTime.IsNight ? 5 : 0);

            if (round % 10 == 0)
            {
                enemies.Add(new MythicBoss("DIOS DEL ABISMO", 800 + shp, 70 + satk));
                for (int i = 0; i < 2; i++) enemies.Add(new EliteEnemy($"Guardián del Abismo {(char)('A' + i)}", 120 + shp / 3, 20 + satk / 2));
            }
            else if (round % 5 == 0)
            {
                string[] bn = { "SEÑOR DE LA OSCURIDAD", "JEFE DE HORDA", "REY ESQUELETO", "ARCHIMAGO CORRUPTO", "DRAGÓN INMORTAL" };
                enemies.Add(new LegendaryBoss(bn[Rand.Next(bn.Length)], 400 + shp, 45 + satk));
            }
            else
            {
                int count = Players.Count + Rand.Next(1, 4);
                for (int i = 0; i < count; i++)
                {
                    string name = GameConfig.EnemyTypes[Rand.Next(GameConfig.EnemyTypes.Length)] + " " + (char)('A' + i);
                    int ehp = 55 + shp + Rand.Next(35), eatk = 10 + satk + Rand.Next(6);
                    int roll = Rand.Next(100);
                    if (roll < 8) enemies.Add(new BossEnemy(name + " ÉLITE", ehp + 80, eatk + 15));
                    else if (roll < 25) enemies.Add(new EliteEnemy(name + " ★", ehp + 40, eatk + 10));
                    else enemies.Add(new Enemy(name, ehp, eatk, 5 + round, 10 + round * 2));
                }
            }
            return enemies;
        }

        static void Battle(List<Enemy> enemies)
        {
            while (Players.Any(p => p.IsAlive()) && enemies.Count > 0)
            {
                var turnOrder = Players.Where(p => p.IsAlive())
                    .OrderByDescending(p => p.Speed + WorldTime.GetWeatherEffects().spdMod).ToList();

                foreach (var p in turnOrder)
                {
                    if (!p.IsAlive()) continue;
                    if (p.HasStatus(StatusEffect.Stunned) || p.HasStatus(StatusEffect.Frozen) || p.HasStatus(StatusEffect.Petrified))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n  ⚠  {p.Name} no puede actuar ({p.GetStatusString()}).");
                        Console.ResetColor(); p.Tick(); Console.ReadKey(true); continue;
                    }
                    p.Tick();

                    if (p.Pet != null && enemies.Any() && Rand.Next(100) < 35)
                    {
                        string pa = p.Pet.ActInCombat(p, enemies);
                        if (!string.IsNullOrEmpty(pa)) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(pa); Console.ResetColor(); }
                    }

                    bool actionTaken = false;
                    while (!actionTaken)
                    {
                        Console.Clear(); ShowStatus(enemies); ShowPlayerMenu(p);
                        string op = Console.ReadLine() ?? "";
                        actionTaken = HandlePlayerAction(p, op, enemies);
                    }

                    int newKills = enemies.Count(e => !e.IsAlive());
                    if (newKills > 0) { p.TotalKills += newKills; GlobalStats.TotalEnemiesKilled += newKills; p.UpdateQuestProgress(newKills); if (ActiveGuild != null) ActiveGuild.TotalKills += newKills; }
                    enemies.RemoveAll(e => !e.IsAlive());
                    if (enemies.Count == 0) break;
                }

                if (enemies.Count > 0) EnemyTurn(enemies);

                foreach (var p in Players.Where(p => !p.IsAlive() && !p.HasUsedRevive))
                {
                    if (p.Talents.GetUnlockedTalents().Contains("Resiliencia"))
                    { p.Health = (int)(p.MaxHealth * 0.3f); p.HasUsedRevive = true; SoundSystem.PlayRevive(); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  ✨ RESILIENCIA: {p.Name} revive con {p.Health} HP!"); Console.ResetColor(); }
                    else p.DeathMessage();
                }
            }

            if (Players.Any(p => p.IsAlive())) VictoryScreen(enemies);
        }

        static bool HandlePlayerAction(Player p, string op, List<Enemy> enemies)
        {
            switch (op)
            {
                case "1":
                    {
                        Enemy? t = ChooseTarget(enemies); if (t == null) return false;
                        int dmg = p.Attack(); bool crit = Rand.Next(100) < p.CritChance;
                        if (crit) { dmg = (int)(dmg * GameConfig.BASE_CRIT_MULTIPLIER); GlobalStats.TotalCriticalHits++; SoundSystem.PlayCritical(); }
                        else SoundSystem.PlayElemental(p.AttackElement);
                        dmg = Math.Max(1, dmg + WorldTime.GetWeatherEffects().atkMod);
                        t.TakeDamage(dmg, p.AttackElement); GlobalStats.TotalDamageDealt += dmg;
                        if (dmg > GlobalStats.HighestDamageInOneTurn) { GlobalStats.HighestDamageInOneTurn = dmg; GlobalStats.HighestDamageDealer = p.Name; }
                        Console.ForegroundColor = crit ? ConsoleColor.Yellow : ConsoleColor.White;
                        Console.WriteLine(crit ? $"\n  💥 ¡CRÍTICO! {p.Name} → {t.Name}: {dmg} [{p.AttackElement}]"
                                              : $"\n  ⚔  {p.Name} → {t.Name}: {dmg} [{p.AttackElement}]");
                        Console.ResetColor(); Console.ReadKey(true); return true;
                    }
                case "2":
                    if (p.Mana < p.SpecialCost) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n  ✘ Sin maná ({p.SpecialCost} requerido)."); Console.ResetColor(); Console.ReadKey(true); return false; }
                    p.Special(enemies); enemies.RemoveAll(e => !e.IsAlive()); Console.ReadKey(true); return true;
                case "3":
                    p.Defending = true; p.ApplyStatus(StatusEffect.Shielded, 1); SoundSystem.PlayDefend();
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n  🛡 {p.Name} se defiende."); Console.ResetColor(); Console.ReadKey(true); return true;
                case "4": p.OpenInventory(); return true;
                case "5": StaticShop(p); return true;
                case "6": p.PassiveAction(Players, enemies); Console.ReadKey(true); return true;
                case "7":
                    if (p.Mana < p.SpecialCost2) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n  ✘ Sin maná ({p.SpecialCost2} requerido)."); Console.ResetColor(); Console.ReadKey(true); return false; }
                    p.Special2(Players, enemies); Console.ReadKey(true); return true;
                case "8": p.Talents.ShowTalentMenu(p); return false;
                case "9": p.OpenCrafting(); return false;
                case "10": p.ShowQuests(); return false;
                case "11": OpenDungeonMenu(p); return false;
                case "12": ActiveGuild?.ShowInfo(); return false;
                case "13": GlobalStats.ShowStats(); return false;
                case "14": ShowDetailedStats(p); return false;
                case "15": p.AdoptCompanion(); return false;
                case "16": SoundSystem.Toggle(); Console.ReadKey(true); return false;
                default:
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ Opción inválida."); Console.ResetColor(); Console.ReadKey(true); return false;
            }
        }

        static Enemy? ChooseTarget(List<Enemy> enemies)
        {
            if (enemies.Count == 1) return enemies[0];
            Console.WriteLine("\n  Elige objetivo:");
            for (int i = 0; i < enemies.Count; i++)
            {
                ConsoleColor col = enemies[i] is BossEnemy ? ConsoleColor.DarkRed : ConsoleColor.DarkYellow;
                Console.ForegroundColor = col;
                Console.WriteLine($"  {i + 1}. {enemies[i].Name,-24} HP:{HealthBar(enemies[i].Health, enemies[i].MaxHealth, 10)} {enemies[i].Health}/{enemies[i].MaxHealth}");
                Console.ResetColor();
            }
            Console.Write("  → ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= enemies.Count) return enemies[idx - 1];
            return enemies[Rand.Next(enemies.Count)];
        }

        static void EnemyTurn(List<Enemy> enemies)
        {
            var alive = Players.Where(p => p.IsAlive()).ToList();
            if (!alive.Any()) return;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ─── Turno de los enemigos ───\n"); Console.ResetColor();

            foreach (var e in enemies)
            {
                e.Tick(); if (!e.IsAlive()) continue;
                if (e.HasStatus(StatusEffect.Stunned) || e.HasStatus(StatusEffect.Frozen)) { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"  {e.Name} no puede actuar."); Console.ResetColor(); continue; }

                Player target;
                if (e is BossEnemy && Rand.Next(100) < 50) target = alive.OrderByDescending(p => p.Mana).First();
                else if (e is EliteEnemy && Rand.Next(100) < 40) target = alive.OrderByDescending(p => p.AttackBonus).First();
                else target = alive.OrderBy(p => (float)p.Health / p.MaxHealth).First();

                int dmg = Math.Max(1, e.Attack() + WorldTime.GetWeatherEffects().defMod);
                target.TakeDamage(dmg, ElementType.Physical); GlobalStats.TotalDamageReceived += dmg;
                SoundSystem.PlayEnemyAttack();
                Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"  {e.Name} ataca a {target.Name}: {dmg} daño."); Console.ResetColor();

                if (e is EliteEnemy && Rand.Next(100) < 30)
                {
                    var bad = new[] { StatusEffect.Poisoned, StatusEffect.Burned, StatusEffect.Bleeding, StatusEffect.Weakened };
                    var eff = bad[Rand.Next(bad.Length)]; target.ApplyStatus(eff, 2);
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"  ⚠  {target.Name} sufre {eff}."); Console.ResetColor();
                }
                if (e is BossEnemy && Rand.Next(100) < 30) e.UseSpecialAttack(target);
            }
            Console.ReadKey(true);
        }

        static void VictoryScreen(List<Enemy> killedEnemies)
        {
            Console.Clear(); PrintBox("✔ VICTORIA", ConsoleColor.Green);

            bool isBossRound = round % 5 == 0;
            bool isMythicRound = round % 10 == 0;

            if (isMythicRound) SoundSystem.PlayBossVictory(round);
            else if (isBossRound) SoundSystem.PlayBossVictory(round);
            else SoundSystem.PlayVictory(round);

            int goldEarned = Rand.Next(20, 60) + round * 4;
            int xpEarned = 30 + round * 6;
            if (isBossRound) { goldEarned *= 2; xpEarned *= 2; }
            if (WorldTime.CurrentWeather == WeatherType.Blessed) goldEarned = (int)(goldEarned * 1.25f);
            if (ActiveGuild != null) { int gb = (int)(goldEarned * ActiveGuild.BonusGoldPercent / 100f); goldEarned += gb; ActiveGuild.AddGold(gb); ActiveGuild.AddExperience(20 + round); }
            GlobalStats.TotalGoldEarned += goldEarned;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Oro ganado:   {goldEarned} 🪙");
            Console.WriteLine($"  XP ganada:    {xpEarned}");
            if (isMythicRound) Console.WriteLine("  ☠  ¡Victoria Mítica! x2 recompensas!");
            else if (isBossRound) Console.WriteLine("  ⭐ Bonus de Jefe x2!");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  ♪ Fanfare: {GetVictoryName(round, isBossRound, isMythicRound)}");
            Console.ResetColor();

            foreach (var p in Players.Where(p => p.IsAlive()))
            {
                p.Gold += goldEarned; p.GainXP(xpEarned); p.RoundsWon++;
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
                if (round % 3 == 0) { p.TalentPoints++; p.Talents.AvailablePoints++; Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ✨ {p.Name} +1 Punto de Talento (Total:{p.Talents.AvailablePoints})"); Console.ResetColor(); }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para la siguiente ronda...");
            Console.ResetColor(); Console.ReadKey(true);
        }

        static string GetVictoryName(int r, bool boss, bool mythic)
        {
            if (mythic) return "Himno del Conquistador Mítico ☠🌟";
            if (boss) return "Marcha del Campeón 🏆⚔";
            if (r <= 4) return "Fanfare del Principiante ⭐";
            if (r <= 9) return "Fanfare del Explorador 🗡";
            if (r <= 14) return "Triada del Héroe ⚔";
            if (r <= 19) return "Himno del Guerrero Épico 🔥";
            return "Legado Legendario ✨";
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
   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  ♪ Réquiem: {GetDefeatName(round)}");
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

            if (ActiveGuild != null) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  Gremio «{ActiveGuild.Name}»: Nivel {ActiveGuild.Level} ({ActiveGuild.Rank})"); Console.ResetColor(); }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Pulsa una tecla para salir...");
            Console.ResetColor(); Console.ReadKey(true);
        }

        static string GetDefeatName(int r)
        {
            if (r <= 4) return "Despedida Suave 🕊";
            if (r <= 9) return "Descenso Dramático 🌧";
            if (r <= 14) return "Marcha Fúnebre 💀";
            if (r <= 19) return "Réquiem Épico ⚰";
            return "Réquiem Legendario 🌑";
        }

        public static void StaticShop(Player p)
        {
            bool inShop = true;
            while (inShop)
            {
                Console.Clear(); PrintBox($"🏪 TIENDA  (Oro: {p.Gold}🪙)", ConsoleColor.Yellow);
                var items = ShopDatabase.GetShopItems(round);
                Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"  Ronda: {round}  |  Inventario: {p.Inventory.Count}/{GameConfig.MAX_INVENTORY}"); Console.ResetColor(); Console.WriteLine();
                for (int i = 0; i < items.Count; i++)
                {
                    ConsoleColor col = items[i].Price > p.Gold ? ConsoleColor.DarkGray : items[i].GetRarityColor();
                    Console.ForegroundColor = col;
                    Console.WriteLine($"  {i + 1,2}. {items[i].GetRarityStars()} [{items[i].Rarity,-10}] {items[i].Name,-28} {items[i].Price,5}🪙  — {items[i].Description}");
                    Console.ResetColor();
                }
                Console.WriteLine($"\n  0. Salir  |  R. Refrescar ({GameConfig.SHOP_REFRESH_COST}🪙)");
                Console.Write("\n  Opción: ");
                string op = Console.ReadLine() ?? "";

                if (op == "0") { inShop = false; }
                else if (op.ToUpper() == "R")
                {
                    if (p.Gold >= GameConfig.SHOP_REFRESH_COST) { p.Gold -= GameConfig.SHOP_REFRESH_COST; GlobalStats.TotalGoldSpent += GameConfig.SHOP_REFRESH_COST; SoundSystem.PlayShopRefresh(); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("  ✔ Tienda refrescada."); Console.ResetColor(); Console.ReadKey(true); }
                    else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ✘ Oro insuficiente."); Console.ResetColor(); Console.ReadKey(true); }
                }
                else if (int.TryParse(op, out int idx) && idx >= 1 && idx <= items.Count)
                {
                    var chosen = items[idx - 1];
                    if (p.Gold >= chosen.Price) { if (p.Inventory.Count >= GameConfig.MAX_INVENTORY) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n  ✘ Inventario lleno."); Console.ResetColor(); Console.ReadKey(true); continue; } p.Gold -= chosen.Price; GlobalStats.TotalGoldSpent += chosen.Price; p.Inventory.Add(chosen); SoundSystem.PlayItemBuy(); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n  ✔ Compraste: [{chosen.Rarity}] {chosen.Name}"); Console.ResetColor(); Console.ReadKey(true); }
                    else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n  ✘ Oro insuficiente."); Console.ResetColor(); Console.ReadKey(true); }
                }
            }
        }

        static void OpenDungeonMenu(Player p)
        {
            Console.Clear(); PrintBox("🗺 MAZMORRAS DISPONIBLES", ConsoleColor.Magenta);
            Console.WriteLine($"  Oro: {p.Gold}🪙  |  Nivel: {p.Level}\n");
            var dungeons = Dungeon.GetAvailableDungeons(round);
            for (int i = 0; i < dungeons.Count; i++)
            {
                ConsoleColor col = dungeons[i].Difficulty switch { Rarity.Legendary => ConsoleColor.Magenta, Rarity.Epic => ConsoleColor.Blue, Rarity.Rare => ConsoleColor.Cyan, _ => ConsoleColor.White };
                Console.ForegroundColor = col; Console.WriteLine($"  {i + 1}. [{dungeons[i].Difficulty}] {dungeons[i].Name}");
                Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"     Tipo:{dungeons[i].Type} Pisos:{dungeons[i].Floors} Recompensa:{dungeons[i].GoldReward}🪙 {dungeons[i].XPReward}XP"); Console.ResetColor();
            }
            Console.WriteLine("  0. Cancelar"); Console.Write("  → ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= dungeons.Count) dungeons[idx - 1].Explore(Players);
        }

        static void ShowStatus(List<Enemy> enemies)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ══ RONDA {round} ══  {WorldTime.GetWeatherIcon()} {WorldTime.CurrentWeather}  {WorldTime.TimeOfDay}  Día {WorldTime.Day}  {(SoundSystem.Enabled ? "🔊" : "🔇")}\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("  ── JUGADORES ──"); Console.ResetColor();
            foreach (var p in Players)
            {
                string hpBar = HealthBar(p.Health, p.MaxHealth, 12), mpBar = ManaBar(p.Mana, 200, 8);
                string st = p.HasAnyStatus() ? $" [{p.GetStatusString()}]" : "", pet = p.Pet != null ? $" {p.Pet.Icon}" : "";
                Console.ForegroundColor = p.IsAlive() ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.WriteLine($"  {p.ClassName[0]}  {p.Name,-14} HP:{hpBar}{p.Health,3}/{p.MaxHealth} MP:{mpBar} Lv:{p.Level} 🪙{p.Gold}{pet}{st}{(p.IsAlive() ? "" : "💀")}");
                Console.ResetColor();
            }
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ── ENEMIGOS ──"); Console.ResetColor();
            foreach (var e in enemies)
            {
                string hpBar = HealthBar(e.Health, e.MaxHealth, 10), est = e.HasAnyStatus() ? $" [{e.GetStatusString()}]" : "";
                ConsoleColor col = e is MythicBoss ? ConsoleColor.Magenta : e is LegendaryBoss ? ConsoleColor.DarkMagenta : e is BossEnemy ? ConsoleColor.DarkRed : ConsoleColor.DarkYellow;
                Console.ForegroundColor = col; Console.WriteLine($"  {e.Name,-24} HP:{hpBar}{e.Health,3}/{e.MaxHealth}{est}"); Console.ResetColor();
            }
            Console.WriteLine();
        }

        static void ShowPlayerMenu(Player p)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"  ─── Turno de {p.Name} ({p.ClassName}) Lv.{p.Level} ───"); Console.ResetColor();
            Console.WriteLine($"  1. ⚔  Atacar  [{p.AttackElement}]");
            Console.WriteLine($"  2. ✨ {p.SpecialName} (Coste:{p.SpecialCost}MP)");
            Console.WriteLine($"  3. 🛡 Defender");
            Console.WriteLine($"  4. 🎒 Inventario ({p.Inventory.Count}/{GameConfig.MAX_INVENTORY})");
            Console.WriteLine($"  5. 🏪 Tienda");
            Console.WriteLine($"  6. 💡 Habilidad Extra");
            Console.WriteLine($"  7. 💫 {p.Special2Name} (Coste:{p.SpecialCost2}MP)");
            Console.WriteLine($"  8. 🌟 Talentos (Puntos:{p.Talents.AvailablePoints})");
            Console.WriteLine($"  9. ⚗  Crafteo");
            Console.WriteLine($"  10. 📜 Misiones");
            Console.WriteLine($"  11. 🗺  Mazmorras");
            Console.WriteLine($"  12. 🏰 Gremio");
            Console.WriteLine($"  13. 📊 Estadísticas");
            Console.WriteLine($"  14. 📋 Mi Personaje");
            Console.WriteLine($"  15. 🐾 Compañero{(p.Pet != null ? $" [{p.Pet.Icon}{p.Pet.Name}]" : "")}");
            Console.WriteLine($"  16. {(SoundSystem.Enabled ? "🔇" : "🔊")} Toggle Sonido");
            Console.Write("\n  Acción: ");
        }

        static void ShowDetailedStats(Player p)
        {
            Console.Clear(); PrintBox($"📋 {p.Name.ToUpper()} — {p.ClassName}", ConsoleColor.Cyan);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n  Nivel: {p.Level}  XP:{p.XP}/{p.Level * 50}  HP:{p.Health}/{p.MaxHealth}  MP:{p.Mana}/200");
            Console.WriteLine($"  ATK:{p.BaseAttack + p.AttackBonus} (Base:{p.BaseAttack}+Bonus:{p.AttackBonus})  DEF:{p.Defense}  VEL:{p.Speed}  CRT:{p.CritChance}%");
            Console.WriteLine($"  Elemento: {p.AttackElement}  Oro: {p.Gold}🪙  Muertes: {p.TotalKills}  Rondas: {p.RoundsWon}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  Arma:      {(p.EquippedWeapon != null ? $"[{p.EquippedWeapon.Rarity}] {p.EquippedWeapon.Name}" : "(ninguna)")}");
            Console.WriteLine($"  Armadura:  {(p.EquippedArmor != null ? $"[{p.EquippedArmor.Rarity}] {p.EquippedArmor.Name}" : "(ninguna)")}");
            Console.WriteLine($"  Accesorio: {(p.EquippedAccessory != null ? $"[{p.EquippedAccessory.Rarity}] {p.EquippedAccessory.Name}" : "(ninguno)")}");
            if (p.Pet != null) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n  Compañero: {p.Pet.Icon} {p.Pet.Name} Lv.{p.Pet.Level}"); }
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine($"\n  Talentos:");
            var ul = p.Talents.GetUnlockedTalents();
            if (!ul.Any()) Console.WriteLine("  (Ninguno)"); else foreach (var t in ul) Console.WriteLine($"  ✔ {t}");
            Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\n  Inventario ({p.Inventory.Count}/{GameConfig.MAX_INVENTORY}):");
            if (!p.Inventory.Any()) Console.WriteLine("  (vacío)");
            else foreach (var g in p.Inventory.GroupBy(i => i.Rarity).OrderByDescending(g => g.Key)) { Console.ForegroundColor = GameConfig.RarityColors[(int)g.Key]; Console.WriteLine($"  [{g.Key}]: {string.Join(", ", g.Select(i => i.Name))}"); Console.ResetColor(); }
            Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("\n  Pulsa una tecla..."); Console.ResetColor(); Console.ReadKey(true);
        }

        static string HealthBar(int cur, int max, int w)
        {
            if (max <= 0) return new string('░', w);
            int f = Math.Clamp((int)((float)cur / max * w), 0, w); float pct = (float)cur / max;
            Console.ForegroundColor = pct > .6f ? ConsoleColor.Green : pct > .3f ? ConsoleColor.Yellow : ConsoleColor.Red;
            string bar = new string('█', f) + new string('░', w - f); Console.ResetColor(); return bar;
        }

        static string ManaBar(int cur, int max, int w)
        {
            int f = Math.Clamp((int)((float)cur / max * w), 0, w);
            Console.ForegroundColor = ConsoleColor.Blue; string bar = new string('█', f) + new string('░', w - f); Console.ResetColor(); return bar;
        }

        internal static void PrintBox(string title, ConsoleColor color)
        {
            int len = title.Sum(c => c > 127 ? 2 : 1); len = Math.Max(len, title.Length);
            string line = new('═', len + 4);
            Console.ForegroundColor = color;
            Console.WriteLine($"  ╔{line}╗"); Console.WriteLine($"  ║  {title}  ║"); Console.WriteLine($"  ╚{line}╝");
            Console.ResetColor();
        }

        public static void StaticBattle(List<Enemy> enemies, List<Player> players)
        {
            while (players.Any(p => p.IsAlive()) && enemies.Any())
            {
                foreach (var p in players.Where(p => p.IsAlive()))
                {
                    if (!enemies.Any()) break;
                    p.Tick(); Console.Clear();
                    ShowStatusStatic(enemies, players); ShowPlayerMenu(p);
                    string op = Console.ReadLine() ?? "1";
                    HandlePlayerActionStatic(p, op, enemies, players);
                    enemies.RemoveAll(e => !e.IsAlive());
                }
                if (enemies.Any()) EnemyTurnStatic(enemies, players);
            }
        }

        static void ShowStatusStatic(List<Enemy> enemies, List<Player> players)
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("  ── JUGADORES ──"); Console.ResetColor();
            foreach (var p in players) { Console.ForegroundColor = p.IsAlive() ? ConsoleColor.White : ConsoleColor.DarkGray; Console.WriteLine($"  {p.ClassName[0]} {p.Name,-14} HP:{p.Health}/{p.MaxHealth} MP:{p.Mana} Lv:{p.Level}"); Console.ResetColor(); }
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  ── ENEMIGOS ──"); Console.ResetColor();
            foreach (var e in enemies) Console.WriteLine($"  {e.Name,-22} HP:{e.Health}/{e.MaxHealth}");
        }

        static bool HandlePlayerActionStatic(Player p, string op, List<Enemy> enemies, List<Player> players)
        {
            if (op == "1" && enemies.Any()) { var t = enemies[Rand.Next(enemies.Count)]; int dmg = p.Attack(); t.TakeDamage(dmg, p.AttackElement); SoundSystem.PlayAttack(); Console.WriteLine($"\n  {p.Name} ataca a {t.Name}: {dmg}."); Console.ReadKey(true); return true; }
            if (op == "2" && p.Mana >= p.SpecialCost) { p.Special(enemies); Console.ReadKey(true); return true; }
            return true;
        }

        static void EnemyTurnStatic(List<Enemy> enemies, List<Player> players)
        {
            var alive = players.Where(p => p.IsAlive()).ToList();
            foreach (var e in enemies) { if (!alive.Any()) break; var t = alive[Rand.Next(alive.Count)]; int dmg = e.Attack(); t.TakeDamage(dmg, ElementType.Physical); SoundSystem.PlayEnemyAttack(); Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine($"  {e.Name} → {t.Name}: {dmg}."); Console.ResetColor(); }
            Console.ReadKey(true);
        }
    }
}