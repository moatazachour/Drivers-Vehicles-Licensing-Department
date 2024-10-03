using DVLD.People;
using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                _ResetPersonCard();
                MessageBox.Show("Person with ID = " + PersonID.ToString() + " doesn\'t exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();

        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                _ResetPersonCard();
                MessageBox.Show("Person with National No = " + NationalNo.ToString() + " doesn\'t exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            _FillPersonInfo();

        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            if (_Person.ImagePath != "")
            {
                if (File.Exists(_Person.ImagePath))
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + _Person.ImagePath, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;

            _LoadPersonImage();
        }


        public void _ResetPersonCard()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblGendor.Text = "[????]";
            lblPhone.Text = "[????]";
            lblEmail.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.ImageLocation = null;
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);

        }
    }
}
