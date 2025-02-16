namespace Taxually.TechnicalTest.Clients
{
    public interface IQueueClient
    {
        Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}