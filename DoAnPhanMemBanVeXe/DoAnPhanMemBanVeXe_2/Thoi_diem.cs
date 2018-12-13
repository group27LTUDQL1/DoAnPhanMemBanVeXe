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
    public class Thoi_diem
    {
        Form_Main fm;
        private DataTable bang_thoi_diem;
        private DataTable bang_tuyen_xe;
        private string lenh;

        private bool flag;
        public void Update_thoi_diem()
        {
            Doc_thoi_diem();
            Tao_lien_ket();
            Doc_tuyen();
            Hide();
        }

        private void Hide()
        {
            fm = new Form_Main();
            fm.rad_KhongLap.Hide();
            fm.rad_LapTuan.Hide();
            fm.lbl_Lap.Hide();
            fm.date_NgayKetThuc.Hide();
        }

        private void Show()
        {
            fm = new Form_Main();
            fm.rad_KhongLap.Show();
            fm.rad_LapTuan.Show();
        }

        private void Doc_tuyen()
        {
            fm = new Form_Main();
            lenh = "Select IdTuyen, TenTuyen from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            var _with1 = fm.cbo_GanTuyen;
            _with1.DataSource = bang_tuyen_xe;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            var _with2 = fm;
            _with2.txt_TenTuyen.DataBindings.Clear();
            _with2.txt_TenTuyen.DataBindings.Add("Text", bang_tuyen_xe, "TenTuyen");
        }

        #region "Doc thoi diem voi tao lien ket da xong"
        private void Doc_thoi_diem()
        {
            fm = new Form_Main();
            lenh = "Select * from ThoiDiem";
            bang_thoi_diem = Ket_noi.Doc_bang(lenh);
            fm.luoi_ThoiDiem.DataSource = bang_thoi_diem;
        }

        private void Tao_lien_ket()
        {
            fm = new Form_Main();
            var _with3 = fm.cbo_MaThoiDiem;
            _with3.DataSource = bang_thoi_diem;
            _with3.DisplayMember = "IdThoiDiem";
            _with3.ValueMember = "IdThoiDiem";
            var _with4 = fm;
            _with4.date_Chay.DataBindings.Clear();
            _with4.txt_GioChay.DataBindings.Clear();

            _with4.date_Chay.DataBindings.Add("Text", bang_thoi_diem, "Ngay");
            _with4.txt_GioChay.DataBindings.Add("Text", bang_thoi_diem, "Gio");
        }
        #endregion

        #region "Xu ly ho tro button da xong"
        private void Clear_Control()
        {
            fm = new Form_Main();
            var _with5 = fm;
            _with5.date_Chay.Text = "";
            _with5.date_NgayKetThuc.Text = "";
            _with5.txt_GioChay.Text = "";
            _with5.rad_KhongLap.Checked = true;
            _with5.date_Chay.Focus();
        }

        private void LockButton(bool dt)
        {
            fm = new Form_Main();
            var _with6 = fm;
            _with6.btn_ThemThoiDiem.Enabled = !dt;
            _with6.btn_SuaThoiDiem.Enabled = !dt;
            _with6.btn_XoaThoiDiem.Enabled = !dt;
            _with6.btn_LuuThoiDiem.Enabled = dt;
            _with6.btn_HuyThoiDiem.Enabled = dt;
        }
        #endregion

        public void them()
        {
            fm = new Form_Main();
            flag = true;
            LockButton(true);
            fm.lbl_Lap.Hide();
            fm.date_NgayKetThuc.Hide();
            Show();
            Clear_Control();
            fm.cbo_MaThoiDiem.Enabled = false;
        }

        public void Sua()
        {
            fm = new Form_Main();
            fm.rad_LapTuan.Checked = false;
            fm.rad_KhongLap.Checked = true;
            Show();
            flag = false;
            LockButton(true);
            fm.cbo_MaThoiDiem.Enabled = false;
        }

        public void Luu()
        {
            fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with7 = fm;
            //Neu nhu trang thai dang la them
            if (flag)
            {

                if (TestInfo())
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
                            LockButton(false);
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
                                LockButton(false);
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
                            LockButton(false);
                            Interaction.MsgBox("Đã cập nhật dữ liệu thành công", MsgBoxStyle.Information, "Thông báo");
                        }
                    }
                }
                //trang thai sua
            }
            else
            {
                if (TestInfo())
                {
                    lenh = "Update ThoiDiem set Ngay = '" + Strings.FormatDateTime(Convert.ToDateTime(_with7.date_Chay.Text), DateFormat.ShortDate) + "', Gio = '" + _with7.txt_GioChay.Text + "' where IdThoiDiem = '" + _with7.cbo_MaThoiDiem.Text + "'";
                    SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                    try
                    {
                        Ket_noi.connect.Open();
                        sqlqr.ExecuteNonQuery();
                        Ket_noi.connect.Close();
                        Update_thoi_diem();
                        LockButton(false);
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

        public void Huy()
        {
            fm = new Form_Main();
            LockButton(false);
            Update_thoi_diem();
            fm.cbo_MaThoiDiem.Enabled = true;
            Hide();
        }

        public void Xoa()
        {
            fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma thoi diem: " + fm.cbo_MaThoiDiem.Text + Constants.vbNewLine + " - Ngay: " + fm.date_Chay.Text + Constants.vbNewLine + " - Giờ: " + fm.txt_GioChay.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from ThoiDiem where IdThoiDiem = '" + fm.cbo_MaThoiDiem.Text + "'";
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
            fm = new Form_Main();
            long i = 0;
            i = DateAndTime.DateDiff(DateInterval.Day, Convert.ToDateTime(fm.date_Chay.Text), Convert.ToDateTime(fm.date_NgayKetThuc.Text), FirstDayOfWeek.System, FirstWeekOfYear.System);
            return i;
        }

        private bool TestInfo()
        {
            fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with8 = fm;
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

            if (Convert.ToDateTime(fm.date_Chay.Text) < DateAndTime.Today.Date)
            {
                functionReturnValue = false;
                MessageBox.Show("Ngay ban them khong duoc nho hon ngay hien tai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return functionReturnValue;
            }
            return functionReturnValue;
        }

        public void Gan_tuyen()
        {
            fm = new Form_Main();
            var _with9 = fm;
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
    }
}
