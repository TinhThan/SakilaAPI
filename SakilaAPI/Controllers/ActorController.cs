using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core;
using SakilaAPI.Core.CQRS.Actor.Command;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models;
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

        public ActorController(ILogger<ActorController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Danh sách actor by name
        /// </summary>
        /// <param name="soluong"></param>
        /// <response code="200">Lấy danh sách actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpGet("danhsach/{soluong}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<List<ActorModel>> DanhSach(int soluong)
        {
            return await _mediator.Send(new ActorListQuery()
            {
                soLuong = soluong
            });
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
        public async Task<string> TaoMoi(ActorTaoMoiModel model)
        {
            return await _mediator.Send(new TaoMoiActorQuery()
            {
                ActorTaoMoiModel = model
            });
        }

        /// <summary>
        /// Cập nhật actor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">Cập nhật actor thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("capnhat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<string> CapNhat(int id, ActorCapNhatModel model)
        {
            return await _mediator.Send(new CapNhatActorQuery()
            {
                CapNhatModel = model,
                Id = id
            });
        }
    }
}
