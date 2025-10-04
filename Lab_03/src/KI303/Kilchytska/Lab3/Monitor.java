package KI303.Kilchytska.Lab3;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

/**
 * Абстрактний клас Monitor моделює базовий монітор.
 */
public abstract class Monitor {
    protected Screen screen;
    protected PowerSupply power;
    protected ControlButtons buttons;
    protected PrintWriter logWriter;

    /**
     * Конструктор з параметрами.
     */
    public Monitor(Screen screen, PowerSupply power, ControlButtons buttons) throws IOException {
        this.screen = screen;
        this.power = power;
        this.buttons = buttons;
        logWriter = new PrintWriter(new FileWriter("monitor_log.txt", true));
        log("Monitor created: " + screen + ", " + power + ", " + buttons);
    }

    /** Увімкнути монітор */
    public void turnOn() {
        power.turnOn();
        log("Monitor turned ON");
    }

    /** Вимкнути монітор */
    public void turnOff() {
        power.turnOff();
        log("Monitor turned OFF");
    }

    /** Показати інформацію про монітор */
    public void showInfo() {
        System.out.println("Screen: " + screen);
        System.out.println("Power: " + power);
        System.out.println("Controls: " + buttons);
    }

    /** Метод для ведення журналу подій */
    protected void log(String message) {
        logWriter.println(message);
        logWriter.flush();
    }

    /** Закриття логу */
    public void closeLog() {
        if (logWriter != null) {
            log("Log closed.");
            logWriter.close();
        }
    }

    /**
     * Абстрактний метод, який мають реалізовувати всі підкласи монітора.
     * Наприклад, оновлення зображення на екрані.
     */
    public abstract void refreshDisplay();
}
