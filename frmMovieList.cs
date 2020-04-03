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
    public partial class frmMovieList : Form {
        #region Constructors

        public frmMovieList() {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddMovie_MouseClick(object sender, MouseEventArgs e) {
            frmMovie frm = new frmMovie();
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmMovieList_Paint(object sender, PaintEventArgs e) {
            BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmMovieList_Load(object sender, EventArgs e) {
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void dgvMovies_DoubleClick(object sender, EventArgs e) {
            // If no cell selected, do nothing
            if (dgvMovies.CurrentCell == null) {
                return;
            }

            // Primary key of selected cell
            long pkID = long.Parse(dgvMovies[0, dgvMovies.CurrentCell.RowIndex].Value.ToString());

            frmMovie frm = new frmMovie(pkID);
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Helper Method

        // Open the application
        public static void ThreadProcMenu() {
            Application.Run(new frmMenu());
        }
        // Put application in a new thread
        public void ThreadStart(System.Threading.Thread thread) {
            thread.Start();
            Close();
        }

        private void PopulateGrid() {
            DataTable dtable = new DataTable();
            dtable = Context.GetDataTable("Movie");
            dgvMovies.DataSource = dtable;
        }

        #endregion
    }
}
