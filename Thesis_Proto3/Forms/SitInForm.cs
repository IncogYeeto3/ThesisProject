using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thesis_Proto3.Models;
using Thesis_Proto3.Services;

namespace Thesis_Proto3.Forms
{
    public partial class SitInForm: Form
    {
        private readonly LoginResponse _loggedInUser;
        private readonly ApiService api;
        public string pcNumber = "COLLEGELAB-13";
        public SitInForm(LoginResponse loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            api = new ApiService();

            SetPlaceholder(txtStudentNumber, "Student Number");
            SetPlaceholder(txtStartTime, "Start Time (HH:mm:ss)");
            SetPlaceholder(txtEndTime, "End Time (HH:mm:ss)");
            SetPlaceholder(txtOverrideDate, "Override Date (yyyy-MM-dd)");

            // Auto-fill PCNumber with system name
            //string pcName = Environment.MachineName;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string studentNumber = txtStudentNumber.Text;
                string startTime = txtStartTime.Text;
                string endTime = txtEndTime.Text;

                // 2. Get Override Date (full DateTime)
                if (!DateTime.TryParse(txtOverrideDate.Text, out DateTime overrideDate))
                {
                    MessageBox.Show("Invalid Override Date format. Please use yyyy-MM-dd or a valid date/time.");
                    return;
                }

                // ✅ Validate time fields
                if (!TimeSpan.TryParse(startTime, out TimeSpan start))
                {
                    MessageBox.Show("Invalid Start Time format. Please use HH:mm or HH:mm:ss (e.g., 09:00 or 09:00:00).");
                    return;
                }

                if (!TimeSpan.TryParse(endTime, out TimeSpan end))
                {
                    MessageBox.Show("Invalid End Time format. Please use HH:mm or HH:mm:ss (e.g., 17:30 or 17:30:00).");
                    return;
                }

                // Optional: check logical order
                if (end <= start)
                {
                    MessageBox.Show("End Time must be later than Start Time.");
                    return;
                }

                // 3. Build request (with preset date for testing)
                var request = new AttendanceOverrideRequest
                {
                    StudentNumber = studentNumber,
                    OverrideDate = overrideDate,
                    StartTime = start,
                    EndTime = end,
                    ApprovedBy = _loggedInUser.Number
                };

                // 5. Submit
                var overrideId = await api.AddAttendanceOverrideAsync(request);
                MessageBox.Show($"Override created successfully. OverrideID = {overrideId}");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void SitInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
