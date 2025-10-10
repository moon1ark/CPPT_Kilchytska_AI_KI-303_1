package KI303.Kilchytska.Lab4;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;

import static java.lang.System.out;

public class Lab4Variant11 {
    public static void main(String[] args) {
        try {
            //  prompt
            out.print("Enter the file name to save the result: ");
            Scanner in = new Scanner(System.in);
            String fName = in.nextLine();
            PrintWriter fout = new PrintWriter(new File(fName));
            try {
                try {
                    Expression eq = new Expression();
                    //  prompt
                    out.print("Enter the value of X (in degrees): ");
                    double x = in.nextDouble();
                    double result = eq.calculate(x);
                    fout.printf("Calculation result for x=%.2f: y = %.4f", x, result);
                } finally {
                    fout.flush();
                    fout.close();
                    //  confirmation
                    out.println("\nResult successfully saved to file '" + fName + "'.");
                }
            } catch (CalculationException ex) {
                out.println(ex.getMessage());
            } catch (Exception ex) {
                //  error message
                out.println("An error occurred: " + ex.getMessage());
            }
        } catch (FileNotFoundException ex) {
            //  error message
            out.println("File Error: The specified file path may be incorrect.");
        }
    }
}