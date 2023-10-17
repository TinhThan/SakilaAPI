using MediatR;
using Microsoft.AspNetCore.Mvc;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.CQRS.User.Command;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models.Actor;
using SakilaAPI.Core.Models.User;

namespace SakilaAPI.Controllers
{
    /// <summary>
    /// Controller user
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserController : CustomBaseController
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="loginModel"></param>
        /// <response code="200">Đăng nhập thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<string, string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<Dictionary<string, string>> DangNhap(LoginModel loginModel)
        {
            return await _mediator.Send(new LoginCommand()
            {
                LoginModel = loginModel
            });
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="registerModel"></param>
        /// <response code="200">Đăng ký thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<string, string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<string> DangKy(RegisterModel registerModel)
        {
            return await _mediator.Send(new RegisterCommand()
            {
                RegisterModel = registerModel
            });
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="refreshTokenModel"></param>
        /// <response code="200">Refresh token thành công</response>
        /// <response code="400">Một vài thông tin truyền vào không hợp lệ</response>
        /// <response code="500">Lỗi đến từ server</response>
        [HttpPost("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponse))]
        public async Task<string> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            return await _mediator.Send(new RefreshTokenCommand()
            {
                RefreshTokenModel = refreshTokenModel
            });
        }
    }
}
