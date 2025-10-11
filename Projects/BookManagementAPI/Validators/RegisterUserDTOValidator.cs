using BookManagementAPI.DTOs;
using FluentValidation;

namespace BookManagementAPI.Validators;

public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserDTOValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 50).WithMessage("Name must between 2 and 100 characters long")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        RuleFor(user => user.Role)
            .NotEmpty().WithMessage("Role is required")
            .Must(BeValidRole).WithMessage("Role must be 'admin' or 'user'");
    }

    private bool BeValidRole(string role)
    {
        var validRoles = new[] { "admin", "user" };
        return validRoles.Contains(role, StringComparer.OrdinalIgnoreCase);
    }
}