
using AutoMapper;
using CqrsDemo.DTOs;
using CqrsDemo.Features.Employees.Commands;
using CqrsDemo.Features.Employees.Queries;
using CqrsDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationInstanceService _instanceService;

        public EmployeesController(ISender sender, IMapper mapper, IApplicationInstanceService instanceService)
        {
            _sender = sender;
            _mapper = mapper;
            _instanceService = instanceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateEmployeeCommand command,
            CancellationToken cancellationToken)
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

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var employees = await _sender.Send(
                new GetAllEmployeesQuery(),
                cancellationToken);

            var response = _mapper.Map<List<EmployeeResponseDto>>(employees);

            return Ok(response);
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

            var response = _mapper.Map<EmployeeResponseDto>(employee);

            return Ok(response);
        }

        [HttpGet("instance")]
        public IActionResult GetApplicationInstance()
        {
            return Ok(new
            {
                ApplicationInstanceId = _instanceService.InstanceId
            });
        }
    }
}
