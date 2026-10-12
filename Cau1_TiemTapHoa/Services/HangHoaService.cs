using Microsoft.EntityFrameworkCore;
using Cau1_TiemTapHoa.Models;

namespace Cau1_TiemTapHoa.Services;

/// <summary>Mọi truy vấn/ghi CSDL bảng HangHoa (EF Core, bất đồng bộ).</summary>
public class HangHoaService
{
    /// <summary>Tìm kiếm: tên chứa từ khóa + lọc loại hàng + chỉ hàng còn kinh doanh.</summary>
    public async Task<List<HangHoa>> TimKiemAsync(string? tuKhoa, string? loaiHang, bool chiConKinhDoanh)
    {
        await using var db = new TiemTapHoaContext();

        var q = db.HangHoas.AsNoTracking().AsQueryable();

        var kw = (tuKhoa ?? "").Trim();
        if (kw.Length > 0)
            q = q.Where(x => x.TenHang.Contains(kw));

        if (!string.IsNullOrWhiteSpace(loaiHang))
            q = q.Where(x => x.LoaiHang == loaiHang);

        if (chiConKinhDoanh)
            q = q.Where(x => x.ConKinhDoanh);

        return await q.OrderBy(x => x.MaHang).ToListAsync();
    }

    /// <summary>Các mặt hàng còn kinh doanh (dùng cho bảng giá và Excel tồn kho).</summary>
    public async Task<List<HangHoa>> LayHangConKinhDoanhAsync()
    {
        await using var db = new TiemTapHoaContext();
        return await db.HangHoas.AsNoTracking()
            .Where(x => x.ConKinhDoanh)
            .OrderBy(x => x.MaHang)
            .ToListAsync();
    }

    public async Task<int> ThemAsync(HangHoa hh)
    {
        await using var db = new TiemTapHoaContext();
        db.HangHoas.Add(hh);
        await db.SaveChangesAsync();
        return hh.MaHang;
    }

    public async Task<bool> SuaAsync(HangHoa moi)
    {
        await using var db = new TiemTapHoaContext();
        var hh = await db.HangHoas.FindAsync(moi.MaHang);
        if (hh == null) return false;

        hh.TenHang = moi.TenHang;
        hh.LoaiHang = moi.LoaiHang;
        hh.DonVi = moi.DonVi;
        hh.GiaBan = moi.GiaBan;
        hh.SoLuongTon = moi.SoLuongTon;
        hh.NgayNhap = moi.NgayNhap;
        hh.ConKinhDoanh = moi.ConKinhDoanh;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> XoaAsync(int maHang)
    {
        await using var db = new TiemTapHoaContext();
        var hh = await db.HangHoas.FindAsync(maHang);
        if (hh == null) return false;

        db.HangHoas.Remove(hh);
        await db.SaveChangesAsync();
        return true;
    }
}
