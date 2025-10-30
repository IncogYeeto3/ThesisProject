using Dapper;
using System.Data;
using Thesis_API1.Models;
using Thesis_API1.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IDbConnection _db;

    public AdminRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Student>> GetStudentsByAdminAsync(string? columnName = null, string? value = null)
    {
        return await _db.QueryAsync<Student>(
            "sp_GetStudentsByAdmin",
            new { ColumnName = columnName, Value = value },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAttendanceByAdminAsync(string? columnName = null, string? value = null)
    {
        return await _db.QueryAsync<AttendanceResponse>(
            "sp_GetAttendanceByAdmin",
            new { ColumnName = columnName, Value = value },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAttendanceByAdminRangeAsync(DateTime startDate, DateTime endDate, int? subjectId = null)
    {
        return await _db.QueryAsync<AttendanceResponse>(
            "sp_GetAttendanceByAdminRange",
            new { StartDate = startDate, EndDate = endDate, SubjectID = subjectId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        return await _db.QueryAsync<Subject>(
            "sp_GetSubjectsByAdmin",
            commandType: CommandType.StoredProcedure
        );
    }
}
