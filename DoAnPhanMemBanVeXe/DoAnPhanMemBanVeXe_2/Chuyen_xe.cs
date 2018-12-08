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
    public class Chuyen_xe
    {
        Form_Main fm = new Form_Main();
        private Ban_ve Ban_ve = new Ban_ve();
        private DataTable bang_chuyen_xe;
        private DataTable bang_tuyen_xe;
        private DataTable bang_Chi_tiet_tuyen;
        private DataTable bang_Thoi_diem;

        private DataTable bang_xe;
        private string lenh;
        private bool flag;

        private int vi_tri_hien_hanh = 0;

        public void Update_Chuyen_xe()
        {
            Doc_chuyen_xe();
            Tao_lien_ket();
            Lock_Control(false);
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        private void Doc_chuyen_xe()
        {
            fm.Luoi_Chuyen_xe.ClearSelection();
            lenh = "Select * from ChuyenXe";
            bang_chuyen_xe = Ket_noi.Doc_bang(lenh);
            fm.Luoi_Chuyen_xe.DataSource = bang_chuyen_xe;
        }

        private void Tao_lien_ket()
        {
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                var _with1 = fm.cbo_IdChuyen;
                _with1.DataSource = bang_chuyen_xe;
                _with1.DisplayMember = "IdChuyen";
                _with1.ValueMember = "IdChuyen";
                Xoa_lien_ket();
                fm.cbo_IdChuyen.Text = fm.Luoi_Chuyen_xe[0, 0].Value.ToString();

                var _with2 = fm;
                _with2.cbo_IdTuyenChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "IdTuyen");
                _with2.cbo_SoXeChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "So_Xe");
                _with2.cbo_NgayDiChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "NgayDi");
                _with2.cbo_GioDiChuyen.DataBindings.Add("Text", _with2.Luoi_Chuyen_xe.DataSource, "Gio");
            }
        }

        private void Xoa_lien_ket()
        {
            var _with3 = fm;
            _with3.cbo_IdTuyenChuyen.DataBindings.Clear();
            _with3.cbo_SoXeChuyen.DataBindings.Clear();
            _with3.cbo_NgayDiChuyen.DataBindings.Clear();
            _with3.cbo_GioDiChuyen.DataBindings.Clear();
        }

        private void Lock_Control(bool f)
        {
            var _with4 = fm;
            _with4.cbo_IdChuyen.Enabled = !f;
            _with4.cbo_IdTuyenChuyen.Enabled = f;
            _with4.cbo_SoXeChuyen.Enabled = f;
            _with4.cbo_NgayDiChuyen.Enabled = f;
            _with4.cbo_GioDiChuyen.Enabled = f;
            _with4.Luoi_Chuyen_xe.Enabled = !f;
        }

        private void Clear_Control()
        {
            var _with5 = fm;
            _with5.cbo_IdChuyen.Text = "";
            _with5.cbo_IdTuyenChuyen.Text = "";
            _with5.cbo_NgayDiChuyen.Text = "";
            _with5.cbo_GioDiChuyen.Text = "";
            _with5.txt_SoDienThoai.Text = "";
            _with5.cbo_SoXeChuyen.Text = "";
            _with5.cbo_IdTuyenChuyen.Focus();
        }

        private void LockButton(bool dt)
        {
            var _with6 = fm;
            _with6.btn_ThemChuyen.Enabled = !dt;
            _with6.btn_SuaChuyen.Enabled = !dt;
            _with6.btn_XoaChuyen.Enabled = !dt;
            _with6.btn_LuuChuyen.Enabled = dt;
            _with6.btn_HuyChuyen.Enabled = dt;
        }

        public void Them()
        {
            Xoa_lien_ket();
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Doc_tuyen_xe();
            Doc_xe();
            Clear_Control();
        }

        public void Sua()
        {
            flag = false;
            Lock_Control(true);
            LockButton(true);
            Doc_tuyen_xe();
            Doc_xe();
        }

        public void Huy()
        {
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            Update_Chuyen_xe();

        }

        private void Doc_tuyen_xe()
        {
            lenh = "Select IdTuyen from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            var _with7 = fm.cbo_IdTuyenChuyen;
            _with7.DataSource = bang_tuyen_xe;
            _with7.DisplayMember = "IdTuyen";
            _with7.ValueMember = "IdTuyen";
        }

        public void Chon_tuyen()
        {
            if (fm.cbo_IdTuyenChuyen.SelectedIndex < 0)
                return;
            //Nghia la ko chọn mục nào
            Loc_Thoi_diem_theo_Tuyen(fm.cbo_IdTuyenChuyen.SelectedValue.ToString());
        }

        private void Loc_Thoi_diem_theo_Tuyen(string IdTuyen)
        {
            lenh = "Select Distinct Ngay from ThoiDiem, ChiTietTuyen where IdTuyen = '" + IdTuyen + "' and ThoiDiem.IdThoiDiem = ChiTietTuyen.IdThoiDiem";
            bang_Chi_tiet_tuyen = Ket_noi.Doc_bang(lenh);
            var _with8 = fm.cbo_NgayDiChuyen;
            _with8.DataSource = bang_Chi_tiet_tuyen;
            _with8.ValueMember = "Ngay";
            _with8.DisplayMember = "Ngay";
        }

        public void Chon_ngay()
        {
            if (fm.cbo_NgayDiChuyen.SelectedIndex < 0)
                return;
            //Nghia la ko chọn mục nào
            Loc_gio_theo_ngay(fm.cbo_NgayDiChuyen.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay(string ngay)
        {
            lenh = "Select Gio from ThoiDiem where Ngay = '" + ngay + "'";
            bang_Thoi_diem = Ket_noi.Doc_bang(lenh);
            var _with9 = fm.cbo_GioDiChuyen;
            _with9.DataSource = bang_Thoi_diem;
            _with9.ValueMember = "Gio";
            _with9.DisplayMember = "Gio";
        }

        private void Doc_xe()
        {
            lenh = "Select So_Xe from Xe";
            bang_xe = Ket_noi.Doc_bang(lenh);
            var _with10 = fm.cbo_SoXeChuyen;
            _with10.DataSource = bang_xe;
            _with10.ValueMember = "So_Xe";
            _with10.DisplayMember = "So_Xe";
        }

        public void Luu()
        {
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with11 = fm;
            //Truong hop them chuyen moi
            if (flag == true)
            {
                if (TestInfo())
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
                        lenh = "Insert into ChuyenXe(IdTuyen, NgayDi, Gio, So_Xe)";
                        lenh += " Values ('" + _with11.cbo_IdTuyenChuyen.Text + "', '" + _with11.cbo_NgayDiChuyen.Text + "', '" + _with11.cbo_GioDiChuyen.Text + "', '" + _with11.cbo_SoXeChuyen.Text + "')";
                        SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                        Ket_noi.connect.Open();
                        try
                        {
                            bo_lenh.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Update_Chuyen_xe();
                            Lock_Control(false);
                            LockButton(false);
                            fm.Luoi_Chuyen_xe.Enabled = true;
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
                        Huy();
                    }
                }
                //Truong hop sua thong tin chuyen
            }
            else
            {
                if (TestInfo())
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
                        lenh = "Update ChuyenXe Set IdTuyen = '" + _with11.cbo_IdTuyenChuyen.Text + "', NgayDi = '" + _with11.cbo_NgayDiChuyen.Text + "', Gio = '" + _with11.cbo_GioDiChuyen.Text + "', So_Xe = '" + _with11.cbo_SoXeChuyen.Text + "' where IdChuyen = '" + _with11.cbo_IdTuyenChuyen.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            Update_Chuyen_xe();
                            Lock_Control(false);
                            LockButton(false);
                            fm.Luoi_Chuyen_xe.Enabled = true;
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
                        Huy();
                    }
                }
            }
            Ban_ve.Update_Ve_xe();
        }

        private bool TestInfo()
        {
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with12 = fm;
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

        public void Xoa()
        {
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Chuyến xe: " + fm.cbo_IdChuyen.Text + Constants.vbNewLine + " - Tuyến xe: " + fm.cbo_IdTuyenChuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from ChuyenXe where IdChuyen = '" + fm.cbo_IdChuyen.SelectedValue.ToString() + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
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
            if (bang_chuyen_xe.Rows.Count != 0)
            {
                DataRow dong = bang_chuyen_xe.Rows[vi_tri_hien_hanh];
                var _with13 = fm;
                _with13.cbo_IdChuyen.Text = dong["IdChuyen"].ToString();
                _with13.cbo_IdTuyenChuyen.Text = dong["IdTuyen"].ToString();
                _with13.cbo_NgayDiChuyen.Text = Convert.ToString(dong["NgayDi"]);
                _with13.cbo_GioDiChuyen.Text = dong["Gio"].ToString();
            }

        }

        public void Di_chuyen_ve_sau()
        {
            if (vi_tri_hien_hanh < bang_chuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Chuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Chuyen_xe();
        }

        public void Di_chuyen_ve_cuoi()
        {
            vi_tri_hien_hanh = bang_chuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Chuyen_xe();
        }
        #endregion

    }
}
