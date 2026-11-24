import random

def pedir_entero(mensaje):
    while True:
        try:
            return int(input(mensaje))
        except ValueError:
            print("Eso no es un número entero, probá de nuevo.")


# Ejercicio 1
def adivinar():
    secreto = random.randint(1, 10)
    intentos = 0
    while True:
        numero = pedir_entero("Adiviná el número (1 a 10): ")
        intentos += 1
        if numero < secreto:
            print("Es mayor.")
        elif numero > secreto:
            print("Es menor.")
        else:
            print(f"¡Acertaste en {intentos} intentos!")
            break


# Ejercicio 2
def divisores():
    n = pedir_entero("Ingresá un número: ")
    print(f"Divisores de {n}:")
    for i in range(1, n + 1):
        if n % i == 0:
            print(i)


# Ejercicio 3
def fibonacci():
    a = 0
    b = 1
    for i in range(20):
        print(a)
        siguiente = a + b
        a = b
        b = siguiente


# Ejercicio 4
def calculadora():
    print("1) Suma  2) Resta  3) Multiplicación  4) División")
    opcion = pedir_entero("Elegí la operación: ")
    num1 = float(input("Primer número: "))
    num2 = float(input("Segundo número: "))
    if opcion == 1:
        print("Resultado:", num1 + num2)
    elif opcion == 2:
        print("Resultado:", num1 - num2)
    elif opcion == 3:
        print("Resultado:", num1 * num2)
    elif opcion == 4:
        if num2 == 0:
            print("No se puede dividir por cero.")
        else:
            print("Resultado:", num1 / num2)
    else:
        print("Opción inválida.")


# Ejercicio 5
def factorial():
    n = pedir_entero("Ingresá un número: ")
    if n < 0:
        print("El factorial no existe para números negativos.")
        return
    resultado = 1
    for i in range(1, n + 1):
        resultado = resultado * i
    print(f"El factorial de {n} es {resultado}")


# Ejercicio 6
def turnos():
    nombres = []
    print("Ingresá nombres (escribí 'fin' para terminar):")
    while True:
        nombre = input("Nombre: ")
        if nombre.lower() == "fin":
            break
        nombres.append(nombre)
    print("\nTurnos asignados:")
    numero = 1
    for nombre in nombres:
        print(f"Turno {numero}: {nombre}")
        numero += 1


# Ejercicio 7
def pares():
    cantidad = 0
    for i in range(1, 101):
        if i % 2 == 0:
            cantidad += 1
    print("Hay", cantidad, "números pares entre 1 y 100.")


# Ejercicio 8
def temperaturas():
    while True:
        celsius = float(input("Temperatura en Celsius: "))
        fahrenheit = celsius * 9 / 5 + 32
        print(f"{celsius}°C equivalen a {fahrenheit}°F")
        otra = input("¿Querés convertir otra? (s/n): ")
        if otra.lower() != "s":
            break
    print("¡Chau!")


# Ejercicio 9
def primo():
    n = pedir_entero("Ingresá un número: ")
    if n < 2:
        print("No es primo.")
        return
    es_primo = True
    for i in range(2, n):
        if n % i == 0:
            es_primo = False
            break
    if es_primo:
        print(n, "es primo.")
    else:
        print(n, "no es primo.")


# Ejercicio 10
def saludos():
    nombres = []
    while True:
        nombre = input("Nombre (o 'fin' para terminar): ")
        if nombre.lower() == "fin":
            break
        nombres.append(nombre)
    for nombre in nombres:
        print(f"¡Hola, {nombre}!")


# Ejercicio 11
def duplicado():
    nombres = []
    while True:
        nombre = input("Ingresá un nombre: ")
        if nombre in nombres:
            print(f"¡'{nombre}' ya estaba! Ingresaste {len(nombres)} nombres antes del duplicado.")
            break
        nombres.append(nombre)


# Ejercicio 12
def multiplos():
    for i in range(100, 201):
        if i % 7 == 0 and i % 10 == 3:
            print(i)


# Ejercicio 13
def precios():
    total = 0
    cantidad = 0
    while total <= 1000:
        precio = float(input("Ingresá un precio: $"))
        total += precio
        cantidad += 1
        print(f"Total acumulado: ${total}")
    print(f"Se superaron los $1000. Se cargaron {cantidad} productos.")


# Ejercicio 14
def formulario():
    nombre = input("Nombre: ")

    edad = input("Edad: ")
    while not edad.isdigit():
        print("La edad tiene que ser un número.")
        edad = input("Edad: ")

    mail = input("Mail: ")
    while "@" not in mail:
        print("El mail tiene que tener '@'.")
        mail = input("Mail: ")

    print("\nDatos cargados:")
    print("Nombre:", nombre)
    print("Edad:", edad)
    print("Mail:", mail)


# Ejercicio 15
def digitos():
    numero = input("Ingresá un número: ")
    for d in numero:
        print(d)


# Ejercicio 16
def piedra_papel_tijera():
    opciones = ["piedra", "papel", "tijera"]
    puntos_usuario = 0
    puntos_pc = 0
    for ronda in range(1, 4):
        print(f"\nRonda {ronda}")
        usuario = input("Elegí piedra, papel o tijera: ").lower()
        while usuario not in opciones:
            usuario = input("Opción inválida. Elegí piedra, papel o tijera: ").lower()
        pc = random.choice(opciones)
        print("La computadora eligió:", pc)
        if usuario == pc:
            print("Empate.")
        elif (usuario == "piedra" and pc == "tijera") or \
             (usuario == "papel" and pc == "piedra") or \
             (usuario == "tijera" and pc == "papel"):
            print("¡Ganaste la ronda!")
            puntos_usuario += 1
        else:
            print("Perdiste la ronda.")
            puntos_pc += 1
    print(f"\nResultado final: vos {puntos_usuario} - computadora {puntos_pc}")
    if puntos_usuario > puntos_pc:
        print("¡Ganaste el juego!")
    elif puntos_pc > puntos_usuario:
        print("Ganó la computadora.")
    else:
        print("Empate.")


# Ejercicio 17
def aleatorios():
    mayores = 0
    for i in range(10):
        n = random.randint(1, 100)
        print(n)
        if n > 50:
            mayores += 1
    print("Números mayores a 50:", mayores)


# Ejercicio 18
def notas():
    suma = 0
    mejor_nota = -1
    mejor_nombre = ""
    for i in range(5):
        nombre = input(f"Nombre del alumno {i + 1}: ")
        nota = float(input("Nota: "))
        suma += nota
        if nota > mejor_nota:
            mejor_nota = nota
            mejor_nombre = nombre
    print(f" Promedio general: {suma / 5}")
    print(f"Mejor nota: {mejor_nombre} con {mejor_nota}")


# Ejercicio 19
def abecedario_inverso():
    letras = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ"
    for letra in letras[::-1]:
        print(letra)


ejercicios = {
    1: adivinar,
    2: divisores,
    3: fibonacci,
    4: calculadora,
    5: factorial,
    6: turnos,
    7: pares,
    8: temperaturas,
    9: primo,
    10: saludos,
    11: duplicado,
    12: multiplos,
    13: precios,
    14: formulario,
    15: digitos,
    16: piedra_papel_tijera,
    17: aleatorios,
    18: notas,
    19: abecedario_inverso,
}

while True:
    print()
    for numero in ejercicios:
        print(f"{numero}) Ejercicio {numero}")
    print("0) Salir")
    opcion = pedir_entero("Elegí: ")
    if opcion == 0:
        break
    elif opcion in ejercicios:
        print()
        ejercicios[opcion]()
    else:
        print("Ese ejercicio no existe.")