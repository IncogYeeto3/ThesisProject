using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thesis_Proto3.Crystal;
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

        //=====================================================================================

        public AdminForm(LoginResponse loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _api = new ApiService();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + _loggedInUser.Role + ", " + _loggedInUser.Username;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            ResetFilterState();
            btnViewAttendance.PerformClick();
        }

        private void btnSitIn_Click(object sender, EventArgs e)
        {
            using (var sitInForm = new SitInForm(_loggedInUser))
            {
                sitInForm.ShowDialog();
            }
        }

        //=====================================================================================

        private async void btnViewStudent_Click(object sender, EventArgs e)
        {
            _isViewingStudents = true;
            ResetFilterState();

            
            var filter = new StudentFilterRequest
            {
                IsAdmin = true,
                Page = 1,
                PageSize = 50,
            };

            var result = await _api.GetStudentsAsync(filter);

            dgv.DataSource = ToDataTable(result.Records);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private async void btnViewAttendance_Click(object sender, EventArgs e)
        {
            ResetFilterState();
            RefreshAttendanceData();

            _isViewingStudents = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Get the current DataTable from the DataGridView
            if (dgv.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                DataSet1 ds = new DataSet1();
                ds.AttendanceRecord.Clear();
                foreach (DataRow row in dt.Rows)
                    ds.AttendanceRecord.ImportRow(row);

                // Open the CrystalReportViewer form
                CrystalReportViewer viewer = new CrystalReportViewer(ds);
                viewer.Show();
            }
            else
            {
                MessageBox.Show("No data available to print.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //=====================================================================================

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

        private async void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFilterState();
                DisplayActiveFilters();
                await RefreshAttendanceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting filters: " + ex.Message);
            }
        }

        //=====================================================================================

        private async void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            _filters.StartDate = dtpStartDate.Value.Date;
            _filters.Page = 1;

            await RefreshAttendanceData();
            _isViewingStudents = false;
        }

        private async void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            _filters.EndDate = dtpEndDate.Value.Date;
            _filters.Page = 1;

            await RefreshAttendanceData();
            _isViewingStudents = false;
        }

        //=====================================================================================

        private async void btnPageFirst_Click(object sender, EventArgs e)
        {
            _filters.Page = 1;
            await RefreshAttendanceData();
        }

        private async void btnPageBack_Click(object sender, EventArgs e)
        {
            if (_filters.Page > 1)
            {
                _filters.Page--;
                await RefreshAttendanceData();
            }
        }

        private async void btnPageNext_Click(object sender, EventArgs e)
        {
            _filters.Page++;
            await RefreshAttendanceData();
        }

        private async void btnPageLast_Click(object sender, EventArgs e)
        {
            // We need total count to calculate last page
            var tempRequest = new AttendanceFilterRequest
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
                Page = 1,
                PageSize = _filters.PageSize
            };

            var result = await _api.GetAttendanceUniversalAsync(tempRequest);
            _filters.Page = (int)Math.Ceiling((double)result.TotalCount / _filters.PageSize);
            await RefreshAttendanceData();
        }

        //=====================================================================================

        private void ResetFilterState()
        {
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

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            flpFilters.Controls.Clear();
            dgv.DataSource = null;
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
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DisplayActiveFilters();

            // Update pagination label
            int start = (result.Records.Count > 0) ? ((_filters.Page - 1) * _filters.PageSize + 1) : 0;
            int end = Math.Min(_filters.Page * _filters.PageSize, result.TotalCount);
            lblPage.Text = $"{start}-{end}/{result.TotalCount}";

            // Enable/disable buttons
            btnPageFirst.Enabled = _filters.Page > 1;
            btnPageBack.Enabled = _filters.Page > 1;
            btnPageNext.Enabled = _filters.Page < Math.Ceiling((double)result.TotalCount / _filters.PageSize);
            btnPageLast.Enabled = _filters.Page < Math.Ceiling((double)result.TotalCount / _filters.PageSize);
        }

        private void DisplayActiveFilters()
        {
            flpFilters.Controls.Clear();

            // Example: only add filters that are actually set
            if (!string.IsNullOrWhiteSpace(_filters.StudentName))
                AddFilterLabel("StudentName", _filters.StudentName);

            if (!string.IsNullOrWhiteSpace(_filters.StudentNumber))
                AddFilterLabel("StudentNumber", _filters.StudentNumber);

            if (!string.IsNullOrWhiteSpace(_filters.SubjectCode))
                AddFilterLabel("SubjectCode", _filters.SubjectCode);

            if (!string.IsNullOrWhiteSpace(_filters.SubjectName))
                AddFilterLabel("SubjectName", _filters.SubjectName);

            if (!string.IsNullOrWhiteSpace(_filters.PCNumber))
                AddFilterLabel("PCNumber", _filters.PCNumber);

            if (!string.IsNullOrWhiteSpace(_filters.RoomNumber))
                AddFilterLabel("RoomNumber", _filters.RoomNumber);

            if (_filters.StartDate.HasValue && _filters.EndDate.HasValue)
                AddFilterLabel("LogDate", $"{_filters.StartDate.Value:yyyy-MM-dd}");
        }

        private void AddFilterLabel(string key, string value)
        {
            Label lbl = new Label();
            lbl.Text = $"{key}: {value}";
            lbl.Padding = new Padding(5, 2, 5, 2);
            lbl.Margin = new Padding(3);
            lbl.BackColor = Color.LightGray;
            lbl.AutoSize = true;
            lbl.Tag = key; // store the filter key

            // Hover effect
            lbl.MouseEnter += (s, e) =>
            {
                lbl.ForeColor = Color.Gray;
                lbl.Font = new Font(lbl.Font, FontStyle.Strikeout);
                lbl.Cursor = Cursors.Hand; // show it’s clickable
            };
            lbl.MouseLeave += (s, e) =>
            {
                lbl.ForeColor = Color.Black;
                lbl.Font = new Font(lbl.Font, FontStyle.Regular);
                lbl.Cursor = Cursors.Default;
            };

            // Click to remove filter
            lbl.MouseClick += async (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (key)
                    {
                        case "StudentNumber":
                            _filters.StudentNumber = null;
                            break;
                        case "StudentName":
                            _filters.StudentName = null;
                            break;
                        case "SubjectCode":
                            _filters.SubjectCode = null;
                            break;
                        case "SubjectName":
                            _filters.SubjectName = null;
                            break;
                        case "PCNumber":
                            _filters.PCNumber = null;
                            break;
                        case "RoomNumber":
                            _filters.RoomNumber = null;
                            break;
                        case "LogDate":
                            _filters.StartDate = null;
                            _filters.EndDate = null;
                            break;
                    }

                    // Refresh the FLP and the DataGridView
                    DisplayActiveFilters();
                    await RefreshAttendanceData();
                }
            };

            flpFilters.Controls.Add(lbl);
        }

        
    }
}
