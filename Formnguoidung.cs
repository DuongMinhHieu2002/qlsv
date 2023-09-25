using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlsv_pj
{
    public partial class Formnguoidung : Form
    {
        List<string> listactype = new List<string>() {"Quản trị viên","Người dùng"};
        int index = -1;
        public Formnguoidung()
        {
            InitializeComponent();
        }

        void LoadListUser() {
            dtgvtk.DataSource = null;
            dtgvtk.DataSource = listuser.Instance.Listacuser;
            dtgvtk.Refresh();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvtk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadListUser();
        }

        private void cbbloaitk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Formnguoidung_Load(object sender, EventArgs e)
        {
            cbbloaitk.DataSource = listactype;
            LoadListUser();
        }

        private void dtgvtk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            txttentk.Text = dtgvtk.Rows[index].Cells[0].Value.ToString();
            txtmk.Text = dtgvtk.Rows[index].Cells[1].Value.ToString();

            switch (listuser.Instance.Listacuser[index].Actype)
            {
                case true: cbbloaitk.Text = "Quản trị viên";
                    break;
                case false: cbbloaitk.Text = "Người dùng";
                    break;
            }

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string un = txttentk.Text;
            string pw = txtmk.Text;
            bool at = false;

            switch (cbbloaitk.Text)
            {
                case "Quản trị viên":
                    at = true;
                    break;
                case "Người dùng":
                    at = false;
                    break;
            }
            listuser.Instance.Listacuser.Add(new user(un, pw, at));
            LoadListUser();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string un = txttentk.Text;
            string pw = txtmk.Text;
            bool at = false;

            switch (cbbloaitk.Text)
            {
                case "Quản trị viên":
                    at = true;
                    break;
                case "Người dùng":
                    at = false;
                    break;
            }
            listuser.Instance.Listacuser[index].Username = un;
            listuser.Instance.Listacuser[index].Password = pw;
            listuser.Instance.Listacuser[index].Actype = at;
            LoadListUser();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            listuser.Instance.Listacuser.RemoveAt(index);
            LoadListUser();
        }
    }
}
