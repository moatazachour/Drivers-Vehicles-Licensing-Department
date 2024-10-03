using DVLD.Controls;
using DVLD.Global_Classes;
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

namespace DVLD.Apps
{
    public partial class frmNewLocalDrivingLicenceApplication : Form
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private int _LocalLicenceApplicationID;
        private int _SelectedPersonID = -1;
        private clsLocalDrivingLicenceApplication _LocalDrivingLicenseApplication;

        public frmNewLocalDrivingLicenceApplication()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmNewLocalDrivingLicenceApplication(int LocalLicenceApplicationID)
        {
            InitializeComponent();

            _LocalLicenceApplicationID = LocalLicenceApplicationID;
            
            _Mode = enMode.Update;
        }

        private void _UploadLicenceClasses()
        {
            DataTable dtLicenceClasses = clsLicenceClass.GetAllLicenceClasses();

            foreach (DataRow Row in dtLicenceClasses.Rows)
            {
                cbLicenceClass.Items.Add(Row["ClassName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _UploadLicenceClasses();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving Licence Application";
                this.Text = "New Local Driving Licence Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenceApplication();
                ctrlPersonCardWithFilter1.FilterFocus();
                tabApplicationInfo.Enabled = false;

                cbLicenceClass.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUserID.Text = clsGlobalSettings.CurrentUser.UserName;
            }
            else
            {
                lblMode.Text = "Update Local DrivingLicense Application";
                this.Text = "Update Local DrivingLicense Application";

                tabApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(_LocalLicenceApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplication, "Application Not Found", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);

            lblLocalApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenceApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenceClass.SelectedIndex = cbLicenceClass.FindString(clsLicenceClass.Find(_LocalDrivingLicenseApplication.LicenceClassID).ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUserID.Text = clsUser.Find(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

        }

        

        private void frmNewLocalDrivingLicenceApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tabApplicationInfo;
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tabApplicationInfo.Enabled = true;
                tabControl1.SelectedTab = tabApplicationInfo;
            }

            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicenceClass.Find(cbLicenceClass.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense,
                LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id = " + 
                    ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenceClass.Focus();
                return;
            }

            // Check If License Exist
            if (clsLicense.IsLicenseExistByPersonID(_SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToDouble(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobalSettings.CurrentUserID;
            _LocalDrivingLicenseApplication.LicenceClassID = LicenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {
                lblMode.Text = "Update Local Driving Licence Application";
                lblLocalApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenceApplicationID.ToString();
                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully.", "Saved");
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Failed");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmNewLocalDrivingLicenceApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
