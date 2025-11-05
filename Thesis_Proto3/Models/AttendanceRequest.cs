using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    class AttendanceRequest
    {
        public string StudentNumber { get; set; }
        public string PCNumber { get; set; }
        public string RoomNumber { get; set; }

        // Optional override fields
        public DateTime? OverrideDate { get; set; }
        public TimeSpan? OverrideTime { get; set; }
    }
}
