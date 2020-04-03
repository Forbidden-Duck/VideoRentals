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
    public partial class frmCustomerList : Form {
        #region Constructors

        public frmCustomerList() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddCustomer_MouseClick(object sender, MouseEventArgs e) {
            frmCustomer frm = new frmCustomer();
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmCustomerList_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void FrmCustomerList_Load(object sender, EventArgs e) {
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void DgvCustomers_DoubleClick(object sender, EventArgs e) {
            // If no cell selected, do nothing
            if (dgvCustomers.CurrentCell == null) {
                return;
            }

            // Primary key of selected cell
            long pkID = long.Parse(dgvCustomers[0, dgvCustomers.CurrentCell.RowIndex].Value.ToString());

            frmCustomer frm = new frmCustomer(pkID);
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

        private void PopulateGrid() {
            DataTable dtable = new DataTable();
            dtable = Context.GetDataTable("Customer");
            dgvCustomers.DataSource = dtable;
        }

        #endregion
    }
}
