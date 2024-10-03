using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;
using DVLD_BusinessLayer;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using DVLD.Global_Classes;


namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public event Action<int> DataBack; 


        private enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode;

        private enum enGendor { Male = 0, Female = 1 }
        private enGendor Gendor;

        private clsPerson _Person;
        private int _PersonID;

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            this._PersonID = PersonID;

            if (PersonID == -1)
                Mode = enMode.AddNew;
            else
                Mode = enMode.Update;
        }
        
        private void _LoadCountries()
        {
            DataTable CountriesDataTable = clsCountry.GetAllCountries();

            foreach (DataRow row in CountriesDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private DateTime GetMinimumAgeForPerson()
        {
            DateTime maxDate;
            maxDate = DateTime.Now.AddYears(-18);

            return maxDate;
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);


            if (_Person.ImagePath != "")
            {
                pbPersonPhoto.ImageLocation = _Person.ImagePath;

            }

            llRemove.Visible = (_Person.ImagePath != "");
        }

        private void _ResetDefaultValues()
        {
            _LoadCountries();

            if (Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonPhoto.Image = Resources.Male_512;
            else
                pbPersonPhoto.Image = Resources.Female_512;

            llRemove.Visible = (pbPersonPhoto.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Tunisia");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (Mode == enMode.Update)
                _LoadData();
        }

        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonPhoto.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }

                if (pbPersonPhoto.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonPhoto.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonPhoto.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;

            int CountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Phone = txtPhone.Text.Trim(); ;
            _Person.Email = txtEmail.Text.Trim(); ;
            _Person.NationalityCountryID = CountryID;
            _Person.Address = txtAddress.Text.Trim();
            
            if (rbMale.Checked)
                _Person.Gendor = (byte)enGendor.Male;
            else
                _Person.Gendor = (byte)enGendor.Female;


            if (pbPersonPhoto.ImageLocation != null)
                _Person.ImagePath = pbPersonPhoto.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved");
                Mode = enMode.Update;
                lblMode.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                
                DataBack?.Invoke(_Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Failed");
            
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonPhoto.Load(selectedFilePath);
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPhoto.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonPhoto.Image = Resources.Male_512;
            else
                pbPersonPhoto.Image = Resources.Female_512;

            llRemove.Visible = false;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonPhoto.ImageLocation == null)
                pbPersonPhoto.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonPhoto.ImageLocation == null)
                pbPersonPhoto.Image = Resources.Female_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DataBack?.Invoke(_Person.PersonID);

            this.Close();
        }

        // VALIDATIONS

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }


       

    }
}
