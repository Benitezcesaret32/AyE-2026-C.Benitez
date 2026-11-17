using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp11
{
    internal class Program
    {
        public struct Ubicacion
        {
            public int x { get; set; }
            public int y { get; set; }
            public string NombreZona { get; set; }

            public Ubicacion(int x, int y, string NombreZona)
            {
                this.x = x;
                this.y = y;
                this.NombreZona = NombreZona;
            }

            public void llenarcampos()
            {
                Console.WriteLine("Ingrese la coordenada x:");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese la coordenada y:");
                y = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese el nombre de la zona:");
                NombreZona = Console.ReadLine();
            }

        }
        static void Main(string[] args)
        {
         Stack<Ubicacion> historial = new Stack<Ubicacion>();
         bool continuar = true;
         historial.Push(new Ubicacion(264, 948, "Bosque"));
         historial.Push(new Ubicacion(47, 94, "desierto"));
         historial.Push(new Ubicacion(611, 304, "desierto"));
            while (continuar)
            {
                Console.WriteLine("Ingrese una opción:");
                Console.WriteLine("1. Agregar ubicación");
                Console.WriteLine("2. Mostrar el ultimo del historial ");
                Console.WriteLine("3. Quitar el ultimo del historial");
                Console.WriteLine("4. Salir");
                int opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Ubicacion ubicacion = new Ubicacion();
                        ubicacion.llenarcampos();
                        historial.Push(ubicacion);
                        break;
                    case 2:
                        Ubicacion ultimo = historial.Peek();
                        Console.WriteLine($"Última ubicación: x={ultimo.x}, y={ultimo.y}, NombreZona={ultimo.NombreZona}");
                        break;
                    case 3:
                        Ubicacion eliminado = historial.Pop();
                        Console.WriteLine($"Última ubicación eliminada del historial: x={eliminado.x}, y={eliminado.y}, NombreZona={eliminado.NombreZona}");
                        break;
                    case 4:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }
              
        }
    }
}
