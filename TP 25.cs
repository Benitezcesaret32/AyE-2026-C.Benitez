
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace Pokedex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ruta = Path.Combine(
                Directory.GetCurrentDirectory(),
                "pokemons.json"
            );

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    List<Pokemon> pokemons = db.Pokemons
                        .OrderBy(p => p.Id)
                        .ToList();

                    string json = JsonSerializer.Serialize(
                        pokemons,
                        new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }
                    );

                    File.WriteAllText(ruta, json);

                    Console.WriteLine("Archivo JSON creado correctamente.");
                    Console.WriteLine("Pokemon guardados: " + pokemons.Count);
                }

                string contenido = File.ReadAllText(ruta);

                List<Pokemon> listaPokemon =
                    JsonSerializer.Deserialize<List<Pokemon>>(contenido);

                Console.WriteLine();
                Console.WriteLine("POKEMON LEIDOS DESDE JSON");

                foreach (Pokemon pokemon in listaPokemon)
                {
                    Console.WriteLine(pokemon);
                }

                Console.WriteLine();
                Console.WriteLine("Total de Pokemon en la lista: " +
                    listaPokemon.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para salir.");
            Console.ReadLine();
        }
    }
}
