using CqrsDemo.Models;
using MediatR;

namespace CqrsDemo.Features.Employees.Queries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<Employee?>;
}
