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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            QuanLy_dataGridView1.LoadDataToDataGridView(dataGridView1);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

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

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
                btnXoa.Enabled = true;
            else btnXoa.Enabled = false;
        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];
                if (selectedCell.OwningRow.DefaultCellStyle.BackColor == QuanLy_dataGridView1.AddedRowColor)
                {
                    btnSua.Enabled = true;
                    return;
                }
                if (selectedCell.OwningColumn.Index != 0 && selectedCell.OwningRow.DefaultCellStyle.BackColor != QuanLy_dataGridView1.RemovedRowColor)
                {
                    btnSua.Enabled = true;
                    return;
                }
            }
            btnSua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            QuanLy_dataGridView1.AddRowToDataGridView(dataGridView1);
            DataGridViewRow newRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            newRow.HeaderCell.Value = String.Format("{0}", newRow.Index + 1);

            QuanLy_dataGridView1.addedRows.Add(newRow);
            newRow.DefaultCellStyle.BackColor = QuanLy_dataGridView1.AddedRowColor;
            dataGridView1.CurrentCell = newRow.Cells[0];
            dataGridView1.BeginEdit(true);

            btnSave.Enabled = true;
            btnCancelChanges.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.SelectedCells[0];
            dataGridView1.BeginEdit(true);
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            btnSua.Enabled = false;

            DataGridViewCell modifiedCell = dataGridView1.SelectedCells[0];
            DataGridViewRow modifiedRow = modifiedCell.OwningRow;

            if (modifiedRow.DefaultCellStyle.BackColor != QuanLy_dataGridView1.AddedRowColor &&
                modifiedRow.DefaultCellStyle.BackColor != QuanLy_dataGridView1.RemovedRowColor &&
                modifiedRow.DefaultCellStyle.BackColor != QuanLy_dataGridView1.ModifiedRowColor)
            {
                QuanLy_dataGridView1.modifiedRows.Add(modifiedRow);
                modifiedRow.DefaultCellStyle.BackColor = QuanLy_dataGridView1.ModifiedRowColor;
            }

            btnSave.Enabled = true;
            btnCancelChanges.Enabled = true;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.DefaultCellStyle.BackColor != QuanLy_dataGridView1.RemovedRowColor)
                {
                    row.ReadOnly = true;

                    if (row.DefaultCellStyle.BackColor == QuanLy_dataGridView1.AddedRowColor)
                    {
                        row.DefaultCellStyle.BackColor = QuanLy_dataGridView1.RemovedRowColor;
                        QuanLy_dataGridView1.addedRows.Remove(row);
                        continue;
                    }

                    if (row.DefaultCellStyle.BackColor == QuanLy_dataGridView1.ModifiedRowColor)
                    {
                        QuanLy_dataGridView1.removedRows.Add(row);
                        row.DefaultCellStyle.BackColor = QuanLy_dataGridView1.RemovedRowColor;
                        QuanLy_dataGridView1.modifiedRows.Remove(row);
                        continue;
                    }

                    QuanLy_dataGridView1.removedRows.Add(row);
                    row.DefaultCellStyle.BackColor = QuanLy_dataGridView1.RemovedRowColor;
                }
            }
            dataGridView1.ClearSelection();

            btnSave.Enabled = true;
            btnCancelChanges.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!QuanLy_dataGridView1.CheckAffectedRows(dataGridView1))
            {
                /* Show error(s) to the user */
                btnSave.Enabled = false;
                MessageBox.Show("Có lỗi khi cập nhật dữ liệu vào CDSL. Vui lòng kiểm tra lại các thay đổi của bạn.", "Cập nhật không thành công");
                QuanLy_dataGridView1.ShowErrorText(lblUpdateStatus);
                QuanLy_dataGridView1.errors.Clear();
            }
            else
            {
                /* Save the changes */
                btnSave.Enabled = false;
                btnCancelChanges.Enabled = false;
                foreach (DataGridViewRow row in QuanLy_dataGridView1.addedRows)
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("ThemChuyenBay", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        QuanLy_dataGridView1.AddParametersToSqlCommand(command, row);
                        command.ExecuteNonQuery();
                    }
                }

                foreach (DataGridViewRow row in QuanLy_dataGridView1.modifiedRows)
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SuaChuyenBay", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        QuanLy_dataGridView1.AddParametersToSqlCommand(command, row);
                        command.ExecuteNonQuery();
                    }
                }

                foreach (DataGridViewRow row in QuanLy_dataGridView1.removedRows)
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Resources.localConnectionString_VietAnh))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("XoaChuyenBay", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        QuanLy_dataGridView1.AddParametersToSqlCommand(command, row);
                        command.ExecuteNonQuery();
                    }
                }
                QuanLy_dataGridView1.ShowChangeLog(lblUpdateStatus);
                QuanLy_dataGridView1.addedRows.Clear();
                QuanLy_dataGridView1.modifiedRows.Clear();
                QuanLy_dataGridView1.removedRows.Clear();
                QuanLy_dataGridView1.LoadDataToDataGridView(dataGridView1);
            }
        }

        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancelChanges.Enabled = false;
            QuanLy_dataGridView1.addedRows.Clear();
            QuanLy_dataGridView1.modifiedRows.Clear();
            QuanLy_dataGridView1.removedRows.Clear();
            QuanLy_dataGridView1.LoadDataToDataGridView(dataGridView1);
        }
    }
}