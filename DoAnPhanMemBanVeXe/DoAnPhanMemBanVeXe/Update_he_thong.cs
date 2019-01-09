using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMemBanVeXe
{
    public class Update_he_thong
    {
        private string lenh;
        private DataTable bang;

        public void update_()
        {
            lenh = "Delete from ChiTietTuyen where IdThoiDiem in (Select IdThoiDiem from ThoiDiem where Ngay < '" + Convert.ToString(DateTime.Today.Date) + "')";

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
            }
            lenh = "Select * from ChiTietTuyen";
            bang = Ket_noi.Doc_bang(lenh);
            if (bang.Rows.Count == 0)
                lenh = "Delete from ThoiDiem";
            else
                lenh = "Delete from ThoiDiem where Ngay < '" + Convert.ToString(DateTime.Today.Date) + "'";

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
            }

            // --------------------------------------------------Xu ly voi bang chuyenxxe, chongoi, banve
            lenh = "Delete from BanVe where IdChuyen in( Select IdChuyen from ChuyenXe where NgayDi < '" + System.Convert.ToString(DateTime.Today.Date) + "')";
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
            }

            lenh = "Delete from ChoNgoi where IdChuyen in( Select IdChuyen from ChuyenXe where NgayDi < '" + System.Convert.ToString(DateTime.Today.Date) + "')";
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
            }


            lenh = "Select * from BanVe";
            bang = Ket_noi.Doc_bang(lenh);
            if (bang.Rows.Count == 0)
                lenh = "Delete from ChuyenXe";
            else
                lenh = "Delete from ChuyenXe where IdChuyen <> (Select IdChuyen from BanVe)";

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
            }
        }
    }
}
