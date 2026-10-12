using Cau2_QuanCafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cau2_QuanCafe.Services;

public class CafeService
{
    public List<Mon> LayDanhSachMon()
    {
        using var db = new QuanCaPheContext();
        return db.Mons
            .AsNoTracking()
            .OrderBy(m => m.NhomMon)
            .ThenBy(m => m.TenMon)
            .ToList();
    }

    public List<HoaDonDisplayDto> TimKiemHoaDon(DateTime? tuNgay, DateTime? denNgay, int? soBan, string? hinhThucTt, string? tenMon)
    {
        using var db = new QuanCaPheContext();
        var query = db.HoaDons
            .AsNoTracking()
            .Include(h => h.ChiTietHoaDons)
            .ThenInclude(ct => ct.MaMonNavigation)
            .AsQueryable();

        if (tuNgay.HasValue)
        {
            var from = tuNgay.Value.Date;
            query = query.Where(h => h.NgayLap >= from);
        }

        if (denNgay.HasValue)
        {
            var to = denNgay.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(h => h.NgayLap <= to);
        }

        if (soBan.HasValue && soBan.Value > 0)
        {
            query = query.Where(h => h.SoBan == soBan.Value);
        }

        if (!string.IsNullOrWhiteSpace(hinhThucTt) && hinhThucTt != "Tất cả")
        {
            query = query.Where(h => h.HinhThucTt == hinhThucTt);
        }

        if (!string.IsNullOrWhiteSpace(tenMon))
        {
            var keyword = tenMon.Trim();
            query = query.Where(h => h.ChiTietHoaDons.Any(ct => ct.MaMonNavigation.TenMon.Contains(keyword)));
        }

        var list = query.OrderBy(h => h.MaHd).ToList();

        return list.Select(h => new HoaDonDisplayDto
        {
            MaHD = h.MaHd,
            NgayLap = h.NgayLap,
            SoBan = h.SoBan,
            HinhThucTT = h.HinhThucTt,
            GhiChu = h.GhiChu,
            TongTien = h.ChiTietHoaDons.Sum(ct => ct.SoLuong * ct.DonGia)
        }).ToList();
    }

    public List<ChiTietIn> LayChiTietHoaDon(int maHd)
    {
        using var db = new QuanCaPheContext();
        var list = db.ChiTietHoaDons
            .AsNoTracking()
            .Include(ct => ct.MaMonNavigation)
            .Where(ct => ct.MaHd == maHd)
            .OrderBy(ct => ct.MaCt)
            .ToList();

        var result = new List<ChiTietIn>();
        int stt = 1;
        foreach (var item in list)
        {
            result.Add(new ChiTietIn
            {
                STT = stt++,
                MaCT = item.MaCt,
                MaMon = item.MaMon,
                TenMon = item.MaMonNavigation?.TenMon ?? "",
                SoLuong = item.SoLuong,
                DonGia = item.DonGia
            });
        }
        return result;
    }

    public HoaDon? LayHoaDonTheoMa(int maHd)
    {
        using var db = new QuanCaPheContext();
        return db.HoaDons.AsNoTracking().FirstOrDefault(h => h.MaHd == maHd);
    }

    public HoaDon ThemHoaDon(DateTime ngayLap, int soBan, string hinhThucTt, string? ghiChu)
    {
        using var db = new QuanCaPheContext();
        var hd = new HoaDon
        {
            NgayLap = ngayLap,
            SoBan = soBan,
            HinhThucTt = hinhThucTt,
            GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null : ghiChu.Trim()
        };
        db.HoaDons.Add(hd);
        db.SaveChanges();
        return hd;
    }

    public bool CapNhatHoaDon(int maHd, DateTime ngayLap, int soBan, string hinhThucTt, string? ghiChu)
    {
        using var db = new QuanCaPheContext();
        var hd = db.HoaDons.Find(maHd);
        if (hd == null) return false;

        hd.NgayLap = ngayLap;
        hd.SoBan = soBan;
        hd.HinhThucTt = hinhThucTt;
        hd.GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null : ghiChu.Trim();
        db.SaveChanges();
        return true;
    }

    public bool XoaHoaDon(int maHd)
    {
        using var db = new QuanCaPheContext();
        var hd = db.HoaDons.Include(h => h.ChiTietHoaDons).FirstOrDefault(h => h.MaHd == maHd);
        if (hd == null) return false;

        // Xóa các dòng chi tiết trước theo đúng yêu cầu
        db.ChiTietHoaDons.RemoveRange(hd.ChiTietHoaDons);
        db.SaveChanges();

        // Xóa hóa đơn
        db.HoaDons.Remove(hd);
        db.SaveChanges();
        return true;
    }

    public bool ThemMonVaoHoaDon(int maHd, int maMon, int soLuong)
    {
        using var db = new QuanCaPheContext();
        var mon = db.Mons.Find(maMon);
        if (mon == null) return false;

        var ct = new ChiTietHoaDon
        {
            MaHd = maHd,
            MaMon = maMon,
            SoLuong = soLuong,
            DonGia = mon.DonGia // Đơn giá lấy từ bảng Mon tại thời điểm thêm
        };
        db.ChiTietHoaDons.Add(ct);
        db.SaveChanges();
        return true;
    }

    public bool SuaSoLuongChiTiet(int maCt, int soLuongMoi)
    {
        using var db = new QuanCaPheContext();
        var ct = db.ChiTietHoaDons.Find(maCt);
        if (ct == null) return false;

        ct.SoLuong = soLuongMoi;
        db.SaveChanges();
        return true;
    }

    public bool XoaChiTiet(int maCt)
    {
        using var db = new QuanCaPheContext();
        var ct = db.ChiTietHoaDons.Find(maCt);
        if (ct == null) return false;

        db.ChiTietHoaDons.Remove(ct);
        db.SaveChanges();
        return true;
    }
}
