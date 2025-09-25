"""
Lab1Kilchytska303

Програма генерує зубчатий масив із заштрихованих областей квадратної матриці.
Варіант 11: верхня права та нижня ліва частини матриці.
Введення: розмірність матриці (n) і символ-заповнювач.
Виведення: зубчатий масив на екран і у текстовий файл.

@author Adriana
@version 1.0
@since version 1.0
"""

# Введення розміру
n = int(input("Enter size of square matrix (even number): "))

if n <= 1 or n % 2 != 0:
    print("Error! Size must be an even number greater than 1.")
    quit()

# Введення символу-заповнювача
filler = input("Enter symbol to fill in the matrix: ")
if len(filler) != 1:
    print("Error! You need to enter only one symbol.")
    quit()

half = n // 2

# Генерація зубчатого масиву
jagged = []
for i in range(n):
    row = [filler] * half
    jagged.append(row)

# Вивід на екран
print("Jagged matrix:")
for i in range(n):
    if i < half:
        print("  " * half, end="")   # зсув правої частини
        print(" ".join(jagged[i]))
    else:
        print(" ".join(jagged[i]))

# Запис у файл
with open("output.txt", "w", encoding="utf-8") as f:
    for i in range(n):
        if i < half:
            f.write("  " * half)
            f.write(" ".join(jagged[i]))
        else:
            f.write(" ".join(jagged[i]))
        f.write("\n")

print("Result written in file output.txt")
