//ejercicio 1
Console.WriteLine("ingrese su contraseña");
string contraseña = "paytoluse";
string contraseñasecret = Console.ReadLine();
while (contraseñasecret != contraseña)
{
    Console.WriteLine("contraseña incorrecta, intente de nuevo");
    contraseñasecret = Console.ReadLine();
}
Console.WriteLine("contraseña correcta, bienvenido");
//ejercicio 2
Console.WriteLine("ejercicio 2");
int contador = 5;

while (contador >=1)
{
        Console.WriteLine(contador);
    contador--;
}
Console.WriteLine("listo para despegar");
Console.WriteLine("despegue!");
//ejercicio 3
Console.WriteLine("ingrese un numero");
int numero = 7;
int numerosecreto = Convert.ToInt32(Console.ReadLine());
while (numerosecreto != numero)
{
    Console.WriteLine("numero incorrecto, intente de nuevo");
    numerosecreto = Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine("Felicidades! Adivinaste el número");
//ejercicio 4
Console.WriteLine("ejercicio 4");
int sumatorio = 0;
Console.WriteLine("ingrese numeros para sumar, (0 para finalizar)");
int numero = Convert.ToInt32(Console.ReadLine());
while (numero != 0)
{
    sumatorio += numero;
    Console.WriteLine("ingrese otro numero para sumar, ingrese 0 para finalizar");
    numero = Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine("la suma total es: " + sumatorio);

