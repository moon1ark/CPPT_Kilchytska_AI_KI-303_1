# monitor_package/monitor.py

from .generic_monitor import GenericMonitor
from .screen import Screen
from .power_supply import PowerSupply
from .control_buttons import ControlButtons


class Monitor(GenericMonitor):
    """
    Похідний клас, що розширює GenericMonitor, додаючи логування дій у файл.
    """

    def __init__(self):
        """
        Конструктор за замовчуванням. Створює монітор з типовими компонентами
        та ініціалізує логер.
        """
        # Створення компонентів за замовчуванням
        default_screen = Screen(24, "1920x1080", "IPS")
        default_power = PowerSupply(40)
        default_buttons = ControlButtons()

        # Виклик конструктора базового класу
        super().__init__(default_screen, default_power, default_buttons)

        # Ініціалізація логера
        self.log_file = open("monitor_log.txt", "a", encoding="utf-8")
        self._log(f"Monitor created: {self.screen}, {self.power}, {self.buttons}")

    def _log(self, message: str):
        """Приватний метод для запису повідомлення в лог-файл."""
        self.log_file.write(message + "\n")
        self.log_file.flush()  # Примусовий запис з буфера у файл

    # Перевизначення методів для додавання логування
    def turn_on(self):
        super().turn_on()
        self._log("Monitor turned ON")

    def turn_off(self):
        super().turn_off()
        self._log("Monitor turned OFF")

    def increase_brightness(self):
        super().increase_brightness()
        self._log(f"Brightness increased to {self.buttons.brightness}")

    def decrease_brightness(self):
        super().decrease_brightness()
        self._log(f"Brightness decreased to {self.buttons.brightness}")

    def increase_contrast(self):
        super().increase_contrast()
        self._log(f"Contrast increased to {self.buttons.contrast}")

    def decrease_contrast(self):
        super().decrease_contrast()
        self._log(f"Contrast decreased to {self.buttons.contrast}")

    def show_info(self):
        self._log("Monitor info requested")
        super().show_info()

    def close_log(self):
        """Метод для коректного закриття файлу логу."""
        if self.log_file:
            self._log("Log closed.")
            self.log_file.close()