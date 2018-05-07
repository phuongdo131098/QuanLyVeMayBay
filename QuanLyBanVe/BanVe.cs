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
    public partial class BanVe : Form
    {
        public BanVe()
        {
            InitializeComponent();
            
        }

        private DataTable getVe()
        {
            SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("Select * From VE", con);

            DataTable dt = new DataTable();

            da.Fill(dt);
            con.Close();

            return dt;            
        }

        private DataTable getVe(string MaHV)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu);
            con.Open();

            string SQL = string.Format("Select * From VE Where MAHV = '{0}'", MaHV);
                        
            SqlDataAdapter da = new SqlDataAdapter(SQL, con);

            DataTable dt = new DataTable();

            da.Fill(dt);
            con.Close();

            return dt;
        }

       

        private void BanVe_Load(object sender, EventArgs e)
        {
            dgvVe.DataSource = getVe();
        }

        private void cboHangVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboHangVe.SelectedIndex == 0)
                {
                    dgvVe.DataSource = getVe("HV001");

                    int demVeDaBan = 0;

                    for (int i = 0; i < dgvVe.Rows.Count; ++i)
                    {
                        if (dgvVe["MATT", i].Value.ToString() == "TT000")
                            break;
                        else
                            demVeDaBan++;
                    }

                    if (demVeDaBan == dgvVe.Rows.Count)
                    {
                        MessageBox.Show("Tất cả các vé hạng 1 đã được bán hết.", "Thông báo", MessageBoxButtons.OK);
                        dgvVe.DataSource = getVe();
                    }
                }
                else if (cboHangVe.SelectedIndex == 1)
                {
                    dgvVe.DataSource = getVe("HV002");

                    int demVeDaBan = 0;

                    for (int i = 0; i < dgvVe.Rows.Count; i++)
                    {
                        if ("TT001" == dgvVe["MATT", i].Value.ToString())
                            demVeDaBan++;
                        else
                            break;
                    }

                    if (demVeDaBan == dgvVe.Rows.Count)
                    {
                        MessageBox.Show("Tất cả các vé hạng 2 đã được bán hết.", "Thông báo", MessageBoxButtons.OK);
                        dgvVe.DataSource = getVe();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString());
            }
        }
        
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtCMND.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin của khách hàng !", "Nhắc nhở", MessageBoxButtons.OK);
            }            
            else
            {
                DialogResult dialogResult = MessageBox.Show("Vui lòng kiểm tra thông tin của khách đã đúng hay chưa.", "Nhắc nhở", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)                
                {

                    using (SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu))
                    { con.Open();
                        string sql = string.Format("Select * From KHACHHANG Where CMND = '{0}'", txtCMND.Text);

                        SqlDataAdapter da = new SqlDataAdapter(sql, con);

                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        
                        int demVe = 0;

                        for (int i = 0; i < dgvVe.Rows.Count; ++i)
                        {
                            if (dgvVe[0, i].Selected)
                            {
                                // Kiểm tra vé đã bán hay chưa
                                if (dgvVe["MATT", i].Value.ToString() == "TT001")
                                {
                                    MessageBox.Show("Vé này đã được bán. Hãy chọn lại một vé khác.", "Thông báo", MessageBoxButtons.OK);
                                    
                                }
                                else
                                {
                                    DataRow KH = dt.Rows[dt.Rows.Count - 1];                                    

                                    // Cập nhật vé đã bán
                                    SqlCommand cmd = new SqlCommand("UpdateTinhTrangVe", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@MaTT", "TT001");
                                    cmd.Parameters.AddWithValue("@MaVe", dgvVe["MAVE", i].Value.ToString());

                                    cmd.ExecuteNonQuery();

                                    // Cập nhật vé đã bán cho khách hàng nào
                                    cmd = new SqlCommand("TaoPhieuDatMua", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@MaVe", dgvVe[1, i].Value.ToString());
                                    cmd.Parameters.AddWithValue("@MaKH", KH["MAKH"].ToString());
                                    cmd.Parameters.AddWithValue("@ThoiGianDat", DateTime.Now);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Bán vé thành công !", "Thông báo", MessageBoxButtons.OK);
                                    dgvVe.DataSource = getVe();

                                    demVe++;
                                }
                            }                          
                        }

                        if (demVe == 0)
                            MessageBox.Show("Không có vé nào được chọn. Vui lòng chọn 01 vé.", "Cảnh báo", MessageBoxButtons.OK);
                        
                        con.Close();
                    }                    
                }
            }
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu))
            {
                con.Open();
                string sql = string.Format("Select * From KHACHHANG Where CMND = '{0}' and HoTen = N'{1}'", txtCMND.Text, txtHoTen.Text);

                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                DataTable dt = new DataTable();

                da.Fill(dt);
              
                // Kiểm tra khách hàng đã là thành viên hay chưa
                if (dt.Rows.Count != 0)
                    MessageBox.Show("Đã là thành viên", "Kết quả kiểm tra", MessageBoxButtons.OK);
                else
                {
                    TaoThanhVien ttv = new TaoThanhVien();
                    ttv.ShowDialog();
                }

                con.Close();
            }
        }
    }
}
