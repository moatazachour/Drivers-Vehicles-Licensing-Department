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

namespace DVLD.Tests.Vision_Test
{
    public partial class frmScheduleAddEditRetakeTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _AppointmentID = -1;


        public frmScheduleAddEditRetakeTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            _TestTypeID= TestTypeID;
            _AppointmentID= AppointmentID;
        }

        private void frmScheduleAddEditRetakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTestAppointment1.TestTypeID = _TestTypeID;
            ctrlScheduleTestAppointment1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
