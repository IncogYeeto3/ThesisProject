using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis_Proto3.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public int LogID { get; set; }
        public string ErrorMessage { get; set; }
    }
}
