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

        private void Form1_Load(object sender, EventArgs e) { }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form baoCao = new BaoCao();
            baoCao.ShowDialog();
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
            textBox15.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox14.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox13.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox12.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox11.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox10.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox15.Focus();
            btnLuuThayDoi.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa vĩnh viễn (các) bản ghi này?", "Xóa bản ghi", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
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
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
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
                    using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("ThemVe", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter parameter;
                        parameter = new SqlParameter("@MaCB", textBox1.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaSBDi", textBox2.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaSBDen", textBox3.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@MaHHK", textBox4.Text);
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter("@ThoiGianBay", int.Parse(textBox5.Text));
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
                catch (Exception) when (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
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
                using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SuaVe", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter;
                    parameter = new SqlParameter("@MaCB", textBox16.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaSBDi", textBox15.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaSBDen", textBox14.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaHHK", textBox13.Text);
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter("@ThoiGianBay", int.Parse(textBox12.Text));
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
                textBox16.Clear();
                textBox15.Clear();
                textBox14.Clear();
                textBox13.Clear();
                textBox12.Clear();
                textBox11.Clear();
                textBox10.Clear();
                textBox9.Clear();
            }
            catch (Exception) when (textBox12.Text == "" || textBox11.Text == "" || textBox10.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống các trường có ký hiệu (*).");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnlSua.Visible = false;
            textBox16.Clear();
            textBox15.Clear();
            textBox14.Clear();
            textBox13.Clear();
            textBox12.Clear();
            textBox11.Clear();
            textBox10.Clear();
            textBox9.Clear();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (!btnLuuThayDoi.Enabled)
                btnLuuThayDoi.Enabled = true;
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
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            btnLuuMoi.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox15.Clear();
            textBox14.Clear();
            textBox13.Clear();
            textBox12.Clear();
            textBox11.Clear();
            textBox10.Clear();
            textBox9.Clear();
            btnLuuThayDoi.Enabled = false;
        }
    }
}