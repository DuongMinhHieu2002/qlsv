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
namespace qlsv_pj
{
    public partial class Formqlgv : Form
    {
        public int id;
        public Formqlgv()
        {
            InitializeComponent();
        }

        private void Formqlgv_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            conn.Open();
            string insertQuery = "INSERT INTO giaovien (hoten,sdt,diachi,monhoc) VALUES (@HoTen, @SDT, @DiaChi, @MonHoc)";
            if (txthoten.Text == "" || txtsdt.Text == "" || txtdiachi.Text == "" || txtmonhoc.Text == "")
            {
                MessageBox.Show("Đữ liệu chưa đầy đủ","Lỗi",MessageBoxButtons.OK);
            }
            else
            {
                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@HoTen", txthoten.Text);
                    command.Parameters.AddWithValue("@SDT", txtsdt.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                    command.Parameters.AddWithValue("@MonHoc", txtmonhoc.Text);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm giáo viên thành công!");
                        txtmonhoc.Clear();
                        txtdiachi.Clear();
                        txthoten.Clear();
                        txtsdt.Clear();
                        LoadDataIntoDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Thêm giáo viên thất bại!");
                    }
                }
            }
            conn.Close();

        }
        private void LoadDataIntoDataGridView()
        {
            
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            string insertQuery = "SELECT * FROM giaovien";
            using (SqlCommand command = new SqlCommand(insertQuery, conn))
            {
                
                SqlDataAdapter adapter = new SqlDataAdapter(insertQuery, conn);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                // Gán dữ liệu từ DataTable vào DataGridView
                dtgvbv.DataSource = dataTable;
            }
        }
        private void XoaGiaoVien(int teacherId)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            conn.Open();
            string deleteQuery = "DELETE FROM giaovien WHERE Id = @TeacherId";

            using (SqlCommand command = new SqlCommand(deleteQuery, conn))
            {
                command.Parameters.AddWithValue("@TeacherId", teacherId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa giáo viên thành công!");
                    LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi xóa
                }
                else
                {
                    MessageBox.Show("Xóa giáo viên thất bại!");
                }
            }

            conn.Close();
        }
        private void CapNhatGiaoVien(int teacherId, string hoTen, string sdt, string diaChi, string monHoc)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            conn.Open();

            string updateQuery = "UPDATE giaovien SET hoten = @HoTen, sdt = @SDT, diachi = @DiaChi, monhoc = @MonHoc WHERE Id = @TeacherId";

            using (SqlCommand command = new SqlCommand(updateQuery, conn))
            {
                command.Parameters.AddWithValue("@HoTen", hoTen);
                command.Parameters.AddWithValue("@SDT", sdt);
                command.Parameters.AddWithValue("@DiaChi", diaChi);
                command.Parameters.AddWithValue("@MonHoc", monHoc);
                command.Parameters.AddWithValue("@TeacherId", teacherId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên thành công!");
                    LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi cập nhật dữ liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên thất bại!");
                }
            }

            conn.Close();
        }

        private void dtgvbv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToUInt16 (dtgvbv.Rows[e.RowIndex].Cells[0].Value.ToString());
            txthoten.Text = dtgvbv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtsdt.Text = dtgvbv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtdiachi.Text = dtgvbv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtmonhoc.Text = dtgvbv.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnrf_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            XoaGiaoVien(id);
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txthoten.Text == "" || txtsdt.Text == "" || txtdiachi.Text == "" || txtmonhoc.Text == "")
            {
                MessageBox.Show("Đữ liệu chưa đầy đủ", "Lỗi", MessageBoxButtons.OK);
            }
            else
            {
                CapNhatGiaoVien(id, txthoten.Text, txtsdt.Text, txtdiachi.Text, txtmonhoc.Text);
                LoadDataIntoDataGridView();
            }
        }
    }
}
