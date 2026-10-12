using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau3_PhieuBaoDiem.Models;
using Cau3_PhieuBaoDiem.Printing;
using Cau3_PhieuBaoDiem.Services;

namespace Cau3_PhieuBaoDiem;

public partial class Form1 : Form
{
    private QuanLyDiemContext _context = null!;
    private List<Lop> _allLops = new();
    private List<SinhVien> _allStudents = new();
    private List<SinhVien> _filteredStudents = new();

    private SinhVien? _selectedStudent = null;
    private List<DiemMon> _studentSubjects = new();
    private DiemMon? _selectedSubject = null;

    private PageSettings _pageSettings = new();
    private PrinterSettings _printerSettings = new();

    private readonly CultureInfo _viCulture = CultureInfo.GetCultureInfo("vi-VN");

    public class LopItem
    {
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string DisplayText { get; set; } = string.Empty;

        public override string ToString() => DisplayText;
    }

    public Form1()
    {
        InitializeComponent();
        InitPrinterSettings();
    }

    private void InitPrinterSettings()
    {
        _pageSettings = new PageSettings
        {
            Margins = new Margins(60, 60, 60, 60),
            Landscape = false
        };
        _printerSettings = new PrinterSettings();
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        _context = new QuanLyDiemContext();

        SetupDataGrids();
        LoadLopList();
        ReloadSinhVienList();
    }

    private void SetupDataGrids()
    {
        // Cấu hình DataGridView Sinh Viên
        dgvSinhVien.AutoGenerateColumns = false;
        dgvSinhVien.Columns.Clear();

        dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSvMa",
            HeaderText = "MSSV",
            DataPropertyName = "MaSv",
            Width = 75,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        });

        dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSvHoTen",
            HeaderText = "Họ tên",
            DataPropertyName = "HoTen",
            Width = 145,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        });

        dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSvNgaySinh",
            HeaderText = "Ngày sinh",
            DataPropertyName = "NgaySinhFormatted",
            Width = 90,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSvGioiTinh",
            HeaderText = "Giới tính",
            DataPropertyName = "GioiTinh",
            Width = 75,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSvLop",
            HeaderText = "Lớp",
            DataPropertyName = "MaLop",
            Width = 70,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        // Cấu hình DataGridView Điểm
        dgvDiem.AutoGenerateColumns = false;
        dgvDiem.Columns.Clear();

        dgvDiem.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDiemStt",
            HeaderText = "STT",
            Width = 50,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvDiem.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDiemMon",
            HeaderText = "Môn học",
            Width = 230,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        });

        dgvDiem.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDiemTc",
            HeaderText = "Số TC",
            Width = 70,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvDiem.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDiemSo",
            HeaderText = "Điểm",
            Width = 70,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
    }

    private void LoadLopList()
    {
        _allLops = _context.Lops.OrderBy(l => l.MaLop).ToList();

        // 1. Đổ vào ListBox Lớp
        lstLop.Items.Clear();
        lstLop.Items.Add(new LopItem { MaLop = "", TenLop = "Tất cả", DisplayText = "Tất cả" });
        foreach (var lop in _allLops)
        {
            lstLop.Items.Add(new LopItem
            {
                MaLop = lop.MaLop,
                TenLop = lop.TenLop,
                DisplayText = $"{lop.MaLop} - {lop.TenLop}"
            });
        }
        lstLop.SelectedIndex = 0;

        // 2. Đổ vào ComboBox Lớp (Thông tin sinh viên)
        cboLop.DataSource = null;
        var cboItems = _allLops.Select(l => new LopItem
        {
            MaLop = l.MaLop,
            TenLop = l.TenLop,
            DisplayText = l.MaLop
        }).ToList();
        cboLop.DisplayMember = "DisplayText";
        cboLop.ValueMember = "MaLop";
        cboLop.DataSource = cboItems;
    }

    private void ReloadSinhVienList(string? selectMaSv = null)
    {
        _allStudents = _context.SinhViens.Include(s => s.MaLopNavigation).OrderBy(s => s.MaSv).ToList();
        ApplyFilter(selectMaSv);
    }

    private void ApplyFilter(string? selectMaSv = null)
    {
        var selectedLop = lstLop.SelectedItem as LopItem;
        string maLopFilter = selectedLop?.MaLop ?? "";
        string keyword = txtTimKiem.Text.Trim();

        var query = _allStudents.AsEnumerable();

        if (!string.IsNullOrEmpty(maLopFilter))
        {
            query = query.Where(s => s.MaLop.Equals(maLopFilter, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(s =>
                s.MaSv.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        _filteredStudents = query.ToList();

        var viewList = _filteredStudents.Select(s => new
        {
            s.MaSv,
            s.HoTen,
            NgaySinhFormatted = s.NgaySinh.ToString("dd/MM/yyyy"),
            s.GioiTinh,
            s.MaLop
        }).ToList();

        dgvSinhVien.DataSource = viewList;

        if (_filteredStudents.Count > 0)
        {
            int targetIndex = 0;
            if (!string.IsNullOrEmpty(selectMaSv))
            {
                int found = _filteredStudents.FindIndex(s => s.MaSv == selectMaSv);
                if (found >= 0) targetIndex = found;
            }

            dgvSinhVien.ClearSelection();
            dgvSinhVien.Rows[targetIndex].Selected = true;
            OnStudentSelected(_filteredStudents[targetIndex]);
        }
        else
        {
            ClearStudentInfo();
            ClearDiemInfo();
        }
    }

    private void lstLop_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void txtTimKiem_TextChanged(object? sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void dgvSinhVien_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvSinhVien.SelectedRows.Count > 0)
        {
            int index = dgvSinhVien.SelectedRows[0].Index;
            if (index >= 0 && index < _filteredStudents.Count)
            {
                OnStudentSelected(_filteredStudents[index]);
            }
        }
    }

    private void OnStudentSelected(SinhVien sv)
    {
        _selectedStudent = sv;

        // Điền vào GroupBox Thông tin sinh viên
        txtMaSV.Text = sv.MaSv;
        txtHoTen.Text = sv.HoTen;
        dtpNgaySinh.Value = sv.NgaySinh;
        if (sv.GioiTinh.Trim().Equals("Nam", StringComparison.OrdinalIgnoreCase))
        {
            radNam.Checked = true;
        }
        else
        {
            radNu.Checked = true;
        }

        cboLop.SelectedValue = sv.MaLop;

        // Tải bảng điểm của sinh viên
        LoadDiemSinhVien(sv.MaSv);
    }

    private void ClearStudentInfo()
    {
        _selectedStudent = null;
        txtMaSV.Text = string.Empty;
        txtHoTen.Text = string.Empty;
        dtpNgaySinh.Value = DateTime.Now;
        radNam.Checked = true;
    }

    private void LoadDiemSinhVien(string maSv)
    {
        _studentSubjects = _context.DiemMons
            .Where(d => d.MaSv == maSv)
            .OrderBy(d => d.MaDiem)
            .ToList();

        dgvDiem.Rows.Clear();
        int stt = 1;
        foreach (var dm in _studentSubjects)
        {
            int rowIndex = dgvDiem.Rows.Add(
                stt++,
                dm.TenMon,
                dm.SoTinChi,
                dm.Diem.ToString("0.0", _viCulture)
            );
            dgvDiem.Rows[rowIndex].Tag = dm;
        }

        ClearDiemInput();
        CalculateAndDisplayStats();

        if (dgvDiem.Rows.Count > 0)
        {
            dgvDiem.ClearSelection();
            dgvDiem.Rows[0].Selected = true;
            if (dgvDiem.Rows[0].Tag is DiemMon dm)
            {
                OnSubjectSelected(dm);
            }
        }
    }

    private void ClearDiemInfo()
    {
        _studentSubjects.Clear();
        dgvDiem.Rows.Clear();
        ClearDiemInput();
        CalculateAndDisplayStats();
    }

    private void ClearDiemInput()
    {
        _selectedSubject = null;
        txtTenMon.Text = string.Empty;
        nudSoTC.Value = 3;
        nudDiem.Value = 8.0m;
    }

    private void dgvDiem_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvDiem.SelectedRows.Count > 0)
        {
            var dm = dgvDiem.SelectedRows[0].Tag as DiemMon;
            if (dm != null)
            {
                OnSubjectSelected(dm);
            }
        }
    }

    private void OnSubjectSelected(DiemMon dm)
    {
        _selectedSubject = dm;
        txtTenMon.Text = dm.TenMon;
        nudSoTC.Value = Math.Clamp(dm.SoTinChi, nudSoTC.Minimum, nudSoTC.Maximum);
        nudDiem.Value = Math.Clamp(dm.Diem, nudDiem.Minimum, nudDiem.Maximum);
    }

    private void CalculateAndDisplayStats()
    {
        if (_studentSubjects.Count == 0)
        {
            lblTongTinChi.Text = "Tổng tín chỉ: 0";
            lblDiemTB.Text = "Điểm TB: Chưa có điểm";
            lblXepLoai.Text = "Xếp loại: Chưa có điểm";
            return;
        }

        int tongTC = _studentSubjects.Sum(d => d.SoTinChi);
        decimal tongDiemWeighted = _studentSubjects.Sum(d => d.Diem * d.SoTinChi);
        decimal diemTb = tongTC > 0 ? Math.Round(tongDiemWeighted / tongTC, 2, MidpointRounding.AwayFromZero) : 0m;

        string xepLoai = GetXepLoai(diemTb);

        lblTongTinChi.Text = $"Tổng tín chỉ: {tongTC}";
        lblDiemTB.Text = $"Điểm TB: {diemTb.ToString("0.00", _viCulture)}";
        lblXepLoai.Text = $"Xếp loại: {xepLoai}";
    }

    private static string GetXepLoai(decimal diemTb)
    {
        if (diemTb >= 8.0m) return "Giỏi";
        if (diemTb >= 7.0m) return "Khá";
        if (diemTb >= 5.0m) return "Trung bình";
        return "Yếu";
    }

    #region CRUD Sinh Viên

    private void btnThemSV_Click(object? sender, EventArgs e)
    {
        string maSv = txtMaSV.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSv))
        {
            MessageBox.Show("Mã số sinh viên (MSSV) không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaSV.Focus();
            return;
        }

        if (_context.SinhViens.Any(s => s.MaSv == maSv))
        {
            MessageBox.Show($"Mã số sinh viên '{maSv}' đã tồn tại trong cơ sở dữ liệu! Vui lòng chọn mã khác.", "Trùng mã sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaSV.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(hoTen))
        {
            MessageBox.Show("Họ tên sinh viên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }

        if (cboLop.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn lớp cho sinh viên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var newSv = new SinhVien
        {
            MaSv = maSv,
            HoTen = hoTen,
            NgaySinh = dtpNgaySinh.Value.Date,
            GioiTinh = radNam.Checked ? "Nam" : "Nữ",
            MaLop = cboLop.SelectedValue.ToString()!
        };

        try
        {
            _context.SinhViens.Add(newSv);
            _context.SaveChanges();
            MessageBox.Show($"Thêm sinh viên '{hoTen}' ({maSv}) thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadSinhVienList(maSv);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi thêm sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSuaSV_Click(object? sender, EventArgs e)
    {
        if (_selectedStudent == null)
        {
            MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string hoTen = txtHoTen.Text.Trim();
        if (string.IsNullOrWhiteSpace(hoTen))
        {
            MessageBox.Show("Họ tên sinh viên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }

        if (cboLop.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn lớp cho sinh viên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var svInDb = _context.SinhViens.Find(_selectedStudent.MaSv);
        if (svInDb == null)
        {
            MessageBox.Show("Không tìm thấy thông tin sinh viên trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            svInDb.HoTen = hoTen;
            svInDb.NgaySinh = dtpNgaySinh.Value.Date;
            svInDb.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
            svInDb.MaLop = cboLop.SelectedValue.ToString()!;

            _context.SaveChanges();
            MessageBox.Show($"Cập nhật sinh viên '{svInDb.MaSv}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadSinhVienList(svInDb.MaSv);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi cập nhật sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnXoaSV_Click(object? sender, EventArgs e)
    {
        if (_selectedStudent == null)
        {
            MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa sinh viên '{_selectedStudent.HoTen}' (MSSV: {_selectedStudent.MaSv})?\n\nLưu ý: Tất cả các điểm môn học của sinh viên này cũng sẽ bị xóa.",
            "Xác nhận xóa sinh viên",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm != DialogResult.Yes) return;

        try
        {
            // Xóa các dòng điểm của sinh viên đó trước
            var diems = _context.DiemMons.Where(d => d.MaSv == _selectedStudent.MaSv).ToList();
            if (diems.Count > 0)
            {
                _context.DiemMons.RemoveRange(diems);
            }

            var svInDb = _context.SinhViens.Find(_selectedStudent.MaSv);
            if (svInDb != null)
            {
                _context.SinhViens.Remove(svInDb);
            }

            _context.SaveChanges();
            MessageBox.Show("Đã xóa sinh viên và toàn bộ điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadSinhVienList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi xóa sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region CRUD Điểm Môn

    private void btnThemMon_Click(object? sender, EventArgs e)
    {
        if (_selectedStudent == null)
        {
            MessageBox.Show("Vui lòng chọn sinh viên trước khi thêm môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string tenMon = txtTenMon.Text.Trim();
        if (string.IsNullOrWhiteSpace(tenMon))
        {
            MessageBox.Show("Tên môn học không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenMon.Focus();
            return;
        }

        var newDiem = new DiemMon
        {
            MaSv = _selectedStudent.MaSv,
            TenMon = tenMon,
            SoTinChi = (int)nudSoTC.Value,
            Diem = nudDiem.Value
        };

        try
        {
            _context.DiemMons.Add(newDiem);
            _context.SaveChanges();
            LoadDiemSinhVien(_selectedStudent.MaSv);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi thêm môn học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSuaDiem_Click(object? sender, EventArgs e)
    {
        if (_selectedStudent == null || _selectedSubject == null)
        {
            MessageBox.Show("Vui lòng chọn môn học trong bảng điểm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string tenMon = txtTenMon.Text.Trim();
        if (string.IsNullOrWhiteSpace(tenMon))
        {
            MessageBox.Show("Tên môn học không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenMon.Focus();
            return;
        }

        var dmInDb = _context.DiemMons.Find(_selectedSubject.MaDiem);
        if (dmInDb == null)
        {
            MessageBox.Show("Không tìm thấy môn học trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            dmInDb.TenMon = tenMon;
            dmInDb.SoTinChi = (int)nudSoTC.Value;
            dmInDb.Diem = nudDiem.Value;

            _context.SaveChanges();
            LoadDiemSinhVien(_selectedStudent.MaSv);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi sửa điểm môn học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnXoaMon_Click(object? sender, EventArgs e)
    {
        if (_selectedStudent == null || _selectedSubject == null)
        {
            MessageBox.Show("Vui lòng chọn môn học cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa môn '{_selectedSubject.TenMon}' của sinh viên {_selectedStudent.HoTen}?",
            "Xác nhận xóa môn",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm != DialogResult.Yes) return;

        var dmInDb = _context.DiemMons.Find(_selectedSubject.MaDiem);
        if (dmInDb != null)
        {
            try
            {
                _context.DiemMons.Remove(dmInDb);
                _context.SaveChanges();
                LoadDiemSinhVien(_selectedStudent.MaSv);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa môn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    #endregion

    #region In Phiếu Báo Điểm

    private PhieuDiemPrinter? CreatePrinterForSelectedStudent()
    {
        if (_selectedStudent == null)
        {
            MessageBox.Show("Vui lòng chọn một sinh viên để in phiếu báo điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        if (_studentSubjects.Count == 0)
        {
            MessageBox.Show($"Sinh viên '{_selectedStudent.HoTen}' ({_selectedStudent.MaSv}) chưa có điểm môn nào, không thể in phiếu báo điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        int tongTC = _studentSubjects.Sum(d => d.SoTinChi);
        decimal tongDiemWeighted = _studentSubjects.Sum(d => d.Diem * d.SoTinChi);
        decimal diemTb = tongTC > 0 ? Math.Round(tongDiemWeighted / tongTC, 2, MidpointRounding.AwayFromZero) : 0m;
        string xepLoai = GetXepLoai(diemTb);

        var printer = new PhieuDiemPrinter(_selectedStudent, _studentSubjects, tongTC, diemTb, xepLoai);

        // Áp dụng cấu hình trang hiện tại
        printer.Document.DefaultPageSettings = (PageSettings)_pageSettings.Clone();
        printer.Document.PrinterSettings = (PrinterSettings)_printerSettings.Clone();

        return printer;
    }

    private void btnXemTruoc_Click(object? sender, EventArgs e)
    {
        var printer = CreatePrinterForSelectedStudent();
        if (printer == null) return;

        try
        {
            printer.Preview(this);
        }
        catch (InvalidPrinterException ex)
        {
            MessageBox.Show($"Máy tính chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết lỗi: {ex.Message}", "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Đã xảy ra lỗi khi mở bản xem trước: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnIn_Click(object? sender, EventArgs e)
    {
        var printer = CreatePrinterForSelectedStudent();
        if (printer == null) return;

        try
        {
            printer.Print(this);
        }
        catch (InvalidPrinterException ex)
        {
            MessageBox.Show($"Máy tính chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết lỗi: {ex.Message}", "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Đã xảy ra lỗi khi in phiếu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCaiDatTrang_Click(object? sender, EventArgs e)
    {
        try
        {
            using var pageSetupDialog = new PageSetupDialog
            {
                PageSettings = _pageSettings,
                PrinterSettings = _printerSettings,
                EnableMetric = true
            };

            if (pageSetupDialog.ShowDialog(this) == DialogResult.OK)
            {
                _pageSettings = pageSetupDialog.PageSettings;
                _printerSettings = pageSetupDialog.PrinterSettings;
            }
        }
        catch (InvalidPrinterException ex)
        {
            MessageBox.Show($"Máy tính chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết lỗi: {ex.Message}", "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Đã xảy ra lỗi khi cài đặt trang: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region Xuất Excel

    private void btnXuatExcel_Click(object? sender, EventArgs e)
    {
        var selectedLopItem = lstLop.SelectedItem as LopItem;
        string? maLop = selectedLopItem?.MaLop;

        // Nếu đang chọn "Tất cả", thử lấy mã lớp từ sinh viên đang chọn hoặc yêu cầu người dùng chọn
        if (string.IsNullOrEmpty(maLop))
        {
            if (_selectedStudent != null)
            {
                maLop = _selectedStudent.MaLop;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp cụ thể từ danh sách 'Lớp' bên trái để xuất điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel Workbook (*.xlsx)|*.xlsx",
            Title = $"Xuất bảng điểm lớp {maLop}",
            FileName = $"BangDiem_{maLop}_{DateTime.Now:yyyyMMdd}.xlsx"
        };

        if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                int count = ExcelExportService.ExportDiemLop(maLop, saveFileDialog.FileName);

                var openResult = MessageBox.Show(
                    $"Xuất Excel thành công cho lớp {maLop} ({count} môn học)!\nBạn có muốn mở file ngay bây giờ không?",
                    "Xuất Excel thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (openResult == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}", "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    #endregion
}
