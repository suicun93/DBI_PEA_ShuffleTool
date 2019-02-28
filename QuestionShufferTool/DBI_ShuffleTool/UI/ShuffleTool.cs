using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Model;
using DBI_ShuffleTool.Utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using DBI_ShuffleTool.Utils.Doc;

namespace DBI_ShuffleTool.UI
{
    public partial class ShuffleTool : Form
    {
        ShuffleExamModel _sem;
        List<Question> _qb;
        string _outputPath;
        bool BeingDragged = false;
        int MouseDownX;
        int MouseDownY;

        public ShuffleTool()
        {
            InitializeComponent();
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = FileUtils.GetFileLocation();
                txtLocationFolderInput.Text = inputPath;
                //Reading data
                _qb = new List<Question>();
                _qb = JsonUtils.DeserializeJson(inputPath);
                //Print result on txtLoadFileResult
                string resImported = "Questions imported: " + _qb.Count;
                int i = 0;
                foreach (Question question in _qb)
                {
                    resImported = resImported + "\nQ" + (++i) + ": " + question.Candidates.Count + " candidate(s)";
                    foreach (Candidate candidate in question.Candidates)
                    {
                        candidate.Point = question.Point;
                    }
                }
                txtLoadFileResult.Text = resImported;
                txtNumberOfTest.Maximum = MaxNumberOfTests();
                txtNumberOfTest.Value = MaxNumberOfTests();
                btnCreateTests.Visible = true;
                btnPreview.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show(ConstantUtils.ErrorLoadFolderFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        public int MaxNumberOfTests()
        {
            int count = 1;
            foreach (Question question in _qb)
            {
                if(question == null || question.Candidates.Count == 0)
                {
                    continue;
                }
                count *= question.Candidates.Count;
            }
            if ((count) < 1) count = 1;
            return count;
        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            try
            {
                string location = FileUtils.SaveFileToLocation();
                if (string.IsNullOrEmpty(location))
                {
                    MessageBox.Show(ConstantUtils.ErrorLoadFolderFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _outputPath = location;
                //Create Test
                int numOfPage = Convert.ToInt32(txtNumberOfTest.Value);
                _sem = new ShuffleExamModel(_qb, numOfPage);
                using (AlertForm progress = new AlertForm(CreateTests))
                {
                    progress.ShowDialog(this);
                }
                btnOpenFolder.Visible = true;
                btnSaveTestsAs.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show(ConstantUtils.ErrorLoadFolderFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        void CreateTests()
        {
            string path = FileUtils.CreateNewDirectory(_outputPath, "DBI_Exam");
            ExportDocUtils.ExportDoc(path, _sem.EiListDoc);
            JsonUtils.WriteJson(_sem.EiListMarking, path);
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_outputPath + @"/DBI_Exam/");
        }

        private void btnSaveTestsAs_Click(object sender, EventArgs e)
        {
            try
            {
                string location = FileUtils.SaveFileToLocation();
                if (string.IsNullOrEmpty(location))
                {
                    MessageBox.Show(ConstantUtils.ErrorLoadFolderFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _outputPath = location;
                ExportDocUtils.ExportDoc(_outputPath, _sem.EiListDoc);
                using (AlertForm progress = new AlertForm(CreateTests))
                {
                    progress.ShowDialog(this);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConstantUtils.ErrorLoadFolderFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PreviewDocUtils.PreviewCandidatesSet(_qb);
            }
            catch (Exception)
            {
                MessageBox.Show(ConstantUtils.ErrorCommon, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShuffleTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Hover(object sender, EventArgs e)
        {
            btnMinimize.Image = Properties.Resources.minimize_hover_red;
        }

        private void btnClose_Hover(object sender, EventArgs e)
        {
            btnClose.Image = Properties.Resources.close_hover_red;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.Image = Properties.Resources.close;
        }

        private void btnMinimize_Leave(object sender, EventArgs e)
        {
            btnMinimize.Image = Properties.Resources.minimize;
        }

        private void controlBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BeingDragged = true;
                MouseDownX = e.X;
                MouseDownY = e.Y;
            }
        }

        private void controlBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (BeingDragged)
            {
                Point tmpPoint = new Point(Location.X + (e.X - MouseDownX),
                    Location.Y + (e.Y - MouseDownY));
                Location = tmpPoint;
            }
        }

        private void controlBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BeingDragged = false;
            }
        }

        private void btnPreview_MouseHover(object sender, EventArgs e)
        {
            btnPreview.SetBounds(btnPreview.Location.X - 2, btnPreview.Location.Y - 2, 28, 28);
            btnPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnPreview_MouseLeave(object sender, EventArgs e)
        {
            btnPreview.SetBounds(btnPreview.Location.X + 2, btnPreview.Location.Y + 2, 24, 24);
        }

        
    }
}
