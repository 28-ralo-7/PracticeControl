using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Services;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewMobile;

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

        [HttpGet("getPracticeInfo")]//Готово
        public async Task<IActionResult> GetPracticeInfo([FromQuery] string groupName, [FromQuery] int studentID )
        {
            var practiceInfo = _getService.GetCurrentPracticeInfo(groupName, studentID);

            if (practiceInfo is null)
            {
                return null;
            }

            return Ok(practiceInfo.Result);
        }

        [HttpGet("getGroups")]//Готово
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _getService.GetGroupViews();
            return Ok(groups);
        }        
        
        [HttpGet("getGroupForName/{name}")]//Готово
        public async Task<IActionResult> GetGroupForName(string name)
        {
            var attendances = await _getService.GetGroupForName(name);
            return Ok(attendances);
        }

        [HttpGet("getEmployee")]//Готово
        public async Task<IActionResult> GetEmployee([FromForm] int id)
        {
            var employee = await _getService.GetEmployee(id);
            return Ok(employee);
        }

        [HttpGet("getEmployeeList")]//Готово
        public async Task<IActionResult> GetEmployeeList()
        {
            List<EmployeeView> employeeList = await _getService.GetEmployeeViewList();

            return Ok(employeeList);
        }

        [HttpGet("getPracticeScheduleList")]//Готово
        public async Task<IActionResult> GetPracticeScheduleList()
        {
            var practiceList = await _getService.GetPracticeScheduleViewList();

            return Ok(practiceList);
        }

        [HttpGet("getStudentGroup/{groupName}")]//Готово
        public async Task<IActionResult> GetStudentsGroup([FromRoute] string groupName)
        {
            var response = await _getService.GetStudentGroup(groupName);

            return Ok(response);
        }

        [HttpGet("getAllStudents")]//Готово
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _getService.GetStudents();

            return Ok(response);
        }

        [HttpGet("getPracticeList")]//Готово
        public async Task<IActionResult> GetPracticeList()
        {
            var response = await _getService.GetPracticeList();

            return Ok(response);
        }


    }
}
