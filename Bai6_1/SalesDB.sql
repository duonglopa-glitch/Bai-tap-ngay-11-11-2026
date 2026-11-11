IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SalesDB')
BEGIN
    CREATE DATABASE SalesDB;
END
GO

USE SalesDB;
GO

-- 1. Bảng Nhân viên bán hàng (NhanVien)
IF OBJECT_ID('dbo.NhanVien', 'U') IS NULL
BEGIN
    CREATE TABLE NhanVien (
        MaNV INT IDENTITY(1,1) PRIMARY KEY,
        TenNV NVARCHAR(100) NOT NULL,
        SoDienThoai VARCHAR(15) UNIQUE NULL,
        ChucVu NVARCHAR(50) DEFAULT N'Thu ngân'
    );
END
GO

-- 2. Bảng Khách hàng (KhachHang)
IF OBJECT_ID('dbo.KhachHang', 'U') IS NULL
BEGIN
    CREATE TABLE KhachHang (
        MaKH INT IDENTITY(1,1) PRIMARY KEY,
        TenKH NVARCHAR(100) NOT NULL,
        DienThoai VARCHAR(15) UNIQUE NULL,
        DiemTichLuy INT NOT NULL DEFAULT 0
    );
END
GO

-- 3. Bảng Sản phẩm (SanPham)
IF OBJECT_ID('dbo.SanPham', 'U') IS NULL
BEGIN
    CREATE TABLE SanPham (
        MaSP VARCHAR(20) PRIMARY KEY,
        TenSP NVARCHAR(150) NOT NULL,
        GiaBan DECIMAL(18,2) NOT NULL CHECK (GiaBan > 0),
        SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0)
    );
END
GO

-- 4. Bảng Hóa đơn bán hàng (HoaDon)
IF OBJECT_ID('dbo.HoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE HoaDon (
        MaHD INT IDENTITY(1,1) PRIMARY KEY,
        NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
        MaNV INT NOT NULL,
        MaKH INT NULL,
        TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
        CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
        CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
    );
END
GO

-- 5. Bảng Chi tiết hóa đơn (ChiTietHoaDon)
IF OBJECT_ID('dbo.ChiTietHoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE ChiTietHoaDon (
        MaHD INT NOT NULL,
        MaSP VARCHAR(20) NOT NULL,
        SoLuong INT NOT NULL CHECK (SoLuong > 0),
        DonGia DECIMAL(18,2) NOT NULL,
        PRIMARY KEY (MaHD, MaSP),
        CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
        CONSTRAINT FK_CTHD_SanPham FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
    );
END
GO

-- CHÈN DỮ LIỆU MẪU
IF NOT EXISTS (SELECT 1 FROM NhanVien)
BEGIN
    INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES
    (N'Nguyễn Văn Thu', '0901112233', N'Thu ngân'),
    (N'Trần Thị Ngân', '0904445566', N'Thu ngân'),
    (N'Phạm Minh Quản', '0907778899', N'Quản lý');
END

IF NOT EXISTS (SELECT 1 FROM KhachHang)
BEGIN
    INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES
    (N'Khách Lẻ', '0000000000', 0),
    (N'Lê Hoàng Nam', '0912345678', 50),
    (N'Nguyễn Mai Phương', '0987654321', 120);
END

IF NOT EXISTS (SELECT 1 FROM SanPham)
BEGIN
    INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES
    ('8934567001', N'Sữa Tươi Tiệt Trùng Vinamilk 1L', 38000, 100),
    ('8934567002', N'Mì Hảo Hảo Tôm Chua Cay (Thùng)', 115000, 45),
    ('8934567003', N'Nước Khoáng Lavie 1.5L', 12000, 200),
    ('8934567004', N'Bánh Quy Cosy Bơ 378g', 55000, 30),
    ('8934567005', N'Dầu Ăn Neptune Light 1L', 62000, 15);
END
GO