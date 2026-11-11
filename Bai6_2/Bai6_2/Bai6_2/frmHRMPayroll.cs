using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HRMManagementApp.BUS;
using HRMManagementApp.DTO;
using MySqlConnector;

namespace Bai6_2
{
    public partial class frmHRMPayroll : Form
    {
        private readonly HRMBUS _bus = new HRMBUS();
        private bool _isEditMode = false;
        private int _selectedEmpId = -1;
        public frmHRMPayroll()
        {
            InitializeComponent();
        }

        private void frmHRMPayroll_Load(object sender, EventArgs e)
        {
            LoadDepartmentDropdowns();
            InitMonthYearDropdown();
            ConfigureGridEmployee();
            ConfigureGridAttendance();
            ConfigureGridPayroll();
            LoadEmployeeData();
        }
        private void LoadDepartmentDropdowns()
        {
            List<PhongBanDTO> depts = _bus.GetDepartments();

            cboDept.DataSource = depts;
            cboDept.DisplayMember = "TenPB";
            cboDept.ValueMember = "MaPB";

            List<PhongBanDTO> filterDepts = new List<PhongBanDTO>
            {
                new PhongBanDTO(0, "Tất cả phòng ban", "")
            };
            filterDepts.AddRange(depts);

            cboDeptFilter.DataSource = filterDepts;
            cboDeptFilter.DisplayMember = "TenPB";
            cboDeptFilter.ValueMember = "MaPB";
            cboDeptFilter.SelectedIndex = 0;
        }

        private void InitMonthYearDropdown()
        {
            cboMonthYear.Items.Clear();
            cboMonthYear.Items.Add("11/2026");
            cboMonthYear.Items.Add("12/2026");
            cboMonthYear.Items.Add("01/2027");
            cboMonthYear.SelectedIndex = 0;
        }

        private void ConfigureGridEmployee()
        {
            dgvEmployee.AutoGenerateColumns = false;
            dgvEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployee.ReadOnly = true;
            dgvEmployee.MultiSelect = false;
            dgvEmployee.Columns.Clear();

            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 70 });
            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ và Tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CCCD", HeaderText = "CCCD", Width = 110 });
            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPB", HeaderText = "Phòng Ban", Width = 130 });
            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LuongCoBan", HeaderText = "Lương CB", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvEmployee.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HeSoLuong", HeaderText = "Hệ Số", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void ConfigureGridAttendance()
        {
            dgvAttendance.AutoGenerateColumns = false;
            dgvAttendance.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvAttendance.ReadOnly = false; // Cho phép sửa trực tiếp SoNgayCong & SoGioOT
            dgvAttendance.Columns.Clear();

            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaCong", HeaderText = "Mã Công", Width = 80, ReadOnly = true });
            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 70, ReadOnly = true });
            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ và Tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPB", HeaderText = "Phòng Ban", Width = 130, ReadOnly = true });
            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoNgayCong", HeaderText = "Số Ngày Công", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoGioOT", HeaderText = "Số Giờ OT", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void ConfigureGridPayroll()
        {
            dgvPayroll.AutoGenerateColumns = false;
            dgvPayroll.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayroll.ReadOnly = true;
            dgvPayroll.Columns.Clear();

            dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 80 });
            dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ và Tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPB", HeaderText = "Phòng Ban", Width = 140 });
            dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThangNam", HeaderText = "Tháng/Năm", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThucLinh", HeaderText = "Thực Lĩnh (VNĐ)", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold) } });
        }
        private void LoadEmployeeData()
        {
            int deptId = Convert.ToInt32(cboDeptFilter.SelectedValue ?? 0);
            dgvEmployee.DataSource = _bus.SearchEmployees(txtSearch.Text.Trim(), deptId);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadEmployeeData();
        private void cboDeptFilter_SelectedIndexChanged(object sender, EventArgs e) => LoadEmployeeData();

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmployee.CurrentRow != null && dgvEmployee.CurrentRow.DataBoundItem is DataRowView drv)
            {
                _selectedEmpId = Convert.ToInt32(drv["MaNV"]);
                txtFullName.Text = drv["HoTen"].ToString();
                dtpDOB.Value = drv["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(drv["NgaySinh"]) : DateTime.Today;
                txtCCCD.Text = drv["CCCD"].ToString();
                cboDept.SelectedValue = Convert.ToInt32(drv["MaPB"]);
                txtBaseSalary.Text = Convert.ToDecimal(drv["LuongCoBan"]).ToString("N0");
                nudCoefficient.Value = Convert.ToDecimal(drv["HeSoLuong"]);
            }
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            SaveEmployeeData();
        }

        private void btnEditEmp_Click(object sender, EventArgs e)
        {
            if (_selectedEmpId <= 0) return;
            _isEditMode = true;
            SaveEmployeeData();
        }
        private void SaveEmployeeData()
        {
            if (!decimal.TryParse(txtBaseSalary.Text.Replace(",", "").Replace(".", ""), out decimal baseSalary))
            {
                MessageBox.Show("Lương cơ bản không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NhanVienDTO nv = new NhanVienDTO
            {
                MaNV = _selectedEmpId,
                HoTen = txtFullName.Text.Trim(),
                NgaySinh = dtpDOB.Value.Date,
                CCCD = txtCCCD.Text.Trim(),
                MaPB = Convert.ToInt32(cboDept.SelectedValue),
                LuongCoBan = baseSalary,
                HeSoLuong = (double)nudCoefficient.Value
            };

            if (_bus.SaveEmployee(nv, _isEditMode, out string err))
            {
                MessageBox.Show(string.Format("{0} hồ sơ nhân viên thành công!", _isEditMode ? "Cập nhật" : "Thêm mới"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeeData();
            }
            else
            {
                MessageBox.Show("Lỗi: " + err, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            if (_selectedEmpId <= 0) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_bus.DeleteEmployee(_selectedEmpId))
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeData();
                }
            }
        }

        private void btnClearEmp_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadAttendance_Click(object sender, EventArgs e)
        {
            string thangNam = cboMonthYear.SelectedItem.ToString();
            DataTable dtAtt = _bus.LoadAttendanceTable(thangNam);
            dgvAttendance.DataSource = dtAtt;

            // Đồng thời hiển thị bảng lương nếu đã chốt trước đó
            dgvPayroll.DataSource = _bus.GetPayrollReport(thangNam);
        }

        private void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                dgvAttendance.EndEdit();
                if (_bus.SaveAttendanceChanges())
                {
                    MessageBox.Show("💾 Đã lưu thay đổi chấm công về CSDL trong 1 thao tác (SqlCommandBuilder)!",
                                    "Thông báo Chấm Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLockPayroll_Click(object sender, EventArgs e)
        {
            string thangNam = cboMonthYear.SelectedItem.ToString();

            DialogResult dr = MessageBox.Show(string.Format("Xác nhận tính và CHỐT LƯƠNG cho tháng [{0}]?\n\nCông thức: Thực Lĩnh = (Lương CB * Hệ Số / 22) * Ngày Công + (Giờ OT * 50,000)", thangNam),
                                              "Xác nhận Chốt Lương (SqlTransaction)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // Trước khi chốt lương, tự động lưu lại các chỉnh sửa chấm công trên lưới
                btnSaveAttendance_Click(sender, e);

                if (_bus.LockAndCalculatePayroll(thangNam, out string err))
                {
                    MessageBox.Show(string.Format("🎉 CHỐT LƯƠNG THÁNG [{0}] THÀNH CÔNG!\nBảng lương chi tiết đã được ghi nhận vào SQL Server.", thangNam),
                                    "Thông báo SqlTransaction", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvPayroll.DataSource = _bus.GetPayrollReport(thangNam);
                }
                else
                {
                    MessageBox.Show("Lỗi chốt lương: " + err, "Giao dịch Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
