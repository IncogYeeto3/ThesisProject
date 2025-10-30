using Thesis_API1.Models;

namespace Thesis_API1.Repositories
{
    public interface IAuthRepository
    {
        Task<LoginResponse?> LoginAsync(string username);
    }
}
