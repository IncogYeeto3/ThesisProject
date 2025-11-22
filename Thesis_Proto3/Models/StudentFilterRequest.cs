using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class StudentFilterRequest
    {
        public bool IsAdmin { get; set; }
        public string TeacherNumber { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public int? GradeLevel { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
