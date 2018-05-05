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
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM CHUYENBAY", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                DataTable datb = new DataTable();
                adapter.Fill(datb);
                dataGridView1.DataSource = datb;
            }
            dataGridView1.ClearSelection();
        }

        public static DataGridViewRow FindRowInDataGridView(DataGridView dgv, string keyword)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == keyword)
                {
                    return row;
                }
            }
            return null;
        }
    }
}