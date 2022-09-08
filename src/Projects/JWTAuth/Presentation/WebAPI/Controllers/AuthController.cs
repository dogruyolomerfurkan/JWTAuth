using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
        {
            var response = await _mediator.Send(loginUserRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserRequest createUserRequest)
        {
            var response = await _mediator.Send(createUserRequest);
            return Created("success", response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
        {
            var response = await _mediator.Send(updateUserRequest);
            return Ok(response);
        }
    }
}
