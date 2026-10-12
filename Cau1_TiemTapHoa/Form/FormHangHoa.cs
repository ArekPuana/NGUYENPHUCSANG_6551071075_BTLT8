using Cau1_TiemTapHoa.Helpers;
using Cau1_TiemTapHoa.Models;
using Cau1_TiemTapHoa.Services;

using Report = FastReport.Report;

namespace Cau1_TiemTapHoa.Forms;

/// <summary>
/// Câu 1 - Tiệm tạp hóa UTC2: quản lý bảng HangHoa (thêm/sửa/xóa/tìm kiếm),
/// in bảng giá bằng FastReport (nguồn DataTable) + xuất PDF, xuất Excel bằng ClosedXML.
/// </summary>
public partial class FormHangHoa : Form
{
    private const string TatCaLoai = "Tất cả";

    private readonly HangHoaService _svc = new();
    private List<HangHoa> _ds = new();
    private int? _maDangChon;     // MaHang của dòng đang chọn (null = chưa chọn)
    private int _phienTai;        // chống 2 lần tải chồng nhau khi gõ tìm kiếm nhanh

    public FormHangHoa()
    {
        InitializeComponent();
        dgvHangHoa.AutoGenerateColumns = false;

        // ComboBox nhập liệu (cho phép gõ thêm giá trị mới)
        cboLoaiHang.Items.AddRange(AppConfig.DsLoaiHang);
        cboDonVi.Items.AddRange(AppConfig.DsDonVi);

        // ComboBox lọc có mục "Tất cả"
        cboLocLoai.Items.Add(TatCaLoai);
        cboLocLoai.Items.AddRange(AppConfig.DsLoaiHang);
        cboLocLoai.SelectedIndex = 0;

        dtpNgayNhap.Value = DateTime.Today;

        // --- Sự kiện ---
        Load += (_, _) => UiHelper.ChayAsync(this, () => TaiLaiAsync());

        // Bấm vào dòng => đổ dữ liệu lên control (CellClick, RowHeaderMouseClick, phím mũi tên)
        dgvHangHoa.CellClick += (_, e) => { if (e.RowIndex >= 0) HienDongDangChon(); };
        dgvHangHoa.RowHeaderMouseClick += (_, e) => { if (e.RowIndex >= 0) HienDongDangChon(); };
        dgvHangHoa.SelectionChanged += (_, _) => { if (dgvHangHoa.Focused) HienDongDangChon(); };

        // Tìm kiếm: chờ 350 ms sau khi ngừng gõ
        txtTimKiem.TextChanged += (_, _) => { timerTimKiem.Stop(); timerTimKiem.Start(); };
        timerTimKiem.Tick += (_, _) => { timerTimKiem.Stop(); UiHelper.ChayAsync(this, () => TaiLaiAsync()); };
        cboLocLoai.SelectedIndexChanged += (_, _) => UiHelper.ChayAsync(this, () => TaiLaiAsync());
        chkChiConKinhDoanh.CheckedChanged += (_, _) => UiHelper.ChayAsync(this, () => TaiLaiAsync());

        // Thêm - Sửa - Xóa - Làm mới
        btnThem.Click += (_, _) => UiHelper.ChayAsync(this, ThemAsync);
        btnSua.Click += (_, _) => UiHelper.ChayAsync(this, SuaAsync);
        btnXoa.Click += (_, _) => UiHelper.ChayAsync(this, XoaAsync);
        btnLamMoi.Click += (_, _) => UiHelper.ChayAsync(this, LamMoiAsync);

        // Report & xuất file
        btnXemBangGia.Click += (_, _) => UiHelper.ChayAsync(this, XemBangGiaAsync);
        btnXuatPdf.Click += (_, _) => UiHelper.ChayAsync(this, XuatPdfAsync);
        btnExcelLuoi.Click += (_, _) => XuatExcelTuLuoi();
        btnExcelTonKho.Click += (_, _) => UiHelper.ChayAsync(this, XuatExcelTonKhoAsync);
    }

    // =====================================================================
    //  TẢI DỮ LIỆU + TÌM KIẾM
    // =====================================================================

    private async Task TaiLaiAsync(int? chonMa = null)
    {
        int phien = ++_phienTai;

        string? loai = cboLocLoai.SelectedIndex > 0 ? cboLocLoai.SelectedItem as string : null;
        var ds = await _svc.TimKiemAsync(txtTimKiem.Text, loai, chkChiConKinhDoanh.Checked);

        if (phien != _phienTai) return;   // đã có lần tải mới hơn

        _ds = ds;
        dgvHangHoa.DataSource = _ds;
        dgvHangHoa.ClearSelection();

        if (chonMa is int ma)
            ChonDongTheoMa(ma);

        lblTrangThai.Text = $"Tìm thấy {_ds.Count} mặt hàng";
    }

    private void ChonDongTheoMa(int ma)
    {
        foreach (DataGridViewRow row in dgvHangHoa.Rows)
        {
            if (row.DataBoundItem is HangHoa hh && hh.MaHang == ma)
            {
                row.Selected = true;
                dgvHangHoa.CurrentCell = row.Cells[colTenHang.Index];
                HienDongDangChon();
                return;
            }
        }
    }

    /// <summary>Đổ dữ liệu dòng đang chọn lên các control nhập liệu.</summary>
    private void HienDongDangChon()
    {
        if (dgvHangHoa.CurrentRow?.DataBoundItem is not HangHoa hh) return;

        _maDangChon = hh.MaHang;
        txtTenHang.Text = hh.TenHang;
        cboLoaiHang.SelectedItem = hh.LoaiHang;
        if (cboLoaiHang.SelectedIndex == -1) cboLoaiHang.Text = hh.LoaiHang ?? "";
        cboDonVi.SelectedItem = hh.DonVi;
        if (cboDonVi.SelectedIndex == -1) cboDonVi.Text = hh.DonVi ?? "";
        nudGiaBan.Value = Math.Clamp(hh.GiaBan, nudGiaBan.Minimum, nudGiaBan.Maximum);
        nudSoLuongTon.Value = Math.Clamp(hh.SoLuongTon, (int)nudSoLuongTon.Minimum, (int)nudSoLuongTon.Maximum);
        dtpNgayNhap.Value = hh.NgayNhap < dtpNgayNhap.MinDate ? DateTime.Today : hh.NgayNhap;
        chkConKinhDoanh.Checked = hh.ConKinhDoanh;
    }

    private void XoaTrangNhapLieu()
    {
        _maDangChon = null;
        txtTenHang.Clear();
        cboLoaiHang.SelectedIndex = -1;
        cboLoaiHang.Text = "";
        cboDonVi.SelectedIndex = -1;
        cboDonVi.Text = "";
        nudGiaBan.Value = 0;
        nudSoLuongTon.Value = 0;
        dtpNgayNhap.Value = DateTime.Today;
        chkConKinhDoanh.Checked = true;
        dgvHangHoa.ClearSelection();
        txtTenHang.Focus();
    }

    // =====================================================================
    //  THÊM - SỬA - XÓA
    // =====================================================================

    /// <summary>Đọc và kiểm tra dữ liệu nhập. Trả về null nếu dữ liệu không hợp lệ.</summary>
    private HangHoa? DocDuLieuNhap()
    {
        string ten = txtTenHang.Text.Trim();
        if (ten.Length == 0)
        {
            MessageBox.Show(this, "Tên hàng không được để trống.", "Dữ liệu chưa hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenHang.Focus();
            return null;
        }

        if (nudGiaBan.Value <= 0)
        {
            MessageBox.Show(this, "Giá bán phải lớn hơn 0.", "Dữ liệu chưa hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudGiaBan.Focus();
            nudGiaBan.Select(0, nudGiaBan.Text.Length);
            return null;
        }

        string loai = cboLoaiHang.Text.Trim();
        string donVi = cboDonVi.Text.Trim();

        return new HangHoa
        {
            TenHang = ten,
            LoaiHang = loai.Length == 0 ? null : loai,
            DonVi = donVi.Length == 0 ? null : donVi,
            GiaBan = nudGiaBan.Value,
            SoLuongTon = (int)nudSoLuongTon.Value,
            NgayNhap = dtpNgayNhap.Value.Date,
            ConKinhDoanh = chkConKinhDoanh.Checked
        };
    }

    private async Task ThemAsync()
    {
        var hh = DocDuLieuNhap();
        if (hh == null) return;

        int maMoi = await _svc.ThemAsync(hh);

        // Đặt lại bộ lọc để mặt hàng vừa thêm hiển thị ngay trên lưới
        txtTimKiem.Clear();
        cboLocLoai.SelectedIndex = 0;
        chkChiConKinhDoanh.Checked = false;

        await TaiLaiAsync(maMoi);

        MessageBox.Show(this, $"Đã thêm mặt hàng \"{hh.TenHang}\".", "Thêm",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task SuaAsync()
    {
        if (_maDangChon is not int ma)
        {
            MessageBox.Show(this, "Hãy chọn một mặt hàng trên lưới để sửa.", "Sửa",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var hh = DocDuLieuNhap();
        if (hh == null) return;
        hh.MaHang = ma;

        if (!await _svc.SuaAsync(hh))
        {
            MessageBox.Show(this, "Mặt hàng không còn tồn tại trong CSDL.", "Sửa",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            await TaiLaiAsync();
            return;
        }

        await TaiLaiAsync(ma);
        MessageBox.Show(this, "Đã cập nhật mặt hàng.", "Sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task XoaAsync()
    {
        if (_maDangChon is not int ma)
        {
            MessageBox.Show(this, "Hãy chọn một mặt hàng trên lưới để xóa.", "Xóa",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var ten = _ds.FirstOrDefault(x => x.MaHang == ma)?.TenHang ?? txtTenHang.Text;
        var kq = MessageBox.Show(this, $"Bạn có chắc muốn xóa mặt hàng \"{ten}\"?", "Xác nhận xóa",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (kq != DialogResult.Yes) return;

        await _svc.XoaAsync(ma);
        XoaTrangNhapLieu();
        await TaiLaiAsync();
    }

    private async Task LamMoiAsync()
    {
        txtTimKiem.Clear();
        cboLocLoai.SelectedIndex = 0;
        chkChiConKinhDoanh.Checked = false;
        XoaTrangNhapLieu();
        await TaiLaiAsync();
    }

    // =====================================================================
    //  REPORT BẢNG GIÁ (FastReport, nguồn DataTable)
    // =====================================================================

    private async Task<Report> TaoReportBangGiaAsync()
    {
        // 1) Truy vấn hàng còn kinh doanh bằng EF Core
        var ds = await _svc.LayHangConKinhDoanhAsync();

        // 2) Chuyển sang DataTable
        var dt = DataTableHelper.ToDataTable(ds, "HangHoa");

        // 3) Nạp mẫu + đăng ký dữ liệu tên "HangHoa" (Enabled = true) + tham số NgayIn
        var r = ReportService.TaoReport(AppConfig.FrxBangGia);
        ReportService.DangKy(r, dt, "HangHoa");
        ReportService.DatThamSo(r, "NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
        return r;
    }

    private async Task XemBangGiaAsync()
    {
        if (!UiHelper.CoMauReport(this, AppConfig.FrxBangGia)) return;

        using var r = await TaoReportBangGiaAsync();
        ReportService.XemTruoc(r);
    }

    private async Task XuatPdfAsync()
    {
        if (!UiHelper.CoMauReport(this, AppConfig.FrxBangGia)) return;

        var path = UiHelper.ChonFileLuu(this, "PDF (*.pdf)|*.pdf", $"BangGia_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        if (path == null) return;

        using var r = await TaoReportBangGiaAsync();
        ReportService.XuatPdf(r, path);
        UiHelper.HoiMoFile(this, path);
    }

    // =====================================================================
    //  XUẤT EXCEL (ClosedXML)
    // =====================================================================

    /// <summary>Xuất đúng các dòng đang hiển thị trên DataGridView.</summary>
    private void XuatExcelTuLuoi()
    {
        if (dgvHangHoa.Rows.Count == 0)
        {
            MessageBox.Show(this, "Lưới đang không có dòng nào để xuất.", "Xuất Excel",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var path = UiHelper.ChonFileLuu(this, "Excel (*.xlsx)|*.xlsx", $"HangHoa_Luoi_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        if (path == null) return;

        UiHelper.Chay(this, () =>
        {
            ExcelExporter.XuatDataGridView(dgvHangHoa, path, "HangHoa");
            UiHelper.HoiMoFile(this, path);
        });
    }

    /// <summary>Truy vấn lại bằng EF Core các hàng còn kinh doanh, có cột công thức "Giá trị tồn" và dòng Tổng (SUM).</summary>
    private async Task XuatExcelTonKhoAsync()
    {
        var path = UiHelper.ChonFileLuu(this, "Excel (*.xlsx)|*.xlsx", $"TonKho_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        if (path == null) return;

        var ds = await _svc.LayHangConKinhDoanhAsync();

        //            A        B            C             D          E                   F                                    G                      H
        var cot = new List<CotExcel>
        {
            new("Mã", "0"),
            new("Tên hàng"),
            new("Loại hàng"),
            new("Đơn vị"),
            new("Giá bán", "#,##0"),
            new("Số lượng tồn", "#,##0", CongTong: true),
            new("Ngày nhập", "dd/MM/yyyy"),
            new("Giá trị tồn", "#,##0", CongTong: true, CongThuc: "E{r}*F{r}")
        };

        var dong = ds.Select(x => new object?[]
        {
            x.MaHang, x.TenHang, x.LoaiHang, x.DonVi, x.GiaBan, x.SoLuongTon, x.NgayNhap, null
        });

        ExcelExporter.Xuat(path, "TonKho", cot, dong);
        UiHelper.HoiMoFile(this, path);
    }
}
