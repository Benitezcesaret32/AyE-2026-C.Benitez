namespace Pokedex
{
    public class ArbolBinario
    {
        public Nodo Raiz { get; set; }

        public ArbolBinario()
        {
            Raiz = null;
        }
        public void Insertar(Pokemon pokemon)
        {
            Raiz = InsertarRecursivo(Raiz, pokemon);
        }

        private Nodo InsertarRecursivo(Nodo nodo, Pokemon pokemon)
        {
            if (nodo == null)
            {
                return new Nodo(pokemon);
            }

            if (pokemon.Id < nodo.Pokemon.Id)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, pokemon);
            }
            else if (pokemon.Id > nodo.Pokemon.Id)
            {
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, pokemon);
            }

            return nodo;
        }
        public Pokemon Buscar(int id)
        {
            return BuscarRecursivo(Raiz, id);
        }

        private Pokemon BuscarRecursivo(Nodo nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (id == nodo.Pokemon.Id)
            {
                return nodo.Pokemon;
            }

            if (id < nodo.Pokemon.Id)
            {
                return BuscarRecursivo(nodo.Izquierda, id);
            }

            return BuscarRecursivo(nodo.Derecha, id);
        }
        public void MostrarEnOrden()
        {
            MostrarEnOrdenRecursivo(Raiz);
        }

        private void MostrarEnOrdenRecursivo(Nodo nodo)
        {
            if (nodo == null)
            {
                return;
            }

            MostrarEnOrdenRecursivo(nodo.Izquierda);

            Console.WriteLine(nodo.Pokemon);

            MostrarEnOrdenRecursivo(nodo.Derecha);
        }
        public void Limpiar()
        {
            Raiz = null;
        }
    }
}