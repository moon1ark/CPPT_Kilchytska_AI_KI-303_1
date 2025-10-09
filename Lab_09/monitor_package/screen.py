# monitor_package/screen.py

class Screen:
    """Клас Screen представляє екран монітора."""

    def __init__(self, size_in_inches: float, resolution: str, panel_type: str):
        """
        Конструктор для ініціалізації екрана.

        :param size_in_inches: Розмір екрана в дюймах.
        :param resolution: Роздільна здатність екрана (наприклад, "1920x1080").
        :param panel_type: Тип матриці (наприклад, "IPS").
        """
        self.size_in_inches = size_in_inches
        self.resolution = resolution
        self.panel_type = panel_type

    def __str__(self):
        """Повертає рядкове представлення об'єкта."""
        return f'{self.size_in_inches}" {self.panel_type} ({self.resolution})'