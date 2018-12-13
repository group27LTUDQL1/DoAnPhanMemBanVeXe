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
    public class Xe
    {
        Form_Main fm;
        private bool flag;
        private string lenh;
        private DataTable bang_xe;

        private int vi_tri_hien_hanh;
        public void UpdateXe()
        {
            fm = new Form_Main();
            Doc_bang_Xe();
            Tao_lien_ket();
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Xe();
            fm.Luoi_Xe.ReadOnly = true;
            Lock_Control(false);
            LockButton(false);

            var _with1 = fm.cbo_HieuXe.Items;
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
            fm = new Form_Main();
            //Lam sach luoi sau moi lan cap nhat
            fm.Luoi_Xe.ClearSelection();
            lenh = "Select * from Xe";
            bang_xe = Ket_noi.Doc_bang(lenh);
            fm.Luoi_Xe.DataSource = bang_xe;
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin xe da hoan tat"
        private void Xuat_thong_tin_Xe()
        {
            fm = new Form_Main();
            DataRow dong = bang_xe.Rows[vi_tri_hien_hanh];
            var _with2 = fm;
            _with2.cbo_SoXe.Text = dong["So_Xe"].ToString();
            _with2.cbo_HieuXe.Text = dong["Hieu_Xe"].ToString();
            _with2.txt_DoiXe.Text = Convert.ToString(dong["Doi_Xe"]);
            _with2.cbo_SoChoNgoi.Text = dong["So_Cho_Ngoi"].ToString();
        }

        public void Di_chuyen_ve_sau()
        {
            if (vi_tri_hien_hanh < bang_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_truoc()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Xe();
            }
        }

        public void Di_chuyen_ve_dau()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Xe();
        }

        public void Di_chuyen_ve_cuoi()
        {
            vi_tri_hien_hanh = bang_xe.Rows.Count - 1;
            Xuat_thong_tin_Xe();
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket()
        {
            fm = new Form_Main();
            if (string.IsNullOrEmpty(fm.cbo_SoChoNgoi.Text))
            {
                var _with3 = fm.cbo_SoChoNgoi;
                _with3.Items.Add(7);
                _with3.Items.Add(16);
                _with3.Items.Add(25);
                _with3.Items.Add(30);
                _with3.Items.Add(45);
            }
            var _with4 = fm.cbo_SoXe;
            _with4.DataSource = fm.Luoi_Xe.DataSource;
            _with4.DisplayMember = "So_Xe";
            _with4.ValueMember = "So_Xe";
            _with4.SelectedValue = "So_Xe";
            Xoa_lien_ket();
            //Form_Main.cbo_IdXe.DataBindings.Add("text", Form_Main.Luoi_Xe.DataSource, "Id_Xe")
            //Do Id xe la value member nen ta se khoi tao no luc moi load form va
            //------------Cach1 nhung khong hay, ta nen huy vung nho cua bang sau moi lan xai xong
            //Form_Main.cbo_IdXe.Text = bang_xe.Rows(0)("Id_Xe").ToString 
            //-----------------Cach 2--------------------------
            fm.cbo_SoXe.Text = (String)fm.Luoi_Xe.Rows[0].Cells[0].Value;
            /*
            fm.cbo_HieuXe.DataBindings.Add("text", fm.Luoi_Xe.DataSource, "Hieu_Xe");
            fm.txt_DoiXe.DataBindings.Add("text", fm.Luoi_Xe.DataSource, "Doi_Xe");
            fm.cbo_SoChoNgoi.DataBindings.Add("text", fm.Luoi_Xe.DataSource, "So_Cho_Ngoi");*/
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket()
        {
            fm = new Form_Main();
            fm.cbo_SoXe.DataBindings.Clear();
            fm.cbo_HieuXe.DataBindings.Clear();
            fm.txt_DoiXe.DataBindings.Clear();
            fm.cbo_SoChoNgoi.DataBindings.Clear();
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
            fm = new Form_Main();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            //Form_Main.cbo_SoXe.Enabled = False
            fm.Luoi_Xe.ReadOnly = false;
            fm.cbo_SoXe.Enabled = false;
        }

        public void Luu_thay_doi()
        {
            fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with5 = fm;
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
                                fm.cbo_HieuXe.Items.Clear();
                                fm.Luoi_Xe.Enabled = true;
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
                        Huy_thao_tac();
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
                            fm.Luoi_Xe.Enabled = true;
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
                        Huy_thao_tac();
                    }
                }
            }
        }
        #endregion

        #region "Xu ly huy thao tac cap nhat da hoan tat"
        public void Huy_thao_tac()
        {
            fm = new Form_Main();
            fm.Luoi_Xe.Enabled = true;
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            UpdateXe();
        }
        #endregion

        #region "Xoa xe hoan tat"
        public void Xoa_Xe()
        {
            fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - So xe: " + fm.cbo_SoXe.Text + Constants.vbNewLine + " - Hieu xe: " + fm.cbo_HieuXe.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from Xe where So_Xe = '" + fm.cbo_SoXe.SelectedValue.ToString() + "'";
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
        private void Lock_Control(bool f)
        {
            fm = new Form_Main();
            var _with6 = fm;
            _with6.cbo_SoXe.Enabled = true;
            _with6.cbo_HieuXe.Enabled = f;
            _with6.txt_DoiXe.Enabled = f;
            _with6.cbo_SoChoNgoi.Enabled = f;
        }

        private void Clear_Control()
        {
            fm = new Form_Main();
            var _with7 = fm;
            _with7.cbo_SoXe.Text = "";
            _with7.cbo_HieuXe.Text = "";
            _with7.txt_DoiXe.Text = "";
            _with7.cbo_SoXe.Focus();
        }

        private void LockButton(bool dt)
        {
            fm = new Form_Main();
            var _with8 = fm;
            _with8.btn_ThemXe.Enabled = !dt;
            _with8.btn_SuaXe.Enabled = !dt;
            _with8.btn_XoaXe.Enabled = !dt;
            _with8.btn_LuuXe.Enabled = dt;
            _with8.btn_HuyXe.Enabled = dt;
        }

        private bool TestInfo()
        {
            fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with9 = fm;
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
    }
}
