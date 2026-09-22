using System;

namespace ConsoleApp67;

class Program
{
    static void Main(string[] args)
    {
        Producto[] productos = new Producto[3];

        productos[0] = new Producto
        {
            Nombre = "Mouse",
            Codigo = 101,
            Precio = 15000
        };

        productos[1] = new Producto
        {
            Nombre = "Teclado",
            Codigo = 102,
            Precio = 25000
        };

        productos[2] = new Producto
        {
            Nombre = "Auriculares",
            Codigo = 103,
            Precio = 20000
        };

        Console.WriteLine("INVENTARIO");
        Console.WriteLine();

        foreach (Producto producto in productos)
        {
            Console.WriteLine("Nombre: " + producto.Nombre);
            Console.WriteLine("Precio: $" + producto.Precio);
            Console.WriteLine();
        }

        using (AppDbContext db = new AppDbContext())
        {
            foreach (Producto producto in productos)
            {
                ProductoBD productoBD = new ProductoBD
                {
                    Nombre = producto.Nombre,
                    Codigo = producto.Codigo,
                    Precio = producto.Precio
                };

                db.Productos.Add(productoBD);
            }

            db.SaveChanges();
        }

        Console.WriteLine("Los productos fueron guardados en la base de datos.");
    }
}