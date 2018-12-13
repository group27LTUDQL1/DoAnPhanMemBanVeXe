using Microsoft.VisualBasic;
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

namespace DoAnPhanMemBanVeXe_2
{
    public partial class Form_Phan_Quyen : Form
    {
        Form_Login fl;
        Form_Main fm;

        private string lenh;

        public Form_Phan_Quyen()
        {
            Load += Form_Phan_Quyen_Load;
            InitializeComponent();
        }

        private void ButtonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Phan_Quyen_Load(object sender, EventArgs e)
        {
            LoadQuyen();
        }

        private void LoadQuyen()
        {
            
            fm = new Form_Main();
            fl = new Form_Login();
            if (fl.LoginLoaiND == "Quan_Ly" || fl.LoginLoaiND == "Admin")
            {
                grb_PhanQuyen.Enabled = true;
            }
            else
            {
                grb_PhanQuyen.Enabled = false;
                btn_DongY.Enabled = false;
            }
            txt_IdNhanVien.Text = fm.cbo_Username.Text;

            SqlCommand query = new SqlCommand("select * from PhanQuyen where IdNhanVien ='" + fm.cbo_Username.Text + "'", Ket_noi.connect);
            SqlDataReader DR = null;
            Ket_noi.connect.Open();
            DR = query.ExecuteReader();
            while (DR.Read() == true)
            {
                if (Convert.ToInt32(DR.GetValue(1).ToString()) == 1)
                {
                    ckb_xe.Checked = true;
                }
                if (Convert.ToInt32(DR.GetValue(2).ToString()) == 1)
                {
                    ckb_tuyen.Checked = true;
                }
                if (Convert.ToInt32(DR.GetValue(3).ToString()) == 1)
                {
                    ckb_ThoiDiem.Checked = true;
                }
                if (Convert.ToInt32(DR.GetValue(4).ToString()) == 1)
                {
                    ckb_chuyenXe.Checked = true;
                }
                if (Convert.ToInt32(DR.GetValue(5).ToString()) == 1)
                {
                    ckb_banve.Checked = true;
                }
            }
            Ket_noi.connect.Close();
        }

        private void btn_DongY_Click(object sender, EventArgs e)
        {
            string Xe = null;
            string TX = null;
            string TD = null;
            string CX = null;
            string BV = null;
            Xe = Convert.ToString(0);
            TX = Convert.ToString(0);
            TD = Convert.ToString(0);
            CX = Convert.ToString(0);
            BV = Convert.ToString(0);
            if (ckb_xe.Checked == true)
            {
                Xe = Convert.ToString(1);
            }
            if (ckb_tuyen.Checked == true)
            {
                TX = Convert.ToString(1);
            }
            if (ckb_ThoiDiem.Checked == true)
            {
                TD = Convert.ToString(1);
            }
            if (ckb_chuyenXe.Checked == true)
            {
                CX = Convert.ToString(1);
            }
            if (ckb_banve.Checked == true)
            {
                BV = Convert.ToString(1);
            }
            DialogResult dlg = MessageBox.Show("Bạn có chắc chắn muốn cấp quyền cho nhân viên " + txt_IdNhanVien.Text + "!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == System.Windows.Forms.DialogResult.Yes)
            {
                //Duyet trong bang phan quyen xem co nhan vien ten do chua, neu chua thi insert vao
                SqlDataReader dr = null;
                var lenh1 = "Select IdNhanVien from PhanQuyen";
                SqlCommand bo_lenh = new SqlCommand(lenh1, Ket_noi.connect);
                int flag = 0;
                Ket_noi.connect.Open();
                dr = bo_lenh.ExecuteReader();
                while (dr.Read() == true)
                {
                    if (dr.GetValue(0).ToString() == txt_IdNhanVien.Text)
                    {
                        flag = 1;
                        break; // TODO: might not be correct. Was : Exit While
                    }
                }
                Ket_noi.connect.Close();
                if (flag == 0)
                {
                    lenh = "Insert into PhanQuyen values('" + txt_IdNhanVien.Text + "', " + Xe + ", " + TX + ", " + TD + ", " + CX + ", " + BV + ")";
                }
                else
                {
                    lenh = "Update PhanQuyen set Xe = '" + Xe + "', TuyenXe = '" + TX + "', ThoiDiem = '" + TD + "', ChuyenXe = '" + CX + "', BanVe = '" + BV + "' where IdNhanVien = '" + txt_IdNhanVien.Text + "'";
                }
                SqlCommand com = new SqlCommand(lenh, Ket_noi.connect);
                //MessageBox.Show(lenh)
                try
                {
                    Ket_noi.connect.Open();
                    com.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    MessageBox.Show("Nhân viên " + txt_IdNhanVien.Text + " đã được cấp quyền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cấp quyền không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                MessageBox.Show("Đã hủy thao tác cấp quyền, bạn có thể câp quyền lại hoặc thoát ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Ket_noi.connect.Close();
            UpdateQuyen();
        }

        public void UpdateQuyen()
        {
            fm = new Form_Main();
            fl = new Form_Login();
            SqlCommand query = new SqlCommand("select * from PhanQuyen where IdNhanVien ='" + fl.LoginTenND + "'", Ket_noi.connect);
            SqlDataReader DR = null;
            Ket_noi.connect.Open();
            DR = query.ExecuteReader();
            while (DR.Read() == true)
            {
                if (Convert.ToInt32(DR.GetValue(1).ToString()) == 0)
                {
                    fm.TabItem_2.Visible = false;
                }
                if (Convert.ToInt32(DR.GetValue(2).ToString()) == 0)
                {
                    fm.TabItem_3.Visible = false;
                }
                if (Convert.ToInt32(DR.GetValue(3).ToString()) == 0)
                {
                    fm.TabItem_4.Visible = false;
                }
                if (Convert.ToInt32(DR.GetValue(4).ToString()) == 0)
                {
                    fm.TabItem_5.Visible = false;
                }
                if (Convert.ToInt32(DR.GetValue(5).ToString()) == 0)
                {
                    fm.TabItem_6.Visible = false;
                }
            }
            Ket_noi.connect.Close();
        }
        
    }
}
