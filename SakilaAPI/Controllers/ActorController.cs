using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Controllers
{
    /// <summary>
    /// Controller actor
    /// </summary>
    [ApiController]
    [Route("api/actor")]
    public class ActorController : CustomBaseController
    {

        private readonly ILogger<ActorController> _logger;

        /// <summary>
        /// Contructor ActorController
        /// </summary>
        /// <param name="logger"></param>
        public ActorController(ILogger<ActorController> logger, IMediator mediator):base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Danh sách actor by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Lấy danh sách actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("danhsach/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<ActorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(ExceptionResponse))]
        public async Task<IActionResult> DanhSach(string name)
        {
            using (var context = new DataContext())
            {
                var actors = await context.Actors.Where(t => t.FirstName.Contains(name) || t.LastName.Contains(name)).ToListAsync();
                if (!actors.Any())
                {
                    return NoContent();
                }
                return Ok(actors);
            }
        }

        /// <summary>
        /// Lấy chi tiết actor by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Lấy chi tiết actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("chitiet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<ActorModel> ChiTiet(int id)
        {
            return await _mediator.Send(new ActorDetailQuery()
            {
                Id = id
            });
        }

        /// <summary>
        /// Thêm mới actor
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Thêm mới actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("taomoi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<IActionResult> TaoMoi(ActorTaoMoiModel model)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var actorEntity = new ActorEntity()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        LastUpdate = DateTime.Now
                    };
                    await context.Actors.AddAsync(actorEntity);
                    await context.SaveChangesAsync();
                    _logger.LogDebug("Tao moi thanh cong Actor Id: {0}", actorEntity.Id);
                    return Ok("Thêm thành công");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// Xóa actor
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Xóa actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpDelete("xoa/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<IActionResult> Xoa(int id)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
                    if (actor == null)
                    {
                        return NoContent();
                    }
                    context.Remove(actor);
                    await context.SaveChangesAsync();
                    _logger.LogDebug("Xoa thanh cong Actor Id: {0}", actor.Id);
                    return Ok("Xóa thành công");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
