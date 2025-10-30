namespace Thesis_API1.Models
{
    public class AttendanceOverrideRequest
    {
        public int StudentNumber { get; set; }
        public DateTime OverrideDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ApprovedBy { get; set; }
    }

}
