-- =============================================================
--  CÂU 2 - QUÁN CÀ PHÊ GÓC PHỐ: HÓA ĐƠN GỌI MÓN
--  Tạo CSDL QuanCaPhe, 3 bảng Mon, HoaDon, ChiTietHoaDon và dữ liệu mẫu
-- =============================================================
USE master;
GO

IF DB_ID('QuanCaPhe') IS NOT NULL
BEGIN
    ALTER DATABASE QuanCaPhe SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanCaPhe;
END
GO

CREATE DATABASE QuanCaPhe COLLATE Vietnamese_CI_AS;
GO

USE QuanCaPhe;
GO

-- 1. Bảng Mon
CREATE TABLE Mon (
    MaMon     INT IDENTITY(1,1) NOT NULL,
    TenMon    NVARCHAR(100)     NOT NULL,
    NhomMon   NVARCHAR(30)      NOT NULL, -- Cà phê, Trà, Bánh, Nước đóng chai
    DonGia    DECIMAL(18, 0)    NOT NULL, -- Giá niêm yết hiện tại
    CONSTRAINT PK_Mon PRIMARY KEY (MaMon),
    CONSTRAINT CK_Mon_DonGia CHECK (DonGia >= 0)
);
GO

-- 2. Bảng HoaDon
CREATE TABLE HoaDon (
    MaHD        INT IDENTITY(1,1) NOT NULL, -- Hiển thị dạng HD000001
    NgayLap     DATETIME          NOT NULL,
    SoBan       INT               NOT NULL, -- Từ 1 đến 10
    HinhThucTT  NVARCHAR(20)      NOT NULL, -- Tiền mặt hoặc Chuyển khoản
    GhiChu      NVARCHAR(200)     NULL,
    CONSTRAINT PK_HoaDon PRIMARY KEY (MaHD),
    CONSTRAINT CK_HoaDon_SoBan CHECK (SoBan >= 1 AND SoBan <= 10)
);
GO

-- 3. Bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaCT      INT IDENTITY(1,1) NOT NULL,
    MaHD      INT               NOT NULL,
    MaMon     INT               NOT NULL,
    SoLuong   INT               NOT NULL, -- Lớn hơn 0
    DonGia    DECIMAL(18, 0)    NOT NULL, -- Giá tại thời điểm bán
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaCT),
    CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietHoaDon_DonGia CHECK (DonGia >= 0),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHoaDon_Mon FOREIGN KEY (MaMon) REFERENCES Mon(MaMon)
);
GO

-- Dữ liệu mẫu bảng Mon
SET IDENTITY_INSERT Mon ON;
INSERT INTO Mon (MaMon, TenMon, NhomMon, DonGia) VALUES
(1, N'Cà phê sữa đá',   N'Cà phê',          29000),
(2, N'Bạc xỉu',          N'Cà phê',          32000),
(3, N'Trà đào cam sả',   N'Trà',             45000),
(4, N'Bánh tiramisu',    N'Bánh',            39000),
(5, N'Nước suối',        N'Nước đóng chai',  12000);
SET IDENTITY_INSERT Mon OFF;
GO

-- Dữ liệu mẫu bảng HoaDon
SET IDENTITY_INSERT HoaDon ON;
INSERT INTO HoaDon (MaHD, NgayLap, SoBan, HinhThucTT, GhiChu) VALUES
(1, '2026-10-09 08:15:00', 3, N'Tiền mặt',       NULL),
(2, '2026-10-09 09:40:00', 5, N'Chuyển khoản',   NULL);
SET IDENTITY_INSERT HoaDon OFF;
GO

-- Dữ liệu mẫu bảng ChiTietHoaDon
-- HD000001: 2 Cà phê sữa đá, 1 Trà đào cam sả, 1 Bánh tiramisu
-- HD000002: 1 Bạc xỉu, 2 Nước suối
INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLuong, DonGia) VALUES
(1, 1, 2, 29000),
(1, 3, 1, 45000),
(1, 4, 1, 39000),
(2, 2, 1, 32000),
(2, 5, 2, 12000);
GO

SELECT * FROM Mon;
SELECT * FROM HoaDon;
SELECT * FROM ChiTietHoaDon;
GO
