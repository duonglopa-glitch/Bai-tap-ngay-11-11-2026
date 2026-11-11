using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSalesApp.DTO;

namespace RetailSalesApp.DAL
{
    public class SalesDAL
    {
        // 1. Lấy danh sách Nhân viên & Khách hàng
        public List<NhanVienDTO> GetEmployees()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM NhanVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NhanVienDTO(
                                Convert.ToInt32(reader["MaNV"]),
                                reader["TenNV"].ToString(),
                                reader["SoDienThoai"]?.ToString(),
                                reader["ChucVu"].ToString()
                            ));
                        }
                    }
                }
            }
            return list;
        }

        public List<KhachHangDTO> GetCustomers()
        {
            List<KhachHangDTO> list = new List<KhachHangDTO>();
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM KhachHang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new KhachHangDTO(
                                Convert.ToInt32(reader["MaKH"]),
                                reader["TenKH"].ToString(),
                                reader["DienThoai"]?.ToString(),
                                Convert.ToInt32(reader["DiemTichLuy"])
                            ));
                        }
                    }
                }
            }
            return list;
        }

        // 2. Tra cứu Sản phẩm 100% Chống SQL Injection bằng SqlParameter (%)
        public DataTable SearchProducts(string keyword)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT MaSP, TenSP, GiaBan, SoLuongTon 
                                 FROM SanPham 
                                 WHERE MaSP LIKE @Keyword OR TenSP LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = "%" + keyword + "%" });
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 3. THỰC THI GIAO DỊCH LẬP HÓA ĐƠN & BÁN HÀNG (SqlTransaction - ACID Compliance)
        public bool ExecuteCheckoutTransaction(HoaDonDTO invoice, List<ChiTietHoaDonDTO> cartDetails, out string errorMessage)
        {
            errorMessage = string.Empty;

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Bước 1: Chèn 1 bản ghi vào bảng HoaDon
                    string insertInvoiceQuery = @"INSERT INTO HoaDon (NgayLap, MaNV, MaKH, TongTien) 
                                                  VALUES (GETDATE(), @MaNV, @MaKH, @TongTien); 
                                                  SELECT SCOPE_IDENTITY();";

                    int generatedInvoiceId = 0;
                    using (SqlCommand cmdInvoice = new SqlCommand(insertInvoiceQuery, conn, trans))
                    {
                        cmdInvoice.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int) { Value = invoice.MaNV });
                        cmdInvoice.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.Int) { Value = (object)invoice.MaKH ?? DBNull.Value });
                        cmdInvoice.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = invoice.TongTien });

                        generatedInvoiceId = Convert.ToInt32(cmdInvoice.ExecuteScalar());
                    }

                    // Bước 2 & 3: Chèn ChiTietHoaDon & Trừ SoLuongTon trong SanPham
                    foreach (var item in cartDetails)
                    {
                        // Kiểm tra tồn kho trước khi xuất bán
                        string checkStockQuery = "SELECT SoLuongTon FROM SanPham WHERE MaSP = @MaSP";
                        int currentStock = 0;
                        using (SqlCommand cmdCheck = new SqlCommand(checkStockQuery, conn, trans))
                        {
                            cmdCheck.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.VarChar) { Value = item.MaSP });
                            object stockObj = cmdCheck.ExecuteScalar();
                            currentStock = stockObj != null ? Convert.ToInt32(stockObj) : 0;
                        }

                        if (currentStock < item.SoLuong)
                        {
                            // HỦY GIAO DỊCH KHI KHÔNG ĐỦ TỒN KHO
                            trans.Rollback();
                            errorMessage = string.Format("Sản phẩm [{0} - {1}] không đủ tồn kho! (Tồn: {2}, Mua: {3})",
                                                        item.MaSP, item.TenSP, currentStock, item.SoLuong);
                            return false;
                        }

                        // Chèn ChiTietHoaDon
                        string insertDetailQuery = @"INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) 
                                                     VALUES (@MaHD, @MaSP, @SoLuong, @DonGia)";
                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, trans))
                        {
                            cmdDetail.Parameters.Add(new SqlParameter("@MaHD", SqlDbType.Int) { Value = generatedInvoiceId });
                            cmdDetail.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.VarChar) { Value = item.MaSP });
                            cmdDetail.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = item.SoLuong });
                            cmdDetail.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.Decimal) { Value = item.DonGia });
                            cmdDetail.ExecuteNonQuery();
                        }

                        // Trừ tồn kho sản phẩm
                        string deductStockQuery = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP";
                        using (SqlCommand cmdDeduct = new SqlCommand(deductStockQuery, conn, trans))
                        {
                            cmdDeduct.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = item.SoLuong });
                            cmdDeduct.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.VarChar) { Value = item.MaSP });
                            cmdDeduct.ExecuteNonQuery();
                        }
                    }

                    // Bước 4: Cộng điểm tích lũy cho Khách hàng (DiemTichLuy += TongTien / 100000)
                    if (invoice.MaKH.HasValue && invoice.MaKH.Value > 1) // Khách hàng thành viên
                    {
                        int pointsToAdd = (int)(invoice.TongTien / 100000m);
                        if (pointsToAdd > 0)
                        {
                            string updatePointsQuery = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @Points WHERE MaKH = @MaKH";
                            using (SqlCommand cmdPoints = new SqlCommand(updatePointsQuery, conn, trans))
                            {
                                cmdPoints.Parameters.Add(new SqlParameter("@Points", SqlDbType.Int) { Value = pointsToAdd });
                                cmdPoints.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.Int) { Value = invoice.MaKH.Value });
                                cmdPoints.ExecuteNonQuery();
                            }
                        }
                    }

                    // Bước 5: Gọi trans.Commit() khi tất cả các bước thành công
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMessage = "Lỗi trong quá trình thanh toán: " + ex.Message;
                    return false;
                }
            }
        }

        // 4. Thống kê Doanh Thu Ngày sử dụng ExecuteScalar
        public decimal GetDailyRevenue(DateTime date)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE CAST(NgayLap AS DATE) = @RevDate";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RevDate", SqlDbType.Date) { Value = date.Date });
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToDecimal(result);
                }
            }
        }

        // 5. Thống kê Top 5 Sản Phẩm Bán Chạy Nhất sử dụng SqlDataReader
        public DataTable GetTop5BestSellingProducts()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSP", typeof(string));
            dt.Columns.Add("TenSP", typeof(string));
            dt.Columns.Add("TongSoLuongBan", typeof(int));
            dt.Columns.Add("TongDoanhThu", typeof(decimal));

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT TOP 5 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuongBan, SUM(ct.SoLuong * ct.DonGia) AS TongDoanhThu
                                 FROM ChiTietHoaDon ct
                                 INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                                 GROUP BY sp.MaSP, sp.TenSP
                                 ORDER BY TongSoLuongBan DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(
                                reader["MaSP"].ToString(),
                                reader["TenSP"].ToString(),
                                Convert.ToInt32(reader["TongSoLuongBan"]),
                                Convert.ToDecimal(reader["TongDoanhThu"])
                            );
                        }
                    }
                }
            }
            return dt;
        }
    }
}