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

namespace DVLD.Apps
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _AppliationTypeID;
        private clsApplicationTypes _ApplicationType;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _AppliationTypeID = ApplicationTypeID;
        }

        private void _LoadData()
        {
            lblID.Text = _AppliationTypeID.ToString();

            _ApplicationType = clsApplicationTypes.Find(_AppliationTypeID);
            
            if (_ApplicationType != null )
            {
                txtTitle.Text = _ApplicationType.Title;
                mtxtFees.Text = _ApplicationType.Fees.ToString();
            }
            
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.Title = txtTitle.Text.Trim();
            _ApplicationType.Fees = Convert.ToDouble(mtxtFees.Text.Trim());

            if (_ApplicationType.Save())
                MessageBox.Show("Data Saved Successfully.", "Saved");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Failed");
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            };
        }

        private void mtxtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(mtxtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(mtxtFees, null);
            };

            
            if (!clsValidation.IsNumber(mtxtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(mtxtFees, null);
            };
        }
    }
}
