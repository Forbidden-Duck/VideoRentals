using SQLController;
using System;
using System.Data;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmCustomer : Form {
        #region Global Variables

        // Create a variable for the Primary Key
        // Create a variable for the DataTable
        // Create a variable for the New DataTable
        long _pkID = 0;
        DataTable _dtable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of frmCustomer
        /// If no primary key is provided
        /// </summary>
        public frmCustomer() {
            // Initialize the form components
            // Assign true to _isNew
            // Intialize the form
            InitializeComponent();
            _isNew = true;
            InitializeForm();
        }
        
        /// <summary>
        /// Create a new instance of frmCustomer
        /// If a primary key was provided
        /// </summary>
        /// <param name="pkID"></param>
        public frmCustomer(long pkID) {
            // Initialize the form components
            // Assign the Primary Key to the Global Variable
            // Initialize the form
            InitializeComponent();
            _pkID = pkID;
            InitializeForm();
        }

        /// <summary>
        /// Initializes the form
        /// </summary>
        private void InitializeForm() {
            // Initialize the DataTable
            // Bind data to the form components
            InitializeDatatable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Close the form
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            /*
             * NOTE:
             * Make sure to end with EndEdit
             * Before saving the DataTable
             */
             // Save the DataTable
            _dtable.Rows[0].EndEdit();

            // Save the table
            Context.SaveDataBaseTable(_dtable);
        }

        #endregion

        #region Form Events

        private void frmCustomer_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour with the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initializes the DataTable
        /// </summary>
        private void InitializeDatatable() {
            // Create and assign a new SQL Query
            // Assign the DataTable with the Customer DataTable
            string sqlQuery = $"SELECT * FROM Customer WHERE CustomeriD='{_pkID}'";
            _dtable = Context.GetDataTable(sqlQuery, "Customer");

            // Check if _isNew is true
            if (_isNew) {
                // Create and assign a new DataRow
                // Assign an empty row to the DataTable
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Bind data to the form components
        /// </summary>
        private void BindControls() {
            // Bind txtCustomerID with CustomerID
            // Bind txtCustomerName with CustomerName
            // Bind txtCustomerPhone with CustomerPhone
            txtCustomerID.DataBindings.Add("Text", _dtable, "CustomerID");
            txtCustomerName.DataBindings.Add("Text", _dtable, "CustomerName");
            txtCustomerPhone.DataBindings.Add("Text", _dtable, "CustomerPhone");
        }

        #endregion
    }
}
