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

            // Auto-fill PCNumber with system name
            //string pcName = Environment.MachineName;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new AttendanceOverrideRequest
                {
                    StudentNumber = txtStudentNumber.Text,
                    OverrideDate = dtpOverrideDate.Value.Date,
                    StartTime = dtpStartTime.Value.TimeOfDay,
                    EndTime = dtpEndTime.Value.TimeOfDay,
                    ApprovedBy = _loggedInUser.Number
                };

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
