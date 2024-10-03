using DVLD.Controls;
using DVLD.Properties;
using DVLD.Tests.Vision_Test;
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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        
        private int _TestAppointmentID;
        private int _Trials;
 


        public frmListTestAppointments(int LocalDrivingLicenceID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenceID;
            _TestType = TestType;
        }


        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblTestAppointmentTitle.Text = "Vision Test Appointments";
                    this.Text = lblTestAppointmentTitle.Text;
                    pbTestAppointmentImage.Image = Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    lblTestAppointmentTitle.Text = "Written Test Appointments";
                    this.Text = lblTestAppointmentTitle.Text;
                    pbTestAppointmentImage.Image = Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    lblTestAppointmentTitle.Text = "Street Test Appointments";
                    this.Text = lblTestAppointmentTitle.Text;
                    pbTestAppointmentImage.Image = Resources.driving_test_512;
                    break;
            }
        }

        private void _LoadData()
        {
            _LoadTestTypeImageAndTitle();

            ctrlDrivingLicenceAndApplicationBasicInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            
            dgvVisionAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordCount.Text = dgvVisionAppointments.RowCount.ToString();

            if (dgvVisionAppointments.Rows.Count > 0)
            {
                dgvVisionAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvVisionAppointments.Columns[0].Width = 150;

                dgvVisionAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvVisionAppointments.Columns[1].Width = 200;

                dgvVisionAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvVisionAppointments.Columns[2].Width = 150;

                dgvVisionAppointments.Columns[3].HeaderText = "Is Locked";
                dgvVisionAppointments.Columns[3].Width = 100;
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenceApplication localDrivingLicenseApplication = clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(
                _LocalDrivingLicenseApplicationID);

            if (clsTestAppointment.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, (int)_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, you can not add new appointment",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmScheduleAddEditRetakeTest frm1 = new frmScheduleAddEditRetakeTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();

                _LoadData();
                return;
            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleAddEditRetakeTest frm2 = new frmScheduleAddEditRetakeTest(
                LastTest.TestAppointmentInfo.LocalDrivingLicenceApplicationID, _TestType);

            frm2.ShowDialog();
            _LoadData();
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvVisionAppointments.CurrentRow.Cells[0].Value;
            
            frmScheduleAddEditRetakeTest frm = new frmScheduleAddEditRetakeTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();

            _LoadData();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvVisionAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();

            _LoadData();
        }

        
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
