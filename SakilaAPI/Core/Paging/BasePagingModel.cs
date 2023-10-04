namespace SakilaAPI.Core.Paging
{
    /// <summary>
    /// Model base paging
    /// </summary>
    public class BasePagingModel
    {
        /// <summary>
        /// Key search paging
        /// </summary>
        public string KeySearch { get;set;}

        /// <summary>
        /// Order column name
        /// </summary>
        public string OrderColumnName { get; set;}

        /// <summary>
        /// Order column type
        /// </summary>
        public OrderType OrderColumnType { get; set; } = OrderType.Asc;

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

    public enum OrderType
    {
        Asc,
        Desc
    }
}
