using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class AttendanceFilterRequest
    {
        public bool IsAdmin { get; set; } = true;
        public string TeacherNumber { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string PCNumber { get; set; }
        public string RoomNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;

    }

}
