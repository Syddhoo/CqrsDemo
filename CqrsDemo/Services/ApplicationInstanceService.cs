namespace CqrsDemo.Services
{
    public class ApplicationInstanceService : IApplicationInstanceService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
