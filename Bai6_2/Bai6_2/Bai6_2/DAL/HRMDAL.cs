using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRMManagementApp.DTO;

namespace HRMManagementApp.DAL
{
    public class HRMDAL
    {
        private SqlDataAdapter _attendanceAdapter;
        private DataTable _attendanceTable;

        // 1. Lấy danh sách Phòng Ban
        public List<PhongBanDTO> GetDepartments()
        {
            List<PhongBanDTO> list = new List<PhongBanDTO>();
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaPB, TenPB, MoTa FROM PhongBan";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhongBanDTO(
                                Convert.ToInt32(reader["MaPB"]),
                                reader["TenPB"].ToString(),
                                reader["MoTa"]?.ToString()
                            ));
                        }
                    }
                }
            }
            return list;
        }

        // 2. Tra cứu Nhân viên (Search bằng Parameterized Query)
        public DataTable SearchEmployees(string keyword, int deptId)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, nv.MaPB, pb.TenPB, nv.LuongCoBan, nv.HeSoLuong
                                 FROM NhanVien nv
                                 INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                                 WHERE (nv.HoTen LIKE @Keyword OR nv.CCCD LIKE @Keyword OR CAST(nv.MaNV AS VARCHAR) = @ExactKeyword)";

                if (deptId > 0)
                {
                    query += " AND nv.MaPB = @DeptId";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = "%" + keyword + "%" });
                    cmd.Parameters.Add(new SqlParameter("@ExactKeyword", SqlDbType.VarChar) { Value = keyword });
                    cmd.Parameters.Add(new SqlParameter("@DeptId", SqlDbType.Int) { Value = deptId });

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 3. Thêm / Sửa / Xóa Nhân viên (CRUD)
        public bool InsertEmployee(NhanVienDTO nv)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) 
                                VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = nv.HoTen });
                    cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = nv.NgaySinh });
                    cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = nv.CCCD });
                    cmd.Parameters.Add(new SqlParameter("@MaPB", SqlDbType.Int) { Value = nv.MaPB });
                    cmd.Parameters.Add(new SqlParameter("@LuongCoBan", SqlDbType.Decimal) { Value = nv.LuongCoBan });
                    cmd.Parameters.Add(new SqlParameter("@HeSoLuong", SqlDbType.Float) { Value = nv.HeSoLuong });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateEmployee(NhanVienDTO nv)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"UPDATE NhanVien 
                                SET HoTen = @HoTen, NgaySinh = @NgaySinh, CCCD = @CCCD, MaPB = @MaPB, LuongCoBan = @LuongCoBan, HeSoLuong = @HeSoLuong 
                                WHERE MaNV = @MaNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int) { Value = nv.MaNV });
                    cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = nv.HoTen });
                    cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = nv.NgaySinh });
                    cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = nv.CCCD });
                    cmd.Parameters.Add(new SqlParameter("@MaPB", SqlDbType.Int) { Value = nv.MaPB });
                    cmd.Parameters.Add(new SqlParameter("@LuongCoBan", SqlDbType.Decimal) { Value = nv.LuongCoBan });
                    cmd.Parameters.Add(new SqlParameter("@HeSoLuong", SqlDbType.Float) { Value = nv.HeSoLuong });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteEmployee(int maNV)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int) { Value = maNV });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. CHẤM CÔNG NGẮT KẾT NỐI (Disconnected Model: SqlDataAdapter + DataTable + SqlCommandBuilder)
        public DataTable LoadAttendanceTable(string thangNam)
        {
            SqlConnection conn = DatabaseConnect.GetConnection();
            conn.Open();

            // Khởi tạo bản ghi chấm công tự động cho tất cả nhân viên chưa có trong tháng
            string initQuery = @"INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT)
                                SELECT nv.MaNV, @ThangNam, 22, 0
                                FROM NhanVien nv
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM BangChamCong c WHERE c.MaNV = nv.MaNV AND c.ThangNam = @ThangNam
                                )";
            using (SqlCommand cmdInit = new SqlCommand(initQuery, conn))
            {
                cmdInit.Parameters.Add(new SqlParameter("@ThangNam", SqlDbType.VarChar) { Value = thangNam });
                cmdInit.ExecuteNonQuery();
            }

            // Nạp dữ liệu vào DataTable ngắt kết nối
            string selectQuery = @"SELECT c.MaCong, c.MaNV, nv.HoTen, pb.TenPB, c.ThangNam, c.SoNgayCong, c.SoGioOT
                                   FROM BangChamCong c
                                   INNER JOIN NhanVien nv ON c.MaNV = nv.MaNV
                                   INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                                   WHERE c.ThangNam = @ThangNam";

            SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
            selectCmd.Parameters.Add(new SqlParameter("@ThangNam", SqlDbType.VarChar) { Value = thangNam });

            _attendanceAdapter = new SqlDataAdapter(selectCmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(_attendanceAdapter);

            _attendanceTable = new DataTable();
            _attendanceAdapter.Fill(_attendanceTable);

            return _attendanceTable;
        }

        // Đồng bộ dữ liệu chấm công về SQL Server trong 1 thao tác bằng adapter.Update()
        public bool SaveAttendanceChanges()
        {
            if (_attendanceAdapter != null && _attendanceTable != null)
            {
                try
                {
                    _attendanceAdapter.Update(_attendanceTable);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi đồng bộ dữ liệu chấm công: " + ex.Message);
                }
            }
            return false;
        }

        // 5. CHỐT LƯƠNG & TÍNH LƯƠNG THÁNG (SqlTransaction - ACID)
        public bool LockAndCalculatePayrollTransaction(string thangNam, out string error)
        {
            error = string.Empty;

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Lấy toàn bộ thông tin lương cơ bản, hệ số, ngày công và giờ OT trong tháng
                    string queryData = @"SELECT c.MaNV, nv.LuongCoBan, nv.HeSoLuong, c.SoNgayCong, c.SoGioOT
                                         FROM BangChamCong c
                                         INNER JOIN NhanVien nv ON c.MaNV = nv.MaNV
                                         WHERE c.ThangNam = @ThangNam";

                    DataTable dtCalc = new DataTable();
                    using (SqlCommand cmdGet = new SqlCommand(queryData, conn, trans))
                    {
                        cmdGet.Parameters.Add(new SqlParameter("@ThangNam", SqlDbType.VarChar) { Value = thangNam });
                        SqlDataAdapter da = new SqlDataAdapter(cmdGet);
                        da.Fill(dtCalc);
                    }

                    if (dtCalc.Rows.Count == 0)
                    {
                        trans.Rollback();
                        error = "Chưa có dữ liệu chấm công cho tháng " + thangNam;
                        return false;
                    }

                    // Chèn/Cập nhật dữ liệu vào BangLuong
                    string insertSalaryQuery = @"IF EXISTS (SELECT 1 FROM BangLuong WHERE MaNV = @MaNV AND ThangNam = @ThangNam)
                                                 BEGIN
                                                     UPDATE BangLuong SET ThucLinh = @ThucLinh WHERE MaNV = @MaNV AND ThangNam = @ThangNam
                                                 END
                                                 ELSE
                                                 BEGIN
                                                     INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh) VALUES (@MaNV, @ThangNam, @ThucLinh)
                                                 END";

                    foreach (DataRow row in dtCalc.Rows)
                    {
                        int maNV = Convert.ToInt32(row["MaNV"]);
                        decimal luongCoBan = Convert.ToDecimal(row["LuongCoBan"]);
                        double heSo = Convert.ToDouble(row["HeSoLuong"]);
                        double ngayCong = Convert.ToDouble(row["SoNgayCong"]);
                        double gioOT = Convert.ToDouble(row["SoGioOT"]);

                        // CÔNG THỨC: ThucLinh = (LuongCoBan * HeSoLuong / 22) * SoNgayCong + (SoGioOT * 50,000)
                        decimal luongNgay = (luongCoBan * (decimal)heSo) / 22m;
                        decimal thucLinh = (luongNgay * (decimal)ngayCong) + ((decimal)gioOT * 50000m);

                        using (SqlCommand cmdSalary = new SqlCommand(insertSalaryQuery, conn, trans))
                        {
                            cmdSalary.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int) { Value = maNV });
                            cmdSalary.Parameters.Add(new SqlParameter("@ThangNam", SqlDbType.VarChar) { Value = thangNam });
                            cmdSalary.Parameters.Add(new SqlParameter("@ThucLinh", SqlDbType.Decimal) { Value = thucLinh });

                            cmdSalary.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    error = "Lỗi khi chốt lương: " + ex.Message;
                    return false;
                }
            }
        }

        // 6. Lấy kết quả Bảng Lương
        public DataTable GetPayrollReport(string thangNam)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT bl.MaLuong, bl.MaNV, nv.HoTen, pb.TenPB, bl.ThangNam, bl.ThucLinh
                                 FROM BangLuong bl
                                 INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                                 INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                                 WHERE bl.ThangNam = @ThangNam";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ThangNam", SqlDbType.VarChar) { Value = thangNam });
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}