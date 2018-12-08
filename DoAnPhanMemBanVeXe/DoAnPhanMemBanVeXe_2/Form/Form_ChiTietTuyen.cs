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

namespace DoAnPhanMemBanVeXe_2
{
    public partial class Form_ChiTietTuyen : Form
    {
        private DataTable bang_thoi_diem;
        private DataTable bang_Tuyen_xe;
        private string lenh;
        private int vi_tri_hien_hanh;
        private bool flag = true;
        private DataTable bang_danh_sach;

        public Form_ChiTietTuyen()
        {
            Load += Form_ChiTietTuyen_Load;
            InitializeComponent();
        }

        private void cbo_MaSoTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_MaSoTuyen.SelectedIndex < 0)
                return;
            Loc_Thoi_diem_theo_IdTuyen(cbo_MaSoTuyen.SelectedValue.ToString());
        }
        public void Update_ChiTietTuyen()
        {
            Doc_Thoi_diem();
            Doc_tuyen_xe();
            vi_tri_hien_hanh = 0;
        }

        private void Form_ChiTietTuyen_Load(object sender, EventArgs e)
        {
            Update_ChiTietTuyen();
        }
        #region "Doc thoi diem, doc tuyen xe da xong"
        private void Doc_Thoi_diem()
        {
            luoi_Thoi_diem.ClearSelection();
            lenh = "Select IdTuyen, ThoiDiem.IdThoiDiem , Ngay, Gio from ThoiDiem, ChiTietTuyen where ChiTietTuyen.IdThoiDiem = ThoiDiem.IdThoiDiem ";
            bang_thoi_diem = Ket_noi.Doc_bang(lenh);
            luoi_Thoi_diem.DataSource = bang_thoi_diem;
        }

        private void Doc_tuyen_xe()
        {
            lenh = "Select IdTuyen, TenTuyen from TuyenXe";
            bang_Tuyen_xe = Ket_noi.Doc_bang(lenh);

            var _with1 = cbo_MaSoTuyen;
            _with1.DataSource = bang_Tuyen_xe;
            _with1.DisplayMember = "IdTuyen";
            _with1.ValueMember = "IdTuyen";
            cbo_MaSoTuyen1.Text = cbo_MaSoTuyen.Text;
            Xoa_lien_ket();
            Tao_lien_ket();
        }
        #endregion

        #region "Tao lien ket voi xoa lien ket da xong"
        private void Tao_lien_ket()
        {
            cbo_MaSoTuyen1.DataBindings.Add("Text", bang_thoi_diem, "IdTuyen");
            cbo_TenTuyen.DataBindings.Add("text", bang_Tuyen_xe, "TenTuyen");
            cbo_MaThoiDiem.DataBindings.Add("text", bang_thoi_diem, "IdThoiDiem");
            date_Chay.DataBindings.Add("text", bang_thoi_diem, "Ngay");
            txt_GioChay.DataBindings.Add("text", bang_thoi_diem, "Gio");
        }

        private void Xoa_lien_ket()
        {
            cbo_MaSoTuyen1.DataBindings.Clear();
            cbo_TenTuyen.DataBindings.Clear();
            cbo_MaThoiDiem.DataBindings.Clear();
            date_Chay.DataBindings.Clear();
            txt_GioChay.DataBindings.Clear();
        }
        #endregion

        #region "Loc danh sach thoi diem da xong"
        public void Loc_Thoi_diem_theo_IdTuyen(string pMa_so_tuyen)
        {
            string dieu_kien = "IdTuyen = '" + pMa_so_tuyen + "'";
            bang_thoi_diem.DefaultView.RowFilter = dieu_kien;
        }
        #endregion

        #region "Xy ly button hien, loc danh sach da xong"
        private void btn_HienDanhSach_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                luoi_Thoi_diem.ClearSelection();
                lenh = "Select ChiTietTuyen.IdTuyen, TenTuyen , ThoiDiem.IdThoiDiem , Ngay, Gio from ThoiDiem, ChiTietTuyen, TuyenXe ";
                lenh += " where ChiTietTuyen.IdThoiDiem = ThoiDiem.IdThoiDiem and TuyenXe.IdTuyen = ChiTietTuyen.IdTuyen";
                bang_danh_sach = Ket_noi.Doc_bang(lenh);
                luoi_Thoi_diem.DataSource = bang_danh_sach;
                var _with2 = cbo_MaSoTuyen;
                _with2.DataSource = bang_danh_sach;
                _with2.DisplayMember = "IdTuyen";
                _with2.ValueMember = "IdTuyen";
                cbo_MaSoTuyen1.DataBindings.Clear();
                cbo_TenTuyen.DataBindings.Clear();
                cbo_MaThoiDiem.DataBindings.Clear();
                date_Chay.DataBindings.Clear();
                txt_GioChay.DataBindings.Clear();

                cbo_MaSoTuyen1.DataBindings.Add("Text", bang_danh_sach, "IdTuyen");
                cbo_TenTuyen.DataBindings.Add("text", bang_danh_sach, "TenTuyen");
                cbo_MaThoiDiem.DataBindings.Add("text", bang_danh_sach, "IdThoiDiem");
                date_Chay.DataBindings.Add("text", bang_danh_sach, "Ngay");
                txt_GioChay.DataBindings.Add("text", bang_danh_sach, "Gio");
                btn_HienDanhSach.Text = "Lọc danh sách tuyến";
                flag = false;
            }
            else
            {
                Update_ChiTietTuyen();
                btn_HienDanhSach.Text = "Hiện tất cả danh sách";
                flag = true;
            }
        }
        #endregion
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo_MaSoTuyen.Text) || string.IsNullOrEmpty(cbo_MaThoiDiem.Text))
            {
                MessageBox.Show("Bạn chưa chọn Mã số tuyến và thời điểm cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dt = new DateTime();
            var qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:" + Constants.vbNewLine + " - Ma số tuyến: " + cbo_MaSoTuyen.Text + Constants.vbNewLine + " - Mã thời điểm: " + cbo_MaThoiDiem.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh1 = "Select IdTuyen, NgayDi, Gio from ChuyenXe";
                SqlDataReader dr = null;
                SqlCommand com = new SqlCommand(lenh1, Ket_noi.connect);
                Ket_noi.connect.Open();
                dr = com.ExecuteReader();
                while (dr.Read() == true)
                {
                    //MessageBox.Show(dr.GetValue(0).ToString & dr.GetValue(1).ToString & dr.GetValue(2).ToString)
                    if (dr.GetValue(0).ToString() == cbo_MaSoTuyen.Text & String.Format(dt.ToShortDateString(), Convert.ToDateTime(dr.GetValue(1).ToString())) == date_Chay.Text & dr.GetValue(2).ToString() == txt_GioChay.Text)
                    {
                        MessageBox.Show("Thoi diem nay da duoc gan cho chuyen xe, bạn phai xoa chuyen đó trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Ket_noi.connect.Close();
                        return;
                    }
                }
                Ket_noi.connect.Close();
                lenh = "Delete from ChiTietTuyen where IdThoiDiem = '" + cbo_MaThoiDiem.Text + "'";
                SqlCommand query1 = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    query1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    Update_ChiTietTuyen();
                    Interaction.MsgBox("Dữ liệu đã xóa thành công", Constants.vbInformation, "Thông báo");
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Xóa dữ liệu không thành công!", Constants.vbExclamation, "Thông báo");
                }
            }
            else
            {
                Interaction.MsgBox("Đã hủy thao tác xóa!", Constants.vbExclamation, "Thông báo");
            }
        }

       
    }
}
