using iBlog.Domain.Entities;

namespace iBlog.WebUI.Models
{
    public class CategoryBar : Category
    {
        /// <summary>
        /// 分类下的文章数
        /// </summary>
        public long PostCount { get; set; }
    }
}