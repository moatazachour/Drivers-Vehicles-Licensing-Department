 using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenceApplication : clsApplication
    {
        public new enum enMode { AddNew = 0, Update = 1 }
        public new enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenceApplicationID { get; set; }
        public int LicenceClassID { get; set; }
        public clsLicenceClass LicenseClassInfo; // Denormalization
        
        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName;
                // return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        public clsLocalDrivingLicenceApplication()
        {
            this.LocalDrivingLicenceApplicationID = -1;
            this.LicenceClassID = -1;

            Mode = enMode.AddNew;

        }

        public clsLocalDrivingLicenceApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
             DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             double PaidFees, int CreatedByUserID, int LicenseClassID) :
            base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.LocalDrivingLicenceApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenceClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenceClass.Find(LicenceClassID);
            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.LocalDrivingLicenceApplicationID = clsLocalDrivingLicenceApplicationData.
                AddNewLocalDrivingLicenceApplication(this.ApplicationID, this.LicenceClassID);

            return (this.LocalDrivingLicenceApplicationID != -1);
        }

        private bool _Update()
        {
            return clsLocalDrivingLicenceApplicationData.UpdateLicenceApplication
                (this.LocalDrivingLicenceApplicationID, this.ApplicationID, this.LicenceClassID);
        }


        public static clsLocalDrivingLicenceApplication FindByLocalDrivingLicenseAppID(int LocalDrivingLicenceApplicationID)
        {
            int ApplicationID = -1, LicenceClassID = -1;
            
            bool IsFound = clsLocalDrivingLicenceApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenceApplicationID, ref ApplicationID, ref LicenceClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenceApplication(LocalDrivingLicenceApplicationID, ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus, 
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenceClassID);
            }
            else
            {
                return null;
            }
        }

        public static clsLocalDrivingLicenceApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenceApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                clsApplication Application = FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenceApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                        Application.ApplicationDate, Application.ApplicationTypeID,
                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                        Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;

        }

        public new bool Save()
        {
            base.Mode = (clsApplication.enMode) Mode;
            
            if (!base.Save()) 
                return false;


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


        public static DataTable GetAllLocalLicenceApplications()
        {
            return clsLocalDrivingLicenceApplicationData.GetAllLocalLicenceApplications();
        }

        // Using View
        public static DataTable GetAllLocalDrivingLicenceApplications()
        {
            return clsLocalDrivingLicenceApplicationData.GetAllLocalLicenceApplicationsUsingView();
        }


        public new bool Delete()
        {
            bool IsLocalDivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDivingApplicationDeleted = clsLocalDrivingLicenceApplicationData.DeleteLocalApplication(this.LocalDrivingLicenceApplicationID);

            if (!IsLocalDivingApplicationDeleted) 
                return false;

            IsBaseApplicationDeleted = base.Delete();

            return IsBaseApplicationDeleted;
        }

        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() != -1;
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenceClassID);
        }

        public bool DoesPassTestType(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingLicenceApplicationData.DoesPassTestType(LocalDrivingLicenceApplicationID, (int)TestType);
        }


        public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestType)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenceClassID, TestType);
        }


        public bool DoesAttendTestType(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingLicenceApplicationData.DoesAttendTestType(this.LocalDrivingLicenceApplicationID, (int)TestType);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingLicenceApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenceApplicationID, (int)TestType);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenceApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenceApplicationID);
        }


        public static bool isLocalLicenceExistAndNew(int PersonID, int LicenceClass)
        {
            return clsLocalDrivingLicenceApplicationData.IsLocalLicenceExistAndNew(PersonID, LicenceClass);   
        }

        public static bool isLocalLicenceCompleted(int LocalDrivingLicenceApplicationID)
        {
            return clsLocalDrivingLicenceApplicationData.IsLocalLicenceCompleted(LocalDrivingLicenceApplicationID);    
        }

        public static int GetPassedTests(int LocalDrivingLicenceApplicationID)
        {
            return clsTest.GetPassedTests(LocalDrivingLicenceApplicationID);
        }


        public int IssueLicenseForTheFirstTime(string Notes, int CurrentUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CurrentUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenceClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }



        
    }
}
