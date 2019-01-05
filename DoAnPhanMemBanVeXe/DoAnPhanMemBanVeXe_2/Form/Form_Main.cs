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
          
        public Form_Login fl;//khởi tạo
        private bool flag = true;
        private Nguoi_dung Nguoidung = new Nguoi_dung();
        private Xe Xe = new Xe();
        public Tuyen_xe Tuyenxe = new Tuyen_xe();
        private Thoi_diem Thoidiem = new Thoi_diem();
        private Chuyen_xe ChuyenXe = new Chuyen_xe();
        private Ban_ve Banve = new Ban_ve();
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
            UpdateTuyenXe();
            Update_thoi_diem();
            Update_Chuyen_xe();
            Update_Ve_xe_ban_ve();
            Quyen.UpdateQuyen();
            Timer1.Interval = 1000;
            expandableSplitter1.Height = 500;
            Timer2.Interval = 100;
            Timer2.Start();
        }
        #region "Cac su kien Close, Logout cua FormMain da xu ly xong"
        private void ButtonX_Close_Click(object sender, EventArgs e)
        {
            //Form_Login fl;
            //fl = new Form_Login();
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
            //Form_Login fl;
            //fl = new Form_Login();
            fl.Visible = true;
            fl.Opacity = 100;
            fl.txtPassword.Clear();
            fl.Timer1.Start();
            fl.Timer2.Start();
            this.Close();
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form_Login fl;
            //fl = new Form_Login();
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
            Di_chuyen_ve_dau_tuyen_xe();
        }

        private void btn_Previous_Tuyen_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_truoc_tuyen_xe();
        }

        private void btn_Next_Tuyen_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_sau_tuyen_xe();
        }

        private void btn_Last_Tuyen_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_cuoi_tuyen_xe();
        }
        #endregion

        #region "Xu ly su kien them, xoa , sua hoan tat"
        private void btn_ThemTuyen_Click(object sender, EventArgs e)
        {
            Them_tuyen_xe();
        }

        private void btn_SuaTuyen_Click(object sender, EventArgs e)
        {
            Sua_tuyen_xe();
        }

        private void btn_XoaTuyen_Click(object sender, EventArgs e)
        {
            Xoa_tuyen_xe();
        }

        private void btn_LuuTuyen_Click(object sender, EventArgs e)
        {
            Luu_tuyen_xe();
        }

        private void btn_HuyTuyen_Click(object sender, EventArgs e)
        {
            Huy_thao_tac_tuyen_xe();
        }

        #endregion

        //----------------------------------------------------Thoi diem-----------------------------------------------'
        #region "Đã xong"
        private void btn_ThemThoiDiem_Click(object sender, EventArgs e)
        {
            them_thoi_diem();
        }

        private void btn_SuaThoiDiem_Click(object sender, EventArgs e)
        {
            Sua_thoi_diem();
        }

        private void btn_XoaThoiDiem_Click(object sender, EventArgs e)
        {
            Xoa_thoi_diem();
        }

        private void btn_LuuThoiDiem_Click(object sender, EventArgs e)
        {
            Luu_thoi_diem();
        }

        private void btn_HuyThoiDiem_Click(object sender, EventArgs e)
        {
            Huy_thoi_diem();
        }

        private void btn_GanTuyen_Click(object sender, EventArgs e)
        {
            Gan_tuyen();
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
            Di_chuyen_ve_dau_chuyen_xe();
        }

        private void btn_PreviousChuyen_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_truoc_chuyen_xe();
        }

        private void btn_NextChuyen_Click(object sender, EventArgs e)
        {
           Di_chuyen_ve_sau_chuyen_xe();
        }

        private void btn_LastChuyen_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_cuoi_chuyen_xe();
        }

        private void btn_ThemChuyen_Click(object sender, EventArgs e)
        {
            Them_chuyen_xe();
        }

        private void btn_SuaChuyen_Click(object sender, EventArgs e)
        {
            Sua_chuyen_xe();
        }

        private void btn_XoaChuyen_Click(object sender, EventArgs e)
        {
            Xoa_chuyen_xe();
        }

        private void btn_LuuChuyen_Click(object sender, EventArgs e)
        {
            Luu_chuyen_xe();
        }

        private void btn_HuyChuyen_Click(object sender, EventArgs e)
        {
            Huy_chuyen_xe();
        }

        private void cbo_IdTuyenChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_tuyen_chuyen_xe();
        }

        private void cbo_NgayDiChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_ngay();
        }

        #endregion

        //-------------------------------------------------Ban Ve-----------------------------------------------'
        #region "Da xong"
        private void cbo_TenTuyenVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_tuyen_ban_ve();
        }

        private void cbo_NgayVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Chon_ngay_ban_ve();
        }

        private void cbo_GioVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_xe_ban_ve();
        }

        private void cbo_XeVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_thong_tin_xe();
        }

        private void btn_ChonChoNgoi_Click(object sender, EventArgs e)
        {
            Chon_cho_ngoi();
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

        //Xu ly Nguoi_dung
        #region "Xử lý class Nguoi_dung hoàn tất"

        public void UpdateNguoiDung()
        {
            //Form_Login fl;
            //fl = new Form_Login();
            if (fl.LoaiND == "Quan_Ly" || fl.LoaiND == "Admin")
            //if(fl.getco() == 1 )
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
            //Form_Login fl;
            //fl = new Form_Login();
            //Lam sach luoi sau moi lan cap nhat
            luoi_NguoiDung.ClearSelection();
            string lenh = null;
            if (fl.LoaiND == "Quan_Ly")
            {
                lenh = "Select * from NguoiDung where IdLoaiND = 'Nhan_Vien' or IdNguoiDung = '" + fl.LoaiND + "'";
            }
            else if (fl.LoaiND == "Nhan_Vien")
            {
                lenh = "Select * from NguoiDung where IdNguoiDung = '" + fl.username + "'";
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
            //Form_Login fl;
            //fm = new Form_Main();
            //fl = new Form_Login();
            SqlCommand query = new SqlCommand("select IdLoaiND from LoaiNguoiDung", Ket_noi.connect);
            Ket_noi.connect.Open();
            SqlDataReader dr = query.ExecuteReader();
            cbo_IdLoaiND.Items.Clear();
            while (dr.Read() == true)
            {
                if (fl.LoaiND == "Admin")
                {
                    cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                }
                else if (fl.LoaiND == "Quan_Ly")
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
            //Form_Login fl;
            //fm = new Form_Main();
            //fl = new Form_Login();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            cbo_Username.Focus();
            cbo_Username.Text = fl.TenND;
            cbo_Username.Enabled = false;
            luoi_NguoiDung.ReadOnly = false;
            cbo_IdLoaiND.Enabled = false;
        }

        public void Luu_thay_doi()
        {           
            //Form_Login fl;
            //fm = new Form_Main();
            //fl = new Form_Login();
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
                    if (cbo_Username.Text != fl.TenND)
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
            //Form_Login fl;
            //fm = new Form_Main();
            //fl = new Form_Login();
            if (Strings.Trim(cbo_Username.Text) == fl.TenND)
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
            Lock_Control_Xe(true);
            LockButton_Xe(true);
            Clear_Control_Xe();
        }

        public void Sua_Xe()
        {
            //fm = new Form_Main();
            flag = false;
            Lock_Control_Xe(true);
            LockButton_Xe(true);
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
                                Tao_lien_ket_Xe();
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
                            Lock_Control_Xe(false);
                            LockButton_Xe(false);
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
            Xoa_lien_ket_Xe();
            Lock_Control_Xe(false);
            LockButton_Xe(false);
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

        //Xu ly Tuyen_Xe
        #region "Xử lý class Tuyen_Xe hoàn tất"
        private DataTable bang_tuyen_xe;        
        private string lenh_tuyen_xe;      
        public void UpdateTuyenXe()
        {
            //fm = new Form_Main();
            Doc_bang_tuyen_xe();
            Tao_lien_ket_tuyen_xe();
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Tuyen_xe();
            luoi_Tuyen_xe.ReadOnly = true;
            Lock_Control_tuyen_xe(false);
            LockButton_tuyen_xe(false);
        }

        #region "Doc bang tuyen xe da xong"
        private void Doc_bang_tuyen_xe()
        {
            //fm = new Form_Main();
            //Lam sach luoi sau moi lan cap nhat
            luoi_Tuyen_xe.ClearSelection();
            lenh_tuyen_xe = "Select * from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh_tuyen_xe);
            luoi_Tuyen_xe.DataSource = bang_tuyen_xe;
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket_tuyen_xe()
        {
            //fm = new Form_Main();
            var _with1 = cbo_IdTuyen;
            _with1.DataSource = luoi_Tuyen_xe.DataSource;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            _with1.SelectedValue = "IdTuyen";
            Xoa_lien_ket_tuyen_xe();

            cbo_IdTuyen.Text = (String)luoi_Tuyen_xe.Rows[0].Cells[0].Value;
            
            cbo_TenTuyen.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "TenTuyen");
            cbo_DiaDiemDi.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "DiaDiemDi");
            cbo_DiaDiemDen.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "DiaDiemDen");
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket_tuyen_xe()
        {
            //fm = new Form_Main();
            cbo_TenTuyen.DataBindings.Clear();
            cbo_DiaDiemDi.DataBindings.Clear();
            cbo_DiaDiemDen.DataBindings.Clear();
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin tuyen xe da hoan tat"
        private void Xuat_thong_tin_Tuyen_xe()
        {
            //fm = new Form_Main();
            DataRow dong = bang_tuyen_xe.Rows[vi_tri_hien_hanh];
            var _with2 = this;
            _with2.cbo_IdTuyen.Text = dong["IdTuyen"].ToString();
            _with2.cbo_TenTuyen.Text = dong["TenTuyen"].ToString();
            _with2.cbo_DiaDiemDi.Text = Convert.ToString(dong["DiaDiemDi"]);
            _with2.cbo_DiaDiemDen.Text = dong["DiaDiemDen"].ToString();
        }

        public void Di_chuyen_ve_sau_tuyen_xe()
        {
            if (vi_tri_hien_hanh < bang_tuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc_tuyen_xe()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau_tuyen_xe()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Tuyen_xe();
        }

        public void Di_chuyen_ve_cuoi_tuyen_xe()
        {
            vi_tri_hien_hanh = bang_tuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Tuyen_xe();
        }
        #endregion

        #region "Them, sua tuyen da hoan tat"
        public void Them_tuyen_xe()
        {
            //fm = new Form_Main();
            flag = true;
            Lock_Control_tuyen_xe(true);
            LockButton_tuyen_xe(true);
            Clear_Control_tuyen_xe();
            luoi_Tuyen_xe.Enabled = false;
            for (int i = 0; i <= bang_tuyen_xe.Rows.Count - 1; i++)
            {
                cbo_TenTuyen.Items.Add(bang_tuyen_xe.Rows[i]["TenTuyen"].ToString());
            }
        }

        public void Sua_tuyen_xe()
        {
            //fm = new Form_Main();
            flag = false;
            Lock_Control_tuyen_xe(true);
            LockButton_tuyen_xe(true);
            cbo_SoXe.Enabled = false;
            Luoi_Xe.ReadOnly = false;
            cbo_IdTuyen.Enabled = false;
        }

        public void Luu_tuyen_xe()
        {
            //fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with3 = this;
            //Them nguoi dung moi
            if (flag == true)
            {
                if (TestInfo_tuyen_xe())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm tuyến xe này không." + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dg == System.Windows.Forms.DialogResult.OK)
                    {
                        int flag = 0;
                        SqlCommand sqlCM = new SqlCommand("select IdTuyen from TuyenXe", Ket_noi.connect);
                        SqlDataReader sqlDR = null;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == _with3.cbo_IdTuyen.Text)
                            {
                                flag = 1;
                                MessageBox.Show("Mã số tuyến " + _with3.cbo_IdTuyen.Text + " đã tồn tại, vui lòng kiểm tra lại ma so tuyen bạn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        Ket_noi.connect.Close();
                        if (flag == 0)
                        {
                            lenh = "Insert into TuyenXe";
                            lenh += " Values ('" + _with3.cbo_IdTuyen.Text + "', '" + _with3.cbo_TenTuyen.Text + "', N'" + _with3.cbo_DiaDiemDi.Text + "', N'" + _with3.cbo_DiaDiemDen.Text + "')";
                            SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                            Ket_noi.connect.Open();
                            try
                            {
                                bo_lenh.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                UpdateTuyenXe();
                                Lock_Control_tuyen_xe(false);
                                LockButton_tuyen_xe(false);
                                luoi_Tuyen_xe.Enabled = true;
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
                        Huy_thao_tac_tuyen_xe();
                    }
                }
            }
            else
            {
                //Sua thong tin nguoi dung
                if (TestInfo_tuyen_xe())
                {
                    DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin tuyến xe này." + Constants.vbNewLine + "Click OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        lenh = "Update TuyenXe Set TenTuyen = '" + _with3.cbo_TenTuyen.Text + "', DiaDiemDi = N'" + _with3.cbo_DiaDiemDi.Text + "', DiaDiemDen = N'" + _with3.cbo_DiaDiemDen.Text + "' where IdTuyen = '" + _with3.cbo_IdTuyen.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            UpdateTuyenXe();
                            Lock_Control_tuyen_xe(false);
                            LockButton_tuyen_xe(false);
                            luoi_Tuyen_xe.Enabled = true;
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
                        Huy_thao_tac_tuyen_xe();
                    }
                }
            }
        }
        #endregion

        #region "Xu ly huy thao tac cap nhat da hoan tat"
        public void Huy_thao_tac_tuyen_xe()
        {
            //fm = new Form_Main();
            luoi_Tuyen_xe.Enabled = true;
            Xoa_lien_ket_tuyen_xe();
            Lock_Control_tuyen_xe(false);
            LockButton_tuyen_xe(false);
            UpdateTuyenXe();
        }
        #endregion

        #region "Xoa tuyen hoan tat"
        public void Xoa_tuyen_xe()
        {
            //fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma so tuyen: " + cbo_IdTuyen.Text + Constants.vbNewLine + " - Ten tuyen: " + cbo_TenTuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from TuyenXe where IdTuyen = '" + cbo_IdTuyen.SelectedValue.ToString() + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    UpdateTuyenXe();
                    Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Không cập nhật được dữ liệu, xóa tuyến không thành công", Constants.vbExclamation, "Thông báo");
                }
            }
            else
            {
                Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
            }
        }
        #endregion

        #region "Cac xu ly phu voi cac dieu khien da hoan tat"
        private void Lock_Control_tuyen_xe(bool f)
        {
            //fm = new Form_Main();
            var _with4 = this;
            _with4.cbo_IdTuyen.Enabled = true;
            _with4.cbo_TenTuyen.Enabled = f;
            _with4.cbo_DiaDiemDi.Enabled = f;
            _with4.cbo_DiaDiemDen.Enabled = f;
        }

        private void Clear_Control_tuyen_xe()
        {
            //fm = new Form_Main();
            var _with5 = this;
            _with5.cbo_IdTuyen.Text = "";
            _with5.cbo_TenTuyen.Text = "";
            _with5.cbo_DiaDiemDi.Text = "";
            _with5.cbo_DiaDiemDen.Text = "";
            _with5.cbo_IdTuyen.Focus();
        }

        private void LockButton_tuyen_xe(bool dt)
        {
            //fm = new Form_Main();
            var _with6 = this;
            _with6.btn_ThemTuyen.Enabled = !dt;
            _with6.btn_SuaTuyen.Enabled = !dt;
            _with6.btn_XoaTuyen.Enabled = !dt;
            _with6.btn_LuuTuyen.Enabled = dt;
            _with6.btn_HuyTuyen.Enabled = dt;
        }

        private bool TestInfo_tuyen_xe()
        {
            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with7 = this;
            if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_IdTuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cbo_TenTuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cbo_DiaDiemDi.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cbo_DiaDiemDen.Text)))
            {
                functionReturnValue = false;
                Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
            }

            if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_IdTuyen.Text)))
            {
                _with7.cbo_IdTuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_TenTuyen.Text)))
            {
                _with7.cbo_TenTuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_DiaDiemDi.Text)))
            {
                _with7.cbo_DiaDiemDi.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cbo_DiaDiemDen.Text)))
            {
                _with7.cbo_DiaDiemDen.Focus();
                return functionReturnValue;
            }
            return functionReturnValue;
        }
        #endregion
        #endregion

        //Xu ly Thoi_Diem
        #region "Xử lý class Thoi_Diem hoàn tất"
        private DataTable bang_thoi_diem;
        private DataTable bang_tuyen_xe_thoi_diem;
        private string lenh_thoi_diem;
        public void Update_thoi_diem()
        {
            Doc_thoi_diem();
            Tao_lien_ket_thoi_diem();
            Doc_tuyen_thoi_diem();
            Hide_thoi_diem();
        }

        private void Hide_thoi_diem()
        {
            //fm = new Form_Main();
            rad_KhongLap.Hide();
            rad_LapTuan.Hide();
            lbl_Lap.Hide();
            date_NgayKetThuc.Hide();
        }

        private void Show_thoi_diem()
        {
            //fm = new Form_Main();
            rad_KhongLap.Show();
            rad_LapTuan.Show();
        }

        private void Doc_tuyen_thoi_diem()
        {
            //fm = new Form_Main();
            lenh = "Select IdTuyen, TenTuyen from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            var _with1 = cbo_GanTuyen;
            _with1.DataSource = bang_tuyen_xe;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            var _with2 = this;
            _with2.txt_TenTuyen.DataBindings.Clear();
            _with2.txt_TenTuyen.DataBindings.Add("Text", bang_tuyen_xe, "TenTuyen");
        }

        #region "Doc thoi diem voi tao lien ket da xong"
        private void Doc_thoi_diem()
        {
            //fm = new Form_Main();
            lenh = "Select * from ThoiDiem";
            bang_thoi_diem = Ket_noi.Doc_bang(lenh);
            luoi_ThoiDiem.DataSource = bang_thoi_diem;
        }

        private void Tao_lien_ket_thoi_diem()
        {
            //fm = new Form_Main();
            var _with3 = cbo_MaThoiDiem;
            _with3.DataSource = bang_thoi_diem;
            _with3.DisplayMember = "IdThoiDiem";
            _with3.ValueMember = "IdThoiDiem";
            var _with4 = this;
            _with4.date_Chay.DataBindings.Clear();
            _with4.txt_GioChay.DataBindings.Clear();

            _with4.date_Chay.DataBindings.Add("Text", bang_thoi_diem, "Ngay");
            _with4.txt_GioChay.DataBindings.Add("Text", bang_thoi_diem, "Gio");
        }
        #endregion

        #region "Xu ly ho tro button da xong"
        private void Clear_Control_thoi_diem()
        {
            //fm = new Form_Main();
            var _with5 = this;
            _with5.date_Chay.Text = "";
            _with5.date_NgayKetThuc.Text = "";
            _with5.txt_GioChay.Text = "";
            _with5.rad_KhongLap.Checked = true;
            _with5.date_Chay.Focus();
        }

        private void LockButton_thoi_diem(bool dt)
        {
            //fm = new Form_Main();
            var _with6 = this;
            _with6.btn_ThemThoiDiem.Enabled = !dt;
            _with6.btn_SuaThoiDiem.Enabled = !dt;
            _with6.btn_XoaThoiDiem.Enabled = !dt;
            _with6.btn_LuuThoiDiem.Enabled = dt;
            _with6.btn_HuyThoiDiem.Enabled = dt;
        }
        #endregion

        public void them_thoi_diem()
        {
            //fm = new Form_Main();
            flag = true;
            LockButton_thoi_diem(true);
            lbl_Lap.Hide();
            date_NgayKetThuc.Hide();
            Show_thoi_diem();
            Clear_Control_thoi_diem();
            cbo_MaThoiDiem.Enabled = false;
        }

        public void Sua_thoi_diem()
        {
            //fm = new Form_Main();
            rad_LapTuan.Checked = false;
            rad_KhongLap.Checked = true;
            Show_thoi_diem();
            flag = false;
            LockButton_thoi_diem(true);
            cbo_MaThoiDiem.Enabled = false;
        }

        public void Luu_thoi_diem()
        {
            //fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with7 = this;
            //Neu nhu trang thai dang la them
            if (flag)
            {

                if (TestInfo_thoi_diem())
                {
                    //Kiem tra ngay gio them vao phai la chua co trong CSDL
                    SqlCommand sqlCM = new SqlCommand("select Ngay, Gio from ThoiDiem", Ket_noi.connect);
                    SqlDataReader sqlDR = null;
                    Ket_noi.connect.Open();
                    sqlDR = sqlCM.ExecuteReader();
                    while (sqlDR.Read() == true)
                    {
                        if (Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(0)), DateFormat.ShortDate) == _with7.date_Chay.Text && sqlDR.GetValue(1).ToString() == _with7.txt_GioChay.Text)
                        {
                            MessageBox.Show("Ngày giờ này đã tồn tại, vui lòng kiểm tra lại thông tin nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Ket_noi.connect.Close();

                    if (_with7.rad_KhongLap.Checked == true)
                    {
                        lenh = "Insert into ThoiDiem(Ngay, Gio) ";
                        lenh += "Values ('" + Strings.FormatDateTime(Convert.ToDateTime(_with7.date_Chay.Text), DateFormat.ShortDate) + "', '" + _with7.txt_GioChay.Text + "')";
                        SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            bo_lenh.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Update_thoi_diem();
                            LockButton_thoi_diem(false);
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else if (_with7.rad_LapTuan.Checked == true)
                    {
                        long i = layKhoangCach();
                        if (i < 0)
                        {
                            MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Ngay them trung ngay hien tai
                        if (i == 0)
                        {
                            lenh = "Insert into ThoiDiem(Ngay, Gio) ";
                            lenh += "Values ('" + Strings.FormatDateTime(Convert.ToDateTime(_with7.date_Chay.Text), DateFormat.ShortDate) + "', '" + _with7.txt_GioChay.Text + "')";
                            SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                            try
                            {
                                Ket_noi.connect.Open();
                                bo_lenh.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                Update_thoi_diem();
                                LockButton_thoi_diem(false);
                                Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không cập nhật được dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                            }
                        }
                        else if (i > 0)
                        {
                            var ngay = 0;
                            var d = Convert.ToDateTime(_with7.date_Chay.Text);
                            while ((ngay <= i))
                            {
                                lenh = "Insert into ThoiDiem(Ngay, Gio) ";
                                lenh += "Values ('" + Strings.FormatDateTime(d, DateFormat.ShortDate) + "', '" + _with7.txt_GioChay.Text + "')";
                                SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                                try
                                {
                                    Ket_noi.connect.Open();
                                    bo_lenh.ExecuteNonQuery();
                                    Ket_noi.connect.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Không cập nhật được dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Ket_noi.connect.Close();
                                    return;
                                }
                                ngay += 7;
                                d = DateAndTime.DateAdd(DateInterval.Day, ngay, d);
                            }
                            Update_thoi_diem();
                            LockButton_thoi_diem(false);
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                    }
                }
                //trang thai sua
            }
            else
            {
                if (TestInfo_thoi_diem())
                {
                    lenh = "Update ThoiDiem set Ngay = '" + Strings.FormatDateTime(Convert.ToDateTime(_with7.date_Chay.Text), DateFormat.ShortDate) + "', Gio = '" + _with7.txt_GioChay.Text + "' where IdThoiDiem = '" + _with7.cbo_MaThoiDiem.Text + "'";
                    SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                    try
                    {
                        Ket_noi.connect.Open();
                        sqlqr.ExecuteNonQuery();
                        Ket_noi.connect.Close();
                        Update_thoi_diem();
                        LockButton_thoi_diem(false);
                        Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không cập nhật được dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Ket_noi.connect.Close();
                    }
                }
            }
        }

        public void Huy_thoi_diem()
        {
            //fm = new Form_Main();
            LockButton(false);
            Update_thoi_diem();
            cbo_MaThoiDiem.Enabled = true;
            Hide_thoi_diem();
        }

        public void Xoa_thoi_diem()
        {
            //fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma thoi diem: " + cbo_MaThoiDiem.Text + Constants.vbNewLine + " - Ngay: " + date_Chay.Text + Constants.vbNewLine + " - Giờ: " + txt_GioChay.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from ThoiDiem where IdThoiDiem = '" + cbo_MaThoiDiem.Text + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    Update_thoi_diem();
                    Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Thời điểm này đã được gán cho tuyến xe, bạn phải xóa thông tin tuyến xe đó trước!", Constants.vbExclamation, "Thông báo");
                }
            }
            else
            {
                Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
            }
        }

        private long layKhoangCach()
        {
            //fm = new Form_Main();
            long i = 0;
            i = DateAndTime.DateDiff(DateInterval.Day, Convert.ToDateTime(date_Chay.Text), Convert.ToDateTime(date_NgayKetThuc.Text), FirstDayOfWeek.System, FirstWeekOfYear.System);
            return i;
        }

        private bool TestInfo_thoi_diem()
        {
            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with8 = this;
            if (_with8.rad_KhongLap.Checked == true)
            {
                if (string.IsNullOrEmpty(Strings.Trim(_with8.date_Chay.Text)) || string.IsNullOrEmpty(Strings.Trim(_with8.txt_GioChay.Text)))
                {
                    functionReturnValue = false;
                    Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                    return functionReturnValue;
                }
            }
            else if (_with8.rad_LapTuan.Checked == true)
            {
                if (string.IsNullOrEmpty(Strings.Trim(_with8.date_Chay.Text)) || string.IsNullOrEmpty(Strings.Trim(_with8.txt_GioChay.Text)) || string.IsNullOrEmpty(_with8.date_NgayKetThuc.Text))
                {
                    functionReturnValue = false;
                    Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                    return functionReturnValue;
                }

                if (!string.IsNullOrEmpty(_with8.date_Chay.Text) && !string.IsNullOrEmpty(_with8.date_NgayKetThuc.Text))
                {
                    if (layKhoangCach() > 365)
                    {
                        functionReturnValue = false;
                        MessageBox.Show("Bạn chỉ được lặp tuần trong phạm vi là 1 năm hay 48 tuần", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return functionReturnValue;
                    }
                }
            }

            if (Convert.ToDateTime(date_Chay.Text) < DateAndTime.Today.Date)
            {
                functionReturnValue = false;
                MessageBox.Show("Ngay ban them khong duoc nho hon ngay hien tai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return functionReturnValue;
            }
            return functionReturnValue;
        }

        public void Gan_tuyen()
        {
            //fm = new Form_Main();
            var _with9 = this;
            if (string.IsNullOrEmpty(_with9.cbo_MaThoiDiem.Text))
            {
                MessageBox.Show("Bạn chưa chọn thời điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Neu ngay gio do da gan cho tuyen do roi thi thong bao loi
            SqlCommand sqlCM = new SqlCommand("select * from ChiTietTuyen", Ket_noi.connect);
            SqlDataReader sqlDR = null;
            Ket_noi.connect.Open();
            sqlDR = sqlCM.ExecuteReader();
            while (sqlDR.Read() == true)
            {
                if (sqlDR.GetValue(0).ToString() == _with9.cbo_GanTuyen.Text && sqlDR.GetValue(1).ToString() == _with9.cbo_MaThoiDiem.Text)
                {
                    MessageBox.Show("Thời điểm này đã được gán cho tuyến " + _with9.cbo_GanTuyen.Text + " rồi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Ket_noi.connect.Close();
                    return;
                }
            }
            Ket_noi.connect.Close();

            //Sau khi kiem tra logic thi bat dau gan tuyen
            lenh = "Insert into ChiTietTuyen values('" + _with9.cbo_GanTuyen.Text + "', '" + _with9.cbo_MaThoiDiem.Text + "')";
            SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                bo_lenh.ExecuteNonQuery();
                Ket_noi.connect.Close();
                Interaction.MsgBox("Ngày " + _with9.date_Chay.Text + " Giờ: " + _with9.txt_GioChay.Text + " đã được gán cho tuyến " + _with9.cbo_GanTuyen.Text, Constants.vbInformation, "Thông báo");
                Update_thoi_diem();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Gán tuyến không thành công", Constants.vbExclamation, "Thông báo");
            }
        }
        #endregion


        //Xu ly Chuyen_Xe
        #region "Xử lý class  Chuyen_Xe hoàn tất"
        private DataTable bang_chuyen_xe;
        private DataTable bang_tuyen_xe_chuyen_xe;
        private DataTable bang_Chi_tiet_tuyen_chuyen_xe;
        private DataTable bang_Thoi_diem_chuyen_xe;

        private DataTable bang_xe_chuyen_xe;
        private string lenh_chuyen_xe;

        public void Update_Chuyen_xe()
        {
            Doc_chuyen_xe();
            Tao_lien_ket_chuyen_xe();
            Lock_Control_chuyen_xe(false);
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        private void Doc_chuyen_xe()
        {
            //Form_Main fm = new Form_Main();
            Luoi_Chuyen_xe.ClearSelection();
            lenh = "Select * from ChuyenXe";
            bang_chuyen_xe = Ket_noi.Doc_bang(lenh);
            Luoi_Chuyen_xe.DataSource = bang_chuyen_xe;
        }

        private void Tao_lien_ket_chuyen_xe()
        {
            //fm = new Form_Main();
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                var _with1 = cbo_IdChuyen;
                _with1.DataSource = bang_chuyen_xe;
                _with1.DisplayMember = "IdChuyen";
                _with1.ValueMember = "IdChuyen";
                Xoa_lien_ket_chuyen_xe();
                cbo_IdChuyen.Text = (String)Luoi_Chuyen_xe.Rows[0].Cells[0].Value;

                var _with2 = this;
                _with2.cbo_IdTuyenChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "IdTuyen");
                _with2.cbo_SoXeChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "So_Xe");
                _with2.cbo_NgayDiChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "NgayDi");
                _with2.cbo_GioDiChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "Gio");
            }
        }

        private void Xoa_lien_ket_chuyen_xe()
        {
            //fm = new Form_Main();
            var _with3 = this;
            _with3.cbo_IdTuyenChuyen.DataBindings.Clear();
            _with3.cbo_SoXeChuyen.DataBindings.Clear();
            _with3.cbo_NgayDiChuyen.DataBindings.Clear();
            _with3.cbo_GioDiChuyen.DataBindings.Clear();
        }

        private void Lock_Control_chuyen_xe(bool f)
        {
            //fm = new Form_Main();
            var _with4 = this;
            _with4.cbo_IdChuyen.Enabled = !f;
            _with4.cbo_IdTuyenChuyen.Enabled = f;
            _with4.cbo_SoXeChuyen.Enabled = f;
            _with4.cbo_NgayDiChuyen.Enabled = f;
            _with4.cbo_GioDiChuyen.Enabled = f;
            _with4.Luoi_Chuyen_xe.Enabled = !f;
        }

        private void Clear_Control_chuyen_xe()
        {
            //fm = new Form_Main();
            var _with5 = this;
            _with5.cbo_IdChuyen.Text = "";
            _with5.cbo_IdTuyenChuyen.Text = "";
            _with5.cbo_NgayDiChuyen.Text = "";
            _with5.cbo_GioDiChuyen.Text = "";
            _with5.txt_SoDienThoai.Text = "";
            _with5.cbo_SoXeChuyen.Text = "";
            _with5.cbo_IdTuyenChuyen.Focus();
        }

        private void LockButton_chuyen_xe(bool dt)
        {
            //fm = new Form_Main();
            var _with6 = this;
            _with6.btn_ThemChuyen.Enabled = !dt;
            _with6.btn_SuaChuyen.Enabled = !dt;
            _with6.btn_XoaChuyen.Enabled = !dt;
            _with6.btn_LuuChuyen.Enabled = dt;
            _with6.btn_HuyChuyen.Enabled = dt;
        }

        public void Them_chuyen_xe()
        {
            Xoa_lien_ket_chuyen_xe();
            flag = true;
            Lock_Control_chuyen_xe(true);
            LockButton_chuyen_xe(true);
            Doc_tuyen_xe_chuyen_xe();
            Doc_xe_chuyen_xe();
            Clear_Control_chuyen_xe();
        }

        public void Sua_chuyen_xe()
        {
            flag = false;
            Lock_Control_chuyen_xe(true);
            LockButton_chuyen_xe(true);
            Doc_tuyen_xe_chuyen_xe();
            Doc_xe_chuyen_xe();
        }

        public void Huy_chuyen_xe()
        {
            Xoa_lien_ket_chuyen_xe();
            Lock_Control_chuyen_xe(false);
            LockButton_chuyen_xe(false);
            Update_Chuyen_xe();

        }

        private void Doc_tuyen_xe_chuyen_xe()
        {
            //fm = new Form_Main();
            lenh_chuyen_xe = "Select IdTuyen from TuyenXe";
            bang_tuyen_xe_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            var _with7 = cbo_IdTuyenChuyen;
            _with7.DataSource = bang_tuyen_xe_chuyen_xe;
            _with7.DisplayMember = "IdTuyen";
            _with7.ValueMember = "IdTuyen";
        }

        public void Chon_tuyen_chuyen_xe()
        {
            //fm = new Form_Main();
            if (cbo_IdTuyenChuyen.SelectedIndex < 0)
                return;
            //Nghia la ko chọn mục nào
            Loc_Thoi_diem_theo_Tuyen(cbo_IdTuyenChuyen.SelectedValue.ToString());
        }       

        private void Loc_Thoi_diem_theo_Tuyen(string IdTuyen)
        {
            //fm = new Form_Main();
            lenh_chuyen_xe = "Select Distinct Ngay from ThoiDiem, ChiTietTuyen where IdTuyen = '" + IdTuyen + "' and ThoiDiem.IdThoiDiem = ChiTietTuyen.IdThoiDiem";
            bang_Chi_tiet_tuyen_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            var _with8 = cbo_NgayDiChuyen;
            _with8.DataSource = bang_Chi_tiet_tuyen_chuyen_xe;
            _with8.ValueMember = "Ngay";
            _with8.DisplayMember = "Ngay";
        }

        public void Chon_ngay()
        {
            //fm = new Form_Main();
            if (cbo_NgayDiChuyen.SelectedIndex < 0)
                return;
            //Nghia la ko chọn mục nào
            Loc_gio_theo_ngay(cbo_NgayDiChuyen.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay(string ngay)
        {
            //fm = new Form_Main();
            lenh_chuyen_xe = "Select Gio from ThoiDiem where Ngay = '" + ngay + "'";
            bang_Thoi_diem_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            var _with9 = cbo_GioDiChuyen;
            _with9.DataSource = bang_Thoi_diem_chuyen_xe;
            _with9.ValueMember = "Gio";
            _with9.DisplayMember = "Gio";
        }

        private void Doc_xe_chuyen_xe()
        {
            //fm = new Form_Main();
            lenh_chuyen_xe = "Select So_Xe from Xe";
            bang_xe_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            var _with10 = cbo_SoXeChuyen;
            _with10.DataSource = bang_xe_chuyen_xe;
            _with10.ValueMember = "So_Xe";
            _with10.DisplayMember = "So_Xe";
        }

        public void Luu_chuyen_xe()
        {
            //fm = new Form_Main();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with11 = this;
            //Truong hop them chuyen moi
            if (flag == true)
            {
                if (TestInfo_chuyen_xe())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm chuyến xe này không, " + Constants.vbNewLine, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //Neu nhan vien dong y
                    if (dg == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Kiem tra xem chuyen xe do co bi trung khong
                        SqlCommand sqlCM = new SqlCommand("select IdTuyen, NgayDi, Gio, So_Xe from ChuyenXe", Ket_noi.connect);
                        SqlDataReader sqlDR = null;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == _with11.cbo_IdTuyenChuyen.Text && Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(1)), DateFormat.ShortDate) == _with11.cbo_NgayDiChuyen.Text && sqlDR.GetValue(2).ToString() == _with11.cbo_GioDiChuyen.Text && sqlDR.GetValue(3).ToString() == _with11.cbo_SoXeChuyen.Text)
                            {
                                MessageBox.Show("Xe " + _with11.cbo_SoXeChuyen.Text + " đã được gán cho tuyến " + _with11.cbo_IdTuyenChuyen.Text + " vào thời điểm này rồi, vui lòng chọn xe khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ket_noi.connect.Close();
                                return;
                            }
                            //Kiem tra hai gio chay cua xe do trong ngay khong duoc chenh lech it nhat la quá 3 tiếng
                            if (Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(1)), DateFormat.ShortDate) == _with11.cbo_NgayDiChuyen.Text && sqlDR.GetValue(3).ToString() == _with11.cbo_SoXeChuyen.Text)
                            {
                                //Cat chuoi gio cua chuyen da co va gio cua chuyen muon them moi
                                string gioDaCo = sqlDR.GetValue(2).ToString();
                                string gioMuonThem = _with11.cbo_GioDiChuyen.Text;
                                int i = 0;
                                int j = 0;
                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 4 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 3 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 3 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 4 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }
                            }
                            //Neu muon kiem tra gi nua thi them o day
                        }
                        Ket_noi.connect.Close();

                        //Bat dau insert dulieu
                        lenh_chuyen_xe = "Insert into ChuyenXe(IdTuyen, NgayDi, Gio, So_Xe)";
                        lenh_chuyen_xe += " Values ('" + _with11.cbo_IdTuyenChuyen.Text + "', '" + _with11.cbo_NgayDiChuyen.Text + "', '" + _with11.cbo_GioDiChuyen.Text + "', '" + _with11.cbo_SoXeChuyen.Text + "')";
                        SqlCommand bo_lenh = new SqlCommand(lenh_chuyen_xe, Ket_noi.connect);
                        Ket_noi.connect.Open();
                        try
                        {
                            bo_lenh.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Update_Chuyen_xe();
                            Lock_Control_chuyen_xe(false);
                            LockButton_chuyen_xe(false);
                            Luoi_Chuyen_xe.Enabled = true;
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, thêm chuyen thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                    {
                        Huy_chuyen_xe();
                    }
                }
                //Truong hop sua thong tin chuyen
            }
            else
            {
                if (TestInfo_chuyen_xe())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin chuyến xe này không, " + Constants.vbNewLine, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //Neu nhan vien dong y
                    if (dg == System.Windows.Forms.DialogResult.Yes)
                    {
                        SqlCommand sqlCM = new SqlCommand("select IdTuyen, NgayDi, Gio, So_Xe from ChuyenXe", Ket_noi.connect);
                        SqlDataReader sqlDR = null;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == _with11.cbo_IdTuyenChuyen.Text && Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(1)), DateFormat.ShortDate) == _with11.cbo_NgayDiChuyen.Text && sqlDR.GetValue(2).ToString() == _with11.cbo_GioDiChuyen.Text && sqlDR.GetValue(3).ToString() == _with11.cbo_SoXeChuyen.Text)
                            {
                                MessageBox.Show("Xe " + _with11.cbo_SoXeChuyen.Text + " đã được gán cho tuyến " + _with11.cbo_IdTuyenChuyen.Text + " vào thời điểm này rồi, vui lòng chọn xe khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ket_noi.connect.Close();
                                return;
                            }
                            //Kiem tra hai gio chay cua xe do trong ngay khong duoc chenh lech it nhat la quá 3 tiếng
                            if (Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(1)), DateFormat.ShortDate) == _with11.cbo_NgayDiChuyen.Text && sqlDR.GetValue(3).ToString() == _with11.cbo_SoXeChuyen.Text)
                            {
                                //Cat chuoi gio cua chuyen da co va gio cua chuyen muon them moi
                                string gioDaCo = sqlDR.GetValue(2).ToString();
                                string gioMuonThem = _with11.cbo_GioDiChuyen.Text;
                                int i = 0;
                                int j = 0;
                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 4 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 3 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 2, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 1));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 2 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 2, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 3 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 4 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 5 && Strings.Mid(gioDaCo, 3, 1) == "h" && Strings.Mid(gioMuonThem, 3, 1) == "h")
                                {
                                    i = Convert.ToInt32(Strings.Left(gioDaCo, 2));
                                    j = Convert.ToInt32(Strings.Left(gioMuonThem, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }
                            }
                            //Neu muon kiem tra gi nua thi them o day
                        }
                        Ket_noi.connect.Close();
                        SqlDataReader dr = null;
                        string lenh3 = "Select IdChuyen from ChoNgoi";
                        SqlCommand bo_lenh = new SqlCommand(lenh3, Ket_noi.connect);
                        Ket_noi.connect.Open();
                        dr = bo_lenh.ExecuteReader();
                        while (dr.Read() == true)
                        {
                            if (dr.GetValue(0).ToString() == _with11.cbo_IdChuyen.Text)
                            {
                                MessageBox.Show("Chuyến xe đã có nguoi đặt chỗ rồi, bạn không được sưa vì sẽ làm mất uy tính khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                                return;
                            }
                        }
                        Ket_noi.connect.Close();
                        lenh_chuyen_xe = "Update ChuyenXe Set IdTuyen = '" + _with11.cbo_IdTuyenChuyen.Text + "', NgayDi = '" + _with11.cbo_NgayDiChuyen.Text + "', Gio = '" + _with11.cbo_GioDiChuyen.Text + "', So_Xe = '" + _with11.cbo_SoXeChuyen.Text + "' where IdChuyen = '" + _with11.cbo_IdTuyenChuyen.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh_chuyen_xe, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Update_Chuyen_xe();
                            Lock_Control_chuyen_xe(false);
                            LockButton_chuyen_xe(false);
                            Luoi_Chuyen_xe.Enabled = true;
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, sửa thông tin chuyen xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                    {
                        Huy_chuyen_xe();
                    }
                }
            }
            Update_Ve_xe_ban_ve();
        }

        private bool TestInfo_chuyen_xe()
        {
            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with12 = this;
            if (string.IsNullOrEmpty(Strings.Trim(_with12.cbo_IdTuyenChuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with12.cbo_NgayDiChuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with12.cbo_GioDiChuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with12.cbo_SoXeChuyen.Text)))
            {
                functionReturnValue = false;
                Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
            }

            if (string.IsNullOrEmpty(Strings.Trim(_with12.cbo_IdTuyenChuyen.Text)))
            {
                _with12.cbo_IdTuyenChuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with12.cbo_NgayDiChuyen.Text)))
            {
                _with12.cbo_NgayDiChuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with12.cbo_GioDiChuyen.Text)))
            {
                _with12.cbo_GioDiChuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with12.cbo_SoXeChuyen.Text)))
            {
                _with12.cbo_SoXeChuyen.Focus();
                return functionReturnValue;
            }
            return functionReturnValue;
        }

        public void Xoa_chuyen_xe()
        {
            //fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Chuyến xe: " + cbo_IdChuyen.Text + Constants.vbNewLine + " - Tuyến xe: " + cbo_IdTuyenChuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh_chuyen_xe = "Delete from ChuyenXe where IdChuyen = '" + cbo_IdChuyen.SelectedValue.ToString() + "'";
                SqlCommand query1 = new SqlCommand(lenh_chuyen_xe, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    Update_Chuyen_xe();
                    Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Không cập nhật được dữ liệu, xóa chuyến không thành công", Constants.vbExclamation, "Thông báo");
                }
            }
            else
            {
                Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
            }
        }

        #region "Xu ly cac nut di chuyen va xuat thong tin xe da hoan tat"
        private void Xuat_thong_tin_Chuyen_xe()
        {
            //fm = new Form_Main();
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                DataRow dong = bang_chuyen_xe.Rows[vi_tri_hien_hanh];
                var _with13 = this;
                _with13.cbo_IdChuyen.Text = dong["IdChuyen"].ToString();
                _with13.cbo_IdTuyenChuyen.Text = dong["IdTuyen"].ToString();
                _with13.cbo_NgayDiChuyen.Text = Convert.ToString(dong["NgayDi"]);
                _with13.cbo_GioDiChuyen.Text = dong["Gio"].ToString();
            }

        }

        public void Di_chuyen_ve_sau_chuyen_xe()
        {
            if (vi_tri_hien_hanh < bang_chuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc_chuyen_xe()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau_chuyen_xe()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        public void Di_chuyen_ve_cuoi_chuyen_xe()
        {
            vi_tri_hien_hanh = bang_chuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Chuyen_xe();
        }
        #endregion
        #endregion

        //Xu ly Ban_Ve
        #region "Xử lý class  Chuyen_Xe hoàn tất"
        private DataTable bang_tuyen_xe_ban_ve;
        private DataTable bang_Thoi_diem_ngay;
        private DataTable bang_Thoi_diem_gio;
        private DataTable bang_Xe_ban_ve;

        private DataTable bang_Thong_tin_xe;
        private DataTable bang_dat_ve;
        public string IdChuyen;

        public string So_cho_ngoi;

        public void Update_Ve_xe_ban_ve()
        {
            Doc_tuyen_xe_ban_ve();
            doc_bang_ve_ban_ve();
            Clear_Controls_ban_ve();
        }

        private void doc_bang_ve_ban_ve()
        {
            //fm = new Form_Main();
            lenh = "Select IdVe, TenHanhKhach, SDTHanhKhach, TenTuyen, NgayDi, Gio, So_Xe from BanVe, ChuyenXe, TuyenXe ";
            lenh += " where BanVe.IdChuyen = ChuyenXe.IdChuyen and ChuyenXe.IdTuyen = TuyenXe.IdTuyen";
            bang_dat_ve = Ket_noi.Doc_bang(lenh);
            var _with1 = cbo_MaSoVe;
            _with1.DataSource = bang_dat_ve;
            _with1.ValueMember = "IdVe";
            _with1.DisplayMember = "IdVe";

            //Tao lien ket
            luoi_ThongTinDatVe.DataSource = bang_dat_ve;

        }




        private void Clear_Controls_ban_ve()
        {
            //fm = new Form_Main();
            var _with2 = this;
            _with2.cbo_TenTuyenVe.Text = "";
            _with2.cbo_NgayVe.Text = "";
            _with2.cbo_GioVe.Text = "";
            _with2.cbo_XeVe.Text = "";
        }

        private void Doc_tuyen_xe_ban_ve()
        {
            //fm = new Form_Main();
            lenh = "Select Distinct ChuyenXe.IdTuyen, TenTuyen from ChuyenXe, TuyenXe where TuyenXe.IdTuyen = ChuyenXe.IdTuyen";
            bang_tuyen_xe_ban_ve = Ket_noi.Doc_bang(lenh);
            var _with3 = cbo_TenTuyenVe;
            _with3.DataSource = bang_tuyen_xe_ban_ve;
            _with3.DisplayMember = "TenTuyen";
            _with3.ValueMember = "IdTuyen";

            //Tao lien ket
            //luoi_XeVe.DataSource = bang_tuyen_xe_ban_ve;
        }

        public void Chon_tuyen_ban_ve()
        {
            //fm = new Form_Main();
            if (cbo_TenTuyenVe.SelectedIndex < 0)
                return;
            Loc_ngay_theo_tuyen(cbo_TenTuyenVe.SelectedValue.ToString());
        }

        private void Loc_ngay_theo_tuyen(string IdTuyen)
        {
            //fm = new Form_Main();
            lenh = "Select Distinct NgayDi from ChuyenXe where IdTuyen = '" + IdTuyen + "'";
            bang_Thoi_diem_ngay = Ket_noi.Doc_bang(lenh);
            var _with4 = cbo_NgayVe;
            _with4.DataSource = bang_Thoi_diem_ngay;
            _with4.ValueMember = "NgayDi";
            _with4.DisplayMember = "NgayDi";
        }

        public void Chon_ngay_ban_ve()
        {
            //fm = new Form_Main();
            if (string.IsNullOrEmpty(cbo_GioVe.Text) & string.IsNullOrEmpty(cbo_XeVe.Text))
            {
                cbo_NgayVe.Text = "";
            }
            if (cbo_NgayVe.SelectedIndex < 0)
                return;
            Loc_gio_theo_ngay_ban_ve(cbo_NgayVe.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay_ban_ve(string Ngay)
        {
            //fm = new Form_Main();
            lenh = "Select Gio from ChuyenXe where NgayDi = '" + Ngay + "' and IdTuyen = '" + cbo_TenTuyenVe.SelectedValue.ToString() + "'";
            bang_Thoi_diem_gio = Ket_noi.Doc_bang(lenh);
            var _with5 = cbo_GioVe;
            _with5.DataSource = bang_Thoi_diem_gio;
            _with5.ValueMember = "Gio";
            _with5.DisplayMember = "Gio";
        }

        public void Chon_xe_ban_ve()
        {
            //fm = new Form_Main();
            if (string.IsNullOrEmpty(cbo_XeVe.Text))
            {
                cbo_GioVe.Text = "";
            }
            if (cbo_GioVe.SelectedIndex < 0)
                return;
            Loc_xe_theo_gio_ban_ve(cbo_GioVe.SelectedValue.ToString());
        }

        private void Loc_xe_theo_gio_ban_ve(string Gio)
        {
            //fm = new Form_Main();
            lenh = "Select So_Xe from ChuyenXe where Gio = '" + Gio + "' and IdTuyen = '" + cbo_TenTuyenVe.SelectedValue.ToString() + "' and NgayDi = '" + cbo_NgayVe.SelectedValue.ToString() + "'";
            bang_Xe_ban_ve = Ket_noi.Doc_bang(lenh);
            var _with6 = cbo_XeVe;
            _with6.DataSource = bang_Xe_ban_ve;
            _with6.ValueMember = "So_Xe";
            _with6.DisplayMember = "So_Xe";
        }

        public void Chon_thong_tin_xe()
        {
            //fm = new Form_Main();
            if (string.IsNullOrEmpty(cbo_GioVe.Text))
            {
                cbo_XeVe.Text = "";
            }
            if (!string.IsNullOrEmpty(cbo_TenTuyenVe.Text) & !string.IsNullOrEmpty(cbo_XeVe.Text) & !string.IsNullOrEmpty(cbo_GioVe.Text) & !string.IsNullOrEmpty(cbo_NgayVe.Text))
            {
                if (cbo_XeVe.SelectedIndex < 0)
                    return;
                Loc_thong_tin_theo_so_xe(cbo_XeVe.Text);

            }
        }

        private void Loc_thong_tin_theo_so_xe(string So_Xe)
        {
            //fm = new Form_Main();
            lenh = "Select * From Xe where So_Xe = '" + So_Xe + "'";
            bang_Thong_tin_xe = Ket_noi.Doc_bang(lenh);
            luoi_XeVe.DataSource = bang_Thong_tin_xe;
        }
        public Form_Xe_16_Cho frm_xe_16;
        //Xu ly nut chon cho ngoi
        public void Chon_cho_ngoi()
        {

            //fm = new Form_Main();
            var _with7 = this;
            if (Kiem_tra_thong_tin_dat_ve())
            {
                lenh = "Select So_Cho_Ngoi From Xe where So_Xe = '" + _with7.cbo_XeVe.Text + "'";
                bang_Thong_tin_xe = Ket_noi.Doc_bang(lenh);
                So_cho_ngoi = bang_Thong_tin_xe.Rows[0]["So_Cho_Ngoi"].ToString();

                if (Convert.ToInt32(So_cho_ngoi) == 7)
                {
                    Form_Xe_7_Cho frm_xe_7 = new Form_Xe_7_Cho() { fm=this};
                    frm_xe_7.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 16)
                {
                    
                    frm_xe_16 = new Form_Xe_16_Cho() { fm = this };
                    frm_xe_16.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 25)
                {
                    Form_Xe_25_Cho frm_xe_25 = new Form_Xe_25_Cho() { fm = this };
                    frm_xe_25.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 30)
                {
                    Form_Xe_30_Cho frm_xe_30 = new Form_Xe_30_Cho() { fm = this };
                    frm_xe_30.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 45)
                {
                    Form_Xe_45_Cho frm_xe_45 = new Form_Xe_45_Cho() { fm = this };
                    frm_xe_45.Show();
                }

            }

        }

        private bool Kiem_tra_thong_tin_dat_ve()
        {
            //fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with8 = this;
            if (string.IsNullOrEmpty(_with8.cbo_TenTuyenVe.Text) || string.IsNullOrEmpty(_with8.cbo_NgayVe.Text) || string.IsNullOrEmpty(_with8.cbo_GioVe.Text) || string.IsNullOrEmpty(_with8.cbo_XeVe.Text) || string.IsNullOrEmpty(_with8.txt_TenHanhKhach.Text) || string.IsNullOrEmpty(_with8.txt_SoDTHanhKhach.Text))
            {
                functionReturnValue = false;
                MessageBox.Show("Phải nhập đầy đủ thông tin đặt vé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return functionReturnValue;
            }

            if (_with8.txt_SoDTHanhKhach.Text.Length > 12 || _with8.txt_SoDTHanhKhach.Text.Length < 9)
            {
                functionReturnValue = false;
                MessageBox.Show("So điện thoại từ 9 đến 12 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return functionReturnValue;
            }
            return functionReturnValue;
        }
        #endregion




        //tạo các properties 
        public DataGridView luoiTuyenxe
        {
            get { return this.luoi_Tuyen_xe; }           
        }

        public DataGridView Luoixe
        {
            get { return this.Luoi_Xe; }
        }

        public ComboBox cboIdTuyen
        {
            get { return this.cbo_IdTuyen; }
        }

        public ComboBox cboTenTuyen
        {
            get { return this.cbo_TenTuyen; }
        }

        public ComboBox cboDiaDiemDi
        {
            get { return this.cbo_DiaDiemDi; }
        }

        public ComboBox cboDiaDiemDen
        {
            get { return this.cbo_DiaDiemDen; }
        }

        public ComboBox cboSoXe
        {
            get { return this.cbo_SoXe; }
        }

        public ButtonX btnThemTuyen
        {
            get { return this.btn_ThemTuyen; }
        }

        public ButtonX btnSuaTuyen
        {
            get { return this.btn_SuaTuyen; }
        }

        public ButtonX btnXoaTuyen
        {
            get { return this.btn_XoaTuyen; }
        }

        public ButtonX btnLuuTuyen
        {
            get { return this.btn_LuuTuyen; }
        }

        public ButtonX btnHuyTuyen
        {
            get { return this.btn_HuyTuyen; }
        }

        public string Tentuyen
        {
            get { return this.cbo_TenTuyenVe.SelectedValue.ToString(); }
            set { this.cbo_TenTuyenVe.SelectedValue = value; }
        }

        public string Ngay
        {
            get { return this.cbo_NgayVe.Text; }
            set { this.cbo_NgayVe.Text= value; }
        }

        public string Gio
        {
            get { return this.cbo_GioVe.Text; }
            set { this.cbo_GioVe.Text = value; }
        }

        public string Ve
        {
            get { return this.cbo_XeVe.Text; }
            set { this.cbo_XeVe.Text = value; }
        }

        /*public string Ve1
        {
            get { return this.cbo_XeVe.Text; }
            set { this.cbo_XeVe.Text = value; }
        }*/

        public string SDT
        {
            get { return this.txt_SoDTHanhKhach.Text; }
            set { this.txt_SoDTHanhKhach.Text = value; }
        }

        public string TenHK
        {
            get { return this.txt_TenHanhKhach.Text; }
            set { this.txt_TenHanhKhach.Text = value; }
        }
    }
}

        
