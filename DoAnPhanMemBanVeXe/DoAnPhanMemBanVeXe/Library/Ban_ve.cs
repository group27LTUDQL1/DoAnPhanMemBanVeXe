using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAnPhanMemBanVeXe.Library
{
    class Ban_ve
    {
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
            Ket_noi ketnoi = new Ket_noi();
            lenh = "Select IdVe, TenHanhKhach, SDTHanhKhach, TenTuyen, NgayDi, Gio, So_Xe from BanVe, ChuyenXe, TuyenXe ";
            lenh += " where BanVe.IdChuyen = ChuyenXe.IdChuyen and ChuyenXe.IdTuyen = TuyenXe.IdTuyen";
            
            bang_dat_ve = Doc_bang(lenh);
            {
                var withBlock = Form_Main.cbo_MaSoVe;
                withBlock.DataSource = bang_dat_ve;
                withBlock.ValueMember = "IdVe";
                withBlock.DisplayMember = "IdVe";
            }

            // Tao lien ket
            Form_Main.luoi_ThongTinDatVe.DataSource = bang_dat_ve;
        }




        private void Clear_Controls()
        {
            {
                var withBlock = Form_Main;
                withBlock.cbo_TenTuyenVe.Text = "";
                withBlock.cbo_NgayVe.Text = "";
                withBlock.cbo_GioVe.Text = "";
                withBlock.cbo_XeVe.Text = "";
            }
        }

        private void Doc_tuyen_xe()
        {
            lenh = "Select Distinct ChuyenXe.IdTuyen, TenTuyen from ChuyenXe, TuyenXe where TuyenXe.IdTuyen = ChuyenXe.IdTuyen";
            bang_tuyen_xe = Doc_bang(lenh);
            {
                var withBlock = Form_Main.cbo_TenTuyenVe;
                withBlock.DataSource = bang_tuyen_xe;
                withBlock.DisplayMember = "TenTuyen";
                withBlock.ValueMember = "IdTuyen";
            }
        }

        public void Chon_tuyen()
        {
            if (Form_Main.cbo_TenTuyenVe.SelectedIndex < 0)
                return;
            Loc_ngay_theo_tuyen(Form_Main.cbo_TenTuyenVe.SelectedValue.ToString);
        }

        private void Loc_ngay_theo_tuyen(string IdTuyen)
        {
            lenh = "Select Distinct NgayDi from ChuyenXe where IdTuyen = '" + IdTuyen + "'";
            bang_Thoi_diem_ngay = Doc_bang(lenh);
            {
                var withBlock = Form_Main.cbo_NgayVe;
                withBlock.DataSource = bang_Thoi_diem_ngay;
                withBlock.ValueMember = "NgayDi";
                withBlock.DisplayMember = "NgayDi";
            }
        }

        public void Chon_ngay()
        {
            if (Form_Main.cbo_GioVe.Text == "" & Form_Main.cbo_XeVe.Text == "")
                Form_Main.cbo_NgayVe.Text = "";
            if (Form_Main.cbo_NgayVe.SelectedIndex < 0)
                return;
            Loc_gio_theo_ngay(Form_Main.cbo_NgayVe.SelectedValue.ToString);
        }

        private void Loc_gio_theo_ngay(string Ngay)
        {
            lenh = "Select Gio from ChuyenXe where NgayDi = '" + Ngay + "' and IdTuyen = '" + Form_Main.cbo_TenTuyenVe.SelectedValue.ToString + "'";
            bang_Thoi_diem_gio = Doc_bang(lenh);
            {
                var withBlock = Form_Main.cbo_GioVe;
                withBlock.DataSource = bang_Thoi_diem_gio;
                withBlock.ValueMember = "Gio";
                withBlock.DisplayMember = "Gio";
            }
        }

        public void Chon_xe()
        {
            if (Form_Main.cbo_XeVe.Text == "")
                Form_Main.cbo_GioVe.Text = "";
            if (Form_Main.cbo_GioVe.SelectedIndex < 0)
                return;
            Loc_xe_theo_gio(Form_Main.cbo_GioVe.SelectedValue.ToString);
        }

        private void Loc_xe_theo_gio(string Gio)
        {
            lenh = "Select So_Xe from ChuyenXe where Gio = '" + Gio + "' and IdTuyen = '" + Form_Main.cbo_TenTuyenVe.SelectedValue.ToString + "' and NgayDi = '" + Form_Main.cbo_NgayVe.SelectedValue.ToString + "'";
            bang_Xe = Doc_bang(lenh);
            {
                var withBlock = Form_Main.cbo_XeVe;
                withBlock.DataSource = bang_Xe;
                withBlock.ValueMember = "So_Xe";
                withBlock.DisplayMember = "So_Xe";
            }
        }

        public void Chon_thong_tin_xe()
        {
            if (Form_Main.cbo_GioVe.Text == "")
                Form_Main.cbo_XeVe.Text = "";
            if (Form_Main.cbo_TenTuyenVe.Text != "" & Form_Main.cbo_XeVe.Text != "" & Form_Main.cbo_GioVe.Text != "" & Form_Main.cbo_NgayVe.Text != "")
            {
                if (Form_Main.cbo_XeVe.SelectedIndex < 0)
                    return;
                Loc_thong_tin_theo_so_xe(Form_Main.cbo_XeVe.SelectedValue.ToString);
            }
        }

        private void Loc_thong_tin_theo_so_xe(string So_Xe)
        {
            lenh = "Select * From Xe where So_Xe = '" + So_Xe + "'";
            bang_Thong_tin_xe = Doc_bang(lenh);
            Form_Main.luoi_XeVe.DataSource = bang_Thong_tin_xe;
        }

        // Xu ly nut chon cho ngoi
        public void Chon_cho_ngoi()
        {
            {
                var withBlock = Form_Main;
                if (Kiem_tra_thong_tin_dat_ve())
                {
                    lenh = "Select So_Cho_Ngoi From Xe where So_Xe = '" + withBlock.cbo_XeVe.SelectedValue.ToString + "'";
                    bang_Thong_tin_xe = Doc_bang(lenh);
                    So_cho_ngoi = bang_Thong_tin_xe.Rows(0)("So_Cho_Ngoi").ToString;

                    if (System.Convert.ToInt32(So_cho_ngoi) == 7)
                    {
                        Form_Xe_7_Cho frm_xe_7 = new Form_Xe_7_Cho();
                        frm_xe_7.Show();
                    }

                    if (System.Convert.ToInt32(So_cho_ngoi) == 16)
                    {
                        Form_Xe_16_Cho frm_xe_16 = new Form_Xe_16_Cho();
                        frm_xe_16.Show();
                    }

                    if (System.Convert.ToInt32(So_cho_ngoi) == 25)
                    {
                        Form_Xe_25_Cho frm_xe_25 = new Form_Xe_25_Cho();
                        frm_xe_25.Show();
                    }

                    if (System.Convert.ToInt32(So_cho_ngoi) == 30)
                    {
                        Form_Xe_30_Cho frm_xe_30 = new Form_Xe_30_Cho();
                        frm_xe_30.Show();
                    }

                    if (System.Convert.ToInt32(So_cho_ngoi) == 45)
                    {
                        Form_Xe_45_Cho frm_xe_45 = new Form_Xe_45_Cho();
                        frm_xe_45.Show();
                    }
                }
            }
        }

        private bool Kiem_tra_thong_tin_dat_ve()
        {
            Kiem_tra_thong_tin_dat_ve = true;
            {
                var withBlock = Form_Main;
                if (withBlock.cbo_TenTuyenVe.Text == "" || withBlock.cbo_NgayVe.Text == "" || withBlock.cbo_GioVe.Text == "" || withBlock.cbo_XeVe.Text == "" || withBlock.txt_TenHanhKhach.Text == "" || withBlock.txt_SoDTHanhKhach.Text == "")
                {
                    Kiem_tra_thong_tin_dat_ve = false;
                    MessageBox.Show("Phải nhập đầy đủ thông tin đặt vé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (withBlock.txt_SoDTHanhKhach.Text.Length > 12 || withBlock.txt_SoDTHanhKhach.Text.Length < 9)
                {
                    Kiem_tra_thong_tin_dat_ve = false;
                    MessageBox.Show("So điện thoại từ 9 đến 12 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
        }
    }
}
