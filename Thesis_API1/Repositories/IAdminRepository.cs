using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Student>> GetStudentsByAdminAsync(string? columnName = null, string? value = null);
        Task<IEnumerable<AttendanceResponse>> GetAttendanceByAdminAsync(string? columnName = null, string? value = null);
        Task<IEnumerable<AttendanceResponse>> GetAttendanceByAdminRangeAsync(DateTime startDate, DateTime endDate, int? subjectId = null);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    }

}
