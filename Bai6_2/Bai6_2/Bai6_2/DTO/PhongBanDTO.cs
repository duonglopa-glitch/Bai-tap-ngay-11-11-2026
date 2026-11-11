namespace HRMManagementApp.DTO
{
    public class PhongBanDTO
    {
        public int MaPB { get; set; }
        public string TenPB { get; set; }
        public string MoTa { get; set; }

        public PhongBanDTO() { }
        public PhongBanDTO(int maPB, string tenPB, string moTa)
        {
            MaPB = maPB;
            TenPB = tenPB;
            MoTa = moTa;
        }
    }
}