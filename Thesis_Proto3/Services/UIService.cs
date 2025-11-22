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
            //// Make form full screen and topmost
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            //form.TopMost = true;

            //// Remove control box (close/minimize/maximize)
            //form.ControlBox = false;
            //form.MinimizeBox = false;
            //form.MaximizeBox = false;

            //// Intercept Alt+F4
            //form.KeyPreview = true;
            //form.KeyDown += BlockAltF4;

            //// Prevent any user‑initiated close (including Alt+F4’s WM_CLOSE)
            //form.FormClosing += PreventClose;
        }

        private static void BlockAltF4(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                MessageBox.Show("Alt+F4 is disabled in this mode.");
            }
        }

        private static void PreventClose(object sender, FormClosingEventArgs e)
        {
            // Block user attempts to close the form
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MessageBox.Show("Closing is disabled in kiosk mode.");
            }
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
