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
    public partial class frmMenu : Form {
        public frmMenu() {
            InitializeComponent();
        }

        private void btnExit_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }

        // Open the application
        public static void ThreadProcSettings() {
            Application.Run(new frmSettings());
        }
        public static void ThreadProcRentalList() {
            Application.Run(new frmRentalList());
        }
        public static void ThreadProcCustomerList() {
            Application.Run(new frmCustomerList());
        }
        public static void ThreadProcMovieList() {
            Application.Run(new frmMovieList());
        }
        public static void ThreadProcReport() {
            Application.Run(new frmReport());
        }
        // Put application in a new thread
        public void ThreadStart(System.Threading.Thread thread) {
            thread.Start();
            this.Close();
        }
        private void btnSettings_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcSettings)));
        }
        private void btnRentals_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcRentalList)));
        }
        private void btnCustomers_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcCustomerList)));
        }
        private void btnMovies_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMovieList)));
        }
        private void btnReport_Click(object sender, EventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcReport)));
        }

        private void frmMenu_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }
    }
}
