using System.Globalization;

namespace Cau2_QuanCafe;

/// <summary>
/// Cấu hình dùng chung: chuỗi kết nối, tên quán, culture, nơi lưu mẫu report.
/// </summary>
public static class AppConfig
{
    public const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=QuanCaPhe;Trusted_Connection=True;TrustServerCertificate=True";

    public const string TenQuan = "QUÁN CÀ PHÊ GÓC PHỐ";

    public const string FrxHoaDon = "HoaDon.frx";

    public static readonly CultureInfo Vi = new("vi-VN");

    public static string DinhDangTien(decimal tien) => tien.ToString("N0", Vi);

    public static string DinhDangMaHd(int maHd) => $"HD{maHd:D6}";

    /// <summary>
    /// Thư mục chứa các file .frx.
    /// Khi chạy từ Visual Studio, exe nằm ở bin\Debug\net10.0-windows\ nên ta đi ngược lên
    /// tới thư mục chứa file .csproj và dùng thư mục Reports\ ở đó.
    /// </summary>
    public static string ThuMucReport { get; } = TimThuMucReport();

    public static string DuongDanReport(string tenFile) => Path.Combine(ThuMucReport, tenFile);

    private static string TimThuMucReport()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (dir.GetFiles("*.csproj").Length > 0)
            {
                var p = Path.Combine(dir.FullName, "Reports");
                Directory.CreateDirectory(p);
                return p;
            }
            dir = dir.Parent;
        }

        var macDinh = Path.Combine(AppContext.BaseDirectory, "Reports");
        Directory.CreateDirectory(macDinh);
        return macDinh;
    }
}
