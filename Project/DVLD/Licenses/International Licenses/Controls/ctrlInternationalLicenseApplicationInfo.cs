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

namespace DVLD.Controls
{
    public partial class ctrlInternationalLicenseApplicationInfo : UserControl
    {
        public ctrlInternationalLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUserID.ToString();
        }

        public void LoadLocalLicense(int LicenseID)
        {
            lblLocalLicenseID.Text = LicenseID.ToString();
        }

        public void LoadInternationalLicenseID(int LoadInternationalLicenseID)
        {
            lblInternationalLicenseID.Text = LoadInternationalLicenseID.ToString();
        }

        public void LoadInternationalLicenseApplicationID(int ApplicationID)
        {
            lblInternationalApplicationID.Text = ApplicationID.ToString();  
        }


        private void ctrlInternationalLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
