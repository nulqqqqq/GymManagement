using System.Collections.Generic;
using FluentValidation;
using GymManagement.Api.Dtos.Trainers;

namespace GymManagement.Api.Validators;

public class CreateTrainerValidator: AbstractValidator<CreateTrainerDto>
{
    private readonly List<string> _allowedSpecializations = new()
    { 
        "Yoga", "Crossfit", "Boxing", "Bodybuilding", "Pilates" 
    };
    
    public CreateTrainerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("First name is too long");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name  cannot be empty")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Last name is too long");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .MaximumLength(100).WithMessage("Email is too long")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .Must(s => _allowedSpecializations.Contains(s))
            .WithMessage($"Please choose a valid specialization: {string.Join(", ", _allowedSpecializations)}");
    }
}