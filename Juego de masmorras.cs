using System;

class JuegoMazmorra
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        int vidaJugador = 100;
        int pociones = 3;

        Console.WriteLine("=== 🏰 MAZMORRA DEL DESTINO ===");

        while (vidaJugador > 0)
        {
            Console.WriteLine("\nAvanzás en la mazmorra...");
            System.Threading.Thread.Sleep(1000);

            // Crear enemigo
            int vidaEnemigo = random.Next(20, 50);
            Console.WriteLine("⚠️ ¡Aparece un enemigo con " + vidaEnemigo + " HP!");

            // Combate
            while (vidaEnemigo > 0 && vidaJugador > 0)
            {
                Console.WriteLine("\nTu vida: " + vidaJugador);
                Console.WriteLine("1. Atacar");
                Console.WriteLine("2. Curarse (" + pociones + " pociones)");
                Console.WriteLine("3. Escapar");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        int dañoJugador = random.Next(10, 25);
                        int dañoEnemigo = random.Next(5, 20);

                        vidaEnemigo -= dañoJugador;
                        vidaJugador -= dañoEnemigo;

                        Console.WriteLine("⚔️ Hiciste " + dañoJugador + " de daño");
                        Console.WriteLine("💥 El enemigo te hizo " + dañoEnemigo);

                        break;

                    case "2":
                        if (pociones > 0)
                        {
                            int curacion = random.Next(15, 30);
                            vidaJugador += curacion;
                            pociones--;

                            Console.WriteLine("❤️ Te curaste " + curacion);
                        }
                        else
                        {
                            Console.WriteLine("❌ No tenés pociones");
                        }
                        break;

                    case "3":
                        Console.WriteLine("🏃 Escapaste...");
                        vidaEnemigo = 0;
                        break;

                    default:
                        Console.WriteLine("❌ Opción inválida");
                        break;
                }
            }

            if (vidaJugador <= 0)
            {
                break;
            }

            Console.WriteLine("✅ Enemigo derrotado");

            // Recompensa
            if (random.Next(0, 2) == 1)
            {
                pociones++;
                Console.WriteLine("🎁 Encontraste una poción!");
            }
        }

        Console.WriteLine("\n💀 GAME OVER");
        Console.WriteLine("Gracias por jugar");
        Console.ReadKey();
    }
}
