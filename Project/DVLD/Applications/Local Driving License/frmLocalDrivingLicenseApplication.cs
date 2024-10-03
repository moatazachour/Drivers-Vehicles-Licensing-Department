using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Apps.Local_License
{
    public partial class frmLocalDrivingLicenseApplication : Form
    {
        private int _LocalDivingLicenseApplicationID;

        public frmLocalDrivingLicenseApplication(int LocalDivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDivingLicenseApplicationID = LocalDivingLicenseApplicationID;
        }

        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenceAndApplicationBasicInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDivingLicenseApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
