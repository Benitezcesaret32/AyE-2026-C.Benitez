using System.Text;
namespace ConsoleApp56
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var history = new Stack<string>();
            var editManager = new EditManager();
            var taskProcessor = new TaskProcessor();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menú - Elija una opción:");
                Console.WriteLine("1) Invertir frase");
                Console.WriteLine("2) Simulador 'Atrás' navegador");
                Console.WriteLine("3) Verificar delimitadores");
                Console.WriteLine("4) Gestor de edición (Registrar/Deshacer)");
                Console.WriteLine("5) Calculadora RPN");
                Console.WriteLine("6) Procesador de Tareas (Push/Peek/Atender)");
                Console.WriteLine("0) Salir");
                Console.Write("Opción: ");
                var opt = Console.ReadLine();
                Console.WriteLine();

                if (opt == "0") break;

                switch (opt)
                {
                    case "1":
                        Console.Write("Ingrese una palabra o frase: ");
                        string input = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"Frase invertida: {ReverseWithStack(input)}");
                        break;

                    case "2":
                        BrowserSimulator(history);
                        break;

                    case "3":
                        Console.Write("Ingrese una expresión para verificar delimitadores: ");
                        string expression = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"La expresión es válida: {CheckDelimiters(expression)}");
                        break;

                    case "4":
                        EditScenario(editManager);
                        break;

                    case "5":
                        Console.Write("Ingrese expresión RPN (operandos y operadores separados por espacios): ");
                        string rpn = Console.ReadLine() ?? string.Empty;
                        try
                        {
                            double result = EvaluateRPN(rpn);
                            Console.WriteLine($"Resultado: {result}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "6":
                        TaskScenario(taskProcessor);
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        // 1) Invertir palabra
        static string ReverseWithStack(string s)
        {
            var st = new Stack<string>();
            for (int i = 0; i < s.Length; i++)
            {
                st.Push(s[i].ToString());
            }

            string resultado = "";
            while (st.Count > 0)
            {
                st.TryPop(out string pieza);
                resultado = resultado + pieza;
            }

            return resultado;
        }

        // 2) Simulador "Atrás"
        static void BrowserSimulator(Stack<string> history)
        {
            while (true)
            {
                Console.Write("Ingrese una URL para visitar o 'atras' para retroceder o 'salir' para volver al menú: ");
                string url = Console.ReadLine() ?? string.Empty;
                string lower = url.ToLowerInvariant();

                if (lower == "salir") break;
                if (lower == "atras")
                {
                    if (history.Count > 0) history.Pop();
                    if (history.Count > 0) Console.WriteLine($"URL actual: {history.Peek()}");
                    else Console.WriteLine("No hay páginas anteriores.");
                }
                else
                {
                    history.Push(url);
                    Console.WriteLine($"URL actual: {url}");
                    break;
                }
            }
        }

        // 3) Comprobar delimitadores
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
                    var open = stack.Pop();
                    if ((c == ')' && open != '(') ||
                        (c == ']' && open != '[') ||
                        (c == '}' && open != '{'))
                        return false;
                }
            }
            return stack.Count == 0;
        }

        // 4) EditManager y AccionTexto - EditScenario
        static void EditScenario(EditManager manager)
        {
            while (true)
            {
                Console.WriteLine("Gestor edición - 'r' Registrar, 'd' Deshacer, 'v' Ver documento, 's' Salir");
                var cmd = Console.ReadLine();
                if (string.IsNullOrEmpty(cmd)) continue;
                string lower = cmd.ToLowerInvariant();
                if (lower == "s") break;
                if (lower == "r")
                {
                    Console.Write("Tipo (insertar/eliminar/reemplazar): ");
                    var tipo = Console.ReadLine() ?? string.Empty;
                    Console.Write("Contenido: ");
                    var contenido = Console.ReadLine() ?? string.Empty;
                    var accion = new AccionTexto(ParseTipoAccion(tipo), contenido, DateTime.Now);
                    manager.ApplyAction(accion);
                    Console.WriteLine("Acción registrada.");
                }
                else if (lower == "d")
                {
                    if (manager.HistoryCount == 0) Console.WriteLine("No hay acciones para deshacer.");
                    else
                    {
                        var undone = manager.UndoLast();
                        Console.WriteLine($"Deshecho: {undone.Tipo} - {undone.Contenido}");
                    }
                }
                else if (lower == "v")
                {
                    Console.WriteLine("Documento actual:");
                    Console.WriteLine(manager.Document);
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

        // 5) Calculadora RPN
        static double EvaluateRPN(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                throw new InvalidOperationException("Expresión RPN vacía.");

            var st = new Stack<double>();
            int i = 0;
            int len = expr.Length;

            while (i < len)
            {
                while (i < len && expr[i] == ' ') i++;
                if (i >= len) break;
                string token = "";
                while (i < len && expr[i] != ' ')
                {
                    token = token + expr[i];
                    i++;
                }

                if (double.TryParse(token, out double num))
                {
                    st.Push(num);
                    continue;
                }
                if (token == "+" || token == "-" || token == "*" || token == "/" || token == "^")
                {
                    if (!st.TryPop(out double b) || !st.TryPop(out double a))
                        throw new InvalidOperationException("Operando(s) faltante(s) en la expresión.");

                    if (token == "+") st.Push(a + b);
                    else if (token == "-") st.Push(a - b);
                    else if (token == "*") st.Push(a * b);
                    else if (token == "/")
                    {
                        if (b == 0) throw new DivideByZeroException("División por cero.");
                        st.Push(a / b);
                    }
                    else
                    {
                        int exp = (int)b;
                        if (b != exp) throw new InvalidOperationException("^ solo admite exponentes enteros en esta versión.");
                        double res = 1.0;
                        int absExp = exp < 0 ? -exp : exp;
                        for (int k = 0; k < absExp; k++) res = res * a;
                        if (exp < 0)
                        {
                            if (res == 0) throw new DivideByZeroException("División por cero al calcular potencia negativa.");
                            res = 1.0 / res;
                        }
                        st.Push(res);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Operador desconocido: " + token);
                }
            }

            if (!st.TryPop(out double result) || st.Count != 0)
                throw new InvalidOperationException("Expresión RPN inválida.");

            return result;
        }

        // 6) Procesador de Tareas
        static void TaskScenario(TaskProcessor processor)
        {
            while (true)
            {
                Console.WriteLine("Tareas - 'a' Añadir, 'v' Ver cima, 't' Atender, 's' Salir");
                var cmd = Console.ReadLine();
                if (string.IsNullOrEmpty(cmd)) continue;
                string lower = cmd.ToLowerInvariant();
                if (lower == "s") break;
                if (lower == "a")
                {
                    Console.Write("Id: ");
                    var id = Console.ReadLine() ?? string.Empty;
                    Console.Write("Título: ");
                    var title = Console.ReadLine() ?? string.Empty;
                    Console.Write("Prioridad (Baja/Media/Alta): ");
                    var p = Console.ReadLine() ?? "Media";
                    Console.Write("Estimación minutos: ");
                    if (!int.TryParse(Console.ReadLine(), out int mins)) mins = 30;
                    var tarea = new Tarea(id, title, ParsePriority(p), mins);
                    processor.Push(tarea);
                    Console.WriteLine("Tarea añadida.");
                }
                else if (lower == "v")
                {
                    var top = processor.Peek();
                    if (top == null) Console.WriteLine("No hay tareas.");
                    else Console.WriteLine($"Cima: {top.Id} - {top.Titulo} [{top.Prioridad}] ({top.EstimacionMinutos} mins)");
                }
                else if (lower == "t")
                {
                    var atendida = processor.Pop();
                    if (atendida != null) Console.WriteLine($"Atendida: {atendida.Id} - {atendida.Titulo}");
                    else Console.WriteLine("No hay tareas para atender.");
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

    public class AccionTexto
    {
        public enum TipoAccion
        {
            Insertar,
            Eliminar,
            Reemplazar
        }

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

        public AccionTexto UndoLast()
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

    public class Tarea
    {
        public enum PrioridadTarea
        {
            Baja,
            Media,
            Alta
        }

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
        private readonly Stack<Tarea> _tareas = new Stack<Tarea>();

        public void Push(Tarea tarea)
        {
            _tareas.Push(tarea);
        }

        public Tarea Peek()
        {
            return _tareas.Count > 0 ? _tareas.Peek() : null;
        }

        public Tarea Pop()
        {
            return _tareas.Count > 0 ? _tareas.Pop() : null;
        }
    }
}