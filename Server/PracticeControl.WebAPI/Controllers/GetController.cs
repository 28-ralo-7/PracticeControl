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
    public class GetController: ControllerBase
    {
        private IGetService _getService;

        public GetController(IGetService getService)
        {
            _getService = getService;
        }

        [HttpGet("getGroups")]
        public IActionResult GetAttendanceView()
        {
            return Ok(_getService.GetGroupViews());
        }

        [HttpGet("getEmployee")]
        public IActionResult GetEmployee([FromForm]int id)
        {
            var employee = _getService.GetEmployee(id);
            return Ok(employee);
        }

        [HttpGet("getEmployeeList")]
        public IActionResult GetEmployeeList()
        {
            List<EmployeeView> employeeList = _getService.GetEmployeeViewList();

            return Ok(employeeList);
        }

        [HttpGet("getPracticeScheduleList")]
        public IActionResult GetPracticeScheduleList()
        {
            var practiceList = _getService.GetPracticeScheduleViewList();

            return Ok(practiceList);
        }
        [HttpGet("getAttendance")]
        public IActionResult GetAttendanceViews()
        {

            return null;
        }
    }
}
