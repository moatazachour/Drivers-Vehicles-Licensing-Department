using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD.Controls
{
    public partial class ctrlUserCard : UserControl
    {

        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(UserID);

            if (_User == null)
            {
                _ResetUserCard();
                MessageBox.Show("User with UserID = " +  UserID.ToString() + "doesn\'t exist", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _FillUserInfo();
            
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);

            _UserID = _User.UserID;
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;

            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";
        }

        private void _ResetUserCard()
        {
            ctrlPersonCard1._ResetPersonCard();

            lblUserID.Text = "???";
            lblUserName.Text = "???";
            lblIsActive.Text = "???";
        }

    }
}
