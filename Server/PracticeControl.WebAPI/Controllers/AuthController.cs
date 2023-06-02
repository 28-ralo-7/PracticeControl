using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views;
using PracticeControl.WebAPI.Views.ViewMobile;

namespace PracticeControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ProductionPracticeControlContext _db;
        public AuthController(IAuthService authService, ProductionPracticeControlContext db)
        {
             _authService = authService;
            _db = db;

        }

        [HttpPost]
        [Route("authorization")]
        public async Task<IActionResult> LoginDesktop([FromBody] Views.ViewMobile.AuthRequest parameters)
        {
            AuthResponseDesktop response = await _authService.Authorize(parameters.Login, parameters.PasswordString);

            if (response is not null)
            {
                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("authorizationMobile")]
        public async Task<IActionResult> LoginMobile([FromBody] Views.ViewMobile.AuthRequest parameters)
        {
            AuthResponseMobile response = await _authService.Authorize(parameters);

            if (response is not null)
            {
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}
