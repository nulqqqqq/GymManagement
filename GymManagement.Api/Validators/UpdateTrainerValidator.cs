using FluentValidation;
using GymManagement.Api.Dtos.Trainers;

namespace GymManagement.Api.Validators;

public class UpdateTrainerValidator:AbstractValidator<UpdateTrainerDto>
{
    public UpdateTrainerValidator()
    {
        // Проверяем основные обязательные поля
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        // У тренера обычно есть специализация, ее тоже нужно защитить
        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(100);

        // Хитрый блок для необязательного номера телефона
        When(x => !string.IsNullOrEmpty(x.PhoneNumber), () => {
            RuleFor(x => x.PhoneNumber)
                .MinimumLength(10)
                .MaximumLength(15);
        });
    }
}