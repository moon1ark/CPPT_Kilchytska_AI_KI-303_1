package KI303.Kilchytska.Lab2;

/**
 * Клас PowerSupply представляє блок живлення монітора.
 */
public class PowerSupply {
    private int powerConsumption;
    private boolean isOn;

    public PowerSupply(int powerConsumption) {
        this.powerConsumption = powerConsumption;
        this.isOn = false;
    }

    public void turnOn() {
        isOn = true;
    }

    public void turnOff() {
        isOn = false;
    }

    public boolean isOn() {
        return isOn;
    }

    @Override
    public String toString() {
        return "Power: " + powerConsumption + "W, state=" + (isOn ? "ON" : "OFF");
    }
}
