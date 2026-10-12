using System.Diagnostics;

namespace Cau1_TiemTapHoa.Helpers;

public static class UiHelper
{
    public static string? ChonFileLuu(IWin32Window owner, string boLoc, string tenMacDinh)
    {
        using var dlg = new SaveFileDialog
        {
            Filter = boLoc,
            FileName = tenMacDinh,
            OverwritePrompt = true,
            Title = "Chọn nơi lưu file"
        };
        return dlg.ShowDialog(owner) == DialogResult.OK ? dlg.FileName : null;
    }

    public static void HoiMoFile(IWin32Window owner, string path)
    {
        var kq = MessageBox.Show(owner, $"Đã lưu file:\n{path}\n\nMở file ngay?", "Hoàn tất",
            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        if (kq == DialogResult.Yes)
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }

    public static bool CoMauReport(IWin32Window owner, string tenFile)
    {
        if (Services.ReportService.CoMau(tenFile)) return true;

        MessageBox.Show(owner,
            $"Không tìm thấy mẫu report \"{tenFile}\" trong thư mục:\n{AppConfig.ThuMucReport}",
            "Chưa có mẫu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    /// <summary>Chạy 1 thao tác đồng bộ, lỗi thì báo MessageBox.</summary>
    public static void Chay(IWin32Window owner, Action tacVu)
    {
        try
        {
            tacVu();
        }
        catch (Exception ex)
        {
            HienLoi(owner, ex);
        }
    }

    /// <summary>Chạy 1 thao tác bất đồng bộ (async/await), đổi con trỏ chờ, lỗi thì báo MessageBox.</summary>
    public static async void ChayAsync(IWin32Window owner, Func<Task> tacVu)
    {
        var ctl = owner as Control;
        try
        {
            if (ctl != null) ctl.Cursor = Cursors.WaitCursor;
            await tacVu();
        }
        catch (Exception ex)
        {
            HienLoi(owner, ex);
        }
        finally
        {
            if (ctl != null && !ctl.IsDisposed) ctl.Cursor = Cursors.Default;
        }
    }

    public static void HienLoi(IWin32Window owner, Exception ex)
    {
        var goc = ex.GetBaseException();

        if (ex is IOException || goc is IOException)
        {
            MessageBox.Show(owner,
                "Không ghi được file vì file đang được mở bởi chương trình khác (Excel / trình đọc PDF).\n\n" +
                "Hãy đóng file đó rồi thử lại.",
                "File đang được mở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string goiY = goc is Microsoft.Data.SqlClient.SqlException
            ? "\n\nGợi ý: kiểm tra đã chạy script Database\\TiemTapHoa.sql chưa và chuỗi kết nối trong AppConfig.cs."
            : "";

        MessageBox.Show(owner, goc.Message + goiY, "Có lỗi xảy ra", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
