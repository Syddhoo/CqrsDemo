using CqrsDemo.Features.Employees.Commands;
using CqrsDemo.Features.Employees.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateEmployeeCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var id = await _sender.Send(command, cancellationToken);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id },
                    new
                    {
                        Id = id,
                        Message = "Employee created successfully."
                    });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    Errors = ex.Errors.Select(error => new
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    })
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var employees = await _sender.Send(
                new GetAllEmployeesQuery(),
                cancellationToken);

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var employee = await _sender.Send(
                new GetEmployeeByIdQuery(id),
                cancellationToken);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
