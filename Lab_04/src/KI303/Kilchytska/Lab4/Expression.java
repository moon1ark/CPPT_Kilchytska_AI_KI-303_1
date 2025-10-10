package KI303.Kilchytska.Lab4;

public class Expression {
    public double calculate(double x) throws CalculationException {
        double y, rad;
        rad = Math.toRadians(x);

        try {
            double tanX = Math.tan(rad);
            y = 1.0 / (tanX * tanX);

            if (Double.isNaN(y) || Double.isInfinite(y)) {
                throw new ArithmeticException();
            }
        } catch (ArithmeticException ex) {
            if (Math.abs(x % 180) == 90) {
                //  error message
                throw new CalculationException("Error: Tangent is undefined for the angle " + x + " degrees.");
            } else if (Math.abs(Math.tan(rad)) < 1e-9) {
                //  error message
                throw new CalculationException("Error: Division by zero. Tangent is 0 for the angle " + x + " degrees.");
            } else {
                //  error message
                throw new CalculationException("Unknown arithmetic error during calculation.");
            }
        }
        return y;
    }
}