using BookManagementAPI.Models;
using FluentValidation;

namespace BookManagementAPI.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Please enter a valid email address");

        RuleFor(user => user.FullName)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 100)
            .WithMessage("Name must between 2 and 100 characters long")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Name can only contain letters and spaces");

        RuleFor(user => user.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt is required")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("CreatedAt cannot in the future");
    }
}