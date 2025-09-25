package KI303.Kilchytska.Lab2;

/**
 * Клас ControlButtons описує кнопки керування монітором.
 */
public class ControlButtons {
    private int brightness;
    private int contrast;

    public ControlButtons() {
        brightness = 50;
        contrast = 50;
    }

    public void increaseBrightness() {
        if (brightness < 100) brightness++;
    }

    public void decreaseBrightness() {
        if (brightness > 0) brightness--;
    }

    public void increaseContrast() {
        if (contrast < 100) contrast++;
    }

    public void decreaseContrast() {
        if (contrast > 0) contrast--;
    }

    public int getBrightness() {
        return brightness;
    }

    public int getContrast() {
        return contrast;
    }

    @Override
    public String toString() {
        return "Brightness=" + brightness + ", Contrast=" + contrast;
    }
}
