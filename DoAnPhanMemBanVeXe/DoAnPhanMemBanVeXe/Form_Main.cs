using Microsoft.VisualBasic;
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

namespace DoAnPhanMemBanVeXe
{
    public partial class Form_Main : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public Form_Main(Form_Login form_login)
        {
            InitializeComponent();
            fl = form_login;
        }

        #region "Khai báo biến của nguoi_dung"
        private bool flag_nguoi_dung = true;
        private int vi_tri_hien_hanh_nguoi_dung;
        DataTable bang_Nguoi_Dung = null;
        #endregion

        #region "Khai báo biến của Xe"
        private bool flag_xe;
        private string lenh_xe;
        private DataTable bang_xe;
        private int vi_tri_hien_hanh_xe;
        #endregion

        #region "Khai báo biến của tuyến xe"
        private DataTable bang_tuyen_xe;
        private int vi_tri_hien_hanh_tuyen_xe;
        private string lenh_tuyen_xe;
        private bool flag_tuyen_xe;
        #endregion

        #region "Khai báo biến của thời điểm"
        private DataTable bang_thoi_diem;
        private DataTable bang_tuyen_xe_thoi_diem;
        private string lenh_thoi_diem;
        private bool flag_thoi_diem;
        #endregion

        #region "Khai báo biến bán vé"
        private DataTable bang_tuyen_xe_ban_ve;
        private DataTable bang_Thoi_diem_ngay_ban_ve;
        private DataTable bang_Thoi_diem_gio_ban_ve;
        private DataTable bang_Xe_ban_ve;
        private DataTable bang_Thong_tin_xe_ban_ve;

        private DataTable bang_dat_ve_ban_ve;
        public string IdChuyen_ban_ve;
        public string So_cho_ngoi_ban_ve;
        private string lenh_ban_ve;
        #endregion

        #region "Khai báo biến chuyến xe"
        //private Ban_ve ban_ve = new Ban_ve();
        private DataTable bang_chuyen_xe;
        private DataTable bang_tuyen_xe_chuyen_xe;
        private DataTable bang_Chi_tiet_tuyen_chuyen_xe;
        private DataTable bang_Thoi_diem_chuyen_xe;
        private DataTable bang_xe_chuyen_xe;

        private string lenh_chuyen_xe;
        private bool flag_chuyen_xe;
        private int vi_tri_hien_hanh_chuyen_xe = 0;
        #endregion

        #region "Khai báo biến của form main"
        Form_Login fl;
        private bool flag = true;
        private Update_he_thong update_he_thong = new Update_he_thong();
        #endregion

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Timer1.Start();
            Timer_ChayChu.Start();
            update_he_thong.update_();

            UpdateNguoiDung(); // xong

            UpdateXe(); // xong
            UpdateTuyenXe(); // xong
            Update_thoi_diem(); // xong
            Update_Chuyen_xe(); // xong
            Update_Ve_xe_ban_ve(); // xong

            Form_Phan_Quyen Quyen = new Form_Phan_Quyen(fl, this);
            Quyen.UpdateQuyen();
            Splitter1.Height = 500;
            Timer2.Start();
        }        

        #region "Xử lý timer đã xong"
        private void RibbonPanel1_Click(object sender, System.EventArgs e)
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

        #region "Các sự kiện Close, Logout, của form main đã xử lý xong"
        private void ButtonX_Logout_Click(object sender, EventArgs e)
        {
            fl.Visible = true;
            fl.Opacity = 100;
            fl.txtPassword.Clear();
            fl.Timer1.Start();
            fl.Timer2.Start();
            Ket_noi.connect.Close();
            this.Close();
        }

        private void ButtonX_Close_Click(object sender, EventArgs e)
        {
            this.WindowState = 0;
            do
            {
                this.Top = this.Top + 10;
                this.Left = this.Left + 10;
                this.Width = this.Width - 30;
                this.Height = this.Height - 30;
            }
            while (this.Top >= this.Height);
            Application.Exit();

            fl.Close();
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            fl.Visible = true;
            fl.Opacity = 100;
            fl.Timer1.Start();
            fl.Timer2.Start();
            fl.txtPassword.Clear();
        }

        #endregion

        #region "Xử lý các sự kiện click các button bên trái để di chuyển các tab điều khiển đã hoàn tất"
        private void btn_QuanLyND_Click(object sender, EventArgs e)
        {
            this.TabControl_Main.SelectedTabIndex = 0;
        }

        private void btn_QuanLyXe_Click(object sender, EventArgs e)
        {
            this.TabControl_Main.SelectedTabIndex = 1;
        }

        private void btn_QuanLyTuyenXe_Click(object sender, EventArgs e)
        {
            TabControl_Main.SelectedTabIndex = 2;
        }

        private void btn_ChuyenXe_Click(object sender, EventArgs e)
        {
            TabControl_Main.SelectedTabIndex = 4;
        }

        private void btn_BanVe_Click(object sender, EventArgs e)
        {
            TabControl_Main.SelectedTabIndex = 5;
        }
        #endregion

        //---------------------------------------------Xử lý người dùng-------------------------------------------------

        #region "Đã xong"

        #region "Xử lý các nút di chuyển và xuất thông tin người dùng đã hoàn tất"
        private void Xuat_thong_tin_Nguoi_Dung()
        {
            DataRow dong = bang_Nguoi_Dung.Rows[vi_tri_hien_hanh_nguoi_dung];
            {
                cbo_Username.Text = dong["IdNguoiDung"].ToString();
                txt_Password.Text = dong["PassND"].ToString();
                txt_HoTen.Text = dong["HoTen"].ToString();
                date_NgaySinh.Text = dong["NgaySinh"].ToString();
                if (dong["GioiTinh"].ToString() == "Nam")
                    radNam.Checked = true;
                else
                    radNu.Checked = true;
                txt_DiaChi.Text = dong["DiaChi"].ToString();
                txt_SoDienThoai.Text = dong["SoDT"].ToString();
                cbo_IdLoaiND.Text = dong["IdLoaiND"].ToString();
            }
        }

        public void Di_chuyen_ve_sau_nguoi_dung()
        {
            if (vi_tri_hien_hanh_nguoi_dung < bang_Nguoi_Dung.Rows.Count - 1)
            {
                vi_tri_hien_hanh_nguoi_dung += 1;
                Xuat_thong_tin_Nguoi_Dung();
            }
        }

        public void Di_chuyen_ve_truoc_nguoi_dung()
        {
            if (vi_tri_hien_hanh_nguoi_dung > 0)
            {
                vi_tri_hien_hanh_nguoi_dung -= 1;
                Xuat_thong_tin_Nguoi_Dung();
            }
        }

        public void Di_chuyen_ve_dau_nguoi_dung()
        {
            vi_tri_hien_hanh_nguoi_dung = 0;
            Xuat_thong_tin_Nguoi_Dung();
        }

        public void Di_chuyen_ve_cuoi_nguoi_dung()
        {
            vi_tri_hien_hanh_nguoi_dung = bang_Nguoi_Dung.Rows.Count - 1;
            Xuat_thong_tin_Nguoi_Dung();
        }
        #endregion

        #region "Tạo liên kết người dùng giữa các điều khiển với datagridview đã hoàn tất"
        private void Tao_lien_ket_nguoi_dung()
        {
            SqlCommand query = new SqlCommand("select IdLoaiND from LoaiNguoiDung", Ket_noi.connect);
            SqlDataReader dr;
            Ket_noi.connect.Open();
            dr = query.ExecuteReader();
            cbo_IdLoaiND.Items.Clear();
            while (dr.Read() == true)
            {
                if (fl.LoginLoaiND == "Admin")
                    cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                else if (fl.LoginLoaiND == "Quan_Ly")
                {
                    if (dr.GetValue(0).ToString() != "Admin")
                        cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                }
                else if (dr.GetValue(0).ToString() != "Admin" && dr.GetValue(0).ToString() != "Quan_Ly")
                    cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
            }
            Ket_noi.connect.Close();
            {
                var withBlock = cbo_Username;
                withBlock.DataSource = luoi_NguoiDung.DataSource;
                withBlock.DisplayMember = "IdNguoiDung";
                withBlock.ValueMember = "IdNguoiDung";
                withBlock.SelectedValue = "IdNguoiDung";
            }
            Xoa_lien_ket_nguoi_dung();

            // Tao gia tri mac dinh la IdNguoiDung dong thu 0 cot 0 luc khoi dong vi IdNguoiDung la member ko lien ket duoc
            cbo_Username.Text = luoi_NguoiDung.Rows[0].Cells[0].Value.ToString();
            txt_Password.DataBindings.Add("text", luoi_NguoiDung.DataSource, "PassND");
            txt_DiaChi.DataBindings.Add("text", luoi_NguoiDung.DataSource, "DiaChi");
            txt_HoTen.DataBindings.Add("text", luoi_NguoiDung.DataSource, "HoTen");
            txt_SoDienThoai.DataBindings.Add("text", luoi_NguoiDung.DataSource, "SoDT");
            date_NgaySinh.DataBindings.Add("text", luoi_NguoiDung.DataSource, "NgaySinh");
            cbo_IdLoaiND.DataBindings.Add("text", luoi_NguoiDung.DataSource, "IdLoaiND");

        }
        #endregion

        #region "Xóa liên kết giữa các điều khiển với datagridview đã hoàn tất"
        private void Xoa_lien_ket_nguoi_dung()
        {
            txt_Password.DataBindings.Clear();
            txt_DiaChi.DataBindings.Clear();
            txt_HoTen.DataBindings.Clear();
            txt_SoDienThoai.DataBindings.Clear();
            cbo_IdLoaiND.DataBindings.Clear();
            date_NgaySinh.DataBindings.Clear();
        }
        #endregion

        #region "Update người dùng hoàn tất"
        public void UpdateNguoiDung()
        {
            if (fl.LoginLoaiND == "Quan_Ly" || fl.LoginLoaiND == "Admin")
            {
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh_nguoi_dung = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket_nguoi_dung();
                luoi_NguoiDung.ReadOnly = true;
                Lock_Control_nguoi_dung(false);
                LockButton_nguoi_dung(false);
            }
            else
            {
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh_nguoi_dung = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket_nguoi_dung();
                luoi_NguoiDung.ReadOnly = true;
                Button_Them.Enabled = false;
                Button_Sua.Enabled = false;
                Button_Xoa.Enabled = false;
                Button_CapPass.Enabled = false;
                Button_PhanQuyen.Text = "Xem Quyền";
            }
        }
        #endregion

        #region "Xử lý đọc bảng người dùng và phân loại người dùng để hiển thị hoàn tất"
        private void Doc_bang_Nguoi_Dung()
        {
            // Lam sach luoi sau moi lan cap nhat
            luoi_NguoiDung.ClearSelection();
            string lenh;
            if (fl.LoginLoaiND == "Quan_Ly")
                lenh = "Select * from NguoiDung where IdLoaiND = 'Nhan_Vien' or IdNguoiDung = '" + fl.LoginTenND + "'";
            else if (fl.LoginLoaiND == "Nhan_Vien")
                lenh = "Select * from NguoiDung where IdNguoiDung = '" + fl.txtUserName.Text + "'";
            else
                lenh = "Select * from NguoiDung";
            bang_Nguoi_Dung = Ket_noi.Doc_bang(lenh);
            luoi_NguoiDung.DataSource = bang_Nguoi_Dung;
        }
        #endregion

        #region "Test Info người dùng hoàn tất"
        private bool TestInfo_nguoi_dung()
        {
            bool check = true;
            if (cbo_Username.Text.Trim() == "" || txt_Password.Text.Trim() == "" || txt_HoTen.Text.Trim() == "" || date_NgaySinh.Text.Trim() == "" || cbo_IdLoaiND.Text.Trim() == "" || txt_SoDienThoai.Text.Trim() == "" || txt_DiaChi.Text.Trim() == "")
            {
                check = false;
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
            }

            if (cbo_Username.Text.Trim() == "")
            {
                cbo_Username.Focus();
            }
            else if (txt_Password.Text.Trim() == "")
            {
                txt_Password.Focus();
            }
            else if (txt_HoTen.Text.Trim() == "")
            {
                txt_HoTen.Focus();
            }
            else if (date_NgaySinh.Text.Trim() == "")
            {
                date_NgaySinh.Focus();
            }
            else if (txt_SoDienThoai.Text.Trim() == "")
            {
                txt_SoDienThoai.Focus();
            }
            else if (txt_DiaChi.Text.Trim() == "")
            {
                txt_DiaChi.Focus();
            }

            if (txt_Password.Text.Trim().Length < 5)
            {
                check = false;
                MessageBox.Show("Password không được ít hơn 5 kí tự!", "Thông báo lỗi");
                txt_Password.Focus();
            }

            if (cbo_IdLoaiND.Text.Trim() != "Quan_Ly" && cbo_IdLoaiND.Text.Trim() != "Nhan_Vien" && cbo_IdLoaiND.Text.Trim() != "Admin")
            {
                check = false;
                MessageBox.Show("Loại người dùng chỉ có thể là QL(Quản Lý) hoặc NV(Nhân Viên)", "Thông báo lỗi");
                cbo_IdLoaiND.Focus();
            }

            if (txt_SoDienThoai.Text.Trim().Length > 11)
            {
                check = false;
                MessageBox.Show("Số điện thoại không được quá 11 số", "Thông báo lỗi");
                txt_SoDienThoai.Focus();
            }

            return check;
        }
        #endregion

        #region "Xử lý hủy thao tác cập nhật hoàn tất"
        public void Huy_thao_tac_nguoi_dung()
        {
            Xoa_lien_ket_nguoi_dung();
            Lock_Control_nguoi_dung(false);
            LockButton_nguoi_dung(false);
            UpdateNguoiDung();
        }
        #endregion

        #region "Các xử lý phụ với các điều khiển đã hoàn tất"
        private void Lock_Control_nguoi_dung(bool f)
        {
                cbo_Username.Enabled = true;
                txt_Password.Enabled = f;
                txt_HoTen.Enabled = f;
                date_NgaySinh.Enabled = f;
                radNam.Enabled = f;
                radNu.Enabled = f;
                cbo_IdLoaiND.Enabled = f;
                txt_SoDienThoai.Enabled = f;
                txt_DiaChi.Enabled = f;
        }

        private void Clear_Control_nguoi_dung()
        {
                txt_Password.Text = "";
                txt_DiaChi.Text = "";
                txt_HoTen.Text = "";
                txt_SoDienThoai.Text = "";
                radNu.Checked = true;
                cbo_Username.Text = "";
                date_NgaySinh.Text = "";
                cbo_IdLoaiND.Text = "Nhan_Vien";
                cbo_Username.Focus();
        }

        private void LockButton_nguoi_dung(bool dt)
        {
                Button_Them.Enabled = !dt;
                Button_Sua.Enabled = !dt;
                Button_Xoa.Enabled = !dt;
                Button_Luu.Enabled = dt;
                Button_Huy.Enabled = dt;
                Button_PhanQuyen.Enabled = !dt;
                Button_CapPass.Enabled = !dt;
        }
        #endregion

        #region "Lưu thay đổi người dùng hoàn tất"
        public void Luu_thay_doi_nguoi_dung()
        {
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
                Ket_noi.connect.Close();
            {
                if (flag_nguoi_dung == true)
                {
                    if (TestInfo_nguoi_dung())
                    {
                        DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm người dùng này không?\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dg == DialogResult.OK)
                        {
                            int flag = 0;
                            SqlCommand sqlCM = new SqlCommand("select IdNguoiDung from NguoiDung ", Ket_noi.connect);
                            SqlDataReader sqlDR;
                            Ket_noi.connect.Open();
                            sqlDR = sqlCM.ExecuteReader();
                            while (sqlDR.Read() == true)
                            {
                                if (sqlDR.GetValue(0).ToString() == cbo_Username.Text)
                                {
                                    flag = 1;
                                    MessageBox.Show("Tài khoản " + cbo_Username.Text + " đã được sử dụng !", "Thông Báo");
                                }
                            }
                            Ket_noi.connect.Close();

                            if (flag == 0)
                            {
                                SqlCommand sqlqr = new SqlCommand();
                                sqlqr.Connection = Ket_noi.connect;
                                if (radNam.Checked == true)
                                    sqlqr.CommandText = "insert into NguoiDung values('" + cbo_Username.Text + "','" + txt_Password.Text + "',N'" + txt_HoTen.Text + "','" + date_NgaySinh.Value + "',N'Nam',N'" + txt_DiaChi.Text + "'," + txt_SoDienThoai.Text + ",'" + cbo_IdLoaiND.Text + "')";
                                else
                                    sqlqr.CommandText = "insert into NguoiDung values('" + cbo_Username.Text + "','" + txt_Password.Text + "',N'" + txt_HoTen.Text + "','" + date_NgaySinh.Value + "',N'Nữ',N'" + txt_DiaChi.Text + "'," + txt_SoDienThoai.Text + ",'" +cbo_IdLoaiND.Text + "')";

                                Ket_noi.connect.Open();
                                try
                                {
                                    sqlqr.ExecuteNonQuery();
                                    Ket_noi.connect.Close();
                                    Doc_bang_Nguoi_Dung();
                                    vi_tri_hien_hanh_nguoi_dung = 0;
                                    Xuat_thong_tin_Nguoi_Dung();
                                    Tao_lien_ket_nguoi_dung();
                                    MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Một số kí tự trong ô 'Họ Tên' và 'Địa Chỉ' không phù hợp\nCác kí có thể nhập là 0 - 9, 26 chữ cái, '_', các dấu trong tiếng việt và một số kí tự khác", "Thông báo lỗi");
                                    Ket_noi.connect.Close();
                                }
                            }
                        }
                        else
                            Huy_thao_tac_nguoi_dung();
                    }
                }
                else if (TestInfo_nguoi_dung())
                {
                    if (cbo_Username.Text != fl.LoginTenND)
                    {
                        DialogResult dg = MessageBox.Show("Bạn chỉ có quyền sưa thông tin cá nhân của mình\nClick OK đê tiếp tục sửa thông tin, Cancel để hủy thao tác", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dg == DialogResult.OK)
                        {
                            Sua_thong_tin_ca_nhan();
                            return;
                        }
                        else
                        {
                            Huy_thao_tac_nguoi_dung();
                            return;
                        }
                    }

                    DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin cá nhân?\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        SqlCommand sqlqr = new SqlCommand();
                        sqlqr.Connection = Ket_noi.connect;
                        if (radNam.Checked)
                            sqlqr.CommandText = "update NguoiDung set PassND='" + txt_Password.Text + "',HoTen=N'" + txt_HoTen.Text + "',NgaySinh='" + date_NgaySinh.Value + "',GioiTinh=N'Nam',DiaChi=N'" + txt_DiaChi.Text + "',SoDT=" + txt_SoDienThoai.Text + ",IdLoaiND='" + cbo_IdLoaiND.Text + "' where IdNguoiDung='" + cbo_Username.Text + "'";
                        else
                            sqlqr.CommandText = "update NguoiDung set PassND='" + txt_Password.Text + "',HoTen=N'" + txt_HoTen.Text + "',NgaySinh='" + date_NgaySinh.Value + "',GioiTinh=N'Nữ',DiaChi=N'" + txt_DiaChi.Text + "',SoDT=" + txt_SoDienThoai.Text + ",IdLoaiND='" + cbo_IdLoaiND.Text + "' where IdNguoiDung='" + cbo_Username.Text + "'";

                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Lock_Control_nguoi_dung(false);
                            LockButton_nguoi_dung(false);
                            luoi_NguoiDung.Enabled = true;
                            UpdateNguoiDung();
                            MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Một số kí tự bạn nhập không phù hợp\nCác kí có thể nhập là 0 - 9, 26 chữ cái, _ @ * % $ & - ~ và một số kí tự khác", "Thông báo lỗi");
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                        Huy_thao_tac_nguoi_dung();
                }
            }
        }
        #endregion

        #region "Xử lý các nút di chuyển về bên phải đã hoàn tất"
        private void btnHead_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_dau_nguoi_dung();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_truoc_nguoi_dung();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_sau_nguoi_dung();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_cuoi_nguoi_dung();
        }

        #endregion

        #region "Thêm và sửa thông tin người dùng đã xử lý xong"
        public void Them_nguoi_dung()
        {
            flag_nguoi_dung = true;
            Lock_Control_nguoi_dung(true);
            LockButton_nguoi_dung(true);
            Clear_Control_nguoi_dung();
            luoi_NguoiDung.ReadOnly = false;
        }

        public void Sua_thong_tin_ca_nhan()
        {
            flag_nguoi_dung = false;
            Lock_Control_nguoi_dung(true);
            LockButton_nguoi_dung(true);
            cbo_Username.Focus();
            cbo_Username.Text = fl.LoginTenND;
            cbo_Username.Enabled = false;
            luoi_NguoiDung.ReadOnly = false;
            cbo_IdLoaiND.Enabled = false;
        }
        #endregion

        #region "Xử lý xóa người dùng hoàn tất"
        public void Xoa_nguoi_dung()
        {
            if (cbo_Username.Text.Trim() == fl.LoginTenND)
                MessageBox.Show("Ban không được quyền xóa thông tin của chính bạn được.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - User name người dùng: " + cbo_Username.Text + "\n" + " - Tên: " + txt_HoTen.Text + "\n - Số điện thoại: " + txt_SoDienThoai.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                        MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa không thành công" + ex.ToString(), "Thông báo");
                    }
                }
                else
                    MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
            }
        }
        #endregion

        #region "Xử lý event thêm, xóa, sửa, người dùng hoàn tất"
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
            Luu_thay_doi_nguoi_dung();
        }

        private void Button_Huy_Click(object sender, EventArgs e)
        {
            Huy_thao_tac_nguoi_dung();
        }
        #endregion

        #region "Xử lý di chuyển radio theo click chuột trên datagridview đã xong"
        private void luoi_NguoiDung_MouseClick(object sender, MouseEventArgs e)
        {
            if (luoi_NguoiDung.CurrentRow.Cells[4].Value.ToString() == "Nam")
                radNam.Checked = true;
            else
                radNu.Checked = true;
        }

        private void luoi_NguoiDung_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbo_Username.Text = luoi_NguoiDung["IdNguoiDung", e.RowIndex].EditedFormattedValue.ToString();
            txt_Password.Text = luoi_NguoiDung["PassND", e.RowIndex].EditedFormattedValue.ToString();
            txt_HoTen.Text = luoi_NguoiDung["HoTen", e.RowIndex].EditedFormattedValue.ToString();
            date_NgaySinh.Text = luoi_NguoiDung["NgaySinh", e.RowIndex].EditedFormattedValue.ToString();
            if (luoi_NguoiDung["GioiTinh", e.RowIndex].EditedFormattedValue.ToString() == "Nam")
                radNam.Checked = true;
            else
                radNu.Checked = true;
            txt_SoDienThoai.Text = luoi_NguoiDung["SoDT", e.RowIndex].EditedFormattedValue.ToString();
            txt_DiaChi.Text = luoi_NguoiDung["DiaChi", e.RowIndex].EditedFormattedValue.ToString();
        }

        private void luoi_NguoiDung_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (luoi_NguoiDung.CurrentRow.Cells[4].Value.ToString() == "Nam")
                radNam.Checked = true;
            else
                radNu.Checked = true;
        }
        #endregion

        #region "Xử lý button cấp pass hoàn tất"
        private void Button_CapPass_Click(object sender, EventArgs e)
        {
            if (cbo_IdLoaiND.Text == "Nhan_Vien")
            {
                Form_Cap_pass frm = new Form_Cap_pass(this);
                frm.ShowDialog();
            }
            else if (cbo_IdLoaiND.Text == "Admin")
                MessageBox.Show("Bạn không được cấp pass cho người Admin, vui lòng chọn 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("Bạn không được cấp pass cho người quản lý, vui lòng chọn 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        #endregion
        #endregion

        //------------------------------------------------Xử lý xe-----------------------------------------------------------

        #region "Đã xong"

        public void UpdateXe()
        {
            Doc_bang_Xe();
            Tao_lien_ket_xe();
            vi_tri_hien_hanh_xe = 0;
            Xuat_thong_tin_Xe();
            Luoi_Xe.ReadOnly = true;
            Lock_Control_xe(false);
            LockButton_xe(false);

            cbo_HieuXe.Items.Add("Ford Transit");
            cbo_HieuXe.Items.Add("Huyndai Country");
            cbo_HieuXe.Items.Add("Toyota");
            cbo_HieuXe.Items.Add("Ford Everest");
            cbo_HieuXe.Items.Add("Huyndai");
        }

        #region "Đọc bảng xe đã xử lý xong"
        private void Doc_bang_Xe()
        {
            // Làm sạch lưới sau mỗi lần cập nhật
            Luoi_Xe.ClearSelection();
            lenh_xe = "Select * from Xe";
            bang_xe = Ket_noi.Doc_bang(lenh_xe);
            Luoi_Xe.DataSource = bang_xe;
        }
        #endregion

        #region "Xử lý các nút di chuyển và xuất thông tin xe đã hoàn tất"
        private void Xuat_thong_tin_Xe()
        {
            DataRow dong = bang_xe.Rows[vi_tri_hien_hanh_xe];
            cbo_SoXe.Text = dong["So_Xe"].ToString();
            cbo_HieuXe.Text = dong["Hieu_Xe"].ToString();
            txt_DoiXe.Text = Convert.ToString(dong["Doi_Xe"]);
            cbo_SoChoNgoi.Text = dong["So_Cho_Ngoi"].ToString();
        }

        public void Di_chuyen_ve_sau_xe()
        {
            if (vi_tri_hien_hanh_xe < bang_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh_xe += 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_truoc_xe()
        {
            if (vi_tri_hien_hanh_xe > 0)
            {
                vi_tri_hien_hanh_xe -= 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_dau_xe()
        {
            vi_tri_hien_hanh_xe = 0;
            Xuat_thong_tin_Xe();
        }

        public void Di_chuyen_ve_cuoi_xe()
        {
            vi_tri_hien_hanh_xe = bang_xe.Rows.Count - 1;
            Xuat_thong_tin_Xe();
        }
        #endregion

        #region "Tạo liên kết giữa các điều khiển với datagridview đã hoàn tất"
        private void Tao_lien_ket_xe()
        {
            if (cbo_SoChoNgoi.Text == "")
            {
                cbo_SoChoNgoi.Items.Add(7);
                cbo_SoChoNgoi.Items.Add(16);
                cbo_SoChoNgoi.Items.Add(25);
                cbo_SoChoNgoi.Items.Add(30);
                cbo_SoChoNgoi.Items.Add(45);
            }
            cbo_SoXe.DataSource = Luoi_Xe.DataSource;
            cbo_SoXe.DisplayMember = "So_Xe";
            cbo_SoXe.ValueMember = "So_Xe";
            cbo_SoXe.SelectedValue = "So_Xe";

            Xoa_lien_ket_xe();

            cbo_SoXe.Text = (String)Luoi_Xe.Rows[0].Cells[0].Value;
            cbo_HieuXe.DataBindings.Add("text", Luoi_Xe.DataSource, "Hieu_Xe");
            txt_DoiXe.DataBindings.Add("text", Luoi_Xe.DataSource, "Doi_Xe");
            cbo_SoChoNgoi.DataBindings.Add("text", Luoi_Xe.DataSource, "So_Cho_Ngoi");
        }

        #endregion

        #region "Xóa liên kết giữa các điều khiển với datagridview đã hoàn tất"
        private void Xoa_lien_ket_xe()
        {
            cbo_SoXe.DataBindings.Clear();
            cbo_HieuXe.DataBindings.Clear();
            txt_DoiXe.DataBindings.Clear();
            cbo_SoChoNgoi.DataBindings.Clear();
        }
        #endregion

        #region "Thêm, sửa, xóa xe đã hoàn tất"
        public void Them_Xe()
        {
            flag_xe = true;
            Lock_Control_xe(true);
            LockButton_xe(true);
            Clear_Control_xe();
        }

        public void Sua_Xe()
        {
            flag_xe = false;
            Lock_Control_xe(true);
            LockButton_xe(true);
            Luoi_Xe.ReadOnly = false;
            cbo_SoXe.Enabled = false;
        }

        public void Luu_thay_doi_xe()
        {
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
                Ket_noi.connect.Close();
            {
                if (flag_xe == true)
                {
                    if (TestInfo_xe())
                    {
                        DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm xe này không?\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dg == DialogResult.OK)
                        {
                            int flag = 0;
                            SqlCommand sqlCM = new SqlCommand("select So_Xe from Xe", Ket_noi.connect);
                            SqlDataReader sqlDR;
                            Ket_noi.connect.Open();
                            sqlDR = sqlCM.ExecuteReader();
                            while (sqlDR.Read() == true)
                            {
                                if (sqlDR.GetValue(0).ToString() == cbo_SoXe.Text)
                                {
                                    flag = 1;
                                    MessageBox.Show("Số xe " + cbo_SoXe.Text + " đã tồn tại, vui lòng kiểm tra lại số xe bạn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            Ket_noi.connect.Close();
                            if (flag == 0)
                            {
                                lenh_xe = "Insert into Xe(So_Xe, Hieu_Xe, Doi_Xe, So_Cho_Ngoi)";
                                lenh_xe += " Values ('" + cbo_SoXe.Text + "', '" + cbo_HieuXe.Text + "', '" + txt_DoiXe.Text + "', '" + cbo_SoChoNgoi.Text + "')";
                                SqlCommand bo_lenh = new SqlCommand(lenh_xe, Ket_noi.connect);
                                Ket_noi.connect.Open();
                                try
                                {
                                    bo_lenh.ExecuteNonQuery();
                                    Ket_noi.connect.Close();
                                    Doc_bang_Xe();
                                    Tao_lien_ket_xe();
                                    vi_tri_hien_hanh_xe = 0;
                                    Xuat_thong_tin_Xe();
                                    cbo_HieuXe.Items.Clear();
                                    Luoi_Xe.Enabled = true;
                                    MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Không cập nhật được dữ liệu, thêm xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Ket_noi.connect.Close();
                                }
                            }
                        }
                        else
                            Huy_thao_tac_xe();
                    }
                }
                else
                   // Sua thong tin nguoi dung
                   if (TestInfo_xe())
                   {
                   DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin xe này?\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                   if (dialog == DialogResult.OK)
                   {
                        lenh_xe = "Update Xe Set Hieu_Xe = '" + cbo_HieuXe.Text + "', Doi_Xe = '" + txt_DoiXe.Text + "', So_Cho_Ngoi = '" + cbo_SoChoNgoi.Text + "' where So_Xe = '" + cbo_SoXe.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh_xe, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            UpdateXe();
                            Lock_Control_xe(false);
                            LockButton_xe(false);
                            Luoi_Xe.Enabled = true;
                            MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, sửa thông tin xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                   }
                    else
                        Huy_thao_tac_xe();
                }
            }
        }
        #endregion

        #region "Xử lý hủy thao tác cập nhật đã hoàn tất"
        public void Huy_thao_tac_xe()
        {
            Luoi_Xe.Enabled = true;
            Xoa_lien_ket_xe();
            Lock_Control_xe(false);
            LockButton_xe(false);
            UpdateXe();
        }
        #endregion

        #region "Xóa xe hoàn tất"
        public void Xoa_Xe()
        {
            DialogResult qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - So xe: " + cbo_SoXe.Text + "\n - Hieu xe: " + cbo_HieuXe.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                    MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không cập nhật được dữ liệu, xóa xe không thành công", "Thông báo");
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
        }
        #endregion

        #region "Các xử lý phụ với các điều khiển hoàn tất"
        private void Lock_Control_xe(bool f)
        {
                cbo_SoXe.Enabled = true;
                cbo_HieuXe.Enabled = f;
                txt_DoiXe.Enabled = f;
                cbo_SoChoNgoi.Enabled = f;
        }

        private void Clear_Control_xe()
        {
                cbo_SoXe.Text = "";
                cbo_HieuXe.Text = "";
                txt_DoiXe.Text = "";
                cbo_SoXe.Focus();
        }

        private void LockButton_xe(bool dt)
        {
                btn_ThemXe.Enabled = !dt;
                btn_SuaXe.Enabled = !dt;
                btn_XoaXe.Enabled = !dt;
                btn_LuuXe.Enabled = dt;
                btn_HuyXe.Enabled = dt;
        }

        private bool TestInfo_xe()
        {
            bool check = true;
            if (cbo_SoXe.Text.Trim() == "" || cbo_HieuXe.Text.Trim() == "" || txt_DoiXe.Text.Trim() == "" || cbo_SoChoNgoi.Text == "")
            {
                check = false;
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
            }

            if (cbo_SoXe.Text.Trim() == "")
            {
                cbo_SoXe.Focus();
            }
            else if (cbo_HieuXe.Text.Trim() == "")
            {
                cbo_HieuXe.Focus();
            }
            else if (txt_DoiXe.Text.Trim() == "")
            {
                txt_DoiXe.Focus();
            }
            else if (cbo_SoChoNgoi.Text.Trim() == "")
            {
                cbo_SoChoNgoi.Focus();
            }

            if (cbo_SoXe.Text.Trim().Length > 8)
            {
                check = false;
                MessageBox.Show("Số xe không được lớn hơn 8 kí tự!", "Thông báo lỗi");
                txt_Password.Focus();
            }

            return check;
        }
        #endregion

        #region "Xử lý các nút di chuyển xe đã hoàn tất"
        private void btn_Top_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_dau_xe();
        }

        private void btn_Xe_Back_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_truoc_xe();
        }

        private void btn_Xe_End_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_cuoi_xe();
        }

        private void btn_Xe_Next_Click(object sender, EventArgs e)
        {
            Di_chuyen_ve_sau_xe();
        }

        #endregion

        #region "Xử lý thêm, xóa, sửa xe đã hoàn tất"
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
            Luu_thay_doi_xe();
        }

        private void btn_HuyXe_Click(object sender, EventArgs e)
        {
            Huy_thao_tac_xe();
        }

        #endregion

        #region "Xử lý sự kiện enter trên lưới xe"
        private void Luoi_Xe_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbo_SoXe.Text = Luoi_Xe["So_Xe", e.RowIndex].EditedFormattedValue.ToString();
            cbo_HieuXe.Text = Luoi_Xe["Hieu_Xe", e.RowIndex].EditedFormattedValue.ToString();
            txt_DoiXe.Text = Luoi_Xe["Doi_Xe", e.RowIndex].EditedFormattedValue.ToString();
            cbo_SoChoNgoi.Text = Luoi_Xe["So_Cho_Ngoi", e.RowIndex].EditedFormattedValue.ToString();
        }
        #endregion
        #endregion

        //--------------------------------------------------Tuyến Xe----------------------------------------------------

        #region "Đã xong"

        public void UpdateTuyenXe()
        {
            Doc_bang_tuyen_xe();
            Tao_lien_ket_tuyen_xe();
            vi_tri_hien_hanh_tuyen_xe = 0;
            Xuat_thong_tin_Tuyen_xe();
            luoi_Tuyen_xe.ReadOnly = true;
            Lock_Control_tuyen_xe(false);
            LockButton_tuyen_xe(false);
        }

        #region "Đọc bảng tuyến xe đã xong"
        private void Doc_bang_tuyen_xe()
        {
            // Làm sạch lưới sau mỗi lần cập nhật
            luoi_Tuyen_xe.ClearSelection();
            lenh_tuyen_xe = "Select * from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh_tuyen_xe);
            luoi_Tuyen_xe.DataSource = bang_tuyen_xe;
        }
        #endregion

        #region "Tạo liên kết giữa các điều khiển với datagridview đã hoàn tất"
        private void Tao_lien_ket_tuyen_xe()
        {
            {
                var withBlock = cbo_IdTuyen;
                withBlock.DataSource = luoi_Tuyen_xe.DataSource;
                withBlock.DisplayMember = "IdTuyen";
                withBlock.ValueMember = "IdTuyen";
                withBlock.SelectedValue = "IdTuyen";
            }
            Xoa_lien_ket_tuyen_xe();

            cbo_IdTuyen.Text = (String)luoi_Tuyen_xe.Rows[0].Cells[0].Value;
            cbo_TenTuyen.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "TenTuyen");
            cbo_DiaDiemDi.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "DiaDiemDi");
            cbo_DiaDiemDen.DataBindings.Add("text", luoi_Tuyen_xe.DataSource, "DiaDiemDen");
        }
        #endregion

        #region "Xóa liên kết giữa các điều khiển với datagridview đã hoàn tất"
        private void Xoa_lien_ket_tuyen_xe()
        {
            cbo_TenTuyen.DataBindings.Clear();
            cbo_DiaDiemDi.DataBindings.Clear();
            cbo_DiaDiemDen.DataBindings.Clear();
        }
        #endregion

        #region "Xử lý các nút di chuyển và xuất thông tin tuyến xe đã hoàn tất"
        private void Xuat_thong_tin_Tuyen_xe()
        {
            DataRow dong = bang_tuyen_xe.Rows[vi_tri_hien_hanh_tuyen_xe];
            {
                cbo_IdTuyen.Text = dong["IdTuyen"].ToString();
                cbo_TenTuyen.Text = dong["TenTuyen"].ToString();
                cbo_DiaDiemDi.Text = dong["DiaDiemDi"].ToString();
                cbo_DiaDiemDen.Text = dong["DiaDiemDen"].ToString();
            }
        }

        public void Di_chuyen_ve_sau_tuyen_xe()
        {
            if (vi_tri_hien_hanh_tuyen_xe < bang_tuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh_tuyen_xe += 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc_tuyen_xe()
        {
            if (vi_tri_hien_hanh_tuyen_xe > 0)
            {
                vi_tri_hien_hanh_tuyen_xe -= 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau_tuyen_xe()
        {
            vi_tri_hien_hanh_tuyen_xe = 0;
            Xuat_thong_tin_Tuyen_xe();
        }

        public void Di_chuyen_ve_cuoi_tuyen_xe()
        {
            vi_tri_hien_hanh_tuyen_xe = bang_tuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Tuyen_xe();
        }
        #endregion

        #region "Thêm sửa tuyến đã hoàn tất"
        public void Them_tuyen_xe()
        {
            flag_tuyen_xe = true;
            Lock_Control_tuyen_xe(true);
            LockButton_tuyen_xe(true);
            Clear_Control_tuyen_xe();
            luoi_Tuyen_xe.Enabled = false;
            for (int i = 0; i <= bang_tuyen_xe.Rows.Count - 1; i++)
                cbo_TenTuyen.Items.Add(bang_tuyen_xe.Rows[i]["TenTuyen"].ToString());
        }

        public void Sua_tuyen_xe()
        {
            flag_tuyen_xe = false;
            Lock_Control_tuyen_xe(true);
            LockButton_tuyen_xe(true);

            Luoi_Xe.ReadOnly = false;
            cbo_IdTuyen.Enabled = false;
        }

        public void Luu_tuyen_xe()
        {
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
                Ket_noi.connect.Close();
            {
                if (flag_tuyen_xe == true)
                {
                    if (TestInfo_tuyen_xe())
                    {
                        DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm tuyến xe này không.\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dg == DialogResult.OK)
                        {
                            int flag = 0;
                            SqlCommand sqlCM = new SqlCommand("select IdTuyen from TuyenXe", Ket_noi.connect);
                            SqlDataReader sqlDR;
                            Ket_noi.connect.Open();
                            sqlDR = sqlCM.ExecuteReader();
                            while (sqlDR.Read() == true)
                            {
                                if (sqlDR.GetValue(0).ToString() == cbo_IdTuyen.Text)
                                {
                                    flag = 1;
                                    MessageBox.Show("Mã số tuyến " + cbo_IdTuyen.Text + " đã tồn tại, vui lòng kiểm tra lại ma so tuyen bạn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            Ket_noi.connect.Close();
                            if (flag == 0)
                            {
                                lenh_tuyen_xe = "Insert into TuyenXe";
                                lenh_tuyen_xe += " Values ('" + cbo_IdTuyen.Text + "', '" + cbo_TenTuyen.Text + "', N'" + cbo_DiaDiemDi.Text + "', N'" + cbo_DiaDiemDen.Text + "')";
                                SqlCommand bo_lenh = new SqlCommand(lenh_tuyen_xe, Ket_noi.connect);
                                Ket_noi.connect.Open();
                                try
                                {
                                    bo_lenh.ExecuteNonQuery();
                                    Ket_noi.connect.Close();
                                    UpdateTuyenXe();
                                    Lock_Control_tuyen_xe(false);
                                    LockButton_tuyen_xe(false);
                                    luoi_Tuyen_xe.Enabled = true;
                                    MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Không cập nhật được dữ liệu, thêm xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Ket_noi.connect.Close();
                                }
                            }
                        }
                        else
                            Huy_thao_tac_tuyen_xe();
                    }
                }
                else
                   // Sua thong tin nguoi dung
                   if (TestInfo_tuyen_xe())
                {
                    DialogResult dialog = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin tuyến xe này.\nClick OK đê đồng ý, Cancel để hủy.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        lenh_tuyen_xe = "Update TuyenXe Set TenTuyen = '" + cbo_TenTuyen.Text + "', DiaDiemDi = N'" + cbo_DiaDiemDi.Text + "', DiaDiemDen = N'" + cbo_DiaDiemDen.Text + "' where IdTuyen = '" + cbo_IdTuyen.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh_tuyen_xe, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            UpdateTuyenXe();
                            Lock_Control_tuyen_xe(false);
                            LockButton_tuyen_xe(false);
                            luoi_Tuyen_xe.Enabled = true;
                            MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, sửa thông tin xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                        Huy_thao_tac_tuyen_xe();
                }
            }
        }
        #endregion

        #region "Xử lý hủy thao tác cập nhật đã hoàn tất
        public void Huy_thao_tac_tuyen_xe()
        {
            luoi_Tuyen_xe.Enabled = true;
            Xoa_lien_ket_tuyen_xe();
            Lock_Control_tuyen_xe(false);
            LockButton_tuyen_xe(false);
            UpdateTuyenXe();
        }
        #endregion

        #region "Xóa tuyến hoàn tất"
        public void Xoa_tuyen_xe()
        {
            DialogResult qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - Ma so tuyen: " + cbo_IdTuyen.Text + "\n - Ten tuyen: " + cbo_TenTuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                    MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không cập nhật được dữ liệu, xóa tuyến không thành công", "Thông báo");
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
        }
        #endregion

        #region "Các xử lý phụ với các điều khiển đã hoàn tất"
        private void Lock_Control_tuyen_xe(bool f)
        {
            {
                cbo_IdTuyen.Enabled = true;
                cbo_TenTuyen.Enabled = f;
                cbo_DiaDiemDi.Enabled = f;
                cbo_DiaDiemDen.Enabled = f;
            }
        }

        private void Clear_Control_tuyen_xe()
        {
            {
                cbo_IdTuyen.Text = "";
                cbo_TenTuyen.Text = "";
                cbo_DiaDiemDi.Text = "";
                cbo_DiaDiemDen.Text = "";
                cbo_IdTuyen.Focus();
            }
        }

        private void LockButton_tuyen_xe(bool dt)
        {
            {
                btn_ThemTuyen.Enabled = !dt;
                btn_SuaTuyen.Enabled = !dt;
                btn_XoaTuyen.Enabled = !dt;
                btn_LuuTuyen.Enabled = dt;
                btn_HuyTuyen.Enabled = dt;
            }
        }

        private bool TestInfo_tuyen_xe()
        {
            bool check = true;
            if (cbo_IdTuyen.Text.Trim() == "" || cbo_TenTuyen.Text.Trim() == "" || cbo_DiaDiemDi.Text.Trim() == "" || cbo_DiaDiemDen.Text.Trim() == "")
            {
                check = false;
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
            }

            if (cbo_IdTuyen.Text.Trim() == "")
            {
                cbo_IdTuyen.Focus();
            }
            else if (cbo_TenTuyen.Text.Trim() == "")
            {
                cbo_TenTuyen.Focus();
            }
            else if (cbo_DiaDiemDi.Text.Trim() == "")
            {
                cbo_DiaDiemDi.Focus();
            }
            else if (cbo_DiaDiemDen.Text.Trim() == "")
            {
                cbo_DiaDiemDen.Focus();
            }

            return check;
        }
        #endregion

        #region "Xử lý các button di chuyển đã xong"
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

        #region "Xử lý các button thêm, xóa, sửa hoàn tất"
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

        private void btn_XemChiTietTuyen_Click(object sender, EventArgs e)
        {
            Form_ChiTietTuyen frm_ChiTietTuyen = new Form_ChiTietTuyen();
            frm_ChiTietTuyen.ShowDialog();
        }
        #endregion

        //--------------------------------------------------Thời điểm----------------------------------------------------

        #region "Đã xong"

        public void Update_thoi_diem()
        {
            Doc_thoi_diem();
            Tao_lien_ket_thoi_diem();
            Doc_tuyen_thoi_diem();
            Hide_thoi_diem();
        }

        private void Hide_thoi_diem()
        {
            rad_KhongLap.Hide();
            rad_LapTuan.Hide();
            lbl_Lap.Hide();
            date_NgayKetThuc.Hide();
        }

        private void Show_thoi_diem()
        {
            rad_KhongLap.Show();
            rad_LapTuan.Show();
        }

        private void Doc_tuyen_thoi_diem()
        {
            lenh_thoi_diem = "Select IdTuyen, TenTuyen from TuyenXe";
            bang_tuyen_xe_thoi_diem = Ket_noi.Doc_bang(lenh_thoi_diem);
            {
                var withBlock = cbo_GanTuyen;
                withBlock.DataSource = bang_tuyen_xe_thoi_diem;
                withBlock.DisplayMember = "IdTuyen";
                withBlock.ValueMember = "IdTuyen";
            }
            {
                txt_TenTuyen.DataBindings.Clear();
                txt_TenTuyen.DataBindings.Add("Text", bang_tuyen_xe_thoi_diem, "TenTuyen");
            }
        }

        #region "Đọc thời điểm với tạo liên kết đã xong"
        private void Doc_thoi_diem()
        {
            lenh_thoi_diem = "Select * from ThoiDiem";
            bang_thoi_diem = Ket_noi.Doc_bang(lenh_thoi_diem);
            luoi_ThoiDiem.DataSource = bang_thoi_diem;
        }

        private void Tao_lien_ket_thoi_diem()
        {
            {
                var withBlock = cbo_MaThoiDiem;
                withBlock.DataSource = bang_thoi_diem;
                withBlock.DisplayMember = "IdThoiDiem";
                withBlock.ValueMember = "IdThoiDiem";
            }
            {
                date_Chay.DataBindings.Clear();
                txt_GioChay.DataBindings.Clear();

                date_Chay.DataBindings.Add("Text", bang_thoi_diem, "Ngay");
                txt_GioChay.DataBindings.Add("Text", bang_thoi_diem, "Gio");
            }
        }
        #endregion

        #region "Xử lý hỗ trợ button đã xong"
        private void Clear_Control_thoi_diem()
        {
            {
                date_Chay.Text = "";
                date_NgayKetThuc.Text = "";
                txt_GioChay.Text = "";
                rad_KhongLap.Checked = true;
                date_Chay.Focus();
            }
        }

        private void LockButton_thoi_diem(bool dt)
        {
            {
                btn_ThemThoiDiem.Enabled = !dt;
                btn_SuaThoiDiem.Enabled = !dt;
                btn_XoaThoiDiem.Enabled = !dt;
                btn_LuuThoiDiem.Enabled = dt;
                btn_HuyThoiDiem.Enabled = dt;
            }
        }
        #endregion

        public void them_thoi_diem()
        {
            flag_thoi_diem = true;
            LockButton_thoi_diem(true);
            lbl_Lap.Hide();
            date_NgayKetThuc.Hide();
            Show_thoi_diem();
            Clear_Control_thoi_diem();
            cbo_MaThoiDiem.Enabled = false;
        }

        public void Sua_thoi_diem()
        {
            rad_LapTuan.Checked = false;
            rad_KhongLap.Checked = true;
            Show_thoi_diem();
            flag_thoi_diem = false;
            LockButton_thoi_diem(true);
            cbo_MaThoiDiem.Enabled = false;
        }

        public void Luu_thoi_diem()
        {
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
                Ket_noi.connect.Close();
            {
                // Nếu như trạng thái đang là thêm
                if (flag_thoi_diem)
                {
                    if (TestInfo_thoi_diem())
                    {

                        // Kiem tra ngay gio them vao phai la chua co trong CSDL
                        SqlCommand sqlCM = new SqlCommand("select Ngay, Gio from ThoiDiem", Ket_noi.connect);
                        SqlDataReader sqlDR;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (Strings.FormatDateTime(Convert.ToDateTime(sqlDR.GetValue(0)), DateFormat.ShortDate) == date_Chay.Text && sqlDR.GetValue(1).ToString() == txt_GioChay.Text)
                            {
                                MessageBox.Show("Ngày giờ này đã tồn tại, vui lòng kiểm tra lại thông tin nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        Ket_noi.connect.Close();

                        if (rad_KhongLap.Checked == true)
                        {
                            lenh_thoi_diem = "Insert into ThoiDiem(Ngay, Gio) ";
                            lenh_thoi_diem += "Values ('" + Strings.FormatDateTime(Convert.ToDateTime(date_Chay.Text), DateFormat.ShortDate) + "', '" + txt_GioChay.Text + "')";
                            SqlCommand bo_lenh = new SqlCommand(lenh_thoi_diem, Ket_noi.connect);
                            try
                            {
                                Ket_noi.connect.Open();
                                bo_lenh.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                Update_thoi_diem();
                                LockButton_thoi_diem(false);
                                MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không cập nhật được dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                            }
                        }
                        else if (rad_LapTuan.Checked == true)
                        {
                            long i = layKhoangCach_thoi_diem();
                            if (i < 0)
                            {
                                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (i == 0)
                            {
                                lenh_thoi_diem = "Insert into ThoiDiem(Ngay, Gio) ";
                                lenh_thoi_diem += "Values ('" + Strings.FormatDateTime(Convert.ToDateTime(date_Chay.Text), DateFormat.ShortDate) + "', '" + txt_GioChay.Text + "')";
                                SqlCommand bo_lenh = new SqlCommand(lenh_thoi_diem, Ket_noi.connect);
                                try
                                {
                                    Ket_noi.connect.Open();
                                    bo_lenh.ExecuteNonQuery();
                                    Ket_noi.connect.Close();
                                    Update_thoi_diem();
                                    LockButton_thoi_diem(false);
                                    MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
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
                                var d = Convert.ToDateTime(date_Chay.Text);
                                while ((ngay <= i))
                                {
                                    lenh_thoi_diem = "Insert into ThoiDiem(Ngay, Gio) ";
                                    lenh_thoi_diem += "Values ('" + Strings.FormatDateTime(d, DateFormat.ShortDate) + "', '" + txt_GioChay.Text + "')";
                                    SqlCommand bo_lenh = new SqlCommand(lenh_thoi_diem, Ket_noi.connect);
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
                                MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                            }
                        }
                    }
                }
                else if (TestInfo_thoi_diem())
                {
                    lenh_thoi_diem = "Update ThoiDiem set Ngay = '" + Strings.FormatDateTime(Convert.ToDateTime(date_Chay.Text), DateFormat.ShortDate) + "', Gio = '" + txt_GioChay.Text + "' where IdThoiDiem = '" + cbo_MaThoiDiem.Text + "'";
                    SqlCommand sqlqr = new SqlCommand(lenh_thoi_diem, Ket_noi.connect);
                    try
                    {
                        Ket_noi.connect.Open();
                        sqlqr.ExecuteNonQuery();
                        Ket_noi.connect.Close();
                        Update_thoi_diem();
                        LockButton_thoi_diem(false);
                        MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
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
            LockButton_thoi_diem(false);
            Update_thoi_diem();
            cbo_MaThoiDiem.Enabled = true;
            Hide_thoi_diem();
        }

        public void Xoa_thoi_diem()
        {
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - Ma thoi diem: " + cbo_MaThoiDiem.Text + "\n - Ngay: " + date_Chay.Text + "\n" + " - Giờ: " + txt_GioChay.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                    MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thời điểm này đã được gán cho tuyến xe, bạn phải xóa thông tin tuyến xe đó trước!", "Thông báo");
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
        }

        private long layKhoangCach_thoi_diem()
        {
            long i;
            i = DateAndTime.DateDiff(DateInterval.Day, Convert.ToDateTime(date_Chay.Text), Convert.ToDateTime(date_NgayKetThuc.Text), FirstDayOfWeek.System, FirstWeekOfYear.System);
            return i;
        }

        private bool TestInfo_thoi_diem()
        {
            bool check = true;
            if (rad_KhongLap.Checked == true)
            {
                if (date_Chay.Text.Trim() == "" || txt_GioChay.Text.Trim() == "")
                {
                    check = false;
                    MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
                }
            }
            else if (rad_LapTuan.Checked == true)
            {
                if (date_Chay.Text.Trim() == "" || txt_GioChay.Text.Trim() == "" || date_NgayKetThuc.Text == "")
                {
                    check = false;
                    MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
                }

                if (date_Chay.Text != "" && date_NgayKetThuc.Text != "")
                {
                    if (layKhoangCach_thoi_diem() > 365)
                    {
                        check = false;
                        MessageBox.Show("Bạn chỉ được lặp tuần trong phạm vi là 1 năm hay 48 tuần", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }

            if (Convert.ToDateTime(date_Chay.Text) < DateTime.Today.Date)
            {
                check = false;
                MessageBox.Show("Ngay ban them khong duoc nho hon ngay hien tai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return check;
        }

        public void Gan_tuyen_thoi_diem()
        {
            {
                if (cbo_MaThoiDiem.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn thời điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Neu ngay gio do da gan cho tuyen do roi thi thong bao loi
                SqlCommand sqlCM = new SqlCommand("select * from ChiTietTuyen", Ket_noi.connect);
                SqlDataReader sqlDR;
                Ket_noi.connect.Open();
                sqlDR = sqlCM.ExecuteReader();
                while (sqlDR.Read() == true)
                {
                    if (sqlDR.GetValue(0).ToString() == cbo_GanTuyen.Text && sqlDR.GetValue(1).ToString() == cbo_MaThoiDiem.Text)
                    {
                        MessageBox.Show("Thời điểm này đã được gán cho tuyến " + cbo_GanTuyen.Text + " rồi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Ket_noi.connect.Close();
                        return;
                    }
                }
                Ket_noi.connect.Close();

                // Sau khi kiem tra logic thi bat dau gan tuyen
                lenh_thoi_diem = "Insert into ChiTietTuyen values('" + cbo_GanTuyen.Text + "', '" + cbo_MaThoiDiem.Text + "')";
                SqlCommand bo_lenh = new SqlCommand(lenh_thoi_diem, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    bo_lenh.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    MessageBox.Show("Ngày " + date_Chay.Text + " Giờ: " + txt_GioChay.Text + " đã được gán cho tuyến " + cbo_GanTuyen.Text, "Thông báo");
                    Update_thoi_diem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gán tuyến không thành công", "Thông báo");
                }
            }
        }

        #region "Xử lý các button thêm xóa sửa hoàn tất"
        private void btn_ThemThoiDiem_Click(object sender, EventArgs e)
        {
            them_thoi_diem();
        }

        private void btn_SuaThoiDiem_Click(object sender, EventArgs e)
        {
            Sua_thoi_diem();
        }

        private void btn_HuyThoiDiem_Click(object sender, EventArgs e)
        {
            Huy_thoi_diem();
        }

        private void btn_LuuThoiDiem_Click(object sender, EventArgs e)
        {
            Luu_thoi_diem();
        }

        private void btn_XoaThoiDiem_Click(object sender, EventArgs e)
        {
            Xoa_thoi_diem();
        }
        #endregion

        private void rad_LapTuan_CheckedChanged(object sender, EventArgs e)
        {
            lbl_Lap.Show();
            date_NgayKetThuc.Show();
        }

        private void btn_GanTuyen_Click(object sender, EventArgs e)
        {
            Gan_tuyen_thoi_diem();
        }
        #endregion

        //--------------------------------------------------Chuyến xe----------------------------------------------------

        #region "Đã xong"
        public void Update_Chuyen_xe()
        {
            Doc_chuyen_xe();
            Tao_lien_ket_chuyen_xe();
            Lock_Control_chuyen_xe(false);
            vi_tri_hien_hanh_chuyen_xe = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        private void Doc_chuyen_xe()
        {
            Luoi_Chuyen_xe.ClearSelection();
            lenh_chuyen_xe = "Select * from ChuyenXe";
            bang_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            Luoi_Chuyen_xe.DataSource = bang_chuyen_xe;
        }

        private void Tao_lien_ket_chuyen_xe()
        {
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                {
                    var withBlock = cbo_IdChuyen;
                    withBlock.DataSource = bang_chuyen_xe;
                    withBlock.DisplayMember = "IdChuyen";
                    withBlock.ValueMember = "IdChuyen";
                }
                Xoa_lien_ket_chuyen_xe();

                cbo_IdChuyen.Text = Luoi_Chuyen_xe.Rows[0].Cells[0].Value.ToString();

                {
                    cbo_IdTuyenChuyen.DataBindings.Add("Text", Luoi_Chuyen_xe.DataSource, "IdTuyen");
                    cbo_SoXeChuyen.DataBindings.Add("Text", Luoi_Chuyen_xe.DataSource, "So_Xe");
                    cbo_NgayDiChuyen.DataBindings.Add("Text", Luoi_Chuyen_xe.DataSource, "NgayDi");
                    cbo_GioDiChuyen.DataBindings.Add("Text", Luoi_Chuyen_xe.DataSource, "Gio");
                }
            }
        }

        private void Xoa_lien_ket_chuyen_xe()
        {
            {
                cbo_IdTuyenChuyen.DataBindings.Clear();
                cbo_SoXeChuyen.DataBindings.Clear();
                cbo_NgayDiChuyen.DataBindings.Clear();
                cbo_GioDiChuyen.DataBindings.Clear();
            }
        }

        private void Lock_Control_chuyen_xe(bool f)
        {
            {
                cbo_IdChuyen.Enabled = !f;
                cbo_IdTuyenChuyen.Enabled = f;
                cbo_SoXeChuyen.Enabled = f;
                cbo_NgayDiChuyen.Enabled = f;
                cbo_GioDiChuyen.Enabled = f;
                Luoi_Chuyen_xe.Enabled = !f;
            }
        }

        private void Clear_Control_chuyen_xe()
        {
            {
                cbo_IdChuyen.Text = "";
                cbo_IdTuyenChuyen.Text = "";
                cbo_NgayDiChuyen.Text = "";
                cbo_GioDiChuyen.Text = "";
                txt_SoDienThoai.Text = "";
                cbo_SoXeChuyen.Text = "";
                cbo_IdTuyenChuyen.Focus();
            }
        }

        private void LockButton_chuyen_xe(bool dt)
        {
            {
                btn_ThemChuyen.Enabled = !dt;
                btn_SuaChuyen.Enabled = !dt;
                btn_XoaChuyen.Enabled = !dt;
                btn_LuuChuyen.Enabled = dt;
                btn_HuyChuyen.Enabled = dt;
            }
        }

        public void Them_chuyen_xe()
        {
            Xoa_lien_ket_chuyen_xe();
            flag_chuyen_xe = true;
            Lock_Control_chuyen_xe(true);
            LockButton_chuyen_xe(true);
            Doc_tuyen_xe();
            Doc_xe();
            Clear_Control_chuyen_xe();
        }

        public void Sua_chuyen_xe()
        {
            flag = false;
            Lock_Control_chuyen_xe(true);
            LockButton_chuyen_xe(true);
            Doc_tuyen_xe();
            Doc_xe();
        }

        public void Huy_chuyen_xe()
        {
            Xoa_lien_ket_chuyen_xe();
            Lock_Control_chuyen_xe(false);
            LockButton_chuyen_xe(false);
            Update_Chuyen_xe();
        }

        private void Doc_tuyen_xe()
        {
            lenh_chuyen_xe = "Select IdTuyen from TuyenXe";
            bang_tuyen_xe_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            {
                var withBlock = cbo_IdTuyenChuyen;
                withBlock.DataSource = bang_tuyen_xe_chuyen_xe;
                withBlock.DisplayMember = "IdTuyen";
                withBlock.ValueMember = "IdTuyen";
            }
        }

        public void Chon_tuyen()
        {
            if (cbo_IdTuyenChuyen.SelectedIndex < 0)
                return; // Nghĩa là không chọn mục nào cả
            Loc_Thoi_diem_theo_Tuyen(cbo_IdTuyenChuyen.SelectedValue.ToString());
        }

        private void Loc_Thoi_diem_theo_Tuyen(string IdTuyen)
        {
            lenh_chuyen_xe = "Select Distinct Ngay from ThoiDiem, ChiTietTuyen where IdTuyen = '" + IdTuyen + "' and ThoiDiem.IdThoiDiem = ChiTietTuyen.IdThoiDiem";
            bang_Chi_tiet_tuyen_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            {
                var withBlock = cbo_NgayDiChuyen;
                withBlock.DataSource = bang_Chi_tiet_tuyen_chuyen_xe;
                withBlock.ValueMember = "Ngay";
                withBlock.DisplayMember = "Ngay";
            }
        }

        public void Chon_ngay()
        {
            if (cbo_NgayDiChuyen.SelectedIndex < 0)
                return; // Nghĩa là không chọn mục nào cả
            Loc_gio_theo_ngay(cbo_NgayDiChuyen.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay(string ngay)
        {
            lenh_chuyen_xe = "Select Gio from ThoiDiem where Ngay = '" + ngay + "'";
            bang_Thoi_diem_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            {
                var withBlock = cbo_GioDiChuyen;
                withBlock.DataSource = bang_Thoi_diem_chuyen_xe;
                withBlock.ValueMember = "Gio";
                withBlock.DisplayMember = "Gio";
            }
        }

        private void Doc_xe()
        {
            lenh_chuyen_xe = "Select So_Xe from Xe";
            bang_xe_chuyen_xe = Ket_noi.Doc_bang(lenh_chuyen_xe);
            {
                var withBlock = cbo_SoXeChuyen;
                withBlock.DataSource = bang_xe_chuyen_xe;
                withBlock.ValueMember = "So_Xe";
                withBlock.DisplayMember = "So_Xe";
            }
        }

        public void Luu_chuyen_xe()
        {
            if (Ket_noi.connect.State == ConnectionState.Open)
                Ket_noi.connect.Close();
            {
                if (flag_chuyen_xe == true)
                {
                    if (TestInfo_chuyen_xe())
                    {
                        DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn thêm chuyến xe này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            // Kiem tra xem chuyen xe do co bi trung khong
                            SqlCommand sqlCM = new SqlCommand("select IdTuyen, NgayDi, Gio, So_Xe from ChuyenXe", Ket_noi.connect);
                            SqlDataReader sqlDR;
                            Ket_noi.connect.Open();
                            sqlDR = sqlCM.ExecuteReader();
                            while (sqlDR.Read() == true)
                            {
                                if (sqlDR.GetValue(0).ToString() == cbo_IdTuyenChuyen.Text && Strings.FormatDateTime((DateTime)sqlDR.GetValue(1), DateFormat.ShortDate) == cbo_NgayDiChuyen.Text && sqlDR.GetValue(2).ToString() == cbo_GioDiChuyen.Text && sqlDR.GetValue(3).ToString() == cbo_SoXeChuyen.Text)
                                {
                                    MessageBox.Show("Xe " + cbo_SoXeChuyen.Text + " đã được gán cho tuyến " + cbo_IdTuyenChuyen.Text + " vào thời điểm này rồi, vui lòng chọn xe khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Ket_noi.connect.Close();
                                    return;
                                }

                                // Kiem tra hai gio chay cua xe do trong ngay khong duoc chenh lech it nhat la quá 3 tiếng
                                if (Strings.FormatDateTime((DateTime)sqlDR.GetValue(1), DateFormat.ShortDate) == cbo_NgayDiChuyen.Text && sqlDR.GetValue(3).ToString() == cbo_SoXeChuyen.Text)
                                {
                                    // Cat chuoi gio cua chuyen da co va gio cua chuyen muon them moi
                                    string gioDaCo = sqlDR.GetValue(2).ToString();
                                    string gioMuonThem = cbo_GioDiChuyen.Text;
                                    int i, j;
                                    if (gioDaCo.Length == 4 & gioMuonThem.Length == 4 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 2 & gioMuonThem.Length == 2 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 3 & gioMuonThem.Length == 2 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 2 & gioMuonThem.Length == 3 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 5 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 2 & gioMuonThem.Length == 5 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 5 & gioMuonThem.Length == 2 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 3 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 5 & gioMuonThem.Length == 3 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 5 & gioMuonThem.Length == 4 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }

                                    if (gioDaCo.Length == 4 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                    {
                                        i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                        j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                        if (i - j < 3 || j - i < 3)
                                        {
                                            MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Ket_noi.connect.Close();
                                            return;
                                        }
                                    }
                                }
                            }
                            Ket_noi.connect.Close();

                            // Bat dau insert dulieu
                            lenh_chuyen_xe = "Insert into ChuyenXe(IdTuyen, NgayDi, Gio, So_Xe)";
                            lenh_chuyen_xe += " Values ('" + cbo_IdTuyenChuyen.Text + "', '" + cbo_NgayDiChuyen.Text + "', '" + cbo_GioDiChuyen.Text + "', '" + cbo_SoXeChuyen.Text + "')";
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
                                MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không cập nhật được dữ liệu, thêm chuyen thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                            }
                        }
                        else
                            Huy_chuyen_xe();
                    }
                }
                else if (TestInfo_chuyen_xe())
                {
                    DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn sửa thông tin chuyến xe này không? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        SqlCommand sqlCM = new SqlCommand("select IdTuyen, NgayDi, Gio, So_Xe from ChuyenXe", Ket_noi.connect);
                        SqlDataReader sqlDR;
                        Ket_noi.connect.Open();
                        sqlDR = sqlCM.ExecuteReader();
                        while (sqlDR.Read() == true)
                        {
                            if (sqlDR.GetValue(0).ToString() == cbo_IdTuyenChuyen.Text && Strings.FormatDateTime((DateTime)sqlDR.GetValue(1), DateFormat.ShortDate) == cbo_NgayDiChuyen.Text && sqlDR.GetValue(2).ToString() == cbo_GioDiChuyen.Text && sqlDR.GetValue(3).ToString() == cbo_SoXeChuyen.Text)
                            {
                                MessageBox.Show("Xe " + cbo_SoXeChuyen.Text + " đã được gán cho tuyến " + cbo_IdTuyenChuyen.Text + " vào thời điểm này rồi, vui lòng chọn xe khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Ket_noi.connect.Close();
                                return;
                            }
                            // Kiem tra hai gio chay cua xe do trong ngay khong duoc chenh lech it nhat la quá 3 tiếng
                            if (Strings.FormatDateTime((DateTime)sqlDR.GetValue(1), DateFormat.ShortDate) == cbo_NgayDiChuyen.Text && sqlDR.GetValue(3).ToString() == cbo_SoXeChuyen.Text)
                            {
                                // Cat chuoi gio cua chuyen da co va gio cua chuyen muon them moi
                                string gioDaCo = sqlDR.GetValue(2).ToString();
                                string gioMuonThem = cbo_GioDiChuyen.Text;
                                int i, j;
                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 4 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 2 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 2 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 3 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 2 & gioMuonThem.Length == 5 && gioDaCo.Substring(2, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 1));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 2 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(2, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 1));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 3 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 3 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 5 & gioMuonThem.Length == 4 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }

                                if (gioDaCo.Length == 4 & gioMuonThem.Length == 5 && gioDaCo.Substring(3, 1) == "h" && gioMuonThem.Substring(3, 1) == "h")
                                {
                                    i = Convert.ToInt32(gioDaCo.Substring(0, 2));
                                    j = Convert.ToInt32(gioMuonThem.Substring(0, 2));
                                    if (i - j < 3 || j - i < 3)
                                    {
                                        MessageBox.Show("Một xe chạy trong cùng 1 ngày không được có thời gian cách nhau nhỏ hơn 3 tiếng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Ket_noi.connect.Close();
                                        return;
                                    }
                                }
                            }
                        }
                        Ket_noi.connect.Close();
                        SqlDataReader dr;
                        string lenh3 = "Select IdChuyen from ChoNgoi";
                        SqlCommand bo_lenh = new SqlCommand(lenh3, Ket_noi.connect);
                        Ket_noi.connect.Open();
                        dr = bo_lenh.ExecuteReader();
                        while (dr.Read() == true)
                        {
                            if (dr.GetValue(0).ToString() == cbo_IdChuyen.Text)
                            {
                                MessageBox.Show("Chuyến xe đã có nguoi đặt chỗ rồi, bạn không được sưa vì sẽ làm mất uy tính khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Ket_noi.connect.Close();
                                return;
                            }
                        }
                        Ket_noi.connect.Close();
                        lenh_chuyen_xe = "Update ChuyenXe Set IdTuyen = '" + cbo_IdTuyenChuyen.Text + "', NgayDi = '" + cbo_NgayDiChuyen.Text + "', Gio = '" + cbo_GioDiChuyen.Text + "', So_Xe = '" + cbo_SoXeChuyen.Text + "' where IdChuyen = '" + cbo_IdTuyenChuyen.Text + "'";
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
                            MessageBox.Show("Đã cập nhật dữ liệu thành công", "Thông báo");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không cập nhật được dữ liệu, sửa thông tin chuyen xe thông thành công.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Ket_noi.connect.Close();
                        }
                    }
                    else
                        Huy_chuyen_xe();
                }
            }
            Update_Ve_xe_ban_ve();
        }

        private bool TestInfo_chuyen_xe()
        {
            bool check = true;
            if (cbo_IdTuyenChuyen.Text.Trim() == "" || cbo_NgayDiChuyen.Text.Trim() == "" || cbo_GioDiChuyen.Text.Trim() == "" || cbo_SoXeChuyen.Text.Trim() == "")
            {
                check = false;
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo lỗi");
            }

            if (cbo_IdTuyenChuyen.Text.Trim() == "")
            {
                cbo_IdTuyenChuyen.Focus();
            }
            else if (cbo_NgayDiChuyen.Text.Trim() == "")
            {
                cbo_NgayDiChuyen.Focus();
            }
            else if (cbo_GioDiChuyen.Text.Trim() == "")
            {
                cbo_GioDiChuyen.Focus();
            }
            else if (cbo_SoXeChuyen.Text.Trim() == "")
            {
                cbo_SoXeChuyen.Focus();
            }

            return check;
        }

        public void Xoa_chuyen_xe()
        {
            DialogResult qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - Chuyến xe: " + cbo_IdChuyen.Text + "\n - Tuyến xe: " + cbo_IdTuyenChuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from ChuyenXe where IdChuyen = '" + cbo_IdChuyen.SelectedValue.ToString() + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    Update_Chuyen_xe();
                    MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không cập nhật được dữ liệu, xóa chuyến không thành công\n" + ex.ToString(), "Thông báo");
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
        }

    #region "Xử lý các nút di chuyển và xuất thông tin xe đã hoàn tất"
        private void Xuat_thong_tin_Chuyen_xe()
        {
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                DataRow dong = bang_chuyen_xe.Rows[vi_tri_hien_hanh_chuyen_xe];
                {
                    cbo_IdChuyen.Text = dong["IdChuyen"].ToString();
                    cbo_IdTuyenChuyen.Text = dong["IdTuyen"].ToString();
                    cbo_NgayDiChuyen.Text = Convert.ToString(dong["NgayDi"]);
                    cbo_GioDiChuyen.Text = dong["Gio"].ToString();
                }
            }
        }

        public void Di_chuyen_ve_sau_chuyen_xe()
        {
            if (vi_tri_hien_hanh_chuyen_xe < bang_chuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh_chuyen_xe += 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc_chuyen_xe()
        {
            if (vi_tri_hien_hanh_chuyen_xe > 0)
            {
                vi_tri_hien_hanh_chuyen_xe -= 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau_chuyen_xe()
        {
            vi_tri_hien_hanh_chuyen_xe = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        public void Di_chuyen_ve_cuoi_chuyen_xe()
        {
            vi_tri_hien_hanh_chuyen_xe = bang_chuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Chuyen_xe();
        }
    #endregion

    #region "Xử lí các button di chuyển đã hoàn tất"
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
        #endregion

    #region "xử lý các button thêm xóa sửa hoàn tất"
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

        #endregion

        private void cbo_IdChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_tuyen();
        }

        private void cbo_NgayDiChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_ngay();
        }
        #endregion

        //--------------------------------------------------Bán vé----------------------------------------------------

        #region "Đã xong"
        public void Update_Ve_xe_ban_ve()
        {
            Doc_tuyen_xe_ban_ve();
            doc_bang_ve_ban_ve();
            Clear_Controls_ban_ve();
        }

        private void doc_bang_ve_ban_ve()
        {
            lenh_ban_ve = "Select IdVe, TenHanhKhach, SDTHanhKhach, TenTuyen, NgayDi, Gio, So_Xe from BanVe, ChuyenXe, TuyenXe ";
            lenh_ban_ve += " where BanVe.IdChuyen = ChuyenXe.IdChuyen and ChuyenXe.IdTuyen = TuyenXe.IdTuyen";
            bang_dat_ve_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            {
                var withBlock = cbo_MaSoVe;
                withBlock.DataSource = bang_dat_ve_ban_ve;
                withBlock.ValueMember = "IdVe";
                withBlock.DisplayMember = "IdVe";
            }

            // Tao lien ket
            luoi_ThongTinDatVe.DataSource = bang_dat_ve_ban_ve;
        }

        private void Clear_Controls_ban_ve()
        {
            cbo_TenTuyenVe.Text = "";
            cbo_NgayVe.Text = "";
            cbo_GioVe.Text = "";
            cbo_XeVe.Text = "";
        }

        private void Doc_tuyen_xe_ban_ve()
        {
            lenh_ban_ve = "Select Distinct ChuyenXe.IdTuyen, TenTuyen from ChuyenXe, TuyenXe where TuyenXe.IdTuyen = ChuyenXe.IdTuyen";
            bang_tuyen_xe_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            var withBlock = cbo_TenTuyenVe;
            withBlock.DataSource = bang_tuyen_xe_ban_ve;
            withBlock.DisplayMember = "TenTuyen";
            withBlock.ValueMember = "IdTuyen";
        }

        public void Chon_tuyen_ban_ve()
        {
            if (cbo_TenTuyenVe.SelectedIndex < 0)
                return;
            Loc_ngay_theo_tuyen_ban_ve(cbo_TenTuyenVe.SelectedValue.ToString());
        }

        private void Loc_ngay_theo_tuyen_ban_ve(string IdTuyen)
        {
            lenh_ban_ve = "Select Distinct NgayDi from ChuyenXe where IdTuyen = '" + IdTuyen + "'";
            bang_Thoi_diem_ngay_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            {
                var withBlock = cbo_NgayVe;
                withBlock.DataSource = bang_Thoi_diem_ngay_ban_ve;
                withBlock.ValueMember = "NgayDi";
                withBlock.DisplayMember = "NgayDi";
            }
        }

        public void Chon_ngay_ban_ve()
        {
            if (cbo_GioVe.Text == "" & cbo_XeVe.Text == "")
                cbo_NgayVe.Text = "";
            if (cbo_NgayVe.SelectedIndex < 0)
                return;
            Loc_gio_theo_ngay_ban_ve(cbo_NgayVe.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay_ban_ve(string Ngay)
        {
            lenh_ban_ve = "Select Gio from ChuyenXe where NgayDi = '" + Ngay + "' and IdTuyen = '" + cbo_TenTuyenVe.SelectedValue.ToString() + "'";
            bang_Thoi_diem_gio_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            {
                var withBlock = cbo_GioVe;
                withBlock.DataSource = bang_Thoi_diem_gio_ban_ve;
                withBlock.ValueMember = "Gio";
                withBlock.DisplayMember = "Gio";
            }
        }

        public void Chon_xe_ban_ve()
        {
            if (cbo_XeVe.Text == "")
                cbo_GioVe.Text = "";
            if (cbo_GioVe.SelectedIndex < 0)
                return;
            Loc_xe_theo_gio_ban_ve(cbo_GioVe.SelectedValue.ToString());
        }

        private void Loc_xe_theo_gio_ban_ve(string Gio)
        {
            lenh_ban_ve = "Select So_Xe from ChuyenXe where Gio = '" + Gio + "' and IdTuyen = '" + cbo_TenTuyenVe.SelectedValue.ToString() + "' and NgayDi = '" + cbo_NgayVe.SelectedValue.ToString() + "'";
            bang_Xe_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            {
                var withBlock = cbo_XeVe;
                withBlock.DataSource = bang_Xe_ban_ve;
                withBlock.ValueMember = "So_Xe";
                withBlock.DisplayMember = "So_Xe";
            }
        }

        public void Chon_thong_tin_xe_ban_ve()
        {
            if (cbo_GioVe.Text == "")
                cbo_XeVe.Text = "";
            if (cbo_TenTuyenVe.Text != "" & cbo_XeVe.Text != "" & cbo_GioVe.Text != "" & cbo_NgayVe.Text != "")
            {
                if (cbo_XeVe.SelectedIndex < 0)
                    return;
                Loc_thong_tin_theo_so_xe_ban_ve(cbo_XeVe.SelectedValue.ToString());
            }
        }

        private void Loc_thong_tin_theo_so_xe_ban_ve(string So_Xe)
        {
            lenh_ban_ve = "Select * From Xe where So_Xe = '" + So_Xe + "'";
            bang_Thong_tin_xe_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
            luoi_XeVe.DataSource = bang_Thong_tin_xe_ban_ve;
        }

        #region "Xử lý nút chọn chỗ ngồi"
        public void Chon_cho_ngoi_ban_ve()
        {
            {
                if (Kiem_tra_thong_tin_dat_ve())
                {
                    lenh_ban_ve = "Select So_Cho_Ngoi From Xe where So_Xe = '" + cbo_XeVe.SelectedValue.ToString() + "'";
                    bang_Thong_tin_xe_ban_ve = Ket_noi.Doc_bang(lenh_ban_ve);
                    So_cho_ngoi_ban_ve = bang_Thong_tin_xe_ban_ve.Rows[0]["So_Cho_Ngoi"].ToString();

                    if (Convert.ToInt32(So_cho_ngoi_ban_ve) == 7)
                    {
                        Form_Xe_7_Cho frm_xe_7 = new Form_Xe_7_Cho(this);
                        frm_xe_7.Show();
                    }

                    if (Convert.ToInt32(So_cho_ngoi_ban_ve) == 16)
                    {
                        Form_Xe_16_Cho frm_xe_16 = new Form_Xe_16_Cho(this);
                        frm_xe_16.Show();
                    }

                    if (Convert.ToInt32(So_cho_ngoi_ban_ve) == 25)
                    {
                        Form_Xe_25_Cho frm_xe_25 = new Form_Xe_25_Cho(this);
                        frm_xe_25.Show();
                    }

                    if (Convert.ToInt32(So_cho_ngoi_ban_ve) == 30)
                    {
                        Form_Xe_30_Cho frm_xe_30 = new Form_Xe_30_Cho(this);
                        frm_xe_30.Show();
                    }

                    if (Convert.ToInt32(So_cho_ngoi_ban_ve) == 45)
                    {
                        Form_Xe_45_Cho frm_xe_45 = new Form_Xe_45_Cho(this);
                        frm_xe_45.Show();
                    }
                }
            }

        }
        #endregion

        private bool Kiem_tra_thong_tin_dat_ve()
        {
            bool check = true;
            {
                if (cbo_TenTuyenVe.Text == "" || cbo_NgayVe.Text == "" || cbo_GioVe.Text == "" || cbo_XeVe.Text == "" || txt_TenHanhKhach.Text == "" || txt_SoDTHanhKhach.Text == "")
                {
                    check = false;
                    MessageBox.Show("Phải nhập đầy đủ thông tin đặt vé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                if (txt_SoDTHanhKhach.Text.Length > 12 || txt_SoDTHanhKhach.Text.Length < 9)
                {
                    check = false;
                    MessageBox.Show("So điện thoại từ 9 đến 12 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            return check;
        }

        private void cbo_TenTuyenVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_tuyen_ban_ve();
        }

        private void cbo_NgayVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_ngay_ban_ve();
        }

        private void cbo_GioVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_xe_ban_ve();
        }

        private void cbo_XeVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chon_thong_tin_xe_ban_ve();
        }

        private void btn_ChonChoNgoi_Click(object sender, EventArgs e)
        {
            Chon_cho_ngoi_ban_ve();
        }

        private void Button_PhanQuyen_Click(object sender, EventArgs e)
        {
            if (cbo_IdLoaiND.Text == "Nhan_Vien")
            {
                Form_Phan_Quyen frm_PhanQuyen = new Form_Phan_Quyen(fl, this);
                frm_PhanQuyen.Show();
            }
            else
                MessageBox.Show("Bạn chỉ được cấp quyền cho nhân viên thôi, vui lòng chọn 1 nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        #endregion

        #region "Xử lý nút information và timer"
        private void ButtonItem14_Click(object sender, EventArgs e)
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
            if (Splitter1.Height == 3)
                Timer2.Stop();
            else
                Splitter1.Height = Splitter1.Height - 20;
        }
        #endregion
    }
}
