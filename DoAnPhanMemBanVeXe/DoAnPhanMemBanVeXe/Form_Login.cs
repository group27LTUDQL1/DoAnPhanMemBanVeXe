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

namespace DoAnPhanMemBanVeXe
{
    public partial class Form_Login : DevComponents.DotNetBar.Office2007Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        private bool flag = false; // Dùng kiểm soát timer
        public string LoginLoaiND = "";
        public string LoginTenND = "";

        #region "Event Login đã hoàn tất"
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter your Username");
                txtUserName.Focus();
                return;
            }

            // Yêu cầu người sử dụng nhập lại pass
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter your pass");
                txtPassword.Focus();
                return;
            }

            // Gọi hàm kiểm tra username và pass
            int x = Logged(txtUserName.Text, txtPassword.Text);

            if (x == -1)
            {
                MessageBox.Show("Bạn nhập sai password");
                txtPassword.Text = "";
                txtPassword.Focus();
            }
            else if (x == -2)
            {
                MessageBox.Show("username không tồn tại");
                txtUserName.Text = "";
                txtUserName.Focus();
            }
            else
                flag = true;
            if (flag)
            {
                TimerClosing.Start();
                TimerClosing.Interval = 100;
            }
        }
        #endregion

        #region "button exit hoàn tất"
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
        #endregion

        #region "Event time Closing đã hoàn tất"
        private void Form_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag)
            {
                e.Cancel = true;
                this.TimerClosing.Enabled = true;
            }
        }
        #endregion

        #region "Kiểm tra tính hợp lệ của kết nối"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="U">Username</param>
        /// <param name="P">Password</param>
        /// <returns></returns>
        public int Logged(string U, string P)
        {
            int check;

            string strSQL = "select IdNguoiDung, PassND, IdLoaiND from NguoiDung where IdNguoiDung = '" + U + "' ";

            SqlCommand Command = new SqlCommand(strSQL, Ket_noi.connect);

            Ket_noi.connect.Open();
            // điền dữ liệu nguồn vào đối tượng SQLDataReader
            SqlDataReader DataReader = Command.ExecuteReader();

            // nếu tồn tại mẫu tin
            if (DataReader.Read())
            {
                // So sánh password
                if (P == DataReader.GetString(1))
                {
                    // Nếu username và password đều hợp lệ
                    // Đăng nhập thành công
                    check = 0;
                    LoginLoaiND = DataReader.GetValue(2).ToString();
                    LoginTenND = DataReader.GetValue(0).ToString();
                }
                else
                    // Sai pass và trả về giá trị -1
                    check = -1;
            }
            else
                // Không tìm thấy username trong CSDL, trả về -2
                check = -2;
            DataReader.Close();
            Ket_noi.connect.Close();
            return check;
        }
        #endregion

        #region "Kiểm tra kết nối đã hoàn tất"
        private void Kiem_tra_ket_noi()
        {
            try
            {
                Ket_noi.connect.Open();
                if (Ket_noi.connect.State == ConnectionState.Open)
                    Ket_noi.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối đến cơ sở dữ liệu không thành công...");
                this.Close();
            }
        }
        #endregion

        #region "Event formload đã hoàn thành"
        private void Form_Login_Load(object sender, EventArgs e)
        {
            Timer1.Start();
            Timer2.Start();
            Ket_noi.Tao_ket_noi();
            Kiem_tra_ket_noi();
            lblChaoMung.Visible = true;
            PictureBox2.Width = 0;
        }
        #endregion

        #region "Event timer đã hoàn tất"
        private void TimerClosing_Tick(object sender, EventArgs e)
        {
            Form_Main fm = new Form_Main(this);
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

        #region "Xử lý event của 2 timer1 và timer2 hoàn tất"
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblChaoMung.Visible = !lblChaoMung.Visible;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (PictureBox2.Width < PictureBox1.Width)
                PictureBox2.Width = PictureBox2.Width + PictureBox1.Width / 20;
            else
                PictureBox2.Width = 0;
        }
        #endregion
    }
}
