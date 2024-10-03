using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID { get; set; } 
        public int LocalDrivingLicenceApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo;

        public int TestID
        {
            get { return _GetTestID();  }
        }

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;
            this.LocalDrivingLicenceApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        public clsTestAppointment(int TestAppointmentID, clsTestTypes.enTestType TestTypeID, int LocalDrivingLicenceApplicationID,
            DateTime AppointmentDate, double PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenceApplicationID = LocalDrivingLicenceApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);

            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenceApplicationID = -1, CreatedByUserID = -1, RetakeApplicationID = -1;   
            DateTime AppointmentDate = DateTime.Now;    
            double PaidFees = 0;
            bool IsLocked = false;

            if (clsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenceApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestTypes.enTestType)TestTypeID, LocalDrivingLicenceApplicationID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeApplicationID);
            else
                return null;
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenceApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            double PaidFees = 0;
            bool IsLocked = false;

            if (clsTestAppointmentData.GetTheLastAppointment(ref TestAppointmentID, (int) TestTypeID, LocalDrivingLicenceApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeApplicationID))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenceApplicationID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeApplicationID);
            else
                return null;
        }


        private bool _AddNew()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int) this.TestTypeID, 
                this.LocalDrivingLicenceApplicationID, this.AppointmentDate, this.PaidFees, 
                this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _Update()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, 
                this.LocalDrivingLicenceApplicationID, this.AppointmentDate, this.PaidFees, 
                this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;   
                    }

                case enMode.Update:
                    return _Update();
            }

            return false;
        }


        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenceApplicationID, 
            clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenceApplicationID, 
                (int)TestTypeID);
        }


        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenceApplicationID, 
            int TestTypeID)
        {
            return clsTestAppointmentData.IsTestAppointmentExistAndUnlocked(LocalDrivingLicenceApplicationID,
                TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }



    }
}
