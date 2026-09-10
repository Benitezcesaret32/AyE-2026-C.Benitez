using System;

internal class Program
{
    static void Main(string[] args)
    {
        Arbol arbolito = new Arbol();
        arbolito.Insertar(10);
        arbolito.Insertar(5);
        arbolito.Insertar(15);
        arbolito.Insertar(3);
        arbolito.Insertar(7);

        Console.WriteLine("=== Resultados de los ejercicios 1-6 ===");

        Console.WriteLine("Buscar 7 y 20:");
        arbolito.Buscar(7);
        arbolito.Buscar(20);

        int? minimo = arbolito.ObtenerMinimo();
        if (minimo.HasValue)
        {
            Console.WriteLine($"1) Mínimo: {minimo.Value}");
        }
        else
        {
            Console.WriteLine("1) Mínimo: árbol vacío");
        }

        int? maximo = arbolito.ObtenerMaximo();
        if (maximo.HasValue)
        {
            Console.WriteLine($"1) Máximo: {maximo.Value}");
        }
        else
        {
            Console.WriteLine("1) Máximo: árbol vacío");
        }

        Console.WriteLine($"2) Cantidad de nodos: {arbolito.ObtenerCantidadNodos()}");
        Console.WriteLine($"3) Altura del árbol: {arbolito.ObtenerAltura()}");
        Console.WriteLine($"4) Cantidad de hojas: {arbolito.ContarHojas()}");

        Console.WriteLine("5) Eliminar: eliminaré el valor 5 y mostraré la nueva cantidad de nodos:");
        arbolito.Eliminar(5);
        Console.WriteLine($"   Cantidad de nodos después de eliminar 5: {arbolito.ObtenerCantidadNodos()}");

        Console.WriteLine($"6) Es válido BST: {arbolito.EsValido()}");

        Console.WriteLine("Pulse una tecla para salir...");
        Console.ReadKey();
    }
}
