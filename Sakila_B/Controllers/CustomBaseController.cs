using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sakila_B.Controllers
{
    /// <summary>
    /// CustomBaseController
    /// </summary>
    public class CustomBaseController : ControllerBase
    {
        /// <summary>
        /// Triển khai CQRS with Mediator
        /// </summary>
        public readonly IMediator _mediator;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="mediator"></param>
        public CustomBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
