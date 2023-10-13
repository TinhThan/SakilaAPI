using AutoMapper;

namespace Sakila_B.Core.CQRS
{
    public class BaseHandler
    {
        public readonly DataContext _dataContext;
        public readonly IMapper _mapper;

        public BaseHandler(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
    }
}
