using System.Collections.Generic;
using System.Threading.Tasks;
using iBlog.Domain.Entities;

namespace iBlog.Domain.Abstract
{
    public interface ICategoryRepository
    {
        Task Save(List<Category> list);

        Task<List<Category>> GetAll();

        Task<string> GetNameByAlias(string alias);
    }
}
