using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class ctrlScheduleTestAppointment : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };   
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        private clsLocalDrivingLicenceApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public clsTestTypes.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestType.Image = Resources.Vision_512;
                        break;


                    case clsTestTypes.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestType.Image = Resources.Written_Test_512;
                        break;
                         

                    case clsTestTypes.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestType.Image = Resources.driving_test_512;
                        break;
                }
            }
        }

        public ctrlScheduleTestAppointment()
        {
            InitializeComponent();
        }


        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1) 
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }


            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            
            else
                _CreationMode= enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gpRetakeTest.Enabled = true;
                lblModeTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gpRetakeTest.Enabled = false;
                lblModeTitle.Text = "Schedule Test";
                lblRAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblLocalDrivingLicenceApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenceApplicationID.ToString();
            lblLicenceClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.Find(_TestTypeID).Fees.ToString();
                dtpDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                

                _TestAppointment = new clsTestAppointment();
            }

            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = ((Convert.ToDouble(lblFees.Text)) + (Convert.ToDouble(lblRAppFees.Text))).ToString();

            if (!_HandleActiveTestAppointmentConstraint()) 
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePreviousTestConstraint())
                return;


        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointmentDate;

            dtpDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gpRetakeTest.Enabled = true;
                lblModeTitle.Text = "Schedule Retake Test";
            }

            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenceApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblModeSubtitle.Text = "Person already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblModeSubtitle.Visible = true;
                lblModeSubtitle.Text = "Person already sat for the test, Appointment Loacked.";
                dtpDate.Enabled=false;
                btnSave.Enabled=false;  
                return false;
            }
            else
            {
                lblModeSubtitle.Visible = false;
            }

            return true;
        }

        private bool _HandlePreviousTestConstraint()
        {
            switch (TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    
                    lblModeSubtitle.Visible = false;
                    return true;


                case clsTestTypes.enTestType.WrittenTest:
                    
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblModeSubtitle.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblModeSubtitle.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblModeSubtitle.Visible = false;
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }


                    return true;

                case clsTestTypes.enTestType.StreetTest:

                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblModeSubtitle.Text = "Cannot Sechule, Written Test should be passed first";
                        lblModeSubtitle.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblModeSubtitle.Visible = false;
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStatusDate = DateTime.Now;
                application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                application.CreatedByUserID = clsGlobalSettings.CurrentUserID;

                if (!application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = application.ApplicationID;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenceApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenceApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToDouble(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
