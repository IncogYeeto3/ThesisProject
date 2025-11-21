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

    public async Task<int?> GetStudentIdByNumberAsync(string studentNumber)
    {
        return await _db.QuerySingleOrDefaultAsync<int?>(
            "sp_GetStudentIdByNumber",
            new { studentNumber },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int?> GetLatestEnrollmentIdAsync(int studentId)
    {
        return await _db.QuerySingleOrDefaultAsync<int?>(
            "sp_GetLatestEnrollmentId",
            new { studentId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int?> GetValidScheduleAsync(int enrollmentId, int studentId, DateTime today, TimeSpan now, string todayCode)
    {
        return await _db.QuerySingleOrDefaultAsync<int?>(
            "sp_GetValidScheduleForStudent",
            new { enrollmentId, studentId, today, now, todayCode },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> LogStudentAttendanceAsync(int studentId, int scheduleId, string pcNumber, string roomNumber, DateTime logDate, TimeSpan logOnTime)
    {
        return await _db.ExecuteScalarAsync<int>(
            "sp_LogStudentAttendance",
            new { studentId, scheduleId, pcNumber, roomNumber, logDate, logOnTime },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<(int TotalCount, List<AttendanceRecord> Records)> GetAttendanceAsync(AttendanceFilterRequest filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IsAdmin", filter.IsAdmin);
        parameters.Add("@TeacherNumber", filter.TeacherNumber);
        parameters.Add("@StudentNumber", filter.StudentNumber);
        parameters.Add("@StudentName", filter.StudentName);
        parameters.Add("@SubjectCode", filter.SubjectCode);
        parameters.Add("@SubjectName", filter.SubjectName);
        parameters.Add("@PCNumber", filter.PCNumber);
        parameters.Add("@RoomNumber", filter.RoomNumber);
        parameters.Add("@StartDate", filter.StartDate);
        parameters.Add("@EndDate", filter.EndDate);
        parameters.Add("@Page", filter.Page);
        parameters.Add("@PageSize", filter.PageSize);

        // Execute stored procedure and get multiple results
        using var multi = await _db.QueryMultipleAsync(
            "sp_GetAttendance_Universal",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure
        );

        int totalCount = multi.ReadFirst<int>();
        var records = multi.Read<AttendanceRecord>().ToList();

        return (totalCount, records);
    }
}
