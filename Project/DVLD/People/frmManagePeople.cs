using DVLD_BusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvManagePeople.DataSource = _dtPeople;
            lblTotalRecords.Text = dgvManagePeople.Rows.Count.ToString();
        }

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            dgvManagePeople.DataSource = _dtPeople;
            cbManagePeopleFilter.SelectedItem = "None";
            lblTotalRecords.Text = dgvManagePeople.Rows.Count.ToString();

            if (dgvManagePeople.Rows.Count > 0)
            {
                dgvManagePeople.Columns[0].HeaderText = "Person ID";
                dgvManagePeople.Columns[0].Width = 110;

                dgvManagePeople.Columns[1].HeaderText = "National No.";
                dgvManagePeople.Columns[1].Width = 120;


                dgvManagePeople.Columns[2].HeaderText = "First Name";
                dgvManagePeople.Columns[2].Width = 120;

                dgvManagePeople.Columns[3].HeaderText = "Second Name";
                dgvManagePeople.Columns[3].Width = 140;


                dgvManagePeople.Columns[4].HeaderText = "Third Name";
                dgvManagePeople.Columns[4].Width = 120;

                dgvManagePeople.Columns[5].HeaderText = "Last Name";
                dgvManagePeople.Columns[5].Width = 120;

                dgvManagePeople.Columns[6].HeaderText = "Gendor";
                dgvManagePeople.Columns[6].Width = 120;

                dgvManagePeople.Columns[7].HeaderText = "Date Of Birth";
                dgvManagePeople.Columns[7].Width = 140;

                dgvManagePeople.Columns[8].HeaderText = "Nationality";
                dgvManagePeople.Columns[8].Width = 120;


                dgvManagePeople.Columns[9].HeaderText = "Phone";
                dgvManagePeople.Columns[9].Width = 120;


                dgvManagePeople.Columns[10].HeaderText = "Email";
                dgvManagePeople.Columns[10].Width = 170;
            }
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbManagePeopleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtxtFilter.Visible = (cbManagePeopleFilter.Text != "None");

            if (mtxtFilter.Visible)
            {
                mtxtFilter.Mask = "";
                mtxtFilter.Text = "";
                mtxtFilter.Focus();
            }
        }

        private void mtxtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbManagePeopleFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (mtxtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvManagePeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, mtxtFilter.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, mtxtFilter.Text.Trim());

            lblTotalRecords.Text = dgvManagePeople.Rows.Count.ToString();
        }

        private void AddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(-1);
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(-1);
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvManagePeople.CurrentRow.Cells[0].Value;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dgvManagePeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvManagePeople.CurrentRow.Cells[0].Value.ToString()
                + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string PersonImage = clsPerson.Find((int)dgvManagePeople.CurrentRow.Cells[0].Value).ImagePath;

                if (PersonImage != "")
                    File.Delete(PersonImage);

                if (clsPerson.DeletePerson((int)dgvManagePeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _RefreshPeoplList();
                }
                else
                {
                    MessageBox.Show("Person is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void dgvManagePeople_DoubleClick(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvManagePeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void mtxtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbManagePeopleFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}

