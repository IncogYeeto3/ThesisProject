using Microsoft.AspNetCore.Mvc;
using Thesis_API1.Repositories;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{
    private readonly IAdminRepository _adminRepo;

    public AdminController(IAdminRepository adminRepo)
    {
        _adminRepo = adminRepo;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents([FromQuery] string? columnName = null, [FromQuery] string? value = null)
        => Ok(await _adminRepo.GetStudentsByAdminAsync(columnName, value));

    [HttpGet("attendance")]
    public async Task<IActionResult> GetAttendance([FromQuery] string? columnName = null, [FromQuery] string? value = null)
        => Ok(await _adminRepo.GetAttendanceByAdminAsync(columnName, value));

    [HttpGet("attendance-range")]
    public async Task<IActionResult> GetAttendanceRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? subjectId = null)
        => Ok(await _adminRepo.GetAttendanceByAdminRangeAsync(startDate, endDate, subjectId));

    [HttpGet("subjects")]
    public async Task<IActionResult> GetAllSubjects()
        => Ok(await _adminRepo.GetAllSubjectsAsync());
}
