using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", request);

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

        public async Task<ApiResponse> RecordAttendanceAsync(AttendanceRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Attendance/login", request);

            // Read raw JSON
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize using System.Text.Json
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // matches JSON like "Success" to C# property "Success"
            });

            return apiResponse;
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


        // ----------- GetStudent Methods -----------

        public async Task<StudentPagedResponse> GetStudentsAsync(StudentFilterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/student/search", request);

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await response.Content.ReadFromJsonAsync<StudentPagedResponse>(options);
        }

        // ----------- Attendance Methods -----------

        public async Task<AttendancePagedResponse> GetAttendanceUniversalAsync(AttendanceFilterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/attendance/search", request);

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await response.Content.ReadFromJsonAsync<AttendancePagedResponse>(options);
        }

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
