﻿using System;
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
    public partial class Form_Xe_45_Cho : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public Form_Xe_45_Cho(Form_Main fm1)
        {
            InitializeComponent();
            fm = fm1;
        }

        private string lenh;
        private string lenh1;
        private string IdChuyen;
        private DataTable bang_dat_ve;

        Form_Main fm;

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            fm.Update_Ve_xe_ban_ve();
            this.Close();
        }

        private void Form_Xe_45_Cho_Load(object sender, EventArgs e)
        {
            Duyet_danh_sach_cho_ngoi();
        }

        private void Duyet_danh_sach_cho_ngoi()
        {
            {
                var withBlock = fm;
                lenh = "Select IdChuyen from ChuyenXe where IdTuyen = '" + withBlock.cbo_TenTuyenVe.SelectedValue.ToString() + "'";
                lenh += " and  NgayDi =  '" + Strings.FormatDateTime(Convert.ToDateTime(withBlock.cbo_NgayVe.SelectedValue.ToString()), DateFormat.ShortDate) + "' and Gio = '" + withBlock.cbo_GioVe.SelectedValue.ToString() + "'";
                lenh += " and So_Xe = '" + withBlock.cbo_XeVe.SelectedValue.ToString() + "'";
                // Lay Idchuyen cua chuyen do ra
                bang_dat_ve = Ket_noi.Doc_bang(lenh);
                IdChuyen = bang_dat_ve.Rows[0]["IdChuyen"].ToString();
            }

            lenh = "Select * from ChoNgoi where IdChuyen = '" + IdChuyen + "' and So_Xe = '" + fm.cbo_XeVe.SelectedValue.ToString() + "'";
            SqlCommand com = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read() == true)
                {
                    for (int i = 0; i <= grb_45.Controls.Count - 1; i++)
                    {
                        if (dr.GetValue(2).ToString() == grb_45.Controls[i].Text)
                            ((DevComponents.DotNetBar.ButtonX)grb_45.Controls[i]).Image = Properties.Resources.hanh_khach;
                    }
                }
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được danh sách chỗ ngồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Duyet(DevComponents.DotNetBar.ButtonX but)
        {
            DialogResult dg = MessageBox.Show("Ban có chắn chắc muốn đặt:\n- Xe: " + fm.cbo_XeVe.SelectedValue.ToString() + "\n- Vị trí chỗ ngồi: " + but.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                lenh = "Insert into ChoNgoi Values('" + IdChuyen + "', '" + fm.cbo_XeVe.Text + "', '" + but.Text + "')";
                lenh1 = "Insert into BanVe(IdChuyen, TenHanhKhach, SDTHanhKhach) ";
                lenh1 += "Values('" + IdChuyen + "', N'" + fm.txt_TenHanhKhach.Text + "', '" + fm.txt_SoDTHanhKhach.Text + "')";
                SqlCommand com = new SqlCommand(lenh, Ket_noi.connect);
                SqlCommand com1 = new SqlCommand(lenh1, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    com.ExecuteNonQuery();
                    com1.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    MessageBox.Show("Đặt chỗ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Duyet_danh_sach_cho_ngoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chỗ này đã có người đặt rồi bạn ơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Ket_noi.connect.Close();
                }
            }
            else
                MessageBox.Show("Đã hủy thao tác chọn chỗ ngồi, bạn có thể chọn chỗ khác nếu muốn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region "Event chọn chỗ ngồi hoàn tất"
        private void btn_TaiXe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chỗ này của tài xế bạn ơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            Duyet(btn_1);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            Duyet(btn_2);
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            Duyet(btn_3);
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            Duyet(btn_4);
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            Duyet(btn_5);
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            Duyet(btn_6);
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            Duyet(btn_7);
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            Duyet(btn_8);
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            Duyet(btn_9);
        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            Duyet(btn_10);
        }

        private void btn_11_Click(object sender, EventArgs e)
        {
            Duyet(btn_11);
        }

        private void btn_12_Click(object sender, EventArgs e)
        {
            Duyet(btn_12);
        }

        private void btn_13_Click(object sender, EventArgs e)
        {
            Duyet(btn_13);
        }

        private void btn_14_Click(object sender, EventArgs e)
        {
            Duyet(btn_14);
        }

        private void btn_15_Click(object sender, EventArgs e)
        {
            Duyet(btn_15);
        }

        private void btn_16_Click(object sender, EventArgs e)
        {
            Duyet(btn_16);
        }

        private void btn_17_Click(object sender, EventArgs e)
        {
            Duyet(btn_17);
        }

        private void btn_18_Click(object sender, EventArgs e)
        {
            Duyet(btn_18);
        }

        private void btn_19_Click(object sender, EventArgs e)
        {
            Duyet(btn_19);
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            Duyet(btn_20);
        }

        private void btn_21_Click(object sender, EventArgs e)
        {
            Duyet(btn_21);
        }

        private void btn_22_Click(object sender, EventArgs e)
        {
            Duyet(btn_22);
        }

        private void btn_23_Click(object sender, EventArgs e)
        {
            Duyet(btn_23);
        }

        private void btn_24_Click(object sender, EventArgs e)
        {
            Duyet(btn_24);
        }

        private void btn_25_Click(object sender, EventArgs e)
        {
            Duyet(btn_25);
        }

        private void btn_26_Click(object sender, EventArgs e)
        {
            Duyet(btn_26);
        }

        private void btn_27_Click(object sender, EventArgs e)
        {
            Duyet(btn_27);
        }

        private void btn_28_Click(object sender, EventArgs e)
        {
            Duyet(btn_28);
        }

        private void btn_29_Click(object sender, EventArgs e)
        {
            Duyet(btn_29);
        }

        private void btn_30_Click(object sender, EventArgs e)
        {
            Duyet(btn_30);
        }

        private void btn_31_Click(object sender, EventArgs e)
        {
            Duyet(btn_31);
        }

        private void btn_32_Click(object sender, EventArgs e)
        {
            Duyet(btn_32);
        }

        private void btn_33_Click(object sender, EventArgs e)
        {
            Duyet(btn_33);
        }

        private void btn_34_Click(object sender, EventArgs e)
        {
            Duyet(btn_34);
        }

        private void btn_35_Click(object sender, EventArgs e)
        {
            Duyet(btn_35);
        }

        private void btn_36_Click(object sender, EventArgs e)
        {
            Duyet(btn_36);
        }

        private void btn_37_Click(object sender, EventArgs e)
        {
            Duyet(btn_37);
        }

        private void btn_38_Click(object sender, EventArgs e)
        {
            Duyet(btn_38);
        }

        private void btn_39_Click(object sender, EventArgs e)
        {
            Duyet(btn_39);
        }

        private void btn_40_Click(object sender, EventArgs e)
        {
            Duyet(btn_40);
        }

        private void btn_41_Click(object sender, EventArgs e)
        {
            Duyet(btn_41);
        }

        private void btn_42_Click(object sender, EventArgs e)
        {
            Duyet(btn_42);
        }

        private void btn_43_Click(object sender, EventArgs e)
        {
            Duyet(btn_43);
        }

        private void btn_44_Click(object sender, EventArgs e)
        {
            Duyet(btn_44);
        }

        private void btn_45_Click(object sender, EventArgs e)
        {
            Duyet(btn_45);
        }
        #endregion
    }
}
