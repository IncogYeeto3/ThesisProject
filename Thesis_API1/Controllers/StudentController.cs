using Microsoft.AspNetCore.Mvc;
using Thesis_API1.Models;
using Thesis_API1.Repositories;

namespace Thesis_API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetStudents([FromBody] StudentFilterRequest filter)
        {
            var (totalCount, students) = await _repository.GetStudentsAsync(filter);

            return Ok(new
            {
                TotalCount = totalCount,
                Records = students
            });
        }
    }
}
