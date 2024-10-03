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
    public partial class frmNewInternationalDrivingLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;

        public frmNewInternationalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int obj) 
        {
            int SelectedLicenseID = obj;
            
            ctrlInternationalLicenseApplicationInfo1.LoadLocalLicense(SelectedLicenseID);

            llShowLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already has an active international license with ID = " + ActiveInternationalLicenseID.ToString(), 
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicensesInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssueInternationalLicense.Enabled = false;
                return;
            }

            btnIssueInternationalLicense.Enabled = true;
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }
        

        private void btnIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            InternationalLicense.DriverID = ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseCardWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

             ctrlInternationalLicenseApplicationInfo1.LoadInternationalLicenseApplicationID(InternationalLicense.ApplicationID);
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            ctrlInternationalLicenseApplicationInfo1.LoadInternationalLicenseID(_InternationalLicenseID);

            MessageBox.Show("International License Issued Successfully with ID = " + InternationalLicense.InternationalLicenseID.ToString(),
                "License Issued");
                    
            llShowLicensesInfo.Enabled = true;
            btnIssueInternationalLicense.Enabled = false;
            ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewInternationalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseCardWithFilter1.txtLicenseIDFocus();
        }
    }
}
