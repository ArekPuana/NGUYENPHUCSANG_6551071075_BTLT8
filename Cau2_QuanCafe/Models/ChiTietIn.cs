namespace Cau2_QuanCafe.Models;

public class ChiTietIn
{
    public int STT { get; set; }
    public int MaCT { get; set; }
    public int MaMon { get; set; }
    public string TenMon { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLuong * DonGia;
}
