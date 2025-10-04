package KI303.Kilchytska.Lab3;

/**
 * Клас-драйвер для тестування сенсорного монітора.
 */
public class MonitorTest {
    public static void main(String[] args) {
        try {
            Screen screen = new Screen(27, "2560x1440", "IPS");
            PowerSupply power = new PowerSupply(50);
            ControlButtons buttons = new ControlButtons();

            TouchScreenMonitor touchMonitor = new TouchScreenMonitor(screen, power, buttons);
            touchMonitor.turnOn();
            touchMonitor.showInfo();
            touchMonitor.onTouch(150, 300);
            touchMonitor.refreshDisplay();
            touchMonitor.turnOff();
            touchMonitor.closeLog();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
