# BLOQUE 1
# 1. Mostrar multiplos de 3 del 1 al 100
def ejercicio1():
    for numero in range(1, 101):
        if numero % 3 == 0:
            print(numero)


# ejercicio 2
def ejercicio2():
    edad = int(input("Ingresa tu edad: "))

    if edad < 18:
        print("Sos menor de edad")
    elif edad == 18:
        print("Tenes justo 18 años")
    else:
        print("Sos mayor de edad")


# ejercicio 3
def ejercicio3():
    palabra = input("Ingresa una palabra: ")
    print("Cantidad de letras:", len(palabra))


# ejercicio 4
def ejercicio4():
    contraseña = "python123"
    intentos = 5

    while intentos > 0:
        respuesta = input("Adivina la contraseña: ")

        if respuesta == contraseña:
            print("¡Correcto! Acceso permitido")
            return
        else:
            intentos -= 1
            print("Contraseña incorrecta")
            print("Intentos restantes:", intentos)

    print("Te quedaste sin intentos")


# ejercicio 5
def ejercicio5():
    mayor = None

    for i in range(10):
        numero = float(input("Ingresa un numero: "))

        if mayor is None or numero > mayor:
            mayor = numero

    print("El numero mas alto es:", mayor)


# ejercicio 6
def ejercicio6():
    nombre = input("Ingresa tu nombre: ")
    print("Hola", nombre.title())


# ejercicio 7
def ejercicio7():
    for i in range(1, 11):
        print("7 x", i, "=", 7 * i)


# ejercicio 8
def ejercicio8():
    for i in range(10, 0, -1):
        print(i)

    print("oa")


# ejercicio 9
def ejercicio9():
    numero = int(input("Ingresa un numero: "))

    if numero % 2 == 0:
        print("Par")
    else:
        print("Impar")


# ejercicio 10
def ejercicio10():
    frase = input("Ingresa una frase: ")
    vocales = "aeiouáéíóú"
    contador = 0

    for letra in frase.lower():
        if letra in vocales:
            contador += 1

    print("Cantidad de vocales:", contador)


# ejercicio 11
def ejercicio11():
    numero = int(input("Ingresa un numero: "))

    for i in range(1, 13):
        print(numero, "x", i, "=", numero * i)


# ejercicio 12
def ejercicio12():
    total = 0

    while total <= 100:
        numero = float(input("Ingresa un numero: "))
        total += numero
        print("Total acumulado:", total)

    print("¡Superaste los 100!")


# ejercicio 13
def ejercicio13():
    palabra = input("Ingresa una palabra: ")

    for letra in palabra:
        print(letra)


# ejercicio 14
def ejercicio14():
    edad = int(input("Ingresa tu edad: "))

    puede_votar = edad >= 16
    puede_manejar = edad >= 17

    if puede_votar and puede_manejar:
        print("Podes votar y manejar")
    elif puede_votar:
        print("Podes votar, pero no manejar")
    elif puede_manejar:
        print("Podes manejar, pero no votar")
    else:
        print("No podes votar ni manejar")


# ejercicio 15
def ejercicio15():
    for numero in range(50, -1, -5):
        print(numero)


# ejercicio 16
def ejercicio16():
    while True:
        contraseña1 = input("Ingresa una contraseña: ")
        contraseña2 = input("Repite la contraseña: ")

        if contraseña1 == contraseña2:
            print("Acceso permitido")
            break
        else:
            print("Las contraseñas no coinciden. Intenta nuevamente")


# ejercicio 17
def ejercicio17():
    while True:
        nombre = input("Ingresa un nombre: ")

        if len(nombre) > 10:
            print("El nombre tiene mas de 10 caracteres")
            break
        else:
            print("El nombre es demasiado corto. Intenta nuevamente")


# ejercicio 18
def ejercicio18():
    oracion = input("Ingresa una oracion: ")
    contador = 0

    for letra in oracion.lower():
        if letra == "a":
            contador += 1

    print("Cantidad de letras A:", contador)


# menu interactivo
def menu():
    while True:
        print("f ========== BLOQUE 1 ==========")
        print("1. Multiplos de 3")
        print("2. Comprobar edad")
        print("3. Contar letras")
        print("4. Adivinar contraseña")
        print("5. Encontrar numero mayor")
        print("6. Saludar con mayusculas")
        print("7. Tabla del 7")
        print("8. Cuenta regresiva")
        print("9. Par o impar")
        print("10. Contar vocales")
        print("11. Tabla de multiplicar")
        print("12. Acumulador hasta 100")
        print("13. Mostrar letras")
        print("14. Votar o manejar")
        print("15. Numeros descendentes")
        print("16. Confirmar contraseña")
        print("17. Ingresar nombres")
        print("18. Contar letras A")
        print("0. Salir")

        opcion = input("\nElegí un ejercicio: ")

        if opcion == "1":
            ejercicio1()
        elif opcion == "2":
            ejercicio2()
        elif opcion == "3":
            ejercicio3()
        elif opcion == "4":
            ejercicio4()
        elif opcion == "5":
            ejercicio5()
        elif opcion == "6":
            ejercicio6()
        elif opcion == "7":
            ejercicio7()
        elif opcion == "8":
            ejercicio8()
        elif opcion == "9":
            ejercicio9()
        elif opcion == "10":
            ejercicio10()
        elif opcion == "11":
            ejercicio11()
        elif opcion == "12":
            ejercicio12()
        elif opcion == "13":
            ejercicio13()
        elif opcion == "14":
            ejercicio14()
        elif opcion == "15":
            ejercicio15()
        elif opcion == "16":
            ejercicio16()
        elif opcion == "17":
            ejercicio17()
        elif opcion == "18":
            ejercicio18()
        elif opcion == "0":
            print("¡Hasta luego!")
            break
        else:
            print("Opcion incorrecta. Intenta nuevamente")

menu()
