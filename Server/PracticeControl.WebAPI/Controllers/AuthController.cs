using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views;

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
        public async Task<IActionResult> Login([FromBody] AuthRequest parameters)
        {
            AuthResponse response = await _authService.Authenticate(parameters.Login, parameters.PasswordString);

            if (response is not null)
            {
                return Ok(response);
            }

            return Unauthorized();
        }

    }
}
