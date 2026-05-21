// EJERCICIO 1 
Console.WriteLine("Ejercicio 1");
Console.Write("Ingresa el codigo de categoria (I, C, J, A): ");
string codigo = Console.ReadLine();

switch (codigo)
{
    case "I":
        Console.WriteLine("Categoria: Infantiles | Cuota: $800");
        break;
    case "C":
        Console.WriteLine("Categoria: Cadete | Cuota: $1000");
        break;
    case "J":
        Console.WriteLine("Categoria: Juvenil | Cuota: $1200");
        break;
    case "A":
        Console.WriteLine("Categoria: Adulto | Cuota: $900");
        break;
    default:
        Console.WriteLine("Codigo no valido.");
        break;
}


// EJERCICIO 2 
Console.WriteLine("Ejercicio 2");
Console.Write("Ingresa el tipo (S=Simple, X=Expreso, E=Especial): ");
string tipo = Console.ReadLine();
Console.Write("Ingresa el destino (L=Local, N=Exterior): ");
string destino = Console.ReadLine();

double basico = 300;
double recargoPorTipo = 0;
double recargoPorDestino = 0;

switch (tipo)
{
    case "S": recargoPorTipo = 0; break;
    case "X": recargoPorTipo = 0.20; break;
    case "E": recargoPorTipo = 0.40; break;
    default:
        Console.WriteLine("Tipo no valido.");
        return;
}

switch (destino)
{
    case "L": recargoPorDestino = 0.05; break;
    case "N": recargoPorDestino = 0.20; break;
    default:
        Console.WriteLine("Destino no valido.");
        return;
}

double costo = basico + (basico * recargoPorTipo) + (basico * recargoPorDestino);
Console.WriteLine("Costo total: $" + costo);


// EJERCICIO 3
Console.WriteLine("Ejercicio 3");
Console.Write("Ingresa el sueldo basico: $");
double sueldoBasico = int.Parse(Console.ReadLine());
Console.Write("Corresponde presentismo? (V/F): ");
string pres = Console.ReadLine();
Console.Write("Codigo de antiguedad (1, 2, 3 o 4): ");
int codAntig = int.Parse(Console.ReadLine());

double porcentajeAntig = 0;
switch (codAntig)
{
    case 1: porcentajeAntig = 0; break;
    case 2: porcentajeAntig = 0.10; break;
    case 3: porcentajeAntig = 0.15; break;
    case 4: porcentajeAntig = 0.20; break;
    default:
        Console.WriteLine("Codigo de antiguedad no valido.");
        return;
}

double antiguedad = sueldoBasico * porcentajeAntig;
double presentismo = 0;
if (pres == "V")
    presentismo = sueldoBasico * 0.10;

double remuneraciones = sueldoBasico + antiguedad + presentismo;
double jubilacion = remuneraciones * 0.13;
double obraSocial = remuneraciones * 0.03;
double descuentos = jubilacion + obraSocial;
double sueldoNeto = remuneraciones - descuentos;

Console.WriteLine("Total Remuneraciones: $" + remuneraciones);
Console.WriteLine("Total Descuentos: $" + descuentos);
Console.WriteLine("Sueldo Neto: $" + sueldoNeto);


// EJERCICIO 4
Console.WriteLine("Ejercicio 4");
Console.Write("Ingresa el dia (dd): ");
int dia = int.Parse(Console.ReadLine());
Console.Write("Ingresa el mes (mm): ");
int mes = int.Parse(Console.ReadLine());
Console.Write("Ingresa el anio (yy): ");
int anio = int.Parse(Console.ReadLine());

string nombreMes = "";
switch (mes)
{
    case 1: nombreMes = "enero"; break;
    case 2: nombreMes = "febrero"; break;
    case 3: nombreMes = "marzo"; break;
    case 4: nombreMes = "abril"; break;
    case 5: nombreMes = "mayo"; break;
    case 6: nombreMes = "junio"; break;
    case 7: nombreMes = "julio"; break;
    case 8: nombreMes = "agosto"; break;
    case 9: nombreMes = "septiembre"; break;
    case 10: nombreMes = "octubre"; break;
    case 11: nombreMes = "noviembre"; break;
    case 12: nombreMes = "diciembre"; break;
    default: nombreMes = "mes invalido"; break;
}

int anioCompleto = 2000 + anio;
Console.WriteLine("Fecha: " + dia + " de " + nombreMes + " de " + anioCompleto);


// EJERCICIO 5
Console.WriteLine("Ejercicio 5");
Console.WriteLine("1. Cuadrado");
Console.WriteLine("2. Rectangulo");
Console.WriteLine("3. Circulo");
Console.WriteLine("4. Triangulo");
Console.WriteLine("5. Trapecio");
Console.Write("Elegi una figura (1-5): ");
int fig = int.Parse(Console.ReadLine());

switch (fig)
{
    case 1:
        {
            Console.Write("Lado: ");
            double lado = double.Parse(Console.ReadLine());
            Console.WriteLine("Perimetro: " + (4 * lado));
            Console.WriteLine("Superficie: " + (lado * lado));
            break;
        }
    case 2:
        {
            Console.Write("Base: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Altura: ");
            double h = double.Parse(Console.ReadLine());
            Console.WriteLine("Perimetro: " + (2 * (b + h)));
            Console.WriteLine("Superficie: " + (b * h));
            break;
        }
    case 3:
        {
            Console.Write("Radio: ");
            double r = double.Parse(Console.ReadLine());
            Console.WriteLine("Perimetro: " + (2 * Math.PI * r));
            Console.WriteLine("Superficie: " + (Math.PI * r * r));
            break;
        }
    case 4:
        {
            Console.Write("Base: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Altura: ");
            double h = double.Parse(Console.ReadLine());
            Console.Write("Lado 1: ");
            double l1 = double.Parse(Console.ReadLine());
            Console.Write("Lado 2: ");
            double l2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Perimetro: " + (b + l1 + l2));
            Console.WriteLine("Superficie: " + (b * h / 2));
            break;
        }
    case 5:
        {
            Console.Write("Base mayor: ");
            double bMayor = double.Parse(Console.ReadLine());
            Console.Write("Base menor: ");
            double bMenor = double.Parse(Console.ReadLine());
            Console.Write("Altura: ");
            double h = double.Parse(Console.ReadLine());
            Console.Write("Lado izquierdo: ");
            double li = double.Parse(Console.ReadLine());
            Console.Write("Lado derecho: ");
            double ld = double.Parse(Console.ReadLine());
            Console.WriteLine("Perimetro: " + (bMayor + bMenor + li + ld));
            Console.WriteLine("Superficie: " + ((bMayor + bMenor) * h / 2));
            break;
        }
    default:
        Console.WriteLine("Opcion no valida.");
        break;
}


// EJERCICIO 6
Console.WriteLine("Ejercicio 6");
Console.Write("Ingresa el total de la compra: $");
double totalCompra = double.Parse(Console.ReadLine());
Console.Write("Color de la bolita (blanco/verde/amarilla/azul/roja): ");
string color = Console.ReadLine().ToLower();

double descuento = 0;
switch (color)
{
    case "blanco": descuento = 0; break;
    case "verde": descuento = 0.10; break;
    case "amarilla": descuento = 0.25; break;
    case "azul": descuento = 0.50; break;
    case "roja": descuento = 1.00; break;
    default:
        Console.WriteLine("Color no valido.");
        return;
}

double montoDesc = totalCompra * descuento;
Console.WriteLine("Descuento: $" + montoDesc);
Console.WriteLine("Total a pagar: $" + (totalCompra - montoDesc));


// EJERCICIO 7 
Console.WriteLine("Ejercicio 7");

string[] preguntas = {
    "Colon descubrio America? (si/no): ",
    "La independencia de Mexico fue en el anio 1810? (si/no): ",
    "The Doors fue un grupo de rock americano? (si/no): "
};

bool gano = true;
for (int i = 0; i < preguntas.Length; i++)
{
    Console.Write(preguntas[i]);
    string resp = Console.ReadLine().ToLower();
    if (resp != "si")
    {
        Console.WriteLine("Respuesta incorrecta. El juego termina.");
        gano = false;
        break;
    }
}
if (gano)
    Console.WriteLine("Ganaste! Respondiste correctamente las tres preguntas.");


// EJERCICIO 8 
Console.WriteLine("Ejercicio 8");
Console.Write("Ingresa el total de la compra: $");
double totalSuper = double.Parse(Console.ReadLine());
Console.Write("Numero escogido al azar (0-99): ");
int numero = int.Parse(Console.ReadLine());

double descuentoSuper = 0;
if (numero < 74)
    descuentoSuper = totalSuper * 0.15;
else
    descuentoSuper = totalSuper * 0.20;

Console.WriteLine("Monto descontado: $" + descuentoSuper);
Console.WriteLine("Total a pagar: $" + (totalSuper - descuentoSuper));


// EJERCICIO 9 
Console.WriteLine("Ejercicio 9");
Console.Write("Ingresa tu sexo (M=Masculino, F=Femenino): ");
string sexo = Console.ReadLine();
Console.Write("Ingresa tu edad: ");
int edad = int.Parse(Console.ReadLine());

if (sexo == "F")
    Console.WriteLine("Pulsaciones cada 10 segundos: " + ((220 - edad) / 10.0));
else if (sexo == "M")
    Console.WriteLine("Pulsaciones cada 10 segundos: " + ((210 - edad) / 10.0));
else
    Console.WriteLine("Sexo no valido.");


// EJERCICIO 10
Console.WriteLine("Ejercicio 10");

string[] nombres = new string[3];
string[] apellidos = new string[3];
double[] promedios = new double[3];

for (int i = 0; i < 3; i++)
{
    Console.WriteLine("Alumno " + (i + 1) + ":");
    Console.Write("  Nombre: ");
    nombres[i] = Console.ReadLine();
    Console.Write("  Apellido: ");
    apellidos[i] = Console.ReadLine();
    Console.Write("  Promedio: ");
    promedios[i] = double.Parse(Console.ReadLine());
}

for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2 - i; j++)
    {
        if (promedios[j] < promedios[j + 1])
        {
            double tempP = promedios[j]; promedios[j] = promedios[j + 1]; promedios[j + 1] = tempP;
            string tempN = nombres[j]; nombres[j] = nombres[j + 1]; nombres[j + 1] = tempN;
            string tempA = apellidos[j]; apellidos[j] = apellidos[j + 1]; apellidos[j + 1] = tempA;
        }
    }
}

Console.WriteLine("Abanderado: " + nombres[0] + " " + apellidos[0] + " (promedio: " + promedios[0] + ")");
Console.WriteLine("Primer escolta: " + nombres[1] + " " + apellidos[1] + " (promedio: " + promedios[1] + ")");
Console.WriteLine("Segundo escolta: " + nombres[2] + " " + apellidos[2] + " (promedio: " + promedios[2] + ")");


// EJERCICIO 11
Console.WriteLine("Ejercicio 11");
Console.Write("Ingresa un numero entero del 0 al 9: ");
int num11 = int.Parse(Console.ReadLine());

switch (num11)
{
    case 0: Console.WriteLine("Zero"); break;
    case 1: Console.WriteLine("One"); break;
    case 2: Console.WriteLine("Two"); break;
    case 3: Console.WriteLine("Three"); break;
    case 4: Console.WriteLine("Four"); break;
    case 5: Console.WriteLine("Five"); break;
    case 6: Console.WriteLine("Six"); break;
    case 7: Console.WriteLine("Seven"); break;
    case 8: Console.WriteLine("Eight"); break;
    case 9: Console.WriteLine("Nine"); break;
    default: Console.WriteLine("Numero fuera del rango (0-9)."); break;
}


// EJERCICIO 12
Console.WriteLine("Ejercicio 12");
Console.WriteLine("1. Triangulo");
Console.WriteLine("2. Circulo");
Console.WriteLine("3. Rectangulo");
Console.WriteLine("4. Hexagono");
Console.Write("Elegi una figura (1-4): ");
int fig12 = int.Parse(Console.ReadLine());

switch (fig12)
{
    case 1:
        {
            Console.Write("Base: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Altura: ");
            double h = double.Parse(Console.ReadLine());
            Console.WriteLine("Area del triangulo: " + (b * h / 2));
            break;
        }
    case 2:
        {
            Console.Write("Radio: ");
            double r = double.Parse(Console.ReadLine());
            Console.WriteLine("Area del circulo: " + (Math.PI * r * r));
            break;
        }
    case 3:
        {
            Console.Write("Base: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Altura: ");
            double h = double.Parse(Console.ReadLine());
            Console.WriteLine("Area del rectangulo: " + (b * h));
            break;
        }
    case 4:
        {
            Console.Write("Lado del hexagono: ");
            double l = double.Parse(Console.ReadLine());
            Console.WriteLine("Area del hexagono: " + ((3 * Math.Sqrt(3) / 2) * l * l));
            break;
        }
    default:
        Console.WriteLine("Opcion no valida.");
        break;
}


// EJERCICIO 13
Console.WriteLine("Ejercicio 13");
Console.Write("Valor de A (v/f): ");
string entradaA = Console.ReadLine().ToLower();
Console.Write("Valor de B (v/f): ");
string entradaB = Console.ReadLine().ToLower();

if ((entradaA != "v" && entradaA != "f") || (entradaB != "v" && entradaB != "f"))
{
    Console.WriteLine("Error: los valores deben ser v o f.");
    return;
}

bool a13 = (entradaA == "v");
bool b13 = (entradaB == "v");

Console.WriteLine("AND: " + ((a13 && b13) ? "v" : "f"));
Console.WriteLine("NOR: " + (!(a13 || b13) ? "v" : "f"));


// EJERCICIO 14
Console.WriteLine("Ejercicio 14");
Console.Write("Cantidad de camisas: ");
int cantidad = int.Parse(Console.ReadLine());
Console.Write("Precio por camisa: $");
double precio = double.Parse(Console.ReadLine());

double totalSinDesc = cantidad * precio;
double porcentaje = 0;

if (cantidad >= 1 && cantidad <= 4)
    porcentaje = 0.125;
else if (cantidad >= 5 && cantidad <= 8)
    porcentaje = 0.20;
else
    porcentaje = 0.315;

double montoDescuento = totalSinDesc * porcentaje;
Console.WriteLine("Compra sin descuento: $" + totalSinDesc);
Console.WriteLine("Descuento aplicado: $" + montoDescuento);
Console.WriteLine("Compra con descuento: $" + (totalSinDesc - montoDescuento));


// EJERCICIO 15 
Console.WriteLine("Ejercicio 15");
Console.Write("Ingresa un numero entero del 1 al 10: ");
int num15 = int.Parse(Console.ReadLine());

if (num15 < 1 || num15 > 10)
{
    Console.WriteLine("Numero fuera del rango (1-10).");
    return;
}

if (num15 == 1)
{
    Console.WriteLine("1 no se considera primo por definicion.");
}
else
{
    bool esPrimo = true;
    for (int i = 2; i < num15; i++)
    {
        if (num15 % i == 0)
        {
            Console.WriteLine(num15 + " no es primo, es divisible por " + i + ".");
            esPrimo = false;
            break;
        }
    }
    if (esPrimo)
        Console.WriteLine(num15 + " ES primo.");
}


// EJERCICIO 16 
Console.WriteLine("Ejercicio 16");
Console.Write("Horas (1-12): ");
int horas = int.Parse(Console.ReadLine());
Console.Write("Minutos (0-59): ");
int minutos = int.Parse(Console.ReadLine());
Console.Write("Segundos (0-59): ");
int segundos = int.Parse(Console.ReadLine());
Console.Write("AM o PM? (am/pm): ");
string meridiano = Console.ReadLine().ToLower();

int horas24 = horas;
if (meridiano == "pm" && horas != 12) horas24 = horas + 12;
if (meridiano == "am" && horas == 12) horas24 = 0;

segundos += 10;
if (segundos >= 60) { segundos -= 60; minutos++; }
if (minutos >= 60) { minutos -= 60; horas24++; }
if (horas24 >= 24) horas24 -= 24;

string nuevoMeridiano = horas24 < 12 ? "am" : "pm";
int nuevaHora = horas24 % 12;
if (nuevaHora == 0) nuevaHora = 12;

Console.WriteLine("Hora resultante: " + nuevaHora + " " + nuevoMeridiano + ": " + minutos + " min.: " + segundos + " seg.");


// EJERCICIO 17 
Console.WriteLine("Ejercicio 17");
Console.Write("Ingresa a: ");
double a17 = double.Parse(Console.ReadLine());
Console.Write("Ingresa b: ");
double b17 = double.Parse(Console.ReadLine());
Console.Write("Ingresa c: ");
double c17 = double.Parse(Console.ReadLine());

if (a17 == 0)
{
    Console.WriteLine("El coeficiente a es 0, no es ecuacion de 2do grado.");
    return;
}

double discriminante = (b17 * b17) - (4 * a17 * c17);

if (discriminante < 0)
    Console.WriteLine("No tiene solucion real (discriminante negativo: " + discriminante + ").");
else if (discriminante == 0)
    Console.WriteLine("Una sola solucion: x = " + (-b17 / (2 * a17)));
else
{
    Console.WriteLine("x1 = " + ((-b17 + Math.Sqrt(discriminante)) / (2 * a17)));
    Console.WriteLine("x2 = " + ((-b17 - Math.Sqrt(discriminante)) / (2 * a17)));
}


// EJERCICIO 18
Console.WriteLine("Ejercicio 18");
Console.WriteLine("1=Metros  2=Pies  3=Centimetros  4=Pulgadas");
Console.Write("Unidad de origen (1-4): ");
int origen = int.Parse(Console.ReadLine());
Console.Write("Valor a convertir: ");
double valor = double.Parse(Console.ReadLine());
Console.Write("Unidad de destino (1-4): ");
int dest18 = int.Parse(Console.ReadLine());

double enMetros = 0;
switch (origen)
{
    case 1: enMetros = valor; break;
    case 2: enMetros = valor * 0.3048; break;
    case 3: enMetros = valor / 100; break;
    case 4: enMetros = valor * 0.0254; break;
    default: Console.WriteLine("Unidad de origen no valida."); return;
}

double resultado18 = 0;
switch (dest18)
{
    case 1: resultado18 = enMetros; break;
    case 2: resultado18 = enMetros / 0.3048; break;
    case 3: resultado18 = enMetros * 100; break;
    case 4: resultado18 = enMetros / 0.0254; break;
    default: Console.WriteLine("Unidad de destino no valida."); return;
}

Console.WriteLine("Resultado: " + resultado18);


// EJERCICIO 19 
Console.WriteLine("Ejercicio 19");
Console.Write("Ingresa un numero natural (1-3999): ");
int num19 = int.Parse(Console.ReadLine());

if (num19 < 1 || num19 > 3999)
{
    Console.WriteLine("Numero fuera del rango permitido.");
    return;
}

int[] valores = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
string[] romano = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

string resultado19 = "";
int restante = num19;

for (int i = 0; i < valores.Length; i++)
{
    while (restante >= valores[i])
    {
        resultado19 += romano[i];
        restante -= valores[i];
    }
}

Console.WriteLine(num19 + " en romano es: " + resultado19);


// EJERCICIO 20 
Console.WriteLine("Ejercicio 20");
Console.Write("Estado interruptor 1 (1=cerrado, 0=abierto): ");
int s1 = int.Parse(Console.ReadLine());
Console.Write("Estado interruptor 2 (1=cerrado, 0=abierto): ");
int s2 = int.Parse(Console.ReadLine());
Console.Write("Estado interruptor 3 (1=cerrado, 0=abierto): ");
int s3 = int.Parse(Console.ReadLine());

int cerrados = s1 + s2 + s3;

if (cerrados >= 2)
    Console.WriteLine("El equipo SI funciona (" + cerrados + " interruptores cerrados).");
else
    Console.WriteLine("El equipo NO funciona (solo " + cerrados + " interruptor/es cerrado/s).");


// EJERCICIO 21 
Console.WriteLine("Ejercicio 21");

int[] conjA = new int[3];
int[] conjB = new int[3];

Console.WriteLine("Ingresa 3 numeros del conjunto A:");
for (int i = 0; i < 3; i++)
{
    Console.Write("  A[" + (i + 1) + "]: ");
    conjA[i] = int.Parse(Console.ReadLine());
}

Console.WriteLine("Ingresa 3 numeros del conjunto B:");
for (int i = 0; i < 3; i++)
{
    Console.Write("  B[" + (i + 1) + "]: ");
    conjB[i] = int.Parse(Console.ReadLine());
}

Console.Write("Interseccion: { ");
bool hayInterseccion = false;

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        if (conjA[i] == conjB[j])
        {
            Console.Write(conjA[i] + " ");
            hayInterseccion = true;
        }
    }
}

if (!hayInterseccion)
    Console.Write("vacia");

Console.WriteLine("}");


// EJERCICIO 22 
Console.WriteLine("Ejercicio 22");
Console.Write("Lado 1: ");
double lado1 = double.Parse(Console.ReadLine());
Console.Write("Lado 2: ");
double lado2 = double.Parse(Console.ReadLine());
Console.Write("Lado 3: ");
double lado3 = double.Parse(Console.ReadLine());

if (lado1 == lado2 && lado2 == lado3)
{
    Console.WriteLine("El triangulo es EQUILATERO.");
}
else if (lado1 == lado2 || lado2 == lado3 || lado1 == lado3)
{
    Console.WriteLine("El triangulo es ISOSCELES.");
}
else
    Console.WriteLine("El triangulo es ESCALENO.");


