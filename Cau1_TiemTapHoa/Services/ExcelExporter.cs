using ClosedXML.Excel;

namespace Cau1_TiemTapHoa.Services;

/// <summary>Mô tả 1 cột Excel: tiêu đề, định dạng số, có cộng tổng không, có phải công thức không ({r} = số dòng).</summary>
public sealed record CotExcel(string TieuDe, string? DinhDang = null, bool CongTong = false, string? CongThuc = null);

public static class ExcelExporter
{
    /// <summary>
    /// Ghi 1 sheet: dòng 1 là tiêu đề (in đậm), các dòng dữ liệu, dòng "Tổng" dùng SUM (nếu có cột CongTong),
    /// bật AutoFilter và cố định dòng tiêu đề.
    /// </summary>
    public static void Xuat(string filePath, string tenSheet, IReadOnlyList<CotExcel> cot, IEnumerable<object?[]> dong)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add(tenSheet);
        int soCot = cot.Count;

        for (int c = 0; c < soCot; c++)
            ws.Cell(1, c + 1).Value = cot[c].TieuDe;

        var tieuDe = ws.Range(1, 1, 1, soCot);
        tieuDe.Style.Font.Bold = true;
        tieuDe.Style.Fill.BackgroundColor = XLColor.LightGray;
        tieuDe.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        int r = 1;
        foreach (var d in dong)
        {
            r++;
            for (int c = 0; c < soCot; c++)
            {
                var cell = ws.Cell(r, c + 1);
                if (cot[c].CongThuc is { } ct)
                {
                    cell.FormulaA1 = ct.Replace("{r}", r.ToString()).TrimStart('=');
                }
                else if (c < d.Length && d[c] is { } v)
                {
                    cell.Value = ChuyenGiaTri(v);
                }
            }
        }

        int cuoi = r;

        if (cuoi >= 2)
        {
            for (int c = 0; c < soCot; c++)
            {
                if (cot[c].DinhDang is not { } fmt) continue;
                var vung = ws.Range(2, c + 1, cuoi, c + 1);
                vung.Style.NumberFormat.Format = fmt;
                if (LaDinhDangSo(fmt))
                    vung.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                else if (fmt.Contains("yy"))
                    vung.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
        }

        if (cuoi >= 2 && cot.Any(x => x.CongTong))
        {
            int rt = cuoi + 1;
            ws.Cell(rt, 1).Value = "Tổng";
            for (int c = 0; c < soCot; c++)
            {
                if (!cot[c].CongTong) continue;
                string chu = XLHelper.GetColumnLetterFromNumber(c + 1);
                var cell = ws.Cell(rt, c + 1);
                cell.FormulaA1 = $"SUM({chu}2:{chu}{cuoi})";
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                if (cot[c].DinhDang is { } fmt)
                    cell.Style.NumberFormat.Format = fmt;
            }
            ws.Range(rt, 1, rt, soCot).Style.Font.Bold = true;
        }

        int dongCuoiCung = cuoi >= 2 && cot.Any(x => x.CongTong) ? cuoi + 1 : Math.Max(cuoi, 1);
        var toanBang = ws.Range(1, 1, dongCuoiCung, soCot);
        toanBang.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        toanBang.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        ws.Range(1, 1, Math.Max(cuoi, 1), soCot).SetAutoFilter();
        ws.SheetView.FreezeRows(1);
        ws.Columns().AdjustToContents();

        wb.SaveAs(filePath);
    }

    /// <summary>Xuất đúng các dòng/cột đang hiển thị trên DataGridView (giữ kiểu số, không thành chuỗi).</summary>
    public static void XuatDataGridView(DataGridView grid, string filePath, string tenSheet)
    {
        var cols = grid.Columns.Cast<DataGridViewColumn>()
            .Where(c => c.Visible)
            .OrderBy(c => c.DisplayIndex)
            .ToList();

        var dong = grid.Rows.Cast<DataGridViewRow>()
            .Where(r => !r.IsNewRow && r.Visible)
            .Select(r => cols.Select(c => r.Cells[c.Index].Value).ToArray())
            .ToList();

        var cot = new List<CotExcel>();
        for (int i = 0; i < cols.Count; i++)
        {
            object? mau = dong.Select(d => d[i]).FirstOrDefault(v => v != null);
            cot.Add(new CotExcel(cols[i].HeaderText, DinhDangTheoKieu(mau)));
        }

        Xuat(filePath, tenSheet, cot, dong);
    }

    private static bool LaDinhDangSo(string fmt) => fmt.Contains('#') || fmt.Contains('0');

    private static string? DinhDangTheoKieu(object? v) => v switch
    {
        decimal or double or float => "#,##0",
        int or long or short => "#,##0",
        DateTime => "dd/MM/yyyy",
        _ => null
    };

    private static XLCellValue ChuyenGiaTri(object v)
    {
        switch (v)
        {
            case string s: return s;
            case bool b: return b ? "Còn bán" : "Ngừng bán";
            case DateTime dt: return dt;
            case TimeSpan ts: return ts;
            case decimal or double or float or byte or sbyte or short or ushort or int or uint or long or ulong:
                return Convert.ToDouble(v);
            default: return v.ToString() ?? "";
        }
    }
}
