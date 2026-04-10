using FluentValidation;
using GymManagement.Api.Dtos;

namespace GymManagement.Api.Validators;

public class UpdateClientValidator:AbstractValidator<UpdateClientDto>
{
    public UpdateClientValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        
        When(x => !string.IsNullOrEmpty(x.PhoneNumber), () => {
            RuleFor(x => x.PhoneNumber)
                .MinimumLength(10)
                .MaximumLength(15);
        });
    }
}