
using SOLID_and_KISS_principles.GoodExample;
using SOLID_and_KISS_principles.Interfaces;
using SOLID_and_KISS_principles.Models;

Console.WriteLine("===SOLID and KISS Principles Demo ===");
Console.WriteLine();

DemonstrateSOLIDPrinciples();
DemonstrateKISSPrinciple();

static void DemonstrateSOLIDPrinciples()
{
    Console.WriteLine("===SOLID Priciples Demonstration===");

    DemonstrateSRP();
    DemonstrateOCP();
    DemonstrateLSP();
    DemonstrateISP();
    DemonstrateDIP();
}

static void DemonstrateSRP()
{
    Console.WriteLine("\n1. Single Responsibility Principle (SRP)");
    Console.WriteLine("----------------------------------------");

    Console.WriteLine("✔ GOOD: Separate Responsibility");
    var logger = new ConsoleLogger();
    var emailService = new EmailService(logger);
    var userRepository = new UserRepository(logger);

    var userService = new UserService(emailService, userRepository, logger);
    userService.RegisterUser("john@example.com", "securepassword257");

    Console.WriteLine("\nNotice how the good example separates email, logging, and database concerns!");
}

static void DemonstrateOCP()
{
    Console.WriteLine("\n2. Open-Close Priciple (OCP)");
    Console.WriteLine("----------------------------");

    Console.WriteLine("✔ GOOD: Can add new shapre without changing AreaCalculator");
    var logger = new ConsoleLogger();
    var areaCalculator = new AreaCalculator(logger);
    var shapes = new Shape[] {
        new Rectangle(5, 3),
        new Circle(2),
        new Triangle(4, 6)
    };

    var totalArea = areaCalculator.CalculateTotalArea(shapes);

    Console.WriteLine("\nAdding Triangle didn't require changing the AreaCalculator class!");
}

static void DemonstrateLSP()
{
    Console.WriteLine("\n3. Liskov Substitution Principle");
    Console.WriteLine("--------------------------------");

    Console.WriteLine("✔ Proper abstraction with interfaces");
    // All birds can ear and make sound
    Bird bird1 = new Sparrow();
    Bird bird2 = new Penguin();
    Bird bird3 = new Duck();

    bird1.Eat();
    bird1.MakeSound();
    bird2.Eat();
    bird2.MakeSound();
    bird3.Eat();
    bird3.MakeSound();

    // Only bird that can fly implement IFlyable
    if (bird1 is IFlyable flyingBird1) flyingBird1.Fly();
    if (bird2 is IFlyable flyingBird2) flyingBird2.Fly(); // Won't execute - penguin can't fly
    if (bird3 is IFlyable flyingBird3) flyingBird3.Fly();

    // Only bird that can swim implement ISwimmable
    if (bird2 is ISwimmable swimmingBird) swimmingBird.Swim();
    if (bird3 is ISwimmable swimmingBird2) swimmingBird2.Swim();

    Console.WriteLine("\nNo exceptions thrown! Each bird behaves correctly for its type.");
}

static void DemonstrateISP()
{
    Console.WriteLine("\n4. Interface Segregation Principle (ISP)");
    Console.WriteLine("----------------------------------------");

    Console.WriteLine("✔ GOOD: Focused interfaces");
    var logger = new ConsoleLogger();
    var notificationService = new NotificationService(logger);

    // Manager only implement ITaskManager
    var manager = new Manager(notificationService, logger);
    manager.AssignTask("Build new feature", "developer@company.com");
    manager.CreateSubTask("Build new feature", "Write unit testings");
    manager.ReviewTask("Build new feature");

    // TeamLead implements both ITaskManager and IWorker
    var teamLead = new TeamLead(notificationService, logger);
    teamLead.AssignTask("Fix bug", "junior@company.com");
    teamLead.WorkOnTask("Code review task");
    teamLead.ReportProgress("Code review task", 80);
    teamLead.CompleteTask("Code review task");

    Console.WriteLine("\nEach role implements only the interfaces it needs!");
}

static void DemonstrateDIP()
{
    Console.WriteLine("\n5. Dependency Inversion Priciple (DIP)");
    Console.WriteLine("--------------------------------------");

    Console.WriteLine("✔ GOOD: Dependencies injected through interfaces");
    var logger = new ConsoleLogger();
    var emailService = new EmailService(logger);
    var userRepository = new UserRepository(logger);
    var priceCalculator = new PriceCalculator(logger);

    // All dependencies are injected
    var order = new Order("customer@example.com");
    order.Items.Add(new OrderItem("Laptop", 1, 999.99m));
    order.Items.Add(new OrderItem("Mouse", 2, 29.99m));

    var total = priceCalculator.CalculateTotal(order.Items);
    var discountedTotal = priceCalculator.ApplyDiscount(total, 10);
    var finalTotal = priceCalculator.ApplyTax(total, 8.5m);

    order.TotalAmount = finalTotal;
    emailService.SendOrderInformation("customer@example.com", order.Id.ToString());

    Console.WriteLine($"Final order total: ${finalTotal:F2}");
    Console.WriteLine("\nEasy to swap implementations for testing or different environments!");
}

static void DemonstrateKISSPrinciple()
{
    Console.WriteLine("\n===KISS Principle Demonstrattion===");
    Console.WriteLine();

    Console.WriteLine("✔ GOOD: Keep simple, focus methods");
    var logger = new ConsoleLogger();
    var calculator = new PriceCalculator(logger);

    // Simple, clean steps
    var items = new List<OrderItem>
    {
        new OrderItem("Widget A", 1, 100m),
        new OrderItem("Widget B", 3, 50m),
    };

    var baseTotal = calculator.CalculateTotal(items);
    var discountedTotal = calculator.ApplyDiscount(baseTotal, 15);
    var finalTotal = calculator.ApplyTax(discountedTotal, 10);

    Console.WriteLine($"Good calculator result: ${finalTotal:F2}");
    Console.WriteLine("^ Simple methods, easy to understand, easy to test, easy to maintain!");
}