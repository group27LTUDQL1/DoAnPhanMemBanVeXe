/*using Microsoft.VisualBasic;
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
    public class Ban_ve
    {
        Form_Main fm;
        private DataTable bang_tuyen_xe;
        private DataTable bang_Thoi_diem_ngay;
        private DataTable bang_Thoi_diem_gio;
        private DataTable bang_Xe;

        private DataTable bang_Thong_tin_xe;
        private DataTable bang_dat_ve;
        public string IdChuyen;

        public string So_cho_ngoi;
        private string lenh;

        public void Update_Ve_xe()
        {
            Doc_tuyen_xe();
            doc_bang_ve();
            Clear_Controls();
        }

        private void doc_bang_ve()
        {
            fm= new Form_Main();
            lenh = "Select IdVe, TenHanhKhach, SDTHanhKhach, TenTuyen, NgayDi, Gio, So_Xe from BanVe, ChuyenXe, TuyenXe ";
            lenh += " where BanVe.IdChuyen = ChuyenXe.IdChuyen and ChuyenXe.IdTuyen = TuyenXe.IdTuyen";
            bang_dat_ve = Ket_noi.Doc_bang(lenh);
            var _with1 = fm.cbo_MaSoVe;
            _with1.DataSource = bang_dat_ve;
            _with1.ValueMember = "IdVe";
            _with1.DisplayMember = "IdVe";

            //Tao lien ket
            fm.luoi_ThongTinDatVe.DataSource = bang_dat_ve;

        }




        private void Clear_Controls()
        {
            fm = new Form_Main();
            var _with2 = fm;
            _with2.cbo_TenTuyenVe.Text = "";
            _with2.cbo_NgayVe.Text = "";
            _with2.cbo_GioVe.Text = "";
            _with2.cbo_XeVe.Text = "";
        }

        private void Doc_tuyen_xe()
        {
            fm = new Form_Main();
            lenh = "Select Distinct ChuyenXe.IdTuyen, TenTuyen from ChuyenXe, TuyenXe where TuyenXe.IdTuyen = ChuyenXe.IdTuyen";
            bang_tuyen_xe = Ket_noi.Doc_bang(lenh);
            var _with3 = fm.cbo_TenTuyenVe;
            _with3.DataSource = bang_tuyen_xe;
            _with3.DisplayMember = "TenTuyen";
            _with3.ValueMember = "IdTuyen";
        }

        public void Chon_tuyen()
        {
            fm = new Form_Main();
            if (fm.cbo_TenTuyenVe.SelectedIndex < 0)
                return;
            Loc_ngay_theo_tuyen(fm.cbo_TenTuyenVe.SelectedValue.ToString());
        }

        private void Loc_ngay_theo_tuyen(string IdTuyen)
        {
            fm = new Form_Main();
            lenh = "Select Distinct NgayDi from ChuyenXe where IdTuyen = '" + IdTuyen + "'";
            bang_Thoi_diem_ngay = Ket_noi.Doc_bang(lenh);
            var _with4 = fm.cbo_NgayVe;
            _with4.DataSource = bang_Thoi_diem_ngay;
            _with4.ValueMember = "NgayDi";
            _with4.DisplayMember = "NgayDi";
        }

        public void Chon_ngay()
        {
            fm = new Form_Main();
            if (string.IsNullOrEmpty(fm.cbo_GioVe.Text) & string.IsNullOrEmpty(fm.cbo_XeVe.Text))
            {
                fm.cbo_NgayVe.Text = "";
            }
            if (fm.cbo_NgayVe.SelectedIndex < 0)
                return;
            Loc_gio_theo_ngay(fm.cbo_NgayVe.SelectedValue.ToString());
        }

        private void Loc_gio_theo_ngay(string Ngay)
        {
            fm = new Form_Main();
            lenh = "Select Gio from ChuyenXe where NgayDi = '" + Ngay + "' and IdTuyen = '" + fm.cbo_TenTuyenVe.SelectedValue.ToString() + "'";
            bang_Thoi_diem_gio = Ket_noi.Doc_bang(lenh);
            var _with5 = fm.cbo_GioVe;
            _with5.DataSource = bang_Thoi_diem_gio;
            _with5.ValueMember = "Gio";
            _with5.DisplayMember = "Gio";
        }

        public void Chon_xe()
        {
            fm = new Form_Main();
            if (string.IsNullOrEmpty(fm.cbo_XeVe.Text))
            {
                fm.cbo_GioVe.Text = "";
            }
            if (fm.cbo_GioVe.SelectedIndex < 0)
                return;
            Loc_xe_theo_gio(fm.cbo_GioVe.SelectedValue.ToString());
        }

        private void Loc_xe_theo_gio(string Gio)
        {
            fm = new Form_Main();
            lenh = "Select So_Xe from ChuyenXe where Gio = '" + Gio + "' and IdTuyen = '" + fm.cbo_TenTuyenVe.SelectedValue.ToString() + "' and NgayDi = '" + fm.cbo_NgayVe.SelectedValue.ToString() + "'";
            bang_Xe = Ket_noi.Doc_bang(lenh);
            var _with6 = fm.cbo_XeVe;
            _with6.DataSource = bang_Xe;
            _with6.ValueMember = "So_Xe";
            _with6.DisplayMember = "So_Xe";
        }

        public void Chon_thong_tin_xe()
        {
            fm = new Form_Main();
            if (string.IsNullOrEmpty(fm.cbo_GioVe.Text))
            {
                fm.cbo_XeVe.Text = "";
            }
            if (!string.IsNullOrEmpty(fm.cbo_TenTuyenVe.Text) & !string.IsNullOrEmpty(fm.cbo_XeVe.Text) & !string.IsNullOrEmpty(fm.cbo_GioVe.Text) & !string.IsNullOrEmpty(fm.cbo_NgayVe.Text))
            {
                if (fm.cbo_XeVe.SelectedIndex < 0)
                    return;
                Loc_thong_tin_theo_so_xe(fm.cbo_XeVe.SelectedValue.ToString());

            }
        }

        private void Loc_thong_tin_theo_so_xe(string So_Xe)
        {
            fm = new Form_Main();
            lenh = "Select * From Xe where So_Xe = '" + So_Xe + "'";
            bang_Thong_tin_xe = Ket_noi.Doc_bang(lenh);
            fm.luoi_XeVe.DataSource = bang_Thong_tin_xe;
        }

        //Xu ly nut chon cho ngoi
        public void Chon_cho_ngoi()
        {
            fm = new Form_Main();
            var _with7 = fm;
            if (Kiem_tra_thong_tin_dat_ve())
            {
                lenh = "Select So_Cho_Ngoi From Xe where So_Xe = '" + _with7.cbo_XeVe.SelectedValue.ToString() + "'";
                bang_Thong_tin_xe = Ket_noi.Doc_bang(lenh);
                So_cho_ngoi = bang_Thong_tin_xe.Rows[0]["So_Cho_Ngoi"].ToString();

                if (Convert.ToInt32(So_cho_ngoi) == 7)
                {
                    Form_Xe_7_Cho frm_xe_7 = new Form_Xe_7_Cho();
                    frm_xe_7.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 16)
                {
                    Form_Xe_16_Cho frm_xe_16 = new Form_Xe_16_Cho();
                    frm_xe_16.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 25)
                {
                    Form_Xe_25_Cho frm_xe_25 = new Form_Xe_25_Cho();
                    frm_xe_25.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 30)
                {
                    Form_Xe_30_Cho frm_xe_30 = new Form_Xe_30_Cho();
                    frm_xe_30.Show();
                }

                if (Convert.ToInt32(So_cho_ngoi) == 45)
                {
                    Form_Xe_45_Cho frm_xe_45 = new Form_Xe_45_Cho();
                    frm_xe_45.Show();
                }

            }

        }

        private bool Kiem_tra_thong_tin_dat_ve()
        {
            fm = new Form_Main();
            bool functionReturnValue = false;
            functionReturnValue = true;
            var _with8 = fm;
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
    }
}
*/