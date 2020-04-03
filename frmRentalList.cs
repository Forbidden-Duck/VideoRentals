using SQLController;
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
    public partial class frmRentalList : Form {
        #region Constructors

        public frmRentalList() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddRental_MouseClick(object sender, MouseEventArgs e) {
            frmRental frm = new frmRental();
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmRentalList_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRentalList_Load(object sender, EventArgs e) {
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void DgvRentals_DoubleClick(object sender, EventArgs e) {
            // If no cell select, do nothing
            if (dgvRentals.CurrentCell == null) {
                return;
            }

            // Primary key of select cell
            long pkID = long.Parse(dgvRentals[0, dgvRentals.CurrentCell.RowIndex].Value.ToString());
            frmRental frm = new frmRental(pkID);
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
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

        public void PopulateGrid() {
            string sqlQuery = 
                "SELECT Rental.RentalID, Customer.CustomerName, Rental.DateRented, Rental.DateReturned " +
                "FROM Customer INNER JOIN " +
                "Rental ON Customer.CustomerID = Rental.CustomerID " +
                "ORDER BY Rental.RentalID DESC";
            DataTable dtable = new DataTable();
            dtable = Context.GetDataTable(sqlQuery, "Rental");
            dgvRentals.DataSource = dtable;
        }

        #endregion
    }
}
