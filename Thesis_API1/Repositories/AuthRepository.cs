using Dapper;
using System.Data;
using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _db;

        public AuthRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<LoginResponse?> LoginAsync(string userNumber)
        {
            return await _db.QueryFirstOrDefaultAsync<LoginResponse>(
                "sp_LoginUser",
                new { userNumber },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
