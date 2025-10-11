using BookManagementAPI.DTOs;
using FluentValidation;

namespace BookManagementAPI.Validators;

public class UpdateBookDTOValidator : AbstractValidator<UpdateBookDTO>
{
    public UpdateBookDTOValidator()
    {
        RuleFor(book => book.Id)
            .NotEmpty().WithMessage("Book Id is required.");

        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(book => book.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(100).WithMessage("Author cannot exceed 100 characters.");

        RuleFor(book => book.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .MaximumLength(50).WithMessage("Genre cannot exceed 50 characters.");

        RuleFor(book => book.PublishedDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");

        RuleFor(book => book.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
    }
}