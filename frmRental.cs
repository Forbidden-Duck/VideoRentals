using SQLController;
using System;
using System.Data;
using System.Windows.Forms;

namespace Rentals {

    public partial class frmRental : Form {
        #region Global Variables

        // Create a variable for the Primary Key
        // Create a variable for the DataTable and Customer DataTable
        // Create a variable the New DataTable
        // Create a variable for checking the custom format
        long _pkID = 0;
        DataTable _dtable = null, _cusTable = null;
        bool _isNew = false;
        bool customFormat = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of frmRental
        /// If no primary key was provided
        /// </summary>
        public frmRental() {
            // Initialize the form components
            // Initialize a new form
            InitializeComponent();
            InitializeNewForm();
        }

        /// <summary>
        /// Create a new instance of frmRental
        /// If a primary key was provided
        /// </summary>
        /// <param name="pkID">The provided Primary Key</param>
        public frmRental(long pkID) {
            // Initialize the form components
            // Initialize an existing form (with the Primary Key)
            InitializeComponent();
            InitializeExistingForm(pkID);
        }

        /// <summary>
        /// Initializes a new form
        /// </summary>
        private void InitializeNewForm() {
            // Assign true to _isNew
            // Disable the buttons
            // Initialize the DataTable
            // Initialize the Customer DataTable
            // Bind data to the form components
            _isNew = true;
            disableButtons();
            InitializeDatatable();
            InitializeCustomerTable();
            BindControls();
        }

        /// <summary>
        /// Initializes an existing form
        /// </summary>
        /// <param name="pkID">The primary key</param>
        private void InitializeExistingForm(long pkID) {
            // Assign the Primary Key to the Global Variable
            // Enable the buttons
            // Initialize the DataTable
            // Initialize the Customer DataTable
            // Bind data to the form components
            _pkID = pkID;
            enableButtons();
            InitializeDatatable();
            InitializeCustomerTable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Close the form
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            // Check if the DataTable has rows
            if (_dtable.Rows.Count > 0) {
                // Check if the DataGridView doesn't equal " "
                // Assign the DateTimePicker DateReturned to the DataTable DateReturned
                if (!dtpDateReturned.Text.Equals(" ")) {
                    _dtable.Rows[0]["DateReturned"] = dtpDateReturned.Value.ToString("yyyy-MM-dd");
                }
                // Save the DataTable
                // Save the table
                _dtable.Rows[0].EndEdit();
                Context.SaveDataBaseTable(_dtable);
            }
        }

        private void BtnInsertItem_Click(object sender, EventArgs e) {
            // Create a new instance of frmRentalItem (with the RentalID)
            frmRentalItem frm = new frmRentalItem(txtRentalD.Text);
            // Show the form
            // If the form returns DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        private void BtnCreateRental_Click(object sender, EventArgs e) {
            // Check if _isNew is true and _pkID is less than or equal to 0
            if (_isNew && _pkID <= 0) {
                // Check if the ComboBox Selected Index is equal to -1
                // Show a MessageBox and stop the method
                if (cboCustomer.SelectedIndex == -1) {
                    MessageBox.Show("No Customer has been selected!",
                        Properties.Settings.Default.ProjectName,
                        MessageBoxButtons.OK);
                    return;
                }

                // Create and assign new column names
                // Create and assign the DateTimePicker DateRented
                // Create and assign the ComboBox CustomerID
                string columnNames = "CustomerID, DateRented, DateReturned";
                string dateRented = dtpDateRented.Value.ToString("yyyy-MM-dd");
                long customerID = long.Parse(cboCustomer.SelectedValue.ToString());

                // Create and assign the CustomerID, DateRented and DateReturned
                // Assign _pkID with the Insert Parent Record return value
                // Assign txtRentalID with the new Primary Key
                string columnValues = $"{customerID}, '{dateRented}', null";
                _pkID = Context.InsertParentTable("Rental", columnNames, columnValues);
                txtRentalD.Text = _pkID.ToString();

                // Initialize the DataTable
                // Enable the buttons
                InitializeDatatable();
                enableButtons();
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e) {
            // Check if the user wants to delete the item
            DialogResult msgBox = MessageBox.Show("Do you want to delete this item?",
                Properties.Settings.Default.ProjectName, MessageBoxButtons.YesNo);
            if (msgBox == DialogResult.Yes) {
                // Try to delete the record
                // Catch any errors
                try {
                    // Create and assign the DataGridView Primary Key
                    long pkID = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex].Value.ToString());

                    // Delete the record
                    // Populate the DataGridView
                    Context.DeleteRecord("RentalItem", "RentalItemID", pkID.ToString());
                    PopulateGrid();
                } catch (Exception) {
                    // Show a MessageBox
                    MessageBox.Show("Can't find record. No records affected",
                        Properties.Settings.Default.ProjectName,
                        MessageBoxButtons.OK);
                }
            }
        }

        #endregion

        #region DataGridView Events

        private void DgvRentalItems_DoubleClick(object sender, EventArgs e) {
            // Check if a cell has been selected in the DataGridView
            // Stop the method
            if (dgvRentalItems.CurrentCell == null) {
                return;
            }

            // Create and assign the DataGridView Primary Key
            long pkID = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex].Value.ToString());

            // Create a new instance of frmRentalItem (with the Primary Key)
            frmRentalItem frm = new frmRentalItem(pkID);
            // Show the form
            // If the form returns the DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmRental_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour with the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region DateTimePicker Events

        private void DtpDateReturned_ValueChanged(object sender, EventArgs e) {
            // Check if customer form is true
            // Else if the DataTimePicker custom format is equal to " "
            if (customFormat == true) {
                // Assign the DataTimePicker Format to custom
                // Assign the custom format
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = "dd-MMM-yyyy";
            } else if (dtpDateReturned.CustomFormat == " ") {
                // Assign true to the custom format
                customFormat = true;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initialize the DataTable
        /// </summary>
        private void InitializeDatatable() {
            // Create and assign a new SQL Query
            // Assign the DataTable with the Rental DataTable
            string sqlQuery = $"SELECT * FROM Rental WHERE RentalID = {_pkID}";
            _dtable = Context.GetDataTable(sqlQuery, "Rental");
        }

        /// <summary>
        /// Initialize the Customer DataTable
        /// </summary>
        private void InitializeCustomerTable() {
            // Create and assign a new SQL Query
            string sqlQuery = $"SELECT CustomerID, CustomerName FROM Customer";

            // Assign the Customer DataTable with the Customer DataTable
            // Create a new column with the CustomerID and 
            _cusTable = Context.GetDataTable(sqlQuery, "Customer");
            _cusTable.Columns.Add("Display", typeof(string), "CustomerID + ' - ' + CustomerName");

            // Populate the DataGridView
            PopulateGrid();
        }

        /// <summary>
        /// Bind data to the form components
        /// </summary>
        private void BindControls() {
            // Bind txtRentalID with RentalID
            // Bind dtpDateRented with DataRented
            // Bind dtpDateReturned with DateReturned
            txtRentalD.DataBindings.Add("Text", _dtable, "RentalID");
            dtpDateRented.DataBindings.Add("Text", _dtable, "DateRented");
            dtpDateReturned.DataBindings.Add("Text", _dtable, "DateReturned");

            // Bind the ValueMember with CustomerID
            // Bind the DisplayMember with the column Display
            // Bind the cboCustomer with the Customer DataTable
            // Assign the BindingContext with the BindingContext
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.DisplayMember = "Display";
            cboCustomer.DataSource = _cusTable;
            cboCustomer.BindingContext = this.BindingContext;

            // Check if _isNew is true
            // Set the Customer SelectedIndex with -1
            // Else set the Customer SelectedIndex with the DataTable CustomerID (Subtract 1)
            if (_isNew) {
                cboCustomer.SelectedIndex = -1;
            } else {
                cboCustomer.SelectedIndex = int.Parse(_dtable.Rows[0]["CustomerID"].ToString()) - 1;
            }

            // Check if _isNew is true or DataTable DateReturned is null or empty
            if (_isNew || string.IsNullOrEmpty(_dtable.Rows[0]["DateReturned"].ToString())) {
                // Set the DateTimePicker Format to custom
                // Set the custom format
                // Set the value to NOW (Add 1 day)
                /*
                 * NOTE:
                 * I do NOW + 1 because if the user selects NOW it doesn't trigger
                 * the ValueChanged Event on the DataTimePicker as it's already selected
                */
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = " ";
                dtpDateReturned.Value = DateTime.Now.AddDays(1);
            }
        }

        /// <summary>
        /// Populate the DataGridView
        /// </summary>
        private void PopulateGrid() {
            // Create and assign a new SQL Query
            string sqlQuery =
                "SELECT RentalItem.RentalItemID, RentalItem.RentalID, Movie.MovieName " +
                "FROM RentalItem INNER JOIN " +
                "Movie ON RentalItem.MovieID = Movie.MovieID " +
                $"WHERE RentalID = {_pkID} " +
                "ORDER BY RentalItem.RentalItemID DESC";

            // Create and assign Item DataTable with RentalItem DataTable
            // Assign the DataGridView with the Item DataTable
            DataTable itemTable = Context.GetDataTable(sqlQuery, "RentalItem");
            dgvRentalItems.DataSource = itemTable;
        }

        /// <summary>
        /// Enables the GroupBox Buttons
        /// </summary>
        private void enableButtons() {
            // Set the GroupBox Enable to true
            // Set the CreateRental Button Enable to false
            gbRentalItems.Enabled = true;
            btnCreateRental.Enabled = false;
        }

        /// <summary>
        /// Disables the Group Box Buttons
        /// </summary>
        private void disableButtons() {
            // Set the GroupBox Enable to false
            // Set the CreateRental Button Enable to true
            gbRentalItems.Enabled = false;
            btnCreateRental.Enabled = true;
        }

        #endregion
    }
}
