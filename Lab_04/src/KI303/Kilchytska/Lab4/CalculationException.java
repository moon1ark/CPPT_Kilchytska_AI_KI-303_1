package KI303.Kilchytska.Lab4;

/**
 * Class <code>CalculationException</code> уточнює виключення ArithmeticException
 * для помилок, специфічних для цього обчислення.
 *
 * @author Kilchytska Adriana
 * @version 1.0
 */
public class CalculationException extends ArithmeticException {
    public CalculationException() {}

    public CalculationException(String cause) {
        super(cause);
    }
}