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

namespace DVLD.Controls
{
    public partial class ctrlDrivingLicenceAndApplicationBasicInfo : UserControl
    {
        
        private clsLocalDrivingLicenceApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _LicenseID;

        public int LocalDrivingLicenceID
        { 
            get { return _LocalDrivingLicenseApplicationID; } 
        }

        

        public ctrlDrivingLicenceAndApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblDrivingLicenceAppID.Text = "[????]";
            lblLicenceClass.Text = "[????]";
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            llShowLicenceInfo.Enabled = (_LicenseID != -1);

            lblDrivingLicenceAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblLicenceClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = clsLocalDrivingLicenceApplication.GetPassedTests(_LocalDrivingLicenseApplicationID).ToString() +
                "/3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenceID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenceID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenceApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenceID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenceID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenceApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenceID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }


        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_LocalDrivingLicenseApplication.GetActiveLicenseID());
            frm.ShowDialog();
        }
    }
}
