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
    public partial class frmManageApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;

        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtAllApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvManageApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordCount.Text = dgvManageApplicationTypes.RowCount.ToString();

            if (dgvManageApplicationTypes.Rows.Count > 0 )
            {
                dgvManageApplicationTypes.Columns[0].HeaderText = "ID";
                dgvManageApplicationTypes.Columns[0].Width = 110;

                dgvManageApplicationTypes.Columns[1].HeaderText = "Title";
                dgvManageApplicationTypes.Columns[1].Width = 400;

                dgvManageApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvManageApplicationTypes.Columns[2].Width = 100;
            }
            
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvManageApplicationTypes.CurrentRow.Cells[0].Value);  
            frm.ShowDialog();

            _LoadData();
        }
    }
}
