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
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            LoadData();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           Form dangNhap = new DangNhap();
            dangNhap.ShowDialog();
        }
        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form baoCao = new BaoCao();
            baoCao.ShowDialog();
        }
        public void LoadData()
        {
            
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-260M5KJ;Initial Catalog=QLVeMayBay;Integrated Security=True";
            conn.Open();

            SqlCommand comm = new SqlCommand("LietKeCB", conn);
            comm.CommandType = CommandType.StoredProcedure;

            //comm.EndExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comm;
            DataTable datb = new DataTable();
            adapter.Fill(datb);

            dataGridView1.DataSource = datb;
            conn.Close();
        }
    }
}
