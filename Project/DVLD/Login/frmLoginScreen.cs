using DVLD_BusinessLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string Username = string.Empty, Password = string.Empty;


            if (clsGlobalSettings.GetStoredCredential(ref Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.Find(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobalSettings.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobalSettings.RememberUsernameAndPassword("", "");
                }

                if (!user.IsActive)
                {

                    txtUsername.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobalSettings.CurrentUserID = user.UserID; 
                clsGlobalSettings.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }

            else
            {
                MessageBox.Show("Invalid Username/Password", "Wrong Credintials", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                txtUsername.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
