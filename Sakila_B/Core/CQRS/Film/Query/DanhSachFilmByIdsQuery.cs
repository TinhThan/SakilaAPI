using MediatR;
using Sakila_B.Core.Models.Film;

namespace Sakila_B.Core.CQRS.Film.Query
{
    public class DanhSachFilmByIdsQuery : IRequest<List<FilmModel>>
    {
        public List<int> Ids { get; set; }
    }
}
