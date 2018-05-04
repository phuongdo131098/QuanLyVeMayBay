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
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            QuanLy.UpdateDataGridView(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form baoCao = new BaoCao();
            baoCao.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBanVe.Enabled = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBanVe.Enabled = false;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBanVe.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1 && !dataGridView1.SelectedRows[0].IsNewRow)
            {
                tabControl1.Enabled = false;
                panel1.Visible = true;

            }
            else
            {
                MessageBox.Show("Chỉ được sửa 1 hàng có dữ liệu tại một thời điểm.");
            }
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            //formBanVe.Show();
        }

        private void llbTaoThanhVien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaoThanhVien taoThanhVien = new TaoThanhVien();
            taoThanhVien.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
            {
                connection.Open();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        using (SqlCommand command = new SqlCommand("DELETE FROM CHUYENBAY WHERE MACB = '" + row.Cells[0].Value.ToString() + "'", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                QuanLy.UpdateDataGridView(dataGridView1);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            tabControl1.Enabled = false;
            panel1.Visible = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                {
                    connection.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == textBox1.Text)
                        {
                            using (SqlCommand command = new SqlCommand("SuaVe", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                //SqlParameter parameter = new SqlParameter("@MaCB", textBox1.Text);
                                //command.Parameters.Add(parameter);

                                //parameter = new SqlParameter("@Ms", txtHoTen.Text);
                                //command.Parameters.Add(parameter);

                                //parameter = new SqlParameter("@Tuoi", Int32.Parse(txtTuoi.Text));
                                //command.Parameters.Add(parameter);
                                //bool b = false; ;
                                //if (rbtnNam.Checked)
                                //    b = true;
                                //parameter = new SqlParameter("@GioiTinh", b);
                                //command.Parameters.Add(parameter);

                                //parameter = new SqlParameter("@CMND", txtCMND.Text);
                                //command.Parameters.Add(parameter);

                                //parameter = new SqlParameter("@DiaChi", txtDiaChi.Text);
                                //command.Parameters.Add(parameter);

                                //parameter = new SqlParameter("@SDT", txtSDT.Text);
                                //command.Parameters.Add(parameter);
                                //command.ExecuteNonQuery();
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Khóa chính chưa được nhập.");
            }
            QuanLy.UpdateDataGridView(dataGridView1);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            panel1.Visible = false;
            tabControl1.Enabled = true;
        }
    }
}