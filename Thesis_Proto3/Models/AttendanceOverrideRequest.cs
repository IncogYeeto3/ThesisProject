using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class AttendanceOverrideRequest
    {
        public string StudentNumber { get; set; }
        public DateTime OverrideDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ApprovedBy { get; set; }
    }
}
