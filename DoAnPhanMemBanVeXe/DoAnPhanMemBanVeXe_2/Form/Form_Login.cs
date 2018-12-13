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
    public partial class Form_Login : Form
    {
        Form_Main fm;
        private bool flag = false; // Dung kiem soat timer
        public string LoginLoaiND = "";
        public string LoginTenND = "";

        public Form_Login()
        {
            Load += Form_Login_Load;
            InitializeComponent();
        }

        #region "Event form load da hoan tat nhung chua hay"
        private void Form_Login_Load(object sender, EventArgs e)
        {
            Ket_noi.Tao_ket_noi();
            Kiem_tra_ket_noi();
            lblChaoMung.Visible = true;
            PictureBox2.Width = 0;
        }
        #endregion
        /// <summary>
        /// xử lí xong hiệu ứng chớp nháy chữ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblChaoMung.Visible = !lblChaoMung.Visible;
        }

        /// <summary>
        /// xử lí xong sự hiệu ứng chuyển đổi hình ảnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (PictureBox2.Width < PictureBox1.Width)
                PictureBox2.Width = PictureBox2.Width + PictureBox1.Width / 20;
            else
                PictureBox2.Width = 0;
        }

        /// <summary>
        /// xử lí xong sự kiện ấn nút thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đóng chương trình");
            }
        }

        #region "Event Timer_Tick da hoan tat"
        private void TimerClosing_Tick(object sender, EventArgs e)
        {
            
            fm = new Form_Main();
            this.Opacity -= 0.05;
            if (this.Opacity == 0)
            {
                TimerClosing.Stop();
                Timer2.Stop();
                Timer1.Stop();
                flag = false;
                this.Visible = false;
                fm.Show();
                fm.WindowState = FormWindowState.Maximized;
            }
        }
        #endregion

        #region "Event Login_Click da hoan tat"
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                Interaction.MsgBox("Please enter your Username");
                txtUserName.Focus();
                return;
            }
            //Yeu cau nguoi su dung nhap lai pass
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                Interaction.MsgBox("Please enter your pass");
                txtPassword.Focus();
                return;
            }
            //Goi ham kiem tra username va pass
            int x = Logged(txtUserName.Text, txtPassword.Text);
            if (x == -1)
            {
                Interaction.MsgBox("Bạn nhập sai password");
                txtPassword.Text = "";
                txtPassword.Focus();
            }
            else if (x == -2)
            {
                Interaction.MsgBox("username không tồn tại");
                txtUserName.Text = "";
                txtUserName.Focus();
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                TimerClosing.Start();
                TimerClosing.Interval = 100;
            }
        }
        #endregion

        #region "Hàm kiêm tra tinh hop le cua ket noi"
        public int Logged(string U, string P)
        {
            int functionReturnValue = 0;
            string strSQL = "select IdNguoiDung, PassND, IdLoaiND from NguoiDung where IdNguoiDung = '" + U + "' ";
            SqlCommand Command = new SqlCommand(strSQL, Ket_noi.connect);
            Ket_noi.connect.Open();
            //dien du lieu nguon vao doi tuong SQLDataReader
            SqlDataReader DataReader = Command.ExecuteReader();
            //Neu ton tai mau tin
            if (DataReader.Read())
            {
                //So sanh password
                if (P == DataReader.GetString(1))
                {
                    //Nếu username và password đều hợp le
                    //Dang nhap thanh cong
                    functionReturnValue = 0;
                    LoginLoaiND = DataReader.GetValue(2).ToString();
                    LoginTenND = DataReader.GetValue(0).ToString();
                }
                else
                {
                    //Sai pass và trả về giá trị -1
                    functionReturnValue = -1;
                }
            }
            else
            {
                //Khong tim thay username trong CSDL, trả về -2
                functionReturnValue = -2;
            }
            DataReader.Close();
            Ket_noi.connect.Close();
            return functionReturnValue;
        }
        #endregion

        #region "Kiem tra trang thai ket noi da hoan tat"
        private void Kiem_tra_ket_noi()
        {
            try
            {
                Ket_noi.connect.Open();
                if (Ket_noi.connect.State == ConnectionState.Open)
                {
                    Ket_noi.connect.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối đến cơ sở dữ liệu không thành công...");
                this.Close();
            }
        }

        #endregion

        #region "Event FormClosing da hoan tat"
        private void Form_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag)
            {
                e.Cancel = true;
                this.TimerClosing.Enabled = true;
            }
        }

        #endregion

       
    }
}
