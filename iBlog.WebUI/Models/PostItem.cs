using System.Collections.Generic;
using iBlog.Domain.Entities;

namespace iBlog.WebUI.Models
{
    public class PostItem : Post
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CateName { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<string> LabelList { get; set; }

        /// <summary>
        /// 创建时间字符串
        /// </summary>
        public string CreateTimeStr { get; set; }

        /// <summary>
        /// 修改时间字符串
        /// </summary>
        public string ModifyTimeStr { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 链接网址
        /// </summary>
        public string Host { get; set; }
    }
}