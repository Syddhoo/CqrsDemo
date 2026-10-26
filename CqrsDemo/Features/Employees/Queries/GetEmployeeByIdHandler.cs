using CqrsDemo.Data;
using CqrsDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Features.Employees.Queries
{
    public class GetEmployeeByIdHandler
    : IRequestHandler<GetEmployeeByIdQuery, Employee?>
    {
        private readonly AppDbContext _context;

        public GetEmployeeByIdHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> Handle(
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    e => e.Id == request.Id,
                    cancellationToken);
        }
    }
}
