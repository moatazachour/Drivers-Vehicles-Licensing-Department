using DVLD.Apps;
using DVLD.People;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtAllDrivers;
        
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            cbManageDriversFilter.SelectedIndex = 0;
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvListDrivers.DataSource = _dtAllDrivers;
            lblRecordCount.Text = dgvListDrivers.Rows.Count.ToString();
            if (dgvListDrivers.Rows.Count > 0)
            {
                dgvListDrivers.Columns[0].HeaderText = "Driver ID";
                dgvListDrivers.Columns[0].Width = 120;

                dgvListDrivers.Columns[1].HeaderText = "Person ID";
                dgvListDrivers.Columns[1].Width = 120;

                dgvListDrivers.Columns[2].HeaderText = "National No.";
                dgvListDrivers.Columns[2].Width = 140;

                dgvListDrivers.Columns[3].HeaderText = "Full Name";
                dgvListDrivers.Columns[3].Width = 320;

                dgvListDrivers.Columns[4].HeaderText = "Date";
                dgvListDrivers.Columns[4].Width = 170;

                dgvListDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvListDrivers.Columns[5].Width = 150;
            }
        }

        private void cbManageDriversFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtxtFilter.Visible = (cbManageDriversFilter.Text != "None");


            if (cbManageDriversFilter.Text == "None")
            {
                mtxtFilter.Enabled = false;
            }
            else
                mtxtFilter.Enabled = true;

            mtxtFilter.Text = "";
            mtxtFilter.Focus();
        }

        private void mtxtFilter_TextChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbManageDriversFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (mtxtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvListDrivers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, mtxtFilter.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, mtxtFilter.Text.Trim());

            lblRecordCount.Text = _dtAllDrivers.Rows.Count.ToString();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListDrivers.CurrentRow.Cells[1].Value;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();

            frmListDrivers_Load(null, null);
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListDrivers.CurrentRow.Cells[1].Value;


            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
