using DVLD.Apps.Local_License;
using DVLD.Tests;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Apps
{
    public partial class frmManageLocalDrivingLicenceApplications : Form
    {
        private DataTable _dtLocalLicenseApplications;

        public frmManageLocalDrivingLicenceApplications()
        {
            InitializeComponent(); 
        }

        private void _LoadData()
        {
            _dtLocalLicenseApplications = clsLocalDrivingLicenceApplication.GetAllLocalDrivingLicenceApplications();

            dgvLocalDrivingLicenceApplications.DataSource = _dtLocalLicenseApplications;
            lblRecordCount.Text = dgvLocalDrivingLicenceApplications.RowCount.ToString();

            if (dgvLocalDrivingLicenceApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenceApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenceApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenceApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenceApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenceApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenceApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenceApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenceApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenceApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenceApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenceApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenceApplications.Columns[5].Width = 150;
            }

            cbManageLocalApplication.SelectedIndex = 0;
        }

        private void frmManageLocalDrivingLicenceApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cbManageLocalApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtxtFilter.Visible = (cbManageLocalApplication.Text != "None");
            
            if (mtxtFilter.Visible)
            {
                mtxtFilter.Focus();
                mtxtFilter.Text = "";
            }

            _dtLocalLicenseApplications.DefaultView.RowFilter = "";
            lblRecordCount.Text = dgvLocalDrivingLicenceApplications.RowCount.ToString();
        }

        private void mtxtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbManageLocalApplication.Text)
            {
                case "L.D.L App":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;


                case "National No":
                    FilterColumn = "NationalNo.";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;


            }
            if (FilterColumn == "None")
            {
                _dtLocalLicenseApplications.DefaultView.RowFilter = null;
                lblRecordCount.Text = dgvLocalDrivingLicenceApplications.RowCount.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtLocalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, mtxtFilter.Text.Trim());
            else
                _dtLocalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, mtxtFilter.Text.Trim());

            lblRecordCount.Text = dgvLocalDrivingLicenceApplications.RowCount.ToString();
        }


        private void btnAddNewLocalDrivingLicence_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenceApplication frm = new frmNewLocalDrivingLicenceApplication();
            frm.ShowDialog();

            _LoadData();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                return;

            int LocalLicenceApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenceApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalLicenceApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadData();
                }
                else
                    MessageBox.Show("Error, Application Cancel Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenceApplication LocalDrivingLicenseApplication = 
                clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            issueDrivingLicenceFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenceToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && LocalDrivingLicenseApplication.ApplicationStatus == 
                clsApplication.enApplicationStatus.New;
            scheduleTestsToolStripMenuItem.Enabled = !LicenseExists;

            cancelApplicationToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            deleteApplicationToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            scheduleTestsToolStripMenuItem.Enabled = (!PassedStreetTest || !PassedWrittenTest || !PassedVisionTest)
                && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //scheduleTestsToolStripMenuItem.Enabled = !(PassedStreetTest && PassedWrittenTest && PassedVisionTest)
            //    && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (scheduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }


        private void _ScheduleTest(clsTestTypes.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();

            _LoadData();
        }


        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }



        private void issueDrivingLicenceFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueLicenseForFirstTime frm = new frmIssueLicenseForFirstTime((int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadData();    
        }

        private void showLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showPersonLicenceHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenceApplication LocalLicenseApplication = 
                clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            
            frmLicenseHistory frm = new frmLicenseHistory(LocalLicenseApplication.ApplicantPersonID);

            frm.ShowDialog();

            _LoadData();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivinLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenceApplication LocalLicenseApplication = 
                clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalDrivinLicenseApplicationID);

            if (MessageBox.Show("Are you sure you want to delete this application", "Confirm Delete", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (LocalLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("A is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value;
            frmNewLocalDrivingLicenceApplication frm = new frmNewLocalDrivingLicenceApplication(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mtxtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbManageLocalApplication.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplication frm = new frmLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenceApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
