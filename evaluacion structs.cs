namespace qqq
{
    internal class Program
    {
        struct Personaje
        {
            public int VidaTotal;
            public int VidaActual;
            public string UltimaAccion;

            public Personaje(int vidaTotal, int vidaActual, string ultimaAccion)
            {
                VidaTotal = vidaTotal;
                VidaActual = vidaActual;
                UltimaAccion = ultimaAccion;
            }
        }

        const int vidatotal = 100;

        static void Main(string[] args)
        {
            var historialDelPersonaje = new Stack<Personaje>();
            Personaje p1 = new Personaje(vidatotal, vidatotal, "Inicio");
            Personaje p2 = new Personaje(vidatotal, vidatotal - 20, "Golpe recibido");
            Personaje p3 = new Personaje(vidatotal, vidatotal - 40, "Golpe recibido");

            historialDelPersonaje.Push(p1);
            historialDelPersonaje.Push(p2);
            historialDelPersonaje.Push(p3);

            Console.WriteLine("Historial del personaje:");
            foreach (var personaje in historialDelPersonaje)
            {
                Console.WriteLine($"Vida Total: {personaje.VidaTotal}, Vida Actual: {personaje.VidaActual}, Última Acción: {personaje.UltimaAccion}");
            }

            volverEnElTiempo(historialDelPersonaje);
            golpear(historialDelPersonaje);
        }

        static void volverEnElTiempo(Stack<Personaje> historial)
        {
            if (historial.Count > 0)
            {
                var ultimoCambio = historial.Pop();
                Console.WriteLine();
                Console.WriteLine($"Última acción borrada: {ultimoCambio.UltimaAccion}");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("El historial está vacío, no hay acción para retroceder.");
            }
        }

        static void golpear(Stack<Personaje> historial)
        {
            if (historial.Count > 0)
            {
                var top = historial.Peek();
                int nuevaVida = top.VidaActual - 20;
                if (nuevaVida < 0) nuevaVida = 0;
                var nuevoCambio = new Personaje(top.VidaTotal, nuevaVida, "Golpe recibido");
                historial.Push(nuevoCambio);
                Console.WriteLine();
                Console.WriteLine("Golpe recibido. Se añadió una nueva acción al historial.");
            }
            else
            {
                int nuevaVida = vidatotal - 20;
                if (nuevaVida < 0) nuevaVida = 0;
                var nuevoCambio = new Personaje(vidatotal, nuevaVida, "Golpe recibido");
                historial.Push(nuevoCambio);
                Console.WriteLine();
                Console.WriteLine("Historial vacío: se creó un nuevo registro con 'Golpe recibido'.");
            }
        }
    }
}
