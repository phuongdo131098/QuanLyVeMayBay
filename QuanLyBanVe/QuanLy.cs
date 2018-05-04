using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public static class QuanLy
    {
        public static void UpdateDataGridView(DataGridView dataGridView1)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Properties.Resources.localConnectionString_VietAnh;
            connection.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM CHUYENBAY", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comm;
            DataTable datb = new DataTable();
            adapter.Fill(datb);
            dataGridView1.DataSource = datb;
            connection.Close();
        }
    }
}