using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AltF4CustomAction
{
    public partial class Form1 : Form
    {
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                //your custom menu or perform any desired action here
                ShowCustomMenu();
                return true;
                //prevent the default Alt+F4 behavior
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowCustomMenu()
        {
            //implement your custom menu logic here
            //for example, show a context menu with options like power off, restart, etc.
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Power Off");
            menu.Items.Add("Restart");

            menu.Show(Cursor.Position);
        }

        private void buttonCustomAltF4_Click(object sender, EventArgs e)
        {
            ShowCustomMenu();
        }
    }
}
