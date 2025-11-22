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

namespace Thesis_Proto3.Forms
{
    public partial class CrystalReportViewer : Form
    {
        public CrystalReportViewer(DataSet1 ds)
        {
            InitializeComponent();

            CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(ds);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
        }
    }
}
