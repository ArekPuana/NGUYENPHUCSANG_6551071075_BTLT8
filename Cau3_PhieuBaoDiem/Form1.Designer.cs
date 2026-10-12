namespace Cau3_PhieuBaoDiem;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label lblLopTitle;
    private System.Windows.Forms.ListBox lstLop;
    private System.Windows.Forms.TextBox txtTimKiem;
    private System.Windows.Forms.DataGridView dgvSinhVien;
    private System.Windows.Forms.GroupBox grpThongTinSV;
    private System.Windows.Forms.Label lblMaSV;
    private System.Windows.Forms.TextBox txtMaSV;
    private System.Windows.Forms.Label lblHoTen;
    private System.Windows.Forms.TextBox txtHoTen;
    private System.Windows.Forms.DateTimePicker dtpNgaySinh;
    private System.Windows.Forms.RadioButton radNam;
    private System.Windows.Forms.RadioButton radNu;
    private System.Windows.Forms.Label lblLop;
    private System.Windows.Forms.ComboBox cboLop;
    private System.Windows.Forms.Button btnThemSV;
    private System.Windows.Forms.Button btnSuaSV;
    private System.Windows.Forms.Button btnXoaSV;

    private System.Windows.Forms.GroupBox grpDiemSV;
    private System.Windows.Forms.DataGridView dgvDiem;
    private System.Windows.Forms.Label lblTenMon;
    private System.Windows.Forms.TextBox txtTenMon;
    private System.Windows.Forms.Label lblSoTC;
    private System.Windows.Forms.NumericUpDown nudSoTC;
    private System.Windows.Forms.Label lblDiem;
    private System.Windows.Forms.NumericUpDown nudDiem;
    private System.Windows.Forms.Button btnThemMon;
    private System.Windows.Forms.Button btnSuaDiem;
    private System.Windows.Forms.Button btnXoaMon;
    private System.Windows.Forms.Label lblTongTinChi;
    private System.Windows.Forms.Label lblDiemTB;
    private System.Windows.Forms.Label lblXepLoai;

    private System.Windows.Forms.Button btnXemTruoc;
    private System.Windows.Forms.Button btnIn;
    private System.Windows.Forms.Button btnCaiDatTrang;
    private System.Windows.Forms.Button btnXuatExcel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dgvDiemHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
        
        lblLopTitle = new System.Windows.Forms.Label();
        lstLop = new System.Windows.Forms.ListBox();
        txtTimKiem = new System.Windows.Forms.TextBox();
        dgvSinhVien = new System.Windows.Forms.DataGridView();
        grpThongTinSV = new System.Windows.Forms.GroupBox();
        lblMaSV = new System.Windows.Forms.Label();
        txtMaSV = new System.Windows.Forms.TextBox();
        lblHoTen = new System.Windows.Forms.Label();
        txtHoTen = new System.Windows.Forms.TextBox();
        dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
        radNam = new System.Windows.Forms.RadioButton();
        radNu = new System.Windows.Forms.RadioButton();
        lblLop = new System.Windows.Forms.Label();
        cboLop = new System.Windows.Forms.ComboBox();
        btnThemSV = new System.Windows.Forms.Button();
        btnSuaSV = new System.Windows.Forms.Button();
        btnXoaSV = new System.Windows.Forms.Button();

        grpDiemSV = new System.Windows.Forms.GroupBox();
        dgvDiem = new System.Windows.Forms.DataGridView();
        lblTenMon = new System.Windows.Forms.Label();
        txtTenMon = new System.Windows.Forms.TextBox();
        lblSoTC = new System.Windows.Forms.Label();
        nudSoTC = new System.Windows.Forms.NumericUpDown();
        lblDiem = new System.Windows.Forms.Label();
        nudDiem = new System.Windows.Forms.NumericUpDown();
        btnThemMon = new System.Windows.Forms.Button();
        btnSuaDiem = new System.Windows.Forms.Button();
        btnXoaMon = new System.Windows.Forms.Button();
        lblTongTinChi = new System.Windows.Forms.Label();
        lblDiemTB = new System.Windows.Forms.Label();
        lblXepLoai = new System.Windows.Forms.Label();

        btnXemTruoc = new System.Windows.Forms.Button();
        btnIn = new System.Windows.Forms.Button();
        btnCaiDatTrang = new System.Windows.Forms.Button();
        btnXuatExcel = new System.Windows.Forms.Button();

        ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
        grpThongTinSV.SuspendLayout();
        grpDiemSV.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDiem).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSoTC).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
        SuspendLayout();

        // 
        // lblLopTitle
        // 
        lblLopTitle.AutoSize = true;
        lblLopTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        lblLopTitle.Location = new System.Drawing.Point(18, 15);
        lblLopTitle.Name = "lblLopTitle";
        lblLopTitle.Size = new System.Drawing.Size(30, 17);
        lblLopTitle.TabIndex = 0;
        lblLopTitle.Text = "Lớp";

        // 
        // lstLop
        // 
        lstLop.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        lstLop.FormattingEnabled = true;
        lstLop.ItemHeight = 17;
        lstLop.Location = new System.Drawing.Point(18, 38);
        lstLop.Name = "lstLop";
        lstLop.Size = new System.Drawing.Size(200, 225);
        lstLop.TabIndex = 1;
        lstLop.SelectedIndexChanged += lstLop_SelectedIndexChanged;

        // 
        // txtTimKiem
        // 
        txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new System.Drawing.Point(232, 12);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm MSSV / họ tên";
        txtTimKiem.Size = new System.Drawing.Size(505, 24);
        txtTimKiem.TabIndex = 2;
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;

        // 
        // dgvSinhVien
        // 
        dgvSinhVien.AllowUserToAddRows = false;
        dgvSinhVien.AllowUserToDeleteRows = false;
        dgvSinhVien.AllowUserToResizeRows = false;
        dgvSinhVien.BackgroundColor = System.Drawing.Color.White;
        dgvSinhVien.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvHeaderStyle.BackColor = System.Drawing.SystemColors.Control;
        dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        dgvHeaderStyle.ForeColor = System.Drawing.SystemColors.WindowText;
        dgvHeaderStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dgvHeaderStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvSinhVien.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
        dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSinhVien.Location = new System.Drawing.Point(232, 42);
        dgvSinhVien.MultiSelect = false;
        dgvSinhVien.Name = "dgvSinhVien";
        dgvSinhVien.ReadOnly = true;
        dgvSinhVien.RowHeadersWidth = 30;
        dgvSinhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvSinhVien.Size = new System.Drawing.Size(505, 221);
        dgvSinhVien.TabIndex = 3;
        dgvSinhVien.SelectionChanged += dgvSinhVien_SelectionChanged;

        // 
        // grpThongTinSV
        // 
        grpThongTinSV.Controls.Add(lblMaSV);
        grpThongTinSV.Controls.Add(txtMaSV);
        grpThongTinSV.Controls.Add(lblHoTen);
        grpThongTinSV.Controls.Add(txtHoTen);
        grpThongTinSV.Controls.Add(dtpNgaySinh);
        grpThongTinSV.Controls.Add(radNam);
        grpThongTinSV.Controls.Add(radNu);
        grpThongTinSV.Controls.Add(lblLop);
        grpThongTinSV.Controls.Add(cboLop);
        grpThongTinSV.Controls.Add(btnThemSV);
        grpThongTinSV.Controls.Add(btnSuaSV);
        grpThongTinSV.Controls.Add(btnXoaSV);
        grpThongTinSV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        grpThongTinSV.Location = new System.Drawing.Point(750, 12);
        grpThongTinSV.Name = "grpThongTinSV";
        grpThongTinSV.Size = new System.Drawing.Size(275, 251);
        grpThongTinSV.TabIndex = 4;
        grpThongTinSV.TabStop = false;
        grpThongTinSV.Text = "Thông tin sinh viên";

        // 
        // lblMaSV
        // 
        lblMaSV.AutoSize = true;
        lblMaSV.Location = new System.Drawing.Point(12, 28);
        lblMaSV.Name = "lblMaSV";
        lblMaSV.Size = new System.Drawing.Size(43, 17);
        lblMaSV.TabIndex = 0;
        lblMaSV.Text = "MSSV";

        // 
        // txtMaSV
        // 
        txtMaSV.Location = new System.Drawing.Point(65, 25);
        txtMaSV.Name = "txtMaSV";
        txtMaSV.Size = new System.Drawing.Size(195, 24);
        txtMaSV.TabIndex = 1;

        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new System.Drawing.Point(12, 60);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new System.Drawing.Size(47, 17);
        lblHoTen.TabIndex = 2;
        lblHoTen.Text = "Họ tên";

        // 
        // txtHoTen
        // 
        txtHoTen.Location = new System.Drawing.Point(65, 57);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new System.Drawing.Size(195, 24);
        txtHoTen.TabIndex = 3;

        // 
        // dtpNgaySinh
        // 
        dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
        dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpNgaySinh.Location = new System.Drawing.Point(65, 89);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.Size = new System.Drawing.Size(195, 24);
        dtpNgaySinh.TabIndex = 4;

        // 
        // radNam
        // 
        radNam.AutoSize = true;
        radNam.Checked = true;
        radNam.Location = new System.Drawing.Point(65, 122);
        radNam.Name = "radNam";
        radNam.Size = new System.Drawing.Size(54, 21);
        radNam.TabIndex = 5;
        radNam.TabStop = true;
        radNam.Text = "Nam";
        radNam.UseVisualStyleBackColor = true;

        // 
        // radNu
        // 
        radNu.AutoSize = true;
        radNu.Location = new System.Drawing.Point(140, 122);
        radNu.Name = "radNu";
        radNu.Size = new System.Drawing.Size(44, 21);
        radNu.TabIndex = 6;
        radNu.Text = "Nữ";
        radNu.UseVisualStyleBackColor = true;

        // 
        // lblLop
        // 
        lblLop.AutoSize = true;
        lblLop.Location = new System.Drawing.Point(12, 155);
        lblLop.Name = "lblLop";
        lblLop.Size = new System.Drawing.Size(30, 17);
        lblLop.TabIndex = 7;
        lblLop.Text = "Lớp";

        // 
        // cboLop
        // 
        cboLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboLop.FormattingEnabled = true;
        cboLop.Location = new System.Drawing.Point(65, 152);
        cboLop.Name = "cboLop";
        cboLop.Size = new System.Drawing.Size(195, 25);
        cboLop.TabIndex = 8;

        // 
        // btnThemSV
        // 
        btnThemSV.Location = new System.Drawing.Point(12, 198);
        btnThemSV.Name = "btnThemSV";
        btnThemSV.Size = new System.Drawing.Size(76, 32);
        btnThemSV.TabIndex = 9;
        btnThemSV.Text = "Thêm";
        btnThemSV.UseVisualStyleBackColor = true;
        btnThemSV.Click += btnThemSV_Click;

        // 
        // btnSuaSV
        // 
        btnSuaSV.Location = new System.Drawing.Point(98, 198);
        btnSuaSV.Name = "btnSuaSV";
        btnSuaSV.Size = new System.Drawing.Size(76, 32);
        btnSuaSV.TabIndex = 10;
        btnSuaSV.Text = "Sửa";
        btnSuaSV.UseVisualStyleBackColor = true;
        btnSuaSV.Click += btnSuaSV_Click;

        // 
        // btnXoaSV
        // 
        btnXoaSV.Location = new System.Drawing.Point(184, 198);
        btnXoaSV.Name = "btnXoaSV";
        btnXoaSV.Size = new System.Drawing.Size(76, 32);
        btnXoaSV.TabIndex = 11;
        btnXoaSV.Text = "Xóa";
        btnXoaSV.UseVisualStyleBackColor = true;
        btnXoaSV.Click += btnXoaSV_Click;

        // 
        // grpDiemSV
        // 
        grpDiemSV.Controls.Add(dgvDiem);
        grpDiemSV.Controls.Add(lblTenMon);
        grpDiemSV.Controls.Add(txtTenMon);
        grpDiemSV.Controls.Add(lblSoTC);
        grpDiemSV.Controls.Add(nudSoTC);
        grpDiemSV.Controls.Add(lblDiem);
        grpDiemSV.Controls.Add(nudDiem);
        grpDiemSV.Controls.Add(btnThemMon);
        grpDiemSV.Controls.Add(btnSuaDiem);
        grpDiemSV.Controls.Add(btnXoaMon);
        grpDiemSV.Controls.Add(lblTongTinChi);
        grpDiemSV.Controls.Add(lblDiemTB);
        grpDiemSV.Controls.Add(lblXepLoai);
        grpDiemSV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        grpDiemSV.Location = new System.Drawing.Point(18, 275);
        grpDiemSV.Name = "grpDiemSV";
        grpDiemSV.Size = new System.Drawing.Size(1007, 260);
        grpDiemSV.TabIndex = 5;
        grpDiemSV.TabStop = false;
        grpDiemSV.Text = "Điểm của sinh viên";

        // 
        // dgvDiem
        // 
        dgvDiem.AllowUserToAddRows = false;
        dgvDiem.AllowUserToDeleteRows = false;
        dgvDiem.AllowUserToResizeRows = false;
        dgvDiem.BackgroundColor = System.Drawing.Color.White;
        dgvDiem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        dgvDiemHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dgvDiemHeaderStyle.BackColor = System.Drawing.SystemColors.Control;
        dgvDiemHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        dgvDiemHeaderStyle.ForeColor = System.Drawing.SystemColors.WindowText;
        dgvDiemHeaderStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dgvDiemHeaderStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dgvDiemHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvDiem.ColumnHeadersDefaultCellStyle = dgvDiemHeaderStyle;
        dgvDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDiem.Location = new System.Drawing.Point(12, 25);
        dgvDiem.MultiSelect = false;
        dgvDiem.Name = "dgvDiem";
        dgvDiem.ReadOnly = true;
        dgvDiem.RowHeadersWidth = 30;
        dgvDiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvDiem.Size = new System.Drawing.Size(490, 220);
        dgvDiem.TabIndex = 0;
        dgvDiem.SelectionChanged += dgvDiem_SelectionChanged;

        // 
        // lblTenMon
        // 
        lblTenMon.AutoSize = true;
        lblTenMon.Location = new System.Drawing.Point(520, 28);
        lblTenMon.Name = "lblTenMon";
        lblTenMon.Size = new System.Drawing.Size(58, 17);
        lblTenMon.TabIndex = 1;
        lblTenMon.Text = "Tên môn";

        // 
        // txtTenMon
        // 
        txtTenMon.Location = new System.Drawing.Point(520, 50);
        txtTenMon.Name = "txtTenMon";
        txtTenMon.Size = new System.Drawing.Size(255, 24);
        txtTenMon.TabIndex = 2;

        // 
        // lblSoTC
        // 
        lblSoTC.AutoSize = true;
        lblSoTC.Location = new System.Drawing.Point(795, 28);
        lblSoTC.Name = "lblSoTC";
        lblSoTC.Size = new System.Drawing.Size(42, 17);
        lblSoTC.TabIndex = 3;
        lblSoTC.Text = "Số TC";

        // 
        // nudSoTC
        // 
        nudSoTC.Location = new System.Drawing.Point(795, 50);
        nudSoTC.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
        nudSoTC.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoTC.Name = "nudSoTC";
        nudSoTC.Size = new System.Drawing.Size(80, 24);
        nudSoTC.TabIndex = 4;
        nudSoTC.Value = new decimal(new int[] { 3, 0, 0, 0 });

        // 
        // lblDiem
        // 
        lblDiem.AutoSize = true;
        lblDiem.Location = new System.Drawing.Point(895, 28);
        lblDiem.Name = "lblDiem";
        lblDiem.Size = new System.Drawing.Size(38, 17);
        lblDiem.TabIndex = 5;
        lblDiem.Text = "Điểm";

        // 
        // nudDiem
        // 
        nudDiem.DecimalPlaces = 1;
        nudDiem.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        nudDiem.Location = new System.Drawing.Point(895, 50);
        nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        nudDiem.Name = "nudDiem";
        nudDiem.Size = new System.Drawing.Size(85, 24);
        nudDiem.TabIndex = 6;
        nudDiem.Value = new decimal(new int[] { 8, 0, 0, 0 });

        // 
        // btnThemMon
        // 
        btnThemMon.Location = new System.Drawing.Point(520, 95);
        btnThemMon.Name = "btnThemMon";
        btnThemMon.Size = new System.Drawing.Size(95, 34);
        btnThemMon.TabIndex = 7;
        btnThemMon.Text = "Thêm môn";
        btnThemMon.UseVisualStyleBackColor = true;
        btnThemMon.Click += btnThemMon_Click;

        // 
        // btnSuaDiem
        // 
        btnSuaDiem.Location = new System.Drawing.Point(630, 95);
        btnSuaDiem.Name = "btnSuaDiem";
        btnSuaDiem.Size = new System.Drawing.Size(95, 34);
        btnSuaDiem.TabIndex = 8;
        btnSuaDiem.Text = "Sửa điểm";
        btnSuaDiem.UseVisualStyleBackColor = true;
        btnSuaDiem.Click += btnSuaDiem_Click;

        // 
        // btnXoaMon
        // 
        btnXoaMon.Location = new System.Drawing.Point(740, 95);
        btnXoaMon.Name = "btnXoaMon";
        btnXoaMon.Size = new System.Drawing.Size(95, 34);
        btnXoaMon.TabIndex = 9;
        btnXoaMon.Text = "Xóa môn";
        btnXoaMon.UseVisualStyleBackColor = true;
        btnXoaMon.Click += btnXoaMon_Click;

        // 
        // lblTongTinChi
        // 
        lblTongTinChi.AutoSize = true;
        lblTongTinChi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblTongTinChi.Location = new System.Drawing.Point(520, 190);
        lblTongTinChi.Name = "lblTongTinChi";
        lblTongTinChi.Size = new System.Drawing.Size(126, 21);
        lblTongTinChi.TabIndex = 10;
        lblTongTinChi.Text = "Tổng tín chỉ: 11";

        // 
        // lblDiemTB
        // 
        lblDiemTB.AutoSize = true;
        lblDiemTB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblDiemTB.Location = new System.Drawing.Point(695, 190);
        lblDiemTB.Name = "lblDiemTB";
        lblDiemTB.Size = new System.Drawing.Size(107, 21);
        lblDiemTB.TabIndex = 11;
        lblDiemTB.Text = "Điểm TB: 7,77";

        // 
        // lblXepLoai
        // 
        lblXepLoai.AutoSize = true;
        lblXepLoai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblXepLoai.Location = new System.Drawing.Point(850, 190);
        lblXepLoai.Name = "lblXepLoai";
        lblXepLoai.Size = new System.Drawing.Size(117, 21);
        lblXepLoai.TabIndex = 12;
        lblXepLoai.Text = "Xếp loại: Khá";

        // 
        // btnXemTruoc
        // 
        btnXemTruoc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        btnXemTruoc.Location = new System.Drawing.Point(18, 550);
        btnXemTruoc.Name = "btnXemTruoc";
        btnXemTruoc.Size = new System.Drawing.Size(125, 34);
        btnXemTruoc.TabIndex = 6;
        btnXemTruoc.Text = "Xem trước phiếu";
        btnXemTruoc.UseVisualStyleBackColor = true;
        btnXemTruoc.Click += btnXemTruoc_Click;

        // 
        // btnIn
        // 
        btnIn.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        btnIn.Location = new System.Drawing.Point(155, 550);
        btnIn.Name = "btnIn";
        btnIn.Size = new System.Drawing.Size(100, 34);
        btnIn.TabIndex = 7;
        btnIn.Text = "In...";
        btnIn.UseVisualStyleBackColor = true;
        btnIn.Click += btnIn_Click;

        // 
        // btnCaiDatTrang
        // 
        btnCaiDatTrang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        btnCaiDatTrang.Location = new System.Drawing.Point(267, 550);
        btnCaiDatTrang.Name = "btnCaiDatTrang";
        btnCaiDatTrang.Size = new System.Drawing.Size(115, 34);
        btnCaiDatTrang.TabIndex = 8;
        btnCaiDatTrang.Text = "Cài đặt trang...";
        btnCaiDatTrang.UseVisualStyleBackColor = true;
        btnCaiDatTrang.Click += btnCaiDatTrang_Click;

        // 
        // btnXuatExcel
        // 
        btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        btnXuatExcel.Location = new System.Drawing.Point(395, 550);
        btnXuatExcel.Name = "btnXuatExcel";
        btnXuatExcel.Size = new System.Drawing.Size(145, 34);
        btnXuatExcel.TabIndex = 9;
        btnXuatExcel.Text = "Xuất Excel điểm lớp";
        btnXuatExcel.UseVisualStyleBackColor = true;
        btnXuatExcel.Click += btnXuatExcel_Click;

        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1040, 600);
        Controls.Add(lblLopTitle);
        Controls.Add(lstLop);
        Controls.Add(txtTimKiem);
        Controls.Add(dgvSinhVien);
        Controls.Add(grpThongTinSV);
        Controls.Add(grpDiemSV);
        Controls.Add(btnXemTruoc);
        Controls.Add(btnIn);
        Controls.Add(btnCaiDatTrang);
        Controls.Add(btnXuatExcel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Quản lý sinh viên và điểm";
        Load += Form1_Load;

        ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
        grpThongTinSV.ResumeLayout(false);
        grpThongTinSV.PerformLayout();
        grpDiemSV.ResumeLayout(false);
        grpDiemSV.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDiem).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSoTC).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
