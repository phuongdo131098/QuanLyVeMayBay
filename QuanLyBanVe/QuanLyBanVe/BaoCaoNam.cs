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
    public partial class BaoCaoNam : Form
    {
        public BaoCaoNam()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Vui lòng nhập năm!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                panel1.Visible = true;
                label1.Text = comboBox1.Text;
                QuanLy.BaoCaoNam(dataGridView1, comboBox1,textBox1);
            }
        }
    }
}
