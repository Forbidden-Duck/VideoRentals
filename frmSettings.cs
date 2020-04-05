using System;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmSettings : Form {
        #region Constructors

        /// <summary>
        /// Create a new instance of frmSettings
        /// </summary>
        public frmSettings() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Create a new thread for frmMenu
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }

        private void btnChangeColour_Click(object sender, EventArgs e) {
            // Create and assign a new ColorDialog
            // Assign a new Customer Colour to the ColorDialog
            ColorDialog colordialog = new ColorDialog();
            colordialog.CustomColors = new int[] { 0xF5F5F5, 0xb3ff00 };

            // Show the ColorDialog
            // If the ColorDialog returns DialogResult 
            if (colordialog.ShowDialog() == DialogResult.OK) {
                // Assign the ColorTheme from the project settings to the ColorDialog Colour
                // Save the project settings
                Properties.Settings.Default.ColorTheme = colordialog.Color;
                Properties.Settings.Default.Save();

                // Assign the form background colour to the ColorTheme from the project settings
                this.BackColor = Properties.Settings.Default.ColorTheme;
            }
        }

        #endregion

        #region Form Events

        private void frmSettings_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour to the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Run the form
        /// </summary>
        public static void ThreadProcMenu() {
            Application.Run(new frmMenu());
        }
        /// <summary>
        /// Start the new thread
        /// </summary>
        /// <param name="thread">The new thread</param>
        public void ThreadStart(System.Threading.Thread thread) {
            // Start the thread
            // Close the current form
            thread.Start();
            this.Close();
        }

        #endregion
    }
}
