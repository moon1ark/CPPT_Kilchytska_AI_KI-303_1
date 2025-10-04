package KI303.Kilchytska.Lab3;

import java.io.IOException;

/**
 * Клас TouchScreenMonitor моделює монітор із сенсорним екраном.
 */
public class TouchScreenMonitor extends Monitor implements Touchable {
    private boolean touchEnabled;

    /**
     * Конструктор для створення сенсорного монітора.
     */
    public TouchScreenMonitor(Screen screen, PowerSupply power, ControlButtons buttons) throws IOException {
        super(screen, power, buttons);
        this.touchEnabled = true;
        log("TouchScreenMonitor initialized with touch support");
    }

    /**
     * Реалізація методу з інтерфейсу Touchable.
     * @param x координата X дотику
     * @param y координата Y дотику
     */
    @Override
    public void onTouch(int x, int y) {
        if (touchEnabled) {
            System.out.println("Touch detected at (" + x + ", " + y + ")");
            log("Touch at (" + x + ", " + y + ")");
        } else {
            System.out.println("Touchscreen is disabled!");
            log("Touch attempt while disabled");
        }
    }

    /**
     * Реалізація абстрактного методу refreshDisplay().
     */
    @Override
    public void refreshDisplay() {
        System.out.println("Display refreshed for touchscreen monitor.");
        log("Display refreshed");
    }

    /** Увімкнення або вимкнення сенсорної функції */
    public void setTouchEnabled(boolean enabled) {
        this.touchEnabled = enabled;
        log("Touch functionality set to " + enabled);
    }
}
