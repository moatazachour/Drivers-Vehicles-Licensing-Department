namespace DVLD.Controls
{
    partial class ctrlDriverLicenseCardWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnLicenseSearch = new System.Windows.Forms.Button();
            this.mtxtLicenseID = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ctrlDriverLicenseCard1 = new DVLD.Controls.ctrlDriverLicenseCard();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnLicenseSearch);
            this.gbFilter.Controls.Add(this.mtxtLicenseID);
            this.gbFilter.Controls.Add(this.label6);
            this.gbFilter.Location = new System.Drawing.Point(10, 2);
            this.gbFilter.Margin = new System.Windows.Forms.Padding(2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Padding = new System.Windows.Forms.Padding(2);
            this.gbFilter.Size = new System.Drawing.Size(451, 89);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnLicenseSearch
            // 
            this.btnLicenseSearch.Image = global::DVLD.Properties.Resources.License_View_32;
            this.btnLicenseSearch.Location = new System.Drawing.Point(391, 28);
            this.btnLicenseSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnLicenseSearch.Name = "btnLicenseSearch";
            this.btnLicenseSearch.Size = new System.Drawing.Size(44, 38);
            this.btnLicenseSearch.TabIndex = 35;
            this.btnLicenseSearch.UseVisualStyleBackColor = true;
            this.btnLicenseSearch.Click += new System.EventHandler(this.btnLicenseSearch_Click);
            // 
            // mtxtLicenseID
            // 
            this.mtxtLicenseID.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtLicenseID.Location = new System.Drawing.Point(141, 36);
            this.mtxtLicenseID.Margin = new System.Windows.Forms.Padding(2);
            this.mtxtLicenseID.Name = "mtxtLicenseID";
            this.mtxtLicenseID.Size = new System.Drawing.Size(229, 25);
            this.mtxtLicenseID.TabIndex = 34;
            this.mtxtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtLicenseID_KeyPress);
            this.mtxtLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtLicenseID_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "LicenseID:";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(74, 258);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(6, 6);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // ctrlDriverLicenseCard1
            // 
            this.ctrlDriverLicenseCard1.Location = new System.Drawing.Point(0, 97);
            this.ctrlDriverLicenseCard1.Name = "ctrlDriverLicenseCard1";
            this.ctrlDriverLicenseCard1.Size = new System.Drawing.Size(868, 360);
            this.ctrlDriverLicenseCard1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlDriverLicenseCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlDriverLicenseCard1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ctrlDriverLicenseCardWithFilter";
            this.Size = new System.Drawing.Size(860, 456);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDriverLicenseCard ctrlDriverLicenseCard1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLicenseSearch;
        private System.Windows.Forms.MaskedTextBox mtxtLicenseID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
