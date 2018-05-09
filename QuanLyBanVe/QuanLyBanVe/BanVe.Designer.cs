namespace QuanLyBanVe
{
    partial class BanVe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.grpBanVe = new System.Windows.Forms.GroupBox();
            this.dgvVe = new System.Windows.Forms.DataGridView();
            this.chonMua = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cboHangVe = new System.Windows.Forms.ComboBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuyBo = new System.Windows.Forms.Button();
            this.lblCMND = new System.Windows.Forms.Label();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.grpBanVe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCMND
            // 
            this.txtCMND.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtCMND.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.txtCMND.Location = new System.Drawing.Point(318, 380);
            this.txtCMND.MaximumSize = new System.Drawing.Size(1000, 800);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.Size = new System.Drawing.Size(261, 20);
            this.txtCMND.TabIndex = 3;
            this.txtCMND.UseWaitCursor = true;
            // 
            // grpBanVe
            // 
            this.grpBanVe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBanVe.Controls.Add(this.dgvVe);
            this.grpBanVe.Controls.Add(this.cboHangVe);
            this.grpBanVe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBanVe.Location = new System.Drawing.Point(3, 4);
            this.grpBanVe.Name = "grpBanVe";
            this.grpBanVe.Size = new System.Drawing.Size(861, 357);
            this.grpBanVe.TabIndex = 1;
            this.grpBanVe.TabStop = false;
            this.grpBanVe.Text = "Bán vé";
            // 
            // dgvVe
            // 
            this.dgvVe.AllowUserToAddRows = false;
            this.dgvVe.AllowUserToDeleteRows = false;
            this.dgvVe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVe.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvVe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chonMua});
            this.dgvVe.Location = new System.Drawing.Point(6, 46);
            this.dgvVe.Name = "dgvVe";
            this.dgvVe.Size = new System.Drawing.Size(849, 305);
            this.dgvVe.TabIndex = 1;
            // 
            // chonMua
            // 
            this.chonMua.Frozen = true;
            this.chonMua.HeaderText = "ChonMua";
            this.chonMua.Name = "chonMua";
            this.chonMua.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cboHangVe
            // 
            this.cboHangVe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHangVe.FormattingEnabled = true;
            this.cboHangVe.Items.AddRange(new object[] {
            "Hạng 1",
            "Hạng 2"});
            this.cboHangVe.Location = new System.Drawing.Point(6, 19);
            this.cboHangVe.Name = "cboHangVe";
            this.cboHangVe.Size = new System.Drawing.Size(121, 21);
            this.cboHangVe.TabIndex = 0;
            this.cboHangVe.Text = "Hạng vé";
            this.cboHangVe.SelectedIndexChanged += new System.EventHandler(this.cboHangVe_SelectedIndexChanged);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhan.Location = new System.Drawing.Point(635, 421);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(108, 32);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận đặt mua";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuyBo.Location = new System.Drawing.Point(750, 421);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(108, 32);
            this.btnHuyBo.TabIndex = 3;
            this.btnHuyBo.Text = "Hủy bỏ";
            this.btnHuyBo.UseVisualStyleBackColor = true;
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // lblCMND
            // 
            this.lblCMND.AutoSize = true;
            this.lblCMND.Location = new System.Drawing.Point(315, 364);
            this.lblCMND.Name = "lblCMND";
            this.lblCMND.Size = new System.Drawing.Size(42, 13);
            this.lblCMND.TabIndex = 4;
            this.lblCMND.Text = "CMND:";
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.Location = new System.Drawing.Point(242, 419);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Size = new System.Drawing.Size(108, 32);
            this.btnKiemTra.TabIndex = 5;
            this.btnKiemTra.Text = "Kiểm tra thành viên";
            this.btnKiemTra.UseVisualStyleBackColor = true;
            this.btnKiemTra.Click += new System.EventHandler(this.btnKiemTra_Click);
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(6, 364);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(42, 13);
            this.lblHoTen.TabIndex = 6;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(9, 380);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(261, 20);
            this.txtHoTen.TabIndex = 7;
            // 
            // BanVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 465);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.btnKiemTra);
            this.Controls.Add(this.lblCMND);
            this.Controls.Add(this.txtCMND);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.grpBanVe);
            this.Name = "BanVe";
            this.Text = "Bán vé";
            this.Load += new System.EventHandler(this.BanVe_Load);
            this.grpBanVe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpBanVe;
        private System.Windows.Forms.ComboBox cboHangVe;
        private System.Windows.Forms.DataGridView dgvVe;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chonMua;
    }
}