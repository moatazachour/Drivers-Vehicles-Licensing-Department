using DVLD.People;
using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }             
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Enabled = _ShowAddPerson;
            }
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
                gbFilter.Enabled = value;
            }
        }


        public void LoadPersonInfo(int PersonID)
        {
            cmbFindBy.SelectedIndex = 1;
            txtFind.Text = PersonID.ToString();
            FindNow();
        }

        private void FindNow()
        {
            if (cmbFindBy.SelectedItem.ToString() == "Person ID")
                ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFind.Text));
            else
                ctrlPersonCard1.LoadPersonInfo(txtFind.Text);

            // Raise Event
            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected (ctrlPersonCard1.PersonID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(-1);

            frm.DataBack += frmAddEditPerson_DataBack; // Subscribe To The Event

            frm.ShowDialog();
        }

        private void frmAddEditPerson_DataBack(int PersonID)
        {
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            cmbFindBy.SelectedIndex = 0;
            txtFind.Text = PersonID.ToString();

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void cmbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFind.Text = "";
            txtFind.Focus();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cmbFindBy.SelectedIndex = 0;
            txtFind.Focus();

            //this.BeginInvoke((MethodInvoker)delegate
            //{
            //    txtFind.Focus();
            //});
        }

        private void txtFind_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFind, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFind, null);
            }
        }

        public void FilterFocus()
        {
            txtFind.Focus();
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }

            if (cmbFindBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
