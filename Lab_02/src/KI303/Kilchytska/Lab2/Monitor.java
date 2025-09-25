package KI303.Kilchytska.Lab2;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

/**
 * Клас Monitor моделює монітор з його складовими частинами.
 */
public class Monitor {
    private Screen screen;
    private PowerSupply power;
    private ControlButtons buttons;

    private PrintWriter logWriter;

    /**
     * Конструктор за замовчуванням.
     */
    public Monitor() throws IOException {
        this(new Screen(24, "1920x1080", "IPS"), new PowerSupply(40), new ControlButtons());
    }

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

    public void turnOn() {
        power.turnOn();
        log("Monitor turned ON");
    }

    public void turnOff() {
        power.turnOff();
        log("Monitor turned OFF");
    }

    public void increaseBrightness() {
        buttons.increaseBrightness();
        log("Brightness increased to " + buttons.getBrightness());
    }

    public void decreaseBrightness() {
        buttons.decreaseBrightness();
        log("Brightness decreased to " + buttons.getBrightness());
    }

    public void increaseContrast() {
        buttons.increaseContrast();
        log("Contrast increased to " + buttons.getContrast());
    }

    public void decreaseContrast() {
        buttons.decreaseContrast();
        log("Contrast decreased to " + buttons.getContrast());
    }

    public void showInfo() {
        log("Monitor info requested");
        System.out.println("Screen: " + screen);
        System.out.println("Power: " + power);
        System.out.println("Controls: " + buttons);
    }

    private void log(String message) {
        logWriter.println(message);
        logWriter.flush();
    }

    /**
     * Метод для закриття логу (коректне завершення роботи з файлом).
     */
    public void closeLog() {
        if (logWriter != null) {
            log("Log closed.");
            logWriter.close();
        }
    }
}
