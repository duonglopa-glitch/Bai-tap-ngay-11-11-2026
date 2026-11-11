using System;
using System.Collections.Generic;
using System.Data;
using HRMManagementApp.DAL;
using HRMManagementApp.DTO;

namespace HRMManagementApp.BUS
{
    public class HRMBUS
    {
        private readonly HRMDAL _dal = new HRMDAL();

        public List<PhongBanDTO> GetDepartments() => _dal.GetDepartments();

        public DataTable SearchEmployees(string keyword, int deptId) => _dal.SearchEmployees(keyword ?? string.Empty, deptId);

        public bool SaveEmployee(NhanVienDTO nv, bool isEdit, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(nv.HoTen))
            {
                error = "Họ tên nhân viên không được để trống!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(nv.CCCD))
            {
                error = "Số CCCD không được để trống!";
                return false;
            }
            if (nv.LuongCoBan <= 0)
            {
                error = "Lương cơ bản phải lớn hơn 0!";
                return false;
            }

            return isEdit ? _dal.UpdateEmployee(nv) : _dal.InsertEmployee(nv);
        }

        public bool DeleteEmployee(int maNV) => _dal.DeleteEmployee(maNV);

        public DataTable LoadAttendanceTable(string thangNam) => _dal.LoadAttendanceTable(thangNam);

        public bool SaveAttendanceChanges() => _dal.SaveAttendanceChanges();

        public bool LockAndCalculatePayroll(string thangNam, out string error) => _dal.LockAndCalculatePayrollTransaction(thangNam, out error);

        public DataTable GetPayrollReport(string thangNam) => _dal.GetPayrollReport(thangNam);
    }
}