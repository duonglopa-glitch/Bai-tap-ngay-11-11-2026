namespace Bai6_2
{
    partial class frmHRMPayroll
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabEmployee = new System.Windows.Forms.TabPage();
            this.tabPayroll = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDeptFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.nudCoefficient = new System.Windows.Forms.NumericUpDown();
            this.cboDept = new System.Windows.Forms.ComboBox();
            this.txtBaseSalary = new System.Windows.Forms.TextBox();
            this.btnAddEmp = new System.Windows.Forms.Button();
            this.btnDeleteEmp = new System.Windows.Forms.Button();
            this.btnEditEmp = new System.Windows.Forms.Button();
            this.btnClearEmp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMonthYear = new System.Windows.Forms.ComboBox();
            this.btnLoadAttendance = new System.Windows.Forms.Button();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();
            this.btnSaveAttendance = new System.Windows.Forms.Button();
            this.btnLockPayroll = new System.Windows.Forms.Button();
            this.dgvPayroll = new System.Windows.Forms.DataGridView();
            this.tabControlMain.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            this.tabPayroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoefficient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayroll)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabEmployee);
            this.tabControlMain.Controls.Add(this.tabPayroll);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1232, 703);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.Tag = "";
            // 
            // tabEmployee
            // 
            this.tabEmployee.Controls.Add(this.groupBox1);
            this.tabEmployee.Controls.Add(this.dgvEmployee);
            this.tabEmployee.Controls.Add(this.txtSearch);
            this.tabEmployee.Controls.Add(this.label2);
            this.tabEmployee.Controls.Add(this.cboDeptFilter);
            this.tabEmployee.Controls.Add(this.label1);
            this.tabEmployee.Location = new System.Drawing.Point(4, 25);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmployee.Size = new System.Drawing.Size(1224, 674);
            this.tabEmployee.TabIndex = 0;
            this.tabEmployee.Text = "Quản lý hồ sơ";
            this.tabEmployee.UseVisualStyleBackColor = true;
            // 
            // tabPayroll
            // 
            this.tabPayroll.Controls.Add(this.dgvPayroll);
            this.tabPayroll.Controls.Add(this.btnLockPayroll);
            this.tabPayroll.Controls.Add(this.btnSaveAttendance);
            this.tabPayroll.Controls.Add(this.dgvAttendance);
            this.tabPayroll.Controls.Add(this.btnLoadAttendance);
            this.tabPayroll.Controls.Add(this.cboMonthYear);
            this.tabPayroll.Controls.Add(this.label3);
            this.tabPayroll.Location = new System.Drawing.Point(4, 25);
            this.tabPayroll.Name = "tabPayroll";
            this.tabPayroll.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayroll.Size = new System.Drawing.Size(1224, 674);
            this.tabPayroll.TabIndex = 1;
            this.tabPayroll.Text = "Chấm công & Tính lương";
            this.tabPayroll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phòng ban:";
            // 
            // cboDeptFilter
            // 
            this.cboDeptFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptFilter.FormattingEnabled = true;
            this.cboDeptFilter.Location = new System.Drawing.Point(92, 6);
            this.cboDeptFilter.Name = "cboDeptFilter";
            this.cboDeptFilter.Size = new System.Drawing.Size(236, 24);
            this.cboDeptFilter.TabIndex = 1;
            this.cboDeptFilter.SelectedIndexChanged += new System.EventHandler(this.cboDeptFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(465, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(306, 22);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Location = new System.Drawing.Point(6, 49);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.RowHeadersWidth = 51;
            this.dgvEmployee.RowTemplate.Height = 24;
            this.dgvEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployee.Size = new System.Drawing.Size(895, 617);
            this.dgvEmployee.TabIndex = 4;
            this.dgvEmployee.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearEmp);
            this.groupBox1.Controls.Add(this.btnEditEmp);
            this.groupBox1.Controls.Add(this.btnDeleteEmp);
            this.groupBox1.Controls.Add(this.btnAddEmp);
            this.groupBox1.Controls.Add(this.txtBaseSalary);
            this.groupBox1.Controls.Add(this.cboDept);
            this.groupBox1.Controls.Add(this.nudCoefficient);
            this.groupBox1.Controls.Add(this.txtCCCD);
            this.groupBox1.Controls.Add(this.dtpDOB);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(907, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 626);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin nhân viên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Họ tên:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ngày sinh:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Hệ số: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Lương cơ bản:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "CCCD: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 347);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "Phòng ban:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(128, 64);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(166, 22);
            this.txtFullName.TabIndex = 7;
            // 
            // dtpDOB
            // 
            this.dtpDOB.Location = new System.Drawing.Point(128, 114);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(166, 22);
            this.dtpDOB.TabIndex = 8;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(128, 169);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(166, 22);
            this.txtCCCD.TabIndex = 9;
            // 
            // nudCoefficient
            // 
            this.nudCoefficient.Location = new System.Drawing.Point(128, 286);
            this.nudCoefficient.Name = "nudCoefficient";
            this.nudCoefficient.Size = new System.Drawing.Size(166, 22);
            this.nudCoefficient.TabIndex = 10;
            // 
            // cboDept
            // 
            this.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDept.FormattingEnabled = true;
            this.cboDept.Location = new System.Drawing.Point(128, 339);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(166, 24);
            this.cboDept.TabIndex = 11;
            // 
            // txtBaseSalary
            // 
            this.txtBaseSalary.Location = new System.Drawing.Point(128, 227);
            this.txtBaseSalary.Name = "txtBaseSalary";
            this.txtBaseSalary.Size = new System.Drawing.Size(166, 22);
            this.txtBaseSalary.TabIndex = 12;
            // 
            // btnAddEmp
            // 
            this.btnAddEmp.Location = new System.Drawing.Point(22, 438);
            this.btnAddEmp.Name = "btnAddEmp";
            this.btnAddEmp.Size = new System.Drawing.Size(117, 46);
            this.btnAddEmp.TabIndex = 13;
            this.btnAddEmp.Text = "Add";
            this.btnAddEmp.UseVisualStyleBackColor = true;
            this.btnAddEmp.Click += new System.EventHandler(this.btnAddEmp_Click);
            // 
            // btnDeleteEmp
            // 
            this.btnDeleteEmp.Location = new System.Drawing.Point(22, 500);
            this.btnDeleteEmp.Name = "btnDeleteEmp";
            this.btnDeleteEmp.Size = new System.Drawing.Size(117, 46);
            this.btnDeleteEmp.TabIndex = 14;
            this.btnDeleteEmp.Text = "Delete";
            this.btnDeleteEmp.UseVisualStyleBackColor = true;
            this.btnDeleteEmp.Click += new System.EventHandler(this.btnDeleteEmp_Click);
            // 
            // btnEditEmp
            // 
            this.btnEditEmp.Location = new System.Drawing.Point(168, 438);
            this.btnEditEmp.Name = "btnEditEmp";
            this.btnEditEmp.Size = new System.Drawing.Size(117, 46);
            this.btnEditEmp.TabIndex = 14;
            this.btnEditEmp.Text = "Edit";
            this.btnEditEmp.UseVisualStyleBackColor = true;
            this.btnEditEmp.Click += new System.EventHandler(this.btnEditEmp_Click);
            // 
            // btnClearEmp
            // 
            this.btnClearEmp.Location = new System.Drawing.Point(168, 500);
            this.btnClearEmp.Name = "btnClearEmp";
            this.btnClearEmp.Size = new System.Drawing.Size(117, 46);
            this.btnClearEmp.TabIndex = 15;
            this.btnClearEmp.Text = "Clear";
            this.btnClearEmp.UseVisualStyleBackColor = true;
            this.btnClearEmp.Click += new System.EventHandler(this.btnClearEmp_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tháng/Năm";
            // 
            // cboMonthYear
            // 
            this.cboMonthYear.FormattingEnabled = true;
            this.cboMonthYear.Location = new System.Drawing.Point(116, 21);
            this.cboMonthYear.Name = "cboMonthYear";
            this.cboMonthYear.Size = new System.Drawing.Size(132, 24);
            this.cboMonthYear.TabIndex = 1;
            // 
            // btnLoadAttendance
            // 
            this.btnLoadAttendance.BackColor = System.Drawing.Color.LightGreen;
            this.btnLoadAttendance.Location = new System.Drawing.Point(943, 16);
            this.btnLoadAttendance.Name = "btnLoadAttendance";
            this.btnLoadAttendance.Size = new System.Drawing.Size(167, 42);
            this.btnLoadAttendance.TabIndex = 2;
            this.btnLoadAttendance.Text = "Nạp Bảng Chấm Công";
            this.btnLoadAttendance.UseVisualStyleBackColor = false;
            this.btnLoadAttendance.Click += new System.EventHandler(this.btnLoadAttendance_Click);
            // 
            // dgvAttendance
            // 
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendance.Location = new System.Drawing.Point(8, 64);
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.ReadOnly = true;
            this.dgvAttendance.RowHeadersWidth = 51;
            this.dgvAttendance.RowTemplate.Height = 24;
            this.dgvAttendance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttendance.Size = new System.Drawing.Size(1208, 258);
            this.dgvAttendance.TabIndex = 3;
            // 
            // btnSaveAttendance
            // 
            this.btnSaveAttendance.BackColor = System.Drawing.Color.LightCoral;
            this.btnSaveAttendance.Location = new System.Drawing.Point(717, 339);
            this.btnSaveAttendance.Name = "btnSaveAttendance";
            this.btnSaveAttendance.Size = new System.Drawing.Size(181, 49);
            this.btnSaveAttendance.TabIndex = 4;
            this.btnSaveAttendance.Text = "Lưu Chấm Công";
            this.btnSaveAttendance.UseVisualStyleBackColor = false;
            this.btnSaveAttendance.Click += new System.EventHandler(this.btnSaveAttendance_Click);
            // 
            // btnLockPayroll
            // 
            this.btnLockPayroll.BackColor = System.Drawing.Color.LightGreen;
            this.btnLockPayroll.Location = new System.Drawing.Point(943, 339);
            this.btnLockPayroll.Name = "btnLockPayroll";
            this.btnLockPayroll.Size = new System.Drawing.Size(181, 49);
            this.btnLockPayroll.TabIndex = 5;
            this.btnLockPayroll.Text = "Chốt Lương Tháng";
            this.btnLockPayroll.UseVisualStyleBackColor = false;
            this.btnLockPayroll.Click += new System.EventHandler(this.btnLockPayroll_Click);
            // 
            // dgvPayroll
            // 
            this.dgvPayroll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayroll.Location = new System.Drawing.Point(8, 404);
            this.dgvPayroll.Name = "dgvPayroll";
            this.dgvPayroll.ReadOnly = true;
            this.dgvPayroll.RowHeadersWidth = 51;
            this.dgvPayroll.RowTemplate.Height = 24;
            this.dgvPayroll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayroll.Size = new System.Drawing.Size(1208, 262);
            this.dgvPayroll.TabIndex = 6;
            // 
            // frmHRMPayroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 703);
            this.Controls.Add(this.tabControlMain);
            this.Name = "frmHRMPayroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Nhân Sự & Tính Lương";
            this.Load += new System.EventHandler(this.frmHRMPayroll_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabEmployee.ResumeLayout(false);
            this.tabEmployee.PerformLayout();
            this.tabPayroll.ResumeLayout(false);
            this.tabPayroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoefficient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayroll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabEmployee;
        private System.Windows.Forms.TabPage tabPayroll;
        private System.Windows.Forms.ComboBox cboDeptFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClearEmp;
        private System.Windows.Forms.Button btnEditEmp;
        private System.Windows.Forms.Button btnDeleteEmp;
        private System.Windows.Forms.Button btnAddEmp;
        private System.Windows.Forms.TextBox txtBaseSalary;
        private System.Windows.Forms.ComboBox cboDept;
        private System.Windows.Forms.NumericUpDown nudCoefficient;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Button btnLockPayroll;
        private System.Windows.Forms.Button btnSaveAttendance;
        private System.Windows.Forms.DataGridView dgvAttendance;
        private System.Windows.Forms.Button btnLoadAttendance;
        private System.Windows.Forms.ComboBox cboMonthYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPayroll;
    }
}

