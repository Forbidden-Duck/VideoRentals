namespace Rentals {
    partial class frmMovieList {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.inkAddMovie = new System.Windows.Forms.LinkLabel();
            this.lblMovieList = new System.Windows.Forms.Label();
            this.dgvMovies = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
            this.SuspendLayout();
            // 
            // inkAddMovie
            // 
            this.inkAddMovie.AutoSize = true;
            this.inkAddMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inkAddMovie.Location = new System.Drawing.Point(13, 50);
            this.inkAddMovie.Name = "inkAddMovie";
            this.inkAddMovie.Size = new System.Drawing.Size(158, 24);
            this.inkAddMovie.TabIndex = 0;
            this.inkAddMovie.TabStop = true;
            this.inkAddMovie.Text = "Add New Movie";
            this.inkAddMovie.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inkAddMovie_MouseClick);
            // 
            // lblMovieList
            // 
            this.lblMovieList.AutoSize = true;
            this.lblMovieList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovieList.Location = new System.Drawing.Point(12, 9);
            this.lblMovieList.Name = "lblMovieList";
            this.lblMovieList.Size = new System.Drawing.Size(120, 25);
            this.lblMovieList.TabIndex = 1;
            this.lblMovieList.Text = "Movie List";
            // 
            // dgvMovies
            // 
            this.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovies.Location = new System.Drawing.Point(17, 82);
            this.dgvMovies.Name = "dgvMovies";
            this.dgvMovies.Size = new System.Drawing.Size(345, 322);
            this.dgvMovies.TabIndex = 2;
            this.dgvMovies.DoubleClick += new System.EventHandler(this.dgvMovies_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(287, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseClick);
            // 
            // frmMovieList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvMovies);
            this.Controls.Add(this.lblMovieList);
            this.Controls.Add(this.inkAddMovie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMovieList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie List";
            this.Load += new System.EventHandler(this.frmMovieList_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMovieList_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel inkAddMovie;
        private System.Windows.Forms.Label lblMovieList;
        private System.Windows.Forms.DataGridView dgvMovies;
        private System.Windows.Forms.Button btnClose;
    }
}