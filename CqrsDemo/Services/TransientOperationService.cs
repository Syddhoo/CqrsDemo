namespace CqrsDemo.Services
{
    public class TransientOperationService : ITransientOperationService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
