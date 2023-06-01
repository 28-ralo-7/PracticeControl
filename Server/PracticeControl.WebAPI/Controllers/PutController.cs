using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.ViewCreate;
using PracticeControl.WebAPI.Views.ViewUpdate;

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

        [HttpPut("updateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentView student)
        {
            var response = _putService.UpdateStudent(student);

            return Ok(response.Result);
        }

        [HttpPut("renameGroup/{oldName}")]
        public async Task<IActionResult> UpdateGroup([FromRoute]string oldName, [FromBody]string groupName)
        {
            var response = _putService.UpdateGroup(oldName, groupName);

            return Ok(response.Result);
        }

        [HttpPut("updateAttendances")]
        public async Task<IActionResult> UpdateAttendance([FromBody] List<UpdateAttendanceView> attendanceView)
        {
            var response = _putService.UpdateAttendance(attendanceView);

            return Ok(response.Result);
        }
    }
}
