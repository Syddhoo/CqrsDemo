using FluentValidation;

namespace CqrsDemo.Features.Employees.Commands
{
    public class CreateEmployeeValidator
    : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Employee name is required.")
                .MaximumLength(100)
                .WithMessage("Employee name cannot exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("Salary must be greater than zero.");
        }
    }
}
