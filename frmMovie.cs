using System;
using System.Data;
using System.Windows.Forms;
using SQLController;

namespace Rentals {
    public partial class frmMovie : Form {
        #region Global Variables

        // Create a variable for the Primary Key
        // Create a variable for the DataTable
        // Create a variable for a New DataTable
        long _pkID = 0;
        DataTable _dtable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        /// <summary>
        /// A new instance of frmMovie
        /// If no Primary Key was provided
        /// </summary>
        public frmMovie() {
            // Initialize the frame components
            // Assign true to _isNew
            // Initialize the form contents
            InitializeComponent();
            _isNew = true;
            InitializeForm();
        }
        /// <summary>
        /// Create a new instance of frmMovie
        /// If the Primary Key was provided
        /// </summary>
        /// <param name="pkID">The provided Primary Key</param>
        public frmMovie(long pkID) {
            // Initialize the frame components
            // Assign the Primary Key to the Global Variable
            // Initalize the Form
            InitializeComponent();
            _pkID = pkID;
            InitializeForm();
        }

        /// <summary>
        /// Initializes the Form
        /// </summary>
        private void InitializeForm() {
            // Initializes the DataTable
            // Binds the frame components with data
            InitializeDatatable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Close the form
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
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

        private void frmMovie_Paint(object sender, PaintEventArgs e) {
            // Assign the form background colour with ColorTheme from the project settings
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initalizes the Data Table
        /// </summary>
        private void InitializeDatatable() {
            // Create and assign a new SQL Query
            // Assign the DataTable with the Movie DataTable
            string sqlQuery = $"SELECT * FROM Movie WHERE MovieID = {_pkID}";
            _dtable = Context.GetDataTable(sqlQuery, "Movie");

            // Check if _isNew is true
            if (_isNew) {
                // Create and assign a new DataRow
                // Assign an empty row to the DataTable
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Binding textboxes with the Data Table
        /// </summary>
        private void BindControls() {
            // Bind txtMovieID with the MovieID
            // Bind txtMovieName with the MovieName
            txtMovieID.DataBindings.Add("Text", _dtable, "MovieID");
            txtMovieName.DataBindings.Add("Text", _dtable, "MovieName");
        }

        #endregion
    }
}
