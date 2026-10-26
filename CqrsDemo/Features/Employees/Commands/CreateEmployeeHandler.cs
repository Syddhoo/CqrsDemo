using CqrsDemo.Data;
using CqrsDemo.Models;
using MediatR;

namespace CqrsDemo.Features.Employees.Commands
{
    public class CreateEmployeeHandler
    : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateEmployeeHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Salary = request.Salary
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}
