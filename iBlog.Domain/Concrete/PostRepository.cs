using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using iBlog.Domain.Abstract;
using iBlog.Domain.Entities;
using iBlog.Domain.Helpers;

namespace iBlog.Domain.Concrete
{
    public class PostRepository : IPostRepository
    {
        private readonly MongoHelper<Post> _posts;

        public PostRepository()
        {
            _posts = new MongoHelper<Post>();
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Insert(Post post)
        {
            await _posts.Collection.InsertOneAsync(post);
        }

        /// <summary>
        /// 判断alias是否唯一
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<bool> CheckAlias(string alias, string uid)
        {
            var result = false;
            long count;
            //新增文章
            if (string.IsNullOrWhiteSpace(uid))
            {
                count = await _posts.Collection.CountAsync(t => t.Alias.ToUpper() == alias.ToUpper());
                result = count == 0;
            }
            else //更新文章
            {
                count = await _posts.Collection.CountAsync(t => t.Alias.ToUpper() == alias.ToUpper());
                if (count == 0)
                {
                    result = true;
                }
                else if (count == 1)
                {
                    var item = await _posts.Collection.Find(t => t.UniqueId.ToUpper() == uid.ToUpper()).SingleOrDefaultAsync();
                    if (item != null)
                    {
                        var anotherAlias = item.Alias;
                        if (anotherAlias == alias)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<bool> Update(Post post)
        {
            var update = Builders<Post>.Update.Set(t => t.Title, post.Title)
                .Set(t => t.Alias, post.Alias)
                .Set(t => t.CategoryAlias, post.CategoryAlias)
                .Set(t => t.Labels, post.Labels)
                .Set(t => t.Summary, post.Summary)
                .Set(t => t.Content, post.Content)
                .Set(t => t.Source, post.Source)
                .Set(t => t.Url, post.Url)
                .Set(t => t.ModifyTime, DateTime.Now);
            var task = await _posts.Collection.UpdateOneAsync(t => t.UniqueId.ToUpper() == post.UniqueId.ToUpper(), update);
            if (task.ModifiedCount > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除文章（软删除）
        /// </summary>
        /// <param name="postIds"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string postIds)
        {
            var postIdArray = postIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var update = Builders<Post>.Update.Set(t => t.IsActive, false);
            var task = await _posts.Collection.UpdateManyAsync(t => postIdArray.Contains(t.UniqueId), update);
            if (task.ModifiedCount == postIds.Count())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取筛选条件下的所有文章
        /// </summary>
        /// <param name="cateAlias"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="filtType"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<PostResult> GetPosts(string cateAlias, int pageIndex, int pageSize, SortBy sortBy = SortBy.Latest, FiltType filtType = FiltType.Content, string keyword = "")
        {
            var result = new PostResult();
            var query = await _posts.Collection.Find(x => x.IsActive).ToListAsync();
            if (!string.IsNullOrWhiteSpace(cateAlias))
            {
                query = query.Where(x => x.CategoryAlias.ToUpper() == cateAlias.ToUpper()).ToList();
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (filtType == FiltType.Title)
                {
                    query = query.Where(x => x.Title.Contains(keyword)).ToList();
                }
                else if (filtType == FiltType.Label)
                {
                    query = query.Where(x => x.Labels.Contains(keyword)).ToList();
                }
                else if (filtType == FiltType.Datetime)
                {
                    DateTime dt;
                    DateTime.TryParse(keyword, out dt);
                    query = query.Where(x => x.CreateTime.Date == dt.Date).ToList();
                }
                else
                {
                    query = query.Where(x => x.Content != null ? (x.Title.Contains(keyword) || x.Summary.Contains(keyword) || x.Content.Contains(keyword)) : (x.Title.Contains(keyword) || x.Summary.Contains(keyword))).ToList();
                }
            }
            result.PostCount = query.Count;
            if (sortBy == SortBy.Hotest)
            {
                query = query.OrderByDescending(x => x.ViewCount).ThenByDescending(x => x.CreateTime).ToList();
            }
            else
            {
                query = query.OrderByDescending(x => x.CreateTime).ToList();
            }
            result.PostList = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }

        /// <summary>
        /// 获取文章管理页面列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="searchText"></param>
        /// <param name="cateAlias"></param>
        /// <param name="uniqueId"></param> 
        /// <param name="title"></param> 
        /// <returns></returns>
        public async Task<PostResult> GetArticles(int pageIndex, int pageSize, string sortName, string sortOrder, string searchText, string cateAlias, string uniqueId, string title)
        {
            var result = new PostResult();
            var query = await _posts.Collection.Find(x => x.IsActive).ToListAsync();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(x => x.Content != null ? (x.UniqueId.ToUpper().Contains(searchText.ToUpper()) || x.Title.ToUpper().Contains(searchText.ToUpper()) || x.Summary.ToUpper().Contains(searchText.ToUpper()) || x.Content.ToUpper().Contains(searchText.ToUpper())) : (x.UniqueId.ToUpper().Contains(searchText.ToUpper()) || x.Title.ToUpper().Contains(searchText.ToUpper()) || x.Summary.ToUpper().Contains(searchText.ToUpper()))).ToList();
            }
            if (!string.IsNullOrWhiteSpace(cateAlias))
            {
                query = query.Where(x => x.CategoryAlias.ToUpper() == cateAlias.ToUpper()).ToList();
            }
            if (!string.IsNullOrWhiteSpace(uniqueId))
            {
                query = query.Where(x => x.UniqueId.ToUpper().Contains(uniqueId.ToUpper())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.ToUpper().Contains(title.ToUpper())).ToList();
            }
            result.PostCount = query.Count;
            switch (sortName)
            {
                case "ModifyTime":
                    if (sortOrder == "desc")
                    {
                        query = query.OrderByDescending(x => x.ModifyTime).ThenByDescending(x => x.CreateTime).ToList();
                    }
                    else
                    {
                        query = query.OrderBy(x => x.ModifyTime).ThenBy(x => x.CreateTime).ToList();
                    }
                    break;
                case "ViewCount":
                    if (sortOrder == "desc")
                    {
                        query = query.OrderByDescending(x => x.ViewCount).ThenByDescending(x => x.CreateTime).ToList();
                    }
                    else
                    {
                        query = query.OrderBy(x => x.ViewCount).ThenBy(x => x.CreateTime).ToList();
                    }
                    break;
                default:
                    if (sortOrder == "desc")
                    {
                        query = query.OrderByDescending(x => x.CreateTime).ToList();
                    }
                    else
                    {
                        query = query.OrderBy(x => x.CreateTime).ToList();
                    }
                    break;
            }
            result.PostList = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }

        /// <summary>
        /// 根据alias获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post> GetPostByAlias(string id)
        {
            var post = await _posts.Collection.Find(t => t.Alias.ToUpper() == id.ToUpper()).SingleOrDefaultAsync();
            var views = post?.ViewCount ?? 0;
            var update = Builders<Post>.Update.Set(t => t.ViewCount, views + 1);
            await _posts.Collection.UpdateOneAsync(t => t.Alias.ToUpper() == id.ToUpper(), update);
            return post;
        }

        /// <summary>
        /// 根据id获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post> GetPostById(string id)
        {
            var post = await _posts.Collection.Find(t => t.UniqueId.ToUpper() == id.ToUpper()).SingleOrDefaultAsync();
            return post;
        }

        /// <summary>
        /// 获取指定分类下的文章数目
        /// </summary>
        /// <param name="cateAlias"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<long> GetPostCountByCate(string cateAlias)
        {
            if (string.IsNullOrWhiteSpace(cateAlias))
            {
                return await _posts.Collection.CountAsync(x => x.IsActive);
            }
            return await _posts.Collection.CountAsync(x => x.IsActive && x.CategoryAlias.ToUpper() == cateAlias.ToUpper());
        }
    }
}
