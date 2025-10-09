# main.py

from monitor_package.monitor import Monitor


def main():
    """Головна функція для тестування класу Monitor."""
    try:
        # Створюємо об'єкт монітора
        monitor = Monitor()

        # Виконуємо послідовність дій
        monitor.turn_on()
        monitor.increase_brightness()
        monitor.increase_contrast()

        print("--- Displaying Monitor Info ---")
        monitor.show_info()
        print("-----------------------------")

        monitor.decrease_brightness()
        monitor.decrease_contrast()
        monitor.turn_off()

    except Exception as e:
        print(f"An error occurred: {e}")
    finally:
        # Важливо закрити файл логу, навіть якщо сталася помилка
        if 'monitor' in locals() and hasattr(monitor, 'close_log'):
            monitor.close_log()
        print("\nProgram finished. Check 'monitor_log.txt' for details.")


if __name__ == "__main__":
    main()