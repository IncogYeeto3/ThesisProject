using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface IAttendanceRepository
    {
        Task UpdateLogOffTimeAsync(int logId);
        Task<int> AddAttendanceOverrideAsync(AttendanceOverrideRequest request);
        Task<int?> GetStudentIdByNumberAsync(string studentNumber);
        Task<int?> GetLatestEnrollmentIdAsync(int studentId);
        Task<int?> GetValidScheduleAsync(int enrollmentId, int studentId, DateTime today, TimeSpan now, string todayCode);
        Task<int> LogStudentAttendanceAsync(int studentId, int scheduleId, string pcNumber, string roomNumber, DateTime logDate, TimeSpan logOnTime);


    }
}
