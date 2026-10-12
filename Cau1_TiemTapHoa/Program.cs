using Cau1_TiemTapHoa.Helpers;
using Cau1_TiemTapHoa.Models;
using Cau1_TiemTapHoa.Services;
using ClosedXML.Excel;

namespace Cau1_TiemTapHoa;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--test")
        {
            RunTests();
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new Forms.FormHangHoa());
    }

    private static void RunTests()
    {
        Console.WriteLine("=== BẮT ĐẦU KIỂM TRA HỆ THỐNG ===");
        var svc = new HangHoaService();

        // 1. Kiểm tra CSDL đủ 6 dòng
        var all = svc.TimKiemAsync("", null, false).GetAwaiter().GetResult();
        Console.WriteLine($"[1] Toàn bộ hàng hóa: {all.Count} (Kỳ vọng: 6)");
        foreach (var h in all)
        {
            Console.WriteLine($"    - {h.MaHang}: {h.TenHang} | {h.LoaiHang} | {h.DonVi} | {h.GiaBan:N0}đ | Tồn: {h.SoLuongTon} | KD: {h.ConKinhDoanh}");
        }

        // 2. Tìm kiếm 'nước'
        var nuoc = svc.TimKiemAsync("nước", null, false).GetAwaiter().GetResult();
        Console.WriteLine($"[2] Tìm 'nước': {nuoc.Count} mặt hàng (Kỳ vọng: 2)");
        foreach (var h in nuoc) Console.WriteLine($"    - {h.TenHang}");

        // 3. Tìm 'nước' + còn kinh doanh
        var nuocKd = svc.TimKiemAsync("nước", null, true).GetAwaiter().GetResult();
        Console.WriteLine($"[3] Tìm 'nước' + Còn kinh doanh: {nuocKd.Count} mặt hàng (Kỳ vọng: 1)");
        foreach (var h in nuocKd) Console.WriteLine($"    - {h.TenHang}");

        // 4. Lấy hàng còn kinh doanh
        var kd = svc.LayHangConKinhDoanhAsync().GetAwaiter().GetResult();
        Console.WriteLine($"[4] Hàng còn kinh doanh: {kd.Count} mặt hàng (Kỳ vọng: 5)");

        // 5. Test FastReport BangGia.frx + PDF Export
        Console.WriteLine("[5] Kiểm tra FastReport...");
        var dt = DataTableHelper.ToDataTable(kd, "HangHoa");
        using var r = ReportService.TaoReport(AppConfig.FrxBangGia);
        ReportService.DangKy(r, dt, "HangHoa");
        ReportService.DatThamSo(r, "NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
        string pdfPath = Path.Combine(AppContext.BaseDirectory, "test_BangGia.pdf");
        ReportService.XuatPdf(r, pdfPath);
        var pdfInfo = new FileInfo(pdfPath);
        Console.WriteLine($"    - Đã xuất PDF: {pdfPath}, dung lượng = {pdfInfo.Length} bytes");

        // 6. Test Excel ClosedXML (Tồn kho có công thức + SUM)
        Console.WriteLine("[6] Kiểm tra Excel tồn kho...");
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
        var dong = kd.Select(x => new object?[]
        {
            x.MaHang, x.TenHang, x.LoaiHang, x.DonVi, x.GiaBan, x.SoLuongTon, x.NgayNhap, null
        });
        string xlsxPath = Path.Combine(AppContext.BaseDirectory, "test_TonKho.xlsx");
        ExcelExporter.Xuat(xlsxPath, "TonKho", cot, dong);

        using var wb = new XLWorkbook(xlsxPath);
        var ws = wb.Worksheet("TonKho");
        for (int row = 1; row <= 7; row++)
        {
            var parts = new List<string>();
            for (int col = 1; col <= 8; col++)
            {
                var cell = ws.Cell(row, col);
                if (cell.HasFormula)
                    parts.Add($"[={cell.FormulaA1} -> {cell.Value}]");
                else
                    parts.Add(cell.GetString());
            }
            Console.WriteLine($"Row {row}: " + string.Join(" | ", parts));
        }
        Console.WriteLine("=== HOÀN TẤT KIỂM TRA ===");
    }
}
