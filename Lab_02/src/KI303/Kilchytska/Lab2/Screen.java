package KI303.Kilchytska.Lab2;

/**
 * Клас Screen представляє екран монітора.
 */
public class Screen {
    private double sizeInInches;
    private String resolution;
    private String panelType;

    public Screen(double sizeInInches, String resolution, String panelType) {
        this.sizeInInches = sizeInInches;
        this.resolution = resolution;
        this.panelType = panelType;
    }

    public double getSizeInInches() {
        return sizeInInches;
    }

    public String getResolution() {
        return resolution;
    }

    public String getPanelType() {
        return panelType;
    }

    @Override
    public String toString() {
        return sizeInInches + "\" " + panelType + " (" + resolution + ")";
    }
}
