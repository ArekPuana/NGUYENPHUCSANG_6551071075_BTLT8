using Cau2_QuanCafe.Helpers;
using Cau2_QuanCafe.Models;
using Cau2_QuanCafe.Services;

namespace Cau2_QuanCafe.Forms;

public partial class FormQuanCafe : Form
{
    private readonly CafeService _cafeService = new();
    private List<HoaDonDisplayDto> _dsHoaDonHienTai = new();
    private int? _maHdDangChon = null;
    private bool _dangDongBoDuLieu = false;

    public FormQuanCafe()
    {
        InitializeComponent();
        KhoiTaoDuLieuBanDau();
    }

    private void KhoiTaoDuLieuBanDau()
    {
        dgvHoaDon.AutoGenerateColumns = false;
        dgvChiTiet.AutoGenerateColumns = false;

        // 1. Cấu hình bộ lọc tìm kiếm
        dtpTuNgay.Value = new DateTime(2026, 10, 1);
        dtpDenNgay.Value = new DateTime(2026, 10, 11);

        cboLocSoBan.Items.Add("Tất cả");
        for (int i = 1; i <= 10; i++)
        {
            cboLocSoBan.Items.Add(i.ToString());
        }
        cboLocSoBan.SelectedIndex = 0;

        cboLocHinhThuc.Items.AddRange(new object[] { "Tất cả", "Tiền mặt", "Chuyển khoản" });
        cboLocHinhThuc.SelectedIndex = 0;

        // 2. Cấu hình control nhập liệu hóa đơn
        for (int i = 1; i <= 10; i++)
        {
            cboSoBan.Items.Add(i);
        }
        cboSoBan.SelectedIndex = 0;

        dtpNgayLap.Value = DateTime.Now;

        // 3. Nạp danh sách món ăn vào ComboBox
        NapDanhSachMon();

        // 4. Gắn các sự kiện
        btnTim.Click += (s, e) => TimKiem();
        txtLocTenMon.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TimKiem();
            }
        };

        dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
        dgvChiTiet.SelectionChanged += DgvChiTiet_SelectionChanged;

        btnHoaDonMoi.Click += BtnHoaDonMoi_Click;
        btnSuaHoaDon.Click += BtnSuaHoaDon_Click;
        btnXoaHoaDon.Click += BtnXoaHoaDon_Click;

        btnThemMon.Click += BtnThemMon_Click;
        btnSuaSoLuong.Click += BtnSuaSoLuong_Click;
        btnXoaMon.Click += BtnXoaMon_Click;

        btnXemHoaDon.Click += BtnXemHoaDon_Click;
        btnXuatPdf.Click += BtnXuatPdf_Click;
        btnIn.Click += BtnIn_Click;

        // 5. Tải dữ liệu ban đầu
        TimKiem();
    }

    private void NapDanhSachMon()
    {
        try
        {
            var dsMon = _cafeService.LayDanhSachMon();
            var comboItems = dsMon.Select(m => new
            {
                m.MaMon,
                HienThi = $"{m.TenMon} ({AppConfig.DinhDangTien(m.DonGia)})"
            }).ToList();

            cboMon.DataSource = comboItems;
            cboMon.DisplayMember = "HienThi";
            cboMon.ValueMember = "MaMon";
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void TimKiem(int? chonMaHd = null)
    {
        try
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            int? soBan = null;
            if (cboLocSoBan.SelectedIndex > 0 && int.TryParse(cboLocSoBan.SelectedItem?.ToString(), out int b))
            {
                soBan = b;
            }

            string? hinhThuc = cboLocHinhThuc.SelectedItem?.ToString();
            if (hinhThuc == "Tất cả") hinhThuc = null;

            string? tenMon = txtLocTenMon.Text.Trim();

            _dsHoaDonHienTai = _cafeService.TimKiemHoaDon(tuNgay, denNgay, soBan, hinhThuc, tenMon);

            _dangDongBoDuLieu = true;
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = _dsHoaDonHienTai;
            _dangDongBoDuLieu = false;

            decimal tongDoanhThu = _dsHoaDonHienTai.Sum(h => h.TongTien);
            lblTongDoanhThu.Text = $"Tổng doanh thu đang hiển thị: {AppConfig.DinhDangTien(tongDoanhThu)} đ";

            if (_dsHoaDonHienTai.Count > 0)
            {
                int targetMaHd = chonMaHd ?? _maHdDangChon ?? _dsHoaDonHienTai[0].MaHD;
                int idx = _dsHoaDonHienTai.FindIndex(h => h.MaHD == targetMaHd);
                if (idx < 0) idx = 0;

                dgvHoaDon.ClearSelection();
                dgvHoaDon.Rows[idx].Selected = true;
                dgvHoaDon.CurrentCell = dgvHoaDon.Rows[idx].Cells[0];
                HienThiChiTietHoaDon(_dsHoaDonHienTai[idx]);
            }
            else
            {
                _maHdDangChon = null;
                grpChiTiet.Text = "Chi tiết hóa đơn";
                dgvChiTiet.DataSource = null;
                lblTongCong.Text = "Tổng cộng: 0 đ";
                txtGhiChu.Text = "";
            }
        }
        catch (Exception ex)
        {
            _dangDongBoDuLieu = false;
            UiHelper.HienLoi(this, ex);
        }
    }

    private void DgvHoaDon_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangDongBoDuLieu) return;

        if (dgvHoaDon.CurrentRow?.DataBoundItem is HoaDonDisplayDto selected)
        {
            HienThiChiTietHoaDon(selected);
        }
    }

    private void HienThiChiTietHoaDon(HoaDonDisplayDto hd)
    {
        _maHdDangChon = hd.MaHD;
        grpChiTiet.Text = $"Chi tiết hóa đơn {hd.MaHienThi}";

        dtpNgayLap.Value = hd.NgayLap;
        if (hd.SoBan >= 1 && hd.SoBan <= 10)
        {
            cboSoBan.SelectedItem = hd.SoBan;
        }

        if (hd.HinhThucTT == "Chuyển khoản")
        {
            radChuyenKhoan.Checked = true;
        }
        else
        {
            radTienMat.Checked = true;
        }

        txtGhiChu.Text = hd.GhiChu ?? "";

        // Tải chi tiết các món
        TaiLuoiChiTiet(hd.MaHD);
    }

    private void TaiLuoiChiTiet(int maHd)
    {
        try
        {
            var dsChiTiet = _cafeService.LayChiTietHoaDon(maHd);
            dgvChiTiet.DataSource = null;
            dgvChiTiet.DataSource = dsChiTiet;

            decimal tong = dsChiTiet.Sum(c => c.ThanhTien);
            lblTongCong.Text = $"Tổng cộng: {AppConfig.DinhDangTien(tong)} đ";

            // Cập nhật tổng tiền ở dòng tương ứng của lưới trái
            CapNhatTongTienDongHienTai(tong);
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void CapNhatTongTienDongHienTai(decimal tongTienMoi)
    {
        if (_maHdDangChon == null) return;

        var hdItem = _dsHoaDonHienTai.FirstOrDefault(h => h.MaHD == _maHdDangChon.Value);
        if (hdItem != null)
        {
            hdItem.TongTien = tongTienMoi;
            dgvHoaDon.Refresh();

            decimal tongDoanhThu = _dsHoaDonHienTai.Sum(h => h.TongTien);
            lblTongDoanhThu.Text = $"Tổng doanh thu đang hiển thị: {AppConfig.DinhDangTien(tongDoanhThu)} đ";
        }
    }

    private void DgvChiTiet_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvChiTiet.CurrentRow?.DataBoundItem is ChiTietIn ct)
        {
            if (ct.SoLuong >= nudSoLuong.Minimum && ct.SoLuong <= nudSoLuong.Maximum)
            {
                nudSoLuong.Value = ct.SoLuong;
            }
            if (ct.MaMon > 0)
            {
                cboMon.SelectedValue = ct.MaMon;
            }
        }
    }

    private void BtnHoaDonMoi_Click(object? sender, EventArgs e)
    {
        try
        {
            int soBan = cboSoBan.SelectedItem is int b ? b : 1;
            string hinhThuc = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
            string? ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
            DateTime ngayLap = dtpNgayLap.Value;

            var newHd = _cafeService.ThemHoaDon(ngayLap, soBan, hinhThuc, ghiChu);
            MessageBox.Show(this, $"Đã tạo hóa đơn {AppConfig.DinhDangMaHd(newHd.MaHd)} thành công. Bạn có thể chọn món và nhấn 'Thêm món'.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TimKiem(newHd.MaHd);
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnSuaHoaDon_Click(object? sender, EventArgs e)
    {
        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn hóa đơn cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int soBan = cboSoBan.SelectedItem is int b ? b : 1;
            string hinhThuc = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
            string? ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
            DateTime ngayLap = dtpNgayLap.Value;

            bool ok = _cafeService.CapNhatHoaDon(_maHdDangChon.Value, ngayLap, soBan, hinhThuc, ghiChu);
            if (ok)
            {
                var hd = _dsHoaDonHienTai.FirstOrDefault(h => h.MaHD == _maHdDangChon.Value);
                if (hd != null)
                {
                    hd.NgayLap = ngayLap;
                    hd.SoBan = soBan;
                    hd.HinhThucTT = hinhThuc;
                    hd.GhiChu = ghiChu;
                    dgvHoaDon.Refresh();
                }

                MessageBox.Show(this, "Cập nhật thông tin hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnXoaHoaDon_Click(object? sender, EventArgs e)
    {
        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn hóa đơn cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string maHienThi = AppConfig.DinhDangMaHd(_maHdDangChon.Value);
        var xacNhan = MessageBox.Show(this,
            $"Bạn có chắc chắn muốn xóa hóa đơn {maHienThi}?\nToàn bộ các dòng món trong hóa đơn này cũng sẽ bị xóa.",
            "Xác nhận xóa hóa đơn",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (xacNhan != DialogResult.Yes) return;

        try
        {
            bool ok = _cafeService.XoaHoaDon(_maHdDangChon.Value);
            if (ok)
            {
                MessageBox.Show(this, $"Đã xóa hóa đơn {maHienThi} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TimKiem();
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnThemMon_Click(object? sender, EventArgs e)
    {
        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn hoặc tạo hóa đơn trước khi thêm món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cboMon.SelectedValue is not int maMon)
        {
            MessageBox.Show(this, "Vui lòng chọn món cần thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int sl = (int)nudSoLuong.Value;
        if (sl <= 0)
        {
            MessageBox.Show(this, "Số lượng món phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            bool ok = _cafeService.ThemMonVaoHoaDon(_maHdDangChon.Value, maMon, sl);
            if (ok)
            {
                TaiLuoiChiTiet(_maHdDangChon.Value);
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnSuaSoLuong_Click(object? sender, EventArgs e)
    {
        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn hóa đơn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dgvChiTiet.CurrentRow?.DataBoundItem is not ChiTietIn ct)
        {
            MessageBox.Show(this, "Vui lòng chọn món cần sửa số lượng trong lưới chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int slMoi = (int)nudSoLuong.Value;
        if (slMoi <= 0)
        {
            MessageBox.Show(this, "Số lượng phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            bool ok = _cafeService.SuaSoLuongChiTiet(ct.MaCT, slMoi);
            if (ok)
            {
                TaiLuoiChiTiet(_maHdDangChon.Value);
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnXoaMon_Click(object? sender, EventArgs e)
    {
        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn hóa đơn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dgvChiTiet.CurrentRow?.DataBoundItem is not ChiTietIn ct)
        {
            MessageBox.Show(this, "Vui lòng chọn món cần xóa trong lưới chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var xacNhan = MessageBox.Show(this,
            $"Bạn có chắc chắn muốn xóa món '{ct.TenMon}' khỏi hóa đơn?",
            "Xác nhận xóa món",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (xacNhan != DialogResult.Yes) return;

        try
        {
            bool ok = _cafeService.XoaChiTiet(ct.MaCT);
            if (ok)
            {
                TaiLuoiChiTiet(_maHdDangChon.Value);
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private bool KiemTraHoaDonTruocKhiIn(out HoaDon? hoaDon, out List<ChiTietIn> dsChiTiet)
    {
        hoaDon = null;
        dsChiTiet = new List<ChiTietIn>();

        if (_maHdDangChon == null)
        {
            MessageBox.Show(this, "Vui lòng chọn một hóa đơn từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        hoaDon = _cafeService.LayHoaDonTheoMa(_maHdDangChon.Value);
        if (hoaDon == null)
        {
            MessageBox.Show(this, "Không tìm thấy hóa đơn được chọn trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        dsChiTiet = _cafeService.LayChiTietHoaDon(hoaDon.MaHd);
        if (dsChiTiet.Count == 0)
        {
            MessageBox.Show(this, "Hóa đơn này chưa có món nào. Không thể in hóa đơn trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void BtnXemHoaDon_Click(object? sender, EventArgs e)
    {
        if (!KiemTraHoaDonTruocKhiIn(out var hd, out var dsChiTiet)) return;

        try
        {
            using var report = ReportService.TaoReport(hd!, dsChiTiet);
            // Giữ phím Ctrl hoặc Shift khi bấm "Xem hóa đơn" để mở FastReport Designer
            if ((ModifierKeys & (Keys.Control | Keys.Shift)) != 0)
            {
                ReportService.ThietKe(report);
            }
            else
            {
                ReportService.XemTruoc(report);
            }
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnXuatPdf_Click(object? sender, EventArgs e)
    {
        if (!KiemTraHoaDonTruocKhiIn(out var hd, out var dsChiTiet)) return;

        try
        {
            string tenMacDinh = $"HoaDon_{AppConfig.DinhDangMaHd(hd!.MaHd)}.pdf";
            string? filePath = UiHelper.ChonFileLuu(this, "Tệp PDF (*.pdf)|*.pdf", tenMacDinh);
            if (filePath == null) return;

            using var report = ReportService.TaoReport(hd!, dsChiTiet);
            ReportService.XuatPdf(report, filePath);
            UiHelper.HoiMoFile(this, filePath);
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }

    private void BtnIn_Click(object? sender, EventArgs e)
    {
        if (!KiemTraHoaDonTruocKhiIn(out var hd, out var dsChiTiet)) return;

        try
        {
            using var report = ReportService.TaoReport(hd!, dsChiTiet);
            ReportService.In(report);
        }
        catch (Exception ex)
        {
            UiHelper.HienLoi(this, ex);
        }
    }
}
