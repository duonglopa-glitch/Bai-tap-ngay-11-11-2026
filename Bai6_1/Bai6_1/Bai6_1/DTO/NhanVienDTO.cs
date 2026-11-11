namespace RetailSalesApp.DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set; }

        public NhanVienDTO() { }
        public NhanVienDTO(int maNV, string tenNV, string soDienThoai, string chucVu)
        {
            MaNV = maNV;
            TenNV = tenNV;
            SoDienThoai = soDienThoai;
            ChucVu = chucVu;
        }
    }
}