using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using iBlog.Domain.Abstract;
using iBlog.Domain.Entities;
using iBlog.Domain.Helpers;

namespace iBlog.Domain.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoHelper<Post> _posts;
        private readonly MongoHelper<Category> _categories;

        public CategoryRepository()
        {
            _posts = new MongoHelper<Post>();
            _categories = new MongoHelper<Category>();
        }

        /// <summary>
        /// 保存全部分类
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task Save(List<Category> list)
        {
            //1. 将被删除分类的文章设为未分类
            var oldList = await _categories.Collection.Find(t => true).ToListAsync();
            var newUniqueids = list.Select(t => t.UniqueId).ToList();
            var newAliasList = list.Select(t => t.Alias).ToList();
            foreach (var cate in oldList)
            {
                if (!newUniqueids.Contains(cate.UniqueId))
                {
                    var update = Builders<Post>.Update.Set(t => t.CategoryAlias, "other");
                    await _posts.Collection.UpdateManyAsync(t => t.CategoryAlias.ToUpper() == cate.Alias.ToUpper(), update);
                }
                else
                {
                    //分类alias被修改了
                    if (!newAliasList.Contains(cate.Alias))
                    {
                        var newAlias = list.SingleOrDefault(t => t.UniqueId.ToUpper() == cate.UniqueId.ToUpper()).Alias;
                        var update = Builders<Post>.Update.Set(t => t.CategoryAlias, newAlias);
                        await _posts.Collection.UpdateManyAsync(t => t.CategoryAlias.ToUpper() == cate.Alias.ToUpper(), update);
                    }
                }
                //else
                //{
                //    若修改了图片则删除原先的图片 暂时去掉
                //    var oldImg = oldList.Single(t => t.UniqueId == cate.UniqueId).Img;
                //    var newImg = list.Single(t => t.UniqueId == cate.UniqueId).Img;
                //    if (!string.IsNullOrWhiteSpace(oldImg) && !oldImg.Equals(newImg, StringComparison.CurrentCultureIgnoreCase))
                //    {
                //        var imgFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + oldImg);
                //        imgFile.Delete();
                //    }
                //}
            }
            //2. 删除现在的所有分类
            await _categories.Collection.DeleteManyAsync(t => true);
            //3. 保存所有分类
            if (list.Any())
            {
                await _categories.Collection.InsertManyAsync(list);
            }
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetAll()
        {
            return await _categories.Collection.Find(t => true).ToListAsync();
        }

        /// <summary>
        /// 根据alias获取分类名称
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public async Task<string> GetNameByAlias(string alias)
        {
            var cateName = string.Empty;
            if (alias == "other")
            {
                cateName = "未分类";
            }
            else
            {
                var item = await _categories.Collection.Find(x => x.Alias.ToUpper() == alias.ToUpper()).SingleOrDefaultAsync();
                if (item != null)
                {
                    cateName = item.CateName;
                }
            }
            return cateName;
        }
    }
}
