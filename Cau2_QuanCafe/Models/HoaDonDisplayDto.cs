namespace Cau2_QuanCafe.Models;

public class HoaDonDisplayDto
{
    public int MaHD { get; set; }
    public string MaHienThi => AppConfig.DinhDangMaHd(MaHD);
    public DateTime NgayLap { get; set; }
    public int SoBan { get; set; }
    public string HinhThucTT { get; set; } = string.Empty;
    public string? GhiChu { get; set; }
    public decimal TongTien { get; set; }
}
