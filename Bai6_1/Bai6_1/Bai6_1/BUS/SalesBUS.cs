using System;
using System.Collections.Generic;
using System.Data;
using RetailSalesApp.DAL;
using RetailSalesApp.DTO;

namespace RetailSalesApp.BUS
{
    public class SalesBUS
    {
        private readonly SalesDAL _dal = new SalesDAL();

        public List<NhanVienDTO> GetEmployees() => _dal.GetEmployees();
        public List<KhachHangDTO> GetCustomers() => _dal.GetCustomers();
        public DataTable SearchProducts(string keyword) => _dal.SearchProducts(keyword ?? string.Empty);
        public decimal GetDailyRevenue(DateTime date) => _dal.GetDailyRevenue(date);
        public DataTable GetTop5BestSellingProducts() => _dal.GetTop5BestSellingProducts();

        public bool Checkout(HoaDonDTO invoice, List<ChiTietHoaDonDTO> cartDetails, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (cartDetails == null || cartDetails.Count == 0)
            {
                errorMessage = "Giỏ hàng hiện đang trống!";
                return false;
            }

            if (invoice.MaNV <= 0)
            {
                errorMessage = "Vui lòng chọn Nhân viên / Thu ngân thực hiện lập hóa đơn!";
                return false;
            }

            decimal total = 0;
            foreach (var item in cartDetails)
            {
                if (item.SoLuong <= 0)
                {
                    errorMessage = string.Format("Số lượng sản phẩm [{0}] phải lớn hơn 0!", item.MaSP);
                    return false;
                }
                total += item.ThanhTien;
            }
            invoice.TongTien = total;

            return _dal.ExecuteCheckoutTransaction(invoice, cartDetails, out errorMessage);
        }
    }
}