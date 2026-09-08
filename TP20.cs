using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp61
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var browser = new BrowserHistory();
            var editManager = new EditManager();
            var taskProcessor = new TaskProcessor();
            var rpnEvaluator = new RpnEvaluator();

            while (true)
            {
                Console.WriteLine("Opciones:");
                Console.WriteLine("1 - Invertir palabra");
                Console.WriteLine("2 - Historial de navegador");
                Console.WriteLine("3 - Comprobar delimitadores");
                Console.WriteLine("4 - Gestor de edición");
                Console.WriteLine("5 - Evaluar expresión RPN");
                Console.WriteLine("6 - Procesador de tareas");
                Console.WriteLine("0 - Salir");
                Console.Write("Opción: ");
                var opt = Console.ReadLine();
                if (opt == null) break;
                if (opt == "0") break;

                switch (opt)
                {
                    case "1":
                        Console.Write("Texto a invertir: ");
                        string input = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine(ReverseWithQueue(input));
                        break;

                    case "2":
                        Console.WriteLine("Comandos: 'visitar <url>' o 'atras' (escribir 'salir' para volver al menú)");
                        BrowserScenario(browser);
                        break;

                    case "3":
                        Console.Write("Expresión: ");
                        string expression = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine(CheckDelimiters(expression) ? "True" : "False");
                        break;

                    case "4":
                        Console.WriteLine("Comandos: 'r' aplicar, 'd' deshacer, 'v' ver documento, 's' salir");
                        EditScenario(editManager);
                        break;

                    case "5":
                        Console.Write("Expresión RPN (tokens separados por espacio): ");
                        string rpn = Console.ReadLine() ?? string.Empty;
                        try
                        {
                            double result = rpnEvaluator.Evaluate(rpn);
                            Console.WriteLine(result);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "6":
                        Console.WriteLine("Comandos: 'a' añadir, 'v' ver tope, 't' atender, 's' salir (FIFO)");
                        TaskScenario(taskProcessor);
                        break;

                    default:
                        Console.WriteLine("Opción no reconocida.");
                        break;
                }
            }
        }

        static string ReverseWithQueue(string s)
        {
            var q = new Queue<char>();
            foreach (var c in s) q.Enqueue(c);

            var sb = new StringBuilder();
            int k = q.Count;
            while (k > 0)
            {
                for (int i = 0; i < k - 1; i++)
                    q.Enqueue(q.Dequeue());
                sb.Append(q.Dequeue());
                k--;
            }
            return sb.ToString();
        }
        static void BrowserScenario(BrowserHistory browser)
        {
            while (true)
            {
                Console.Write("Comando > ");
                var line = Console.ReadLine() ?? string.Empty;
                var parts = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                var cmd = parts[0].ToLowerInvariant();
                if (cmd == "salir") break;
                if (cmd == "visitar" && parts.Length == 2)
                {
                    browser.Visit(parts[1]);
                    Console.WriteLine(browser.Current ?? string.Empty);
                }
                else if (cmd == "atras")
                {
                    browser.Back();
                    Console.WriteLine(browser.Current ?? string.Empty);
                }
                else
                {
                    Console.WriteLine("Comando no reconocido.");
                }
            }
        }

        static bool CheckDelimiters(string expression)
        {
            var stack = new Stack<char>();
            foreach (char c in expression)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0) return false;
                    char open = stack.Pop();
                    if ((c == ')' && open != '(') ||
                        (c == ']' && open != '[') ||
                        (c == '}' && open != '{'))
                        return false;
                }
            }
            return stack.Count == 0;
        }

        static void EditScenario(EditManager manager)
        {
            while (true)
            {
                Console.Write("Comando (r/d/v/s) > ");
                var cmd = Console.ReadLine();
                if (string.IsNullOrEmpty(cmd)) continue;
                var lower = cmd.ToLowerInvariant();
                if (lower == "s") break;
                if (lower == "r")
                {
                    Console.Write("Tipo (insertar/eliminar/reemplazar): ");
                    var tipo = Console.ReadLine() ?? string.Empty;
                    Console.Write("Contenido: ");
                    var contenido = Console.ReadLine() ?? string.Empty;
                    var accion = new AccionTexto(ParseTipoAccion(tipo), contenido, DateTime.Now);
                    manager.ApplyAction(accion);
                }
                else if (lower == "d")
                {
                    var undone = manager.UndoLast();
                    if (undone != null) Console.WriteLine($"{undone.Tipo} - {undone.Contenido}");
                    else Console.WriteLine("Nada que deshacer.");
                }
                else if (lower == "v")
                {
                    Console.WriteLine(manager.Document);
                }
                else
                {
                    Console.WriteLine("Comando no reconocido.");
                }
            }
        }

        static AccionTexto.TipoAccion ParseTipoAccion(string s)
        {
            return s?.ToLower() switch
            {
                "insertar" => AccionTexto.TipoAccion.Insertar,
                "eliminar" => AccionTexto.TipoAccion.Eliminar,
                "reemplazar" => AccionTexto.TipoAccion.Reemplazar,
                _ => AccionTexto.TipoAccion.Insertar
            };
        }

        static void TaskScenario(TaskProcessor processor)
        {
            while (true)
            {
                Console.Write("Comando (a/v/t/s) > ");
                var cmd = Console.ReadLine();
                if (string.IsNullOrEmpty(cmd)) continue;
                var lower = cmd.ToLowerInvariant();
                if (lower == "s") break;
                if (lower == "a")
                {
                    Console.Write("Id: ");
                    var id = Console.ReadLine() ?? string.Empty;
                    Console.Write("Título: ");
                    var title = Console.ReadLine() ?? string.Empty;
                    Console.Write("Prioridad (Alta/Media/Baja): ");
                    var p = Console.ReadLine() ?? "Media";
                    Console.Write("Estimación minutos: ");
                    if (!int.TryParse(Console.ReadLine(), out int mins)) mins = 30;
                    var tarea = new Tarea(id, title, ParsePriority(p), mins);
                    processor.Push(tarea);
                }
                else if (lower == "v")
                {
                    var top = processor.Peek();
                    if (top != null) Console.WriteLine($"{top.Id}|{top.Titulo}|{top.Prioridad}|{top.EstimacionMinutos}");
                    else Console.WriteLine("No hay tareas.");
                }
                else if (lower == "t")
                {
                    var atendida = processor.Pop();
                    if (atendida != null) Console.WriteLine($"{atendida.Id}|{atendida.Titulo}");
                    else Console.WriteLine("No hay tareas para atender.");
                }
                else
                {
                    Console.WriteLine("Comando no reconocido.");
                }
            }
        }

        static Tarea.PrioridadTarea ParsePriority(string s)
        {
            return s?.ToLower() switch
            {
                "alta" => Tarea.PrioridadTarea.Alta,
                "media" => Tarea.PrioridadTarea.Media,
                "baja" => Tarea.PrioridadTarea.Baja,
                _ => Tarea.PrioridadTarea.Media
            };
        }
    }

    public class BrowserHistory
    {
        private readonly Queue<string> _history = new Queue<string>();

        public string? Current => _history.Count == 0 ? null : _history.Peek();

        public void Visit(string url) => _history.Enqueue(url);
        public void Back() { if (_history.Count > 0) _history.Dequeue(); }
    }

    public class AccionTexto
    {
        public enum TipoAccion { Insertar, Eliminar, Reemplazar }

        public TipoAccion Tipo { get; }
        public string Contenido { get; }
        public DateTime FechaHora { get; }

        public AccionTexto(TipoAccion tipo, string contenido, DateTime fechaHora)
        {
            Tipo = tipo;
            Contenido = contenido;
            FechaHora = fechaHora;
        }
    }

    public class EditManager
    {
        private readonly Stack<AccionTexto> _historial = new Stack<AccionTexto>();
        private readonly StringBuilder _documento = new StringBuilder();

        public int HistoryCount => _historial.Count;
        public string Document => _documento.ToString();

        public void ApplyAction(AccionTexto accion)
        {
            _historial.Push(accion);
            switch (accion.Tipo)
            {
                case AccionTexto.TipoAccion.Insertar:
                    _documento.Append(accion.Contenido);
                    break;
                case AccionTexto.TipoAccion.Eliminar:
                    if (_documento.Length >= accion.Contenido.Length)
                        _documento.Remove(_documento.Length - accion.Contenido.Length, accion.Contenido.Length);
                    break;
                case AccionTexto.TipoAccion.Reemplazar:
                    _documento.Clear();
                    _documento.Append(accion.Contenido);
                    break;
            }
        }

        public AccionTexto? UndoLast()
        {
            if (_historial.Count == 0) return null;
            var accion = _historial.Pop();
            switch (accion.Tipo)
            {
                case AccionTexto.TipoAccion.Insertar:
                    if (_documento.Length >= accion.Contenido.Length)
                        _documento.Remove(_documento.Length - accion.Contenido.Length, accion.Contenido.Length);
                    break;
                case AccionTexto.TipoAccion.Eliminar:
                    _documento.Append(accion.Contenido);
                    break;
                case AccionTexto.TipoAccion.Reemplazar:
                    _documento.Clear();
                    break;
            }
            return accion;
        }
    }

    public class RpnEvaluator
    {
        public double Evaluate(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr)) throw new ArgumentException("Expresión vacía");

            var stack = new Stack<double>();
            var tokens = expr.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    stack.Push(num);
                }
                else
                {
                    if (stack.Count < 2) throw new InvalidOperationException("Operandos insuficientes");
                    double b = stack.Pop();
                    double a = stack.Pop();
                    double res = token switch
                    {
                        "+" => a + b,
                        "-" => a - b,
                        "*" => a * b,
                        "/" => b == 0 ? throw new DivideByZeroException() : a / b,
                        "^" => Math.Pow(a, b),
                        _ => throw new InvalidOperationException($"Operador desconocido: {token}")
                    };
                    stack.Push(res);
                }
            }

            if (stack.Count != 1) throw new InvalidOperationException("Expresión RPN inválida");
            return stack.Pop();
        }
    }

    public class Tarea
    {
        public enum PrioridadTarea { Baja, Media, Alta }

        public string Id { get; }
        public string Titulo { get; }
        public PrioridadTarea Prioridad { get; }
        public int EstimacionMinutos { get; }

        public Tarea(string id, string titulo, PrioridadTarea prioridad, int estimacionMinutos)
        {
            Id = id;
            Titulo = titulo;
            Prioridad = prioridad;
            EstimacionMinutos = estimacionMinutos;
        }
    }

    public class TaskProcessor
    {
        private readonly Queue<Tarea> _tareas = new Queue<Tarea>();

        public void Push(Tarea tarea) => _tareas.Enqueue(tarea);
        public Tarea? Peek() => _tareas.Count > 0 ? _tareas.Peek() : null;
        public Tarea? Pop() => _tareas.Count > 0 ? _tareas.Dequeue() : null;
    }
}
