using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Cau3_PhieuBaoDiem.Models;
using Cau3_PhieuBaoDiem.Printing;
using Cau3_PhieuBaoDiem.Services;

namespace Cau3_PhieuBaoDiem;

public static class VerificationTests
{
    public static int RunAllTests()
    {
        int passed = 0;
        int failed = 0;

        Console.WriteLine("==================================================");
        Console.WriteLine("BẮT ĐẦU KIỂM TRA HỆ THỐNG PHIẾU BÁO ĐIỂM (CÂU 3)");
        Console.WriteLine("==================================================\n");

        using var context = new QuanLyDiemContext();

        // Test 1: Kiểm tra CSDL và dữ liệu mẫu
        try
        {
            Console.Write("[TEST 1] Kiểm tra cơ sở dữ liệu và dữ liệu ban đầu: ");
            var lops = context.Lops.ToList();
            var svs = context.SinhViens.ToList();
            var diems = context.DiemMons.ToList();

            if (lops.Count >= 2 && svs.Count >= 3 && diems.Count >= 7)
            {
                Console.WriteLine($"THÀNH CÔNG! ({lops.Count} lớp, {svs.Count} SV, {diems.Count} điểm môn)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: Số lượng dữ liệu không khớp.");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 2: Kiểm tra lọc và tìm kiếm
        try
        {
            Console.Write("[TEST 2] Kiểm tra logic lọc theo lớp và tìm kiếm: ");
            var allSvs = context.SinhViens.ToList();

            // Tất cả: 3
            int cntTatCa = allSvs.Count;
            // CNTT01: 2
            int cntCntt = allSvs.Count(s => s.MaLop.Equals("CNTT01", StringComparison.OrdinalIgnoreCase));
            // KT01: 1
            int cntKt = allSvs.Count(s => s.MaLop.Equals("KT01", StringComparison.OrdinalIgnoreCase));
            // Tìm "anh"
            var findAnh = allSvs.Where(s => s.HoTen.Contains("anh", StringComparison.OrdinalIgnoreCase) || s.MaSv.Contains("anh", StringComparison.OrdinalIgnoreCase)).ToList();

            if (cntTatCa == 3 && cntCntt == 2 && cntKt == 1 && findAnh.Count == 1 && findAnh[0].MaSv == "SV001")
            {
                Console.WriteLine("THÀNH CÔNG! (Tất cả: 3, CNTT01: 2, KT01: 1, 'anh': 1)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: Kết quả lọc không khớp (All: {cntTatCa}, CNTT: {cntCntt}, KT: {cntKt}, Anh: {findAnh.Count})");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 3: Tính điểm trung bình và xếp loại
        try
        {
            Console.Write("[TEST 3] Kiểm tra tính Điểm TB có trọng số và Xếp loại: ");
            // SV001
            var d1 = context.DiemMons.Where(d => d.MaSv == "SV001").ToList();
            int tc1 = d1.Sum(x => x.SoTinChi);
            decimal tb1 = Math.Round(d1.Sum(x => x.Diem * x.SoTinChi) / tc1, 2, MidpointRounding.AwayFromZero);
            string xl1 = tb1 >= 8.0m ? "Giỏi" : (tb1 >= 7.0m ? "Khá" : (tb1 >= 5.0m ? "Trung bình" : "Yếu"));

            // SV002
            var d2 = context.DiemMons.Where(d => d.MaSv == "SV002").ToList();
            int tc2 = d2.Sum(x => x.SoTinChi);
            decimal tb2 = Math.Round(d2.Sum(x => x.Diem * x.SoTinChi) / tc2, 2, MidpointRounding.AwayFromZero);
            string xl2 = tb2 >= 8.0m ? "Giỏi" : (tb2 >= 7.0m ? "Khá" : (tb2 >= 5.0m ? "Trung bình" : "Yếu"));

            // SV003
            var d3 = context.DiemMons.Where(d => d.MaSv == "SV003").ToList();

            bool ok1 = (tc1 == 11 && tb1 == 7.77m && xl1 == "Khá");
            bool ok2 = (tc2 == 8 && tb2 == 8.50m && xl2 == "Giỏi");
            bool ok3 = (d3.Count == 0);

            if (ok1 && ok2 && ok3)
            {
                Console.WriteLine($"THÀNH CÔNG! (SV001: 11 TC, {tb1}, {xl1}; SV002: 8 TC, {tb2}, {xl2}; SV003: Chưa có điểm)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: SV001: ({tc1} TC, {tb1}, {xl1}), SV002: ({tc2} TC, {tb2}, {xl2})");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 4: Xuất Excel ClosedXML với công thức
        try
        {
            Console.Write("[TEST 4] Kiểm tra xuất Excel với công thức AVERAGE và SUM: ");
            string tempExcel = Path.Combine(Path.GetTempPath(), "Test_Diem_CNTT01.xlsx");
            if (File.Exists(tempExcel)) File.Delete(tempExcel);

            int exportedRows = ExcelExportService.ExportDiemLop("CNTT01", tempExcel);

            using var wb = new XLWorkbook(tempExcel);
            var ws = wb.Worksheets.First();

            // Kiểm tra số dòng dữ liệu
            // Data rows: 2 -> 8 (7 items)
            // Row 9: SUM
            // Row 10: AVERAGE
            string sumFormula = ws.Cell(9, 5).FormulaA1;
            string avgFormula = ws.Cell(10, 6).FormulaA1;

            bool isFormulaSum = sumFormula.Equals("SUM(E2:E8)", StringComparison.OrdinalIgnoreCase);
            bool isFormulaAvg = avgFormula.Equals("AVERAGE(F2:F8)", StringComparison.OrdinalIgnoreCase);

            if (exportedRows == 7 && isFormulaSum && isFormulaAvg)
            {
                Console.WriteLine($"THÀNH CÔNG! ({exportedRows} dòng dữ liệu, SUM: '{sumFormula}', AVERAGE: '{avgFormula}')");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: rows={exportedRows}, sumFormula='{sumFormula}', avgFormula='{avgFormula}'");
                failed++;
            }

            File.Delete(tempExcel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 5: In phiếu báo điểm và phân trang cho SV001 (1 trang)
        try
        {
            Console.Write("[TEST 5] Kiểm tra in phiếu SV001 (1 trang, 'Trang 1/1'): ");
            var sv1 = context.SinhViens.First(s => s.MaSv == "SV001");
            var d1 = context.DiemMons.Where(d => d.MaSv == "SV001").ToList();

            var printer = new PhieuDiemPrinter(sv1, d1, 11, 7.77m, "Khá");

            int pageCount = 0;
            printer.Document.PrintController = new PreviewPrintController();
            printer.Document.PrintPage += (s, ev) =>
            {
                pageCount++;
            };

            // In lần 1
            printer.Document.Print();
            int pagesRun1 = pageCount;

            // In lần 2 (kiểm tra tính nhất quán, không lệch trang khi xem nhiều lần)
            pageCount = 0;
            printer.Document.Print();
            int pagesRun2 = pageCount;

            if (pagesRun1 == 1 && pagesRun2 == 1)
            {
                Console.WriteLine("THÀNH CÔNG! (SV001 có đúng 1 trang, nhất quán qua các lần in)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: pagesRun1={pagesRun1}, pagesRun2={pagesRun2}");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 6: In phiếu cho sinh viên có 40 môn học (tự động phân trang)
        try
        {
            Console.Write("[TEST 6] Kiểm tra sinh viên có 40 môn tự động sang nhiều trang: ");
            var svMock = new SinhVien
            {
                MaSv = "SV_TEST",
                HoTen = "Sinh Viên Test 40 Môn",
                NgaySinh = new DateTime(2005, 1, 1),
                GioiTinh = "Nam",
                MaLop = "CNTT01"
            };

            var mockSubjects = new List<DiemMon>();
            for (int i = 1; i <= 40; i++)
            {
                mockSubjects.Add(new DiemMon
                {
                    MaDiem = i,
                    MaSv = "SV_TEST",
                    TenMon = $"Môn học kiểm thử số {i:D2}",
                    SoTinChi = 3,
                    Diem = 8.0m
                });
            }

            var printer40 = new PhieuDiemPrinter(svMock, mockSubjects, 120, 8.00m, "Giỏi");
            printer40.Document.PrintController = new PreviewPrintController();

            int pageCount40 = 0;
            printer40.Document.PrintPage += (s, ev) =>
            {
                pageCount40++;
            };

            printer40.Document.Print();
            int pages1 = pageCount40;

            // Kiểm tra chạy lại lần 2
            pageCount40 = 0;
            printer40.Document.Print();
            int pages2 = pageCount40;

            if (pages1 >= 2 && pages1 == pages2)
            {
                Console.WriteLine($"THÀNH CÔNG! (40 môn tự động chia thành {pages1} trang, lần 2: {pages2} trang - không lệch trang)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: pages1={pages1}, pages2={pages2}");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        // Test 7: Kiểm tra thay đổi PageSetup sang khổ ngang (Landscape)
        try
        {
            Console.Write("[TEST 7] Kiểm tra bố cục khi in khổ ngang (Landscape): ");
            var sv1 = context.SinhViens.First(s => s.MaSv == "SV001");
            var d1 = context.DiemMons.Where(d => d.MaSv == "SV001").ToList();

            var printerLandscape = new PhieuDiemPrinter(sv1, d1, 11, 7.77m, "Khá");
            printerLandscape.Document.DefaultPageSettings.Landscape = true;
            printerLandscape.Document.PrintController = new PreviewPrintController();

            int landscapePages = 0;
            printerLandscape.Document.PrintPage += (s, ev) =>
            {
                landscapePages++;
            };
            printerLandscape.Document.Print();

            if (landscapePages == 1)
            {
                Console.WriteLine("THÀNH CÔNG! (Khổ ngang in chuẩn xác 1 trang, bố cục tự động co giãn theo MarginBounds)");
                passed++;
            }
            else
            {
                Console.WriteLine($"THẤT BẠI: landscapePages={landscapePages}");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI: {ex.Message}");
            failed++;
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine($"KẾT QUẢ: {passed} PASS / {failed} FAIL (TỔNG {passed + failed} TESTS)");
        Console.WriteLine("==================================================");

        return failed;
    }
}
