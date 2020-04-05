using SQLController;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmReport : Form {
        #region Global Variables

        // Create a variable for the DataView
        DataView _dvHistory = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of frmReport
        /// </summary>
        public frmReport() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e) {
            // Create a new thread for frmMenu
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }

        private void BtnExport_Click(object sender, EventArgs e) {
            // Create and assign a new StringBuilder
            StringBuilder csv = new StringBuilder();

            // For each DataRowView in the DataView
            foreach (DataRowView drv in _dvHistory) {
                // Append a line with DateRented, CustomerName, MovieName and DateReturned
                csv.AppendLine(
                    $"{drv["DateRented"].ToString()}" +
                    $"{drv["CustomerName"].ToString()}" +
                    $"{drv["MovieName"].ToString()}" +
                    $"{drv["DateReturned"].ToString()}");
            }

            // Write the StringBuilder to the VideoRental CSV
            // Show a MessageBox
            File.WriteAllText(Application.StartupPath + @"\VideoRentalsHistory.csv", csv.ToString());
            MessageBox.Show("Video Rentals exported to CSV", Properties.Settings.Default.ProjectName);
        }

        #endregion

        #region Form Events

        private void frmReport_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour to the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void FrmReport_Load(object sender, EventArgs e) {
            // Populate the DataGridView
            PopulateGrid();
        }

        #endregion

        #region TextBox Events

        private void TxtSearch_TextChanged(object sender, EventArgs e) {
            // Assign the DataView RowFilter
            _dvHistory.RowFilter =
                $"CustomerName LIKE '%{txtSearch.Text}%'" +
                $"OR MovieName LIKE '%{txtSearch.Text}%'";
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

        /// <summary>
        /// Populate the DataGridView
        /// </summary>
        private void PopulateGrid() {
            // Create and assign a new SQL Query
            string sqlQuery =
                "SELECT Rental.DateRented, Customer.CustomerName, Movie.MovieName, Rental.DateReturned " +
                "FROM Customer INNER JOIN " +
                "Rental ON Customer.CustomerID = Rental.CustomerID INNER JOIN " +
                "RentalItem ON Rental.RentalID = RentalItem.RentalID CROSS JOIN Movie " +
                "ORDER BY Rental.DateRented DESC";

            // Create and assign the DataTable with the MovieHistory DataTable
            // Assign the DataTable to the DataView
            // Assign the DataGridView with the DataView
            DataTable dtable = Context.GetDataTable(sqlQuery, "MovieHistory", true);
            _dvHistory = new DataView(dtable);
            dgvReport.DataSource = _dvHistory;
        }

        #endregion
    }
}
