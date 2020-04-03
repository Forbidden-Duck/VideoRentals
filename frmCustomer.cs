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
    public partial class frmCustomer : Form {
        #region Global Variables

        long _pkID = 0;
        DataTable _dtable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        public frmCustomer() {
            InitializeComponent();
            _isNew = true;
            InitializeForm();
        }

        public frmCustomer(long pkID) {
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

        private void BtnSave_Click(object sender, EventArgs e) {
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

        private void frmCustomer_Paint(object sender, PaintEventArgs e) {
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Helper Methods

        private void InitializeDatatable() {
            string sqlQuery = $"SELECT * FROM Customer WHERE CustomeriD='{_pkID}'";
            _dtable = Context.GetDataTable(sqlQuery, "Customer");

            if (_isNew) {
                DataRow row = _dtable.NewRow();
                _dtable.Rows.Add(row);
            }
        }

        private void BindControls() {
            txtCustomerID.DataBindings.Add("Text", _dtable, "CustomerID");
            txtCustomerName.DataBindings.Add("Text", _dtable, "CustomerName");
            txtCustomerPhone.DataBindings.Add("Text", _dtable, "CustomerPhone");
        }

        #endregion
    }
}
