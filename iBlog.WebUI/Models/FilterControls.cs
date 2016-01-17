namespace iBlog.WebUI.Models
{
    public class ArticleFilterControls
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sortName { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string sortOrder { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string searchText { get; set; }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        public string filter { get; set; }
    }

    public class ArticleFilterItem
    {
        public string CateName { get; set; }

        public string UniqueId { get; set; }

        public string Title { get; set; }
    }

    public class RegFilterControls
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string searchText { get; set; }
    }
}