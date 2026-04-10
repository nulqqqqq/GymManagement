using System;
using System.Collections.Generic;
using FluentValidation;
using GymManagement.Api.Dtos.WorkoutSessions;

namespace GymManagement.Api.Validators;

public class UpdateWorkoutSessionValidator:AbstractValidator<UpdateWorkoutSessionDto>
{
    private readonly List<string> _allowedStatuses = new() 
    { 
        "Scheduled", "Completed", "Cancelled" 
    };
    
    public UpdateWorkoutSessionValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date and time are required.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.")
            .LessThanOrEqualTo(240).WithMessage("Workout duration cannot exceed 4 hours.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .Must(s => _allowedStatuses.Contains(s))
            .WithMessage($"Please choose a valid status: {string.Join(", ", _allowedStatuses)}");
        
        
    
    }
}