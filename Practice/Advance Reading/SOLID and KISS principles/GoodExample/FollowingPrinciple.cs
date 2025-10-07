using SOLID_and_KISS_principles.Interfaces;
using SOLID_and_KISS_principles.Models;

namespace SOLID_and_KISS_principles.GoodExample;

public class UserService
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public UserService(IEmailService emailService, IUserRepository userRepository, ILogger logger)
    {
        _emailService = emailService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public void RegisterUser(string email, string password)
    {
        try
        {
            if (!_emailService.ValidateEmail(email))
                throw new ArgumentException("Invalid email format");

            if (string.IsNullOrEmpty(password) || password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");

            if (_userRepository.UserExists(email))
                throw new InvalidOperationException("User already exists");

            var user = new User(email, password);
            _userRepository.SaveUser(user);

            _emailService.SendWelcomeEmail(email);

            _logger.LogInfo($"User registered successfully: {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to register user {email}: {ex.Message}");
            throw;
        }
    }
}

public class EmailService : IEmailService
{
    private readonly ILogger _logger;

    public EmailService(ILogger logger)
    {
        _logger = logger;
    }

    public bool ValidateEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email)
            && email.Contains('@')
            && email.Contains('.')
            && email.IndexOf('@') < email.LastIndexOf('.');
    }

    public void SendWelcomeEmail(string email)
    {
        Console.WriteLine($"📧 Sending welcome email to {email}");
        _logger.LogInfo($"Welcome email sent to {email}");
    }

    public void SendOrderInformation(string email, string orderId)
    {
        Console.WriteLine($"📧 Sending order information to {email} for order: {orderId}");
        _logger.LogInfo($"Order confirmation sent to {email}");
    }
}

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private readonly ILogger _logger;

    public UserRepository(ILogger logger)
    {
        _logger = logger;
    }

    public void SaveUser(User user)
    {
        _users.Add(user);
        _logger.LogInfo($"User saved: {user.Email}");
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public bool UserExists(string email)
    {
        return _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
    }

    public void LogError(Exception exception)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {exception.Message}");
    }
}

public class AreaCalculator
{
    private ILogger _logger;

    public AreaCalculator(ILogger logger)
    {
        _logger = logger;
    }

    public double CalculateTotalArea(Shape[] shapes)
    {
        double totalArea = 0;

        foreach (var shape in shapes)
        {
            var area = shape.Area();
            totalArea += area;

            _logger.LogInfo($"Calculate area for {shape.GetShapeInfo()}");
        }

        _logger.LogInfo($"Total area of all shapes: {totalArea:F2}");
        return totalArea;
    }
}

public class Manager : ITaskManager
{
    public readonly INotificationService _notificationService;
    public readonly ILogger _logger;

    public Manager(INotificationService notificationService, ILogger logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public void AssignTask(string taskName, string assignee)
    {
        Console.WriteLine($"👔 Manager assigning '{taskName}' to {assignee}");
        _notificationService.SendTaskAssignment(assignee, taskName);
        _logger.LogInfo($"Task '{taskName}' assigned to {assignee}");
    }

    public void CreateSubTask(string parentTask, string subTask)
    {
        Console.WriteLine($"👔 Manager creating subtask '{subTask}' under '{parentTask}'");
        _logger.LogInfo($"Subtask '{subTask}' created under '{parentTask}'");
    }

    public void ReviewTask(string taskName)
    {
        Console.WriteLine($"👔 Manager reviewing task: '{taskName}'");
        _logger.LogInfo($"Task reviewed: '{taskName}'");
    }
}

public class TeamLead : ITaskManager, IWorker
{
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;

    public TeamLead(INotificationService notificationService, ILogger logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public void AssignTask(string taskName, string assignee)
    {
        Console.WriteLine($"👨‍💼 TeamLead assigning '{taskName}' to {assignee}");
        _notificationService.SendTaskAssignment(assignee, taskName);
        _logger.LogInfo($"Task '{taskName}' assigned to {assignee}");
    }

    public void CreateSubTask(string parentTask, string subTask)
    {
        Console.WriteLine($"👨‍💼 TeamLead creating subtask '{subTask}' under '{parentTask}'");
        _logger.LogInfo($"Subtask '{subTask}' created under '{parentTask}'");
    }

    public void ReviewTask(string taskName)
    {
        Console.WriteLine($"👨‍💼 TeamLead reviewing task: '{taskName}'");
        _logger.LogInfo($"Task reviewed: '{taskName}'");
    }

    // Work responsibilities - TeamLead can also work on task
    public void WorkOnTask(string taskName)
    {
        Console.WriteLine($"👨‍💼 TeamLead work on task '{taskName}'");
        _logger.LogInfo($"TeamLead started working on '{taskName}'");
    }

    public void CompleteTask(string taskName)
    {
        Console.WriteLine($"👨‍💼 TeamLead completed task: {taskName}");
        _notificationService.SendTaskCompletion("manager@company.com", taskName);
        _logger.LogInfo($"Task completed: '{taskName}'");
    }

    public void ReportProgress(string taskName, int progressPercentage)
    {
        Console.WriteLine($"👨‍💼 TeamLead reports {progressPercentage}% progress on '{taskName}'");
        _logger.LogInfo($"Progress reported: {progressPercentage}% on '{taskName}'");
    }
}

public class NotificationService : INotificationService
{
    private readonly ILogger _logger;

    public NotificationService(ILogger logger)
    {
        _logger = logger;
    }

    public void SendTaskAssignment(string recipient, string taskName)
    {
        Console.WriteLine($"📫 Notification sent to {recipient}: You've been assigned '{taskName}'");
        _logger.LogInfo($"Task assignment notification sent to {recipient}");
    }

    public void SendTaskCompletion(string recipient, string taskName)
    {
        Console.WriteLine($"📫 Notification sent to {recipient}: Task '{taskName}' has been completed");
        _logger.LogInfo($"Task completion notification sent to {recipient}");
    }
}

public class PriceCalculator : IPriceCalculator
{
    private readonly ILogger _logger;

    public PriceCalculator(ILogger logger)
    {
        _logger = logger;
    }

    public decimal CalculateTotal(List<OrderItem> items)
    {
        decimal total = items.Sum(item => item.TotalPrice);
        _logger.LogInfo($"Base total calculated: ${total:F2}");
        return total;
    }

    public decimal ApplyDiscount(decimal total, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Discount percentage must be between 0 and 100");

        decimal discountAmount = total * (discountPercentage / 100);
        decimal discountedTotal = total - discountAmount;

        _logger.LogInfo($"Applied {discountPercentage}% discount: ${discountAmount:F2}");
        return discountedTotal;
    }

    public decimal ApplyTax(decimal total, decimal taxRate)
    {
        if (taxRate < 0)
            throw new ArgumentException("Tax rate cannot be negative");

        decimal taxAmount = total * (taxRate / 100);
        decimal totalWithTax = total + taxAmount;

        _logger.LogInfo($"Applied {taxRate}% tax: ${taxAmount:F2}");
        return totalWithTax;
    }
}