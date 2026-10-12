namespace Cau1_TiemTapHoa.Forms
{
    partial class FormHangHoa
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle styleGiaBan = new DataGridViewCellStyle();
            DataGridViewCellStyle styleSoLuong = new DataGridViewCellStyle();
            DataGridViewCellStyle styleNgayNhap = new DataGridViewCellStyle();
            grpThongTin = new GroupBox();
            lblTenHang = new Label();
            txtTenHang = new TextBox();
            lblLoaiHang = new Label();
            cboLoaiHang = new ComboBox();
            lblDonVi = new Label();
            cboDonVi = new ComboBox();
            lblGiaBan = new Label();
            nudGiaBan = new NumericUpDown();
            lblSoLuongTon = new Label();
            nudSoLuongTon = new NumericUpDown();
            lblNgayNhap = new Label();
            dtpNgayNhap = new DateTimePicker();
            chkConKinhDoanh = new CheckBox();
            grpTimKiem = new GroupBox();
            txtTimKiem = new TextBox();
            cboLocLoai = new ComboBox();
            chkChiConKinhDoanh = new CheckBox();
            dgvHangHoa = new DataGridView();
            colMaHang = new DataGridViewTextBoxColumn();
            colTenHang = new DataGridViewTextBoxColumn();
            colLoaiHang = new DataGridViewTextBoxColumn();
            colDonVi = new DataGridViewTextBoxColumn();
            colGiaBan = new DataGridViewTextBoxColumn();
            colSoLuongTon = new DataGridViewTextBoxColumn();
            colNgayNhap = new DataGridViewTextBoxColumn();
            colConKinhDoanh = new DataGridViewCheckBoxColumn();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnXemBangGia = new Button();
            btnXuatPdf = new Button();
            btnExcelLuoi = new Button();
            btnExcelTonKho = new Button();
            statusStrip1 = new StatusStrip();
            lblTrangThai = new ToolStripStatusLabel();
            timerTimKiem = new System.Windows.Forms.Timer(components);
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
            grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            //
            // grpThongTin
            //
            grpThongTin.Controls.Add(lblTenHang);
            grpThongTin.Controls.Add(txtTenHang);
            grpThongTin.Controls.Add(lblLoaiHang);
            grpThongTin.Controls.Add(cboLoaiHang);
            grpThongTin.Controls.Add(lblDonVi);
            grpThongTin.Controls.Add(cboDonVi);
            grpThongTin.Controls.Add(lblGiaBan);
            grpThongTin.Controls.Add(nudGiaBan);
            grpThongTin.Controls.Add(lblSoLuongTon);
            grpThongTin.Controls.Add(nudSoLuongTon);
            grpThongTin.Controls.Add(lblNgayNhap);
            grpThongTin.Controls.Add(dtpNgayNhap);
            grpThongTin.Controls.Add(chkConKinhDoanh);
            grpThongTin.Location = new Point(12, 8);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(600, 150);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin hàng hóa";
            //
            // lblTenHang
            //
            lblTenHang.AutoSize = true;
            lblTenHang.Location = new Point(14, 29);
            lblTenHang.Name = "lblTenHang";
            lblTenHang.Text = "Tên hàng:";
            //
            // txtTenHang
            //
            txtTenHang.Location = new Point(100, 26);
            txtTenHang.MaxLength = 100;
            txtTenHang.Name = "txtTenHang";
            txtTenHang.Size = new Size(480, 23);
            txtTenHang.TabIndex = 0;
            //
            // lblLoaiHang
            //
            lblLoaiHang.AutoSize = true;
            lblLoaiHang.Location = new Point(14, 59);
            lblLoaiHang.Name = "lblLoaiHang";
            lblLoaiHang.Text = "Loại hàng:";
            //
            // cboLoaiHang
            //
            cboLoaiHang.FormattingEnabled = true;
            cboLoaiHang.Location = new Point(100, 56);
            cboLoaiHang.MaxLength = 50;
            cboLoaiHang.Name = "cboLoaiHang";
            cboLoaiHang.Size = new Size(170, 23);
            cboLoaiHang.TabIndex = 1;
            //
            // lblDonVi
            //
            lblDonVi.AutoSize = true;
            lblDonVi.Location = new Point(14, 89);
            lblDonVi.Name = "lblDonVi";
            lblDonVi.Text = "Đơn vị:";
            //
            // cboDonVi
            //
            cboDonVi.FormattingEnabled = true;
            cboDonVi.Location = new Point(100, 86);
            cboDonVi.MaxLength = 20;
            cboDonVi.Name = "cboDonVi";
            cboDonVi.Size = new Size(170, 23);
            cboDonVi.TabIndex = 2;
            //
            // lblGiaBan
            //
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(14, 119);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Text = "Giá bán:";
            //
            // nudGiaBan
            //
            nudGiaBan.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudGiaBan.Location = new Point(100, 116);
            nudGiaBan.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudGiaBan.Name = "nudGiaBan";
            nudGiaBan.Size = new Size(170, 23);
            nudGiaBan.TabIndex = 3;
            nudGiaBan.TextAlign = HorizontalAlignment.Right;
            nudGiaBan.ThousandsSeparator = true;
            //
            // lblSoLuongTon
            //
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(300, 59);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Text = "Số lượng tồn:";
            //
            // nudSoLuongTon
            //
            nudSoLuongTon.Location = new Point(400, 56);
            nudSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSoLuongTon.Name = "nudSoLuongTon";
            nudSoLuongTon.Size = new Size(180, 23);
            nudSoLuongTon.TabIndex = 4;
            nudSoLuongTon.TextAlign = HorizontalAlignment.Right;
            nudSoLuongTon.ThousandsSeparator = true;
            //
            // lblNgayNhap
            //
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(300, 89);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Text = "Ngày nhập:";
            //
            // dtpNgayNhap
            //
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.Location = new Point(400, 86);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(180, 23);
            dtpNgayNhap.TabIndex = 5;
            //
            // chkConKinhDoanh
            //
            chkConKinhDoanh.AutoSize = true;
            chkConKinhDoanh.Checked = true;
            chkConKinhDoanh.CheckState = CheckState.Checked;
            chkConKinhDoanh.Location = new Point(400, 118);
            chkConKinhDoanh.Name = "chkConKinhDoanh";
            chkConKinhDoanh.TabIndex = 6;
            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.UseVisualStyleBackColor = true;
            //
            // grpTimKiem
            //
            grpTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTimKiem.Controls.Add(txtTimKiem);
            grpTimKiem.Controls.Add(cboLocLoai);
            grpTimKiem.Controls.Add(chkChiConKinhDoanh);
            grpTimKiem.Location = new Point(628, 8);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Size = new Size(360, 150);
            grpTimKiem.TabIndex = 1;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = "Tìm kiếm";
            //
            // txtTimKiem
            //
            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Location = new Point(14, 26);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Gõ tên hàng cần tìm…";
            txtTimKiem.Size = new Size(330, 23);
            txtTimKiem.TabIndex = 0;
            //
            // cboLocLoai
            //
            cboLocLoai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboLocLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLoai.FormattingEnabled = true;
            cboLocLoai.Location = new Point(14, 56);
            cboLocLoai.Name = "cboLocLoai";
            cboLocLoai.Size = new Size(330, 23);
            cboLocLoai.TabIndex = 1;
            //
            // chkChiConKinhDoanh
            //
            chkChiConKinhDoanh.AutoSize = true;
            chkChiConKinhDoanh.Location = new Point(14, 88);
            chkChiConKinhDoanh.Name = "chkChiConKinhDoanh";
            chkChiConKinhDoanh.TabIndex = 2;
            chkChiConKinhDoanh.Text = "Chỉ hiện hàng còn kinh doanh";
            chkChiConKinhDoanh.UseVisualStyleBackColor = true;
            //
            // dgvHangHoa
            //
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.AllowUserToDeleteRows = false;
            dgvHangHoa.AllowUserToResizeRows = false;
            dgvHangHoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHangHoa.BackgroundColor = SystemColors.Window;
            dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] { colMaHang, colTenHang, colLoaiHang, colDonVi, colGiaBan, colSoLuongTon, colNgayNhap, colConKinhDoanh });
            dgvHangHoa.Location = new Point(12, 168);
            dgvHangHoa.MultiSelect = false;
            dgvHangHoa.Name = "dgvHangHoa";
            dgvHangHoa.ReadOnly = true;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.Size = new Size(976, 314);
            dgvHangHoa.TabIndex = 2;
            //
            // colMaHang
            //
            colMaHang.DataPropertyName = "MaHang";
            colMaHang.FillWeight = 40F;
            colMaHang.HeaderText = "Mã";
            colMaHang.Name = "colMaHang";
            colMaHang.ReadOnly = true;
            //
            // colTenHang
            //
            colTenHang.DataPropertyName = "TenHang";
            colTenHang.FillWeight = 200F;
            colTenHang.HeaderText = "Tên hàng";
            colTenHang.Name = "colTenHang";
            colTenHang.ReadOnly = true;
            //
            // colLoaiHang
            //
            colLoaiHang.DataPropertyName = "LoaiHang";
            colLoaiHang.FillWeight = 110F;
            colLoaiHang.HeaderText = "Loại hàng";
            colLoaiHang.Name = "colLoaiHang";
            colLoaiHang.ReadOnly = true;
            //
            // colDonVi
            //
            colDonVi.DataPropertyName = "DonVi";
            colDonVi.FillWeight = 70F;
            colDonVi.HeaderText = "Đơn vị";
            colDonVi.Name = "colDonVi";
            colDonVi.ReadOnly = true;
            //
            // colGiaBan
            //
            colGiaBan.DataPropertyName = "GiaBan";
            styleGiaBan.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleGiaBan.Format = "N0";
            colGiaBan.DefaultCellStyle = styleGiaBan;
            colGiaBan.FillWeight = 90F;
            colGiaBan.HeaderText = "Giá bán";
            colGiaBan.Name = "colGiaBan";
            colGiaBan.ReadOnly = true;
            //
            // colSoLuongTon
            //
            colSoLuongTon.DataPropertyName = "SoLuongTon";
            styleSoLuong.Alignment = DataGridViewContentAlignment.MiddleRight;
            styleSoLuong.Format = "N0";
            colSoLuongTon.DefaultCellStyle = styleSoLuong;
            colSoLuongTon.FillWeight = 70F;
            colSoLuongTon.HeaderText = "Tồn";
            colSoLuongTon.Name = "colSoLuongTon";
            colSoLuongTon.ReadOnly = true;
            //
            // colNgayNhap
            //
            colNgayNhap.DataPropertyName = "NgayNhap";
            styleNgayNhap.Alignment = DataGridViewContentAlignment.MiddleCenter;
            styleNgayNhap.Format = "dd/MM/yyyy";
            colNgayNhap.DefaultCellStyle = styleNgayNhap;
            colNgayNhap.FillWeight = 90F;
            colNgayNhap.HeaderText = "Ngày nhập";
            colNgayNhap.Name = "colNgayNhap";
            colNgayNhap.ReadOnly = true;
            //
            // colConKinhDoanh
            //
            colConKinhDoanh.DataPropertyName = "ConKinhDoanh";
            colConKinhDoanh.FillWeight = 70F;
            colConKinhDoanh.HeaderText = "Kinh doanh";
            colConKinhDoanh.Name = "colConKinhDoanh";
            colConKinhDoanh.ReadOnly = true;
            //
            // btnThem
            //
            btnThem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThem.Location = new Point(12, 492);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(85, 30);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            //
            // btnSua
            //
            btnSua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSua.Location = new Point(103, 492);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(85, 30);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            //
            // btnXoa
            //
            btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoa.Location = new Point(194, 492);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(85, 30);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            //
            // btnLamMoi
            //
            btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLamMoi.Location = new Point(285, 492);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(85, 30);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            //
            // btnXemBangGia
            //
            btnXemBangGia.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXemBangGia.Location = new Point(478, 492);
            btnXemBangGia.Name = "btnXemBangGia";
            btnXemBangGia.Size = new Size(110, 30);
            btnXemBangGia.TabIndex = 7;
            btnXemBangGia.Text = "Xem bảng giá";
            btnXemBangGia.UseVisualStyleBackColor = true;
            //
            // btnXuatPdf
            //
            btnXuatPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXuatPdf.Location = new Point(594, 492);
            btnXuatPdf.Name = "btnXuatPdf";
            btnXuatPdf.Size = new Size(95, 30);
            btnXuatPdf.TabIndex = 8;
            btnXuatPdf.Text = "Xuất PDF";
            btnXuatPdf.UseVisualStyleBackColor = true;
            //
            // btnExcelLuoi
            //
            btnExcelLuoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExcelLuoi.Location = new Point(695, 492);
            btnExcelLuoi.Name = "btnExcelLuoi";
            btnExcelLuoi.Size = new Size(140, 30);
            btnExcelLuoi.TabIndex = 9;
            btnExcelLuoi.Text = "Xuất Excel (lưới)";
            btnExcelLuoi.UseVisualStyleBackColor = true;
            //
            // btnExcelTonKho
            //
            btnExcelTonKho.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExcelTonKho.Location = new Point(841, 492);
            btnExcelTonKho.Name = "btnExcelTonKho";
            btnExcelTonKho.Size = new Size(147, 30);
            btnExcelTonKho.TabIndex = 10;
            btnExcelTonKho.Text = "Xuất Excel tồn kho";
            btnExcelTonKho.UseVisualStyleBackColor = true;
            //
            // statusStrip1
            //
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblTrangThai });
            statusStrip1.Location = new Point(0, 533);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1000, 22);
            statusStrip1.TabIndex = 11;
            //
            // lblTrangThai
            //
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Text = "Sẵn sàng";
            //
            // timerTimKiem
            //
            timerTimKiem.Interval = 350;
            //
            // FormHangHoa
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 555);
            Controls.Add(grpThongTin);
            Controls.Add(grpTimKiem);
            Controls.Add(dgvHangHoa);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXemBangGia);
            Controls.Add(btnXuatPdf);
            Controls.Add(btnExcelLuoi);
            Controls.Add(btnExcelTonKho);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1016, 594);
            Name = "FormHangHoa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hàng hóa - Tạp hóa UTC2";
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
            grpTimKiem.ResumeLayout(false);
            grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenHang;
        private TextBox txtTenHang;
        private Label lblLoaiHang;
        private ComboBox cboLoaiHang;
        private Label lblDonVi;
        private ComboBox cboDonVi;
        private Label lblGiaBan;
        private NumericUpDown nudGiaBan;
        private Label lblSoLuongTon;
        private NumericUpDown nudSoLuongTon;
        private Label lblNgayNhap;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;
        private GroupBox grpTimKiem;
        private TextBox txtTimKiem;
        private ComboBox cboLocLoai;
        private CheckBox chkChiConKinhDoanh;
        private DataGridView dgvHangHoa;
        private DataGridViewTextBoxColumn colMaHang;
        private DataGridViewTextBoxColumn colTenHang;
        private DataGridViewTextBoxColumn colLoaiHang;
        private DataGridViewTextBoxColumn colDonVi;
        private DataGridViewTextBoxColumn colGiaBan;
        private DataGridViewTextBoxColumn colSoLuongTon;
        private DataGridViewTextBoxColumn colNgayNhap;
        private DataGridViewCheckBoxColumn colConKinhDoanh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnXemBangGia;
        private Button btnXuatPdf;
        private Button btnExcelLuoi;
        private Button btnExcelTonKho;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblTrangThai;
        private System.Windows.Forms.Timer timerTimKiem;
    }
}
