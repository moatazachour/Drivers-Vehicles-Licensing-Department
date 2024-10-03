namespace DVLD.Tests.Vision_Test
{
    partial class frmScheduleAddEditRetakeTest
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
            this.ctrlScheduleTestAppointment1 = new DVLD.Tests.ctrlScheduleTestAppointment();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlScheduleTestAppointment1
            // 
            this.ctrlScheduleTestAppointment1.Location = new System.Drawing.Point(9, 12);
            this.ctrlScheduleTestAppointment1.Name = "ctrlScheduleTestAppointment1";
            this.ctrlScheduleTestAppointment1.Size = new System.Drawing.Size(583, 681);
            this.ctrlScheduleTestAppointment1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(457, 699);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 34);
            this.btnClose.TabIndex = 52;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmScheduleAddEditRetakeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 750);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlScheduleTestAppointment1);
            this.Name = "frmScheduleAddEditRetakeTest";
            this.Text = "frmScheduleAddEditRetakeVisionTest";
            this.Load += new System.EventHandler(this.frmScheduleAddEditRetakeTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlScheduleTestAppointment ctrlScheduleTestAppointment1;
        private System.Windows.Forms.Button btnClose;
    }
}