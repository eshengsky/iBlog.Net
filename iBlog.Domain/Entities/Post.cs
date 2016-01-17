using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace iBlog.Domain.Entities
{
    /// <summary>
    /// 文章集合
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Post
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public string UniqueId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "文章标题不能为空")]
        public string Title { get; set; }

        /// <summary>
        /// 文章别名
        /// </summary>
        [Required(ErrorMessage = "Alias不能为空")]
        public string Alias { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Required(ErrorMessage = "文章摘要不能为空")]
        public string Summary { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分类别名
        /// </summary>
        [Required]
        public string CategoryAlias { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Labels { get; set; }

        /// <summary>
        /// 外链Url
        /// </summary>
        [Required(ErrorMessage = "Url不能为空")]
        [Url(ErrorMessage = "请输入合法的Url，一般以 http:// 或 https:// 开头")]
        public string Url { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
    }

}
