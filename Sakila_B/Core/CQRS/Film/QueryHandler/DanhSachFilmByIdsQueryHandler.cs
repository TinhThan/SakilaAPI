using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila_B.Core.CQRS.Film.Query;
using Sakila_B.Core.Models.Film;

namespace Sakila_B.Core.CQRS.Film.QueryHandler
{
    public class DanhSachFilmByIdsQueryHandler : BaseHandler, IRequestHandler<DanhSachFilmByIdsQuery, List<FilmModel>>
    {
        public DanhSachFilmByIdsQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<List<FilmModel>> Handle(DanhSachFilmByIdsQuery request, CancellationToken cancellationToken)
        => await _dataContext.Films.Where(t => request.Ids.Contains(t.FilmId))
                        .ProjectTo<FilmModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
