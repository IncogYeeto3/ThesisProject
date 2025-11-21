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
            var studentId = await _attendanceRepo.GetStudentIdByNumberAsync(request.StudentNumber);
            if (studentId == null)
                return Ok(new ApiResponse { Success = false, LogID = 0, ErrorMessage = "Invalid student number." });

            var enrollmentId = await _attendanceRepo.GetLatestEnrollmentIdAsync(studentId.Value);
            if (enrollmentId == null)
                return Ok(new ApiResponse { Success = false, LogID = 0, ErrorMessage = "No enrollment found for student." });

            // Use real or overridden date/time
            var today = request.OverrideDate ?? DateTime.Now.Date;
            var now = request.OverrideTime ?? DateTime.Now.TimeOfDay;
            var todayCode = GetTodayCode(today.DayOfWeek);

            // Always returns a real schedule ID (normal or override), or null
            var scheduleId = await _attendanceRepo.GetValidScheduleAsync(
                enrollmentId.Value,
                studentId.Value,
                today,
                now,
                todayCode
            );

            if (scheduleId == null)
                return Ok(new ApiResponse { Success = false, LogID = 0, ErrorMessage = "No valid schedule for this time." });

            // No more "0" logic — scheduleId is always valid here
            var logId = await _attendanceRepo.LogStudentAttendanceAsync(
                studentId.Value,
                scheduleId.Value,
                request.PCNumber,
                request.RoomNumber,
                today,
                now
            );

            return Ok(new ApiResponse { Success = true, LogID = logId });
        }
        catch (SqlException ex)
        {
            return Ok(new ApiResponse { Success = false, LogID = 0, ErrorMessage = ex.Message });
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

    [HttpPost("search")]
    public async Task<IActionResult> GetAttendance([FromBody] AttendanceFilterRequest filter)
    {
        var (totalCount, records) = await _attendanceRepo.GetAttendanceAsync(filter);
        return Ok(new
        {
            TotalCount = totalCount,
            Records = records
        });
    }


    private string GetTodayCode(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => "M",
            DayOfWeek.Tuesday => "T",
            DayOfWeek.Wednesday => "W",
            DayOfWeek.Thursday => "Th",
            DayOfWeek.Friday => "F",
            DayOfWeek.Saturday => "S",
            DayOfWeek.Sunday => "Su",
            _ => throw new ArgumentOutOfRangeException(nameof(day), "Invalid day of week")
        };
    }



}
