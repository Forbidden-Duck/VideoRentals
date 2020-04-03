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

    public partial class frmRental : Form {
        #region Global Variables

        long _pkID = 0;
        DataTable _dtable = null, _cusTable = null;
        bool _isNew = false;
        bool customFormat = false;

        #endregion

        #region Constructors

        public frmRental() {
            InitializeComponent();
            InitializeNewForm();
        }

        public frmRental(long pkID) {
            InitializeComponent();
            InitializeExistingForm(pkID);
        }

        private void InitializeNewForm() {
            _isNew = true;
            disableButtons();
            InitializeDatatable();
            InitializeCustomerTable();
            BindControls();
        }

        private void InitializeExistingForm(long pkID) {
            _pkID = pkID;
            enableButtons();
            InitializeDatatable();
            InitializeCustomerTable();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            if (_dtable.Rows.Count > 0) {
                if (!dtpDateReturned.Text.Equals(" ")) {
                    _dtable.Rows[0]["DateReturned"] = dtpDateReturned.Value.ToString("yyyy-MM-dd");
                }
                _dtable.Rows[0].EndEdit();
                Context.SaveDataBaseTable(_dtable);
            }
        }

        private void BtnInsertItem_Click(object sender, EventArgs e) {
            frmRentalItem frm = new frmRentalItem(txtRentalD.Text);
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        private void BtnCreateRental_Click(object sender, EventArgs e) {
            if (_isNew && _pkID <= 0) {
                if (cboCustomer.SelectedIndex == -1) {
                    MessageBox.Show("No Customer has been selected!",
                        Properties.Settings.Default.ProjectName,
                        MessageBoxButtons.OK);
                    return;
                }

                string columnNames = "CustomerID, DateRented, DateReturned";

                string dateRented = dtpDateRented.Value.ToString("yyyy-MM-dd");
                long customerID = long.Parse(cboCustomer.SelectedValue.ToString());

                string columnValues = $"{customerID}, '{dateRented}', null";
                _pkID = Context.InsertParentTable("Rental", columnNames, columnValues);
                txtRentalD.Text = _pkID.ToString();

                InitializeDatatable();
                enableButtons();
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e) {
            DialogResult msgBox = MessageBox.Show("Do you want to delete this item?",
                Properties.Settings.Default.ProjectName, MessageBoxButtons.YesNo);
            if (msgBox == DialogResult.Yes) {
                try {
                    long pkID = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex].Value.ToString());

                    Context.DeleteRecord("RentalItem", "RentalItemID", pkID.ToString());
                    PopulateGrid();
                } catch (Exception) {
                    MessageBox.Show("Can't find record. No records affected",
                        Properties.Settings.Default.ProjectName,
                        MessageBoxButtons.OK);
                }
            }
        }

        #endregion

        #region DataGridView Events

        private void DgvRentalItems_DoubleClick(object sender, EventArgs e) {
            // If no cell selected, do nothing
            if (dgvRentalItems.CurrentCell == null) {
                return;
            }

            // Primary key of selected cell
            long pkID = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex].Value.ToString());

            frmRentalItem frm = new frmRentalItem(pkID);
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmRental_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region DateTimePicker Events

        private void DtpDateReturned_ValueChanged(object sender, EventArgs e) {
            if (customFormat == true) {
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = "dd-MMM-yyyy";
            } else if (dtpDateReturned.CustomFormat == " ") {
                customFormat = true;
            }
        }

        #endregion

        #region Helper Methods

        private void InitializeDatatable() {
            string sqlQuery = $"SELECT * FROM Rental WHERE RentalID = {_pkID}";
            _dtable = Context.GetDataTable(sqlQuery, "Rental");
        }

        private void InitializeCustomerTable() {
            string sqlQuery = $"SELECT CustomerID, CustomerName FROM Customer";
            _cusTable = Context.GetDataTable(sqlQuery, "Customer");
            _cusTable.Columns.Add("Display", typeof(string), "CustomerID + ' - ' + CustomerName");
            PopulateGrid();
        }

        private void BindControls() {
            txtRentalD.DataBindings.Add("Text", _dtable, "RentalID");
            dtpDateRented.DataBindings.Add("Text", _dtable, "DateRented");
            dtpDateReturned.DataBindings.Add("Text", _dtable, "DateReturned");

            // Binding the ComboBox
            cboCustomer.ValueMember = "CustomerID";
            cboCustomer.DisplayMember = "Display";
            cboCustomer.DataSource = _cusTable;
            cboCustomer.BindingContext = this.BindingContext;

            // Set Customer Index
            if (_isNew) {
                cboCustomer.SelectedIndex = -1;
            } else {
                cboCustomer.SelectedIndex = int.Parse(_dtable.Rows[0]["CustomerID"].ToString()) - 1;
            }

            // Set state of DateReturned
            if (_isNew || string.IsNullOrEmpty(_dtable.Rows[0]["DateReturned"].ToString())) {
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = " ";
                dtpDateReturned.Value = DateTime.Now.AddDays(1);
            }
        }

        private void PopulateGrid() {
            string sqlQuery =
                "SELECT RentalItem.RentalItemID, RentalItem.RentalID, Movie.MovieName " +
                "FROM RentalItem INNER JOIN " +
                "Movie ON RentalItem.MovieID = Movie.MovieID " +
                $"WHERE RentalID = {_pkID} " +
                "ORDER BY RentalItem.RentalItemID DESC";
            DataTable itemTable = Context.GetDataTable(sqlQuery, "RentalItem");
            dgvRentalItems.DataSource = itemTable;
        }

        private void enableButtons() {
            gbRentalItems.Enabled = true;
            btnCreateRental.Enabled = false;
        }

        private void disableButtons() {
            gbRentalItems.Enabled = false;
            btnCreateRental.Enabled = true;
        }

        #endregion
    }
}
