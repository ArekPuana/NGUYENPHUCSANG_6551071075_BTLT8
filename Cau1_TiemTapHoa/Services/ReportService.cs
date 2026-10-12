using FastReport;
using FastReport.Data;
using FastReport.Export.Pdf;
using System.Data;

namespace Cau1_TiemTapHoa.Services;

public static class ReportService
{
    public static bool CoMau(string tenFile) => File.Exists(AppConfig.DuongDanReport(tenFile));

    public static Report TaoReport(string tenFile)
    {
        var path = AppConfig.DuongDanReport(tenFile);
        var report = new Report();

        if (File.Exists(path))
        {
            report.Load(path);
        }
        else
        {
            var page = new ReportPage();
            report.Pages.Add(page);
            page.CreateUniqueName();
        }

        report.FileName = path;
        report.DoublePass = true;   // để [TotalPages#] tính đúng
        return report;
    }

    /// <summary>Đăng ký DataTable vào report và bật Enabled = true.</summary>
    public static void DangKy(Report report, DataTable bang, string ten)
    {
        report.RegisterData(bang, ten);
        report.GetDataSource(ten).Enabled = true;
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
            report.ShowPrepared(true);   // true = modal
    }

    public static void ThietKe(Report report) => report.Design(true);

    public static void XuatPdf(Report report, string filePath)
    {
        report.Prepare();
        var pdf = new PDFExport
        {
            EmbeddingFonts = true
        };
        report.Export(pdf, filePath);
    }
}
