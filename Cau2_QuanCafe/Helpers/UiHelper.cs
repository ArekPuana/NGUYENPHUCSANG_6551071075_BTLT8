using System.Diagnostics;

namespace Cau2_QuanCafe.Helpers;

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
        var kq = MessageBox.Show(owner, $"Đã xuất file thành công:\n{path}\n\nBạn có muốn mở file ngay không?", "Hoàn tất",
            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        if (kq == DialogResult.Yes)
        {
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
    }

    public static void HienLoi(IWin32Window owner, Exception ex)
    {
        var goc = ex.GetBaseException();

        if (ex is IOException || goc is IOException)
        {
            MessageBox.Show(owner,
                "Không ghi được file vì file đang được mở bởi chương trình khác (trình đọc PDF).\n\nHãy đóng file đó rồi thử lại.",
                "File đang được mở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string goiY = goc is Microsoft.Data.SqlClient.SqlException
            ? "\n\nGợi ý: Kiểm tra đã chạy script Database\\QuanCaPhe.sql chưa và chuỗi kết nối trong AppConfig.cs."
            : "";

        MessageBox.Show(owner, goc.Message + goiY, "Có lỗi xảy ra", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
