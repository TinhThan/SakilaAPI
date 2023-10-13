using AutoMapper;

namespace Sakila_B.Core.Paging
{
    /// <summary>
    /// search data
    /// </summary>
    public class Pagination<TEntity,TModel>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalData { get; set; }
        public IEnumerable<TModel> ListData { get; set; }


        #region private property
        private IQueryable<TEntity> _queryable { get; set; }
        private IMapper _mapper { get; set; }
        private BasePagingModel _paging { get; set; }
        #endregion

        public Pagination(IQueryable<TEntity> queryable, BasePagingModel basePagingModel, IMapper mapper)
        {
            _queryable = queryable;
            _mapper = mapper;
            _paging = basePagingModel;
        }

        //public async Task<Pagination<TEntity, TModel>> RunQueryAsync(CancellationToken cancellationToken, bool? iOrderedQueryable = true)
        //{
        //    var queryable = _queryable;
        //    if (iOrderedQueryable.Value)
        //    {
        //        queryable = _queryable as IOrderedQueryable<TEntity>;
        //    }
        //    Order
        //}
    }
}
