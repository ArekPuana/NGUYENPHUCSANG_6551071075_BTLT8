using FastReport;
using FastReport.Data;
using FastReport.Export.Pdf;
using Cau2_QuanCafe.Models;

namespace Cau2_QuanCafe.Services;

public static class ReportService
{
    public static Report TaoReport(HoaDon hd, List<ChiTietIn> dsChiTiet)
    {
        var path = AppConfig.DuongDanReport(AppConfig.FrxHoaDon);
        var report = new Report();

        if (File.Exists(path))
        {
            report.Load(path);
        }
        else
        {
            throw new FileNotFoundException($"Không tìm thấy file mẫu báo cáo tại: {path}");
        }

        report.FileName = path;
        report.DoublePass = true; // để [TotalPages#] hiển thị chính xác

        // Đăng ký danh sách ChiTietIn với tên "ChiTiet"
        report.RegisterData(dsChiTiet, "ChiTiet");
        var ds = report.GetDataSource("ChiTiet");
        if (ds != null)
        {
            ds.Enabled = true;
        }

        // Truyền các tham số từ code
        DatThamSo(report, "TenQuan", AppConfig.TenQuan);
        DatThamSo(report, "SoHD", AppConfig.DinhDangMaHd(hd.MaHd));
        DatThamSo(report, "NgayLap", hd.NgayLap.ToString("dd/MM/yyyy HH:mm"));
        DatThamSo(report, "SoBan", hd.SoBan.ToString());
        DatThamSo(report, "HinhThucTT", hd.HinhThucTt);

        decimal tongCong = dsChiTiet.Sum(c => c.ThanhTien);
        DatThamSo(report, "TongCong", AppConfig.DinhDangTien(tongCong));

        return report;
    }

    public static void DatThamSo(Report report, string ten, object? giaTri)
    {
        var p = report.GetParameter(ten);
        if (p == null)
        {
            p = new Parameter { Name = ten };
            report.Dictionary.Parameters.Add(p);
        }
        p.Value = giaTri;
    }

    public static void XemTruoc(Report report)
    {
        if (report.Prepare())
        {
            report.ShowPrepared(true); // modal dialog
        }
    }

    public static void ThietKe(Report report)
    {
        report.Design(true); // Mở FastReport Designer trực quan
    }

    public static void XuatPdf(Report report, string filePath)
    {
        report.Prepare();
        using var pdf = new PDFExport
        {
            EmbeddingFonts = true
        };
        report.Export(pdf, filePath);
    }

    public static void In(Report report)
    {
        if (report.Prepare())
        {
            report.PrintPrepared();
        }
    }
}
