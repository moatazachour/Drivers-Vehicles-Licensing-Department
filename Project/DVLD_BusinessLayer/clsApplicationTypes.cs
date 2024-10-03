using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsApplicationTypes
    {
        public int ID {  get; set; }  
        public string Title { get; set; }    
        public double Fees {  get; set; }    

        public clsApplicationTypes(int ID, string Title, double Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static clsApplicationTypes Find(int ID)
        {
            string Title = "";
            double Fees = 0;

            if (clsApplicationTypesData.GetApplicationTypeInfoByID(ID, ref Title, ref Fees))
                return new clsApplicationTypes(ID, Title, Fees);
            else
                return null;
        }

        private bool _Update()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            return _Update();
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }
    }
}
