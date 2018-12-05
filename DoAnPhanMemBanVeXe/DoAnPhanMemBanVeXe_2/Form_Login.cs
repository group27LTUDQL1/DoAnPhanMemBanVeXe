using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnPhanMemBanVeXe_2
{
    public partial class Form_Login : Form
    {
        private bool flag = false; // Dung kiem soat timer
        public string LoginLoaiND = "";
        public string LoginTenND = "";

        public Form_Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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

        private void TimerClosing_Tick(object sender, EventArgs e)
        {
            /*this.Opacity -= 0.05;
            if (this.Opacity == 0)
            {
                TimerClosing.Stop();
                Timer2.Stop();
                Timer1.Stop();
                flag = false;
                this.Visible = false;
                Form_Main.Show();
                Form_Main.WindowState = FormWindowState.Maximized;
            }*/
        }
    }
}
