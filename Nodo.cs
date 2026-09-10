using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp62
{
    public class Nodo
    { 
        public int valor { get; set; }
        public Nodo izquierdo { get; set; }
        public Nodo derecho { get; set; }
        public Nodo(int valor)
        {
            valor = valor;
            izquierdo = null;
            derecho = null;
        }
    }
}
