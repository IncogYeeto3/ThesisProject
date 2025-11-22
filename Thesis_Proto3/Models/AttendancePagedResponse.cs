using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis_Proto3.Models;

namespace Thesis_Proto3.Models
{
    public class AttendancePagedResponse
    {
        public int TotalCount { get; set; }
        public List<AttendanceRecord> Records { get; set; }
    }

}
