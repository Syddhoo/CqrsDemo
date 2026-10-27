using CqrsDemo.DTOs;
using CqrsDemo.Features.Employees.Commands;
using CqrsDemo.Models;
using AutoMapper;

namespace CqrsDemo.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<Employee, EmployeeResponseDto>();
        }
    }
}
