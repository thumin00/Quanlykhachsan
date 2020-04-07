using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlKhachSan
{
    public partial class frmQLKS : Form
    {
        SqlConnection con;
        public frmQLKS()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            string sql;
            DataTable tblKS;
            sql = "select * from tbIPhong";
            tblKS = Thucthisql.Docbang(sql);
            dtgvQLKH.DataSource = tblKS;
            dtgvQLKH.Columns[0].HeaderText = "Mã phòng";
            dtgvQLKH.Columns[1].HeaderText = "tên phòng";
            dtgvQLKH.Columns[2].HeaderText = "đơn giá";
            dtgvQLKH.Columns[0].Width = 150;
            dtgvQLKH.Columns[1].Width = 150;
            dtgvQLKH.Columns[2].Width = 150;
            dtgvQLKH.AllowUserToAddRows = false;
            dtgvQLKH.EditMode = DataGridViewEditMode.EditProgrammatically;
            tblKS.Dispose();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        //private void loadDataToGridView()
        //{
        //    string sql = "select *From tblPhong";
        //    SqlDataAdapter adp = new SqlDataAdapter(sql, con);
        //    DataTable tabletbIPhong = new DataTable();
        //    adp.Fill(tabletbIPhong);
        //    DataGridView_btIPhong.datasouce = tabletbIPhong;
        //}
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenPhong.Enabled = true;
            txtMaphong.Enabled = true;
            txtDongia.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            hienthi();
        }

        private void dtgvQLKH_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //hienthi();
        }
        

      
        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //txtMaphong.Text = "";
            //txtTenPhong.Text = "";
            //txtDongia.Text = "";
            //btnHuy.Enabled = false;
            //btnThem.Enabled = true;
            //btnXoa.Enabled = true;
            //btnSua.Enabled = true;
            //btnLuu.Enabled = false;
            //txtMaphong.Enabled = false;
            //loadDataToGridView();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtDongia.Text == "" && txtMaphong.Text == "" && txtTenPhong.Text == "")
            {
                MessageBox.Show("Bạn không được để trống ", "Thông báo");
                txtTenPhong.Focus();
            }
            else
            {
                sql = "select * from tbIPhong where Maphong=N'" + txtMaphong.Text.Trim() + "'";
                DataTable tblKS = Thucthisql.Docbang(sql);
                if (tblKS.Rows.Count > 0)
                {
                    MessageBox.Show("Mã phòng đã tồn tại, bạn phải đổi sang mã khác", "Thông báo");
                    txtMaphong.Text = "";
                    txtMaphong.Focus();
                    return;
                }
                sql = "INSERT INTO tbIPhong(Maphong,Tenphong,Dongia) values(N'" + txtMaphong.Text + "',N'" + txtTenPhong.Text + "',N'" + txtDongia.Text + "')";
                Thucthisql.capnhat(sql);
                hienthi();
                btnLuu.Enabled = false;
                txtMaphong.Text = "";
                txtTenPhong.Text = "";
                txtDongia.Text = "";
                txtMaphong.Enabled = false;
                txtTenPhong.Enabled = false;
                txtDongia.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnSua.Enabled = true;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String sql;
            //Form f = new Form();
            //f.StartPosition = FormStartPosition.CenterScreen;
            txtDongia.Enabled = true;
            txtTenPhong.Enabled = true;
            txtMaphong.Enabled = false;
            txtMaphong.Text = dtgvQLKH.CurrentRow.Cells["Maphong"].Value.ToString();
            txtTenPhong.Text = dtgvQLKH.CurrentRow.Cells["Tenphong"].Value.ToString();
            txtDongia.Text = dtgvQLKH.CurrentRow.Cells["Dongia"].Value.ToString();
            
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenPhong.Text.Trim().Length == 0)
            {
                MessageBox.Show("bạn phải nhập tên phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return;
            }
            sql = "update tbIPhong set TenPhong=N'" + txtTenPhong.Text.Trim() + "',DonGia=N'" + txtDongia.Text.Trim() + "'where MaPhong=N'" + txtMaphong.Text.Trim() + "'";
            Thucthisql.capnhat(sql);
            txtDongia.Enabled = false;
            txtTenPhong.Enabled = false;
            txtMaphong.Enabled = false;
            txtDongia.Text = "";
            txtTenPhong.Text = "";
            txtMaphong.Text = "";
            hienthi();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (MessageBox.Show("bạn cso muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) ;
            {
                sql = "delete tbIPhong where MaPhong=N'" + dtgvQLKH.CurrentRow.Cells["Maphong"].Value.ToString() + "'";
                Thucthisql.capnhat(sql);
                hienthi();
            }
        }
    }
}