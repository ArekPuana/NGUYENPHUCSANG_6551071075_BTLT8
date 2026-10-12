using System;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Cau3_PhieuBaoDiem.Models;

namespace Cau3_PhieuBaoDiem.Services;

public class ExcelExportService
{
    public static int ExportDiemLop(string maLop, string filePath)
    {
        using var context = new QuanLyDiemContext();

        var query = from d in context.DiemMons
                    join sv in context.SinhViens on d.MaSv equals sv.MaSv
                    join l in context.Lops on sv.MaLop equals l.MaLop
                    where l.MaLop == maLop
                    orderby sv.MaSv, d.MaDiem
                    select new
                    {
                        MSSV = sv.MaSv,
                        HoTen = sv.HoTen,
                        Lop = l.MaLop,
                        TenLop = l.TenLop,
                        MonHoc = d.TenMon,
                        SoTC = d.SoTinChi,
                        Diem = (double)d.Diem
                    };

        var list = query.ToList();
        if (list.Count == 0)
        {
            throw new InvalidOperationException($"Lớp {maLop} không có dữ liệu điểm nào để xuất.");
        }

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add($"Diem_{maLop}");

        // Tiêu đề cột
        string[] headers = { "MSSV", "Họ tên", "Lớp", "Môn học", "Số TC", "Điểm" };
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#E2EFDA");
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }
        worksheet.Row(1).Height = 25;

        // Ghi dữ liệu
        int currentRow = 2;
        foreach (var item in list)
        {
            worksheet.Cell(currentRow, 1).Value = item.MSSV;
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 2).Value = item.HoTen;
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            worksheet.Cell(currentRow, 3).Value = item.Lop;
            worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 4).Value = item.MonHoc;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            worksheet.Cell(currentRow, 5).Value = item.SoTC;
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "0";

            worksheet.Cell(currentRow, 6).Value = item.Diem;
            worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "0.0";

            for (int col = 1; col <= 6; col++)
            {
                worksheet.Cell(currentRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            worksheet.Row(currentRow).Height = 20;
            currentRow++;
        }

        int lastDataRow = currentRow - 1;

        // Dòng công thức 1: "Tổng số tín chỉ" dùng SUM cột Số TC
        int sumRow = currentRow;
        worksheet.Cell(sumRow, 4).Value = "Tổng số tín chỉ";
        worksheet.Cell(sumRow, 4).Style.Font.Bold = true;
        worksheet.Cell(sumRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

        worksheet.Cell(sumRow, 5).FormulaA1 = $"SUM(E2:E{lastDataRow})";
        worksheet.Cell(sumRow, 5).Style.Font.Bold = true;
        worksheet.Cell(sumRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell(sumRow, 5).Style.NumberFormat.Format = "0";

        for (int col = 1; col <= 6; col++)
        {
            worksheet.Cell(sumRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }
        worksheet.Row(sumRow).Height = 22;

        // Dòng công thức 2: "Điểm trung bình" dùng AVERAGE cột Điểm
        int avgRow = sumRow + 1;
        worksheet.Cell(avgRow, 4).Value = "Điểm trung bình";
        worksheet.Cell(avgRow, 4).Style.Font.Bold = true;
        worksheet.Cell(avgRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

        worksheet.Cell(avgRow, 6).FormulaA1 = $"AVERAGE(F2:F{lastDataRow})";
        worksheet.Cell(avgRow, 6).Style.Font.Bold = true;
        worksheet.Cell(avgRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell(avgRow, 6).Style.NumberFormat.Format = "0.00";

        for (int col = 1; col <= 6; col++)
        {
            worksheet.Cell(avgRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }
        worksheet.Row(avgRow).Height = 22;

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);
        return list.Count;
    }
}
