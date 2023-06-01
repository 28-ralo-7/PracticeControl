using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Interfaces.IRepositories;
using PracticeControl.WebAPI.Interfaces.IServices;
using PracticeControl.WebAPI.Views.View;
using static PracticeControl.WebAPI.Converters.EmployeeConverter;
using static PracticeControl.WebAPI.Converters.StudentConverter;
using static PracticeControl.WebAPI.Converters.GroupConverter;

namespace PracticeControl.WebAPI.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly IDeleteRepository _deleteRepository;
        public DeleteService(IDeleteRepository deleteRepository)
        {
            _deleteRepository= deleteRepository;
        }

        //Сотрудники
        public async Task<bool> DeleteEmployee(string login)
        {
            if (string.IsNullOrEmpty(login))
                return false;

            Employee? employee = await _deleteRepository.DeleteEmployee(login);

            if (employee is null)
                return false;


            return true;

        }

        //Студенты
        public async Task<StudentView> DeleteStudent(string login)
        {
            if (string.IsNullOrEmpty(login))
                return null;

            Student student = await _deleteRepository.DeleteStudent(login);

            if (student is null)
                return null;

            StudentView studentView = ConvertToView(student);

            return studentView;

        }

        //Группы
        public async Task<GroupView> DeleteGroup(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            Group group = await _deleteRepository.DeleteGroup(name);

            if (group is null)
                return null;

            GroupView groupView = ConvertToGroupView(group);

            return groupView;
        }

    }
}
