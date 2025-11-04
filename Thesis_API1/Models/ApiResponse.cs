namespace Thesis_API1.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }       // true for success, false for error
        public int LogID { get; set; }             // the payload, e.g., int logId, or whatever type
        public string ErrorMessage { get; set; } // any error message
    }
}
