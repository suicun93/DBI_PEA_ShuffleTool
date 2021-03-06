﻿using DBI_ShuffleTool.Utils;

namespace DBI_ShuffleTool.UI
{
    partial class ShuffleToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShuffleToolForm));
            this.panelChoose = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.PictureBox();
            this.txtLocationFolderInput = new System.Windows.Forms.TextBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.txtLoadFileResult = new System.Windows.Forms.RichTextBox();
            this.btnImportQuestionSet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.controlBar = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNumberOfTest = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreateTests = new System.Windows.Forms.Button();
            this.toolTipPreview = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelChoose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfTest)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChoose
            // 
            this.panelChoose.AccessibleDescription = "";
            this.panelChoose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelChoose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChoose.Controls.Add(this.btnPreview);
            this.panelChoose.Controls.Add(this.txtLocationFolderInput);
            this.panelChoose.Controls.Add(this.btnMinimize);
            this.panelChoose.Controls.Add(this.btnClose);
            this.panelChoose.Controls.Add(this.txtLoadFileResult);
            this.panelChoose.Controls.Add(this.btnImportQuestionSet);
            this.panelChoose.Controls.Add(this.label2);
            this.panelChoose.Controls.Add(this.lblLocation);
            this.panelChoose.Controls.Add(this.controlBar);
            this.panelChoose.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChoose.Location = new System.Drawing.Point(0, 0);
            this.panelChoose.Name = "panelChoose";
            this.panelChoose.Size = new System.Drawing.Size(321, 290);
            this.panelChoose.TabIndex = 0;
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.White;
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.Image = global::DBI_ShuffleTool.Properties.Resources.preview1;
            this.btnPreview.Location = new System.Drawing.Point(262, 133);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(0);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(24, 24);
            this.btnPreview.TabIndex = 12;
            this.btnPreview.TabStop = false;
            this.toolTipPreview.SetToolTip(this.btnPreview, "You can check the details of Candidate Package here!");
            this.btnPreview.Visible = false;
            this.btnPreview.VisibleChanged += new System.EventHandler(this.btnPreview_VisibleChanged);
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            this.btnPreview.MouseEnter += new System.EventHandler(this.btnPreview_MouseEnter);
            this.btnPreview.MouseLeave += new System.EventHandler(this.btnPreview_MouseLeave);
            // 
            // txtLocationFolderInput
            // 
            this.txtLocationFolderInput.BackColor = System.Drawing.Color.White;
            this.txtLocationFolderInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocationFolderInput.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocationFolderInput.ForeColor = System.Drawing.Color.DimGray;
            this.txtLocationFolderInput.Location = new System.Drawing.Point(12, 65);
            this.txtLocationFolderInput.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtLocationFolderInput.Name = "txtLocationFolderInput";
            this.txtLocationFolderInput.ReadOnly = true;
            this.txtLocationFolderInput.Size = new System.Drawing.Size(201, 22);
            this.txtLocationFolderInput.TabIndex = 1;
            this.txtLocationFolderInput.TabStop = false;
            this.txtLocationFolderInput.Text = "Please import Question Set";
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.InitialImage = null;
            this.btnMinimize.Location = new System.Drawing.Point(282, 7);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(16, 16);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_Leave);
            this.btnMinimize.MouseHover += new System.EventHandler(this.btnMinimize_Hover);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.InitialImage = null;
            this.btnClose.Location = new System.Drawing.Point(302, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_Leave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_Hover);
            // 
            // txtLoadFileResult
            // 
            this.txtLoadFileResult.BackColor = System.Drawing.Color.White;
            this.txtLoadFileResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoadFileResult.Font = new System.Drawing.Font("Dubai", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoadFileResult.Location = new System.Drawing.Point(12, 123);
            this.txtLoadFileResult.Name = "txtLoadFileResult";
            this.txtLoadFileResult.ReadOnly = true;
            this.txtLoadFileResult.Size = new System.Drawing.Size(293, 155);
            this.txtLoadFileResult.TabIndex = 10;
            this.txtLoadFileResult.TabStop = false;
            this.txtLoadFileResult.Text = "";
            // 
            // btnImportQuestionSet
            // 
            this.btnImportQuestionSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnImportQuestionSet.FlatAppearance.BorderSize = 0;
            this.btnImportQuestionSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportQuestionSet.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportQuestionSet.ForeColor = System.Drawing.Color.White;
            this.btnImportQuestionSet.Location = new System.Drawing.Point(233, 59);
            this.btnImportQuestionSet.Name = "btnImportQuestionSet";
            this.btnImportQuestionSet.Size = new System.Drawing.Size(75, 32);
            this.btnImportQuestionSet.TabIndex = 0;
            this.btnImportQuestionSet.Text = "Import";
            this.btnImportQuestionSet.UseVisualStyleBackColor = false;
            this.btnImportQuestionSet.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import Result";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.BackColor = System.Drawing.SystemColors.Control;
            this.lblLocation.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(12, 40);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(152, 22);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Question Set File Location";
            // 
            // controlBar
            // 
            this.controlBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.controlBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlBar.Font = new System.Drawing.Font("Dubai", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlBar.ForeColor = System.Drawing.Color.White;
            this.controlBar.Location = new System.Drawing.Point(0, 0);
            this.controlBar.Name = "controlBar";
            this.controlBar.Size = new System.Drawing.Size(319, 30);
            this.controlBar.TabIndex = 11;
            this.controlBar.Text = "Shuffle Questions Tool";
            this.controlBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.controlBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlBar_MouseDown);
            this.controlBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.controlBar_MouseMove);
            this.controlBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.controlBar_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtNumberOfTest);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnCreateTests);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 86);
            this.panel1.TabIndex = 1;
            // 
            // txtNumberOfTest
            // 
            this.txtNumberOfTest.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfTest.Location = new System.Drawing.Point(12, 37);
            this.txtNumberOfTest.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtNumberOfTest.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumberOfTest.Name = "txtNumberOfTest";
            this.txtNumberOfTest.Size = new System.Drawing.Size(201, 29);
            this.txtNumberOfTest.TabIndex = 2;
            this.txtNumberOfTest.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of test";
            // 
            // btnCreateTests
            // 
            this.btnCreateTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCreateTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTests.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTests.ForeColor = System.Drawing.Color.White;
            this.btnCreateTests.Location = new System.Drawing.Point(233, 37);
            this.btnCreateTests.Name = "btnCreateTests";
            this.btnCreateTests.Size = new System.Drawing.Size(75, 30);
            this.btnCreateTests.TabIndex = 2;
            this.btnCreateTests.Text = "Create Test(s)";
            this.btnCreateTests.UseVisualStyleBackColor = false;
            this.btnCreateTests.Visible = false;
            this.btnCreateTests.Click += new System.EventHandler(this.BtnCreateTests_Click);
            // 
            // ShuffleToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 371);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelChoose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShuffleToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShuffleTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShuffleTool_FormClosed);
            this.panelChoose.ResumeLayout(false);
            this.panelChoose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChoose;
        private System.Windows.Forms.RichTextBox txtLoadFileResult;
        private System.Windows.Forms.Button btnImportQuestionSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocationFolderInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreateTests;
        private System.Windows.Forms.NumericUpDown txtNumberOfTest;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.Label controlBar;
        private System.Windows.Forms.PictureBox btnPreview;
        private System.Windows.Forms.ToolTip toolTipPreview;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}