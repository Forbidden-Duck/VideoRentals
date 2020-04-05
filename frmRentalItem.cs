using SQLController;
using System;
using System.Data;
using System.Windows.Forms;

namespace Rentals {
    public partial class frmRentalItem : Form {
        #region Global Variables

        // Create a variable for the Primary Key
        // Create a variable for the Rental Primary Key
        // Create a variable for the DataTable and Movie DataTable
        // Create a variable for the New DataTable
        long _pkID = 0;
        long _rentalID = 0;
        DataTable _dtable = null, _movTable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of frmRentalItem
        /// If a primary key was provided
        /// </summary>
        /// <param name="pkID"></param>
        public frmRentalItem(long pkID) {
            // Initialize the form components
            // Assign the Primary Key to the Global Variable
            // Initialize the form
            InitializeComponent();
            _pkID = pkID;
            InitializeForm();
        }

        /// <summary>
        /// Create a new instance of frmRentalItem
        /// If a Rental Primary Key was provided
        /// </summary>
        /// <param name="rentalID"></param>
        public frmRentalItem(string rentalID) {
            // Initialize the form components
            // Assign true to _isNew
            // Assign the Rental Primary Key to the Global Variable
            // Initialize the form
            InitializeComponent();
            _isNew = true;
            _rentalID = long.Parse(rentalID);
            InitializeForm();
        }

        /// <summary>
        /// Initialize the form
        /// </summary>
        private void InitializeForm() {
            // Initialize the DataTable
            // Initialize the Movie DataTable
            // Bind data to the form components
            InitializeDatatable();
            InitializeMovieTable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void BtnClose_Click(object sender, EventArgs e) {
            // Close the form
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            // Check if a cell has been selected in the DataGridView
            // If not show a MessageBox and stop the method
            if (cboMovies.SelectedIndex == -1) {
                MessageBox.Show("No movie has been selected!",
                    Properties.Settings.Default.ProjectName,
                    MessageBoxButtons.OK);

                return;
            }

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
        private void frmRentalItem_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour with the ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region ComboBox Events

        private void CboMovies_SelectedIndexChanged(object sender, EventArgs e) {
            // Check if the Movies SelectedIndex is greater than 0
            // Assign the Movies SelectedValue to the DataTable MovieID
            if (cboMovies.SelectedIndex > 0) {
                _dtable.Rows[0]["MovieID"] = cboMovies.SelectedValue;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initialize the DataTable
        /// </summary>
        private void InitializeDatatable() {
            // Create and assign the SQL Query
            string sqlQuery = $"SELECT * FROM RentalItem WHERE RentalItemID='{_pkID}'";

            // Assign the DataTable with the RentalItem DataTable
            _dtable = Context.GetDataTable(sqlQuery, "RentalItem");

            // Check if _isNew is true
            if (_isNew) {
                // Create and assign a new DataRow
                // Assign the empty row to the DataTable
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Initialize the Movie DataTable
        /// </summary>
        private void InitializeMovieTable() {
            // Create and assign a new SQL Query
            string sqlQuery = $"SELECT MovieID, MovieName FROM Movie";

            // Assign the Movie DataTable with the Movie DataTable
            // Create a new column with the MovieID and MovieName
            _movTable = Context.GetDataTable(sqlQuery, "Movie");
            _movTable.Columns.Add("Display", typeof(string), "MovieID + ' - ' + MovieName");
        }

        /// <summary>
        /// Bind data to the form components
        /// </summary>
        private void BindControls() {
            // Check if the Rental Primary Key is equal to 0
            // Assign the DataTable RentalID to the Rental Primary Key
            // Else assign the Rental Primary Key to the DataTable RentalID
            if (_rentalID == 0) {
                _rentalID = long.Parse(_dtable.Rows[0]["RentalID"].ToString());
            } else {
                _dtable.Rows[0]["RentalID"] = _rentalID;
            }

            // Bind txtRentalID with RentalID
            txtRentalD.DataBindings.Add("Text", _dtable, "RentalID");

            // Bind the ValueMember with the MovieID
            // Bind the DisplayMember with the column Display
            // Bind cboMovies with the Movie DataTable
            // Bind the BindContext with the BindingContext
            cboMovies.ValueMember = "MovieID";
            cboMovies.DisplayMember = "Display";
            cboMovies.DataSource = _movTable;
            cboMovies.BindingContext = this.BindingContext;

            // Check if _isNew is true
            // Set the Movies SelectedIndex to -1
            // Else set the Movies SelectedIndex to the DataTable MovieID (Subtract 1)
            if (_isNew) {
                cboMovies.SelectedIndex = -1;
            } else {
                cboMovies.SelectedIndex = int.Parse(_dtable.Rows[0]["MovieID"].ToString()) - 1;
            }
        }

        #endregion
    }
}
