namespace Rentals {
    partial class frmRentalList {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvRentals = new System.Windows.Forms.DataGridView();
            this.lblRentalList = new System.Windows.Forms.Label();
            this.inkAddRental = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(287, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseClick);
            // 
            // dgvRentals
            // 
            this.dgvRentals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRentals.Location = new System.Drawing.Point(17, 82);
            this.dgvRentals.Name = "dgvRentals";
            this.dgvRentals.Size = new System.Drawing.Size(345, 322);
            this.dgvRentals.TabIndex = 10;
            this.dgvRentals.DoubleClick += new System.EventHandler(this.DgvRentals_DoubleClick);
            // 
            // lblRentalList
            // 
            this.lblRentalList.AutoSize = true;
            this.lblRentalList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentalList.Location = new System.Drawing.Point(12, 9);
            this.lblRentalList.Name = "lblRentalList";
            this.lblRentalList.Size = new System.Drawing.Size(125, 25);
            this.lblRentalList.TabIndex = 9;
            this.lblRentalList.Text = "Rental List";
            // 
            // inkAddRental
            // 
            this.inkAddRental.AutoSize = true;
            this.inkAddRental.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inkAddRental.Location = new System.Drawing.Point(13, 50);
            this.inkAddRental.Name = "inkAddRental";
            this.inkAddRental.Size = new System.Drawing.Size(161, 24);
            this.inkAddRental.TabIndex = 8;
            this.inkAddRental.TabStop = true;
            this.inkAddRental.Text = "Add New Rental";
            this.inkAddRental.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inkAddRental_MouseClick);
            // 
            // frmRentalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvRentals);
            this.Controls.Add(this.lblRentalList);
            this.Controls.Add(this.inkAddRental);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRentalList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rental List";
            this.Load += new System.EventHandler(this.frmRentalList_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRentalList_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvRentals;
        private System.Windows.Forms.Label lblRentalList;
        private System.Windows.Forms.LinkLabel inkAddRental;
    }
}