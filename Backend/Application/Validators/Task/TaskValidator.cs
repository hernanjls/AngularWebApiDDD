using FluentValidation;
using TEST.Application.Dtos.Task.Request;

namespace TEST.Application.Validators.Category
{
    public class TaskValidator : AbstractValidator<TaskRequestDto>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("The field name not can be null.")
                .NotEmpty().WithMessage("The field name not can be empty.");
        }
    }
}