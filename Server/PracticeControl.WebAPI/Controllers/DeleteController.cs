using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Interfaces.IServices;

namespace PracticeControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly IDeleteService _deleteService;
        public DeleteController(IDeleteService deleteService)
        {
            _deleteService = deleteService;
        }

        [HttpDelete("deleteEmployee/{login}")]
        public IActionResult DeleteEmployee(string login)
        {
            var response = _deleteService.DeleteEmployee(login);

            return Ok(response.Result);
        }
    }
}
