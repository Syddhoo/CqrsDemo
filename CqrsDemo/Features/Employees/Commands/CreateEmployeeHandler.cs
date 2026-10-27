using AutoMapper;
using CqrsDemo.Data;
using CqrsDemo.Models;
using MediatR;

namespace CqrsDemo.Features.Employees.Commands
{
    public class CreateEmployeeHandler
        : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}
