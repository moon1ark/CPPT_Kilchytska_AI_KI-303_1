package KI303.Kilchytska.Lab6;
import java.util.ArrayList;
import java.util.List;

/**
 * Інтерфейс, що представляє будь-який об'єкт, який можна орендувати в торговому центрі.
 * Вимагає реалізації порівняння для визначення "найбільшого" орендаря.
 */
interface RentableSpace extends Comparable<RentableSpace> {
    /**
     * Повертає вартість місячної оренди приміщення.
     * @return Вартість оренди у вигляді числа double.
     */
    double getMonthlyRent();

    /**
     * Виводить детальну інформацію про орендаря на консоль.
     */
    void displayInfo();
}

/**
 * Клас, що представляє магазин у торговому центрі.
 * Реалізує інтерфейс RentableSpace для можливості розміщення в ТЦ.
 */
class Store implements RentableSpace {
    private String storeName;
    private String productCategory;
    private double monthlyRent;

    /**
     * Конструктор для створення екземпляра магазину.
     * @param storeName Назва магазину.
     * @param productCategory Категорія товарів.
     * @param monthlyRent Вартість місячної оренди.
     */
    public Store(String storeName, String productCategory, double monthlyRent) {
        this.storeName = storeName;
        this.productCategory = productCategory;
        this.monthlyRent = monthlyRent;
    }

    @Override
    public double getMonthlyRent() {
        return monthlyRent;
    }

    @Override
    public void displayInfo() {
        System.out.println("Shop: " + storeName + ", Category: " + productCategory + ", Rent: $" + monthlyRent);
    }

    /**
     * Порівнює цей магазин з іншим орендарем за розміром орендної плати.
     * @param other Інший об'єкт, що реалізує RentableSpace.
     * @return -1, 0 або 1, якщо оренда цього магазину менша, рівна або більша за іншого.
     */
    @Override
    public int compareTo(RentableSpace other) {
        return Double.compare(this.monthlyRent, other.getMonthlyRent());
    }
}

/**
 * Клас, що представляє ресторан у торговому центрі.
 * Реалізує інтерфейс RentableSpace.
 */
class Restaurant implements RentableSpace {
    private String restaurantName;
    private String cuisineType;
    private int seatingCapacity;
    private double monthlyRent;

    /**
     * Конструктор для створення екземпляра ресторану.
     * @param restaurantName Назва ресторану.
     * @param cuisineType Тип кухні.
     * @param seatingCapacity Кількість посадкових місць.
     * @param monthlyRent Вартість місячної оренди.
     */
    public Restaurant(String restaurantName, String cuisineType, int seatingCapacity, double monthlyRent) {
        this.restaurantName = restaurantName;
        this.cuisineType = cuisineType;
        this.seatingCapacity = seatingCapacity;
        this.monthlyRent = monthlyRent;
    }

    @Override
    public double getMonthlyRent() {
        return monthlyRent;
    }

    @Override
    public void displayInfo() {
        System.out.println("Restaurant: " + restaurantName + ", Kitchen: " + cuisineType + ", Places: " + seatingCapacity + ", Rent: $" + monthlyRent);
    }

    /**
     * Порівнює цей ресторан з іншим орендарем за розміром орендної плати.
     * @param other Інший об'єкт, що реалізує RentableSpace.
     * @return -1, 0 або 1, якщо оренда цього ресторану менша, рівна або більша за іншого.
     */
    @Override
    public int compareTo(RentableSpace other) {
        return Double.compare(this.monthlyRent, other.getMonthlyRent());
    }
}

/**
 * Параметризований клас, що реалізує модель Торгового Центру.
 * Може містити будь-які об'єкти, що є підтипами RentableSpace.
 * @param <T> Тип орендарів, яких може містити ТЦ.
 */
class ShoppingCenter<T extends RentableSpace> {
    /**
     * Внутрішній список для зберігання орендарів.
     */
    private List<T> tenants;

    /**
     * Конструктор за замовчуванням, що ініціалізує порожній список орендарів.
     */
    public ShoppingCenter() {
        tenants = new ArrayList<>();
    }

    /**
     * Додає нового орендаря до торгового центру. (Метод 1: розміщення елемента)
     * @param tenant Орендар для додавання.
     */
    public void addTenant(T tenant) {
        tenants.add(tenant);
        System.out.print("Added tenant: ");
        tenant.displayInfo();
    }

    /**
     * Видаляє орендаря з торгового центру. (Метод 2: виймання елемента)
     * @param tenant Орендар для видалення.
     * @return true, якщо орендаря було знайдено та видалено, інакше false.
     */
    public boolean removeTenant(T tenant) {
        boolean removed = tenants.remove(tenant);
        if (removed) {
            System.out.print("Tenant removed: ");
            tenant.displayInfo();
        } else {
            System.out.println("Unable to find the specified tenant for deletion.");
        }
        return removed;
    }

    /**
     * Знаходить орендаря з найвищою місячною орендною платою. (Метод 3: пошук максимального)
     * @return Орендар з максимальною орендою або null, якщо центр порожній.
     */
    public T findMaxRentTenant() {
        if (tenants.isEmpty()) {
            return null;
        }

        T maxTenant = tenants.get(0);
        for (int i = 1; i < tenants.size(); i++) {
            if (tenants.get(i).compareTo(maxTenant) > 0) {
                maxTenant = tenants.get(i);
            }
        }
        return maxTenant;
    }

    /**
     * Виводить інформацію про всіх поточних орендарів. (Метод 4: опрацювання даних)
     */
    public void displayAllTenants() {
        System.out.println("\n--- Current tenants in the Shopping Center ---");
        if (tenants.isEmpty()) {
            System.out.println("The shopping center is currently empty..");
        } else {
            for (T tenant : tenants) {
                tenant.displayInfo();
            }
        }
        System.out.println("------------------------------------------\n");
    }
}

/**
 * Головний клас-драйвер для демонстрації роботи класу ShoppingCenter.
 */
public class ShoppingCenterDriver {

    /**
     * Точка входу в програму.
     * @param args Аргументи командного рядка (не використовуються).
     */
    public static void main(String[] args) {
        // Використання wildcard для більшої гнучкості контейнера, як у прикладі
        ShoppingCenter<? super RentableSpace> mall = new ShoppingCenter<RentableSpace>();

        System.out.println("Loading the shopping center...");
        
        // Створення екземплярів двох різних класів
        Store electronicsStore = new Store("ElectroWorld", "Electronics", 5500.75);
        Restaurant italianRestaurant = new Restaurant("Bella Italia", "Italian", 80, 7200.50);
        Store clothingStore = new Store("Fashion Hub", "Clothes", 6100.00);
        Restaurant cafe = new Restaurant("The Daily Grind", "Coffe shop", 40, 4800.00);

        // Додавання орендарів до ТЦ
        mall.addTenant(electronicsStore);
        mall.addTenant(italianRestaurant);
        mall.addTenant(clothingStore);
        mall.addTenant(cafe);

        // Відображення всіх орендарів
        mall.displayAllTenants();

        // Пошук та виведення орендаря з максимальною орендою
        // Потрібне приведення типів, оскільки компілятор знає лише про `Object` через wildcard `? super`
        RentableSpace maxRentTenant = (RentableSpace) mall.findMaxRentTenant();
        
        if (maxRentTenant != null) {
            System.out.println("Tenant with the highest rent:");
            maxRentTenant.displayInfo();
        } else {
            System.out.println("No tenant found, shopping center is empty.");
        }
        
        System.out.println();

        // Демонстрація видалення елемента
        System.out.println("Attempt to remove tenant...");
        mall.removeTenant(electronicsStore);

        // Повторне відображення списку орендарів для перевірки змін
        mall.displayAllTenants();
    }
}