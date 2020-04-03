using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmSettings : Form {
        public frmSettings() {
            InitializeComponent();
        }

        // Open the application
        public static void ThreadProcMenu() {
            Application.Run(new frmMenu());
        }
        // Put application in a new thread
        public void ThreadStart(System.Threading.Thread thread) {
            thread.Start();
            this.Close();
        }
        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }

        private void frmSettings_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void btnChangeColour_Click(object sender, EventArgs e) {
            ColorDialog colordialog = new ColorDialog();
            colordialog.CustomColors = new int[] { 0xF5F5F5, 0xb3ff00 };
            if (colordialog.ShowDialog() == DialogResult.OK) {
                // Change ColorTheme Property
                Properties.Settings.Default.ColorTheme = colordialog.Color;
                Properties.Settings.Default.Save();
                // Change current form color
                this.BackColor = Properties.Settings.Default.ColorTheme;
            }
        }
    }
}
