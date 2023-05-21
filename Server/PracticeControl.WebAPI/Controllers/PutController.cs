using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanksCreate;
using PracticeControl.WebAPI.Views.blanksUpdate;

namespace PracticeControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PutController : ControllerBase
    {
        private readonly IPutService _putService;
        public PutController(IPutService putService)
        {
            _putService = putService;
        }
        [HttpPut("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeView employee)
        {
            var response = _putService.UpdateEmployee(employee);

            return Ok(response.Result);
        }
    }
}
