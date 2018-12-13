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

        

      

      