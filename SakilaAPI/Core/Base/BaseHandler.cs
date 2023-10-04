using AutoMapper;

namespace SakilaAPI.Core.Base
{
    /// <summary>
    /// Base handler có đăng ký sẳn mapper, datacontext
    /// </summary>
    public class BaseHandler
    {
        public readonly IMapper _mapper;
        public readonly DataContext _dataContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataContext"></param>
        public BaseHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
    }
}
