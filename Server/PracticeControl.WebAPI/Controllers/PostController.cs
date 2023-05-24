using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.blanks;
using PracticeControl.WebAPI.Views.blanksCreate;

namespace PracticeControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("createEmployee")]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeView? employeeView)
        {
            var response = _postService.CreateEmployee(employeeView);

            return Ok(response);
        }

        [HttpPost("createGroup")]
        public IActionResult CreateGroup([FromBody] CreateGroupView? groupView)
        {
            var response = _postService.CreateGroup(groupView);

            return Ok(response);
        }

        [HttpPost("createStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudentView? student)
        {
            var response = _postService.CreateStudent(student);

            return Ok(response is not null ? true : false);
        }
    }
}
