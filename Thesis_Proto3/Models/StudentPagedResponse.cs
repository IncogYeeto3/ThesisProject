using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class StudentPagedResponse
    {
        public int TotalCount { get; set; }
        public List<Student> Records { get; set; }
    }
}
