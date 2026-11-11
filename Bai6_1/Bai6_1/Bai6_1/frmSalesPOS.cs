using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using RetailSalesApp.BUS;
using RetailSalesApp.DTO;
using MySqlConnector;

namespace Bai6_1
{
    public partial class frmSalesPOS : Form
    {
        private readonly SalesBUS _bus = new SalesBUS();
        private List<ChiTietHoaDonDTO> _cartList = new List<ChiTietHoaDonDTO>();
        public frmSalesPOS()
        {
            InitializeComponent();
        }

        private void frmSalesPOS_Load(object sender, EventArgs e)
        {
            LoadStaffDropdown();
            LoadCustomerDropdown();
            ConfigureGridCatalog();
            ConfigureGridCart();
            LoadProductCatalog();
        }
        private void LoadStaffDropdown()
        {
            List<NhanVienDTO> staffList = _bus.GetEmployees();
            cboStaff.DataSource = staffList;
            cboStaff.DisplayMember = "TenNV";
            cboStaff.ValueMember = "MaNV";
        }

        private void LoadCustomerDropdown()
        {
            List<KhachHangDTO> customerList = _bus.GetCustomers();
            cboCustomer.DataSource = customerList;
            cboCustomer.DisplayMember = "TenKH";
            cboCustomer.ValueMember = "MaKH";
        }

        private void ConfigureGridCatalog()
        {
            dgvProductCatalog.AutoGenerateColumns = false;
            dgvProductCatalog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductCatalog.ReadOnly = true;
            dgvProductCatalog.MultiSelect = false;
            dgvProductCatalog.Columns.Clear();

            dgvProductCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSP", HeaderText = "Mã SP/Vạch", Width = 110 });
            dgvProductCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSP", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProductCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaBan", HeaderText = "Giá Bán", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvProductCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "Tồn Kho", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void ConfigureGridCart()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.ReadOnly = true;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSP", HeaderText = "Mã SP", Width = 100 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSP", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "SL", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành Tiền", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void LoadProductCatalog()
        {
            dgvProductCatalog.DataSource = _bus.SearchProducts(txtSearchProduct.Text.Trim());
        }

        private void cboStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSearchProduct_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            LoadProductCatalog();
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (dgvProductCatalog.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView row = dgvProductCatalog.CurrentRow.DataBoundItem as DataRowView;
            string maSP = row["MaSP"].ToString();
            string tenSP = row["TenSP"].ToString();
            decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
            int buyQty = (int)nudQuantity.Value;

            var existingItem = _cartList.Find(x => x.MaSP == maSP);
            if (existingItem != null)
            {
                existingItem.SoLuong += buyQty;
            }
            else
            {
                _cartList.Add(new ChiTietHoaDonDTO
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    SoLuong = buyQty,
                    DonGia = giaBan
                });
            }

            RefreshCartGrid();
        }
        private void RefreshCartGrid()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cartList;

            decimal totalMoney = 0;
            foreach (var item in _cartList) totalMoney += item.ThanhTien;

            lblTotalAmount.Text = string.Format("Tổng tiền: {0:N0} VNĐ", totalMoney);
            lblAccumulatedPoints.Text = string.Format("Điểm tích lũy cộng: {0} điểm", (int)(totalMoney / 100000m));
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cboStaff.SelectedValue == null) return;

            int staffId = Convert.ToInt32(cboStaff.SelectedValue);
            int? customerId = cboCustomer.SelectedValue != null ? (int?)Convert.ToInt32(cboCustomer.SelectedValue) : null;

            HoaDonDTO invoice = new HoaDonDTO
            {
                MaNV = staffId,
                MaKH = customerId
            };

            if (_bus.Checkout(invoice, _cartList, out string error))
            {
                MessageBox.Show("🎉 THANH TOÁN THÀNH CÔNG!\n\nĐã chèn Hóa đơn, Chi tiết hóa đơn, trừ tồn kho và cộng điểm tích lũy đồng bộ.",
                                "Thông báo POS Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _cartList.Clear();
                RefreshCartGrid();
                LoadCustomerDropdown(); // Tải lại danh sách để cập nhật điểm tích lũy
                LoadProductCatalog();    // Tải lại danh sách để cập nhật số lượng tồn kho
            }
            else
            {
                MessageBox.Show("Giao dịch bị HỦY (Rollback)! " + error, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
