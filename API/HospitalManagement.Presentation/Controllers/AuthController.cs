using Azure;
using HospitalManagement.Application.Features.Identity.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
       private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginCommand command)
        {

            var response = await _mediator.Send(command);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Lax,
                Secure = true 
            };

            Response.Cookies.Append("user-token", response.Token, cookieOptions);

            return Ok(new { Token = response.Token });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            Response.Cookies.Delete("user-token", cookieOption);

            return Ok(new { message = "تم تسجيل الخروج بنجاح" });
        }
    }
}
