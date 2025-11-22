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
    public partial class AdminForm : Form
    {
        private AttendanceFilterRequest _filters = new AttendanceFilterRequest();
        private readonly LoginResponse _loggedInUser;
        private readonly ApiService _api;
        private bool _isViewingStudents = true;
        private string _lastSortedColumn = null;
        private SortOrder _lastSortOrder = SortOrder.None;


        public AdminForm(LoginResponse loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _api = new ApiService();
        }
        private async void AdminForm_Load(object sender, EventArgs e)
        {
            btnViewStudent.PerformClick();

            label1.Text = "Welcome " + _loggedInUser.Role + ", " + _loggedInUser.Username;


            var subjects = await _api.GetAllSubjectsAsync();

            subjects.Insert(0, new Subject { SubjectID = 0, SubjectName = "" });

            cmbSubject.DataSource = ToDataTable(subjects);
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
            var students = await _api.GetStudentsByAdminAsync();

            dgv.DataSource = ToDataTable(students);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _isViewingStudents = true;
        }

        private async void btnViewAttendance_Click(object sender, EventArgs e)
        {
            var attendance = await _api.GetAttendanceByAdminAsync();

            dgv.DataSource = ToDataTable(attendance);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _isViewingStudents = false;
        }


        //TODO UPDATE THIS
        private async void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgv.Columns[e.ColumnIndex].Name;
            string value = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

            if (string.IsNullOrWhiteSpace(value)) return;

            // Map the column name dynamically to the filter state
            switch (columnName)
            {
                case "StudentNumber":
                    _filters.StudentNumber = value;
                    break;
                case "StudentName":
                    _filters.StudentName = value;
                    break;
                case "SubjectCode":
                    _filters.SubjectCode = value;
                    break;
                case "SubjectName":
                    _filters.SubjectName = value;
                    break;
                case "PCNumber":
                    _filters.PCNumber = value;
                    break;
                case "RoomNumber":
                    _filters.RoomNumber = value;
                    break;
                case "LogDate":
                    if (DateTime.TryParse(value, out DateTime parsedDate))
                    {
                        _filters.StartDate = parsedDate.Date;
                        _filters.EndDate = parsedDate.Date;
                    }
                    break;
                default:
                    // For any other column, just ignore
                    return;
            }

            // Reset page to 1 after applying new filter
            _filters.Page = 1;

            // Reload the grid with updated filter
            await RefreshAttendanceData();
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

        private async void btnDateSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _filters.StartDate = dtpStartDate.Value.Date;
                _filters.EndDate = dtpEndDate.Value.Date;

                _filters.Page = 1; // reset pagination when changing filters

                await RefreshAttendanceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading monthly report: " + ex.Message);
            }

            _isViewingStudents = false;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset your filter state
                _filters = new AttendanceFilterRequest
                {
                    IsAdmin = true,
                    StudentNumber = null,
                    StudentName = null,
                    SubjectCode = null,
                    SubjectName = null,
                    PCNumber = null,
                    RoomNumber = null,
                    StartDate = null,
                    EndDate = null,
                    Page = 1,
                    PageSize = 50
                };

                // Reset WinForms controls
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Today;
                cmbSubject.SelectedIndex = 0; // or -1 if you want none selected

                // Clear DataGridView
                dgv.DataSource = null;

                _isViewingStudents = false;
                DisplayActiveFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting filters: " + ex.Message);
            }
        }

        private async Task RefreshAttendanceData()
        {
            var request = new AttendanceFilterRequest
            {
                IsAdmin = true,
                StudentNumber = string.IsNullOrWhiteSpace(_filters.StudentNumber) ? null : _filters.StudentNumber,
                StudentName = string.IsNullOrWhiteSpace(_filters.StudentName) ? null : _filters.StudentName,
                SubjectCode = string.IsNullOrWhiteSpace(_filters.SubjectCode) ? null : _filters.SubjectCode,
                SubjectName = string.IsNullOrWhiteSpace(_filters.SubjectName) ? null : _filters.SubjectName,
                PCNumber = string.IsNullOrWhiteSpace(_filters.PCNumber) ? null : _filters.PCNumber,
                RoomNumber = string.IsNullOrWhiteSpace(_filters.RoomNumber) ? null : _filters.RoomNumber,
                StartDate = _filters.StartDate,
                EndDate = _filters.EndDate,
                Page = _filters.Page,
                PageSize = _filters.PageSize
            };

            var result = await _api.GetAttendanceUniversalAsync(request);

            dgv.DataSource = ToDataTable(result.Records);
            DisplayActiveFilters();
            //lblTotalCount.Text = $"Total: {result.TotalCount}";
        }

        private void DisplayActiveFilters()
        {
            // Clear previous items
            flpFilters.Controls.Clear();

            // Create a helper dictionary: property name => display name
            var filterMap = new Dictionary<string, string>
                {
                    { nameof(_filters.StudentNumber), "Student #" },
                    { nameof(_filters.StudentName), "Student Name" },
                    { nameof(_filters.SubjectCode), "Subject Code" },
                    { nameof(_filters.SubjectName), "Subject Name" },
                    { nameof(_filters.PCNumber), "PC Number" },
                    { nameof(_filters.RoomNumber), "Room" },
                    { nameof(_filters.StartDate), "Start Date" },
                    { nameof(_filters.EndDate), "End Date" }
                };

            // List of properties to ignore
            var ignoreProps = new HashSet<string> { nameof(_filters.IsAdmin), nameof(_filters.Page), nameof(_filters.PageSize) };

            // Reflection to loop over _filters properties
            var props = _filters.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (ignoreProps.Contains(prop.Name))
                    continue;

                object value = prop.GetValue(_filters);

                if (value != null && !(value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    string displayName = filterMap.ContainsKey(prop.Name) ? filterMap[prop.Name] : prop.Name;
                    string displayValue;

                    // Format dates nicely
                    if (value is DateTime dt)
                    {
                        displayValue = dt.ToShortDateString();
                    }
                    else
                    {
                        displayValue = value.ToString();
                    }

                    // Create a Label for each active filter
                    Label lbl = new Label
                    {
                        Text = $"{displayName}: {displayValue}",
                        AutoSize = true,
                        Padding = new Padding(5),
                        Margin = new Padding(3),
                        BackColor = Color.LightBlue,
                        ForeColor = Color.Black,
                        Cursor = Cursors.Hand // hint that it’s clickable later
                    };

                    flpFilters.Controls.Add(lbl);
                }
            }
        }



    }
}
