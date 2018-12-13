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
using DevComponents.DotNetBar;


namespace DoAnPhanMemBanVeXe_2
{
    public partial class Form_Main : DevComponents.DotNetBar.Office2007RibbonForm
    {

        Form_Login fl;//khởi tạo
        private bool flag = true;
        private Nguoi_dung Nguoi_dung = new Nguoi_dung();
        private Xe Xe = new Xe();
        private Tuyen_xe Tuyen_xe = new Tuyen_xe();
        private Thoi_diem Thoi_diem = new Thoi_diem();
        private Chuyen_xe Chuyen_Xe = new Chuyen_xe();
        private Ban_ve Ban_ve = new Ban_ve();
        private Form_Phan_Quyen Quyen = new Form_Phan_Quyen();
        private Update_he_thong update_he_thong = new Update_he_thong();
        public Form_Main()
        {
            
            InitializeComponent();
        }

        

        #region "Xu ly Timer da xong"
        private void ribbonControl1_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Timer_ChayChu.Stop();
                flag = !flag;
            }
            else
            {
                Timer_ChayChu.Start();
                flag = !flag;
            }
        }

        private void Timer_ChayChu_Tick(object sender, EventArgs e)
        {
            if (lblChayChu.Left < 0)
            {
                lblChayChu.Left = 1400;
                flag = !flag;
            }
            else
            {
                lblChayChu.Left -= 10;
                if (flag)
                {
                    lblChayChu.ForeColor = Color.Black;
                    flag = !flag;
                }
                else
                {
                    lblChayChu.ForeColor = Color.Teal;
                    flag = !flag;
                }
            }
        }

        #endregion

        

      

      