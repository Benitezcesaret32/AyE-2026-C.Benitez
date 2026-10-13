namespace Prueba_Base_de_Datos
{
    public class Nodo
    {
        public Agente valor { get; set; }
        public Nodo izquierdo { get; set; }
        public Nodo derecho { get; set; }

        public Nodo(Agente valor)
        {
            this.valor = valor;
            izquierdo = null;
            derecho = null;
        }
    }
}