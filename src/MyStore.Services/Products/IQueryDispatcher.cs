using System.Threading.Tasks;

namespace MyStore.Services.Products
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}