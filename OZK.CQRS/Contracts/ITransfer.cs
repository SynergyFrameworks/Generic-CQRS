using System.Threading.Tasks;

namespace OZK.CQRS.Contracts
{
    public interface ITransfer<T> where T : class
    {
        Task<T> Transfer(T model);


    }
}
