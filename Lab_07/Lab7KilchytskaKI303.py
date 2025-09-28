"""
Lab7KilchytskaKI303

Програма генерує зубчатий масив із заштрихованих областей квадратної матриці.
Варіант 11: верхня права та нижня ліва частини матриці.
Введення: розмірність матриці (n) і символ-заповнювач.
Виведення: зубчатий масив на екран і у текстовий файл.

@author Adriana
@version 1.0
@since version 1.0
"""

from typing import List

def generate_jagged_matrix(n: int, filler: str) -> List[List[str]]:
    """
    Генерує "зубчатий" масив: кожен ряд містить n//2 символів-заповнювачів.
    Повертає список рядків (як списки символів).
    """
    half = (n // 2) - 1
    return [[filler] * half for _ in range(n)]

def format_matrix_lines(jagged: List[List[str]]) -> List[str]:
    """
    Форматує дані у список рядків для друку/запису.
    Для верхньої половини додає зсув (відповідно до варіанта).
    """
    n = len(jagged)
    half = len(jagged[0]) if jagged else 0
    lines = []
    for i, row in enumerate(jagged):
        if i < n // 2:
            lines.append("  " * half + " ".join(row) + " " + "&")
        else:
            lines.append(" ".join(row) + " " + "&")
    return lines

def write_output_file(lines: List[str], filename: str = "output.txt") -> None:
    """Записує список рядків у файл (utf-8)."""
    with open(filename, "w", encoding="utf-8") as f:
        for line in lines:
            f.write(line + "\n")

def main() -> None:
    """Інтерактивна частина програми (виконується лише при запуску як скрипт)."""
    try:
        n = int(input("Enter size of square matrix (even number): "))
    except ValueError:
        print("Error! Size must be an integer.")
        return

    if n <= 1 or n % 2 != 0:
        print("Error! Size must be an even number greater than 1.")
        return

    filler = input("Enter symbol to fill in the matrix: ")
    if len(filler) != 1:
        print("Error! You need to enter only one symbol.")
        return

    jagged = generate_jagged_matrix(n, filler)
    lines = format_matrix_lines(jagged)

    print("Jagged matrix:")
    for line in lines:
        print(line)

    write_output_file(lines, "output.txt")
    print("Result written in file output.txt")

if __name__ == "__main__":
    main()
