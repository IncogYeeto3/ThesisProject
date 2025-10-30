using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thesis_Proto3.Forms;
using Thesis_Proto3.Models;
using Thesis_Proto3.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Thesis_Proto3
{
    public partial class TeacherForm: Form
    {
        private readonly LoginResponse _loggedInUser;
        private readonly ApiService _api;
        private bool _isViewingStudents = true;
        private string _lastSortedColumn = null;
        private SortOrder _lastSortOrder = SortOrder.None;

        public TeacherForm(LoginResponse loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _api = new ApiService();

        }

        private async void TeacherForm_Load(object sender, EventArgs e)
        {
            btnViewStudent.PerformClick();

            label1.Text = "Welcome " + _loggedInUser.Role + " , " + _loggedInUser.Username;

            var subjects = await _api.GetSubjectsByTeacherAsync(Int32.Parse(_loggedInUser.Number));

            subjects.Insert(0, new Subject { SubjectID = 0, SubjectName = "" });

            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void btnSitIn_Click(object sender, EventArgs e)
        {
            using (var sitInForm = new SitInForm(_loggedInUser))
            {
                sitInForm.ShowDialog();
            }
        }

        private async void btnViewStudent_Click(object sender, EventArgs e)
        {
            var students = await _api.GetStudentsByTeacherAsync(Int32.Parse(_loggedInUser.Number));

            dgv.DataSource = ToDataTable(students); 
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _isViewingStudents = true;
        }

        private async void btnViewAttendance_Click(object sender, EventArgs e)
        {
            var attendance = await _api.GetAttendanceByTeacherAsync(Int32.Parse(_loggedInUser.Number));

            dgv.DataSource = ToDataTable(attendance);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _isViewingStudents = false;
        }

        private async void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dgv.Columns[e.ColumnIndex].Name;
            string value = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            if (string.IsNullOrWhiteSpace(value))
                return;

            MessageBox.Show($"Filtering by {columnName} = {value}");

            if (_isViewingStudents)
            {
                var students = await _api.GetStudentsByTeacherAsync(Int32.Parse(_loggedInUser.Number), columnName, value);
                dgv.DataSource = ToDataTable(students);
            }
            else
            {
                var attendance = await _api.GetAttendanceByTeacherAsync(Int32.Parse(_loggedInUser.Number), columnName, value);
                dgv.DataSource = ToDataTable(attendance);
            }
        }

        private async void btnDaily_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Today;

                int? subjectId = (int)cmbSubject.SelectedValue;
                if (subjectId == 0) subjectId = null;

                var attendance = await _api.GetAttendanceByTeacherRange(
                    Int32.Parse(_loggedInUser.Number), today, today, subjectId);

                dgv.DataSource = ToDataTable(attendance);
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading daily report: " + ex.Message);
            }

            _isViewingStudents = false;
        }

        private async void btnWeekly_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Sunday
                DateTime endOfWeek = startOfWeek.AddDays(6);

                int? subjectId = (int)cmbSubject.SelectedValue;
                if (subjectId == 0) subjectId = null;

                var attendance = await _api.GetAttendanceByTeacherRange(
                    Int32.Parse(_loggedInUser.Number), startOfWeek, endOfWeek, subjectId);

                dgv.DataSource = ToDataTable(attendance);
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading weekly report: " + ex.Message);
            }

            _isViewingStudents = false;
        }

        private async void btnMonthly_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                int? subjectId = (int)cmbSubject.SelectedValue;
                if (subjectId == 0) subjectId = null;

                var attendance = await _api.GetAttendanceByTeacherRange(
                    Int32.Parse(_loggedInUser.Number), startOfMonth, endOfMonth, subjectId);

                dgv.DataSource = ToDataTable(attendance);
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading monthly report: " + ex.Message);
            }

            _isViewingStudents = false;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //var loginForm = Application.OpenForms["LoginForm"] as LoginForm;
            //if (loginForm != null)
            //{
            //    loginForm.Show();
            //    loginForm.ClearFields();
            //}

            LoginForm loginForm = new LoginForm();
            loginForm.ClearFields();
            loginForm.Show();

            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgv.Columns[e.ColumnIndex].DataPropertyName;

            // Decide the new sort order
            SortOrder newSortOrder;
            if (_lastSortedColumn == columnName && _lastSortOrder == SortOrder.Ascending)
                newSortOrder = SortOrder.Descending;
            else
                newSortOrder = SortOrder.Ascending;

            // Apply sort if data source supports it
            var data = dgv.DataSource as DataTable;
            if (data != null)
            {
                data.DefaultView.Sort = $"{columnName} {(newSortOrder == SortOrder.Ascending ? "ASC" : "DESC")}";
                dgv.DataSource = data;
            }

            // Update tracker
            _lastSortedColumn = columnName;
            _lastSortOrder = newSortOrder;
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            var table = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in items)
            {
                var row = table.NewRow();
                foreach (var prop in props)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
