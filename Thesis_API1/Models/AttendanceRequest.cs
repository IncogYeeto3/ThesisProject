namespace Thesis_API1.Models
{
    public class AttendanceRequest
    {
        public string StudentNumber { get; set; }
        public string PCNumber { get; set; }
        public string RoomNumber { get; set; }


        // Optional override fields
        public DateTime? OverrideDate { get; set; }
        public TimeSpan? OverrideTime { get; set; }
    }
}
