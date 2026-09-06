namespace ConsoleApp59
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Detector de Texto IA - Modo Experto";

            while (true)
            {
                Console.Clear();

                MostrarTitulo();

                Console.WriteLine();
                Console.WriteLine("Pegá el texto que querés analizar.");
                Console.WriteLine("Podés pegar varias líneas.");
                Console.WriteLine("Cuando termines, escribí una línea vacía.");
                Console.WriteLine();

                StringBuilder texto = new StringBuilder();

                while (true)
                {
                    string linea = Console.ReadLine() ?? "";

                    if (string.IsNullOrEmpty(linea))
                        break;

                    texto.AppendLine(linea);
                }

                string contenido = texto.ToString().Trim();

                if (string.IsNullOrWhiteSpace(contenido))
                {
                    Console.WriteLine();
                    Console.WriteLine("No ingresaste ningún texto.");
                    Console.WriteLine("Presioná una tecla...");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                MostrarTitulo();

                Console.WriteLine();
                Console.WriteLine("Analizando texto...");
                Console.WriteLine();

                for (int i = 0; i <= 20; i++)
                {
                    Console.Write("\r[");

                    for (int j = 0; j < 20; j++)
                    {
                        Console.Write(j < i ? "#" : "-");
                    }

                    Console.Write($"] {i * 5}%");
                    System.Threading.Thread.Sleep(30);
                }

                Console.WriteLine();
                Console.WriteLine();

                Resultado resultado = AnalizarTextoSimple(contenido);

                MostrarResultado(resultado);

                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine("ENTER = analizar otro texto");
                Console.WriteLine("ESC   = salir");
                Console.WriteLine("==============================================");

                ConsoleKey tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.Escape)
                    break;
            }
        }

        static void MostrarTitulo()
        {
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║          DETECTOR DE TEXTO IA               ║");
            Console.WriteLine("║               MODO EXPERTO                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
        }

        // Versión simplificada sin LINQ ni Regex
        static Resultado AnalizarTextoSimple(string texto)
        {
            // Crear struct e inicializar la lista aquí
            Resultado resultado = new Resultado();
            resultado.Indicadores = new List<string>();
            resultado.Confianza = "";

            string textoMinuscula = texto.ToLower();

            string[] palabras = ObtenerPalabrasSimple(textoMinuscula);
            string[] oraciones = ObtenerOracionesSimple(texto);
            string[] parrafos = ObtenerParrafosSimple(texto);

            resultado.Palabras = palabras.Length;
            resultado.Oraciones = oraciones.Length;

            if (palabras.Length < 20)
            {
                resultado.Puntaje = 50;
                resultado.Confianza = "MUY BAJA";
                resultado.Indicadores.Add(
                    "El texto es demasiado corto para realizar una estimación confiable."
                );

                return resultado;
            }

            double diversidad = CalcularDiversidadSimple(palabras);
            double longitudPromedio = oraciones.Length > 0 ? (double)palabras.Length / oraciones.Length : palabras.Length;
            double variacionOraciones = CalcularVariacionOracionesSimple(oraciones);
            double repeticion = CalcularRepeticionSimple(palabras);
            double uniformidad = CalcularUniformidadSimple(oraciones);
            double conectores = CalcularConectoresSimple(textoMinuscula);
            double frasesGenericas = CalcularFrasesGenericasSimple(textoMinuscula);
            double puntuacionEstructura = CalcularEstructuraSimple(oraciones);
            double puntuacionPuntuacion = CalcularPuntuacionSimple(texto);
            double puntuacionParrafos = CalcularParrafosSimple(parrafos);
            double puntuacionComplejidad = CalcularComplejidadSimple(palabras);

            double puntuacionFinal = 50;

            if (diversidad < 0.35) puntuacionFinal += 10;
            else if (diversidad < 0.45) puntuacionFinal += 5;
            else if (diversidad > 0.70) puntuacionFinal -= 6;

            if (longitudPromedio > 28) puntuacionFinal += 9;
            else if (longitudPromedio > 22) puntuacionFinal += 5;
            else if (longitudPromedio < 8) puntuacionFinal -= 5;

            if (variacionOraciones < 0.25) puntuacionFinal += 10;
            else if (variacionOraciones < 0.40) puntuacionFinal += 4;
            else if (variacionOraciones > 0.75) puntuacionFinal -= 7;

            if (repeticion > 0.35) puntuacionFinal += 8;
            else if (repeticion > 0.25) puntuacionFinal += 4;

            if (uniformidad > 0.80) puntuacionFinal += 9;
            else if (uniformidad > 0.65) puntuacionFinal += 4;
            else if (uniformidad < 0.35) puntuacionFinal -= 5;

            if (conectores > 0.045) puntuacionFinal += 6;
            else if (conectores > 0.025) puntuacionFinal += 2;

            if (frasesGenericas >= 4) puntuacionFinal += 8;
            else if (frasesGenericas >= 2) puntuacionFinal += 4;

            puntuacionFinal += puntuacionEstructura;
            puntuacionFinal += puntuacionPuntuacion;
            puntuacionFinal += puntuacionParrafos;
            puntuacionFinal += puntuacionComplejidad;

            puntuacionFinal = Math.Max(1, Math.Min(99, puntuacionFinal));

            resultado.Puntaje = puntuacionFinal;

            if (diversidad < 0.45)
                resultado.Indicadores.Add("La variedad de vocabulario es relativamente baja.");

            if (longitudPromedio > 22)
                resultado.Indicadores.Add("Las oraciones tienen una longitud media elevada.");

            if (variacionOraciones < 0.40)
                resultado.Indicadores.Add("La longitud de las oraciones es bastante uniforme.");

            if (repeticion > 0.25)
                resultado.Indicadores.Add("Se detecta repetición de palabras.");

            if (uniformidad > 0.65)
                resultado.Indicadores.Add("La estructura del texto presenta bastante regularidad.");

            if (frasesGenericas >= 2)
                resultado.Indicadores.Add("Aparecen varias expresiones frecuentes en textos formales.");

            if (puntuacionEstructura > 5)
                resultado.Indicadores.Add("La organización de las oraciones es bastante regular.");

            if (puntuacionPuntuacion > 4)
                resultado.Indicadores.Add("La puntuación presenta patrones relativamente uniformes.");

            if (puntuacionParrafos > 3)
                resultado.Indicadores.Add("Los párrafos presentan una estructura regular.");

            if (puntuacionComplejidad > 4)
                resultado.Indicadores.Add("El nivel de complejidad sintáctica es relativamente uniforme.");

            if (palabras.Length < 50) resultado.Confianza = "MUY BAJA";
            else if (palabras.Length < 100) resultado.Confianza = "BAJA";
            else if (palabras.Length < 250) resultado.Confianza = "MEDIA";
            else resultado.Confianza = "MEDIA-ALTA";

            return resultado;
        }

        // Tokenización básica: palabras (letras y dígitos)
        static string[] ObtenerPalabrasSimple(string texto)
        {
            List<string> palabras = new List<string>();
            StringBuilder actual = new StringBuilder();

            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];

                if (char.IsLetterOrDigit(c))
                {
                    actual.Append(c);
                }
                else
                {
                    if (actual.Length > 0)
                    {
                        palabras.Add(actual.ToString());
                        actual.Clear();
                    }
                }
            }

            if (actual.Length > 0)
                palabras.Add(actual.ToString());

            return palabras.ToArray();
        }

        // Oraciones sencillas separadas por . ! ?
        static string[] ObtenerOracionesSimple(string texto)
        {
            List<string> oraciones = new List<string>();
            StringBuilder actual = new StringBuilder();

            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                actual.Append(c);

                if (c == '.' || c == '!' || c == '?')
                {
                    string s = actual.ToString().Trim();

                    // quitar los signos finales repetidos
                    while (s.Length > 0 && (s[s.Length - 1] == '.' || s[s.Length - 1] == '!' || s[s.Length - 1] == '?'))
                        s = s.Substring(0, s.Length - 1);

                    s = s.Trim();
                    if (s.Length > 0)
                        oraciones.Add(s);

                    actual.Clear();
                }
            }

            // resto como una oración si tiene contenido
            string resto = actual.ToString().Trim();
            if (resto.Length > 0)
                oraciones.Add(resto);

            return oraciones.ToArray();
        }

        // Párrafos: grupos de líneas no vacías
        static string[] ObtenerParrafosSimple(string texto)
        {
            List<string> parrafos = new List<string>();
            string[] lineas = texto.Split('\n');

            StringBuilder current = new StringBuilder();
            for (int i = 0; i < lineas.Length; i++)
            {
                string l = lineas[i].TrimEnd();

                if (string.IsNullOrWhiteSpace(l))
                {
                    if (current.Length > 0)
                    {
                        parrafos.Add(current.ToString().Trim());
                        current.Clear();
                    }
                }
                else
                {
                    if (current.Length > 0)
                        current.AppendLine();
                    current.Append(l);
                }
            }

            if (current.Length > 0)
                parrafos.Add(current.ToString().Trim());

            return parrafos.ToArray();
        }

        static double CalcularDiversidadSimple(string[] palabras)
        {
            if (palabras.Length == 0) return 0;

            List<string> unicas = new List<string>();
            for (int i = 0; i < palabras.Length; i++)
            {
                string w = palabras[i];
                if (!unicas.Contains(w))
                    unicas.Add(w);
            }

            return unicas.Count / (double)palabras.Length;
        }

        static double CalcularVariacionOracionesSimple(string[] oraciones)
        {
            if (oraciones.Length < 2) return 0.5;

            List<int> longitudes = new List<int>();
            for (int i = 0; i < oraciones.Length; i++)
            {
                string[] ps = ObtenerPalabrasSimple(oraciones[i]);
                if (ps.Length > 0) longitudes.Add(ps.Length);
            }

            if (longitudes.Count < 2) return 0.5;

            double suma = 0;
            for (int i = 0; i < longitudes.Count; i++) suma += longitudes[i];
            double promedio = suma / longitudes.Count;
            if (promedio == 0) return 0;

            double varSum = 0;
            for (int i = 0; i < longitudes.Count; i++)
            {
                double d = longitudes[i] - promedio;
                varSum += d * d;
            }

            double desviacion = Math.Sqrt(varSum / longitudes.Count);
            return desviacion / promedio;
        }

        static double CalcularRepeticionSimple(string[] palabras)
        {
            if (palabras.Length == 0) return 0;

            List<string> unicas = new List<string>();
            int repetidas = 0;
            for (int i = 0; i < palabras.Length; i++)
            {
                string w = palabras[i];
                if (unicas.Contains(w)) repetidas++;
                else unicas.Add(w);
            }

            return repetidas / (double)palabras.Length;
        }

        static double CalcularUniformidadSimple(string[] oraciones)
        {
            if (oraciones.Length < 3) return 0.5;

            List<int> longitudes = new List<int>();
            for (int i = 0; i < oraciones.Length; i++)
            {
                int cantidad = ObtenerPalabrasSimple(oraciones[i]).Length;
                if (cantidad > 0) longitudes.Add(cantidad);
            }

            if (longitudes.Count < 3) return 0.5;

            int min = int.MaxValue;
            int max = int.MinValue;
            for (int i = 0; i < longitudes.Count; i++)
            {
                if (longitudes[i] < min) min = longitudes[i];
                if (longitudes[i] > max) max = longitudes[i];
            }

            if (max == 0) return 0;
            return 1.0 - ((max - min) / (double)max);
        }

        static int ContarSubcadenas(string texto, string sub)
        {
            int contador = 0;
            int pos = 0;
            if (string.IsNullOrEmpty(sub)) return 0;

            while (true)
            {
                int i = texto.IndexOf(sub, pos, StringComparison.OrdinalIgnoreCase);
                if (i == -1) break;
                contador++;
                pos = i + sub.Length;
            }

            return contador;
        }

        static double CalcularConectoresSimple(string texto)
        {
            string[] conectores =
            {
                "además",
                "sin embargo",
                "por lo tanto",
                "en conclusión",
                "por otro lado",
                "asimismo",
                "en consecuencia",
                "por consiguiente",
                "finalmente",
                "en resumen",
                "de esta manera",
                "por otra parte",
                "es importante",
                "cabe destacar"
            };

            int encontrados = 0;
            for (int i = 0; i < conectores.Length; i++)
                encontrados += ContarSubcadenas(texto, conectores[i]);

            int palabras = ObtenerPalabrasSimple(texto).Length;
            if (palabras == 0) return 0;
            return encontrados / (double)palabras;
        }

        static double CalcularFrasesGenericasSimple(string texto)
        {
            string[] frases =
            {
                "es importante destacar",
                "en conclusión",
                "en resumen",
                "por otro lado",
                "sin embargo",
                "por lo tanto",
                "cabe destacar",
                "en este sentido",
                "de esta manera",
                "podemos afirmar",
                "es fundamental",
                "resulta importante",
                "a lo largo de",
                "en términos generales",
                "desde esta perspectiva"
            };

            int cantidad = 0;
            for (int i = 0; i < frases.Length; i++)
            {
                if (texto.IndexOf(frases[i], StringComparison.OrdinalIgnoreCase) != -1)
                    cantidad++;
            }

            return cantidad;
        }

        static double CalcularEstructuraSimple(string[] oraciones)
        {
            if (oraciones.Length < 3) return 0;

            int cuentan = 0;
            for (int i = 0; i < oraciones.Length; i++)
            {
                string limpia = oraciones[i].Trim();
                if (limpia.Length > 0 && char.IsUpper(limpia[0])) cuentan++;
            }

            double proporcion = cuentan / (double)oraciones.Length;
            if (proporcion > 0.95) return 5;
            if (proporcion > 0.85) return 2;
            return 0;
        }

        static double CalcularPuntuacionSimple(string texto)
        {
            int comas = 0, puntos = 0, dosPuntos = 0, puntoComa = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                if (c == ',') comas++;
                else if (c == '.') puntos++;
                else if (c == ':') dosPuntos++;
                else if (c == ';') puntoComa++;
            }

            int palabras = ObtenerPalabrasSimple(texto).Length;
            if (palabras == 0) return 0;

            double puntuacion = (comas + dosPuntos + puntoComa) / (double)palabras;
            if (puntuacion > 0.08) return 5;
            if (puntuacion > 0.05) return 2;
            return 0;
        }

        static double CalcularParrafosSimple(string[] parrafos)
        {
            if (parrafos.Length < 2) return 0;

            List<int> cantidades = new List<int>();
            for (int i = 0; i < parrafos.Length; i++)
            {
                int cantidad = ObtenerPalabrasSimple(parrafos[i]).Length;
                if (cantidad > 0) cantidades.Add(cantidad);
            }

            if (cantidades.Count < 2) return 0;

            double suma = 0;
            for (int i = 0; i < cantidades.Count; i++) suma += cantidades[i];
            double promedio = suma / cantidades.Count;
            if (promedio == 0) return 0;

            double varSum = 0;
            for (int i = 0; i < cantidades.Count; i++)
            {
                double d = cantidades[i] - promedio;
                varSum += d * d;
            }

            double desviacion = Math.Sqrt(varSum / cantidades.Count);
            double variacion = desviacion / promedio;

            if (variacion < 0.25) return 5;
            if (variacion < 0.40) return 2;
            return 0;
        }

        static double CalcularComplejidadSimple(string[] palabras)
        {
            if (palabras.Length == 0) return 0;

            double suma = 0;
            for (int i = 0; i < palabras.Length; i++) suma += palabras[i].Length;
            double promedio = suma / palabras.Length;

            if (promedio > 7.0) return 5;
            if (promedio > 6.0) return 2;
            return 0;
        }

        static void MostrarResultado(Resultado resultado)
        {
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║                 RESULTADO                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.WriteLine();

            Console.WriteLine($"Palabras analizadas : {resultado.Palabras}");
            Console.WriteLine($"Oraciones           : {resultado.Oraciones}");

            Console.WriteLine();

            Console.WriteLine($"🤖 IA estimada      : {resultado.Puntaje:F1}%");
            Console.WriteLine($"👤 Humano estimado  : {100 - resultado.Puntaje:F1}%");

            Console.WriteLine();

            DibujarBarra(resultado.Puntaje);

            Console.WriteLine();

            if (resultado.Puntaje >= 75)
            {
                Console.WriteLine("RESULTADO:");
                Console.WriteLine("⚠ PROBABLEMENTE GENERADO POR IA");
            }
            else if (resultado.Puntaje >= 55)
            {
                Console.WriteLine("RESULTADO:");
                Console.WriteLine("⚠ POSIBLES CARACTERÍSTICAS DE IA");
            }
            else if (resultado.Puntaje >= 40)
            {
                Console.WriteLine("RESULTADO:");
                Console.WriteLine("❓ INDETERMINADO");
            }
            else
            {
                Console.WriteLine("RESULTADO:");
                Console.WriteLine("✓ PROBABLEMENTE HUMANO");
            }

            Console.WriteLine();

            Console.WriteLine($"Confianza del análisis: {resultado.Confianza}");

            Console.WriteLine();
            Console.WriteLine("INDICADORES DETECTADOS");
            Console.WriteLine("----------------------------------------------");

            if (resultado.Indicadores == null || resultado.Indicadores.Count == 0)
            {
                Console.WriteLine("No se encontraron indicadores importantes.");
            }
            else
            {
                for (int i = 0; i < resultado.Indicadores.Count; i++)
                    Console.WriteLine("• " + resultado.Indicadores[i]);
            }

            Console.WriteLine();
        }

        static void DibujarBarra(double porcentaje)
        {
            int total = 40;
            int llenos = (int)Math.Round(porcentaje / 100 * total);

            Console.Write("[");

            for (int i = 0; i < total; i++)
            {
                Console.Write(i < llenos ? "#" : "-");
            }

            Console.WriteLine("]");
        }
    }

    // Cambiado a struct (se debe inicializar la lista cuando se crea)
    struct Resultado
    {
        public double Puntaje;
        public int Palabras;
        public int Oraciones;
        public string Confianza;
        public List<string> Indicadores;
    }
}