package KI303.Kilchytska.Lab05;

import java.io.IOException;
import java.util.Scanner;

/**
 * Клас <code>CalculationApp</code> є головним класом для тестування
 * функціоналу класу <code>TrigCalculator</code>.
 *
 * @author Кільчицька Адріана
 * @version 1.0
 */
public class CalculationApp {

    /**
     * Головний метод програми.
     *
     * @param args Аргументи командного рядка (не використовуються).
     * @throws IOException у разі помилок вводу/виводу.
     */
    public static void main(String[] args) throws IOException {
        TrigCalculator obj = new TrigCalculator();
        Scanner s = new Scanner(System.in);

        try {
            System.out.print("Enter the value of x (angle in radians): ");
            double data = s.nextDouble();

            obj.calculate(data);
            System.out.println("The result of the calculation y=ctg(x)/tg(x): " + obj.getResult());

            // Робота з файлами
            String textFile = "text_result.txt";
            String binaryFile = "binary_result.bin";

            obj.writeResTxt(textFile);
            obj.writeResBin(binaryFile);

            // Читання та перевірка
            System.out.println("\n--- Reading from files ---");
            obj.readResBin(binaryFile);
            System.out.println("Result read from a binary file: " + obj.getResult());

            obj.readResTxt(textFile);
            System.out.println("Result read from a text file: " + obj.getResult());

        } catch (IllegalArgumentException e) {
            System.err.println("Calculation error: " + e.getMessage());
        } catch (Exception e) {
            System.err.println("An unknown error has occurred: " + e.getMessage());
        } finally {
            s.close();
        }
    }
}