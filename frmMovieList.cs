using System;
using System.Data;
using System.Windows.Forms;
using SQLController;

namespace Rentals {
    public partial class frmMovieList : Form {
        #region Constructors

        /// <summary>
        /// Initialize the frame
        /// </summary>
        public frmMovieList() {
            // Initialize the frame components
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_MouseClick(object sender, MouseEventArgs e) {
            // Start a new thread frmMenu
            ThreadStart(new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProcMenu)));
        }
        private void inkAddMovie_MouseClick(object sender, MouseEventArgs e) {
            // Initialize a new frmMovie
            frmMovie frm = new frmMovie();
            // Show the new form
            // If the form returns the DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Form Events

        private void frmMovieList_Paint(object sender, PaintEventArgs e) {
            // Change the frame background colour to the ColorTheme in the project settings
            BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmMovieList_Load(object sender, EventArgs e) {
            // Populate the DataGridView
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void dgvMovies_DoubleClick(object sender, EventArgs e) {
            // Check if a cell has been selected in the DataGridView
            // If not stop the method
            if (dgvMovies.CurrentCell == null) {
                return;
            }

            // Create and assign the primary key of the DataGridView
            long pkID = long.Parse(dgvMovies[0, dgvMovies.CurrentCell.RowIndex].Value.ToString());

            // Create a new instance of frmMovie (with the Primary Key)
            frmMovie frm = new frmMovie(pkID);
            // Show the form
            // If the form returns DialogResult OK
            // Populate the DataGridView
            if (frm.ShowDialog() == DialogResult.OK) {
                PopulateGrid();
            }
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// Create a new Form
        /// </summary>
        public static void ThreadProcMenu() {
            Application.Run(new frmMenu());
        }
        /// <summary>
        /// Starts the new thread
        /// </summary>
        /// <param name="thread">The new thread</param>
        public void ThreadStart(System.Threading.Thread thread) {
            // Start the thread
            // Close the current frame
            thread.Start();
            Close();
        }

        /// <summary>
        /// Populate the DataGridView
        /// </summary>
        private void PopulateGrid() {
            // Create and assign a new Movie DataTable
            // Assign the DataGridView with the new DataTable
            DataTable dtable = Context.GetDataTable("Movie");
            dgvMovies.DataSource = dtable;
        }

        #endregion
    }
}
