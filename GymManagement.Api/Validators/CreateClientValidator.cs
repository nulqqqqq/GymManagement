using FluentValidation;
using GymManagement.Api.Dtos;

namespace GymManagement.Api.Validators;

public class CreateClientValidator: AbstractValidator<CreateClientDto>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FistName cannot be empty")
            .MinimumLength(2).WithMessage("FistName must be at least 2 characters long")
            .MaximumLength(50).WithMessage("FistName is too long");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName cannot be empty")
            .MinimumLength(2).WithMessage("LastName must be at least 2 characters long")
            .MaximumLength(50).WithMessage("LastName is too long");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .MaximumLength(100).WithMessage("Email is too long")
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.Plan)
            .NotEmpty().WithMessage("Plan is required")
            .Must(x => x == "Basic" || x == "Premium" || x == "Elite")
            .WithMessage("Please select a valid plan: Basic, Premium or Elite");
    }
}