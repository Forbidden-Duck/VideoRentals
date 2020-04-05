using System;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmMenu : Form {
        #region Constructors

        /// <summary>
        /// Initlize the frame
        /// </summary>
        public frmMenu() {
            // Initalize the frame components
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnExit_MouseClick(object sender, MouseEventArgs e) {
            // Close the Frame
            this.Close();
        }

        private void btnSettings_MouseClick(object sender, MouseEventArgs e) {
            // Start a new thread for frmSettings
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcSettings)));
        }
        private void btnRentals_MouseClick(object sender, MouseEventArgs e) {
            // Start a new thread for frmRentalList
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcRentalList)));
        }
        private void btnCustomers_MouseClick(object sender, MouseEventArgs e) {
            // Start a new thread for frmCustomerList
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcCustomerList)));
        }
        private void btnMovies_MouseClick(object sender, MouseEventArgs e) {
            // Start a new thread for frmMovieList
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMovieList)));
        }
        private void btnReport_Click(object sender, EventArgs e) {
            // Start a new thread for frmReport
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcReport)));
        }

        #endregion

        #region Form Events

        private void frmMenu_Paint(object sender, PaintEventArgs e) {
            // Change the form background colour to the ColorTheme saved in the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Run a new Form
        /// </summary>
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

        /// <summary>
        /// Starts the new thread
        /// </summary>
        /// <param name="thread">The new thread</param>
        public void ThreadStart(System.Threading.Thread thread) {
            // Start the new thread
            // Close the current frame
            thread.Start();
            this.Close();
        }

        #endregion
    }
}
