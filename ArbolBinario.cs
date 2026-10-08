using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoleadoresMundial;

namespace GoleadoresMundial
{
    public class Nodo
    {
        public Jugador Jugador;
        public Nodo Izquierda;
        public Nodo Derecha;

        public Nodo(Jugador jugador)
        {
            Jugador = jugador;
            Izquierda = null;
            Derecha = null;
        }
    }

    public class ArbolBinario
    {
        public Nodo Raiz;

        public void Insertar(Jugador jugador)
        {
            Raiz = InsertarNodo(Raiz, jugador);
        }

        private Nodo InsertarNodo(Nodo nodo, Jugador jugador)
        {
            if (nodo == null)
            {
                return new Nodo(jugador);
            }

            int comparacion = string.Compare(
                jugador.apellido,
                nodo.Jugador.apellido,
                StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                nodo.Izquierda =
                    InsertarNodo(nodo.Izquierda, jugador);
            }
            else
            {
                nodo.Derecha =
                    InsertarNodo(nodo.Derecha, jugador);
            }

            return nodo;
        }

        public Jugador Buscar(string apellido)
        {
            Nodo actual = Raiz;

            while (actual != null)
            {
                int comparacion = string.Compare(
                    apellido,
                    actual.Jugador.apellido,
                    StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    return actual.Jugador;
                }

                if (comparacion < 0)
                {
                    actual = actual.Izquierda;
                }
                else
                {
                    actual = actual.Derecha;
                }
            }

            return null;
        }

        public void Mostrar()
        {
            MostrarEnOrden(Raiz);
        }

        private void MostrarEnOrden(Nodo nodo)
        {
            if (nodo != null)
            {
                MostrarEnOrden(nodo.Izquierda);

                Console.WriteLine(nodo.Jugador);

                MostrarEnOrden(nodo.Derecha);
            }
        }
    }
}