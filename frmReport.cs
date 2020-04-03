using SQLController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmReport : Form {
        #region Global Variables

        DataView _dvHistory = null;

        #endregion

        #region Constructors

        public frmReport() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }

        private void BtnExport_Click(object sender, EventArgs e) {
            StringBuilder csv = new StringBuilder();
            foreach (DataRowView drv in _dvHistory) {
                csv.AppendLine(
                    $"{drv["DateRented"].ToString()}" +
                    $"{drv["CustomerName"].ToString()}" +
                    $"{drv["MovieName"].ToString()}" +
                    $"{drv["DateReturned"].ToString()}");
            }
            File.WriteAllText(Application.StartupPath + @"\VideoRentalsHistory.csv", csv.ToString());
            MessageBox.Show("Video Rentals exported to CSV", Properties.Settings.Default.ProjectName);
        }

        #endregion

        #region Form Events

        private void frmReport_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void FrmReport_Load(object sender, EventArgs e) {
            PopulateGrid();
        }

        #endregion

        #region TextBox Events

        private void TxtSearch_TextChanged(object sender, EventArgs e) {
            _dvHistory.RowFilter =
                $"CustomerName LIKE '%{txtSearch.Text}%'" +
                $"OR MovieName LIKE '%{txtSearch.Text}%'";
        }

        #endregion

        #region Helper Methods

        // Open the application
        public static void ThreadProcMenu() {
            Application.Run(new frmMenu());
        }

        // Put application in a new thread
        public void ThreadStart(System.Threading.Thread thread) {
            thread.Start();
            this.Close();
        }

        private void PopulateGrid() {
            string sqlQuery =
                "SELECT Rental.DateRented, Customer.CustomerName, Movie.MovieName, Rental.DateReturned " +
                "FROM Customer INNER JOIN " +
                "Rental ON Customer.CustomerID = Rental.CustomerID INNER JOIN " +
                "RentalItem ON Rental.RentalID = RentalItem.RentalID CROSS JOIN Movie " +
                "ORDER BY Rental.DateRented DESC";
            DataTable dtable = Context.GetDataTable(sqlQuery, "MovieHistory", true);
            _dvHistory = new DataView(dtable);
            dgvReport.DataSource = _dvHistory;
        }

        #endregion
    }
}
