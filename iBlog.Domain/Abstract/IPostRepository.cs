using System.Threading.Tasks;
using iBlog.Domain.Entities;

namespace iBlog.Domain.Abstract
{
    public interface IPostRepository
    {
        Task Insert(Post post);

        Task<bool> CheckAlias(string alias, string uid);

        Task<bool> Update(Post post);

        Task<bool> Delete(string postIds);

        Task<PostResult> GetPosts(string cateAlias, int pageIndex, int pageSize, SortBy sortBy = SortBy.Latest, FiltType filtType = FiltType.Content, string keyword = "");

        Task<PostResult> GetArticles(int pageIndex, int pageSize, string sortName, string sortOrder, string searchText, string cateAlias, string uniqueId, string title);

        Task<Post> GetPostByAlias(string id);

        Task<Post> GetPostById(string id);

        Task<long> GetPostCountByCate(string cateAlias);
    }
}
