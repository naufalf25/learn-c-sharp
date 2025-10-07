using SOLID_and_KISS_principles.Models;

namespace SOLID_and_KISS_principles.Interfaces;

public interface IEmailService
{
    bool ValidateEmail(string email);
    void SendWelcomeEmail(string email);
    void SendOrderInformation(string email, string orderId);
}

public interface IUserRepository
{
    void SaveUser(User user);
    User? GetUserByEmail(string email);
    bool UserExists(string email);
}

public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
    void LogError(Exception exception);
}

public interface IPriceCalculator
{
    decimal CalculateTotal(List<OrderItem> items);
    decimal ApplyDiscount(decimal total, decimal discountPercentage);
    decimal ApplyTax(decimal total, decimal taxRate);
}