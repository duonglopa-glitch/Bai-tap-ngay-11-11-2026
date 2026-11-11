namespace RetailSalesApp.DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public int DiemTichLuy { get; set; }

        public KhachHangDTO() { }
        public KhachHangDTO(int maKH, string tenKH, string dienThoai, int diemTichLuy)
        {
            MaKH = maKH;
            TenKH = tenKH;
            DienThoai = dienThoai;
            DiemTichLuy = diemTichLuy;
        }
    }
}