import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

/**
 * Lab1Kilchytska303
 * 
 * Програма генерує зубчатий масив із заштрихованих областей квадратної матриці.
 * Введення: розмірність матриці (n) і символ-заповнювач.
 * Виведення: зубчатий масив на екран і у текстовий файл.
 *
 * @author Adriana
 * @version 1.0
 * @since version 1.0
 *
 */

public class Lab1KilchytskaKI303 {

    public static void main(String[] args) {
        try (Scanner sc = new Scanner(System.in)) {   // <-- автоматичне закриття

            // Введення розмірності
            System.out.print("Enter size of square matrix (even number): ");
            int n = sc.nextInt();
            sc.nextLine(); // очищення буфера

            // Перевірка на парність
            if (n <= 1 || n % 2 != 0) {
                System.out.println("Error! Size must be an even number greater than 1.");
                return;
            }

            // Введення символу-заповнювача
            System.out.print("Enter symbol to fill in the matrix: ");
            String input = sc.nextLine();

            if (input.length() != 1) {
                System.out.println("Error! You need to enter only one symbol.");
                return;
            }

            char fill = input.charAt(0);

            // Генерація зубчатого масиву
            char[][] jagged = new char[n][];
            int half = n / 2;

            for (int i = 0; i < n; i++) {
                jagged[i] = new char[half];
                for (int j = 0; j < half; j++) {
                    jagged[i][j] = fill;
                }
            }

            // Виведення на екран
            System.out.println("Jagged matrix:");
            for (int i = 0; i < n; i++) {
                if (i < half) {
                    for (int j = 0; j < half; j++) {
                        System.out.print("  ");
                    }
                    for (char c : jagged[i]) {
                        System.out.print(c + " ");
                    }
                } else {
                    for (char c : jagged[i]) {
                        System.out.print(c + " ");
                    }
                }
                System.out.println();
            }

            // Запис у файл
            try (FileWriter writer = new FileWriter("output.txt")) {
                for (int i = 0; i < n; i++) {
                    if (i < half) {
                        for (int j = 0; j < half; j++) {
                            writer.write("  ");
                        }
                        for (char c : jagged[i]) {
                            writer.write(c + " ");
                        }
                    } else {
                        for (char c : jagged[i]) {
                            writer.write(c + " ");
                        }
                    }
                    writer.write("\n");
                }
                System.out.println("Result written in file output.txt");
            } catch (IOException e) {
                System.out.println("Error while writing text in file!");
            }
        }
    }
}
