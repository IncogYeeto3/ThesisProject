using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using Thesis_Proto3.Models;
using Thesis_Proto3.Services;
using Thesis_Proto3.Forms;
using System.Drawing.Drawing2D;
using static Thesis_Proto3.Services.ApiService;
using System.Security.Cryptography;


namespace Thesis_Proto3
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Make btnLogin have rounded corners
            GraphicsPath path = new GraphicsPath();
            int radius = 15; // corner roundness

            path.AddArc(0, 0, radius, radius, 180, 90); // top-left
            path.AddArc(btnLogin.Width - radius, 0, radius, radius, 270, 90); // top-right
            path.AddArc(btnLogin.Width - radius, btnLogin.Height - radius, radius, radius, 0, 90); // bottom-right
            path.AddArc(0, btnLogin.Height - radius, radius, radius, 90, 90); // bottom-left
            path.CloseAllFigures();

            btnLogin.Region = new Region(path);

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            UIService.Enable(this);

            SetPlaceholder(txtUserNumber, "Username", false);
            SetPlaceholder(txtPassword, "Password", true);

            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;

            txtUserNumber.Font = new Font("Segoe UI", 12, FontStyle.Bold);

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserNumber.Text;
            string password = txtPassword.Text;
            ApiService api = new ApiService();

            // Step 1: Login
            var user = await api.LoginAsync(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid login.");
                return;
            }

            // Step 2: Student → Record attendance directly
            if (user.Role == "Student") // Student
            {
                try
                {
                    var attendanceRequest = new AttendanceRequest
                    {
                        StudentNumber = user.Number,
                        PCNumber = "COLLEGELAB-13",   // TODO: replace with real PC number
                        RoomNumber = "COLLEGELAB"  // TODO: replace with real room number
                    };

                    int logId = await api.RecordAttendanceAsync(attendanceRequest);

                    if (logId <= 0)
                    {
                        MessageBox.Show("No valid Schedule/Override Found, please contact MIS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show($"Attendance recorded! LogID = {logId}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    StudentForm frm = new StudentForm(user, logId);
                    frm.Show();
                    this.Hide();
                }
                catch (ApiException ex)
                {
                    // API returned a bad request (SQL RAISERROR bubbled up)
                    MessageBox.Show(ex.Message, "Attendance Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Some unexpected error (network, serialization, etc.)
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (user.Role == "Employee") // Teacher
            {
                MessageBox.Show($"Welcome Teacher-{user.Number}!");
                TeacherForm frm = new TeacherForm(user);
                frm.Show();
                this.Hide();
            }
            else if (user.Role == "MIS") // Admin
            {
                MessageBox.Show($"Welcome Admin-{user.Username},{user.Number}!");
                AdminForm frm = new AdminForm(user);
                frm.Show();
                this.Hide();
            }
        }

        public void ClearFields()
        {
            txtUserNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {
            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;
            panelLogin.Anchor = AnchorStyles.None;
        }

        private void SetPlaceholder(TextBox txt, string placeholder, bool isPassword = false)
        {
            float normalSize = txt.Font.Size;
            float placeholderSize = normalSize - 6;

            txt.Tag = placeholder; // store placeholder text
            txt.ForeColor = Color.Gray;
            txt.Font = new Font(txt.Font.FontFamily, placeholderSize, FontStyle.Italic);
            txt.Text = placeholder;
            txt.UseSystemPasswordChar = false;

            txt.GotFocus += Txt_GotFocus;
            txt.LostFocus += Txt_LostFocus;
            // you can add a dedicated TextChanged handler too if you need
        }


        private void Txt_GotFocus(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            string placeholder = txt.Tag?.ToString();

            if (txt.Text == placeholder)
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
                txt.Font = new Font(txt.Font.FontFamily, 12, FontStyle.Regular);
            }
        }

        private void Txt_LostFocus(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            string placeholder = txt.Tag?.ToString();

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.ForeColor = Color.Gray;
                txt.Font = new Font(txt.Font.FontFamily, 10, FontStyle.Italic);
                txt.Text = placeholder;
                txt.UseSystemPasswordChar = false;
            }
        }

    }
}
