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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            dgvManageTestTypes.DataSource = clsTestTypes.GetAllTestTypes();
            lblRecordCount.Text = dgvManageTestTypes.RowCount.ToString();

            if (dgvManageTestTypes.Rows.Count > 0 )
            {
                dgvManageTestTypes.Columns[0].HeaderText = "ID";
                dgvManageTestTypes.Columns[0].Width = 120;

                dgvManageTestTypes.Columns[1].HeaderText = "Title";
                dgvManageTestTypes.Columns[1].Width = 200;

                dgvManageTestTypes.Columns[2].HeaderText = "Description";
                dgvManageTestTypes.Columns[2].Width = 400;

                dgvManageTestTypes.Columns[3].HeaderText = "Fees";
                dgvManageTestTypes.Columns[3].Width = 100;
            }
            
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((clsTestTypes.enTestType)dgvManageTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadData();
        }
    }
}
