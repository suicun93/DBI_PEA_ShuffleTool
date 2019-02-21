namespace DBI_ShuffleTool.UI
{
    partial class ShuffleTool
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
            this.panelChoose = new System.Windows.Forms.Panel();
            this.txtLoadFileResult = new System.Windows.Forms.RichTextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocationFolderInput = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNumberOfTest = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnSaveTestsAs = new System.Windows.Forms.Button();
            this.btnCreateTests = new System.Windows.Forms.Button();
            this.panelChoose.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfTest)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChoose
            // 
            this.panelChoose.AccessibleDescription = "";
            this.panelChoose.Controls.Add(this.txtLoadFileResult);
            this.panelChoose.Controls.Add(this.btnBrowse);
            this.panelChoose.Controls.Add(this.label2);
            this.panelChoose.Controls.Add(this.lblLocation);
            this.panelChoose.Controls.Add(this.txtLocationFolderInput);
            this.panelChoose.Location = new System.Drawing.Point(12, 12);
            this.panelChoose.Name = "panelChoose";
            this.panelChoose.Size = new System.Drawing.Size(418, 175);
            this.panelChoose.TabIndex = 0;
            // 
            // txtLoadFileResult
            // 
            this.txtLoadFileResult.Enabled = false;
            this.txtLoadFileResult.Location = new System.Drawing.Point(88, 55);
            this.txtLoadFileResult.Name = "txtLoadFileResult";
            this.txtLoadFileResult.ReadOnly = true;
            this.txtLoadFileResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.txtLoadFileResult.Size = new System.Drawing.Size(201, 109);
            this.txtLoadFileResult.TabIndex = 3;
            this.txtLoadFileResult.TabStop = false;
            this.txtLoadFileResult.Text = "";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(309, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import Result";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(16, 18);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(48, 13);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location";
            // 
            // txtLocationFolderInput
            // 
            this.txtLocationFolderInput.Enabled = false;
            this.txtLocationFolderInput.Location = new System.Drawing.Point(88, 15);
            this.txtLocationFolderInput.Name = "txtLocationFolderInput";
            this.txtLocationFolderInput.ReadOnly = true;
            this.txtLocationFolderInput.Size = new System.Drawing.Size(201, 20);
            this.txtLocationFolderInput.TabIndex = 2;
            this.txtLocationFolderInput.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNumberOfTest);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOpenFolder);
            this.panel1.Controls.Add(this.btnSaveTestsAs);
            this.panel1.Controls.Add(this.btnCreateTests);
            this.panel1.Location = new System.Drawing.Point(13, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 110);
            this.panel1.TabIndex = 1;
            // 
            // txtNumberOfTest
            // 
            this.txtNumberOfTest.Location = new System.Drawing.Point(88, 18);
            this.txtNumberOfTest.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtNumberOfTest.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumberOfTest.Name = "txtNumberOfTest";
            this.txtNumberOfTest.Size = new System.Drawing.Size(120, 20);
            this.txtNumberOfTest.TabIndex = 3;
            this.txtNumberOfTest.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNumberOfTest.ValueChanged += new System.EventHandler(this.txtNumberOfTest_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Test";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(248, 60);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 2;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnSaveTestsAs
            // 
            this.btnSaveTestsAs.Location = new System.Drawing.Point(168, 60);
            this.btnSaveTestsAs.Name = "btnSaveTestsAs";
            this.btnSaveTestsAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveTestsAs.TabIndex = 2;
            this.btnSaveTestsAs.Text = "Save as";
            this.btnSaveTestsAs.UseVisualStyleBackColor = true;
            this.btnSaveTestsAs.Click += new System.EventHandler(this.btnSaveTestsAs_Click);
            // 
            // btnCreateTests
            // 
            this.btnCreateTests.Location = new System.Drawing.Point(88, 60);
            this.btnCreateTests.Name = "btnCreateTests";
            this.btnCreateTests.Size = new System.Drawing.Size(75, 23);
            this.btnCreateTests.TabIndex = 2;
            this.btnCreateTests.Text = "Create Tests";
            this.btnCreateTests.UseVisualStyleBackColor = true;
            this.btnCreateTests.Click += new System.EventHandler(this.btnCreateTests_Click);
            // 
            // ShuffleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 316);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelChoose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ShuffleTool";
            this.Text = "ShuffleTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShuffleTool_FormClosed);
            this.panelChoose.ResumeLayout(false);
            this.panelChoose.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChoose;
        private System.Windows.Forms.RichTextBox txtLoadFileResult;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocationFolderInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreateTests;
        private System.Windows.Forms.NumericUpDown txtNumberOfTest;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnSaveTestsAs;
    }
}