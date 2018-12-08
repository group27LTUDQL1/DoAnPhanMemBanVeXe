namespace DoAnPhanMemBanVeXe_2
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
            this.Timer_Doi_Anh = new System.Windows.Forms.Timer(this.components);
            this.Label_UserName = new DevComponents.DotNetBar.LabelX();
            this.Label_Password = new DevComponents.DotNetBar.LabelX();
            this.txt_NewPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_IdNguoiDung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btn_DongY = new DevComponents.DotNetBar.ButtonX();
            this.cmdExit = new DevComponents.DotNetBar.ButtonX();
            this.LinkLabelLanguague = new System.Windows.Forms.LinkLabel();
            this.ReflectionLabel_ChaoMung = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel_Cappass = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.Panel_Cappass.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer_Doi_Anh
            // 
            this.Timer_Doi_Anh.Enabled = true;
            this.Timer_Doi_Anh.Interval = 1000;
            // 
            // Label_UserName
            // 
            // 
            // 
            // 
            this.Label_UserName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Label_UserName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label_UserName.Location = new System.Drawing.Point(200, 118);
            this.Label_UserName.Name = "Label_UserName";
            this.Label_UserName.Size = new System.Drawing.Size(96, 23);
            this.Label_UserName.TabIndex = 1;
            this.Label_UserName.Text = "&Id người dùng";
            // 
            // Label_Password
            // 
            // 
            // 
            // 
            this.Label_Password.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Label_Password.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label_Password.Location = new System.Drawing.Point(200, 162);
            this.Label_Password.Name = "Label_Password";
            this.Label_Password.Size = new System.Drawing.Size(96, 23);
            this.Label_Password.TabIndex = 3;
            this.Label_Password.Text = "&Password mới";
            // 
            // txt_NewPassword
            // 
            // 
            // 
            // 
            this.txt_NewPassword.Border.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor2;
            this.txt_NewPassword.Border.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.txt_NewPassword.Border.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.txt_NewPassword.Border.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.txt_NewPassword.Border.Class = "TextBoxBorder";
            this.txt_NewPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_NewPassword.FocusHighlightEnabled = true;
            this.txt_NewPassword.Location = new System.Drawing.Point(312, 162);
            this.txt_NewPassword.Name = "txt_NewPassword";
            this.txt_NewPassword.PasswordChar = '●';
            this.txt_NewPassword.Size = new System.Drawing.Size(160, 21);
            this.txt_NewPassword.TabIndex = 4;
            this.txt_NewPassword.UseSystemPasswordChar = true;
            // 
            // txt_IdNguoiDung
            // 
            // 
            // 
            // 
            this.txt_IdNguoiDung.Border.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor2;
            this.txt_IdNguoiDung.Border.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.txt_IdNguoiDung.Border.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.txt_IdNguoiDung.Border.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.txt_IdNguoiDung.Border.Class = "TextBoxBorder";
            this.txt_IdNguoiDung.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_IdNguoiDung.FocusHighlightEnabled = true;
            this.txt_IdNguoiDung.Location = new System.Drawing.Point(312, 118);
            this.txt_IdNguoiDung.Name = "txt_IdNguoiDung";
            this.txt_IdNguoiDung.Size = new System.Drawing.Size(160, 21);
            this.txt_IdNguoiDung.TabIndex = 4;
            // 
            // btn_DongY
            // 
            this.btn_DongY.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_DongY.AutoSize = true;
            this.btn_DongY.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btn_DongY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_DongY.Location = new System.Drawing.Point(265, 222);
            this.btn_DongY.Name = "btn_DongY";
            this.btn_DongY.Size = new System.Drawing.Size(87, 26);
            this.btn_DongY.TabIndex = 5;
            this.btn_DongY.Text = "&Đồng ý";
            this.btn_DongY.Click += new System.EventHandler(this.btn_DongY_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExit.AutoSize = true;
            this.cmdExit.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdExit.Location = new System.Drawing.Point(402, 222);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(84, 26);
            this.cmdExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "&Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // LinkLabelLanguague
            // 
            this.LinkLabelLanguague.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LinkLabelLanguague.Location = new System.Drawing.Point(0, 0);
            this.LinkLabelLanguague.Name = "LinkLabelLanguague";
            this.LinkLabelLanguague.Size = new System.Drawing.Size(100, 23);
            this.LinkLabelLanguague.TabIndex = 0;
            // 
            // ReflectionLabel_ChaoMung
            // 
            this.ReflectionLabel_ChaoMung.BackColor = System.Drawing.Color.ForestGreen;
            // 
            // 
            // 
            this.ReflectionLabel_ChaoMung.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ReflectionLabel_ChaoMung.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReflectionLabel_ChaoMung.ForeColor = System.Drawing.Color.White;
            this.ReflectionLabel_ChaoMung.Location = new System.Drawing.Point(0, 0);
            this.ReflectionLabel_ChaoMung.Name = "ReflectionLabel_ChaoMung";
            this.ReflectionLabel_ChaoMung.Size = new System.Drawing.Size(521, 67);
            this.ReflectionLabel_ChaoMung.TabIndex = 8;
            this.ReflectionLabel_ChaoMung.Text = "CẤP PASS CHO NGƯỜI DÙNG";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = global::DoAnPhanMemBanVeXe_2.Properties.Resources.ferme;
            this.PictureBox1.Location = new System.Drawing.Point(31, 101);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(132, 120);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 9;
            this.PictureBox1.TabStop = false;
            // 
            // Panel_Cappass
            // 
            this.Panel_Cappass.CanvasColor = System.Drawing.SystemColors.Control;
            this.Panel_Cappass.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Panel_Cappass.Controls.Add(this.PictureBox1);
            this.Panel_Cappass.Controls.Add(this.ReflectionLabel_ChaoMung);
            this.Panel_Cappass.Controls.Add(this.LinkLabelLanguague);
            this.Panel_Cappass.Controls.Add(this.cmdExit);
            this.Panel_Cappass.Controls.Add(this.btn_DongY);
            this.Panel_Cappass.Controls.Add(this.txt_IdNguoiDung);
            this.Panel_Cappass.Controls.Add(this.txt_NewPassword);
            this.Panel_Cappass.Controls.Add(this.Label_Password);
            this.Panel_Cappass.Controls.Add(this.Label_UserName);
            this.Panel_Cappass.DisabledBackColor = System.Drawing.Color.Empty;
            this.Panel_Cappass.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Panel_Cappass.Location = new System.Drawing.Point(-1, -1);
            this.Panel_Cappass.Name = "Panel_Cappass";
            this.Panel_Cappass.Size = new System.Drawing.Size(517, 283);
            this.Panel_Cappass.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Panel_Cappass.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.Panel_Cappass.Style.BackColor2.Color = System.Drawing.Color.MediumSpringGreen;
            this.Panel_Cappass.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Panel_Cappass.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Panel_Cappass.Style.ForeColor.Color = System.Drawing.Color.Chartreuse;
            this.Panel_Cappass.Style.GradientAngle = 90;
            this.Panel_Cappass.StyleMouseDown.BackColor1.Color = System.Drawing.Color.GreenYellow;
            this.Panel_Cappass.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Panel_Cappass.StyleMouseOver.BackColor1.Color = System.Drawing.Color.PaleGreen;
            this.Panel_Cappass.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Panel_Cappass.TabIndex = 7;
            // 
            // Form_Cap_pass
            // 
            this.AcceptButton = this.btn_DongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 286);
            this.Controls.Add(this.Panel_Cappass);
            this.Name = "Form_Cap_pass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Cap_pass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.Panel_Cappass.ResumeLayout(false);
            this.Panel_Cappass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Timer Timer_Doi_Anh;
        private DevComponents.DotNetBar.LabelX Label_UserName;
        private DevComponents.DotNetBar.LabelX Label_Password;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_NewPassword;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_IdNguoiDung;
        private DevComponents.DotNetBar.ButtonX btn_DongY;
        private DevComponents.DotNetBar.ButtonX cmdExit;
        internal System.Windows.Forms.LinkLabel LinkLabelLanguague;
        private DevComponents.DotNetBar.Controls.ReflectionLabel ReflectionLabel_ChaoMung;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private DevComponents.DotNetBar.PanelEx Panel_Cappass;
    }
}