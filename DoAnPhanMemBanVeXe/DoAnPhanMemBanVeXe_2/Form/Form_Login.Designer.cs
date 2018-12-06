namespace DoAnPhanMemBanVeXe_2
{
    partial class Form_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.TimerClosing = new System.Windows.Forms.Timer(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.ReflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.lblChaoMung = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.PanelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.PanelLogin = new DevComponents.DotNetBar.PanelEx();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.LinkLabelLanguague = new System.Windows.Forms.LinkLabel();
            this.Label_HuongDan = new DevComponents.DotNetBar.LabelX();
            this.cmdExit = new DevComponents.DotNetBar.ButtonX();
            this.cmdLogin = new DevComponents.DotNetBar.ButtonX();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Label_Password = new DevComponents.DotNetBar.LabelX();
            this.Label_UserName = new DevComponents.DotNetBar.LabelX();
            this.DockContainerItem3 = new DevComponents.DotNetBar.DockContainerItem();
            this.DockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
            this.DockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.DockContainerItem4 = new DevComponents.DotNetBar.DockContainerItem();
            this.PanelEx1.SuspendLayout();
            this.PanelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Timer2
            // 
            this.Timer2.Enabled = true;
            this.Timer2.Interval = 300;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // TimerClosing
            // 
            this.TimerClosing.Tick += new System.EventHandler(this.TimerClosing_Tick);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 400;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ReflectionLabel1
            // 
            // 
            // 
            // 
            this.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ReflectionLabel1.ForeColor = System.Drawing.Color.Blue;
            this.ReflectionLabel1.Location = new System.Drawing.Point(12, 267);
            this.ReflectionLabel1.Name = "ReflectionLabel1";
            this.ReflectionLabel1.Size = new System.Drawing.Size(219, 43);
            this.ReflectionLabel1.TabIndex = 8;
            this.ReflectionLabel1.Text = "<b><font size=\"+4\"><font color=\"#B02B2C\">Chúc một ngày tốt lành</font></font></b>" +
    "";
            // 
            // lblChaoMung
            // 
            // 
            // 
            // 
            this.lblChaoMung.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblChaoMung.ForeColor = System.Drawing.Color.Blue;
            this.lblChaoMung.Location = new System.Drawing.Point(51, 222);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(130, 43);
            this.lblChaoMung.TabIndex = 8;
            this.lblChaoMung.Text = "<b><font size=\"+8\"><font color=\"#B02B2C\">Đăng nhập</font></font></b>";
            // 
            // PanelEx1
            // 
            this.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PanelEx1.Controls.Add(this.PanelLogin);
            this.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelEx1.Location = new System.Drawing.Point(0, 0);
            this.PanelEx1.Name = "PanelEx1";
            this.PanelEx1.Size = new System.Drawing.Size(543, 322);
            this.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.PanelEx1.Style.BackColor2.Color = System.Drawing.Color.MediumSpringGreen;
            this.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.PanelEx1.Style.GradientAngle = 90;
            this.PanelEx1.StyleMouseDown.BackColor1.Color = System.Drawing.Color.GreenYellow;
            this.PanelEx1.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(180)))));
            this.PanelEx1.StyleMouseOver.BackColor1.Color = System.Drawing.Color.PaleGreen;
            this.PanelEx1.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PanelEx1.TabIndex = 4;
            // 
            // PanelLogin
            // 
            this.PanelLogin.CanvasColor = System.Drawing.SystemColors.Control;
            this.PanelLogin.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PanelLogin.Controls.Add(this.PictureBox2);
            this.PanelLogin.Controls.Add(this.PictureBox1);
            this.PanelLogin.Controls.Add(this.ReflectionLabel1);
            this.PanelLogin.Controls.Add(this.lblChaoMung);
            this.PanelLogin.Controls.Add(this.LinkLabelLanguague);
            this.PanelLogin.Controls.Add(this.Label_HuongDan);
            this.PanelLogin.Controls.Add(this.cmdExit);
            this.PanelLogin.Controls.Add(this.cmdLogin);
            this.PanelLogin.Controls.Add(this.txtUserName);
            this.PanelLogin.Controls.Add(this.txtPassword);
            this.PanelLogin.Controls.Add(this.Label_Password);
            this.PanelLogin.Controls.Add(this.Label_UserName);
            this.PanelLogin.DisabledBackColor = System.Drawing.Color.Empty;
            this.PanelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLogin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PanelLogin.Location = new System.Drawing.Point(0, 0);
            this.PanelLogin.Name = "PanelLogin";
            this.PanelLogin.Size = new System.Drawing.Size(543, 322);
            this.PanelLogin.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.PanelLogin.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.PanelLogin.Style.BackColor2.Color = System.Drawing.Color.MediumSpringGreen;
            this.PanelLogin.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.PanelLogin.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PanelLogin.Style.ForeColor.Color = System.Drawing.Color.Chartreuse;
            this.PanelLogin.Style.GradientAngle = 90;
            this.PanelLogin.StyleMouseDown.BackColor1.Color = System.Drawing.Color.GreenYellow;
            this.PanelLogin.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PanelLogin.StyleMouseOver.BackColor1.Color = System.Drawing.Color.PaleGreen;
            this.PanelLogin.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PanelLogin.TabIndex = 1;
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = global::DoAnPhanMemBanVeXe_2.Properties.Resources.bus_icon;
            this.PictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PictureBox2.Location = new System.Drawing.Point(37, 76);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(157, 140);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox2.TabIndex = 12;
            this.PictureBox2.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = global::DoAnPhanMemBanVeXe_2.Properties.Resources.user_login_icon;
            this.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PictureBox1.Location = new System.Drawing.Point(37, 76);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(157, 140);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 11;
            this.PictureBox1.TabStop = false;
            // 
            // LinkLabelLanguague
            // 
            this.LinkLabelLanguague.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LinkLabelLanguague.Location = new System.Drawing.Point(0, 0);
            this.LinkLabelLanguague.Name = "LinkLabelLanguague";
            this.LinkLabelLanguague.Size = new System.Drawing.Size(100, 23);
            this.LinkLabelLanguague.TabIndex = 0;
            // 
            // Label_HuongDan
            // 
            // 
            // 
            // 
            this.Label_HuongDan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Label_HuongDan.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label_HuongDan.Location = new System.Drawing.Point(212, 61);
            this.Label_HuongDan.Name = "Label_HuongDan";
            this.Label_HuongDan.Size = new System.Drawing.Size(282, 36);
            this.Label_HuongDan.TabIndex = 0;
            this.Label_HuongDan.Text = "Nhập UserName và Password của bạn";
            // 
            // cmdExit
            // 
            this.cmdExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExit.AutoSize = true;
            this.cmdExit.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdExit.Location = new System.Drawing.Point(402, 222);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(84, 23);
            this.cmdExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdLogin
            // 
            this.cmdLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdLogin.AutoSize = true;
            this.cmdLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.cmdLogin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdLogin.Location = new System.Drawing.Point(265, 222);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(87, 23);
            this.cmdLogin.TabIndex = 5;
            this.cmdLogin.Text = "Đăng nhập";
            // 
            // txtUserName
            // 
            // 
            // 
            // 
            this.txtUserName.Border.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor2;
            this.txtUserName.Border.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.txtUserName.Border.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.txtUserName.Border.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.txtUserName.Border.Class = "TextBoxBorder";
            this.txtUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserName.FocusHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtUserName.FocusHighlightEnabled = true;
            this.txtUserName.Location = new System.Drawing.Point(312, 118);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(160, 21);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            // 
            // 
            // 
            this.txtPassword.Border.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor2;
            this.txtPassword.Border.BorderLeftColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.txtPassword.Border.BorderRightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.txtPassword.Border.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword.FocusHighlightEnabled = true;
            this.txtPassword.Location = new System.Drawing.Point(312, 162);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(160, 21);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // Label_Password
            // 
            // 
            // 
            // 
            this.Label_Password.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Label_Password.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label_Password.Location = new System.Drawing.Point(221, 162);
            this.Label_Password.Name = "Label_Password";
            this.Label_Password.Size = new System.Drawing.Size(75, 23);
            this.Label_Password.TabIndex = 3;
            this.Label_Password.Text = "&Password";
            // 
            // Label_UserName
            // 
            // 
            // 
            // 
            this.Label_UserName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Label_UserName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label_UserName.Location = new System.Drawing.Point(221, 118);
            this.Label_UserName.Name = "Label_UserName";
            this.Label_UserName.Size = new System.Drawing.Size(75, 23);
            this.Label_UserName.TabIndex = 1;
            this.Label_UserName.Text = "&User name";
            // 
            // DockContainerItem3
            // 
            this.DockContainerItem3.Name = "DockContainerItem3";
            this.DockContainerItem3.Text = "DockContainerItem3";
            // 
            // DockContainerItem2
            // 
            this.DockContainerItem2.Name = "DockContainerItem2";
            this.DockContainerItem2.Text = "DockContainerItem2";
            // 
            // DockContainerItem1
            // 
            this.DockContainerItem1.Name = "DockContainerItem1";
            this.DockContainerItem1.Text = "DockContainerItem1";
            // 
            // DockContainerItem4
            // 
            this.DockContainerItem4.Name = "DockContainerItem4";
            this.DockContainerItem4.Text = "DockContainerItem4";
            // 
            // Form_Login
            // 
            this.AcceptButton = this.cmdLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 322);
            this.Controls.Add(this.PanelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PanelEx1.ResumeLayout(false);
            this.PanelLogin.ResumeLayout(false);
            this.PanelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        internal System.Windows.Forms.Timer Timer2;
        internal System.Windows.Forms.Timer TimerClosing;
        internal System.Windows.Forms.Timer Timer1;
        internal DevComponents.DotNetBar.Controls.ReflectionLabel ReflectionLabel1;
        internal DevComponents.DotNetBar.Controls.ReflectionLabel lblChaoMung;
        internal DevComponents.DotNetBar.PanelEx PanelEx1;
        internal DevComponents.DotNetBar.PanelEx PanelLogin;
        internal System.Windows.Forms.LinkLabel LinkLabelLanguague;
        internal DevComponents.DotNetBar.LabelX Label_HuongDan;
        internal DevComponents.DotNetBar.ButtonX cmdExit;
        internal DevComponents.DotNetBar.ButtonX cmdLogin;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        internal DevComponents.DotNetBar.LabelX Label_Password;
        internal DevComponents.DotNetBar.LabelX Label_UserName;
        internal DevComponents.DotNetBar.DockContainerItem DockContainerItem3;
        internal DevComponents.DotNetBar.DockContainerItem DockContainerItem2;
        internal DevComponents.DotNetBar.DockContainerItem DockContainerItem1;
        internal DevComponents.DotNetBar.DockContainerItem DockContainerItem4;
        internal System.Windows.Forms.PictureBox PictureBox2;
        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}