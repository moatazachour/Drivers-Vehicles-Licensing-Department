using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLicenceClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; } 
        public byte DefaultValidityLength { get; set; }
        public double ClassFees { get; set; }

        public clsLicenceClass(int ID, string ClassName, string ClassDescription, byte MinimumAllowedAge,
            byte DefaultValidityLength, double ClassFees)
        {
            this.LicenseClassID = ID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static clsLicenceClass Find(int ID)
        {
            string ClassName = string.Empty, ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            double ClassFees = 0;   

            if (clsLicenceClassData.GetLicenceClassByID(ID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
                return new clsLicenceClass(ID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

        public static clsLicenceClass Find(string ClassName)
        {
            int ID = -1;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            double ClassFees = 0;

            if (clsLicenceClassData.GetLicenceClassByClassName(ref ID, ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
                return new clsLicenceClass(ID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

        public static DataTable GetAllLicenceClasses()
        {
            return clsLicenceClassData.GetAllLicenceClasses();
        }

        private bool _Update()
        {
            return clsLicenceClassData.UpdateLicenceClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge,
                this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            return _Update();
        }
    }
}
