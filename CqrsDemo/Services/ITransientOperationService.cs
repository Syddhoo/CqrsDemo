namespace CqrsDemo.Services
{
    public interface ITransientOperationService
    {
        Guid InstanceId { get; }
    }
}
