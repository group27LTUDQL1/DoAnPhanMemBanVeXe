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
        public Form_Main fm;
        
        private DataTable bang_tuyen_xe;
        private int vi_tri_hien_hanh;
        private string lenh;

        private bool flag;
        public void UpdateTuyenXe()
        {
            fm = new Form_Main() { Tuyenxe = this };
            Doc_bang_tuyen_xe();
            Tao_lien_ket();
            vi_tri_hien_hanh = 0;
            Xuat_thong_tin_Tuyen_xe();
            fm.luoiTuyenxe.ReadOnly = true;
            Lock_Control(false);
            LockButton(false);
        }

        #region "Doc bang tuyen xe da xong"
        private void Doc_bang_tuyen_xe()
        {
            fm = new Form_Main() { Tuyenxe = this };
            //Lam sach luoi sau moi lan cap nhat
            fm.luoiTuyenxe.ClearSelection();
            lenh = "Select * from TuyenXe";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            fm.luoiTuyenxe.DataSource = bang_tuyen_xe;
        }
        #endregion

        #region "Tao lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Tao_lien_ket()
        {
            fm = new Form_Main() { Tuyenxe = this };
            var _with1 = fm.cboIdTuyen;
            _with1.DataSource = fm.luoiTuyenxe.DataSource;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            _with1.SelectedValue = "IdTuyen";
            Xoa_lien_ket();

            fm.cboIdTuyen.Text = (String)fm.luoiTuyenxe.Rows[0].Cells[0].Value;
            
            fm.cboTenTuyen.DataBindings.Add("text", fm.luoiTuyenxe.DataSource, "TenTuyen");
            fm.cboDiaDiemDi.DataBindings.Add("text", fm.luoiTuyenxe.DataSource, "DiaDiemDi");
            fm.cboDiaDiemDen.DataBindings.Add("text", fm.luoiTuyenxe.DataSource, "DiaDiemDen");
        }
        #endregion

        #region "Xoa lien ket giua cac dieu khien voi datagridview da hoan tat"
        private void Xoa_lien_ket()
        {
            fm = new Form_Main() { Tuyenxe = this };
            fm.cboTenTuyen.DataBindings.Clear();
            fm.cboDiaDiemDi.DataBindings.Clear();
            fm.cboDiaDiemDen.DataBindings.Clear();
        }
        #endregion

        #region "Xu ly cac nut di chuyen va xuat thong tin tuyen xe da hoan tat"
        private void Xuat_thong_tin_Tuyen_xe()
        {
            fm = new Form_Main() { Tuyenxe = this };
            DataRow dong = bang_tuyen_xe.Rows[vi_tri_hien_hanh];
            var _with2 = fm;
            _with2.cboIdTuyen.Text = dong["IdTuyen"].ToString();
            _with2.cboTenTuyen.Text = dong["TenTuyen"].ToString();
            _with2.cboDiaDiemDi.Text = Convert.ToString(dong["DiaDiemDi"]);
            _with2.cboDiaDiemDen.Text = dong["DiaDiemDen"].ToString();
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
            fm = new Form_Main() { Tuyenxe = this };
            flag = true;
            Lock_Control(true);
            LockButton(true);
            Clear_Control();
            fm.luoiTuyenxe.Enabled = false;
            for (int i = 0; i <= bang_tuyen_xe.Rows.Count - 1; i++)
            {
                fm.cboTenTuyen.Items.Add(bang_tuyen_xe.Rows[i]["TenTuyen"].ToString());
            }
        }

        public void Sua()
        {
            fm = new Form_Main() { Tuyenxe = this };
            flag = false;
            Lock_Control(true);
            LockButton(true);
            fm.cboSoXe.Enabled = false;
            fm.Luoixe.ReadOnly = false;
            fm.cboIdTuyen.Enabled = false;
        }

        public void Luu()
        {
            fm = new Form_Main() { Tuyenxe = this };
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
                            if (sqlDR.GetValue(0).ToString() == _with3.cboIdTuyen.Text)
                            {
                                flag = 1;
                                MessageBox.Show("Mã số tuyến " + _with3.cboIdTuyen.Text + " đã tồn tại, vui lòng kiểm tra lại ma so tuyen bạn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        Ket_noi.connect.Close();
                        if (flag == 0)
                        {
                            lenh = "Insert into TuyenXe";
                            lenh += " Values ('" + _with3.cboIdTuyen.Text + "', '" + _with3.cboTenTuyen.Text + "', N'" + _with3.cboDiaDiemDi.Text + "', N'" + _with3.cboDiaDiemDen.Text + "')";
                            SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                            Ket_noi.connect.Open();
                            try
                            {
                                bo_lenh.ExecuteNonQuery();
                                Ket_noi.connect.Close();
                                UpdateTuyenXe();
                                Lock_Control(false);
                                LockButton(false);
                                fm.luoiTuyenxe.Enabled = true;
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
                        lenh = "Update TuyenXe Set TenTuyen = '" + _with3.cboTenTuyen.Text + "', DiaDiemDi = N'" + _with3.cboDiaDiemDi.Text + "', DiaDiemDen = N'" + _with3.cboDiaDiemDen.Text + "' where IdTuyen = '" + _with3.cboIdTuyen.Text + "'";
                        SqlCommand sqlqr = new SqlCommand(lenh, Ket_noi.connect);
                        try
                        {
                            Ket_noi.connect.Open();
                            sqlqr.ExecuteNonQuery();
                            Ket_noi.connect.Close();
                            UpdateTuyenXe();
                            Lock_Control(false);
                            LockButton(false);
                            fm.luoiTuyenxe.Enabled = true;
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
            fm = new Form_Main() { Tuyenxe = this };
            fm.luoiTuyenxe.Enabled = true;
            Xoa_lien_ket();
            Lock_Control(false);
            LockButton(false);
            UpdateTuyenXe();
        }
        #endregion

        #region "Xoa tuyen hoan tat"
        public void Xoa()
        {
            fm = new Form_Main() { Tuyenxe = this };
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma so tuyen: " + fm.cboIdTuyen.Text + Constants.vbNewLine + " - Ten tuyen: " + fm.cboTenTuyen.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh = "Delete from TuyenXe where IdTuyen = '" + fm.cboIdTuyen.SelectedValue.ToString() + "'";
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
            fm = new Form_Main() { Tuyenxe = this };
            var _with4 = fm;
            _with4.cboIdTuyen.Enabled = true;
            _with4.cboTenTuyen.Enabled = f;
            _with4.cboDiaDiemDi.Enabled = f;
            _with4.cboDiaDiemDen.Enabled = f;
        }

        private void Clear_Control()
        {
            fm = new Form_Main() { Tuyenxe = this };
            var _with5 = fm;
            _with5.cboIdTuyen.Text = "";
            _with5.cboTenTuyen.Text = "";
            _with5.cboDiaDiemDi.Text = "";
            _with5.cboDiaDiemDen.Text = "";
            _with5.cboIdTuyen.Focus();
        }

        private void LockButton(bool dt)
        {
            fm = new Form_Main() { Tuyenxe = this };
            var _with6 = fm;
            _with6.btnThemTuyen.Enabled = !dt;
            _with6.btnSuaTuyen.Enabled = !dt;
            _with6.btnXoaTuyen.Enabled = !dt;
            _with6.btnLuuTuyen.Enabled = dt;
            _with6.btnHuyTuyen.Enabled = dt;
        }

        private bool TestInfo()
        {
            fm = new Form_Main() { Tuyenxe = this };
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with7 = fm;
            if (string.IsNullOrEmpty(Strings.Trim(_with7.cboIdTuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cboTenTuyen.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cboDiaDiemDi.Text)) || string.IsNullOrEmpty(Strings.Trim(_with7.cboDiaDiemDen.Text)))
            {
                functionReturnValue = false;
                Interaction.MsgBox("Bạn phải nhập đầy đủ thông tin!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
            }

            if (string.IsNullOrEmpty(Strings.Trim(_with7.cboIdTuyen.Text)))
            {
                _with7.cboIdTuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cboTenTuyen.Text)))
            {
                _with7.cboTenTuyen.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cboDiaDiemDi.Text)))
            {
                _with7.cboDiaDiemDi.Focus();
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(Strings.Trim(_with7.cboDiaDiemDen.Text)))
            {
                _with7.cboDiaDiemDen.Focus();
                return functionReturnValue;
            }
            return functionReturnValue;
        }
        #endregion
    }
}
