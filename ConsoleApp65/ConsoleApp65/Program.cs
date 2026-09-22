using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ConsoleApp65
{
    struct Punto2D
    {
        public int X;
        public int Y;

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

    class Punto
    {
        public int id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        static void Main()
        {
            using (AppDbContext db = new AppDbContext())
            {
                Console.WriteLine("Ingrese el primer punto:");
                Console.Write("X: ");
                int x1 = int.Parse(Console.ReadLine());

                Console.Write("Y: ");
                int y1 = int.Parse(Console.ReadLine());

                Punto2D punto1 = new Punto2D(x1, y1);


                Console.WriteLine("Ingrese el segundo punto:");
                Console.Write("X: ");
                int x2 = int.Parse(Console.ReadLine());

                Console.Write("Y: ");
                int y2 = int.Parse(Console.ReadLine());

                Punto2D punto2 = new Punto2D(x2, y2);


                Console.WriteLine("Ingrese el tercer punto:");
                Console.Write("X: ");
                int x3 = int.Parse(Console.ReadLine());

                Console.Write("Y: ");
                int y3 = int.Parse(Console.ReadLine());

                Punto2D punto3 = new Punto2D(x3, y3);


                // Mostrar los puntos

                punto1.Mostrar();
                punto2.Mostrar();
                punto3.Mostrar();


                // Insertar los 3 puntos

                db.Punto.Add(new Punto
                {
                    X = punto1.X,
                    Y = punto1.Y
                });

                db.Punto.Add(new Punto
                {
                    X = punto2.X,
                    Y = punto2.Y
                });

                db.Punto.Add(new Punto
                {
                    X = punto3.X,
                    Y = punto3.Y
                });

                db.SaveChanges();


                // Actualizar el tercer punto

                Console.WriteLine("Ingrese los nuevos datos del tercer punto:");

                Console.Write("Nuevo X: ");
                int nuevoX = int.Parse(Console.ReadLine());

                Console.Write("Nuevo Y: ");
                int nuevoY = int.Parse(Console.ReadLine());

                Punto tercerPunto = db.Punto
                    .OrderByDescending(p => p.id)
                    .First();

                tercerPunto.X = nuevoX;
                tercerPunto.Y = nuevoY;

                db.SaveChanges();


                // Borrar el primer punto

                Punto primerPunto = db.Punto
                    .OrderBy(p => p.id)
                    .First();

                db.Punto.Remove(primerPunto);

                db.SaveChanges();


                // Mostrar todos los puntos

                Console.WriteLine("Puntos de la base de datos:");

                foreach (Punto punto in db.Punto)
                {
                    Console.WriteLine(
                        $"ID: {punto.id} - ({punto.X}, {punto.Y})"
                    );
                }
            }
        }
    }
}