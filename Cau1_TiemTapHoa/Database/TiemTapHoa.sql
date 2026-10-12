-- =============================================================
--  CÂU 1 - TIỆM TẠP HÓA NHÀ TRƯỜNG UTC2
--  Tạo CSDL TiemTapHoa, bảng HangHoa và 6 dòng dữ liệu mẫu
-- =============================================================
USE master;
GO

IF DB_ID('TiemTapHoa') IS NOT NULL
BEGIN
    ALTER DATABASE TiemTapHoa SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TiemTapHoa;
END
GO

-- Collation tiếng Việt, không phân biệt hoa/thường => gõ "nước" tìm được "Nước ..."
CREATE DATABASE TiemTapHoa COLLATE Vietnamese_CI_AS;
GO

USE TiemTapHoa;
GO

CREATE TABLE HangHoa (
    MaHang        INT IDENTITY(1,1) NOT NULL,
    TenHang       NVARCHAR(100)     NOT NULL,
    LoaiHang      NVARCHAR(50)      NULL,      -- Đồ uống, Thực phẩm khô, Gia vị, Đồ gia dụng
    DonVi         NVARCHAR(20)      NULL,      -- Chai, Gói, Hộp, Túi
    GiaBan        DECIMAL(18, 0)    NOT NULL,
    SoLuongTon    INT               NOT NULL,
    NgayNhap      DATETIME          NOT NULL,  -- datetime để khớp DateTimePicker
    ConKinhDoanh  BIT               NOT NULL,  -- 1 = còn bán, 0 = ngừng bán
    CONSTRAINT PK_HangHoa PRIMARY KEY (MaHang),
    CONSTRAINT CK_HangHoa_GiaBan CHECK (GiaBan > 0),
    CONSTRAINT CK_HangHoa_SoLuongTon CHECK (SoLuongTon >= 0)
);
GO

-- Dữ liệu mẫu (6 dòng)
INSERT INTO HangHoa (TenHang, LoaiHang, DonVi, GiaBan, SoLuongTon, NgayNhap, ConKinhDoanh) VALUES
(N'Nước suối Aquafina 500ml', N'Đồ uống',        N'Chai',  6000, 120, '2026-10-02', 1),
(N'Mì Hảo Hảo tôm chua cay',  N'Thực phẩm khô',  N'Gói',   4500, 200, '2026-10-02', 1),
(N'Dầu ăn Tường An 1L',       N'Gia vị',         N'Chai', 48000,  35, '2026-10-05', 1),
(N'Bột giặt OMO 800g',        N'Đồ gia dụng',    N'Túi',  52000,  40, '2026-10-05', 1),
(N'Sữa tươi Vinamilk 1L',     N'Đồ uống',        N'Hộp',  32000,  60, '2026-10-08', 1),
(N'Nước mắm Nam Ngư 500ml',   N'Gia vị',         N'Chai', 38000,  25, '2026-10-01', 0);
GO

SELECT * FROM HangHoa;
GO

-- =============================================================
--  Scaffold (Package Manager Console), hoặc dùng EF Core Power Tools với efpt.config.json:
--  Scaffold-DbContext "Server=.\SQLEXPRESS;Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True"
--      Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context TiemTapHoaContext -Tables HangHoa -Force
-- =============================================================
