using CqrsDemo.Models;
using MediatR;

namespace CqrsDemo.Features.Employees.Queries
{
    public record GetAllEmployeesQuery : IRequest<List<Employee>>;
}
