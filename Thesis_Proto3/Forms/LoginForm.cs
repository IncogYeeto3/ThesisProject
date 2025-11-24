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
using System.Reflection;



namespace Thesis_Proto3
{
    public partial class LoginForm : Form
    {
        private bool isSimulationMode = false;

        public LoginForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            // Make btnLogin have rounded corners
            GraphicsPath path = new GraphicsPath();
            int radius = 15; // corner roundness


            path.AddArc(0, 0, radius, radius, 180, 90); // top-left
            path.AddArc(btnLogin.Width - radius, 0, radius, radius, 270, 90); // top-right
            path.AddArc(btnLogin.Width - radius, btnLogin.Height - radius, radius, radius, 0, 90); // bottom-right
            path.AddArc(0, btnLogin.Height - radius, radius, radius, 90, 90); // bottom-left
            path.CloseAllFigures();

            btnLogin.Region = new Region(path);

            SetRandomBackgroundImage();

        }

        private void SetRandomBackgroundImage()
        {
            string[] imageNames = { "Bg1", "Bg2", "Bg3", "Bg4", "Bg5", "Bg6" };
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string selectedName = imageNames[rand.Next(imageNames.Length)];

            var image = (Image)Properties.Resources.ResourceManager.GetObject(selectedName);

            if (image != null)
            {
                Bitmap stretched = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(stretched))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, stretched.Width, stretched.Height));
                }
                this.BackgroundImage = stretched;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
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
                        RoomNumber = "COLLEGELAB"     // TODO: replace with real room number
                    };

                    // If simulation mode is active, override date/time
                    if (isSimulationMode)
                    {
                        attendanceRequest.OverrideDate = new DateTime(2025, 10, 14);  // Pretend it's Oct 18, 2025
                        attendanceRequest.OverrideTime = new TimeSpan(13, 0, 0);      // Pretend it's 10:00 AM
                    }

                    // Make the API call and expect a structured response
                    ApiResponse response = await api.RecordAttendanceAsync(attendanceRequest);

                    // Check if the API returned success
                    if (!response.Success)
                    {
                        MessageBox.Show(response.ErrorMessage ?? "No valid Schedule/Override Found, please contact MIS",
                            "Attendance Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int logId = response.LogID;

                    MessageBox.Show($"Attendance recorded! LogID = {logId}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open the student form with the logId
                    StudentForm frm = new StudentForm(user, logId);
                    frm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors (network, deserialization, etc.)
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
            txt.Font = new Font(txt.Font.FontFamily, placeholderSize, FontStyle.Regular);
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
                txt.Font = new Font(txt.Font.FontFamily, 12, FontStyle.Italic);
                txt.Text = placeholder;
                txt.UseSystemPasswordChar = false;
            }
        }

        private void BtnToggleTime_Click(object sender, EventArgs e)
        {
            isSimulationMode = !isSimulationMode;

            if (isSimulationMode)
            {
                BtnToggleTime.Text = "Assuming CompLab Time";
                // Enable date/time controls, or lock them to Oct 18
            }
            else
            {
                BtnToggleTime.Text = "Using Real Time";
                // Hide or disable date/time inputs
            }
        }

    }
}
