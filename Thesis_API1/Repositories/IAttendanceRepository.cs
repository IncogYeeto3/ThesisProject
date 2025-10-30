using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface IAttendanceRepository
    {
        Task<int> ValidateStudentAttendanceAsync(AttendanceRequest request);
        Task UpdateLogOffTimeAsync(int logId);
        Task<int> AddAttendanceOverrideAsync(AttendanceOverrideRequest request);
        
    }
}
