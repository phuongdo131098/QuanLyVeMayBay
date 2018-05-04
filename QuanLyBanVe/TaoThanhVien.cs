using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyBanVe
{
    public partial class TaoThanhVien : Form
    {
        public TaoThanhVien()
        {
            InitializeComponent();
            
        }

        private void btnTaoThanhVien_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Properties.Resources.localConnectionString_VietAnh;
                conn.Open();

                using (SqlCommand comm = new SqlCommand("TaoThanhVien", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;

                    SqlParameter para = new SqlParameter("@MaKH", txtMaKH.Text);
                    comm.Parameters.Add(para);

                    para = new SqlParameter("@HoTen", txtHoTen.Text);
                    comm.Parameters.Add(para);

                    para = new SqlParameter("@Tuoi", Int32.Parse(txtTuoi.Text));
                    comm.Parameters.Add(para);
                    bool b = false; ;
                    if (rbtnNam.Checked)
                        b = true;
                    para = new SqlParameter("@GioiTinh", b);
                    comm.Parameters.Add(para);

                    para = new SqlParameter("@CMND", txtCMND.Text);
                    comm.Parameters.Add(para);

                    para = new SqlParameter("@DiaChi", txtDiaChi.Text);
                    comm.Parameters.Add(para);

                    para = new SqlParameter("@SDT", txtSDT.Text);
                    comm.Parameters.Add(para);
                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Close();
        }
    }
}
