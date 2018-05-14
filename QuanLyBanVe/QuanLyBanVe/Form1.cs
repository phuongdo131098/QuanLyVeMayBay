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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            QuanLy.UpdateDataGridView(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbTaoThanhVien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaoThanhVien taoThanhVien = new TaoThanhVien();
            taoThanhVien.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                btnSua.Enabled = true;
                btnSaoChep.Enabled = true;
                btnBanVe.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnSaoChep.Enabled = false;
                btnBanVe.Enabled = false;
            }
            if (dataGridView1.SelectedRows.Count != 0)
                btnXoa.Enabled = true;
            else btnXoa.Enabled = false;
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                btnSua.Enabled = true;
                btnSaoChep.Enabled = true;
                btnBanVe.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnSaoChep.Enabled = false;
                btnBanVe.Enabled = false;
            }
            if (dataGridView1.SelectedRows.Count != 0)
                btnXoa.Enabled = true;
            else btnXoa.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            pnlSua.Visible = true;
            textBox16.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            cbbMaSBDi.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            cbbMaSBDen.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cbbHHK.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            textBox11.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox10.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            
            btnLuuThayDoi.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa vĩnh viễn (các) bản ghi này?", "Xóa bản ghi", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
                {
                    connection.Open();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        using (SqlCommand command = new SqlCommand("XoaChuyenBay", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            SqlParameter parameter = new SqlParameter("@MaCB", row.Cells[0].Value.ToString());
                            command.Parameters.Add(parameter);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                QuanLy.UpdateDataGridView(dataGridView1);
            }
        }

        private void btnSaoChep_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox1.Focus();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void btnLuuMoi_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("ThemCB", connection);
                        command.CommandType = CommandType.StoredProcedure;
                         
                        SqlParameter parameter;
                        parameter = new SqlParameter("@MaCB", textBox1.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaSBDi", comboBox1.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaSBDen", comboBox2.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaHHK", comboBox3.Text);
                        command.Parameters.Add(parameter);
                    
                        parameter = new SqlParameter("@ThoiGianKhoiHanh", Convert.ToDateTime(dateTimePicker1.Value.ToString()));
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@ThoiGianDen", Convert.ToDateTime(dateTimePicker2.Value.ToString()));
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@SoGheHang1", int.Parse(textBox6.Text));
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@SoGheHang2", int.Parse(textBox7.Text));
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@GiaVe", int.Parse(textBox8.Text));
                        command.Parameters.Add(parameter);

                        command.ExecuteNonQuery();
                    }
                    QuanLy.UpdateDataGridView(dataGridView1);
                }
                catch (Exception) when (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Vui lòng không bỏ trống các trường có ký hiệu (*).");
                }
                catch (Exception)
                {
                    DataGridViewRow existingRow = QuanLy.FindRowInDataGridView(dataGridView1, textBox1.Text);
                    if (existingRow != null)
                    {
                        MessageBox.Show("Chuyến bay '" + textBox1.Text + "' đã tồn tại trong CSDL. Nếu muốn thay đổi dữ liệu của chuyến bay này, vui lòng nhấn 'Sửa'.");
                        dataGridView1.ClearSelection();
                        existingRow.Selected = true;
                    }
                }
            }
            else MessageBox.Show("Chưa nhập MACB.");
        }

        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SuaCB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter;
                    parameter = new SqlParameter("@MaCB", textBox16.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaSBDi", cbbMaSBDi.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaSBDen", cbbMaSBDen.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaHHK", cbbHHK.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@ThoiGianKhoiHanh", Convert.ToDateTime(dateTimePicker1.Value.ToString()));
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@ThoiGianDen", Convert.ToDateTime(dateTimePicker2.Value.ToString()));
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@SoGheHang1", int.Parse(textBox11.Text));
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@SoGheHang2", int.Parse(textBox10.Text));
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@GiaVe", int.Parse(textBox9.Text));
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
                QuanLy.UpdateDataGridView(dataGridView1);
                // Tắt sửa chuyến bay
                pnlSua.Visible = false;
                cbbMaSBDen.Text = null;
                cbbMaSBDi.Text = null;
                cbbHHK.Text = null;
                textBox16.Clear();           

                textBox11.Clear();
                textBox10.Clear();
                textBox9.Clear();
            }
            catch (Exception) when (textBox11.Text == "" || textBox10.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống các trường có ký hiệu (*).");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnlSua.Visible = false;
            cbbMaSBDen.Text = null;
            cbbMaSBDi.Text = null;
            cbbHHK.Text = null;
            textBox16.Clear();

            textBox11.Clear();
            textBox10.Clear();
            textBox9.Clear();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuMoi.Enabled)
                btnLuuMoi.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.Text = null;
            comboBox2.Text = null;
            comboBox3.Text = null;

            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            btnLuuMoi.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            cbbMaSBDen.Text = null;
            cbbMaSBDi.Text = null;
            cbbHHK.Text = null;

            textBox11.Clear();
            textBox10.Clear();
            textBox9.Clear();
            btnLuuThayDoi.Enabled = false;
        }

        private void cbbNoiDi_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(cbbNoiDi);
        }
        private void cbbNoiDen_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(cbbNoiDen);
        }
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (cbbNoiDen.Text == cbbNoiDi.Text)
            {
                MessageBox.Show("Không thể tra cứu");
            }
            else
            {
                QuanLy.TraCuu(dataGridView2, cbbNoiDi, cbbNoiDen, dateTraCuu);
            }
         
        }

        private void cbbMaSBDi_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(cbbMaSBDi);

        }
        private void cbbMaSBDen_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(cbbMaSBDen);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnBanVe2.Enabled = true;
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadHHK(cbbHHK);
        }

        private void cbbMaSBDi_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void cbbMaSBDen_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void cbbHHK_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void comboBox1_DropDown_1(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(comboBox1);
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadSanBay(comboBox2);
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            QuanLy.LoadHHK(comboBox3);
        }
        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = ((DataGridView)sender).HitTest(e.X, e.Y);
                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hti.RowIndex].Selected = true;
                }
            }
            pictureBox1_Click(sender, e);
        }       
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnXoa_Click(sender, e);
        }

        private void làmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLy.UpdateDataGridView(dataGridView1);
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            string maCB = getMaCB(dataGridView1);
            BanVe formBanVe = new BanVe(maCB);
            formBanVe.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            btnLuuThayDoi.Enabled = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            btnLuuThayDoi.Enabled = true;
        }

        private void btnBanVe2_Click(object sender, EventArgs e)
        {
            string maCB = getMaCB(dataGridView1);
            BanVe formBanVe = new BanVe(maCB);
            formBanVe.ShowDialog();
        }

        private string getMaCB(DataGridView dataGridView1)
        {
            return dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void bánVéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBanVe_Click(sender, e);
        }

        private void bánVéToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnBanVe2_Click(sender, e);
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = ((DataGridView)sender).HitTest(e.X, e.Y);
                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[hti.RowIndex].Selected = true;
                }
            }
        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void làmMớiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnTraCuu_Click(sender, e);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void báoCáoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BaoCao formBaoCao = new BaoCao();
            formBaoCao.ShowDialog();
        }

        private void báoCáoNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCaoNam formBaoCaoNam = new BaoCaoNam();
            formBaoCaoNam.ShowDialog();
        }
    }
}