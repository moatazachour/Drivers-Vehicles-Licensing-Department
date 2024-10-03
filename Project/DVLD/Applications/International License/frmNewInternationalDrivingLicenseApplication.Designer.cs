namespace DVLD.Apps
{
    partial class frmNewInternationalDrivingLicenseApplication
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
            this.lblMode = new System.Windows.Forms.Label();
            this.llShowLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.llShowLicensesInfo = new System.Windows.Forms.LinkLabel();
            this.ctrlInternationalLicenseApplicationInfo1 = new DVLD.Controls.ctrlInternationalLicenseApplicationInfo();
            this.ctrlDriverLicenseCardWithFilter1 = new DVLD.Controls.ctrlDriverLicenseCardWithFilter();
            this.btnIssueInternationalLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Cascadia Mono", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMode.Location = new System.Drawing.Point(158, 17);
            this.lblMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(611, 40);
            this.lblMode.TabIndex = 1;
            this.lblMode.Text = "International License Application";
            // 
            // llShowLicensesHistory
            // 
            this.llShowLicensesHistory.AutoSize = true;
            this.llShowLicensesHistory.Enabled = false;
            this.llShowLicensesHistory.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicensesHistory.Location = new System.Drawing.Point(9, 759);
            this.llShowLicensesHistory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llShowLicensesHistory.Name = "llShowLicensesHistory";
            this.llShowLicensesHistory.Size = new System.Drawing.Size(198, 20);
            this.llShowLicensesHistory.TabIndex = 3;
            this.llShowLicensesHistory.TabStop = true;
            this.llShowLicensesHistory.Text = "Show Licenses History";
            this.llShowLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicensesHistory_LinkClicked);
            // 
            // llShowLicensesInfo
            // 
            this.llShowLicensesInfo.AutoSize = true;
            this.llShowLicensesInfo.Enabled = false;
            this.llShowLicensesInfo.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicensesInfo.Location = new System.Drawing.Point(220, 759);
            this.llShowLicensesInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llShowLicensesInfo.Name = "llShowLicensesInfo";
            this.llShowLicensesInfo.Size = new System.Drawing.Size(162, 20);
            this.llShowLicensesInfo.TabIndex = 4;
            this.llShowLicensesInfo.TabStop = true;
            this.llShowLicensesInfo.Text = "Show License Info";
            this.llShowLicensesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicensesInfo_LinkClicked);
            // 
            // ctrlInternationalLicenseApplicationInfo1
            // 
            this.ctrlInternationalLicenseApplicationInfo1.Location = new System.Drawing.Point(9, 523);
            this.ctrlInternationalLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlInternationalLicenseApplicationInfo1.Name = "ctrlInternationalLicenseApplicationInfo1";
            this.ctrlInternationalLicenseApplicationInfo1.Size = new System.Drawing.Size(842, 226);
            this.ctrlInternationalLicenseApplicationInfo1.TabIndex = 2;
            // 
            // ctrlDriverLicenseCardWithFilter1
            // 
            this.ctrlDriverLicenseCardWithFilter1.FilterEnabled = true;
            this.ctrlDriverLicenseCardWithFilter1.Location = new System.Drawing.Point(9, 63);
            this.ctrlDriverLicenseCardWithFilter1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlDriverLicenseCardWithFilter1.Name = "ctrlDriverLicenseCardWithFilter1";
            this.ctrlDriverLicenseCardWithFilter1.Size = new System.Drawing.Size(865, 456);
            this.ctrlDriverLicenseCardWithFilter1.TabIndex = 0;
            this.ctrlDriverLicenseCardWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlDriverLicenseCardWithFilter1_OnLicenseSelected);
            // 
            // btnIssueInternationalLicense
            // 
            this.btnIssueInternationalLicense.Enabled = false;
            this.btnIssueInternationalLicense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIssueInternationalLicense.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssueInternationalLicense.Image = global::DVLD.Properties.Resources.International_32;
            this.btnIssueInternationalLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueInternationalLicense.Location = new System.Drawing.Point(694, 753);
            this.btnIssueInternationalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.btnIssueInternationalLicense.Name = "btnIssueInternationalLicense";
            this.btnIssueInternationalLicense.Size = new System.Drawing.Size(130, 31);
            this.btnIssueInternationalLicense.TabIndex = 9;
            this.btnIssueInternationalLicense.Text = "Issue";
            this.btnIssueInternationalLicense.UseVisualStyleBackColor = true;
            this.btnIssueInternationalLicense.Click += new System.EventHandler(this.btnIssueInternationalLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(559, 753);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 31);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmNewInternationalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 804);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssueInternationalLicense);
            this.Controls.Add(this.llShowLicensesInfo);
            this.Controls.Add(this.llShowLicensesHistory);
            this.Controls.Add(this.ctrlInternationalLicenseApplicationInfo1);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.ctrlDriverLicenseCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmNewInternationalDrivingLicenseApplication";
            this.Text = "New International License Application";
            this.Activated += new System.EventHandler(this.frmNewInternationalDrivingLicenseApplication_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlDriverLicenseCardWithFilter ctrlDriverLicenseCardWithFilter1;
        private System.Windows.Forms.Label lblMode;
        private Controls.ctrlInternationalLicenseApplicationInfo ctrlInternationalLicenseApplicationInfo1;
        private System.Windows.Forms.LinkLabel llShowLicensesHistory;
        private System.Windows.Forms.LinkLabel llShowLicensesInfo;
        private System.Windows.Forms.Button btnIssueInternationalLicense;
        private System.Windows.Forms.Button btnClose;
    }
}