# monitor_package/power_supply.py

class PowerSupply:
    """Клас PowerSupply представляє блок живлення монітора."""

    def __init__(self, power_consumption: int):
        """
        Конструктор для ініціалізації блока живлення.

        :param power_consumption: Споживана потужність у ватах.
        """
        self.power_consumption = power_consumption
        self.is_on = False  # Початковий стан - вимкнено

    def turn_on(self):
        """Вмикає блок живлення."""
        self.is_on = True

    def turn_off(self):
        """Вимикає блок живлення."""
        self.is_on = False

    def __str__(self):
        """Повертає рядкове представлення об'єкта."""
        state = "ON" if self.is_on else "OFF"
        return f'Power: {self.power_consumption}W, state={state}'