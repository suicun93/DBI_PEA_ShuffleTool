using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Model;
using DBI_ShuffleTool.Utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DBI_ShuffleTool.UI
{
    public partial class ShuffleTool : Form
    {
        ShuffleExamModel _sem;
        QuestionsBank _qb;
        String _outputPath;

        public ShuffleTool()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            String inputPath = FileUtils.GetFileLocation();
            txtLocationFolderInput.Text = inputPath;
            //Reading data
            _qb = new QuestionsBank(JsonUtils.DeserializeJson(inputPath));
            //Print result on txtLoadFileResult
            String resImported = "Questions imported: " + _qb.QBank.Count;
            int i = 0;
            foreach (Question question in _qb.QBank)
            {
                resImported = resImported + "\nQ" + (++i) + ": " + question.Candidates.Count + " candidates";
                foreach (Candidate candidate in question.Candidates)
                {
                    candidate.Point = question.Point;
                }
            }
            txtLoadFileResult.Text = resImported;
            txtNumberOfTest.Value = MaxNumberOfTests();
            txtNumberOfTest.Maximum = MaxNumberOfTests();
        }

        public int MaxNumberOfTests()
        {
            int count = 1;
            foreach (Question question in _qb.QBank)
            {
                count *= question.Candidates.Count;
            }
            count /= 2;
            if ((count) < 1) count = 1;
            return count;
        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            //Create Test
            int numOfPage = Convert.ToInt32(txtNumberOfTest.Value);
            _sem = new ShuffleExamModel(_qb, numOfPage);
            _outputPath = FileUtils.SaveFileToLocation();
            using (AlertForm progress = new AlertForm(CreateTests))
            {
                progress.ShowDialog(this);
            }
        }

        void CreateTests()
        {
            
            String path = FileUtils.CreateNewDirectory(_outputPath, "DBI_Exam");
            DocUtils.ExportDoc(path, _sem.GetExamItemsList());
            JsonUtils.WriteJson(_sem.GetExamItemsList(), path);
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_outputPath);
        }

        private void btnSaveTestsAs_Click(object sender, EventArgs e)
        {
            _outputPath = FileUtils.SaveFileToLocation();
            DocUtils.ExportDoc(_outputPath, _sem.GetExamItemsList());
            using (AlertForm progress = new AlertForm(CreateTests))
            {
                progress.ShowDialog(this);
            }
        }



        private void txtNumberOfTest_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ShuffleTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
