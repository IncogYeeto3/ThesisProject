using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Thesis_Proto3.Models;
using Thesis_Proto3.Services;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

namespace Thesis_Proto3
{
    public partial class StudentForm: Form
    {
        private readonly LoginResponse _loggedInUser;
        private readonly ApiService _api = new ApiService();
        private readonly int? _currentLogId;
        public StudentForm(LoginResponse loggedInUser, int logId)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _currentLogId = logId;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UIService.Disable(this);

            label4.Text = "Welcome, " + _loggedInUser.Username;
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (_currentLogId.HasValue)
            {
                try
                {
                    // force synchronous execution
                    _api.UpdateLogOffTimeAsync(_currentLogId.Value).GetAwaiter().GetResult();
                }
                catch
                {
                    // optional: log error somewhere, cannot show MessageBox during shutdown
                }
            }
        }

        private async void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currentLogId.HasValue)
            {
                try
                {
                    await _api.UpdateLogOffTimeAsync(_currentLogId.Value);

                    MessageBox.Show("LogOffTime recorded.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving LogOffTime: " + ex.Message);
                }
            }
        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                // Call API to update logoff time
                await _api.UpdateLogOffTimeAsync(_currentLogId.Value);

                // Hide student form
                this.Hide();

                // Show login form again
                //var loginForm = Application.OpenForms["LoginForm"] as LoginForm;
                //if (loginForm != null)
                //{
                //    loginForm.ClearFields();
                //    loginForm.Show();
                //}

                LoginForm loginForm = new LoginForm();
                loginForm.ClearFields();
                loginForm.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error logging out: " + ex.Message);
            }
        }

        private void SetComboBoxPlaceholder(ComboBox comboBox, string placeholder)
        {
            comboBox.ForeColor = Color.Gray;
            comboBox.Text = placeholder;

            comboBox.Enter += (s, e) =>
            {
                if (comboBox.Text == placeholder)
                {
                    comboBox.Text = "";
                    comboBox.ForeColor = Color.Black;
                }
            };

            comboBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    comboBox.Text = placeholder;
                    comboBox.ForeColor = Color.Gray;
                }
            };
        }
    }
}
