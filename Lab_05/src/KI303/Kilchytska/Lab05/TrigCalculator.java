package KI303.Kilchytska.Lab05;
import java.io.*;
import java.util.Scanner;

/**
 * Клас <code>TrigCalculator</code> реалізує обчислення виразу ctg(x)/tg(x)
 * та надає методи для збереження результату в текстовий та двійковий файли,
 * а також для зчитування результату з цих файлів.
 *
 * @author Кільчицька Адріана 
 * @version 1.0
 */

public class TrigCalculator {

    private double result;

    /**
     * Обчислює значення виразу ctg(x)/tg(x).
     * Формула спрощується до 1 / (tg(x)^2).
     *
     * @param x Вхідне значення (кут у радіанах).
     * @throws IllegalArgumentException якщо tg(x) дорівнює нулю, що призводить до ділення на нуль.
     */
    public void calculate(double x) {
        double tanX = Math.tan(x);
        if (Math.abs(tanX) < 1e-9) { // Перевірка на близькість до нуля
            throw new IllegalArgumentException("Error: tg(x) equals zero, division by zero is not possible.");
        }
        result = 1.0 / (tanX * tanX);
    }
    
    /**
     * Повертає поточний результат обчислень.
     *
     * @return Значення результату.
     */
    public double getResult() {
        return result;
    }

    /**
     * Записує результат обчислень у текстовий файл.
     *
     * @param fName Ім'я файлу для запису.
     * @throws FileNotFoundException якщо файл не може бути створений.
     */
    public void writeResTxt(String fName) throws FileNotFoundException {
        PrintWriter f = new PrintWriter(fName);
        f.printf("%f", result);
        f.close();
        System.out.println("The result has been successfully saved to a text file: " + fName);
    }

    /**
     * Зчитує результат обчислень з текстового файлу.
     *
     * @param fName Ім'я файлу для читання.
     */
    public void readResTxt(String fName) {
        try {
            File f = new File(fName);
            if (f.exists()) {
                Scanner s = new Scanner(f);
                result = s.nextDouble();
                s.close();
                System.out.println("Result successfully read from text file: " + fName);
            } else {
                throw new FileNotFoundException("File " + fName + " not found.");
            }
        } catch (FileNotFoundException ex) {
            System.err.println(ex.getMessage());
        }
    }

    /**
     * Записує результат обчислень у двійковий файл.
     *
     * @param fName Ім'я файлу для запису.
     * @throws IOException у разі помилки запису.
     */
    public void writeResBin(String fName) throws IOException {
        DataOutputStream f = new DataOutputStream(new FileOutputStream(fName));
        f.writeDouble(result);
        f.close();
        System.out.println("The result has been successfully saved to a binary file.: " + fName);
    }

    /**
     * Зчитує результат обчислень з двійкового файлу.
     *
     * @param fName Ім'я файлу для читання.
     * @throws IOException у разі помилки читання.
     */
    public void readResBin(String fName) throws IOException {
        DataInputStream f = new DataInputStream(new FileInputStream(fName));
        result = f.readDouble();
        f.close();
        System.out.println("Result successfully read from binary file: " + fName);
    }
}