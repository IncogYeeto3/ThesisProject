namespace Thesis_Proto3.Forms
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.btnSitIn = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnViewStudent = new System.Windows.Forms.Button();
            this.btnViewAttendance = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.USTACLogo = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPageLast = new System.Windows.Forms.Button();
            this.btnPageNext = new System.Windows.Forms.Button();
            this.btnPageBack = new System.Windows.Forms.Button();
            this.btnPageFirst = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.flpFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.USTACLogo)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSitIn
            // 
            this.btnSitIn.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnSitIn.FlatAppearance.BorderSize = 0;
            this.btnSitIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSitIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSitIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.btnSitIn.Image = ((System.Drawing.Image)(resources.GetObject("btnSitIn.Image")));
            this.btnSitIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSitIn.Location = new System.Drawing.Point(3, 3);
            this.btnSitIn.Name = "btnSitIn";
            this.btnSitIn.Size = new System.Drawing.Size(212, 55);
            this.btnSitIn.TabIndex = 7;
            this.btnSitIn.Text = "             Sit-In";
            this.btnSitIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSitIn.UseVisualStyleBackColor = false;
            this.btnSitIn.Click += new System.EventHandler(this.btnSitIn_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(49, 105);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(1617, 709);
            this.dgv.TabIndex = 8;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseClick);
            // 
            // btnViewStudent
            // 
            this.btnViewStudent.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnViewStudent.FlatAppearance.BorderSize = 0;
            this.btnViewStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewStudent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.btnViewStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnViewStudent.Image")));
            this.btnViewStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewStudent.Location = new System.Drawing.Point(3, 125);
            this.btnViewStudent.Name = "btnViewStudent";
            this.btnViewStudent.Size = new System.Drawing.Size(215, 55);
            this.btnViewStudent.TabIndex = 9;
            this.btnViewStudent.Text = "    View Student";
            this.btnViewStudent.UseVisualStyleBackColor = false;
            this.btnViewStudent.Click += new System.EventHandler(this.btnViewStudent_Click);
            // 
            // btnViewAttendance
            // 
            this.btnViewAttendance.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnViewAttendance.FlatAppearance.BorderSize = 0;
            this.btnViewAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewAttendance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAttendance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.btnViewAttendance.Image = ((System.Drawing.Image)(resources.GetObject("btnViewAttendance.Image")));
            this.btnViewAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewAttendance.Location = new System.Drawing.Point(3, 64);
            this.btnViewAttendance.Name = "btnViewAttendance";
            this.btnViewAttendance.Size = new System.Drawing.Size(212, 55);
            this.btnViewAttendance.TabIndex = 10;
            this.btnViewAttendance.Text = "           View Attendance";
            this.btnViewAttendance.UseVisualStyleBackColor = false;
            this.btnViewAttendance.Click += new System.EventHandler(this.btnViewAttendance_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1287, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Admin";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.btnLogOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Image = ((System.Drawing.Image)(resources.GetObject("btnLogOut.Image")));
            this.btnLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogOut.Location = new System.Drawing.Point(1587, 0);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(122, 82);
            this.btnLogOut.TabIndex = 16;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowText;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.USTACLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 1051);
            this.panel1.TabIndex = 17;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(-3, 355);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(212, 55);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "                Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(4, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 32);
            this.label4.TabIndex = 15;
            this.label4.Text = "   NAVIGATION   ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(133, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 32);
            this.label2.TabIndex = 14;
            this.label2.Text = "AC";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(82, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 32);
            this.label3.TabIndex = 13;
            this.label3.Text = "UST";
            // 
            // USTACLogo
            // 
            this.USTACLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("USTACLogo.BackgroundImage")));
            this.USTACLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.USTACLogo.Location = new System.Drawing.Point(11, 7);
            this.USTACLogo.Name = "USTACLogo";
            this.USTACLogo.Size = new System.Drawing.Size(64, 65);
            this.USTACLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.USTACLogo.TabIndex = 12;
            this.USTACLogo.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(8)))));
            this.panel2.Controls.Add(this.btnLogOut);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(215, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1709, 82);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.flpFilters);
            this.panel3.Controls.Add(this.btnResetFilter);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dtpEndDate);
            this.panel3.Controls.Add(this.dtpStartDate);
            this.panel3.Controls.Add(this.dgv);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(215, 82);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1709, 969);
            this.panel3.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSitIn);
            this.flowLayoutPanel1.Controls.Add(this.btnViewAttendance);
            this.flowLayoutPanel1.Controls.Add(this.btnViewStudent);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 166);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(213, 183);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPageLast);
            this.panel4.Controls.Add(this.btnPageNext);
            this.panel4.Controls.Add(this.btnPageBack);
            this.panel4.Controls.Add(this.btnPageFirst);
            this.panel4.Controls.Add(this.lblPage);
            this.panel4.Location = new System.Drawing.Point(1418, 846);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(248, 80);
            this.panel4.TabIndex = 27;
            // 
            // btnPageLast
            // 
            this.btnPageLast.BackColor = System.Drawing.SystemColors.Control;
            this.btnPageLast.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPageLast.FlatAppearance.BorderSize = 0;
            this.btnPageLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPageLast.Location = new System.Drawing.Point(180, 22);
            this.btnPageLast.Name = "btnPageLast";
            this.btnPageLast.Size = new System.Drawing.Size(52, 45);
            this.btnPageLast.TabIndex = 32;
            this.btnPageLast.Text = ">>";
            this.btnPageLast.UseVisualStyleBackColor = false;
            this.btnPageLast.Click += new System.EventHandler(this.btnPageLast_Click);
            // 
            // btnPageNext
            // 
            this.btnPageNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnPageNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPageNext.FlatAppearance.BorderSize = 0;
            this.btnPageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPageNext.Location = new System.Drawing.Point(145, 27);
            this.btnPageNext.Name = "btnPageNext";
            this.btnPageNext.Size = new System.Drawing.Size(29, 34);
            this.btnPageNext.TabIndex = 31;
            this.btnPageNext.Text = ">";
            this.btnPageNext.UseVisualStyleBackColor = false;
            this.btnPageNext.Click += new System.EventHandler(this.btnPageNext_Click);
            // 
            // btnPageBack
            // 
            this.btnPageBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnPageBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPageBack.FlatAppearance.BorderSize = 0;
            this.btnPageBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPageBack.Location = new System.Drawing.Point(88, 28);
            this.btnPageBack.Name = "btnPageBack";
            this.btnPageBack.Size = new System.Drawing.Size(29, 34);
            this.btnPageBack.TabIndex = 30;
            this.btnPageBack.Text = "<";
            this.btnPageBack.UseVisualStyleBackColor = false;
            this.btnPageBack.Click += new System.EventHandler(this.btnPageBack_Click);
            // 
            // btnPageFirst
            // 
            this.btnPageFirst.BackColor = System.Drawing.SystemColors.Control;
            this.btnPageFirst.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPageFirst.FlatAppearance.BorderSize = 0;
            this.btnPageFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPageFirst.Location = new System.Drawing.Point(35, 29);
            this.btnPageFirst.Name = "btnPageFirst";
            this.btnPageFirst.Size = new System.Drawing.Size(47, 31);
            this.btnPageFirst.TabIndex = 29;
            this.btnPageFirst.Text = "<<";
            this.btnPageFirst.UseVisualStyleBackColor = false;
            this.btnPageFirst.Click += new System.EventHandler(this.btnPageFirst_Click);
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPage.Location = new System.Drawing.Point(83, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(91, 25);
            this.lblPage.TabIndex = 28;
            this.lblPage.Text = "Page 0/0";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flpFilters
            // 
            this.flpFilters.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpFilters.Location = new System.Drawing.Point(49, 70);
            this.flpFilters.Name = "flpFilters";
            this.flpFilters.Size = new System.Drawing.Size(1617, 29);
            this.flpFilters.TabIndex = 26;
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.AutoSize = true;
            this.btnResetFilter.BackColor = System.Drawing.Color.DarkRed;
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetFilter.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnResetFilter.Location = new System.Drawing.Point(463, 32);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(97, 35);
            this.btnResetFilter.TabIndex = 25;
            this.btnResetFilter.Text = "ResetFilter";
            this.btnResetFilter.UseVisualStyleBackColor = false;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(253, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "End Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(46, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Start Date";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("Consolas", 18F);
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(256, 34);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(201, 32);
            this.dtpEndDate.TabIndex = 21;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Consolas", 18F);
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(49, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(201, 32);
            this.dtpStartDate.TabIndex = 20;
            this.dtpStartDate.Value = new System.DateTime(2025, 11, 22, 21, 33, 15, 0);
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1051);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.USTACLogo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSitIn;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnViewStudent;
        private System.Windows.Forms.Button btnViewAttendance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox USTACLogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.FlowLayoutPanel flpFilters;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPageFirst;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnPageLast;
        private System.Windows.Forms.Button btnPageNext;
        private System.Windows.Forms.Button btnPageBack;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}