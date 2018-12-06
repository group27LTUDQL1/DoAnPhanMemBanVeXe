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
    public partial class Form_Cap_pass : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public Form_Cap_pass()
        {
            InitializeComponent();
        }

        private void Form_Cap_pass_Load(object sender, EventArgs e)
        {

        }

        private void Timer_Doi_Anh_Tick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// xong button thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Timer_Doi_Anh.Stop();
            this.Close();
        }

        private void ReflectionLabel_ChaoMung_Click(object sender, EventArgs e)
        {
            
        }
    }
}
