using System;
using System.Collections.Generic;

namespace GoleadoresMundial
{
    public class Ordenamientos
    {
        public static void BurbujaAlfabetico(
            List<Jugador> jugadores)
        {
            for (int i = 0;
                 i < jugadores.Count - 1;
                 i++)
            {
                for (int j = 0;
                     j < jugadores.Count - 1 - i;
                     j++)
                {
                    string jugador1 =
                        jugadores[j].apellido +
                        " " +
                        jugadores[j].nombre;

                    string jugador2 =
                        jugadores[j + 1].apellido +
                        " " +
                        jugadores[j + 1].nombre;

                    if (string.Compare(
                        jugador1,
                        jugador2,
                        StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        Jugador auxiliar = jugadores[j];

                        jugadores[j] =
                            jugadores[j + 1];

                        jugadores[j + 1] =
                            auxiliar;
                    }
                }
            }
        }

        public static void BurbujaMundiales(
            List<Jugador> jugadores)
        {
            for (int i = 0;
                 i < jugadores.Count - 1;
                 i++)
            {
                for (int j = 0;
                     j < jugadores.Count - 1 - i;
                     j++)
                {
                    if (jugadores[j].mundiales_jugados >
                        jugadores[j + 1].mundiales_jugados)
                    {
                        Jugador auxiliar = jugadores[j];

                        jugadores[j] =
                            jugadores[j + 1];

                        jugadores[j + 1] =
                            auxiliar;
                    }
                }
            }
        }
    }
}
