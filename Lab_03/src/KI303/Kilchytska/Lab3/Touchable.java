package KI303.Kilchytska.Lab3;

/**
 * Інтерфейс Touchable описує взаємодію з сенсорним екраном.
 */
public abstract interface Touchable {
    /**
     * Метод для обробки натискання на екран.
     * @param x координата X дотику
     * @param y координата Y дотику
     */
    void onTouch(int x, int y);
}
