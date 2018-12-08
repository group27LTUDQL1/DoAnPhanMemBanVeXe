namespace DoAnPhanMemBanVeXe_2
{
    partial class Form_ChiTietTuyen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ReflectionLabel_ChaoMung = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.LinkLabelLanguague = new System.Windows.Forms.LinkLabel();
            this.lblMaSoTuyen = new DevComponents.DotNetBar.LabelX();
            this.cot_gio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cot_Ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cot_IdTuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cot_Ma_so_tuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luoi_Thoi_diem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txt_GioChay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.date_Chay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.LabelX3 = new DevComponents.DotNetBar.LabelX();
            this.LabelX2 = new DevComponents.DotNetBar.LabelX();
            this.LabelX4 = new DevComponents.DotNetBar.LabelX();
            this.LabelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbo_MaSoTuyen1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbo_MaThoiDiem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btn_Xoa = new DevComponents.DotNetBar.ButtonX();
            this.btn_thoat = new DevComponents.DotNetBar.ButtonX();
            this.GroupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btn_HienDanhSach = new DevComponents.DotNetBar.ButtonX();
            this.cbo_TenTuyen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbo_MaSoTuyen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.grb_ThoiDiem = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblTenTuyen = new DevComponents.DotNetBar.LabelX();
            this.Panel_ChiTietTuyen = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.luoi_Thoi_diem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Chay)).BeginInit();
            this.GroupPanel1.SuspendLayout();
            this.grb_ThoiDiem.SuspendLayout();
            this.Panel_ChiTietTuyen.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReflectionLabel_ChaoMung
            // 
            this.ReflectionLabel_ChaoMung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReflectionLabel_ChaoMung.BackColor = System.Drawing.Color.ForestGreen;
            // 
            // 
            // 
            this.ReflectionLabel_ChaoMung.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ReflectionLabel_ChaoMung.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReflectionLabel_ChaoMung.ForeColor = System.Drawing.Color.White;
            this.ReflectionLabel_ChaoMung.Location = new System.Drawing.Point(0, 0);
            this.ReflectionLabel_ChaoMung.Name = "ReflectionLabel_ChaoMung";
            this.ReflectionLabel_ChaoMung.Size = new System.Drawing.Size(907, 59);
            this.ReflectionLabel_ChaoMung.TabIndex = 8;
            this.ReflectionLabel_ChaoMung.Text = "                                      CHI TIẾT TUYẾN XE";
            // 
            // LinkLabelLanguague
            // 
            this.LinkLabelLanguague.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LinkLabelLanguague.Location = new System.Drawing.Point(0, 0);
            this.LinkLabelLanguague.Name = "LinkLabelLanguague";
            this.LinkLabelLanguague.Size = new System.Drawing.Size(100, 23);
            this.LinkLabelLanguague.TabIndex = 0;
            // 
            // lblMaSoTuyen
            // 
            // 
            // 
            // 
            this.lblMaSoTuyen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMaSoTuyen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMaSoTuyen.Location = new System.Drawing.Point(39, 75);
            this.lblMaSoTuyen.Name = "lblMaSoTuyen";
            this.lblMaSoTuyen.Size = new System.Drawing.Size(81, 23);
            this.lblMaSoTuyen.TabIndex = 1;
            this.lblMaSoTuyen.Text = "&Mã số tuyến";
            // 
            // cot_gio
            // 
            this.cot_gio.DataPropertyName = "Gio";
            this.cot_gio.HeaderText = "Giờ khởi hành";
            this.cot_gio.Name = "cot_gio";
            // 
            // cot_Ngay
            // 
            this.cot_Ngay.DataPropertyName = "Ngay";
            this.cot_Ngay.HeaderText = "Ngày";
            this.cot_Ngay.Name = "cot_Ngay";
            // 
            // cot_IdTuyen
            // 
            this.cot_IdTuyen.DataPropertyName = "IdThoiDiem";
            this.cot_IdTuyen.HeaderText = "Mã thời điểm";
            this.cot_IdTuyen.Name = "cot_IdTuyen";
            // 
            // cot_Ma_so_tuyen
            // 
            this.cot_Ma_so_tuyen.DataPropertyName = "IdTuyen";
            this.cot_Ma_so_tuyen.HeaderText = "Mã số tuyến";
            this.cot_Ma_so_tuyen.Name = "cot_Ma_so_tuyen";
            // 
            // luoi_Thoi_diem
            // 
            this.luoi_Thoi_diem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.luoi_Thoi_diem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.luoi_Thoi_diem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.luoi_Thoi_diem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.luoi_Thoi_diem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.luoi_Thoi_diem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cot_Ma_so_tuyen,
            this.cot_IdTuyen,
            this.cot_Ngay,
            this.cot_gio});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.luoi_Thoi_diem.DefaultCellStyle = dataGridViewCellStyle14;
            this.luoi_Thoi_diem.EnableHeadersVisualStyles = false;
            this.luoi_Thoi_diem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.luoi_Thoi_diem.Location = new System.Drawing.Point(24, 18);
            this.luoi_Thoi_diem.Name = "luoi_Thoi_diem";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.luoi_Thoi_diem.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.luoi_Thoi_diem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.luoi_Thoi_diem.Size = new System.Drawing.Size(479, 328);
            this.luoi_Thoi_diem.TabIndex = 0;
            // 
            // txt_GioChay
            // 
            this.txt_GioChay.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txt_GioChay.Border.Class = "TextBoxBorder";
            this.txt_GioChay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_GioChay.DisabledBackColor = System.Drawing.Color.White;
            this.txt_GioChay.ForeColor = System.Drawing.Color.Black;
            this.txt_GioChay.Location = new System.Drawing.Point(139, 157);
            this.txt_GioChay.Name = "txt_GioChay";
            this.txt_GioChay.Size = new System.Drawing.Size(124, 21);
            this.txt_GioChay.TabIndex = 12;
            // 
            // date_Chay
            // 
            // 
            // 
            // 
            this.date_Chay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.date_Chay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date_Chay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.date_Chay.ButtonDropDown.Visible = true;
            this.date_Chay.IsPopupCalendarOpen = false;
            this.date_Chay.Location = new System.Drawing.Point(139, 109);
            // 
            // 
            // 
            // 
            // 
            // 
            this.date_Chay.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.date_Chay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date_Chay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.date_Chay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date_Chay.MonthCalendar.DisplayMonth = new System.DateTime(2010, 12, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.date_Chay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.date_Chay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.date_Chay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.date_Chay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date_Chay.MonthCalendar.TodayButtonVisible = true;
            this.date_Chay.Name = "date_Chay";
            this.date_Chay.Size = new System.Drawing.Size(124, 21);
            this.date_Chay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.date_Chay.TabIndex = 11;
            // 
            // LabelX3
            // 
            this.LabelX3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LabelX3.Location = new System.Drawing.Point(20, 157);
            this.LabelX3.Name = "LabelX3";
            this.LabelX3.Size = new System.Drawing.Size(86, 23);
            this.LabelX3.TabIndex = 1;
            this.LabelX3.Text = "Giờ khởi hành";
            // 
            // LabelX2
            // 
            this.LabelX2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LabelX2.Location = new System.Drawing.Point(20, 109);
            this.LabelX2.Name = "LabelX2";
            this.LabelX2.Size = new System.Drawing.Size(48, 23);
            this.LabelX2.TabIndex = 1;
            this.LabelX2.Text = "Ngày";
            // 
            // LabelX4
            // 
            this.LabelX4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LabelX4.Location = new System.Drawing.Point(20, 16);
            this.LabelX4.Name = "LabelX4";
            this.LabelX4.Size = new System.Drawing.Size(87, 23);
            this.LabelX4.TabIndex = 1;
            this.LabelX4.Text = "Mã số tuyến";
            // 
            // LabelX1
            // 
            this.LabelX1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LabelX1.Location = new System.Drawing.Point(20, 65);
            this.LabelX1.Name = "LabelX1";
            this.LabelX1.Size = new System.Drawing.Size(87, 23);
            this.LabelX1.TabIndex = 1;
            this.LabelX1.Text = "Mã thời điểm";
            // 
            // cbo_MaSoTuyen1
            // 
            this.cbo_MaSoTuyen1.DisplayMember = "Text";
            this.cbo_MaSoTuyen1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_MaSoTuyen1.ForeColor = System.Drawing.Color.Black;
            this.cbo_MaSoTuyen1.FormattingEnabled = true;
            this.cbo_MaSoTuyen1.ItemHeight = 16;
            this.cbo_MaSoTuyen1.Location = new System.Drawing.Point(137, 16);
            this.cbo_MaSoTuyen1.Name = "cbo_MaSoTuyen1";
            this.cbo_MaSoTuyen1.Size = new System.Drawing.Size(153, 22);
            this.cbo_MaSoTuyen1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_MaSoTuyen1.TabIndex = 10;
            // 
            // cbo_MaThoiDiem
            // 
            this.cbo_MaThoiDiem.DisplayMember = "Text";
            this.cbo_MaThoiDiem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_MaThoiDiem.ForeColor = System.Drawing.Color.Black;
            this.cbo_MaThoiDiem.FormattingEnabled = true;
            this.cbo_MaThoiDiem.ItemHeight = 16;
            this.cbo_MaThoiDiem.Location = new System.Drawing.Point(137, 65);
            this.cbo_MaThoiDiem.Name = "cbo_MaThoiDiem";
            this.cbo_MaThoiDiem.Size = new System.Drawing.Size(126, 22);
            this.cbo_MaThoiDiem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_MaThoiDiem.TabIndex = 10;
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Xoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Xoa.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btn_Xoa.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Xoa.Location = new System.Drawing.Point(546, 284);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(113, 62);
            this.btn_Xoa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Xoa.TabIndex = 13;
            this.btn_Xoa.Text = "Xóa";
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // btn_thoat
            // 
            this.btn_thoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_thoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_thoat.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btn_thoat.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Location = new System.Drawing.Point(727, 284);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(118, 62);
            this.btn_thoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_thoat.TabIndex = 12;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // GroupPanel1
            // 
            this.GroupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.GroupPanel1.Controls.Add(this.txt_GioChay);
            this.GroupPanel1.Controls.Add(this.date_Chay);
            this.GroupPanel1.Controls.Add(this.LabelX3);
            this.GroupPanel1.Controls.Add(this.LabelX2);
            this.GroupPanel1.Controls.Add(this.LabelX4);
            this.GroupPanel1.Controls.Add(this.LabelX1);
            this.GroupPanel1.Controls.Add(this.cbo_MaSoTuyen1);
            this.GroupPanel1.Controls.Add(this.cbo_MaThoiDiem);
            this.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.GroupPanel1.Location = new System.Drawing.Point(529, 18);
            this.GroupPanel1.Name = "GroupPanel1";
            this.GroupPanel1.Size = new System.Drawing.Size(316, 245);
            // 
            // 
            // 
            this.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.GroupPanel1.Style.BackColorGradientAngle = 90;
            this.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GroupPanel1.Style.BorderBottomWidth = 1;
            this.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GroupPanel1.Style.BorderLeftWidth = 1;
            this.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GroupPanel1.Style.BorderRightWidth = 1;
            this.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GroupPanel1.Style.BorderTopWidth = 1;
            this.GroupPanel1.Style.CornerDiameter = 4;
            this.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.GroupPanel1.TabIndex = 11;
            this.GroupPanel1.Text = "Thời điểm";
            // 
            // btn_HienDanhSach
            // 
            this.btn_HienDanhSach.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_HienDanhSach.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_HienDanhSach.Location = new System.Drawing.Point(543, 75);
            this.btn_HienDanhSach.Name = "btn_HienDanhSach";
            this.btn_HienDanhSach.Size = new System.Drawing.Size(173, 23);
            this.btn_HienDanhSach.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_HienDanhSach.TabIndex = 11;
            this.btn_HienDanhSach.Text = "Hiện tất cả danh sách";
            this.btn_HienDanhSach.Click += new System.EventHandler(this.btn_HienDanhSach_Click);
            // 
            // cbo_TenTuyen
            // 
            this.cbo_TenTuyen.DisplayMember = "Text";
            this.cbo_TenTuyen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_TenTuyen.ForeColor = System.Drawing.Color.Black;
            this.cbo_TenTuyen.FormattingEnabled = true;
            this.cbo_TenTuyen.ItemHeight = 16;
            this.cbo_TenTuyen.Location = new System.Drawing.Point(348, 75);
            this.cbo_TenTuyen.Name = "cbo_TenTuyen";
            this.cbo_TenTuyen.Size = new System.Drawing.Size(147, 22);
            this.cbo_TenTuyen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_TenTuyen.TabIndex = 10;
            // 
            // cbo_MaSoTuyen
            // 
            this.cbo_MaSoTuyen.DisplayMember = "Text";
            this.cbo_MaSoTuyen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_MaSoTuyen.ForeColor = System.Drawing.Color.Black;
            this.cbo_MaSoTuyen.FormattingEnabled = true;
            this.cbo_MaSoTuyen.ItemHeight = 16;
            this.cbo_MaSoTuyen.Location = new System.Drawing.Point(137, 77);
            this.cbo_MaSoTuyen.Name = "cbo_MaSoTuyen";
            this.cbo_MaSoTuyen.Size = new System.Drawing.Size(104, 22);
            this.cbo_MaSoTuyen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_MaSoTuyen.TabIndex = 10;
            this.cbo_MaSoTuyen.SelectedIndexChanged += new System.EventHandler(this.cbo_MaSoTuyen_SelectedIndexChanged);
            // 
            // grb_ThoiDiem
            // 
            this.grb_ThoiDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grb_ThoiDiem.CanvasColor = System.Drawing.SystemColors.Control;
            this.grb_ThoiDiem.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grb_ThoiDiem.Controls.Add(this.btn_Xoa);
            this.grb_ThoiDiem.Controls.Add(this.btn_thoat);
            this.grb_ThoiDiem.Controls.Add(this.GroupPanel1);
            this.grb_ThoiDiem.Controls.Add(this.luoi_Thoi_diem);
            this.grb_ThoiDiem.DisabledBackColor = System.Drawing.Color.Empty;
            this.grb_ThoiDiem.Location = new System.Drawing.Point(12, 104);
            this.grb_ThoiDiem.Name = "grb_ThoiDiem";
            this.grb_ThoiDiem.Size = new System.Drawing.Size(875, 406);
            // 
            // 
            // 
            this.grb_ThoiDiem.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grb_ThoiDiem.Style.BackColorGradientAngle = 90;
            this.grb_ThoiDiem.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grb_ThoiDiem.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grb_ThoiDiem.Style.BorderBottomWidth = 1;
            this.grb_ThoiDiem.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grb_ThoiDiem.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grb_ThoiDiem.Style.BorderLeftWidth = 1;
            this.grb_ThoiDiem.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grb_ThoiDiem.Style.BorderRightWidth = 1;
            this.grb_ThoiDiem.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grb_ThoiDiem.Style.BorderTopWidth = 1;
            this.grb_ThoiDiem.Style.CornerDiameter = 4;
            this.grb_ThoiDiem.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grb_ThoiDiem.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grb_ThoiDiem.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grb_ThoiDiem.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grb_ThoiDiem.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grb_ThoiDiem.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grb_ThoiDiem.TabIndex = 9;
            this.grb_ThoiDiem.Text = "Danh sách thời điểm khởi hành";
            // 
            // lblTenTuyen
            // 
            // 
            // 
            // 
            this.lblTenTuyen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTenTuyen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTenTuyen.Location = new System.Drawing.Point(263, 75);
            this.lblTenTuyen.Name = "lblTenTuyen";
            this.lblTenTuyen.Size = new System.Drawing.Size(68, 23);
            this.lblTenTuyen.TabIndex = 1;
            this.lblTenTuyen.Text = "&Tên tuyến";
            // 
            // Panel_ChiTietTuyen
            // 
            this.Panel_ChiTietTuyen.CanvasColor = System.Drawing.SystemColors.Control;
            this.Panel_ChiTietTuyen.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Panel_ChiTietTuyen.Controls.Add(this.btn_HienDanhSach);
            this.Panel_ChiTietTuyen.Controls.Add(this.cbo_TenTuyen);
            this.Panel_ChiTietTuyen.Controls.Add(this.cbo_MaSoTuyen);
            this.Panel_ChiTietTuyen.Controls.Add(this.grb_ThoiDiem);
            this.Panel_ChiTietTuyen.Controls.Add(this.ReflectionLabel_ChaoMung);
            this.Panel_ChiTietTuyen.Controls.Add(this.LinkLabelLanguague);
            this.Panel_ChiTietTuyen.Controls.Add(this.lblTenTuyen);
            this.Panel_ChiTietTuyen.Controls.Add(this.lblMaSoTuyen);
            this.Panel_ChiTietTuyen.DisabledBackColor = System.Drawing.Color.Empty;
            this.Panel_ChiTietTuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_ChiTietTuyen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Panel_ChiTietTuyen.Location = new System.Drawing.Point(0, 0);
            this.Panel_ChiTietTuyen.Name = "Panel_ChiTietTuyen";
            this.Panel_ChiTietTuyen.Size = new System.Drawing.Size(907, 521);
            this.Panel_ChiTietTuyen.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Panel_ChiTietTuyen.Style.BackColor1.Color = System.Drawing.Color.DeepSkyBlue;
            this.Panel_ChiTietTuyen.Style.BackColor2.Color = System.Drawing.Color.DeepSkyBlue;
            this.Panel_ChiTietTuyen.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Panel_ChiTietTuyen.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Panel_ChiTietTuyen.Style.ForeColor.Color = System.Drawing.Color.Chartreuse;
            this.Panel_ChiTietTuyen.Style.GradientAngle = 90;
            this.Panel_ChiTietTuyen.TabIndex = 7;
            // 
            // Form_ChiTietTuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 521);
            this.Controls.Add(this.Panel_ChiTietTuyen);
            this.Name = "Form_ChiTietTuyen";
            this.Text = "Form_ChiTietTuyen";
            this.Load += new System.EventHandler(this.Form_ChiTietTuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.luoi_Thoi_diem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Chay)).EndInit();
            this.GroupPanel1.ResumeLayout(false);
            this.grb_ThoiDiem.ResumeLayout(false);
            this.Panel_ChiTietTuyen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.ReflectionLabel ReflectionLabel_ChaoMung;
        internal System.Windows.Forms.LinkLabel LinkLabelLanguague;
        internal DevComponents.DotNetBar.LabelX lblMaSoTuyen;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cot_gio;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cot_Ngay;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cot_IdTuyen;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cot_Ma_so_tuyen;
        internal DevComponents.DotNetBar.Controls.DataGridViewX luoi_Thoi_diem;
        internal DevComponents.DotNetBar.Controls.TextBoxX txt_GioChay;
        internal DevComponents.Editors.DateTimeAdv.DateTimeInput date_Chay;
        internal DevComponents.DotNetBar.LabelX LabelX3;
        internal DevComponents.DotNetBar.LabelX LabelX2;
        internal DevComponents.DotNetBar.LabelX LabelX4;
        internal DevComponents.DotNetBar.LabelX LabelX1;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cbo_MaSoTuyen1;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cbo_MaThoiDiem;
        internal DevComponents.DotNetBar.ButtonX btn_Xoa;
        internal DevComponents.DotNetBar.ButtonX btn_thoat;
        internal DevComponents.DotNetBar.Controls.GroupPanel GroupPanel1;
        internal DevComponents.DotNetBar.ButtonX btn_HienDanhSach;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cbo_TenTuyen;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cbo_MaSoTuyen;
        internal DevComponents.DotNetBar.Controls.GroupPanel grb_ThoiDiem;
        internal DevComponents.DotNetBar.LabelX lblTenTuyen;
        internal DevComponents.DotNetBar.PanelEx Panel_ChiTietTuyen;
    }
}