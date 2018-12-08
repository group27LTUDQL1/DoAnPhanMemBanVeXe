using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DoAnPhanMemBanVeXe_2
{
     static class Ket_noi
    {

        public static SqlConnection connect;
        public static void Tao_ket_noi()
        {
            if (connect == null)
            {
                string chuoi_ket_noi = @"Data Source=NGHIA69\SQLEXPRESS;Initial Catalog=QuanLyBenXe;Integrated Security=True";
                connect = new SqlConnection(chuoi_ket_noi);
            }
        }

        public static DataTable Doc_bang(string lenh)
        {
            Tao_ket_noi();
            System.Data.DataTable bang = new System.Data.DataTable();
            SqlDataAdapter bo_doc_ghi = new SqlDataAdapter(lenh, connect);
            bo_doc_ghi.FillSchema(bang, SchemaType.Source);
            bo_doc_ghi.Fill(bang);
            return bang;
        }
    }
}
