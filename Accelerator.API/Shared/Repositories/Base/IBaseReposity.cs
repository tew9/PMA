using Accelerator.Contracts.Base;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntityDTO
    {
        Task InsertAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(string id);
    }
}
