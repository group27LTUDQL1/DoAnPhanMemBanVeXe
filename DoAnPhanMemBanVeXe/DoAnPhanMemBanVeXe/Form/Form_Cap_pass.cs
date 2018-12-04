using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnPhanMemBanVeXe
{
    public partial class Form_Cap_pass : Form
    {
        public Form_Cap_pass()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// sự kiện đóng form ấn button thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Timer_Doi_Anh.Stop();
            this.Close();
        }
    }
}
