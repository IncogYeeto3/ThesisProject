using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    class AttendanceResponse
    {
        public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string PCNumber { get; set; }
        public string RoomNumber { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan LogOnTime { get; set; }
        public TimeSpan? LogOffTime { get; set; }
    }
}
