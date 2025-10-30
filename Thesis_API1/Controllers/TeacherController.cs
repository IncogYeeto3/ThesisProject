using Microsoft.AspNetCore.Mvc;
using Thesis_API1.Repositories;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : Controller
{
    private readonly ITeacherRepository _teacherRepo;

    public TeacherController(ITeacherRepository teacherRepo)
    {
        _teacherRepo = teacherRepo;
    }

    [HttpGet("{teacherNumber}/students")]
    public async Task<IActionResult> GetStudentsByTeacher(int teacherNumber, [FromQuery] string? columnName = null, [FromQuery] string? value = null)
        => Ok(await _teacherRepo.GetStudentsByTeacherAsync(teacherNumber, columnName, value));

    [HttpGet("{teacherNumber}/attendance")]
    public async Task<IActionResult> GetAttendanceByTeacher(int teacherNumber, [FromQuery] string? columnName = null, [FromQuery] string? value = null)
        => Ok(await _teacherRepo.GetAttendanceByTeacherAsync(teacherNumber, columnName, value));

    [HttpGet("{teacherNumber}/attendance-range")]
    public async Task<IActionResult> GetAttendanceByTeacherRange(int teacherNumber, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? subjectId = null)
        => Ok(await _teacherRepo.GetAttendanceByTeacherRangeAsync(teacherNumber, startDate, endDate, subjectId));

    [HttpGet("{teacherNumber}/subjects")]
    public async Task<IActionResult> GetSubjectsByTeacher(int teacherNumber)
        => Ok(await _teacherRepo.GetSubjectsByTeacherAsync(teacherNumber));
}
