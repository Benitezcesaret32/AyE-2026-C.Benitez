using System;

namespace ConsoleApp62
{
    public class Arbol
    {
        private class Nodo
        {
            public int Valor;
            public Nodo Izquierdo;
            public Nodo Derecho;

            public Nodo(int valor)
            {
                Valor = valor;
            }
        }

        private Nodo raiz;

        public void Insertar(int valor)
        {
            raiz = InsertarRec(raiz, valor);
        }

        private Nodo InsertarRec(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRec(nodo.Derecho, valor);

            return nodo;
        }

        public bool Buscar(int valor)
        {
            return BuscarRec(raiz, valor);
        }

        private bool BuscarRec(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                Console.WriteLine($"No se encontró el valor {valor}.");
                return false;
            }
            if (valor == nodo.Valor)
            {
                Console.WriteLine($"Se encontró el valor {valor}.");
                return true;
            }
            if (valor < nodo.Valor)
                return BuscarRec(nodo.Izquierdo, valor);
            else
                return BuscarRec(nodo.Derecho, valor);
        }

        public int? ObtenerMinimo()
        {
            if (raiz == null) return null;
            Nodo actual = raiz;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;
            return actual.Valor;
        }

        public int? ObtenerMaximo()
        {
            if (raiz == null) return null;
            Nodo actual = raiz;
            while (actual.Derecho != null)
                actual = actual.Derecho;
            return actual.Valor;
        }

        public int ObtenerCantidadNodos()
        {
            return ContarNodos(raiz);
        }

        private int ContarNodos(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarNodos(nodo.Izquierdo) + ContarNodos(nodo.Derecho);
        }

        public int ObtenerAltura()
        {
            return Altura(raiz);
        }

        private int Altura(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
        }

        public int ContarHojas()
        {
            return ContarHojasRec(raiz);
        }

        private int ContarHojasRec(Nodo nodo)
        {
            if (nodo == null) return 0;
            if (nodo.Izquierdo == null && nodo.Derecho == null) return 1;
            return ContarHojasRec(nodo.Izquierdo) + ContarHojasRec(nodo.Derecho);
        }

        public void Eliminar(int valor)
        {
            raiz = EliminarRec(raiz, valor);
        }

        private Nodo EliminarRec(Nodo nodo, int valor)
        {
            if (nodo == null) return null;

            if (valor < nodo.Valor)
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = EliminarRec(nodo.Derecho, valor);
            else
            {
                if (nodo.Izquierdo == null) return nodo.Derecho;
                if (nodo.Derecho == null) return nodo.Izquierdo;

                nodo.Valor = MinValor(nodo.Derecho);
                nodo.Derecho = EliminarRec(nodo.Derecho, nodo.Valor);
            }
            return nodo;
        }

        private int MinValor(Nodo nodo)
        {
            int minv = nodo.Valor;
            while (nodo.Izquierdo != null)
            {
                minv = nodo.Izquierdo.Valor;
                nodo = nodo.Izquierdo;
            }
            return minv;
        }

        public bool EsValido()
        {
            return EsValidoRec(raiz, int.MinValue, int.MaxValue);
        }

        private bool EsValidoRec(Nodo nodo, int min, int max)
        {
            if (nodo == null) return true;
            if (nodo.Valor < min || nodo.Valor > max) return false;
            return EsValidoRec(nodo.Izquierdo, min, nodo.Valor - 1) &&
                   EsValidoRec(nodo.Derecho, nodo.Valor + 1, max);
        }
    }
}
