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
        ShuffleExamModel sem;
        QuestionsBank qb;
        String outputPath;

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
            qb = new QuestionsBank(JsonUtils.DeserializeJson(inputPath));
            //Print result on txtLoadFileResult
            String resImported = "Questions imported: " + qb.QBank.Count;
            int i = 0;
            foreach (Question question in qb.QBank)
            {
                resImported = resImported + "\nQ" + (++i) + ": " + question.Candidates.Count + " candidates";
            }
            txtLoadFileResult.Text = resImported;
            txtNumberOfTest.Value = MaxNumberOfTests();
            txtNumberOfTest.Maximum = MaxNumberOfTests();
        }

        public int MaxNumberOfTests()
        {
            int count = 1;
            foreach (Question question in qb.QBank)
            {
                count *= question.Candidates.Count;
            }
            return count;
        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            //Create Test
            int numOfPage = Convert.ToInt32(txtNumberOfTest.Value);
            sem = new ShuffleExamModel(qb, numOfPage);
            CreateTests(btnCreateTests);
        }

        private void CreateTests(Button btn)
        {
            String old = btn.Text;
            btn.Text = "Creating...";

            outputPath = FileUtils.SaveFileToLocation();
            String path = FileUtils.CreateNewDirectory(outputPath, "DBI_Exam");
            DocUtils.ExportDoc(path, sem.GetExamItemsList());
            btn.Text = old;
            if (JsonUtils.WriteJson(sem.GetExamItemsList(), path))
            {
                MessageBox.Show(ConstantUtils.MESSAGE_CREATED_FOLDER_OK);
            }
            else
            {
                MessageBox.Show(ConstantUtils.ERROR_COMMON);
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(outputPath);
        }

        private void btnSaveTestsAs_Click(object sender, EventArgs e)
        {
            outputPath = FileUtils.SaveFileToLocation();
            DocUtils.ExportDoc(outputPath, sem.GetExamItemsList());
            CreateTests(btnSaveTestsAs);
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
