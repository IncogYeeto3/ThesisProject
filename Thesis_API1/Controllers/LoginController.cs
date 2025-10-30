using Dapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Thesis_API1.Models;
using Thesis_API1.Repositories;
using LoginRequest = Thesis_API1.Models.LoginRequest;

namespace Thesis_API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public LoginController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _authRepository.LoginAsync(Convert.ToString(request.Username));

            if (user == null)
                return Unauthorized("Invalid user number");

            return Ok(user);
        }
    }
}
