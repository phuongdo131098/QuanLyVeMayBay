using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class DangNhap : Form
    {
        private DialogResult loginResult;

        public DialogResult LoginResult
        {
            get
            {
                return loginResult;
            }
        }

        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "admin" && txtMatKhau.Text == "admin")
            {
                this.loginResult = DialogResult.OK;
                MessageBox.Show("Đăng nhập thành công!");
                Close();
            }
            else
            {
                this.loginResult = DialogResult.Retry;
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                txtTenDangNhap.Focus();
                txtMatKhau.Clear();
            }
        }
    }
}