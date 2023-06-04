using Microsoft.AspNetCore.Mvc;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.View;

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

        [HttpDelete("deleteEmployee/{login}")]//Готово
        public IActionResult DeleteEmployee(string login)
        {
            var response = _deleteService.DeleteEmployee(login);

            return Ok(response.Result);
        }

        [HttpDelete("deleteStudent/{login}")]//Готово
        public IActionResult DeleteStudent(string login)
        {
            var response = _deleteService.DeleteStudent(login);

            return Ok(response.Result);
        }

        [HttpDelete("deleteGroup/{name}")]//Готово
        public IActionResult DeleteGroup(string name)
        {
            var response = _deleteService.DeleteGroup(name);

            return Ok(response.Result);
        }

        [HttpDelete("deletePractice/{id}")]//Готово
        public IActionResult DeletePractice(int id)
        {
            var response = _deleteService.DeletePracice(id);

            return Ok(response.Result);
        }

        [HttpDelete("deletePracticeSchedule/{id}")]//Готово
        public IActionResult DeletePracticeSchedule(int id)
        {
            var response = _deleteService.DeletePracticeSchedule(id);

            return Ok(response.Result);
        }
    }
}
