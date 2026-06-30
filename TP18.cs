namespace ConsoleApp40
{
    internal class Program
    {
        // EJERCICIO 1
        public struct Punto2D
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Punto2D(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Mostrar()
            {
                Console.WriteLine($"({X}, {Y})");
            }
        }

        // EJERCICIO 2
        public struct Dimensiones
        {
            public int Ancho { get; set; }
            public int Alto { get; set; }

            public Dimensiones(int ancho, int alto)
            {
                Ancho = ancho;
                Alto = alto;
            }
        }

        // EJERCICIO 3
        public struct Producto
        {
            public string Nombre { get; set; }
            public int Codigo { get; set; }
            public double Precio { get; set; }

            public Producto(string nombre, int codigo, double precio)
            {
                Nombre = nombre;
                Codigo = codigo;
                Precio = precio;
            }
        }

        // EJERCICIO 4:
        public struct Estudiante
        {
            public string Nombre { get; set; }
            public double[] Notas { get; set; }

            public Estudiante(string nombre, double[] notas)
            {
                Nombre = nombre;
                Notas = notas;
            }

            public double CalcularPromedio()
            {
                double suma = 0;
                foreach (double nota in Notas)
                {
                    suma += nota;
                }
                return suma / Notas.Length;
            }
        }

        static void Main(string[] args)
        {
            // EJERCICIO 1
            Console.WriteLine("EJERCICIO 1");
            Punto2D punto = new Punto2D(5, 10);
            punto.Mostrar();
            Console.WriteLine();

            // EJERCICIO 2
            Console.WriteLine("EJERCICIO 2");
            Dimensiones d1 = new Dimensiones(10, 20);
            Dimensiones d2 = d1;
            d2.Ancho = 99;

            Console.WriteLine($"d1 - Ancho: {d1.Ancho}, Alto: {d1.Alto}");
            Console.WriteLine($"d2 - Ancho: {d2.Ancho}, Alto: {d2.Alto}");
            // ¿Por qué el ancho de d1 no cambió a 99?
            // Porque los structs son tipos de valor. Cuando asignamos d2 = d1,
            // se crea una COPIA completa de d1 en d2. Por lo tanto, cambios en d2 no afectan a d1.
            Console.WriteLine();

            // EJERCICIO 3
            Console.WriteLine("EJERCICIO 3");
            Producto[] inventario = new Producto[3];

            inventario[0] = new Producto("Laptop", 101, 999.99);
            inventario[1] = new Producto("Mouse", 102, 29.99);
            inventario[2] = new Producto("Teclado", 103, 79.99);

            foreach (Producto producto in inventario)
            {
                Console.WriteLine($"Nombre: {producto.Nombre}, Precio: ${producto.Precio}");
            }
            Console.WriteLine();

            // EJERCICIO 4
            Console.WriteLine("EJERCICIO 4");
            double[] notas = { 8.5, 9.0, 7.5 };
            Estudiante estudiante = new Estudiante("Juan Pérez", notas);

            double promedio = estudiante.CalcularPromedio();
            Console.WriteLine($"Estudiante: {estudiante.Nombre}");
            Console.WriteLine($"Promedio: {promedio:F2}");
        }
    }
}