import os
import struct
import sys
import math

# Запис результату у текстовий файл
def writeResTxt(fName, result):
    with open(fName, 'w') as f:
        f.write(str(result))

# Читання результату з текстового файлу
def readResTxt(fName):
    result = 0.0
    try:
        if os.path.exists(fName):
            with open(fName, 'r') as f:
                result = f.read()
        else:
            raise FileNotFoundError(f"File {fName} not found.")
    except FileNotFoundError as e:
        print(e)
    return result

# Запис результату у двійковий файл
def writeResBin(fName, result):
    with open(fName, 'wb') as f:
        f.write(struct.pack('f', result))

# Читання результату з двійкового файлу
def readResBin(fName):
    result = 0.0
    try:
        if os.path.exists(fName):
            with open(fName, 'rb') as f:
                result = struct.unpack('f', f.read())[0]
        else:
            raise FileNotFoundError(f"File {fName} not found.")
    except FileNotFoundError as e:
        print(e)
    return result

# Функція обчислення виразу y = ctg(x)/tg(x) = 1/(tan(x)^2)
def calculate(x):
    try:
        tan_x = math.tan(x)
        if tan_x == 0:  # уникаємо ділення на нуль
            raise ZeroDivisionError("tan(x) = 0, обчислення неможливе.")
        return 1 / (tan_x ** 2)
    except ZeroDivisionError as e:
        print(e)
        return None

# Основна частина програми
if __name__ == "__main__":
    try:
        data = float(input("Введіть значення x (у радіанах): "))
        result = calculate(data)
        if result is not None:
            print(f"Результат: {result}")

            # Запис у файли
            writeResTxt("textRes.txt", result)
            writeResBin("binRes.bin", result)

            # Зчитування з файлів
            print("Зчитано з binRes.bin:", readResBin("binRes.bin"))
            print("Зчитано з textRes.txt:", readResTxt("textRes.txt"))
    except ValueError:
        print("Помилка: введене значення не є числом.")
        sys.exit(1)
