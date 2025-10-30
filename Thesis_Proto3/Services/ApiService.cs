using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thesis_Proto3.Models;

namespace Thesis_Proto3.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:55042/"); // adjust port if needed
        }

        // ----------- Login Methods -----------
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }

            return null;
        }

        // ----------- Student Methods -----------
        public async Task<List<Subject>> GetStudentSubjectsAsync(int studentNumber)
        {
            var response = await _httpClient.GetAsync($"api/student/{studentNumber}/subjects");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Subject>>();
            }

            return new List<Subject>();
        }

        public async Task<int> RecordAttendanceAsync(AttendanceRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/attendance/login", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }

            // Extract error from API (BadRequest)
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new ApiException(error?.Error ?? "Unknown error from server.");

        }
        public async Task<bool> UpdateLogOffTimeAsync(int logId)
        {
            var response = await _httpClient.PutAsync($"api/attendance/{logId}/logoff", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> GetStudentIdByNumberAsync(int studentNumber)
        {
            var response = await _httpClient.GetAsync($"api/student/{studentNumber}/id");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        public async Task<int> GetSubjectIdByCodeAsync(string subjectCode)
        {
            var response = await _httpClient.GetAsync($"api/subject/{subjectCode}/id");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        // ----------- Teacher Methods -----------

        public async Task<List<Student>> GetStudentsByTeacherAsync(
            int teacherNumber,
            string columnName = null,
            string value = null)
        {
            string url = $"api/teacher/{teacherNumber}/students";

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(value))
            {
                url += $"?columnName={columnName}&value={value}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Student>>();
            }

            return new List<Student>();
        }

        public async Task<List<Subject>> GetSubjectsByTeacherAsync(int teacherNumber)
        {
            
                string url = $"api/teacher/{teacherNumber}/subjects";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<List<Subject>>(json,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    throw new Exception($"Error fetching subjects: {response.StatusCode}");
                }
            
        }

        public async Task<List<AttendanceResponse>> GetAttendanceByTeacherAsync(
            int teacherNumber,
            string columnName = null,
            string value = null)
        {
            string url = $"api/teacher/{teacherNumber}/attendance";

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(value))
            {
                url += $"?columnName={columnName}&value={value}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<AttendanceResponse>>();
            }

            return new List<AttendanceResponse>();
        }

        public async Task<List<AttendanceResponse>> GetAttendanceByTeacherRange(
            int teacherNumber, DateTime startDate, DateTime endDate, int? subjectId = null)
        {
            string url = $"api/teacher/{teacherNumber}/attendance-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            if (subjectId.HasValue)
                url += $"&subjectId={subjectId.Value}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<AttendanceResponse>>();
            }

            throw new Exception($"API call failed: {response.StatusCode}");
        }

        // ----------- Admin Methods -----------

        public async Task<List<Student>> GetStudentsByAdminAsync(
            string columnName = null,
            string value = null)
        {
            string url = "api/admin/students";

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(value))
            {
                url += $"?columnName={columnName}&value={value}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Student>>();
            }

            return new List<Student>();
        }

        public async Task<List<AttendanceResponse>> GetAttendanceByAdminAsync(
            string columnName = null,
            string value = null)
        {
            string url = "api/admin/attendance";

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(value))
            {
                url += $"?columnName={columnName}&value={value}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<AttendanceResponse>>();
            }

            return new List<AttendanceResponse>();
        }

        public async Task<List<AttendanceResponse>> GetAttendanceByAdminRange(
            DateTime startDate, DateTime endDate, int? subjectId = null)
        {
            
                string url = $"api/admin/attendance-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

                if (subjectId.HasValue)
                    url += $"&subjectId={subjectId.Value}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("API call failed: " + response.ReasonPhrase);

                return await response.Content.ReadFromJsonAsync<List<AttendanceResponse>>();
            
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            var response = await _httpClient.GetAsync("api/admin/subjects");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Subject>>();
            }
            return new List<Subject>();
        }

        // ----------- Sit-In Methods -----------

        public async Task<int> AddAttendanceOverrideAsync(AttendanceOverrideRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/attendance/override", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AttendanceOverrideResponse>();
            return result.OverrideID;
        }


        // ----------- Api Error Stuff -----------
        public class ApiError
        {
            public string Error { get; set; }
        }

        public class ApiException : Exception
        {
            public ApiException(string message) : base(message) { }
        }

    }
}
