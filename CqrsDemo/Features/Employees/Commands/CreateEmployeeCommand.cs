using MediatR;

namespace CqrsDemo.Features.Employees.Commands
{
    public record CreateEmployeeCommand(
    string Name,
    decimal Salary
) : IRequest<int>;
}
