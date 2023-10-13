using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sakila_B.Core.CQRS.Film.Query;
using Sakila_B.Core.Exceptions;
using Sakila_B.Core.Models.Film;
using System.IO;

namespace Sakila_B.Controllers
{
    [ApiController]
    [Route("api/film")]
    public class FilmController : CustomBaseController
    {

        private readonly ILogger<FilmController> _logger;

        public FilmController(ILogger<FilmController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Danh sách film by name
        /// </summary>
        /// <param name="Ids"></param>
        /// <response code="200">Lấy danh sách film thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("danhsach")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FilmModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<List<FilmModel>> DanhSachByIds([FromQuery] List<int> Ids)
        => await _mediator.Send(new DanhSachFilmByIdsQuery()
        {
            Ids = Ids
        });
    }
}
