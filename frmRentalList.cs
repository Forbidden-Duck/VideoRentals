using SQLController;
using System;
using System.Data;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmRentalList : Form {
        #region Constructors

        /// <summary>
        /// Create a new instance of frmRentalList
        /// </summary>
        public frmRentalList() {
            // Initialize the form components
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Create a new thread for frmMenu
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddRental_MouseClick(object sender, MouseEventArgs e) {
            // Create a new instance of frmRental
            frmRental frm = new frmRental();
            // Show the form
            // If the form returns DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmRentalList_Paint(object sender, PaintEventArgs e) {
            // Assign form background colour with the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRentalList_Load(object sender, EventArgs e) {
            // Populate the DataGridView
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void DgvRentals_DoubleClick(object sender, EventArgs e) {
            // Check if a cell has been selected in the DataGridView
            // If not then stop the method
            if (dgvRentals.CurrentCell == null) {
                return;
            }

            // Create and assign the DataGridView Primary Key
            long pkID = long.Parse(dgvRentals[0, dgvRentals.CurrentCell.RowIndex].Value.ToString());

            // Create a new instance of frmRental (with the Primary Key)
            frmRental frm = new frmRental(pkID);
            // Show the form
            // If the form returns DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
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
            // Starts the new thread
            // Closes the current form
            thread.Start();
            this.Close();
        }

        /// <summary>
        /// Populates the DataGridView
        /// </summary>
        public void PopulateGrid() {
            // Create and assign a new SQL Query
            string sqlQuery = 
                "SELECT Rental.RentalID, Customer.CustomerName, Rental.DateRented, Rental.DateReturned " +
                "FROM Customer INNER JOIN " +
                "Rental ON Customer.CustomerID = Rental.CustomerID " +
                "ORDER BY Rental.RentalID DESC";

            // Create and assign the Rental DataTable
            // Assign DataGridView with the DataTable
            DataTable dtable = Context.GetDataTable(sqlQuery, "Rental");
            dgvRentals.DataSource = dtable;
        }

        #endregion
    }
}
