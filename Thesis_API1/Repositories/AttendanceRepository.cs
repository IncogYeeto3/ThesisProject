using Dapper;
using System.Data;
using Thesis_API1.Models;
using Thesis_API1.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly IDbConnection _db;

    public AttendanceRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<int> ValidateStudentAttendanceAsync(AttendanceRequest request)
    {
        return await _db.ExecuteScalarAsync<int>(
            "sp_ValidateStudentAttendance",
            new
            {
                studentNumber = request.StudentNumber,
                pcNumber = request.PCNumber,
                roomNumber = request.RoomNumber
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateLogOffTimeAsync(int logId)
    {
        await _db.ExecuteAsync(
            "sp_UpdateLogOffTime",
            new { LogID = logId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> AddAttendanceOverrideAsync(AttendanceOverrideRequest request)
    {
        return await _db.ExecuteScalarAsync<int>(
            "sp_AddAttendanceOverride",
            new
            {
                studentNumber = request.StudentNumber,
                overrideDate = request.OverrideDate,
                startTime = request.StartTime,
                endTime = request.EndTime,
                approvedBy = request.ApprovedBy
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
