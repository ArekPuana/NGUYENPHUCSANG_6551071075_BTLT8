using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using Cau3_PhieuBaoDiem.Models;

namespace Cau3_PhieuBaoDiem.Printing;

public class PhieuDiemPrinter
{
    public SinhVien Student { get; set; }
    public List<DiemMon> Subjects { get; set; } = new();
    public string TenKhoa { get; set; } = "KHOA CÔNG NGHỆ THÔNG TIN";
    public int TongTinChi { get; set; }
    public decimal DiemTb { get; set; }
    public string XepLoai { get; set; } = string.Empty;

    public PrintDocument Document { get; private set; }

    private int _currentPage = 1;
    private int _totalPages = 1;
    private int _currentSubjectIndex = 0;

    private readonly CultureInfo _viCulture = CultureInfo.GetCultureInfo("vi-VN");

    public PhieuDiemPrinter(SinhVien student, List<DiemMon> subjects, int tongTinChi, decimal diemTb, string xepLoai)
    {
        Student = student;
        Subjects = subjects ?? new List<DiemMon>();
        TongTinChi = tongTinChi;
        DiemTb = diemTb;
        XepLoai = xepLoai;

        Document = new PrintDocument();
        Document.DocumentName = $"PhieuBaoDiem_{Student.MaSv}";
        Document.DefaultPageSettings.Margins = new Margins(60, 60, 60, 60);

        Document.BeginPrint += Document_BeginPrint;
        Document.PrintPage += Document_PrintPage;
    }

    private void Document_BeginPrint(object? sender, PrintEventArgs e)
    {
        _currentPage = 1;
        _currentSubjectIndex = 0;

        // Tính toán tổng số trang dựa trên MarginBounds thực tế của DefaultPageSettings
        Rectangle pageBounds = Document.DefaultPageSettings.Bounds;
        Margins margins = Document.DefaultPageSettings.Margins;
        Rectangle printableArea = new Rectangle(
            pageBounds.Left + margins.Left,
            pageBounds.Top + margins.Top,
            Math.Max(100, pageBounds.Width - margins.Left - margins.Right),
            Math.Max(100, pageBounds.Height - margins.Top - margins.Bottom)
        );

        _totalPages = CalculateTotalPages(printableArea);
        if (_totalPages < 1) _totalPages = 1;
    }

    private float GetPage1HeaderHeight()
    {
        // Tên khoa (30) + Tiêu đề (45) + Thông tin SV (4 dòng * 22 = 88) + Cách (15) + Header bảng (28)
        return 30f + 45f + 88f + 15f + 28f;
    }

    private float GetContinuedHeaderHeight()
    {
        // Trang tiếp theo: Tiêu đề nhỏ/nhắc SV (35) + Header bảng (28)
        return 35f + 28f;
    }

    private int CalculateTotalPages(Rectangle bounds)
    {
        if (Subjects.Count == 0) return 1;

        int pageCount = 1;
        float rowHeight = 28f;
        float lastPageRequired = 140f; // Điểm trung bình + 2 chữ ký
        float pageBottomLimit = bounds.Bottom - 30f; // Chừa chỗ cho "Trang x/y"

        float currentY = bounds.Top + GetPage1HeaderHeight();

        for (int i = 0; i < Subjects.Count; i++)
        {
            bool isLast = (i == Subjects.Count - 1);
            float spaceNeeded = rowHeight + (isLast ? lastPageRequired : 0);

            if (currentY + spaceNeeded > pageBottomLimit)
            {
                pageCount++;
                currentY = bounds.Top + GetContinuedHeaderHeight() + rowHeight;
            }
            else
            {
                currentY += rowHeight;
            }
        }

        return pageCount;
    }

    private void Document_PrintPage(object? sender, PrintPageEventArgs e)
    {
        Graphics g = e.Graphics!;
        Rectangle bounds = e.MarginBounds;

        using Font fontFaculty = new Font("Arial", 10, FontStyle.Regular);
        using Font fontTitle = new Font("Arial", 16, FontStyle.Bold);
        using Font fontSubTitle = new Font("Arial", 11, FontStyle.Italic);
        using Font fontInfo = new Font("Arial", 10.5f, FontStyle.Regular);
        using Font fontInfoBold = new Font("Arial", 10.5f, FontStyle.Bold);
        using Font fontTableHeader = new Font("Arial", 10, FontStyle.Bold);
        using Font fontTableBody = new Font("Arial", 10, FontStyle.Regular);
        using Font fontSummary = new Font("Arial", 11, FontStyle.Bold);
        using Font fontSignTitle = new Font("Arial", 10.5f, FontStyle.Bold);
        using Font fontSignNote = new Font("Arial", 9.5f, FontStyle.Italic);
        using Font fontFooter = new Font("Arial", 9, FontStyle.Italic);

        using StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        using StringFormat sfLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        using StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        float rowHeight = 28f;
        float colSttWidth = 50f;
        float colTcWidth = 65f;
        float colDiemWidth = 65f;
        float colMonWidth = bounds.Width - (colSttWidth + colTcWidth + colDiemWidth);

        float colSttX = bounds.Left;
        float colMonX = colSttX + colSttWidth;
        float colTcX = colMonX + colMonWidth;
        float colDiemX = colTcX + colTcWidth;

        float currentY = bounds.Top;

        // 1. In phần đầu trang (Header)
        if (_currentPage == 1)
        {
            // Tên khoa
            g.DrawString(TenKhoa, fontFaculty, Brushes.Black, bounds.Left, currentY);
            currentY += 28f;

            // Tiêu đề PHIẾU BÁO ĐIỂM
            SizeF titleSize = g.MeasureString("PHIẾU BÁO ĐIỂM", fontTitle);
            float titleX = bounds.Left + (bounds.Width - titleSize.Width) / 2f;
            g.DrawString("PHIẾU BÁO ĐIỂM", fontTitle, Brushes.Black, titleX, currentY);
            currentY += titleSize.Height + 12f;

            // Thông tin sinh viên
            float infoLineHeight = 22f;
            DrawStudentInfoLine(g, "MSSV:", Student.MaSv, fontInfoBold, fontInfo, bounds.Left, currentY);
            currentY += infoLineHeight;

            DrawStudentInfoLine(g, "Họ tên:", Student.HoTen, fontInfoBold, fontInfo, bounds.Left, currentY);
            currentY += infoLineHeight;

            DrawStudentInfoLine(g, "Ngày sinh:", Student.NgaySinh.ToString("dd/MM/yyyy"), fontInfoBold, fontInfo, bounds.Left, currentY);
            currentY += infoLineHeight;

            DrawStudentInfoLine(g, "Lớp:", Student.MaLop, fontInfoBold, fontInfo, bounds.Left, currentY);
            currentY += infoLineHeight + 15f;
        }
        else
        {
            // Trang tiếp theo
            string subHeader = $"PHIẾU BÁO ĐIỂM (Tiếp theo) - SV: {Student.HoTen} ({Student.MaSv}) - Lớp: {Student.MaLop}";
            g.DrawString(subHeader, fontSubTitle, Brushes.Black, bounds.Left, currentY);
            currentY += 28f;
        }

        // 2. In tiêu đề bảng (Table Header)
        RectangleF rectHStt = new RectangleF(colSttX, currentY, colSttWidth, rowHeight);
        RectangleF rectHMon = new RectangleF(colMonX, currentY, colMonWidth, rowHeight);
        RectangleF rectHTc = new RectangleF(colTcX, currentY, colTcWidth, rowHeight);
        RectangleF rectHDiem = new RectangleF(colDiemX, currentY, colDiemWidth, rowHeight);

        g.DrawRectangle(Pens.Black, rectHStt.X, rectHStt.Y, rectHStt.Width, rectHStt.Height);
        g.DrawRectangle(Pens.Black, rectHMon.X, rectHMon.Y, rectHMon.Width, rectHMon.Height);
        g.DrawRectangle(Pens.Black, rectHTc.X, rectHTc.Y, rectHTc.Width, rectHTc.Height);
        g.DrawRectangle(Pens.Black, rectHDiem.X, rectHDiem.Y, rectHDiem.Width, rectHDiem.Height);

        g.DrawString("STT", fontTableHeader, Brushes.Black, rectHStt, sfCenter);
        g.DrawString("Môn học", fontTableHeader, Brushes.Black, rectHMon, sfCenter);
        g.DrawString("Số TC", fontTableHeader, Brushes.Black, rectHTc, sfCenter);
        g.DrawString("Điểm", fontTableHeader, Brushes.Black, rectHDiem, sfCenter);

        currentY += rowHeight;

        // 3. In các dòng dữ liệu môn học
        float lastPageRequired = 140f;
        float pageBottomLimit = bounds.Bottom - 30f;

        while (_currentSubjectIndex < Subjects.Count)
        {
            bool isLastSubject = (_currentSubjectIndex == Subjects.Count - 1);
            float spaceNeeded = rowHeight + (isLastSubject ? lastPageRequired : 0);

            if (currentY + spaceNeeded > pageBottomLimit)
            {
                // Hết chỗ trên trang hiện tại -> Chuyển sang trang tiếp theo
                DrawFooter(g, bounds, _currentPage, _totalPages, fontFooter, sfRight);
                _currentPage++;
                e.HasMorePages = true;
                return;
            }

            var subject = Subjects[_currentSubjectIndex];
            int stt = _currentSubjectIndex + 1;

            RectangleF rectRStt = new RectangleF(colSttX, currentY, colSttWidth, rowHeight);
            RectangleF rectRMon = new RectangleF(colMonX, currentY, colMonWidth, rowHeight);
            RectangleF rectRTc = new RectangleF(colTcX, currentY, colTcWidth, rowHeight);
            RectangleF rectRDiem = new RectangleF(colDiemX, currentY, colDiemWidth, rowHeight);

            g.DrawRectangle(Pens.Black, rectRStt.X, rectRStt.Y, rectRStt.Width, rectRStt.Height);
            g.DrawRectangle(Pens.Black, rectRMon.X, rectRMon.Y, rectRMon.Width, rectRMon.Height);
            g.DrawRectangle(Pens.Black, rectRTc.X, rectRTc.Y, rectRTc.Width, rectRTc.Height);
            g.DrawRectangle(Pens.Black, rectRDiem.X, rectRDiem.Y, rectRDiem.Width, rectRDiem.Height);

            // STT căn giữa
            g.DrawString(stt.ToString(), fontTableBody, Brushes.Black, rectRStt, sfCenter);

            // Môn học căn trái với khoảng đệm
            RectangleF rectRMonPadded = new RectangleF(colMonX + 8f, currentY, colMonWidth - 16f, rowHeight);
            g.DrawString(subject.TenMon, fontTableBody, Brushes.Black, rectRMonPadded, sfLeft);

            // Số TC căn giữa
            g.DrawString(subject.SoTinChi.ToString(), fontTableBody, Brushes.Black, rectRTc, sfCenter);

            // Điểm căn giữa
            string diemStr = subject.Diem.ToString("0.0", _viCulture);
            g.DrawString(diemStr, fontTableBody, Brushes.Black, rectRDiem, sfCenter);

            _currentSubjectIndex++;
            currentY += rowHeight;
        }

        // 4. In Điểm trung bình và Chữ ký (Chỉ in ở trang cuối cùng)
        currentY += 15f;
        string summaryText = $"Điểm trung bình: {DiemTb.ToString("0.00", _viCulture)} - Xếp loại: {XepLoai}";
        SizeF summarySize = g.MeasureString(summaryText, fontSummary);
        float summaryX = bounds.Left + (bounds.Width - summarySize.Width) / 2f;
        g.DrawString(summaryText, fontSummary, Brushes.Black, summaryX, currentY);
        currentY += summarySize.Height + 25f;

        // Chữ ký: Giáo vụ khoa và Sinh viên
        float signWidth = bounds.Width / 2f;
        RectangleF rectGiaoVu = new RectangleF(bounds.Left, currentY, signWidth, 22f);
        RectangleF rectGiaoVuNote = new RectangleF(bounds.Left, currentY + 22f, signWidth, 20f);

        RectangleF rectSinhVien = new RectangleF(bounds.Left + signWidth, currentY, signWidth, 22f);
        RectangleF rectSinhVienNote = new RectangleF(bounds.Left + signWidth, currentY + 22f, signWidth, 20f);

        g.DrawString("Giáo vụ khoa", fontSignTitle, Brushes.Black, rectGiaoVu, sfCenter);
        g.DrawString("(Ký, ghi rõ họ tên)", fontSignNote, Brushes.Black, rectGiaoVuNote, sfCenter);

        g.DrawString("Sinh viên", fontSignTitle, Brushes.Black, rectSinhVien, sfCenter);
        g.DrawString("(Ký, ghi rõ họ tên)", fontSignNote, Brushes.Black, rectSinhVienNote, sfCenter);

        // 5. In số trang ở cuối
        DrawFooter(g, bounds, _currentPage, _totalPages, fontFooter, sfRight);

        e.HasMorePages = false;
    }

    private void DrawStudentInfoLine(Graphics g, string label, string value, Font fontLabel, Font fontValue, float x, float y)
    {
        g.DrawString(label, fontLabel, Brushes.Black, x, y);
        float labelWidth = g.MeasureString(label, fontLabel).Width;
        g.DrawString(value, fontValue, Brushes.Black, x + labelWidth + 4f, y);
    }

    private void DrawFooter(Graphics g, Rectangle bounds, int currentPage, int totalPages, Font fontFooter, StringFormat sfRight)
    {
        string footerText = $"Trang {currentPage}/{totalPages}";
        RectangleF rectFooter = new RectangleF(bounds.Left, bounds.Bottom - 25f, bounds.Width, 25f);
        g.DrawString(footerText, fontFooter, Brushes.Black, rectFooter, sfRight);
    }

    public void Preview(IWin32Window? owner = null)
    {
        using var previewDialog = new PrintPreviewDialog
        {
            Document = Document,
            Width = 950,
            Height = 700,
            StartPosition = FormStartPosition.CenterParent
        };
        previewDialog.ShowDialog(owner);
    }

    public void Print(IWin32Window? owner = null)
    {
        using var printDialog = new PrintDialog
        {
            Document = Document,
            UseEXDialog = true
        };

        if (printDialog.ShowDialog(owner) == DialogResult.OK)
        {
            Document.Print();
        }
    }

    public void PageSetup(IWin32Window? owner = null)
    {
        using var pageSetupDialog = new PageSetupDialog
        {
            Document = Document,
            PageSettings = Document.DefaultPageSettings,
            PrinterSettings = Document.PrinterSettings,
            EnableMetric = true
        };

        if (pageSetupDialog.ShowDialog(owner) == DialogResult.OK)
        {
            Document.DefaultPageSettings = pageSetupDialog.PageSettings;
            Document.PrinterSettings = pageSetupDialog.PrinterSettings;
        }
    }
}
