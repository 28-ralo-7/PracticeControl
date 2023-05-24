using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Services;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private IGetService _getService;

        public GetController(IGetService getService)
        {
            _getService = getService;
        }

        [HttpGet("getGroups")]
        public async Task<IActionResult> GetAttendanceView()
        {
            var attendances = await _getService.GetGroupViews();
            return Ok(attendances);
        }

        [HttpGet("getEmployee")]
        public async Task<IActionResult> GetEmployee([FromForm] int id)
        {
            var employee = await _getService.GetEmployee(id);
            return Ok(employee);
        }

        [HttpGet("getEmployeeList")]
        public async Task<IActionResult> GetEmployeeList()
        {
            List<EmployeeView> employeeList = await _getService.GetEmployeeViewList();

            return Ok(employeeList);
        }

        [HttpGet("getPracticeScheduleList")]
        public async Task<IActionResult> GetPracticeScheduleList()
        {
            var practiceList = await _getService.GetPracticeScheduleViewList();

            return Ok(practiceList);
        }
        [HttpGet("getAttendance")]
        public IActionResult GetAttendanceViews()
        {
            return null;
        }

        [HttpGet("getStudentGroup/{groupName}")]
        public async Task<IActionResult> GetStudentsGroup([FromRoute] string groupName)
        {
            var response = await _getService.GetStudentGroup(groupName);

            return Ok(response);
        }

        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await _getService.GetStudents();

            return Ok(response);
        }
    }
}
