using SQLController;
using System;
using System.Data;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmCustomerList : Form {
        #region Constructors

        /// <summary>
        /// Initialize the form
        /// </summary>
        public frmCustomerList() {
            // Initialize the form components
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Create a new thread for frmMenu
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddCustomer_MouseClick(object sender, MouseEventArgs e) {
            // Create a new instance of frmCustomer
            frmCustomer frm = new frmCustomer();
            // Show the form
            // If the form returns DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmCustomerList_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour to the ColorTheme in the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void FrmCustomerList_Load(object sender, EventArgs e) {
            // Populate the DataGridView
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void DgvCustomers_DoubleClick(object sender, EventArgs e) {
            // Check if a cell has been selected in the DataGridView
            // If not then stop the method
            if (dgvCustomers.CurrentCell == null) {
                return;
            }

            // Create and assign the DataGridView Primary Key
            long pkID = long.Parse(dgvCustomers[0, dgvCustomers.CurrentCell.RowIndex].Value.ToString());

            // Create a new instance of frmCustomer (with the Primary Key)
            frmCustomer frm = new frmCustomer(pkID);
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
        /// Create a new thread for the form
        /// </summary>
        /// <param name="thread">The new thread</param>
        public void ThreadStart(System.Threading.Thread thread) {
            // Start the thread
            // Close the current form
            thread.Start();
            this.Close();
        }

        private void PopulateGrid() {
            // Create and assign the Movie DataTable
            // Assign the DataTable to the DataGridView
            DataTable dtable = Context.GetDataTable("Customer");
            dgvCustomers.DataSource = dtable;
        }

        #endregion
    }
}
