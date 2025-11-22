using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface IStudentRepository
    {
        Task<(int TotalCount, List<Student> Records)> GetStudentsAsync(StudentFilterRequest filter);
    }

}
