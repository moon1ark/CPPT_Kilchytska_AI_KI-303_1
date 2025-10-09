# monitor_package/control_buttons.py

class ControlButtons:
    """Клас ControlButtons описує кнопки керування монітором."""

    def __init__(self):
        """Конструктор для ініціалізації кнопок керування."""
        self.brightness = 50  # Початкова яскравість
        self.contrast = 50  # Початковий контраст

    def increase_brightness(self):
        """Збільшує яскравість, якщо вона менша за 100."""
        if self.brightness < 100:
            self.brightness += 1

    def decrease_brightness(self):
        """Зменшує яскравість, якщо вона більша за 0."""
        if self.brightness > 0:
            self.brightness -= 1

    def increase_contrast(self):
        """Збільшує контраст, якщо він менший за 100."""
        if self.contrast < 100:
            self.contrast += 1

    def decrease_contrast(self):
        """Зменшує контраст, якщо він більший за 0."""
        if self.contrast > 0:
            self.contrast -= 1

    def __str__(self):
        """Повертає рядкове представлення об'єкта."""
        return f'Brightness={self.brightness}, Contrast={self.contrast}'