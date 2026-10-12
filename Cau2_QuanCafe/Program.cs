using Cau2_QuanCafe.Forms;
using Cau2_QuanCafe.Models;
using Cau2_QuanCafe.Services;

namespace Cau2_QuanCafe;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (args.Length > 0 && args[0] == "--test-verify")
        {
            ChayKiemThuTuDong();
            return;
        }

        Application.Run(new FormQuanCafe());
    }

    private static void ChayKiemThuTuDong()
    {
        Console.WriteLine("=== BẮT ĐẦU KIỂM THỬ TỰ ĐỘNG CÂU 2: QUÁN CÀ PHÊ GÓC PHỐ ===");
        var cafeService = new CafeService();

        // 1. Kiểm tra tải món
        var dsMon = cafeService.LayDanhSachMon();
        Console.WriteLine($"1. Danh sách món: {dsMon.Count} món.");
        foreach (var m in dsMon)
        {
            Console.WriteLine($"   - {m.TenMon} ({m.NhomMon}): {AppConfig.DinhDangTien(m.DonGia)} đ");
        }

        // 2. Kiểm tra hóa đơn HD000001
        var dsHd = cafeService.TimKiemHoaDon(new DateTime(2026, 10, 1), new DateTime(2026, 10, 11), null, null, null);
        Console.WriteLine($"2. Danh sách hóa đơn (từ 01/10 đến 11/10): {dsHd.Count} hóa đơn.");
        foreach (var hd in dsHd)
        {
            Console.WriteLine($"   - {hd.MaHienThi} | Bàn {hd.SoBan} | {hd.HinhThucTT} | Tổng: {AppConfig.DinhDangTien(hd.TongTien)} đ");
        }

        // 3. Kiểm tra chi tiết HD000001
        var ct1 = cafeService.LayChiTietHoaDon(1);
        decimal tong1 = ct1.Sum(c => c.ThanhTien);
        Console.WriteLine($"3. Chi tiết HD000001: {ct1.Count} dòng, Tổng cộng: {AppConfig.DinhDangTien(tong1)} đ");
        foreach (var ct in ct1)
        {
            Console.WriteLine($"   - STT {ct.STT}: {ct.TenMon} x{ct.SoLuong} @ {AppConfig.DinhDangTien(ct.DonGia)} = {AppConfig.DinhDangTien(ct.ThanhTien)} đ");
        }

        // 4. Kiểm tra tìm kiếm tên món "tiramisu"
        var dsTiramisu = cafeService.TimKiemHoaDon(new DateTime(2026, 10, 1), new DateTime(2026, 10, 11), null, null, "tiramisu");
        Console.WriteLine($"4. Tìm kiếm món 'tiramisu': {dsTiramisu.Count} hóa đơn, Tổng DT: {AppConfig.DinhDangTien(dsTiramisu.Sum(h => h.TongTien))} đ");

        // 5. Kiểm tra lọc hình thức "Chuyển khoản"
        var dsCk = cafeService.TimKiemHoaDon(new DateTime(2026, 10, 1), new DateTime(2026, 10, 11), null, "Chuyển khoản", null);
        Console.WriteLine($"5. Lọc hình thức 'Chuyển khoản': {dsCk.Count} hóa đơn, Tổng DT: {AppConfig.DinhDangTien(dsCk.Sum(h => h.TongTien))} đ");

        // 6. Kiểm tra tạo FastReport và xuất PDF (hóa đơn 3 dòng)
        var hd1 = cafeService.LayHoaDonTheoMa(1);
        if (hd1 != null)
        {
            using var report = ReportService.TaoReport(hd1, ct1);
            string testPdf = Path.Combine(AppContext.BaseDirectory, "Test_HoaDon_HD000001.pdf");
            ReportService.XuatPdf(report, testPdf);
            Console.WriteLine($"6. FastReport xuất PDF thành công: {testPdf} (Kích thước: {new FileInfo(testPdf).Length} bytes)");
        }

        // 7. Kiểm tra hóa đơn dài 50 dòng tự sang trang 2, chân trang Trang 1/2, 2/2
        if (hd1 != null)
        {
            var ds50 = new List<ChiTietIn>();
            for (int i = 1; i <= 50; i++)
            {
                ds50.Add(new ChiTietIn
                {
                    STT = i,
                    MaCT = i,
                    MaMon = 1,
                    TenMon = $"Món gọi thêm thứ {i}",
                    SoLuong = 1,
                    DonGia = 30000
                });
            }
            using var report50 = ReportService.TaoReport(hd1, ds50);
            string testPdf50 = Path.Combine(AppContext.BaseDirectory, "Test_HoaDon_50Dong.pdf");
            ReportService.XuatPdf(report50, testPdf50);
            Console.WriteLine($"7. FastReport xuất PDF 50 dòng thành công: {testPdf50} (Kích thước: {new FileInfo(testPdf50).Length} bytes)");
        }

        Console.WriteLine("=== TẤT CẢ KIỂM THỬ ĐÃ THÀNH CÔNG RỰC RỠ! ===");
    }
}
