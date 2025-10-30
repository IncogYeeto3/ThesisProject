using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Thesis_API1.Models;
using Thesis_API1.Repositories;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : Controller
{
    private readonly IAttendanceRepository _attendanceRepo;

    public AttendanceController(IAttendanceRepository attendanceRepo)
    {
        _attendanceRepo = attendanceRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> ValidateStudentAttendance([FromBody] AttendanceRequest request)
    {
        try
        {
            var logId = await _attendanceRepo.ValidateStudentAttendanceAsync(request);
            return Ok(logId);
        }
        catch (SqlException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{logId}/logoff")]
    public async Task<IActionResult> UpdateLogOffTime(int logId)
    {
        await _attendanceRepo.UpdateLogOffTimeAsync(logId);
        return Ok("LogOffTime updated successfully.");
    }

    //need to add something here that forces the user to be an admin or teacher
    [HttpPost("override")]
    public async Task<IActionResult> AddAttendanceOverride([FromBody] AttendanceOverrideRequest request)
    {
        try
        {
            var overrideId = await _attendanceRepo.AddAttendanceOverrideAsync(request);
            return Ok(new { OverrideID = overrideId });
        }
        catch (SqlException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    
}
