using SakilaAPI.Core.Exceptions;

namespace SakilaAPI.Core.Paging
{
    public static class OrderByHyper
    {
        public static void SetValueOrderColumnName<TSearchCriteria, TEntity>(TSearchCriteria searchCriteria, TEntity entity)
        {
            var searchPropertyInfos = searchCriteria.GetType().GetProperties();
            var entityPropertyInfos = entity.GetType().GetProperties();
            var orderColumnName = searchPropertyInfos.FirstOrDefault(t => t.Name == nameof(BasePagingModel.OrderColumnName));
            if (orderColumnName == null)
            {
                throw new StatusClientErrorException();
            }
            var entityPropertyInfo = entityPropertyInfos.FirstOrDefault(t => t.Name == orderColumnName.GetValue(searchCriteria).ToString());

            if (entityPropertyInfo == null)
                orderColumnName.SetValue(searchCriteria, "last_update");
        }
    }
}
