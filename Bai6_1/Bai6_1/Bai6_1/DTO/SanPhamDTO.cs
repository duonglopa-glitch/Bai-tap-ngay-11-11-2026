namespace RetailSalesApp.DTO
{
    public class SanPhamDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }

        public SanPhamDTO() { }
        public SanPhamDTO(string maSP, string tenSP, decimal giaBan, int soLuongTon)
        {
            MaSP = maSP;
            TenSP = tenSP;
            GiaBan = giaBan;
            SoLuongTon = soLuongTon;
        }
    }
}