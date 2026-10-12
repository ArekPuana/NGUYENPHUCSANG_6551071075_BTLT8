using System.Globalization;

namespace Cau1_TiemTapHoa;

/// <summary>Cấu hình dùng chung: chuỗi kết nối, culture, nơi lưu mẫu report.</summary>
public static class AppConfig
{
    // Mặc định kết nối SQL Server (SQLEXPRESS) giống project mẫu.
    // Nếu dùng LocalDB, đổi thành: @"Server=(localdb)\MSSQLLocalDB;Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True"
    public const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True";

    public const string TenCuaHang = "TẠP HÓA NHÀ TRƯỜNG UTC2";

    // Tên file mẫu report (.frx) trong thư mục Reports
    public const string FrxBangGia = "BangGia.frx";

    // Danh mục cố định cho ComboBox
    public static readonly string[] DsLoaiHang = { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" };
    public static readonly string[] DsDonVi = { "Chai", "Gói", "Hộp", "Túi" };

    public static readonly CultureInfo Vi = new("vi-VN");

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
