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
    public partial class frmRentalItem : Form {
        #region Global Variables

        long _pkID = 0;
        long _rentalID = 0;
        DataTable _dtable = null, _movTable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        public frmRentalItem(long pkID) {
            InitializeComponent();
            _pkID = pkID;
            InitializeForm();
        }

        public frmRentalItem(string rentalID) {
            InitializeComponent();
            _isNew = true;
            _rentalID = long.Parse(rentalID);
            InitializeForm();
        }

        private void InitializeForm() {
            InitializeDatatable();
            InitializeMovieTable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void BtnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            // Check if selected index
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
            _dtable.Rows[0].EndEdit();

            // Call the Contexnt Save Method
            Context.SaveDataBaseTable(_dtable);
        }

        #endregion

        #region Form Events
        private void frmRentalItem_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region ComboBox Events

        private void CboMovies_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboMovies.SelectedIndex > 0) {
                _dtable.Rows[0]["MovieID"] = cboMovies.SelectedValue;
            }
        }

        #endregion

        #region Helper Methods

        private void InitializeDatatable() {
            string sqlQuery = $"SELECT * FROM RentalItem WHERE RentalItemID='{_pkID}'";
            _dtable = Context.GetDataTable(sqlQuery, "RentalItem");
            if (_isNew) {
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        private void InitializeMovieTable() {
            string sqlQuery = $"SELECT MovieID, MovieName FROM Movie";
            _movTable = Context.GetDataTable(sqlQuery, "Movie");
            _movTable.Columns.Add("Display", typeof(string), "MovieID + ' - ' + MovieName");
        }

        private void BindControls() {
            // Set RentalID
            if (_rentalID == 0) {
                _rentalID = long.Parse(_dtable.Rows[0]["RentalID"].ToString());
            } else {
                _dtable.Rows[0]["RentalID"] = _rentalID;
            }

            // Bind RentalID
            txtRentalD.DataBindings.Add("Text", _dtable, "RentalID");

            // Binding the ComboBox
            cboMovies.ValueMember = "MovieID";
            cboMovies.DisplayMember = "Display";
            cboMovies.DataSource = _movTable;
            cboMovies.BindingContext = this.BindingContext;

            // Set Movie Index
            if (_isNew) {
                cboMovies.SelectedIndex = -1;
            } else {
                cboMovies.SelectedIndex = int.Parse(_dtable.Rows[0]["MovieID"].ToString()) - 1;
            }
        }

        #endregion
    }
}
