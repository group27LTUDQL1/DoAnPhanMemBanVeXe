using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace DoAnPhanMemBanVeXe_2
{
    public class Nguoi_dung
    {
        public  Form_Main FM;
        public  Form_Login FL;
        private bool flag;
        private DataTable bang_Nguoi_Dung;
        private int vi_tri_hien_hanh;

        public void UpdateNguoiDung()
        {
           
            var fl = FL;
            var fm = FM;
            fm = new Form_Main();
            fl = new Form_Login();
            if (fl.LoginLoaiND == "Quan_Ly" || fl.LoginLoaiND == "Admin")
            {                
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket();
                fm.luoi_NguoiDung.ReadOnly = true;
                Lock_Control(false);
                LockButton(false);
            }
            else
            {                
                Doc_bang_Nguoi_Dung();
                vi_tri_hien_hanh = 0;
                Xuat_thong_tin_Nguoi_Dung();
                Tao_lien_ket();
                fm.luoi_NguoiDung.ReadOnly = true;
                fm.Button_Them.Enabled = false;
                fm.Button_Sua.Enabled = false;
                fm.Button_Xoa.Enabled = false;
                fm.Button_CapPass.Enabled = false;
                fm.Button_PhanQuyen.Text = "Xem Quyền";
            }
        }

        #region "Xu ly doc bang nguoi dung va phan loai nguoi dung de hien thi da hoan tat"
        public void Doc_bang_Nguoi_Dung()
        {
            var fl = FL;
            var fm = FM;
            fm = new Form_Main();
            fl = new Form_Login();
            //Lam sach luoi sau moi lan cap nhat
            fm.luoi_NguoiDung.ClearSelection();
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
            fm.luoi_NguoiDung.DataSource = bang_Nguoi_Dung;
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin nguoi dung da hoan tat"
        private void Xuat_thong_tin_Nguoi_Dung()
        {
            //
            DataRow dong = bang_Nguoi_Dung.Rows[vi_tri_hien_hanh];
            var _with1 = FM;
            _with1 = new Form_Main();
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

            
            var fm = FM;
            var fl = FL;
            fm = new Form_Main();
            fl = new Form_Login();
            SqlCommand query = new SqlCommand("select IdLoaiND from LoaiNguoiDung", Ket_noi.connect);
            Ket_noi.connect.Open();
            SqlDataReader dr = query.ExecuteReader();
            fm.cbo_IdLoaiND.Items.Clear();
            while (dr.Read() == true)
                {
                    if (fl.LoginLoaiND == "Admin")
                {
                    fm.cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                }
                else if (fl.LoginLoaiND == "Quan_Ly")
                {
                    if (dr.GetValue(0).ToString() != "Admin")
                    {
                        fm.cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                    }
                }
                else
                {
                    if (dr.GetValue(0).ToString() != "Admin" && dr.GetValue(0).ToString() != "Quan_Ly")
                    {
                        fm.cbo_IdLoaiND.Items.Add(dr.GetValue(0).ToString());
                    }
                }
            }
            Ket_noi.connect.Close();
            var _with2 = fm.cbo_Username;
            _with2.DataSource = fm.luoi_NguoiDung.DataSource;
            _with2.DisplayMember = "IdNguoiDung";
            _with2.ValueMember = "IdNguoiDung";
            _with2.SelectedValue = "IdNguoiDung";
            Xoa_lien_ket();           
            //Tao gia tri mac dinh la IdNguoiDung dong thu 0 cot 0 luc khoi dong vi IdNguoiDung la member ko lien ket duoc
            /*fm.cbo_Username.Text = (String)fm.luoi_NguoiDung.Rows[0].Cells[0].Value;
            fm.txt_Password.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "PassND");
            fm.txt_DiaChi.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "DiaChi");
            fm.txt_HoTen.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "HoTen");
            fm.txt_SoDienThoai.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "SoDT");
            fm.date_NgaySinh.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "NgaySinh");
            fm.cbo_IdLoaiND.DataBindings.Add("text", fm.luoi_NguoiDung.DataSource, "IdLoaiND");*/
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket()
        {
            var fm = FM;
            fm = new Form_Main();
            fm.txt_Password.DataBindings.Clear();
            fm.txt_DiaChi.DataBindings.Clear();
            fm.txt_HoTen.DataBindings.Clear();
            fm.txt_SoDienThoai.DataBindings.Clear();
            fm.cbo_IdLoaiND.DataBindings.Clear();
            fm.date_NgaySinh.DataBindings.Clear();
        }
        #endregion

        #region "Them va sua thong tin nguoi dung da ly ly xong"
        public void Them_nguoi_dung()
        {
            var fm = FM;
            fm = new Form_Main();
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Clear_Control();
            fm.luoi_NguoiDung.ReadOnly = false;
        }

        public void Sua_thong_tin_ca_nhan()
        {
            var fm = FM;
            var fl = FL;
            fm = new Form_Main();
            fl = new Form_Login();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            fm.cbo_Username.Focus();
            fm.cbo_Username.Text = fl.LoginTenND;
            fm.cbo_Username.Enabled = false;
            fm.luoi_NguoiDung.ReadOnly = false;
            fm.cbo_IdLoaiND.Enabled = false;
        }

        public void Luu_thay_doi()
        {
            var fm = FM;
            var fl = FL;
            fm = new Form_Main();
            fl = new Form_Login();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with3 = FM;
            _with3 = new Form_Main();
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
                    if (fm.cbo_Username.Text != fl.LoginTenND)
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
                            fm.luoi_NguoiDung.Enabled = true;
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
            var fm = FM;
            var fl = FL;
            fm = new Form_Main();
            fl = new Form_Login();
            if (Strings.Trim(fm.cbo_Username.Text) == fl.LoginTenND)
            {
                DialogResult dg = MessageBox.Show("Ban không được quyền xóa thông tin của chính bạn được. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - User name người dùng: " + fm.cbo_Username.Text + Constants.vbNewLine + " - Tên: " + fm.txt_HoTen.Text + Constants.vbNewLine + " - Số điện thoại: " + fm.txt_SoDienThoai.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (qs == DialogResult.Yes)
                {
                    string lenh = "Delete from NguoiDung where IdNguoiDung = '" + fm.cbo_Username.SelectedValue.ToString() + "'";
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
            var fm = FM;
            fm = new Form_Main();
            var _with4 = FM;
            _with4 = new Form_Main();
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
            var fm = FM;
            fm = new Form_Main();
            var _with5 = FM;
            _with5 = new Form_Main();
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
            var fm = FM;
            fm = new Form_Main();
            var _with6 = FM;
            _with6 = new Form_Main();
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
            var fm = FM;
            fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with7 = FM;
            _with7 = new Form_Main();
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
    }
}
