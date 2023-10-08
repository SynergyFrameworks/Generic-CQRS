using OZK.Datalayer.Domain;
using OZK.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OZKService.Contracts
{
    public interface ITransfer<T> where T : class
    {
        Task<BankAccount> Transfer(OZKTransfer transfer, CancellationToken cancellationToken = default);

    }
}
