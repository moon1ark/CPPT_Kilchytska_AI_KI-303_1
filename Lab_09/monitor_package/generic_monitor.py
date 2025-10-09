# monitor_package/generic_monitor.py

from .screen import Screen
from .power_supply import PowerSupply
from .control_buttons import ControlButtons


class GenericMonitor:
    """
    Базовий клас, що моделює монітор та його складові частини (композиція).
    """

    def __init__(self, screen: Screen, power: PowerSupply, buttons: ControlButtons):
        """
        Конструктор з параметрами для створення монітора.

        :param screen: Об'єкт екрана.
        :param power: Об'єкт блока живлення.
        :param buttons: Об'єкт кнопок керування.
        """
        self.screen = screen
        self.power = power
        self.buttons = buttons

    def turn_on(self):
        """Вмикає монітор."""
        self.power.turn_on()

    def turn_off(self):
        """Вимикає монітор."""
        self.power.turn_off()

    def increase_brightness(self):
        """Збільшує яскравість."""
        self.buttons.increase_brightness()

    def decrease_brightness(self):
        """Зменшує яскравість."""
        self.buttons.decrease_brightness()

    def increase_contrast(self):
        """Збільшує контраст."""
        self.buttons.increase_contrast()

    def decrease_contrast(self):
        """Зменшує контраст."""
        self.buttons.decrease_contrast()

    def show_info(self):
        """Виводить інформацію про стан монітора в консоль."""
        print(f"Screen: {self.screen}")
        print(f"Power: {self.power}")
        print(f"Controls: {self.buttons}")