namespace DoAnPhanMemBanVeXe
{
    partial class Form_Cap_pass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_NewPassword = new System.Windows.Forms.TextBox();
            this.txt_IdNguoiDung = new System.Windows.Forms.TextBox();
            this.btn_DongY = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Timer_Doi_Anh = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Người dùng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password mới";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_NewPassword);
            this.groupBox1.Controls.Add(this.txt_IdNguoiDung);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(180, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 105);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập thông tin tài khoản mới";
            // 
            // txt_NewPassword
            // 
            this.txt_NewPassword.Location = new System.Drawing.Point(114, 66);
            this.txt_NewPassword.Name = "txt_NewPassword";
            this.txt_NewPassword.Size = new System.Drawing.Size(230, 26);
            this.txt_NewPassword.TabIndex = 3;
            // 
            // txt_IdNguoiDung
            // 
            this.txt_IdNguoiDung.Location = new System.Drawing.Point(114, 30);
            this.txt_IdNguoiDung.Name = "txt_IdNguoiDung";
            this.txt_IdNguoiDung.Size = new System.Drawing.Size(230, 26);
            this.txt_IdNguoiDung.TabIndex = 2;
            // 
            // btn_DongY
            // 
            this.btn_DongY.Location = new System.Drawing.Point(283, 271);
            this.btn_DongY.Name = "btn_DongY";
            this.btn_DongY.Size = new System.Drawing.Size(75, 40);
            this.btn_DongY.TabIndex = 3;
            this.btn_DongY.Text = "Đồng ý";
            this.btn_DongY.UseVisualStyleBackColor = true;
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(365, 271);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 40);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DoAnPhanMemBanVeXe.Properties.Resources.ferme;
            this.pictureBox1.Location = new System.Drawing.Point(2, 159);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 152);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Timer_Doi_Anh
            // 
            this.Timer_Doi_Anh.Interval = 1000;
            // 
            // Form_Cap_pass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 314);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.btn_DongY);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Cap_pass";
            this.Text = "Cấp Password";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_NewPassword;
        private System.Windows.Forms.TextBox txt_IdNguoiDung;
        private System.Windows.Forms.Button btn_DongY;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer Timer_Doi_Anh;
    }
}

