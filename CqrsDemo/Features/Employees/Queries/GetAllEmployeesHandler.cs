using CqrsDemo.Data;
using CqrsDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Features.Employees.Queries
{
    public class GetAllEmployeesHandler
        : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly AppDbContext _context;

        public GetAllEmployeesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Handle(
            GetAllEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
