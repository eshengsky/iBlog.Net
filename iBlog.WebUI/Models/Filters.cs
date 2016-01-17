using iBlog.Domain.Entities;

namespace iBlog.WebUI.Models
{
    public class Filters
    {
        public Filters()
        {
            this.PageSize = Settings.Config.PageSize;
        }

        /// <summary>
        /// 分类alias
        /// </summary>
        public string CateAlias { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序依据
        /// </summary>
        public SortBy SortBy { get; set; }

        /// <summary>
        /// 筛选类型
        /// </summary>
        public FiltType FiltType { get; set; }
        
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}