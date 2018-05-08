using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    /* This class is associated with CHUYENBAY table in the SQL Server database */

    public static class QuanLy_dataGridView1
    {
        public static List<DataGridViewRow> addedRows = new List<DataGridViewRow>();
        public static List<DataGridViewRow> modifiedRows = new List<DataGridViewRow>();
        public static List<DataGridViewRow> removedRows = new List<DataGridViewRow>();
        public static List<string> errors = new List<string>();

        private static string NotNullColumns
        {
            get { return "THOIGIANBAY, SOGHEHANG1, SOGHEHANG2 và GIAVE"; }
        }

        public static Color AddedRowColor
        {
            get { return Color.LawnGreen; }
        }

        public static Color ModifiedRowColor
        {
            get { return Color.Gold; }
        }

        public static Color RemovedRowColor
        {
            get { return Color.LightSalmon; }
        }

        public static Color PrimaryKeyColor
        {
            get { return Color.DarkRed; }
        }

        public static Color ErrorTextColor
        {
            get { return Color.Red; }
        }

        public static Color ChangeLogColor
        {
            get { return Color.Black; }
        }

        private static string errorText = "";
        public static string ErrorText
        {
            get { return errorText; }
        }

        private static string changeLog = "";
        public static string ChangeLog
        {
            get { return changeLog; }
        }

        public static void LoadDataToDataGridView(DataGridView dgv)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CHUYENBAY", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgv.DataSource = table;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[0].ReadOnly = true;
                row.Cells[0].Style.ForeColor = PrimaryKeyColor;
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
            dgv.ClearSelection();
        }

        public static DataGridViewRow FindRowInDataGridView(DataGridView dgv, string keyword)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value.ToString() == keyword)
                {
                    return row;
                }
            }
            return null;
        }

        public static DataGridViewRow FindRowInDataGridView(DataGridView dgv, string keyword, DataGridViewRow exceptionRow)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow && row != exceptionRow && row.Cells[0].Value.ToString() == keyword)
                {
                    return row;
                }
            }
            return null;
        }

        public static void FillDataTable(DataTable table)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM CHUYENBAY", connection);
                adapter.Fill(table);
            }
        }

        public static void AddRowToDataGridView(DataGridView dgv)
        {
            DataTable table = (DataTable)dgv.DataSource;
            table.Rows.Add(table.NewRow());
            dgv.DataSource = table;
        }

        public static bool CheckAffectedRows(DataGridView dgv)
        {
            bool saveSuccess = true;
            foreach (DataGridViewRow row in addedRows)
            {
                string rowNumber = "Hàng " + (row.Index + 1).ToString() + "\n";
                string MACB = row.Cells[0].Value.ToString();
                if (MACB == "")
                {
                    saveSuccess = false;
                    string error = rowNumber + "Không thể thêm bản ghi mới: MACB (khóa chính) bị bỏ trống.";
                    errors.Add(error);
                }
                else
                {
                    DataGridViewRow existingRow = FindRowInDataGridView(dgv, MACB, row);
                    if (existingRow != null)
                    {
                        saveSuccess = false;
                        string error = rowNumber + "Không thể thêm bản ghi mới: Đã tồn tại bản ghi có MACB = '" + row.Cells[0].Value.ToString() + "' trong CSDL (hàng " + (existingRow.Index + 1).ToString() + ").";
                        errors.Add(error);
                    }
                }

                if (row.Cells[4].Value.ToString() == "" || row.Cells[5].Value.ToString() == "" || row.Cells[6].Value.ToString() == "" || row.Cells[7].Value.ToString() == "")
                {
                    saveSuccess = false;
                    string error = rowNumber + "Không thể thêm bản ghi mới: Có ít nhất một trường trong các trường " + NotNullColumns + " bị bỏ trống.";
                    errors.Add(error);
                }
            }
            foreach (DataGridViewRow row in modifiedRows)
            {
                string rowNumber = "Hàng " + (row.Index + 1).ToString() + "\n";
                if (row.Cells[4].Value.ToString() == "" || row.Cells[5].Value.ToString() == "" || row.Cells[6].Value.ToString() == "" || row.Cells[7].Value.ToString() == "")
                {
                    saveSuccess = false;
                    string error = rowNumber + "Không thể sửa bản ghi: Có ít nhất một trường trong các trường " + NotNullColumns + " bị bỏ trống.";
                    errors.Add(error);
                }
            }
            if (saveSuccess)
                WriteChangeLog();
            else WriteErrorText();
            return saveSuccess;
        }

        public static void AddParametersToSqlCommand(SqlCommand cmd, DataGridViewRow row)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
            {
                connection.Open();
                SqlParameter parameter;
                parameter = new SqlParameter("@MaCB", row.Cells[0].Value.ToString());
                cmd.Parameters.Add(parameter);
                if (cmd.CommandText == "ThemChuyenBay" || cmd.CommandText == "SuaChuyenBay")
                {
                    parameter = new SqlParameter("@MaSBDi", row.Cells[1].Value.ToString());
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaSBDen", row.Cells[2].Value.ToString());
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@MaHHK", row.Cells[3].Value.ToString());
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@ThoiGianBay", int.Parse(row.Cells[4].Value.ToString()));
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@SoGheHang1", int.Parse(row.Cells[5].Value.ToString()));
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@SoGheHang2", int.Parse(row.Cells[6].Value.ToString()));
                    cmd.Parameters.Add(parameter);

                    parameter = new SqlParameter("@GiaVe", int.Parse(row.Cells[7].Value.ToString()));
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        private static void WriteErrorText()
        {
            errorText = "Cập nhật bảng [CHUYENBAY] không thành công - Không có thay đổi nào được lưu.\n";
            errorText += errors.Count.ToString() + " lỗi đã xảy ra:\n";
            foreach (string error in errors)
            {
                errorText += "* " + error + "\n";
            }
        }

        private static void WriteChangeLog()
        {
            changeLog = "Cập nhật bảng [CHUYENBAY] thành công - ";
            changeLog += (addedRows.Count + modifiedRows.Count + removedRows.Count).ToString() + " thay đổi đã được lưu (";
            changeLog += "thêm " + addedRows.Count.ToString() + " hàng, sửa " + modifiedRows.Count.ToString() + " hàng và xóa " + removedRows.Count.ToString() + " hàng).";
            if (addedRows.Count != 0 || modifiedRows.Count != 0 || removedRows.Count != 0)
            {
                changeLog += "\nCác thay đổi:\n";
                foreach (DataGridViewRow row in addedRows)
                {
                    changeLog += "* Đã thêm bản ghi có MACB = '" + row.Cells[0].Value.ToString() + "'.\n";
                }
                foreach (DataGridViewRow row in modifiedRows)
                {
                    changeLog += "* Đã sửa bản ghi có MACB = '" + row.Cells[0].Value.ToString() + "'.\n";
                }
                foreach (DataGridViewRow row in removedRows)
                {
                    changeLog += "* Đã xóa bản ghi có MACB = '" + row.Cells[0].Value.ToString() + "'.\n";
                }
            }
        }

        public static void ShowErrorText(Label labelToShow)
        {
            labelToShow.ForeColor = ErrorTextColor;
            labelToShow.Text = ErrorText;
        }

        public static void ShowChangeLog(Label labelToShow)
        {
            labelToShow.ForeColor = ChangeLogColor;
            labelToShow.Text = ChangeLog;
        }
    }
}