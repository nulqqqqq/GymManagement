using System;
using System.ComponentModel;
using FluentValidation;
using GymManagement.Api.Dtos.WorkoutSessions;

namespace GymManagement.Api.Validators;

public class CreateWorkoutSessionValidator: AbstractValidator<CreateWorkoutSessionDto>
{
    public CreateWorkoutSessionValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date and time are required")
            .GreaterThan(DateTime.UtcNow).WithMessage("You cannot schedule a workout in the past");
        
        RuleFor(x => x.DurationOnMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes.")
            .LessThanOrEqualTo(240).WithMessage("Workout duration cannot exceed 4 hours.");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId is required to assign a client.");

        RuleFor(x => x.TrainerId)
            .NotEmpty().WithMessage("TrainerId is required to assign a trainer.");
    }
}   