namespace DVLD.Tests
{
    partial class frmListTestAppointments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTestAppointmentTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvVisionAppointments = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.pbTestAppointmentImage = new System.Windows.Forms.PictureBox();
            this.ctrlDrivingLicenceAndApplicationBasicInfo1 = new DVLD.Controls.ctrlDrivingLicenceAndApplicationBasicInfo();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisionAppointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestAppointmentImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestAppointmentTitle
            // 
            this.lblTestAppointmentTitle.AutoSize = true;
            this.lblTestAppointmentTitle.Font = new System.Drawing.Font("Cascadia Mono", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestAppointmentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTestAppointmentTitle.Location = new System.Drawing.Point(280, 165);
            this.lblTestAppointmentTitle.Name = "lblTestAppointmentTitle";
            this.lblTestAppointmentTitle.Size = new System.Drawing.Size(425, 39);
            this.lblTestAppointmentTitle.TabIndex = 0;
            this.lblTestAppointmentTitle.Text = "Vision Test Appointments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 603);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Appointments:";
            // 
            // dgvVisionAppointments
            // 
            this.dgvVisionAppointments.AllowUserToAddRows = false;
            this.dgvVisionAppointments.AllowUserToDeleteRows = false;
            this.dgvVisionAppointments.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVisionAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisionAppointments.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvVisionAppointments.Location = new System.Drawing.Point(16, 640);
            this.dgvVisionAppointments.Name = "dgvVisionAppointments";
            this.dgvVisionAppointments.ReadOnly = true;
            this.dgvVisionAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisionAppointments.Size = new System.Drawing.Size(927, 163);
            this.dgvVisionAppointments.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 80);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 806);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "# Records:";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(117, 806);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(18, 20);
            this.lblRecordCount.TabIndex = 6;
            this.lblRecordCount.Text = "0";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(793, 809);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Image = global::DVLD.Properties.Resources.AddAppointment_32;
            this.btnAddAppointment.Location = new System.Drawing.Point(877, 591);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(66, 43);
            this.btnAddAppointment.TabIndex = 7;
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // pbTestAppointmentImage
            // 
            this.pbTestAppointmentImage.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pbTestAppointmentImage.Location = new System.Drawing.Point(373, 27);
            this.pbTestAppointmentImage.Name = "pbTestAppointmentImage";
            this.pbTestAppointmentImage.Size = new System.Drawing.Size(162, 124);
            this.pbTestAppointmentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestAppointmentImage.TabIndex = 1;
            this.pbTestAppointmentImage.TabStop = false;
            // 
            // ctrlDrivingLicenceAndApplicationBasicInfo1
            // 
            this.ctrlDrivingLicenceAndApplicationBasicInfo1.Location = new System.Drawing.Point(12, 231);
            this.ctrlDrivingLicenceAndApplicationBasicInfo1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlDrivingLicenceAndApplicationBasicInfo1.Name = "ctrlDrivingLicenceAndApplicationBasicInfo1";
            this.ctrlDrivingLicenceAndApplicationBasicInfo1.Size = new System.Drawing.Size(944, 354);
            this.ctrlDrivingLicenceAndApplicationBasicInfo1.TabIndex = 2;
            // 
            // frmListTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 862);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvVisionAppointments);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlDrivingLicenceAndApplicationBasicInfo1);
            this.Controls.Add(this.pbTestAppointmentImage);
            this.Controls.Add(this.lblTestAppointmentTitle);
            this.Name = "frmListTestAppointments";
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.frmListTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisionAppointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestAppointmentImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestAppointmentTitle;
        private System.Windows.Forms.PictureBox pbTestAppointmentImage;
        private Controls.ctrlDrivingLicenceAndApplicationBasicInfo ctrlDrivingLicenceAndApplicationBasicInfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvVisionAppointments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}