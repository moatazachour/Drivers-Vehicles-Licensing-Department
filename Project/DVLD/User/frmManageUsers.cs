using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmManageUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvManageUsers.DataSource = _dtAllUsers;
            lblTotalRecords.Text = dgvManageUsers.RowCount.ToString();
            cbManageUsersFilter.SelectedIndex = 0;

            if (dgvManageUsers.Rows.Count > 0)
            {
                dgvManageUsers.Columns[0].HeaderText = "User ID";
                dgvManageUsers.Columns[0].Width = 110;

                dgvManageUsers.Columns[1].HeaderText = "Person ID";
                dgvManageUsers.Columns[1].Width = 120;

                dgvManageUsers.Columns[2].HeaderText = "Full Name";
                dgvManageUsers.Columns[2].Width = 350;

                dgvManageUsers.Columns[3].HeaderText = "UserName";
                dgvManageUsers.Columns[3].Width = 120;

                dgvManageUsers.Columns[4].HeaderText = "Is Active";
                dgvManageUsers.Columns[4].Width = 120;
            }
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cbManageUsersFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbManageUsersFilter.Text == "Is Active")
            {
                mtxtFilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else
            {
                mtxtFilter.Visible = (cbManageUsersFilter.Text != "None");
                cbIsActive.Visible = false;

                mtxtFilter.Text = "";
                mtxtFilter.Focus();
            }
        }

        private void mtxtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbManageUsersFilter.SelectedItem.ToString())
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (mtxtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvManageUsers.RowCount.ToString();
                return;
            }

            if (FilterColumn == "UserID" || FilterColumn == "PersonID")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, mtxtFilter.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, mtxtFilter.Text.Trim());

            lblTotalRecords.Text = dgvManageUsers.RowCount.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(-1);
            frm.ShowDialog();

            _LoadData();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(-1);
            frm.ShowDialog();

            _LoadData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvManageUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvManageUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvManageUsers.CurrentRow.Cells[0].Value;


            if (MessageBox.Show("Are you sure you want to delete User [" + UserID.ToString()
                + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User Deleted Successfully.", "Successful", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    frmManageUsers_Load(null, null);
                }
                else
                {
                    MessageBox.Show("User is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvManageUsers.CurrentRow.Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void mtxtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbManageUsersFilter.Text == "Person ID" || cbManageUsersFilter.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
