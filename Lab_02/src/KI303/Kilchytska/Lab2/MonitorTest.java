package KI303.Kilchytska.Lab2;

/**
 * Клас-драйвер для тестування класу Monitor.
 */
public class MonitorTest {
    public static void main(String[] args) {
        try {
            Monitor monitor = new Monitor();
            monitor.turnOn();
            monitor.increaseBrightness();
            monitor.increaseContrast();
            monitor.showInfo();
            monitor.decreaseBrightness();
            monitor.decreaseContrast();
            monitor.turnOff();
            monitor.closeLog();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
