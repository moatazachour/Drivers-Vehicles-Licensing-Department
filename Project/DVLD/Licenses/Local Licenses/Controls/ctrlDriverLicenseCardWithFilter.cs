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
    public partial class ctrlDriverLicenseCardWithFilter : UserControl
    {
        // For The Custom Event OnLicenseSelected
        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int Result)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(Result);
            }
        }

        public ctrlDriverLicenseCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }


        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseCard1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseCard1.SelectedLicenseInfo; }
        }


        public void LoadLicenseInfo(int LicenseID)
        {
            mtxtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseCard1.LoadDataByLicenseID(LicenseID);
            _LicenseID = ctrlDriverLicenseCard1.LicenseID;
            
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                LicenseSelected(_LicenseID);
        }

        public void _FillLicenseInfo()
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtLicenseID.Focus();
                return;

            }

            _LicenseID = int.Parse(mtxtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }


        private void btnLicenseSearch_Click(object sender, EventArgs e)
        {
            _FillLicenseInfo();
        }


        public void txtLicenseIDFocus()
        {
            mtxtLicenseID.Focus();
        }

        private void mtxtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(mtxtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(mtxtLicenseID, null);
            }
        }

        private void mtxtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnLicenseSearch.PerformClick();
            }

        }
    }
}
