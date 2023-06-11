using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Services;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;

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
        
        [HttpPost("createEmployee")]//Готово
        public IActionResult CreateEmployee([FromBody] CreateEmployeeView? employeeView)
        {
            var response = _postService.CreateEmployee(employeeView);

            return Ok(response);
        }

        [HttpPost("createGroup")]//Готово
        public IActionResult CreateGroup([FromBody] CreateGroupView? groupView)
        {
            var response = _postService.CreateGroup(groupView);

            return Ok(response);
        }

        
        [HttpPost("createStudent")]//Готово
        public IActionResult CreateStudent([FromBody] CreateStudentView? student)
        {
            var response = _postService.CreateStudent(student);

            return Ok(response is not null ? true : false);
        }

        
        [HttpPost("createPracticeSchedule")]//Готово
        public IActionResult CreatePracticeSchedule([FromBody] CreatePracticeScheduleView schedule)
        {
            var response = _postService.CreatePracticeSchedule(schedule);

            return Ok(response);
        }

        
        [HttpPost("createPractice")]//Готово
        public IActionResult CreatePractice([FromBody] CreatePracticeView schedule)
        {
            var response = _postService.CreatePractice(schedule);

            return Ok(response);
        }

        #region Блок проверки уникальности новых записей
        
        [HttpPost("checkUniquePractice")]//Готово
        public async Task<IActionResult> CheckUniquePractice(PracticeView practiceView)
        {
            var response = await _postService.CheckUnique(practiceView);

            return Ok(response);
        }

        
        [HttpPost("checkUniquePracticeSchedule")]//Готово
        public async Task<IActionResult> CheckUniquePracticeSchedule(PracticeScheduleView practiceScheduleView)
        {
            var response = await _postService.CheckUnique(practiceScheduleView);

            return Ok(response);
        }

        
        [HttpPost("checkUniqueGroup")]//Готово
        public async Task<IActionResult> CheckUniqueGroup([FromBody]string groupView)
        {
            var response = await _postService.CheckUniqueGroup(groupView);

            return Ok(response);
        }

        
        [HttpPost("checkUniqueEmployee")]//Готово
        public async Task<IActionResult> CheckUniqueEmployee([FromBody]string login)
        {
            var response = await _postService.CheckUnique(login);

            return Ok(response);
        }

        
        [HttpPost("checkUniqueStudent")]//Готово
        public async Task<IActionResult> CheckUniqueStudent([FromBody]string login)
        {
            var response = await _postService.CheckUniqueStudent(login);

            return Ok(response);
        }

        
        [HttpPost("checkValidDateForPractice")]//Готово
        public IActionResult CheckValidDateForPractice([FromBody]CreatePracticeScheduleView createPracticeView)
        {
            var response = _postService.CheckValidDateForPractice(createPracticeView);

            return Ok(response);
        }
        #endregion


    }
}
