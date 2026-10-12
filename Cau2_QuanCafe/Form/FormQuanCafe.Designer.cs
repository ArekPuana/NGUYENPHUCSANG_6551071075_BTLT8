namespace Cau2_QuanCafe.Forms
{
    partial class FormQuanCafe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle styleMaHD = new DataGridViewCellStyle();
            DataGridViewCellStyle styleNgayLap = new DataGridViewCellStyle();
            DataGridViewCellStyle styleSoBan = new DataGridViewCellStyle();
            DataGridViewCellStyle styleTongTien = new DataGridViewCellStyle();
            DataGridViewCellStyle styleSTT = new DataGridViewCellStyle();
            DataGridViewCellStyle styleSL = new DataGridViewCellStyle();
            DataGridViewCellStyle styleDonGia = new DataGridViewCellStyle();
            DataGridViewCellStyle styleThanhTien = new DataGridViewCellStyle();

            grpTimKiem = new GroupBox();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblGachNgang = new Label();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            lblLocSoBan = new Label();
            cboLocSoBan = new ComboBox();
            lblLocHinhThuc = new Label();
            cboLocHinhThuc = new ComboBox();
            lblLocTenMon = new Label();
            txtLocTenMon = new TextBox();
            btnTim = new Button();

            grpDanhSach = new GroupBox();
            dgvHoaDon = new DataGridView();
            colMaHD = new DataGridViewTextBoxColumn();
            colNgayLap = new DataGridViewTextBoxColumn();
            colSoBan = new DataGridViewTextBoxColumn();
            colHinhThucTT = new DataGridViewTextBoxColumn();
            colTongTien = new DataGridViewTextBoxColumn();
            lblTongDoanhThu = new Label();

            grpChiTiet = new GroupBox();
            dtpNgayLap = new DateTimePicker();
            lblSoBan = new Label();
            cboSoBan = new ComboBox();
            radTienMat = new RadioButton();
            radChuyenKhoan = new RadioButton();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            lblChonMon = new Label();
            cboMon = new ComboBox();
            lblSL = new Label();
            nudSoLuong = new NumericUpDown();
            btnThemMon = new Button();
            dgvChiTiet = new DataGridView();
            colSTT = new DataGridViewTextBoxColumn();
            colTenMon = new DataGridViewTextBoxColumn();
            colSL = new DataGridViewTextBoxColumn();
            colDonGia = new DataGridViewTextBoxColumn();
            colThanhTien = new DataGridViewTextBoxColumn();
            lblTongCong = new Label();

            btnHoaDonMoi = new Button();
            btnSuaHoaDon = new Button();
            btnXoaHoaDon = new Button();
            btnSuaSoLuong = new Button();
            btnXoaMon = new Button();
            btnXemHoaDon = new Button();
            btnXuatPdf = new Button();
            btnIn = new Button();

            grpTimKiem.SuspendLayout();
            grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();

            // 
            // grpTimKiem
            // 
            grpTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTimKiem.Controls.Add(btnTim);
            grpTimKiem.Controls.Add(txtLocTenMon);
            grpTimKiem.Controls.Add(lblLocTenMon);
            grpTimKiem.Controls.Add(cboLocHinhThuc);
            grpTimKiem.Controls.Add(lblLocHinhThuc);
            grpTimKiem.Controls.Add(cboLocSoBan);
            grpTimKiem.Controls.Add(lblLocSoBan);
            grpTimKiem.Controls.Add(dtpDenNgay);
            grpTimKiem.Controls.Add(lblDenNgay);
            grpTimKiem.Controls.Add(lblGachNgang);
            grpTimKiem.Controls.Add(dtpTuNgay);
            grpTimKiem.Controls.Add(lblTuNgay);
            grpTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            grpTimKiem.Location = new Point(12, 12);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Size = new Size(1010, 75);
            grpTimKiem.TabIndex = 0;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = "Tìm kiếm hóa đơn";

            // lblTuNgay
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(15, 20);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(50, 15);
            lblTuNgay.TabIndex = 0;
            lblTuNgay.Text = "Từ ngày";

            // dtpTuNgay
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(15, 38);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(115, 23);
            dtpTuNgay.TabIndex = 1;

            // lblGachNgang
            lblGachNgang.AutoSize = true;
            lblGachNgang.Location = new Point(135, 41);
            lblGachNgang.Name = "lblGachNgang";
            lblGachNgang.Size = new Size(12, 15);
            lblGachNgang.TabIndex = 2;
            lblGachNgang.Text = "-";

            // lblDenNgay
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(152, 20);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(58, 15);
            lblDenNgay.TabIndex = 3;
            lblDenNgay.Text = "Đến ngày";

            // dtpDenNgay
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(152, 38);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(115, 23);
            dtpDenNgay.TabIndex = 4;

            // lblLocSoBan
            lblLocSoBan.AutoSize = true;
            lblLocSoBan.Location = new Point(285, 20);
            lblLocSoBan.Name = "lblLocSoBan";
            lblLocSoBan.Size = new Size(43, 15);
            lblLocSoBan.TabIndex = 5;
            lblLocSoBan.Text = "Số bàn";

            // cboLocSoBan
            cboLocSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocSoBan.FormattingEnabled = true;
            cboLocSoBan.Location = new Point(285, 38);
            cboLocSoBan.Name = "cboLocSoBan";
            cboLocSoBan.Size = new Size(110, 23);
            cboLocSoBan.TabIndex = 6;

            // lblLocHinhThuc
            lblLocHinhThuc.AutoSize = true;
            lblLocHinhThuc.Location = new Point(415, 20);
            lblLocHinhThuc.Name = "lblLocHinhThuc";
            lblLocHinhThuc.Size = new Size(59, 15);
            lblLocHinhThuc.TabIndex = 7;
            lblLocHinhThuc.Text = "Hình thức";

            // cboLocHinhThuc
            cboLocHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocHinhThuc.FormattingEnabled = true;
            cboLocHinhThuc.Location = new Point(415, 38);
            cboLocHinhThuc.Name = "cboLocHinhThuc";
            cboLocHinhThuc.Size = new Size(125, 23);
            cboLocHinhThuc.TabIndex = 8;

            // lblLocTenMon
            lblLocTenMon.AutoSize = true;
            lblLocTenMon.Location = new Point(560, 20);
            lblLocTenMon.Name = "lblLocTenMon";
            lblLocTenMon.Size = new Size(53, 15);
            lblLocTenMon.TabIndex = 9;
            lblLocTenMon.Text = "Tên món";

            // txtLocTenMon
            txtLocTenMon.Location = new Point(560, 38);
            txtLocTenMon.Name = "txtLocTenMon";
            txtLocTenMon.Size = new Size(220, 23);
            txtLocTenMon.TabIndex = 10;

            // btnTim
            btnTim.Location = new Point(800, 36);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(80, 26);
            btnTim.TabIndex = 11;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;

            // 
            // grpDanhSach
            // 
            grpDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpDanhSach.Controls.Add(dgvHoaDon);
            grpDanhSach.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            grpDanhSach.Location = new Point(12, 93);
            grpDanhSach.Name = "grpDanhSach";
            grpDanhSach.Size = new Size(480, 420);
            grpDanhSach.TabIndex = 1;
            grpDanhSach.TabStop = false;
            grpDanhSach.Text = "Danh sách hóa đơn";

            // dgvHoaDon
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon.BackgroundColor = SystemColors.Window;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon.Columns.AddRange(new DataGridViewColumn[] { colMaHD, colNgayLap, colSoBan, colHinhThucTT, colTongTien });
            dgvHoaDon.Location = new Point(10, 22);
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.RowTemplate.Height = 25;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.Size = new Size(460, 388);
            dgvHoaDon.TabIndex = 0;

            // colMaHD
            styleMaHD.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHD.DataPropertyName = "MaHienThi";
            colMaHD.DefaultCellStyle = styleMaHD;
            colMaHD.FillWeight = 85F;
            colMaHD.HeaderText = "Mã HD";
            colMaHD.Name = "colMaHD";
            colMaHD.ReadOnly = true;

            // colNgayLap
            styleNgayLap.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayLap.DataPropertyName = "NgayLap";
            colNgayLap.DefaultCellStyle = styleNgayLap;
            colNgayLap.FillWeight = 125F;
            colNgayLap.HeaderText = "Ngày lập";
            colNgayLap.Name = "colNgayLap";
            colNgayLap.ReadOnly = true;

            // colSoBan
            styleSoBan.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoBan.DataPropertyName = "SoBan";
            colSoBan.DefaultCellStyle = styleSoBan;
            colSoBan.FillWeight = 55F;
            colSoBan.HeaderText = "Số bàn";
            colSoBan.Name = "colSoBan";
            colSoBan.ReadOnly = true;

            // colHinhThucTT
            colHinhThucTT.DataPropertyName = "HinhThucTT";
            colHinhThucTT.FillWeight = 100F;
            colHinhThucTT.HeaderText = "Hình thức TT";
            colHinhThucTT.Name = "colHinhThucTT";
            colHinhThucTT.ReadOnly = true;

            // colTongTien
            styleTongTien.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleTongTien.Format = "N0";
            colTongTien.DataPropertyName = "TongTien";
            colTongTien.DefaultCellStyle = styleTongTien;
            colTongTien.FillWeight = 95F;
            colTongTien.HeaderText = "Tổng tiền";
            colTongTien.Name = "colTongTien";
            colTongTien.ReadOnly = true;

            // 
            // lblTongDoanhThu
            // 
            lblTongDoanhThu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongDoanhThu.AutoSize = true;
            lblTongDoanhThu.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lblTongDoanhThu.Location = new Point(12, 522);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(430, 28);
            lblTongDoanhThu.TabIndex = 2;
            lblTongDoanhThu.Text = "Tổng doanh thu đang hiển thị: 0 đ";

            // 
            // grpChiTiet
            // 
            grpChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpChiTiet.Controls.Add(dgvChiTiet);
            grpChiTiet.Controls.Add(btnThemMon);
            grpChiTiet.Controls.Add(nudSoLuong);
            grpChiTiet.Controls.Add(lblSL);
            grpChiTiet.Controls.Add(cboMon);
            grpChiTiet.Controls.Add(lblChonMon);
            grpChiTiet.Controls.Add(txtGhiChu);
            grpChiTiet.Controls.Add(lblGhiChu);
            grpChiTiet.Controls.Add(radChuyenKhoan);
            grpChiTiet.Controls.Add(radTienMat);
            grpChiTiet.Controls.Add(cboSoBan);
            grpChiTiet.Controls.Add(lblSoBan);
            grpChiTiet.Controls.Add(dtpNgayLap);
            grpChiTiet.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            grpChiTiet.Location = new Point(505, 93);
            grpChiTiet.Name = "grpChiTiet";
            grpChiTiet.Size = new Size(517, 420);
            grpChiTiet.TabIndex = 3;
            grpChiTiet.TabStop = false;
            grpChiTiet.Text = "Chi tiết hóa đơn";

            // dtpNgayLap
            dtpNgayLap.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayLap.Format = DateTimePickerFormat.Custom;
            dtpNgayLap.Location = new Point(15, 22);
            dtpNgayLap.Name = "dtpNgayLap";
            dtpNgayLap.Size = new Size(145, 23);
            dtpNgayLap.TabIndex = 0;

            // lblSoBan
            lblSoBan.AutoSize = true;
            lblSoBan.Location = new Point(168, 25);
            lblSoBan.Name = "lblSoBan";
            lblSoBan.Size = new Size(46, 15);
            lblSoBan.TabIndex = 1;
            lblSoBan.Text = "Số bàn:";

            // cboSoBan
            cboSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSoBan.FormattingEnabled = true;
            cboSoBan.Location = new Point(218, 22);
            cboSoBan.Name = "cboSoBan";
            cboSoBan.Size = new Size(55, 23);
            cboSoBan.TabIndex = 2;

            // radTienMat
            radTienMat.AutoSize = true;
            radTienMat.Checked = true;
            radTienMat.Location = new Point(285, 23);
            radTienMat.Name = "radTienMat";
            radTienMat.Size = new Size(71, 19);
            radTienMat.TabIndex = 3;
            radTienMat.TabStop = true;
            radTienMat.Text = "Tiền mặt";
            radTienMat.UseVisualStyleBackColor = true;

            // radChuyenKhoan
            radChuyenKhoan.AutoSize = true;
            radChuyenKhoan.Location = new Point(365, 23);
            radChuyenKhoan.Name = "radChuyenKhoan";
            radChuyenKhoan.Size = new Size(102, 19);
            radChuyenKhoan.TabIndex = 4;
            radChuyenKhoan.Text = "Chuyển khoản";
            radChuyenKhoan.UseVisualStyleBackColor = true;

            // lblGhiChu
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(15, 55);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(51, 15);
            lblGhiChu.TabIndex = 5;
            lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            txtGhiChu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtGhiChu.Location = new Point(85, 52);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            txtGhiChu.Size = new Size(415, 42);
            txtGhiChu.TabIndex = 6;

            // lblChonMon
            lblChonMon.AutoSize = true;
            lblChonMon.Location = new Point(15, 107);
            lblChonMon.Name = "lblChonMon";
            lblChonMon.Size = new Size(66, 15);
            lblChonMon.TabIndex = 7;
            lblChonMon.Text = "Chọn món:";

            // cboMon
            cboMon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboMon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMon.FormattingEnabled = true;
            cboMon.Location = new Point(85, 104);
            cboMon.Name = "cboMon";
            cboMon.Size = new Size(205, 23);
            cboMon.TabIndex = 8;

            // lblSL
            lblSL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSL.AutoSize = true;
            lblSL.Location = new Point(298, 107);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(22, 15);
            lblSL.TabIndex = 9;
            lblSL.Text = "SL:";

            // nudSoLuong
            nudSoLuong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudSoLuong.Location = new Point(325, 104);
            nudSoLuong.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(50, 23);
            nudSoLuong.TabIndex = 10;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // btnThemMon
            btnThemMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThemMon.Location = new Point(390, 102);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new Size(85, 26);
            btnThemMon.TabIndex = 11;
            btnThemMon.Text = "Thêm món";
            btnThemMon.UseVisualStyleBackColor = true;

            // dgvChiTiet
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.BackgroundColor = SystemColors.Window;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { colSTT, colTenMon, colSL, colDonGia, colThanhTien });
            dgvChiTiet.Location = new Point(15, 137);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowTemplate.Height = 25;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(485, 273);
            dgvChiTiet.TabIndex = 12;

            // colSTT
            styleSTT.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSTT.DataPropertyName = "STT";
            colSTT.DefaultCellStyle = styleSTT;
            colSTT.FillWeight = 45F;
            colSTT.HeaderText = "STT";
            colSTT.Name = "colSTT";
            colSTT.ReadOnly = true;

            // colTenMon
            colTenMon.DataPropertyName = "TenMon";
            colTenMon.FillWeight = 165F;
            colTenMon.HeaderText = "Tên món";
            colTenMon.Name = "colTenMon";
            colTenMon.ReadOnly = true;

            // colSL
            styleSL.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSL.DataPropertyName = "SoLuong";
            colSL.DefaultCellStyle = styleSL;
            colSL.FillWeight = 45F;
            colSL.HeaderText = "SL";
            colSL.Name = "colSL";
            colSL.ReadOnly = true;

            // colDonGia
            styleDonGia.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleDonGia.Format = "N0";
            colDonGia.DataPropertyName = "DonGia";
            colDonGia.DefaultCellStyle = styleDonGia;
            colDonGia.FillWeight = 90F;
            colDonGia.HeaderText = "Đơn giá";
            colDonGia.Name = "colDonGia";
            colDonGia.ReadOnly = true;

            // colThanhTien
            styleThanhTien.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleThanhTien.Format = "N0";
            colThanhTien.DataPropertyName = "ThanhTien";
            colThanhTien.DefaultCellStyle = styleThanhTien;
            colThanhTien.FillWeight = 95F;
            colThanhTien.HeaderText = "Thành tiền";
            colThanhTien.Name = "colThanhTien";
            colThanhTien.ReadOnly = true;

            // 
            // lblTongCong
            // 
            lblTongCong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTongCong.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTongCong.Location = new Point(620, 520);
            lblTongCong.Name = "lblTongCong";
            lblTongCong.Size = new Size(395, 30);
            lblTongCong.TabIndex = 4;
            lblTongCong.Text = "Tổng cộng: 0 đ";
            lblTongCong.TextAlign = ContentAlignment.MiddleRight;

            // 
            // btnHoaDonMoi
            // 
            btnHoaDonMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnHoaDonMoi.Location = new Point(12, 560);
            btnHoaDonMoi.Name = "btnHoaDonMoi";
            btnHoaDonMoi.Size = new Size(100, 30);
            btnHoaDonMoi.TabIndex = 5;
            btnHoaDonMoi.Text = "Hóa đơn mới";
            btnHoaDonMoi.UseVisualStyleBackColor = true;

            // 
            // btnSuaHoaDon
            // 
            btnSuaHoaDon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSuaHoaDon.Location = new Point(120, 560);
            btnSuaHoaDon.Name = "btnSuaHoaDon";
            btnSuaHoaDon.Size = new Size(100, 30);
            btnSuaHoaDon.TabIndex = 6;
            btnSuaHoaDon.Text = "Sửa hóa đơn";
            btnSuaHoaDon.UseVisualStyleBackColor = true;

            // 
            // btnXoaHoaDon
            // 
            btnXoaHoaDon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaHoaDon.Location = new Point(228, 560);
            btnXoaHoaDon.Name = "btnXoaHoaDon";
            btnXoaHoaDon.Size = new Size(100, 30);
            btnXoaHoaDon.TabIndex = 7;
            btnXoaHoaDon.Text = "Xóa hóa đơn";
            btnXoaHoaDon.UseVisualStyleBackColor = true;

            // 
            // btnSuaSoLuong
            // 
            btnSuaSoLuong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSuaSoLuong.Location = new Point(350, 560);
            btnSuaSoLuong.Name = "btnSuaSoLuong";
            btnSuaSoLuong.Size = new Size(85, 30);
            btnSuaSoLuong.TabIndex = 8;
            btnSuaSoLuong.Text = "Sửa SL";
            btnSuaSoLuong.UseVisualStyleBackColor = true;

            // 
            // btnXoaMon
            // 
            btnXoaMon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoaMon.Location = new Point(540, 560);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new Size(90, 30);
            btnXoaMon.TabIndex = 9;
            btnXoaMon.Text = "Xóa món";
            btnXoaMon.UseVisualStyleBackColor = true;

            // 
            // btnXemHoaDon
            // 
            btnXemHoaDon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXemHoaDon.Location = new Point(650, 560);
            btnXemHoaDon.Name = "btnXemHoaDon";
            btnXemHoaDon.Size = new Size(105, 30);
            btnXemHoaDon.TabIndex = 10;
            btnXemHoaDon.Text = "Xem hóa đơn";
            btnXemHoaDon.UseVisualStyleBackColor = true;

            // 
            // btnXuatPdf
            // 
            btnXuatPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXuatPdf.Location = new Point(775, 560);
            btnXuatPdf.Name = "btnXuatPdf";
            btnXuatPdf.Size = new Size(105, 30);
            btnXuatPdf.TabIndex = 11;
            btnXuatPdf.Text = "Xuất PDF";
            btnXuatPdf.UseVisualStyleBackColor = true;

            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnIn.Location = new Point(900, 560);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(95, 30);
            btnIn.TabIndex = 12;
            btnIn.Text = "In";
            btnIn.UseVisualStyleBackColor = true;

            // 
            // FormQuanCafe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 606);
            Controls.Add(btnIn);
            Controls.Add(btnXuatPdf);
            Controls.Add(btnXemHoaDon);
            Controls.Add(btnXoaMon);
            Controls.Add(btnSuaSoLuong);
            Controls.Add(btnXoaHoaDon);
            Controls.Add(btnSuaHoaDon);
            Controls.Add(btnHoaDonMoi);
            Controls.Add(lblTongCong);
            Controls.Add(grpChiTiet);
            Controls.Add(lblTongDoanhThu);
            Controls.Add(grpDanhSach);
            Controls.Add(grpTimKiem);
            MinimumSize = new Size(980, 580);
            Name = "FormQuanCafe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quán cà phê Góc Phố - Quản lý hóa đơn";
            grpTimKiem.ResumeLayout(false);
            grpTimKiem.PerformLayout();
            grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            grpChiTiet.ResumeLayout(false);
            grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpTimKiem;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblGachNgang;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Label lblLocSoBan;
        private ComboBox cboLocSoBan;
        private Label lblLocHinhThuc;
        private ComboBox cboLocHinhThuc;
        private Label lblLocTenMon;
        private TextBox txtLocTenMon;
        private Button btnTim;

        private GroupBox grpDanhSach;
        private DataGridView dgvHoaDon;
        private DataGridViewTextBoxColumn colMaHD;
        private DataGridViewTextBoxColumn colNgayLap;
        private DataGridViewTextBoxColumn colSoBan;
        private DataGridViewTextBoxColumn colHinhThucTT;
        private DataGridViewTextBoxColumn colTongTien;
        private Label lblTongDoanhThu;

        private GroupBox grpChiTiet;
        private DateTimePicker dtpNgayLap;
        private Label lblSoBan;
        private ComboBox cboSoBan;
        private RadioButton radTienMat;
        private RadioButton radChuyenKhoan;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private Label lblChonMon;
        private ComboBox cboMon;
        private Label lblSL;
        private NumericUpDown nudSoLuong;
        private Button btnThemMon;
        private DataGridView dgvChiTiet;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenMon;
        private DataGridViewTextBoxColumn colSL;
        private DataGridViewTextBoxColumn colDonGia;
        private DataGridViewTextBoxColumn colThanhTien;
        private Label lblTongCong;

        private Button btnHoaDonMoi;
        private Button btnSuaHoaDon;
        private Button btnXoaHoaDon;
        private Button btnSuaSoLuong;
        private Button btnXoaMon;
        private Button btnXemHoaDon;
        private Button btnXuatPdf;
        private Button btnIn;
    }
}
