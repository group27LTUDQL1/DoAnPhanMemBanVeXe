using System;
using Microsoft.VisualBasic;
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
    public partial class Form_Cap_pass : DevComponents.DotNetBar.Office2007RibbonForm
    {
        Form_Main fm = new Form_Main();
        private bool flag = true;
        private Nguoi_dung Nguoi_dung = new Nguoi_dung();

        public Form_Cap_pass()
        {
            InitializeComponent();
        }

        private void Form_Cap_pass_Load(object sender, EventArgs e)
        {
            txt_IdNguoiDung.Text = fm.cbo_Username.Text;
            txt_IdNguoiDung.Enabled = false;
            txt_NewPassword.Focus();
        }

        private void Timer_Doi_Anh_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                PictureBox1.Image = DoAnPhanMemBanVeXe_2.Properties.Resources.Fermer_session;
                flag = !flag;
            }
            else
            {
                PictureBox1.Image = DoAnPhanMemBanVeXe_2.Properties.Resources.ferme;
                flag = !flag;
            }
        }

        /// <summary>
        /// xong button thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void btn_DongY_Click(object sender, EventArgs e)
        {
            if (txt_NewPassword.Text.Trim().Length < 5)
            {
                Interaction.MsgBox("Password không được ít hơn 5 kí tự!", MsgBoxStyle.Exclamation, "Thông báo lỗi");
                txt_NewPassword.Focus();
            }
            else
            {
                string lenh = "Update NguoiDung Set PassND = '" + txt_NewPassword.Text + "' where IdNguoiDung = '" + txt_IdNguoiDung.Text + "'";
                SqlCommand bo_lenh = new SqlCommand(lenh, Ket_noi.connect);
                try
                {
                    Ket_noi.connect.Open();
                    bo_lenh.ExecuteNonQuery();
                    Ket_noi.connect.Close();
                    MessageBox.Show("Pass mới đã được cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Nguoi_dung.UpdateNguoiDung();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cấp pass không thành công, yêu cầu kiểm tra lại kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Timer_Doi_Anh.Stop();
            this.Close();
        }

        
    }
}
