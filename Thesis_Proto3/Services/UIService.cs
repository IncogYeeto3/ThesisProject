using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thesis_Proto3.Services
{
    public class UIService
    {
        public static void Enable(Form form)
        {
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            //form.TopMost = true;
            //form.ControlBox = false;
            //form.MinimizeBox = false;
        }

        public static void Disable(Form form)
        {
            form.TopMost = false;
            form.MinimizeBox = true;
            form.ControlBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.WindowState = FormWindowState.Normal;
        }
    }
}
