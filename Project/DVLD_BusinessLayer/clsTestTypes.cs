using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestTypes
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public enTestType ID {  get; set; }    
        public string Title { get; set; }
        public string Description { get; set; }
        public double Fees { get; set; }

        public clsTestTypes(enTestType ID, string Title, string Description, double Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
        }

        public static clsTestTypes Find(enTestType ID)
        {
            string Title = "", Description = "";
            double Fees = 0;

            if (clsTestTypesData.GetTestTypeInfoByID((int) ID, ref Title, ref Description, ref Fees))
                return new clsTestTypes(ID, Title, Description, Fees);
            else
                return null;
        }

        private bool _Update()
        {
            return clsTestTypesData.UpdateTestType((int) this.ID, this.Title, this.Description, this.Fees);
        }

        public bool Save()
        {
            return _Update();
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
