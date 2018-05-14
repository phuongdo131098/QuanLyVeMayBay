﻿using System;
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
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand("LietKeCB", connection);
                comm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                DataTable datb = new DataTable();
                adapter.Fill(datb);
                dataGridView1.DataSource = datb;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
        public static void LoadSanBay(ComboBox comboBox)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT TENSANBAY FROM SANBAY", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;

                DataTable datb = new DataTable();
                dataAdapter.Fill(datb);
                comboBox.DataSource = datb;
                comboBox.DisplayMember = "TENSANBAY";
                comboBox.ValueMember = "TENSANBAY";
            }
        }
        public static void LoadHHK(ComboBox comboBox)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT TENHHK FROM HANGHK", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataTable datb = new DataTable();
                dataAdapter.Fill(datb);
                comboBox.DataSource = datb;
                comboBox.DisplayMember = "TENHHK";
                comboBox.ValueMember = "TENHHK";

            }
        }
        public static void TraCuu(DataGridView dataGridView, ComboBox cbbDi, ComboBox cbbDen, DateTimePicker date)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                dataGridView.RowHeadersVisible = false;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                conn.Open();
                SqlCommand comm = new SqlCommand("TraCuu", conn);
                comm.CommandType = CommandType.StoredProcedure;

                SqlParameter para = new SqlParameter("@MaSBDi", cbbDi.Text);
                comm.Parameters.Add(para);

                para = new SqlParameter("@MaSBDen", cbbDen.Text);
                comm.Parameters.Add(para);

                para = new SqlParameter("@ThoiGianBay", Convert.ToDateTime(date.Value.ToShortDateString()));
                comm.Parameters.Add(para);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;
                DataTable datb = new DataTable();
                dataAdapter.Fill(datb);
                dataGridView.DataSource = datb;
            }
            dataGridView.ClearSelection();
        }
        public static void BaoCao(DataGridView gridView, DateTimePicker dateDi, DateTimePicker dateDen,TextBox txt)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                gridView.RowHeadersVisible = false;
                gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                conn.Open();
                SqlCommand comm = new SqlCommand("BaoCao", conn);
                comm.CommandType = CommandType.StoredProcedure;

                SqlParameter para = new SqlParameter("@TuNgay", Convert.ToDateTime(dateDi.Value.ToString()));
                comm.Parameters.Add(para);

                para = new SqlParameter("@DenNgay", Convert.ToDateTime(dateDen.Value.ToString()));
                comm.Parameters.Add(para);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;

                DataTable datb = new DataTable();
                dataAdapter.Fill(datb);
                gridView.DataSource = datb;
                int sum = 0;
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    sum += Int32.Parse(gridView["DOANH THU", i].Value.ToString());
                    gridView["Tỉ lệ", i].Value = Math.Round(Convert.ToDouble(gridView["Tỉ lệ", i].Value), 2);
                }
                txt.Text = sum.ToString();
            }
            gridView.ClearSelection();
        }

        public static void BaoCaoNam(DataGridView gridView, ComboBox box, TextBox txt)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.localConnectionString_HoangAn))
            {
                gridView.RowHeadersVisible = false;
                gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                conn.Open();
                SqlCommand comm = new SqlCommand("BaoCaoNam", conn);
                comm.CommandType = CommandType.StoredProcedure;

                SqlParameter para = new SqlParameter("@NAM", Int32.Parse(box.Text));
                comm.Parameters.Add(para);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comm;

                DataTable datb = new DataTable();
                dataAdapter.Fill(datb);
                gridView.DataSource = datb;
                int sum = 0;
                for(int i=0; i<gridView.RowCount; i++)
                {
                    sum += Int32.Parse(gridView["DOANH THU", i].Value.ToString());
                    gridView["Tỉ lệ", i].Value = Math.Round(Convert.ToDouble(gridView["Tỉ lệ", i].Value), 2);
                }
                txt.Text = sum.ToString();
            }
            gridView.ClearSelection();             
        }
    }
}