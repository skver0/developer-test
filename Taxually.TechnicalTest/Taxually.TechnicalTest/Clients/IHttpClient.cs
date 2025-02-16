namespace Taxually.TechnicalTest.Clients
{
    public interface IHttpClient
    {
        Task PostAsync<TRequest>(string url, TRequest request);
    }
}