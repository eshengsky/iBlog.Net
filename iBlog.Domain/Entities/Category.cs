using MongoDB.Bson.Serialization.Attributes;

namespace iBlog.Domain.Entities
{
    public class Category
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public string UniqueId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CateName { get; set; }

        /// <summary>
        /// 分类别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }
    }
}
