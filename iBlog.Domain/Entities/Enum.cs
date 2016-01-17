namespace iBlog.Domain.Entities
{
    /// <summary>
    /// 排序依据
    /// </summary>
    public enum SortBy
    {
        /// <summary>
        /// 最新
        /// </summary>
        Latest = 0,

        /// <summary>
        /// 最热
        /// </summary>
        Hotest = 1
    }

    /// <summary>
    /// 筛选类型
    /// </summary>
    public enum FiltType
    {
        /// <summary>
        /// 全文
        /// </summary>
        Content = 0,

        /// <summary>
        /// 标题
        /// </summary>
        Title = 1,

        /// <summary>
        /// 标签
        /// </summary>
        Label = 2,

        /// <summary>
        /// 发布时间
        /// </summary>
        Datetime = 3
    }
}
