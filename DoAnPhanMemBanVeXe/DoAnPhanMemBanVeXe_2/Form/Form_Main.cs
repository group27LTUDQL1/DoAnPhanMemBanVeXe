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
using DevComponents.DotNetBar;


namespace DoAnPhanMemBanVeXe_2
{
    public partial class Form_Main : DevComponents.DotNetBar.Office2007RibbonForm
    {
        Form_Login fl;
        private bool flag = true;
        private Nguoi_dung Nguoi_dung = new Nguoi_dung();
        private Xe Xe = new Xe();
        private Tuyen_xe Tuyen_xe = new Tuyen_xe();
        private Thoi_diem Thoi_diem = new Thoi_diem();
        private Chuyen_xe Chuyen_Xe = new Chuyen_xe();
        private Ban_ve Ban_ve = new Ban_ve();
        private Form_Phan_Quyen Quyen = new Form_Phan_Quyen();
        private Update_he_thong update_he_thong = new Update_he_thong();


        public Form_Main()
        {
            
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            update_he_thong.update_();
            Nguoi_dung.UpdateNguoiDung();
            Xe.UpdateXe();
            Tuyen_xe.UpdateTuyenXe();
            Thoi_diem.Update_thoi_diem();
            Chuyen_Xe.Update_Chuyen_xe();
            Ban_ve.Update_Ve_xe();
            Quyen.UpdateQuyen();
            Timer1.Interval = 1000;
            
            Timer2.Interval = 100;
            Timer2.Start();
        }

        #region "Xu ly Timer da xong"
        private void ribbonControl1_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Timer_ChayChu.Stop();
                flag = !flag;
            }
            else
            {
                Timer_ChayChu.Start();
                flag = !flag;
            }
        }

        private void Timer_ChayChu_Tick(object sender, EventArgs e)
        {
            if (lblChayChu.Left < 0)
            {
                lblChayChu.Left = 1400;
                flag = !flag;
            }
            else
            {
                lblChayChu.Left -= 10;
                if (flag)
                {
                    lblChayChu.ForeColor = Color.Black;
                    flag = !flag;
                }
                else
                {
                    lblChayChu.ForeColor = Color.Teal;
                    flag = !flag;
                }
            }
        }

        #endregion

        #region "Cac su kien Close, Logout cua FormMain da xu ly xong"
        private void ButtonX_Close_Click(object sender, EventArgs e)
        {
            
            fl = new Form_Login();
            this.WindowState = 0;
            do
            {
                this.Top = this.Top + 10;
                this.Left = this.Left + 10;
                this.Width = this.Width - 30;
                this.Height = this.Height - 30;
            } while (!(this.Top >= this.Height));
            System.Environment.Exit(0);
            fl.Close();
        }

        private void ButtonX_Logout_Click(object sender, EventArgs e)
        {
            
            fl = new Form_Login();
            fl.Visible = true;
            fl.Opacity = 100;
            fl.txtPassword.Clear();
            fl.Timer1.Start();
            fl.Timer2.Start();
            this.Close();

        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            fl = new Form_Login();
            fl.Visible = true;
            fl.Opacity = 100;
            fl.Timer1.Start();
            fl.Timer2.Start();
            fl.txtPassword.Clear();
        }

        #endregion

        #region "Xu ly cac su kien click cac button ben trai de di chuyen cac tab dieu khien hoan tat"
        private void btn_QuanLyND_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 0;
        }

        private void btn_QuanLyTuyenXe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 2;
        }

        private void btn_QuanLyXe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 1;
        }

        private void btn_ChuyenXe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 4;
        }

        private void btn_BanVe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 5;
        }
        #endregion

        //-----------------------------------------------Xu Ly Nguoi Dung----------------------------------------'
        #region "Xu ly cac nut di chuyen ben phai da hoan tat"
        private void btnBack_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Di_chuyen_ve_truoc();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Di_chuyen_ve_sau();
        }

        private void btnHead_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Di_chuyen_ve_dau();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Di_chuyen_ve_cuoi();
        }

        #endregion

        #region "Xu ly su kien them, sua, xoa nguoi dung da hoan tat"
        private void Button_Them_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Them_nguoi_dung();
        }

        private void Button_Luu_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Luu_thay_doi();
        }

        private void Button_Huy_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Huy_thao_tac();
        }

        private void Button_Sua_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Sua_thong_tin_ca_nhan();
        }

        private void Button_Xoa_Click(object sender, EventArgs e)
        {
            Nguoi_dung.Xoa_nguoi_dung();
        }

        #endregion

        #region "Xu ly di chuyen radio theo click chuot tren datagrid da xong"
        private void luoi_NguoiDung_MouseClick(object sender, MouseEventArgs e)
        {
            if (luoi_NguoiDung.CurrentRow.Cells[4].Value.ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
        }

        private void luoi_NguoiDung_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbo_Username.Text = luoi_NguoiDung["IdNguoiDung", e.RowIndex].EditedFormattedValue.ToString();
            txt_Password.Text = luoi_NguoiDung["PassND", e.RowIndex].EditedFormattedValue.ToString();
            txt_HoTen.Text = luoi_NguoiDung["HoTen", e.RowIndex].EditedFormattedValue.ToString();
            date_NgaySinh.Text = luoi_NguoiDung["NgaySinh", e.RowIndex].EditedFormattedValue.ToString();
            if (luoi_NguoiDung["GioiTinh", e.RowIndex].EditedFormattedValue.ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
            txt_SoDienThoai.Text = luoi_NguoiDung["SoDT", e.RowIndex].EditedFormattedValue.ToString();
            txt_DiaChi.Text = luoi_NguoiDung["DiaChi", e.RowIndex].EditedFormattedValue.ToString();
        }

        private void luoi_NguoiDung_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (luoi_NguoiDung.SelectedRows[e.RowIndex].Cells[4].Value.ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
        }


        #endregion

        #region "Xu ly button cap pass hoan tat "
        private void Button_CapPass_Click(object sender, EventArgs e)
        {
            if (cbo_IdLoaiND.Text == "Nhan_Vien")
            {
                Form_Cap_pass frm = new Form_Cap_pass();
                frm.ShowDialog();
            }
            else if (cbo_IdLoaiND.Text == "Admin")
            {
                MessageBox.Show("Bạn không được cấp pass cho người Admin, vui lòng chọn 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Bạn không được cấp pass cho người quản lý, vui lòng chọn 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #endregion

        //--------------------------------------------Xu Ly Xe--------------------------------------------'
        #region "Xu ly cac nut di chuyen xe da hoan tat "
        private void btn_Top_Click(object sender, EventArgs e)
        {
            Xe.Di_chuyen_ve_dau();
        }

        private void btn_Xe_Back_Click(object sender, EventArgs e)
        {
            Xe.Di_chuyen_ve_truoc();
        }

        private void btn_Xe_Next_Click(object sender, EventArgs e)
        {
            Xe.Di_chuyen_ve_sau();
        }

        private void btn_Xe_End_Click(object sender, EventArgs e)
        {
            Xe.Di_chuyen_ve_cuoi();
        }

        #endregion

        #region "Xu ly them, sua , xoa Xe da hoan tat"
        private void btn_ThemXe_Click(object sender, EventArgs e)
        {
            Xe.Them_Xe();
        }

        private void btn_SuaXe_Click(object sender, EventArgs e)
        {
            Xe.Sua_Xe();
        }

        private void btn_HuyXe_Click(object sender, EventArgs e)
        {
            Xe.Huy_thao_tac();
        }

        private void btn_LuuXe_Click(object sender, EventArgs e)
        {
            Xe.Luu_thay_doi();
        }

        private void btn_XoaXe_Click(object sender, EventArgs e)
        {
            Xe.Xoa_Xe();
        }


        #endregion

        #region "Su kien nhan enter tren Luoi_xe"
        private void Luoi_Xe_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbo_SoXe.Text = Luoi_Xe["So_Xe", e.RowIndex].EditedFormattedValue.ToString();
            cbo_HieuXe.Text = Luoi_Xe["Hieu_Xe", e.RowIndex].EditedFormattedValue.ToString();
            txt_DoiXe.Text = Luoi_Xe["Doi_Xe", e.RowIndex].EditedFormattedValue.ToString();
            cbo_SoChoNgoi.Text = Luoi_Xe["So_Cho_Ngoi", e.RowIndex].EditedFormattedValue.ToString();
        }

        #endregion

        //--------------------------------------------------Tuyen Xe----------------------------------------------------'
        #region "Xu ly cac button di chuyen da xong"
        private void btn_First_Tuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Di_chuyen_ve_dau();
        }

        private void btn_Previous_Tuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Di_chuyen_ve_truoc();
        }

        private void btn_Next_Tuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Di_chuyen_ve_sau();
        }

        private void btn_Last_Tuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Di_chuyen_ve_cuoi();
        }

        #endregion

        #region "Xu ly su kien them, xoa , sua hoan tat"
        private void btn_ThemTuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Them();
        }

        private void btn_SuaTuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Sua();
        }

        private void btn_XoaTuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Xoa();
        }

        private void btn_LuuTuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Luu();
        }

        private void btn_HuyTuyen_Click(object sender, EventArgs e)
        {
            Tuyen_xe.Huy_thao_tac();
        }

        #endregion
              
        private void btn_XemChiTietTuyen_Click(object sender, EventArgs e)
        {
            Form_ChiTietTuyen frm_ChiTietTuyen = new Form_ChiTietTuyen();
            frm_ChiTietTuyen.ShowDialog();
        }

        //----------------------------------------------------Thoi diem-----------------------------------------------'
        #region "Đã xong"
        private void btn_ThemThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.them();
        }

        private void btn_SuaThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Sua();
        }

        private void btn_HuyThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Huy();
        }

        private void btn_LuuThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Luu();
        }
            
        private void btn_XoaThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Xoa();
        }

        private void rad_LapTuan_CheckedChanged(object sender, EventArgs e)
        {
            lbl_Lap.Show();
            date_NgayKetThuc.Show();
        }

        private void btn_GanTuyen_Click(object sender, EventArgs e)
        {
            Thoi_diem.Gan_tuyen();
        }

        #endregion

        //----------------------------------------------------Chuyen Xe------------------------------------------'
        #region "Da xong"
        private void btn_ThemChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Them();
        }

        private void btn_SuaChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Sua();
        }

        private void btn_LuuChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Luu();
        }

        private void btn_HuyChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Huy();
        }

        private void btn_XoaChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Xoa();
        }

        private void cbo_IdTuyenChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chuyen_Xe.Chon_tuyen();
        }

        private void cbo_NgayDiChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chuyen_Xe.Chon_ngay();
        }

        private void btn_FirstChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Di_chuyen_ve_dau();
        }

        private void btn_PreviousChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Di_chuyen_ve_truoc();
        }

        private void btn_NextChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Di_chuyen_ve_sau();
        }

        private void btn_LastChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Di_chuyen_ve_cuoi();
        }

        #endregion

        //-------------------------------------------------Ban Ve-----------------------------------------------'
        #region "Da xong"
        private void cbo_TenTuyenVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ban_ve.Chon_tuyen();
        }

        private void cbo_NgayVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ban_ve.Chon_ngay();
        }

        private void cbo_GioVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ban_ve.Chon_xe();
        }

        private void cbo_XeVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ban_ve.Chon_thong_tin_xe();
        }

        private void btn_ChonChoNgoi_Click(object sender, EventArgs e)
        {
            Ban_ve.Chon_cho_ngoi();
        }

        private void Button_PhanQuyen_Click(object sender, EventArgs e)
        {
            if (cbo_IdLoaiND.Text == "Nhan_Vien")
            {
                Form_Phan_Quyen frm_PhanQuyen = new Form_Phan_Quyen();
                frm_PhanQuyen.Show();
            }
            else
            {
                MessageBox.Show("Bạn chỉ được cấp quyền cho nhân viên thôi, vui lòng chọn 1 nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        #endregion

        private void buttonX14_Click(object sender, EventArgs e)
        {
            Infomation frm_Info = new Infomation();
            frm_Info.Show();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {

        }

        private void cbo_Username_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void Luoi_Chuyen_xe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
