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
    public class Update_he_thong
    {
        private string lenh;

        private DataTable bang;
        public void update_()
        {
            lenh = "Delete from ChiTietTuyen where IdThoiDiem in (Select IdThoiDiem from ThoiDiem where Ngay < '" + Convert.ToString(DateAndTime.Today.Date) + "')";
            //MessageBox.Show(lenh)
            SqlCommand com1 = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                com1.ExecuteNonQuery();
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                Ket_noi.connect.Close();
                //MessageBox.Show("Xoa ko thanh cong")
            }
            lenh = "Select * from ChiTietTuyen";
            bang = Ket_noi.Doc_bang(lenh);
            if (bang.Rows.Count == 0)
            {
                //MessageBox.Show(bang.Rows.Count.ToString)
                lenh = "Delete from ThoiDiem";
            }
            else
            {
                //MessageBox.Show(bang.Rows.Count.ToString)
                lenh = "Delete from ThoiDiem where Ngay < '" + Convert.ToString(DateAndTime.Today.Date) + "'";
                //IdThoiDiem <> (Select distinct IdThoiDiem from ChiTietTuyen) and 
                //MessageBox.Show(lenh)
            }

            //MessageBox.Show(lenh1)
            //MessageBox.Show(lenh)
            //'connect.Close()
            SqlCommand com2 = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                com2.ExecuteNonQuery();
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                Ket_noi.connect.Close();
                //MessageBox.Show("Xoa ko thanh cong")
            }

            //--------------------------------------------------Xu ly voi bang chuyenxxe, chongoi, banve
            lenh = "Delete from BanVe where IdChuyen in( Select IdChuyen from ChuyenXe where NgayDi < '" + Convert.ToString(DateAndTime.Today.Date) + "')";
            SqlCommand com = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                com.ExecuteNonQuery();
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                Ket_noi.connect.Close();
                //MessageBox.Show("Xoa ko thanh cong")
            }

            lenh = "Delete from ChoNgoi where IdChuyen in( Select IdChuyen from ChuyenXe where NgayDi < '" + Convert.ToString(DateAndTime.Today.Date) + "')";
            SqlCommand com4 = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                com4.ExecuteNonQuery();
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                Ket_noi.connect.Close();
                //MessageBox.Show("Xoa ko thanh cong")
            }


            lenh = "Select * from BanVe";
            bang = Ket_noi.Doc_bang(lenh);
            if (bang.Rows.Count == 0)
            {
                //MessageBox.Show(bang.Rows.Count.ToString)
                lenh = "Delete from ChuyenXe";
            }
            else
            {
                //MessageBox.Show(bang.Rows.Count.ToString)
                lenh = "Delete from ChuyenXe where IdChuyen <> (Select IdChuyen from BanVe)";
            }

            //MessageBox.Show(lenh)
            //MessageBox.Show(lenh)
            //connect.Close()
            SqlCommand com3 = new SqlCommand(lenh, Ket_noi.connect);
            try
            {
                Ket_noi.connect.Open();
                com3.ExecuteNonQuery();
                Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                Ket_noi.connect.Close();
                //MessageBox.Show("Xoa ko thanh cong")
            }

        }
    }
}
