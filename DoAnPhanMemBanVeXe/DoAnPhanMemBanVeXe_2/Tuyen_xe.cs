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
    public class Tuyen_xe
    {
        Form_Main fm;
        private DataTable bang_tuyen_xe;
        private int vi_tri_hien_hanh;
        private string lenh;

        private bool flag;
        public void UpdateTuyenXe()
        {
            fm = new Form_Main();
            Doc_bang_tuyen_xe();
            Tao_lien_ket();
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Tuyen_xe();
            fm.luoi_Tuyen_xe.ReadOnly = true;
            Lock_Control(false);
            LockButton(false);
        }

        #region "Doc bang tuyen xe da xong"
        private void Doc_bang_tuyen_xe()
        {
            fm = new Form_Main();
            //Lam sach luoi sau moi lan cap nhat
            fm.luoi_Tuyen_xe.ClearSelection();
            lenh = "Select * from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            fm.luoi_Tuyen_xe.DataSource = bang_tuyen_xe;
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket()
        {
            fm = new Form_Main();
            var _with1 = fm.cbo_IdTuyen;
            _with1.DataSource = fm.luoi_Tuyen_xe.DataSource;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            _with1.SelectedValue = "IdTuyen";
            Xoa_lien_ket();

            fm.cbo_IdTuyen.Text = (String)fm.luoi_Tuyen_xe.Rows[0].Cells[0].Value;
            /*
            fm.cbo_TenTuyen.DataBindings.Add("text", fm.luoi_Tuyen_xe.DataSource, "TenTuyen");
            fm.cbo_DiaDiemDi.DataBindings.Add("text", fm.luoi_Tuyen_xe.DataSource, "DiaDiemDi");
            fm.cbo_DiaDiemDen.DataBindings.Add("text", fm.luoi_Tuyen_xe.DataSource, "DiaDiemDen");*/
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket()
        {
            fm = new Form_Main();
            fm.cbo_TenTuyen.DataBindings.Clear();
            fm.cbo_DiaDiemDi.DataBindings.Clear();
            fm.cbo_DiaDiemDen.DataBindings.Clear();
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin tuyen xe da hoan tat"
        private void Xuat_thong_tin_Tuyen_xe()
        {
            fm = new Form_Main();
            DataRow dong = bang_tuyen_xe.Rows[vi_tri_hien_hanh];
            var _with2 = fm;
            _with2.cbo_IdTuyen.Text = dong["IdTuyen"].ToString();
            _with2.cbo_TenTuyen.Text = dong["TenTuyen"].ToString();
            _with2.cbo_DiaDiemDi.Text = Convert.ToString(dong["DiaDiemDi"]);
            _with2.cbo_DiaDiemDen.Text = dong["DiaDiemDen"].ToString();
        }

        public void Di_chuyen_ve_sau()
        {
            if (vi_tri_hien_hanh < bang_tuyen_xe.Rows.Count - 1)
            {
                vi_tri_hien_hanh += 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_truoc()
        {
            if (vi_tri_hien_hanh > 0)
            {
                vi_tri_hien_hanh -= 1;
                Xuat_thong_tin_Tuyen_xe();
            }
        }

        public void Di_chuyen_ve_dau()
        {
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Tuyen_xe();
        }

        public void Di_chuyen_ve_cuoi()
        {
            vi_tri_hien_hanh = bang_tuyen_xe.Rows.Count - 1;
            Xuat_thong_tin_Tuyen_xe();
        }
        #endregion

        #region "Them, sua tuyen da hoan tat"
        public void Them()
        {
            fm = new Form_Main();
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Clear_Control();
            fm.luoi_Tuyen_xe.Enabled = false;
            for (int i = 0; i <= bang_tuyen_xe.Rows.Count - 1; i++)
            {
                fm.cbo_TenTuyen.Items.Add(bang_tuyen_xe.Rows[i]["TenTuyen"].ToString());
            }
        }

        public void Sua()
        {
            fm = new Form_Main();
            flag = false;
            Lock_Control(true);
            LockButton(true);
            //Form_Main.cbo_SoXe.Enabled = False
            fm.Luoi_Xe.ReadOnly = false;
            fm.cbo_IdTuyen.Enabled = false;
        }

        public void Luu()
        {
            fm = new Form_Main();
            Ket_noi.Tao_ket_noi();
            if (Ket_noi.connect.State == ConnectionState.Open)
            {
                Ket_noi.connect.Close();
            }
            var _with3 = fm;
            //Them nguoi dung moi
            if (flag == true)
            {
                if (TestInfo())
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
                                Lock_Control(false);
                                LockButton(false);
                                fm.luoi_Tuyen_xe.Enabled = true;
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
                            Lock_Control(false);
                            LockButton(false);
                            fm.luoi_Tuyen_xe.Enabled = true;
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
            fm.luoi_Tuyen_xe.Enabled = true;
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            UpdateTuyenXe();
        }
        #endregion

        #region "Xoa tuyen hoan tat"
        public void Xoa()
        {
            fm = new Form_Main();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma so tuyen: " + fm.cbo_IdTuyen.Text + Constants.vbNewLine + " - Ten tuyen: " + fm.cbo_TenTuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from TuyenXe where IdTuyen = '" + fm.cbo_IdTuyen.SelectedValue.ToString() + "'";
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
        private void Lock_Control(bool f)
        {
            fm = new Form_Main();
            var _with4 = fm;
            _with4.cbo_IdTuyen.Enabled = true;
            _with4.cbo_TenTuyen.Enabled = f;
            _with4.cbo_DiaDiemDi.Enabled = f;
            _with4.cbo_DiaDiemDen.Enabled = f;
        }

        private void Clear_Control()
        {
            fm = new Form_Main();
            var _with5 = fm;
            _with5.cbo_IdTuyen.Text = "";
            _with5.cbo_TenTuyen.Text = "";
            _with5.cbo_DiaDiemDi.Text = "";
            _with5.cbo_DiaDiemDen.Text = "";
            _with5.cbo_IdTuyen.Focus();
        }

        private void LockButton(bool dt)
        {
            fm = new Form_Main();
            var _with6 = fm;
            _with6.btn_ThemTuyen.Enabled = !dt;
            _with6.btn_SuaTuyen.Enabled = !dt;
            _with6.btn_XoaTuyen.Enabled = !dt;
            _with6.btn_LuuTuyen.Enabled = dt;
            _with6.btn_HuyTuyen.Enabled = dt;
        }

        private bool TestInfo()
        {
            fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with7 = fm;
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
    }
}
