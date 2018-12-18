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
          
        //public Form_Login fl;//khởi tạo
        private bool flag = true;
        private Nguoi_dung Nguoi_dung = new Nguoi_dung();
        private Xe Xe = new Xe();
        private Tuyen_xe Tuyen_xe = new Tuyen_xe();
        private Thoi_diem Thoi_diem = new Thoi_diem();
        private Chuyen_xe Chuyen_Xe = new Chuyen_xe();
        private Ban_ve Ban_ve = new Ban_ve();
        private Form_Phan_Quyen Quyen = new Form_Phan_Quyen();
        private Update_he_thong update_he_thong = new Update_he_thong();

       
        private DataTable bang_Nguoi_Dung;
        public int vi_tri_hien_hanh;

        public Form_Main()
        {           
            InitializeComponent();
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
        private void Form_Main_Load(object sender, EventArgs e)
        {        
            update_he_thong.update_();
            UpdateNguoiDung();
            UpdateXe();
            Tuyen_xe.UpdateTuyenXe();
            Thoi_diem.Update_thoi_diem();
            Chuyen_Xe.Update_Chuyen_xe();
            Ban_ve.Update_Ve_xe();
            Quyen.UpdateQuyen();
            Timer1.Interval = 1000;

            Timer2.Interval = 100;
            Timer2.Start();
        }
        #region "Cac su kien Close, Logout cua FormMain da xu ly xong"
        private void ButtonX_Close_Click(object sender, EventArgs e)
        {
            Form_Login fl;
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
            Form_Login fl;
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
            Form_Login fl;
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

        private void btn_QuanLyXe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 1;
        }

        private void btn_QuanLyTuyenXe_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTabIndex = 2;
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
            Di_chuyen_ve_truoc();
        }

        private void btnHead_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_dau();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_sau();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
           Di_chuyen_ve_cuoi();
        }

        #endregion

        #region "Xu ly su kien them, sua, xoa nguoi dung da hoan tat"
        private void Button_Them_Click(object sender, EventArgs e)
        {
            Them_nguoi_dung();
        }

        private void Button_Sua_Click(object sender, EventArgs e)
        {
            Sua_thong_tin_ca_nhan();
        }

        private void Button_Xoa_Click(object sender, EventArgs e)
        {
            Xoa_nguoi_dung();
        }

        private void Button_Luu_Click(object sender, EventArgs e)
        {
            Luu_thay_doi();
        }

        private void Button_Huy_Click(object sender, EventArgs e)
        {
            Huy_thao_tac();
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
            Di_chuyen_ve_dau_Xe();
        }

        private void btn_Xe_End_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_cuoi_Xe();
        }

        private void btn_Xe_Next_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_sau_Xe();
        }

        private void btn_Xe_Back_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_truoc_Xe();
        }
        #endregion

        #region "Xu ly them, sua , xoa Xe da hoan tat"
        private void btn_ThemXe_Click(object sender, EventArgs e)
        {
            Them_Xe();
        }

        private void btn_SuaXe_Click(object sender, EventArgs e)
        {
            Sua_Xe();
        }

        private void btn_XoaXe_Click(object sender, EventArgs e)
        {
            Xoa_Xe();
        }

        private void btn_LuuXe_Click(object sender, EventArgs e)
        {
            Luu_thay_doi();
        }

        private void btn_HuyXe_Click(object sender, EventArgs e)
        {
            Huy_thao_tac();
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
        private void btn_XemChiTietTuyen_Click(object sender, EventArgs e)
        {
            Form_ChiTietTuyen frm_ChiTietTuyen = new Form_ChiTietTuyen();
            frm_ChiTietTuyen.ShowDialog();
        }

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

        private void btn_XoaThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Xoa();
        }

        private void btn_LuuThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Luu();
        }

        private void btn_HuyThoiDiem_Click(object sender, EventArgs e)
        {
            Thoi_diem.Huy();
        }

        private void btn_GanTuyen_Click(object sender, EventArgs e)
        {
            Thoi_diem.Gan_tuyen();
        }

        private void rad_LapTuan_CheckedChanged(object sender, EventArgs e)
        {
            lbl_Lap.Show();
            date_NgayKetThuc.Show();
        }
        #endregion

        //----------------------------------------------------Chuyen Xe------------------------------------------'
        #region "Da xong"
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

        private void btn_ThemChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Them();
        }

        private void btn_SuaChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Sua();
        }

        private void btn_XoaChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Xoa();
        }

        private void btn_LuuChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Luu();
        }

        private void btn_HuyChuyen_Click(object sender, EventArgs e)
        {
            Chuyen_Xe.Huy();
        }

        private void cbo_IdTuyenChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chuyen_Xe.Chon_tuyen();
        }

        private void cbo_NgayDiChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chuyen_Xe.Chon_ngay();
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

        //Xu ly Nguoidung
        #region "Xử lý class Nguoi_dung hoàn tất"

        public void UpdateNguoiDung()
        {
            Form_Login fl;
            fl = new Form_Login();
            if (fl.LoginLoaiND != "Quan_Ly" || fl.LoginLoaiND != "Admin")
            {
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket();
                luoi_NguoiDung.ReadOnly = true;
                Lock_Control(false);
                LockButton(false);
            }
            else
            {
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket();
                luoi_NguoiDung.ReadOnly = true;
                Button_Them.Enabled = false;
                Button_Sua.Enabled = false;
                Button_Xoa.Enabled = false;
                Button_CapPass.Enabled = false;
                Button_PhanQuyen.Text = "Xem Quyền";
            }
        }

        #region "Xu ly doc bang nguoi dung va phan loai nguoi dung de hien thi da hoan tat"
        public void Doc_bang_Nguoi_Dung()
        {
            Form_Login fl;
            fl = new Form_Login();
            //Lam sach luoi sau moi lan cap nhat
            luoi_NguoiDung.ClearSelection();
            string lenh = null;
            if (fl.LoginLoaiND == "Quan_Ly")
            {
                lenh = "Select * from NguoiDung where IdLoaiND = 'Nhan_Vien' or IdNguoiDung = '" + fl.LoginTenND + "'";
            }
            else if (fl.LoginLoaiND == "Nhan_Vien")
            {
                lenh = "Select * from NguoiDung where IdNguoiDung = '" + fl.txtUserName.Text + "'";
            }
            else
            {
                lenh = "Select * from NguoiDung";
            }
            bang_Nguoi_Dung = Ket_noi.Doc_bang(lenh);
            luoi_NguoiDung.DataSource = bang_Nguoi_Dung;
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin nguoi dung da hoan tat"
        private void Xuat_thong_tin_Nguoi_Dung()
        {
            //fm = new Form_Main();
            DataRow dong = bang_Nguoi_Dung.Rows[vi_tri_hien_hanh];
            var _with1=this;
            _with1.cbo_Username.Text = dong["IdNguoiDung"].ToString();
            _with1.txt_Password.Text = dong["PassND"].ToString();
            _with1.txt_HoTen.Text = Convert.ToString(dong["HoTen"]);
            _with1.date_NgaySinh.Text = Convert.ToString(dong["NgaySinh"]);
            if (dong["GioiTinh"].ToString() == "Nam")
            {
                _with1.radNam.Checked = true;
            }
            else
            {
                _with1.radNu.Checked = true;
            }
            _with1.txt_DiaChi.Text = dong["DiaChi"].ToString();
            _with1.txt_SoDienThoai.Text = dong["SoDT"].ToString();
            _with1.cbo_IdLoaiND.Text = dong["IdLoaiND"].ToString();
        }

        public void Di_chuyen_ve_sau()
        {
            if (vi_tri_hien_hanh < bang_Nguoi_Dung.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Nguoi_Dung();
            }
        }

        public void Di_chuyen_ve_truoc()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Nguoi_Dung();
            }
        }

        public void Di_chuyen_ve_dau()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Nguoi_Dung();
        }

        public void Di_chuyen_ve_cuoi()
        {
            vi_tri_hien_hanh = bang_Nguoi_Dung.Rows.Count - 1;
            Xuat_thong_tin_Nguoi_Dung();
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket()
        {
            Form_Login fl;
            //fm = new Form_Main();
            fl = new Form_Login();
            SqlCommand query = new SqlCommand("select IdLoaiND from LoaiNguoiDung", Ket_noi.connect);
            Ket_noi.connect.Open();
            SqlDataReader dr = query.ExecuteReader();
            cbo_IdLoaiND.Items.Clear();
            while (dr.Read() == true)
            {
                if (fl.LoginLoaiND == "Admin")
                {
                    cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                }
                else if (fl.LoginLoaiND == "Quan_Ly")
                {
                    if (dr.GetValue(0).ToString() != "Admin")
                    {
                        cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                    }
                }
                else
                {
                    if (dr.GetValue(0).ToString() != "Admin" && dr.GetValue(0).ToString() != "Quan_Ly")
                    {
                        cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                    }
                }
            }
            Ket_noi.connect.Close();
            var _with2 = cbo_Username;
            _with2.DataSource = luoi_NguoiDung.DataSource;
            _with2.DisplayMember = "IdNguoiDung";
            _with2.ValueMember = "IdNguoiDung";
            _with2.SelectedValue = "IdNguoiDung";
            Xoa_lien_ket();
            //Tao gia tri mac dinh la IdNguoiDung dong thu 0 cot 0 luc khoi dong vi IdNguoiDung la member ko lien ket duoc
            cbo_Username.Text = (String)luoi_NguoiDung.Rows[0].Cells[0].Value;
            txt_Password.DataBindings.Add("text", luoi_NguoiDung.DataSource, "PassND");
            txt_DiaChi.DataBindings.Add("text", luoi_NguoiDung.DataSource, "DiaChi");
            txt_HoTen.DataBindings.Add("text", luoi_NguoiDung.DataSource, "HoTen");
            txt_SoDienThoai.DataBindings.Add("text", luoi_NguoiDung.DataSource, "SoDT");
            date_NgaySinh.DataBindings.Add("text", luoi_NguoiDung.DataSource, "NgaySinh");
            cbo_IdLoaiND.DataBindings.Add("text", luoi_NguoiDung.DataSource, "IdLoaiND");
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket()
        {

            //fm = new Form_Main();
            txt_Password.DataBindings.Clear();
            txt_DiaChi.DataBindings.Clear();
            txt_HoTen.DataBindings.Clear();
            txt_SoDienThoai.DataBindings.Clear();
            cbo_IdLoaiND.DataBindings.Clear();
            date_NgaySinh.DataBindings.Clear();
        }
        #endregion

        #region "Them va sua thong tin nguoi dung da ly ly xong"
        public void Them_nguoi_dung()
        {

            //fm = new Form_Main();
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Clear_Control();
            luoi_NguoiDung.ReadOnly = false;
        }

        public void Sua_thong_tin_ca_nhan()
        {           
            Form_Login fl;
            //fm = new Form_Main();
            fl = new Form_Login();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            cbo_Username.Focus();
            cbo_Username.Text = fl.LoginTenND;
            cbo_Username.Enabled = false;
            luoi_NguoiDung.ReadOnly = false;
            cbo_IdLoaiND.Enabled = false;
        }

        public void Luu_thay_doi()
        {           
            Form_Login fl;
            //fm = new Form_Main();
            fl = new Form_Login();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with3 = this;
            //Them nguoi dung moi
            if (flag == true)
            {
                if (TestInfo())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm người dùng này không, " + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dg == System.Windows.Forms.DialogResult.OK)
                    {
                        int flag = 0;
                        SqlCommand sqlCM = new SqlCommand("select IdNguoiDung from NguoiDung ", Ket_noi.connect);
                        SqlDataReader sqlDR = null;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == _with3.cbo_Username.Text)
                            {
                                flag = 1;
                                Interaction.MsgBox("Tài khoản " + _with3.cbo_Username.Text + " đã được sử dụng !", MsgBoxStyle.OkOnly, "Thông Báo");
                            }
                        }
                        Ket_noi.connect.Close();

                        if (flag == 0)
                        {
                            SqlCommand sqlqr = new SqlCommand();
                            sqlqr.Connection = Ket_noi.connect;
                            if (_with3.radNam.Checked == true)
                            {
                                sqlqr.CommandText = "insert into NguoiDung values('" + _with3.cbo_Username.Text + "','" + _with3.txt_Password.Text + "',N'" + _with3.txt_HoTen.Text + "','" + Strings.Format(_with3.date_NgaySinh.Value, "yyyy/MM/dd") + "',N'Nam',N'" + _with3.txt_DiaChi.Text + "'," + _with3.txt_SoDienThoai.Text + ",'" + _with3.cbo_IdLoaiND.Text + "')";
                            }
                            else
                            {
                                sqlqr.CommandText = "insert into NguoiDung values('" + _with3.cbo_Username.Text + "','" + _with3.txt_Password.Text + "',N'" + _with3.txt_HoTen.Text + "','" + Strings.Format(_with3.date_NgaySinh.Value, "yyyy/MM/dd") + "',N'Nữ',N'" + _with3.txt_DiaChi.Text + "'," + _with3.txt_SoDienThoai.Text + ",'" + _with3.cbo_IdLoaiND.Text + "')";
                            }

                            Ket_noi.connect.Open();
                            try
                            {
                                sqlqr.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                Doc_bang_Nguoi_Dung();
                                vi_tri_hien_hanh = 0;
                                Xuat_thong_tin_Nguoi_Dung();
                                Tao_lien_ket();
                                Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                Interaction.MsgBox("Một số kí tự trong ô 'Họ Tên' và 'Địa Chỉ' không phù hợp" + Constants.vbNewLine + "Các kí có thể nhập là 0 - 9, 26 chữ cái, '_', các dấu trong tiếng việt và một số kí tự khác", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                                Ket_noi.connect.Close();
                            }
                        }
                    }
                    else
                    {
                        Huy_thao_tac();
                    }

                }
                //Sua thong tin nguoi dung
            }
            else
            {
                if (TestInfo())
                {
                    if (cbo_Username.Text != fl.LoginTenND)
                    {
                        DialogResult dg = MessageBox.Show("Ban chỉ có quyền sưa thông tin cá nhân của mình, " + Constants.vbNewLine + "Click OK đê tiếp tục sửa thông tin, Cancel để hủy thao tác", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dg == System.Windows.Forms.DialogResult.OK)
                        {
                            Sua_thong_tin_ca_nhan();
                            return;
                        }
                        else
                        {
                            Huy_thao_tac();
                            return;
                        }
                    }

                    DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin cá nhân., " + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        SqlCommand sqlqr = new SqlCommand();
                        sqlqr.Connection = Ket_noi.connect;
                        if (_with3.radNam.Checked)
                        {
                            sqlqr.CommandText = "update NguoiDung set PassND='" + _with3.txt_Password.Text + "',HoTen=N'" + _with3.txt_HoTen.Text + "',NgaySinh='" + Strings.Format(_with3.date_NgaySinh.Value, "yyyy/MM/dd") + "',GioiTinh=N'Nam',DiaChi=N'" + _with3.txt_DiaChi.Text + "',SoDT=" + _with3.txt_SoDienThoai.Text + ",IdLoaiND='" + _with3.cbo_IdLoaiND.Text + "' where IdNguoiDung='" + _with3.cbo_Username.Text + "'";
                        }
                        else
                        {
                            sqlqr.CommandText = "update NguoiDung set PassND='" + _with3.txt_Password.Text + "',HoTen=N'" + _with3.txt_HoTen.Text + "',NgaySinh='" + Strings.Format(_with3.date_NgaySinh.Value, "yyyy/MM/dd") + "',GioiTinh=N'Nữ',DiaChi=N'" + _with3.txt_DiaChi.Text + "',SoDT=" + _with3.txt_SoDienThoai.Text + ",IdLoaiND='" + _with3.cbo_IdLoaiND.Text + "' where IdNguoiDung='" + _with3.cbo_Username.Text + "'";
                        }

                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Lock_Control(false);
                            LockButton(false);
                            luoi_NguoiDung.Enabled = true;
                            UpdateNguoiDung();
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox("Một số kí tự bạn nhập không phù hợp" + Constants.vbNewLine + "Các kí có thể nhập là 0 - 9, 26 chữ cái, _ @ * % $ & - ~ và một số kí tự khác", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                    {
                        Huy_thao_tac();
                    }
                }
            }
        }
        #endregion

        #region "Xu ly huy thao tac cap nhat da hoan tat"
        public void Huy_thao_tac()
        {
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            UpdateNguoiDung();
        }
        #endregion

        #region "Xu ly xoa nguoi dung da hoan tat"
        public void Xoa_nguoi_dung()
        {
            Form_Login fl;
            //fm = new Form_Main();
            fl = new Form_Login();
            if (Strings.Trim(cbo_Username.Text) == fl.LoginTenND)
            {
                DialogResult dg = MessageBox.Show("Ban không được quyền xóa thông tin của chính bạn được. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - User name người dùng: " + cbo_Username.Text + Constants.vbNewLine + " - Tên: " + txt_HoTen.Text + Constants.vbNewLine + " - Số điện thoại: " + txt_SoDienThoai.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (qs == DialogResult.Yes)
                {
                    string lenh = "Delete from NguoiDung where IdNguoiDung = '" + cbo_Username.SelectedValue.ToString() + "'";
                    SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                    try
                    {
                        Ket_noi.connect.Open();
                        query1.ExecuteNonQuery();
                        Ket_noi.connect.Close();
                        UpdateNguoiDung();
                        Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        Interaction.MsgBox("Xóa không thành công", Constants.vbExclamation, "Thông báo");
                    }
                }
                else
                {
                    Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
                }
            }
        }
        #endregion

        #region "Cac xu ly phu voi cac dieu khien da hoan tat"
        private void Lock_Control(bool f)
        {

            //fm = new Form_Main();
            var _with4 = this;
            _with4.cbo_Username.Enabled = true;
            _with4.txt_Password.Enabled = f;
            _with4.txt_HoTen.Enabled = f;
            _with4.date_NgaySinh.Enabled = f;
            _with4.radNam.Enabled = f;
            _with4.radNu.Enabled = f;
            _with4.cbo_IdLoaiND.Enabled = f;
            _with4.txt_SoDienThoai.Enabled = f;
            _with4.txt_DiaChi.Enabled = f;
        }

        private void Clear_Control()
        {

            //fm = new Form_Main();
            var _with5 = this;
            _with5.txt_Password.Text = "";
            _with5.txt_DiaChi.Text = "";
            _with5.txt_HoTen.Text = "";
            _with5.txt_SoDienThoai.Text = "";
            _with5.radNu.Checked = true;
            _with5.cbo_Username.Text = "";
            _with5.date_NgaySinh.Text = "";
            _with5.cbo_IdLoaiND.Text = "Nhan_Vien";
            _with5.cbo_Username.Focus();
        }

        private void LockButton(bool dt)
        {

            //fm = new Form_Main();
            var _with6 = this;
            _with6.Button_Them.Enabled = !dt;
            _with6.Button_Sua.Enabled = !dt;
            _with6.Button_Xoa.Enabled = !dt;
            _with6.Button_Luu.Enabled = dt;
            _with6.Button_Huy.Enabled = dt;
            _with6.Button_PhanQuyen.Enabled = !dt;
            _with6.Button_CapPass.Enabled = !dt;
        }

        private bool TestInfo()
        {

            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with7 = this;
            if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_Username.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.txt_Password.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.txt_HoTen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.date_NgaySinh.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cbo_IdLoaiND.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.txt_SoDienThoai.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.txt_DiaChi.Text)))
            {
                functionReturnValue = false;
                Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
            }

            if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_Username.Text)))
            {
                _with7.cbo_Username.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.txt_Password.Text)))
            {
                _with7.txt_Password.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.txt_HoTen.Text)))
            {
                _with7.txt_HoTen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.date_NgaySinh.Text)))
            {
                _with7.date_NgaySinh.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.txt_SoDienThoai.Text)))
            {
                _with7.txt_SoDienThoai.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.txt_DiaChi.Text)))
            {
                _with7.txt_DiaChi.Focus();
                return functionReturnValue;
            }

            if (Strings.Trim(_with7.txt_Password.Text).Length < 5)
            {
                functionReturnValue = false;
                Interaction.MsgBox("Password không được ít hơn 5 kí tự!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                _with7.txt_Password.Focus();
                return functionReturnValue;
            }

            if (Strings.Trim(_with7.cbo_IdLoaiND.Text) != "Quan_Ly" && Strings.Trim(_with7.cbo_IdLoaiND.Text) != "Nhan_Vien" && Strings.Trim(_with7.cbo_IdLoaiND.Text) != "Admin")
            {
                functionReturnValue = false;
                Interaction.MsgBox("Loại người dùng chỉ có thể là QL(Quản Lý) hoặc NV(Nhân Viên)", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                _with7.cbo_IdLoaiND.Focus();
                return functionReturnValue;
            }

            if (Strings.Trim(_with7.txt_SoDienThoai.Text).Length > 11)
            {
                functionReturnValue = false;
                Interaction.MsgBox("Số điện thoại không được quá 11 số", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                _with7.txt_SoDienThoai.Focus();
                return functionReturnValue;
            }
            return functionReturnValue;
        }
        #endregion
        #endregion

        //Xu ly Xe
        #region "Xử lý class Xe hoàn tất"
        private string lenh;
        private DataTable bang_xe;       
        public void UpdateXe()
        {
            //fm = new Form_Main();
            Doc_bang_Xe();
            Tao_lien_ket_Xe();
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Xe();
            Luoi_Xe.ReadOnly = true;
            Lock_Control_Xe(false);
            LockButton_Xe(false);

            var _with1 = cbo_HieuXe.Items;
            _with1.Add("Ford Transit");
            _with1.Add("Huyndai Country");
            _with1.Add("Toyota");
            _with1.Add("Ford Everest");
            _with1.Add("Huyndai");
            //Doc du lieu tu bang_xe vao cbo_HieuXe
            //Dim dem As Integer = 0
            //For i As Integer = 0 To bang_xe.Rows.Count - 1
            //    For j As Integer = 0 To Form_Main.cbo_HieuXe.Items.Count - 1
            //        If Form_Main.cbo_HieuXe.Items(j).ToString = bang_xe.Rows(i)("Hieu_Xe").ToString Then
            //            dem += 1
            //        End If
            //    Next
            //    If dem = 0 Then
            //        Form_Main.cbo_HieuXe.Items.Add(bang_xe.Rows(i)("Hieu_Xe").ToString)
            //    End If
            //    dem = 0
            //Next
        }

        #region "Doc bang xe da xu ly xong"
        private void Doc_bang_Xe()
        {
            //fm = new Form_Main();
            //Lam sach luoi sau moi lan cap nhat
            Luoi_Xe.ClearSelection();
            lenh = "Select * from Xe";
            bang_xe = Ket_noi.Doc_bang(lenh);
            Luoi_Xe.DataSource = bang_xe;
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin xe da hoan tat"
        private void Xuat_thong_tin_Xe()
        {
            //fm = new Form_Main();
            DataRow dong = bang_xe.Rows[vi_tri_hien_hanh];
            var _with2 = this;
            _with2.cbo_SoXe.Text = dong["So_Xe"].ToString();
            _with2.cbo_HieuXe.Text = dong["Hieu_Xe"].ToString();
            _with2.txt_DoiXe.Text = Convert.ToString(dong["Doi_Xe"]);
            _with2.cbo_SoChoNgoi.Text = dong["So_Cho_Ngoi"].ToString();
        }

        public void Di_chuyen_ve_sau_Xe()
        {
            if (vi_tri_hien_hanh < bang_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_truoc_Xe()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_dau_Xe()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Xe();
        }

        public void Di_chuyen_ve_cuoi_Xe()
        {
            vi_tri_hien_hanh = bang_xe.Rows.Count - 1;
            Xuat_thong_tin_Xe();
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket_Xe()
        {
            //fm = new Form_Main();
            if (string.IsNullOrEmpty(cbo_SoChoNgoi.Text))
            {
                var _with3 = cbo_SoChoNgoi;
                _with3.Items.Add(7);
                _with3.Items.Add(16);
                _with3.Items.Add(25);
                _with3.Items.Add(30);
                _with3.Items.Add(45);
            }
            var _with4 = cbo_SoXe;
            _with4.DataSource = Luoi_Xe.DataSource;
            _with4.DisplayMember = "So_Xe";
            _with4.ValueMember = "So_Xe";
            _with4.SelectedValue = "So_Xe";
            Xoa_lien_ket_Xe();
            //Form_Main.cbo_IdXe.DataBindings.Add("text", Form_Main.Luoi_Xe.DataSource, "Id_Xe")
            //Do Id xe la value member nen ta se khoi tao no luc moi load form va
            //------------Cach1 nhung khong hay, ta nen huy vung nho cua bang sau moi lan xai xong
            //Form_Main.cbo_IdXe.Text = bang_xe.Rows(0)("Id_Xe").ToString 
            //-----------------Cach 2--------------------------
            cbo_SoXe.Text = (String)Luoi_Xe.Rows[0].Cells[0].Value;
            
            cbo_HieuXe.DataBindings.Add("text", Luoi_Xe.DataSource, "Hieu_Xe");
            txt_DoiXe.DataBindings.Add("text", Luoi_Xe.DataSource, "Doi_Xe");
            cbo_SoChoNgoi.DataBindings.Add("text", Luoi_Xe.DataSource, "So_Cho_Ngoi");
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket_Xe()
        {
            //fm = new Form_Main();
            cbo_SoXe.DataBindings.Clear();
            cbo_HieuXe.DataBindings.Clear();
            txt_DoiXe.DataBindings.Clear();
            cbo_SoChoNgoi.DataBindings.Clear();
        }
        #endregion

        #region "Them, sua xe da hoan tat"
        public void Them_Xe()
        {
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Clear_Control();
        }

        public void Sua_Xe()
        {
            //fm = new Form_Main();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            //Form_Main.cbo_SoXe.Enabled = False
            Luoi_Xe.ReadOnly = false;
            cbo_SoXe.Enabled = false;
        }

        public void Luu_thay_doi_Xe()
        {
            //fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with5 = this;
            //Them xe moi
            if (flag == true)
            {
                if (TestInfo())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm xe này không." + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dg == System.Windows.Forms.DialogResult.OK)
                    {
                        int flag = 0;
                        SqlCommand sqlCM = new SqlCommand("select So_Xe from Xe", Ket_noi.connect);
                        SqlDataReader sqlDR = null;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == _with5.cbo_SoXe.Text)
                            {
                                flag = 1;
                                MessageBox.Show("Số xe " + _with5.cbo_SoXe.Text + " đã tồn tại, vui lòng kiểm tra lại số xe bạn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        Ket_noi.connect.Close();
                        if (flag == 0)
                        {
                            lenh = "Insert into Xe(So_Xe, Hieu_Xe, Doi_Xe, So_Cho_Ngoi)";
                            lenh += " Values ('" + _with5.cbo_SoXe.Text + "', '" + _with5.cbo_HieuXe.Text + "', '" + _with5.txt_DoiXe.Text + "', '" + _with5.cbo_SoChoNgoi.Text + "')";
                            SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                            Ket_noi.connect.Open();
                            try
                            {
                                bo_lenh.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                Doc_bang_Xe();
                                Tao_lien_ket();
                                vi_tri_hien_hanh = 0;
                                Xuat_thong_tin_Xe();
                                cbo_HieuXe.Items.Clear();
                                Luoi_Xe.Enabled = true;
                                Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không cập nhật được dữ liệu, thêm xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                            }
                        }
                    }
                    else
                    {
                        Huy_thao_tac_Xe();
                    }
                }
            }
            else
            {
                //Sua thong tin nguoi dung

                if (TestInfo())
                {
                    DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin xe này." + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        lenh = "Update Xe Set Hieu_Xe = '" + _with5.cbo_HieuXe.Text + "', Doi_Xe = '" + _with5.txt_DoiXe.Text + "', So_Cho_Ngoi = '" + _with5.cbo_SoChoNgoi.Text + "' where So_Xe = '" + _with5.cbo_SoXe.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            UpdateXe();
                            Lock_Control(false);
                            LockButton(false);
                            Luoi_Xe.Enabled = true;
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, sửa thông tin xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                    {
                        Huy_thao_tac_Xe();
                    }
                }
            }
        }
        #endregion

        #region "Xu ly huy thao tac cap nhat da hoan tat"
        public void Huy_thao_tac_Xe()
        {
            //fm = new Form_Main();
            Luoi_Xe.Enabled = true;
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            UpdateXe();
        }
        #endregion

        #region "Xoa xe hoan tat"
        public void Xoa_Xe()
        {
            //fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - So xe: " + cbo_SoXe.Text + Constants.vbNewLine + " - Hieu xe: " + cbo_HieuXe.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from Xe where So_Xe = '" + cbo_SoXe.SelectedValue.ToString() + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    UpdateXe();
                    Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Không cập nhật được dữ liệu, xóa xe không thành công", Constants.vbExclamation, "Thông báo");
                }
            }
            else
            {
                Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
            }
        }
        #endregion

        #region "Cac xu ly phu voi cac dieu khien da hoan tat"
        private void Lock_Control_Xe(bool f)
        {
           //fm = new Form_Main();
            var _with6 = this;
            _with6.cbo_SoXe.Enabled = true;
            _with6.cbo_HieuXe.Enabled = f;
            _with6.txt_DoiXe.Enabled = f;
            _with6.cbo_SoChoNgoi.Enabled = f;
        }

        private void Clear_Control_Xe()
        {
            //fm = new Form_Main();
            var _with7 = this;
            _with7.cbo_SoXe.Text = "";
            _with7.cbo_HieuXe.Text = "";
            _with7.txt_DoiXe.Text = "";
            _with7.cbo_SoXe.Focus();
        }

        private void LockButton_Xe(bool dt)
        {
            //fm = new Form_Main();
            var _with8 = this;
            _with8.btn_ThemXe.Enabled = !dt;
            _with8.btn_SuaXe.Enabled = !dt;
            _with8.btn_XoaXe.Enabled = !dt;
            _with8.btn_LuuXe.Enabled = dt;
            _with8.btn_HuyXe.Enabled = dt;
        }

        private bool TestInfo_Xe()
        {
            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with9 = this;
            if (string.IsNullOrEmpty(Strings.Trim(_with9.cbo_SoXe.Text)) || string.IsNullOrEmpty(Strings.Trim(_with9.cbo_HieuXe.Text)) || string.IsNullOrEmpty(Strings.Trim(_with9.txt_DoiXe.Text)) || string.IsNullOrEmpty(Strings.Trim(_with9.cbo_SoChoNgoi.Text)))
            {
                functionReturnValue = false;
                Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
            }

            if (string.IsNullOrEmpty(Strings.Trim(_with9.cbo_SoXe.Text)))
            {
                _with9.cbo_SoXe.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with9.cbo_HieuXe.Text)))
            {
                _with9.cbo_HieuXe.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with9.txt_DoiXe.Text)))
            {
                _with9.txt_DoiXe.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with9.cbo_SoChoNgoi.Text)))
            {
                _with9.cbo_SoChoNgoi.Focus();
                return functionReturnValue;
            }

            if (Strings.Trim(_with9.cbo_SoXe.Text).Length > 8)
            {
                functionReturnValue = false;
                Interaction.MsgBox("Số xe không được lớn hơn 8 kí tự!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                _with9.txt_Password.Focus();
                return functionReturnValue;
            }
            return functionReturnValue;

        }
        #endregion
        #endregion
    }
}

        
