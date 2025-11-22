using Dapper;
using System.Data;
using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbConnection _db;

        public StudentRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<(int TotalCount, List<Student> Records)> GetStudentsAsync(StudentFilterRequest filter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IsAdmin", filter.IsAdmin);
            parameters.Add("@TeacherNumber", filter.TeacherNumber);
            parameters.Add("@StudentNumber", filter.StudentNumber);
            parameters.Add("@StudentName", filter.StudentName);
            parameters.Add("@GradeLevel", filter.GradeLevel);
            parameters.Add("@Page", filter.Page);
            parameters.Add("@PageSize", filter.PageSize);

            // Execute stored procedure and get multiple results (first result = total count, second = paged rows)
            using var multi = await _db.QueryMultipleAsync(
                "sp_GetStudents_Universal",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int totalCount = multi.ReadFirst<int>();
            var records = multi.Read<Student>().ToList();

            return (totalCount, records);
        }
    }
}
