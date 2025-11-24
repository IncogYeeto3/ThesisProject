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
using System.Reflection;
using System.Drawing.Drawing2D;

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
            this.DoubleBuffered = true;
            _loggedInUser = loggedInUser;
            api = new ApiService();

            // Make btnLogin have rounded corners
            GraphicsPath path = new GraphicsPath();
            int radius = 15; // corner roundness


            path.AddArc(0, 0, radius, radius, 180, 90); // top-left
            path.AddArc(btnSubmit.Width - radius, 0, radius, radius, 270, 90); // top-right
            path.AddArc(btnSubmit.Width - radius, btnSubmit.Height - radius, radius, radius, 0, 90); // bottom-right
            path.AddArc(0, btnSubmit.Height - radius, radius, radius, 90, 90); // bottom-left
            path.CloseAllFigures();

            path.AddArc(0, 0, radius, radius, 180, 90); // top-left
            path.AddArc(btnClose.Width - radius, 0, radius, radius, 270, 90); // top-right
            path.AddArc(btnClose.Width - radius, btnClose.Height - radius, radius, radius, 0, 90); // bottom-right
            path.AddArc(0, btnClose.Height - radius, radius, radius, 90, 90); // bottom-left
            path.CloseAllFigures();

            btnSubmit.Region = new Region(path);
            btnClose.Region = new Region(path);

            SetPlaceholder(txtStudentNumber, "Student Number");
            SetRandomBackgroundImage();

            // Auto-fill PCNumber with system name
            //string pcName = Environment.MachineName;
        }

        private void SetRandomBackgroundImage()
        {
            string[] imageNames = { "Bg1", "Bg2", "Bg3", "Bg4", "Bg5", "Bg6" };
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string selectedName = imageNames[rand.Next(imageNames.Length)];

            // Use reflection to get the image from resources
            var image = (Image)Properties.Resources.ResourceManager.GetObject(selectedName);

            if (image != null)
            {
                // Create a new bitmap with the form's size to avoid lag from large images
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
