using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Student>> GetStudentsByTeacherAsync(int teacherNumber, string? columnName = null, string? value = null);
        Task<IEnumerable<AttendanceResponse>> GetAttendanceByTeacherAsync(int teacherNumber, string? columnName = null, string? value = null);
        Task<IEnumerable<AttendanceResponse>> GetAttendanceByTeacherRangeAsync(int teacherNumber, DateTime startDate, DateTime endDate, int? subjectId = null);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherNumber);
    }
}
