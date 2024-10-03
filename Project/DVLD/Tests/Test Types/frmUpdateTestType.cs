using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_BusinessLayer;

namespace DVLD.Apps
{
    public partial class frmUpdateTestType : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsTestTypes _TestType;

        public frmUpdateTestType(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _LoadData()
        {
            _TestType = clsTestTypes.Find(_TestTypeID);
            
            if (_TestType != null)
            {
                lblID.Text = _TestType.ID.ToString();
                txtTitle.Text = _TestType.Title.ToString();
                txtDescription.Text = _TestType.Description.ToString();
                mtxtFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = txtTitle.Text.Trim();    
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToDouble(mtxtFees.Text.Trim());

            if (_TestType.Save())
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
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
