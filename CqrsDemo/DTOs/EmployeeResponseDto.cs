namespace CqrsDemo.DTOs
{
    public class EmployeeResponseDto
    {   
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
