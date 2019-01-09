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
using Microsoft.VisualBasic;

namespace DoAnPhanMemBanVeXe
{
    public partial class Form_ChiTietTuyen : DevComponents.DotNetBar.Office2007Form
    {
        public Form_ChiTietTuyen()
        {
            InitializeComponent();
        }

        private DataTable bang_thoi_diem;
        private DataTable bang_Tuyen_xe;
        private string lenh;
        private int vi_tri_hien_hanh;
        private bool flag = true;
        private DataTable bang_danh_sach;

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (cbo_MaSoTuyen.Text == "" || cbo_MaThoiDiem.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Mã số tuyến và thời điểm cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult qs = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả thông tin về:\n - Mã số tuyến: " + cbo_MaSoTuyen.Text + "\n - Mã thời điểm: " + cbo_MaThoiDiem.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (qs == DialogResult.Yes)
            {
                string lenh1 = "Select IdTuyen, NgayDi, Gio from ChuyenXe";
                SqlDataReader dr;
                SqlCommand com = new SqlCommand(lenh1, Ket_noi.connect);
                Ket_noi.connect.Close();
                Ket_noi.connect.Open();
                dr = com.ExecuteReader();
                while (dr.Read() == true)
                {
                    if (((dr.GetValue(0).ToString() == cbo_MaSoTuyen.Text) && ((Strings.FormatDateTime(DateTime.Parse(dr.GetValue(1).ToString()), DateFormat.ShortDate) == date_Chay.Text) && (dr.GetValue(2).ToString() == txt_GioChay.Text))))
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
                    MessageBox.Show("Dữ liệu đã xóa thành công", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa dữ liệu không thành công!", "Thông báo");
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác xóa!", "Thông báo");
        }

        private void Form_ChiTietTuyen_Load(object sender, EventArgs e)
        {
            Update_ChiTietTuyen();
        }

        public void Update_ChiTietTuyen()
        {
            Doc_Thoi_diem();
            Doc_tuyen_xe();
            vi_tri_hien_hanh = 0;
        }

        private void cbo_MaSoTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_MaSoTuyen.SelectedIndex < 0)
                return;
            Loc_Thoi_diem_theo_IdTuyen(cbo_MaSoTuyen.SelectedValue.ToString());
        }

        #region "Đọc thời điểm, đọc tuyến xe đã xong"
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

            cbo_MaSoTuyen.DataSource = bang_Tuyen_xe;
            cbo_MaSoTuyen.DisplayMember = "IdTuyen";
            cbo_MaSoTuyen.ValueMember = "IdTuyen";

            cbo_MaSoTuyen1.Text = cbo_MaSoTuyen.Text;

            Xoa_lien_ket();
            Tao_lien_ket();
        }
        #endregion

        #region "Tạo liên kết với xóa liên kết đã xong"
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

        #region "Lọc danh sách thời điểm đã xong"
        public void Loc_Thoi_diem_theo_IdTuyen(string pMa_so_tuyen)
        {
            string dieu_kien = "IdTuyen = '" + pMa_so_tuyen + "'";
            bang_thoi_diem.DefaultView.RowFilter = dieu_kien;
        }

        #endregion

        #region "Xử lý button hiện, lọc danh sách đã xong"
        private void btn_HienDanhSach_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                luoi_Thoi_diem.ClearSelection();
                lenh = "Select ChiTietTuyen.IdTuyen, TenTuyen , ThoiDiem.IdThoiDiem , Ngay, Gio from ThoiDiem, ChiTietTuyen, TuyenXe ";
                lenh += " where ChiTietTuyen.IdThoiDiem = ThoiDiem.IdThoiDiem and TuyenXe.IdTuyen = ChiTietTuyen.IdTuyen";
                bang_danh_sach = Ket_noi.Doc_bang(lenh);
                luoi_Thoi_diem.DataSource = bang_danh_sach;

                cbo_MaSoTuyen.DataSource = bang_danh_sach;
                cbo_MaSoTuyen.DisplayMember = "IdTuyen";
                cbo_MaSoTuyen.ValueMember = "IdTuyen";

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
    }
}
