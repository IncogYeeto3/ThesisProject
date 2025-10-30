using Dapper;
using System.Data;
using Thesis_API1.Models;
using Thesis_API1.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly IDbConnection _db;

    public TeacherRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Student>> GetStudentsByTeacherAsync(int teacherNumber, string? columnName = null, string? value = null)
    {
        return await _db.QueryAsync<Student>(
            "sp_GetStudentsByTeacher",
            new { TeacherNumber = teacherNumber, ColumnName = columnName, Value = value },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAttendanceByTeacherAsync(int teacherNumber, string? columnName = null, string? value = null)
    {
        return await _db.QueryAsync<AttendanceResponse>(
            "sp_GetAttendanceByTeacher",
            new { TeacherNumber = teacherNumber, ColumnName = columnName, Value = value },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAttendanceByTeacherRangeAsync(int teacherNumber, DateTime startDate, DateTime endDate, int? subjectId = null)
    {
        return await _db.QueryAsync<AttendanceResponse>(
            "sp_GetAttendanceByTeacherRange",
            new { TeacherNumber = teacherNumber, StartDate = startDate, EndDate = endDate, SubjectID = subjectId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherNumber)
    {
        return await _db.QueryAsync<Subject>(
            "sp_GetSubjectsByTeacher",
            new { TeacherNumber = teacherNumber },
            commandType: CommandType.StoredProcedure
        );
    }
}
