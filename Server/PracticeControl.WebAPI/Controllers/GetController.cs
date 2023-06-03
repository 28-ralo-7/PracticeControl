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

        [HttpGet("getPracticeInfo/{groupName}")]
        public async Task<IActionResult> GetPracticeInfo([FromRoute] string groupName)
        {
            var practice = _getService.GetPracticeScheduleViewList().Result.FirstOrDefault(b=>b.Group.GroupName == groupName && Convert.ToDateTime(b.StartDate).Date <= DateTime.Now.Date && DateTime.Now.Date <= Convert.ToDateTime(b.EndDate));

            if (practice is null)
            {
                return null;
            }

            var practiceInfo = new CurrentPracticeInfoView
            {
                PracticeName = practice.Abbreviation + " " + practice.PracticeModule,
                DateStart = practice.StartDate,
                DateEnd = practice.EndDate,
            };

            return Ok(practiceInfo);
        }

        [HttpGet("getGroups")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _getService.GetGroupViews();
            return Ok(groups);
        }        
        
        [HttpGet("getGroupForName/{name}")]
        public async Task<IActionResult> GetGroupForName(string name)
        {
            var attendances = await _getService.GetGroupForName(name);
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


        [HttpGet("getStudentGroup/{groupName}")]
        public async Task<IActionResult> GetStudentsGroup([FromRoute] string groupName)
        {
            var response = await _getService.GetStudentGroup(groupName);

            return Ok(response);
        }


        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _getService.GetStudents();

            return Ok(response);
        }


        [HttpGet("getPracticeList")]
        public async Task<IActionResult> GetPracticeList()
        {
            var response = await _getService.GetPracticeList();

            return Ok(response);
        }


    }
}
