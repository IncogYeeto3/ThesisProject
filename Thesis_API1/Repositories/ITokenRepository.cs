using Microsoft.AspNetCore.Identity;

namespace Thesis_API1.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
