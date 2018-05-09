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
        private string maCB;

        public BanVe(string MaCB)
        {
            InitializeComponent();
            this.maCB = MaCB;
        }

        private DataTable getVe()
        {
            SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu);
            con.Open();

            SqlCommand comm = new SqlCommand("LietKeVe", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@MaCB", this.maCB);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comm;
            DataTable datb = new DataTable();
            adapter.Fill(datb);
            return datb;            
        }

        private DataTable getVe(string TenHV)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.localConnectionString_CamTu);
            con.Open();

            SqlCommand comm = new SqlCommand("ChonHangVe", con);
            comm.CommandType = CommandType.StoredProcedure;

            SqlParameter para = new SqlParameter("@TenHangVe", TenHV);
            comm.Parameters.Add(para);

            para = new SqlParameter("@MaCB", maCB);
            comm.Parameters.Add(para);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comm;

            DataTable dt = new DataTable();

            adapter.Fill(dt);
            con.Close();

            return dt;
        }

        private void BanVe_Load(object sender, EventArgs e)
        {
            dgvVe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvVe.DataSource = getVe();
        }

        private void cboHangVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboHangVe.SelectedIndex == 0)
                {
                    dgvVe.DataSource = getVe("Hạng 1");

                    int demVeDaBan = 0;

                    for (int i = 0; i < dgvVe.Rows.Count; ++i)
                    {
                        if (dgvVe["TÌNH TRẠNG", i].Value.ToString() == "Còn trống")
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
                    dgvVe.DataSource = getVe("Hạng 2");

                    int demVeDaBan = 0;

                    for (int i = 0; i < dgvVe.Rows.Count; i++)
                    {
                        if (dgvVe["TÌNH TRẠNG", i].Value.ToString() == "Còn trống")
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
                    {
                        con.Open();
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
                    MessageBox.Show("Chưa là thành viên! Vui lòng nhập thông tin!", "Kết quả kiểm tra", MessageBoxButtons.OK);
                    TaoThanhVien ttv = new TaoThanhVien();
                    ttv.ShowDialog();
                }
                con.Close();
            }
        }

    }
}
