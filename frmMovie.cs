using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLController;

namespace Rentals {
    public partial class frmMovie : Form {
        #region Global Variables

        long _pkID = 0;
        DataTable _dtable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        public frmMovie() {
            InitializeComponent();
            _isNew = true;
            InitializeForm();
        }
        public frmMovie(long pkID) {
            InitializeComponent();
            _pkID = pkID;
            InitializeForm();
        }

        private void InitializeForm() {
            InitializeDatatable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            /*
             * NOTE:
             * Make sure to end with EndEdit
             * Before saving the DataTable
             */
            _dtable.Rows[0].EndEdit();

            // Call the Contexnt Save Method
            Context.SaveDataBaseTable(_dtable);
        }

        #endregion

        #region Form Events

        private void frmMovie_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initalizes the Data Table
        /// </summary>
        private void InitializeDatatable() {
            string sqlQuery = $"SELECT * FROM Movie WHERE MovieID = {_pkID}";
            _dtable = Context.GetDataTable(sqlQuery, "Movie");

            if (_isNew) {
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Binding textboxes with the Data Table
        /// </summary>
        private void BindControls() {
            txtMovieID.DataBindings.Add("Text", _dtable, "MovieID");
            txtMovieName.DataBindings.Add("Text", _dtable, "MovieName");
        }

        #endregion
    }
}
